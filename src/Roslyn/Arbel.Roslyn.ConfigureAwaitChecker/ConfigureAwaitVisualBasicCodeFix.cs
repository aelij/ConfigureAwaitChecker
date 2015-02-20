using System.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.VisualBasic;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using System.Collections.Immutable;

namespace Arbel.Roslyn.ConfigureAwaitChecker
{
    [ExportCodeFixProvider(ConfigureAwaitAnalyzer.DiagnosticId, LanguageNames.VisualBasic), Shared]
    public class ConfigureAwaitVisualBasicCodeFix : CodeFixProvider
    {
        public override FixAllProvider GetFixAllProvider()
        {
            return WellKnownFixAllProviders.BatchFixer;
        }

        public override ImmutableArray<string> GetFixableDiagnosticIds()
        {
            return ImmutableArray.Create(ConfigureAwaitAnalyzer.DiagnosticId);
        }

        public override async Task ComputeFixesAsync(CodeFixContext context)
        {
            var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);
            var diagnostic = context.Diagnostics.First();
            var diagnosticSpan = diagnostic.Location.SourceSpan;

            var declaration = root.FindToken(diagnosticSpan.Start).Parent
                .FirstAncestorOrSelf<AwaitExpressionSyntax>();

            context.RegisterFix(
                CodeAction.Create("Add ConfigureAwait(false)", cancellationToken =>
                    AddConfigureAwait(context.Document, declaration, false, cancellationToken)),
                diagnostic);
            context.RegisterFix(
                CodeAction.Create("Add ConfigureAwait(true)", cancellationToken =>
                    AddConfigureAwait(context.Document, declaration, true, cancellationToken)),
                diagnostic);
        }

        private async Task<Document> AddConfigureAwait(Document document, AwaitExpressionSyntax awaitSyntax, bool value, CancellationToken cancellationToken)
        {
            var oldExpression = awaitSyntax.Expression;
            var newExpression =
                SyntaxFactory.InvocationExpression(
                    SyntaxFactory.MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression, oldExpression.WithoutTrailingTrivia(), SyntaxFactory.Token(SyntaxKind.DotToken),
                        SyntaxFactory.IdentifierName("ConfigureAwait")),
                    SyntaxFactory.ArgumentList(
                        SyntaxFactory.SingletonSeparatedList<ArgumentSyntax>(
                            SyntaxFactory.SimpleArgument(
                                value 
                                ? SyntaxFactory.LiteralExpression(
                                    SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword))
                                : SyntaxFactory.LiteralExpression(
                                    SyntaxKind.FalseLiteralExpression, SyntaxFactory.Token(SyntaxKind.FalseKeyword))))));

            return await document.ReplaceAsync(oldExpression, newExpression, cancellationToken).ConfigureAwait(false);
        }
    }
}
