using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using CSharp = Microsoft.CodeAnalysis.CSharp;
using CSharpSyntax = Microsoft.CodeAnalysis.CSharp.Syntax;
using VisualBasic = Microsoft.CodeAnalysis.VisualBasic;
using VisualBasicSyntax = Microsoft.CodeAnalysis.VisualBasic.Syntax;

namespace Arbel.Roslyn.ConfigureAwaitChecker
{
    [DiagnosticAnalyzer]
    public class ConfigureAwaitAnalyzer : DiagnosticAnalyzer
    {
        public const string DiagnosticId = "AwaitWithoutConfigureAwait";

        internal static DiagnosticDescriptor Rule = new DiagnosticDescriptor(
            id: DiagnosticId,
            title: "Await used without ConfigureAwait",
            messageFormat: "Await used without ConfigureAwait",
            category: "Usage",
            defaultSeverity: DiagnosticSeverity.Warning,
            isEnabledByDefault: true);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics
        {
            get { return ImmutableArray.Create(Rule); }
        }

        public override void Initialize(AnalysisContext context)
        {
            context.RegisterSyntaxNodeAction(AnalyzeCSharpSymbol, CSharp.SyntaxKind.AwaitExpression);
            context.RegisterSyntaxNodeAction(AnalyzeVisualBasicSymbol, VisualBasic.SyntaxKind.AwaitExpression);
        }

        private void AnalyzeCSharpSymbol(SyntaxNodeAnalysisContext context)
        {
            var p = (CSharpSyntax.AwaitExpressionSyntax)context.Node;
            if (p.Expression != null)
            {
                TryReportDiagnostic(context, p, p.Expression);
            }
        }

        private void AnalyzeVisualBasicSymbol(SyntaxNodeAnalysisContext context)
        {
            var p = (VisualBasicSyntax.AwaitExpressionSyntax)context.Node;
            if (p.Expression != null)
            {
                TryReportDiagnostic(context, p, p.Expression);
            }
        }

        private static void TryReportDiagnostic(SyntaxNodeAnalysisContext context, SyntaxNode parent, SyntaxNode node)
        {
            var type = context.SemanticModel.GetTypeInfo(node).Type;
            if (type.ContainingNamespace.ToString() == "System.Threading.Tasks" && type.Name == "Task")
            {
                context.ReportDiagnostic(Diagnostic.Create(Rule, parent.GetLocation()));
            }
        }
    }
}
