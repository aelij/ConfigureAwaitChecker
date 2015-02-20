using Microsoft.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace Arbel.Roslyn.ConfigureAwaitChecker
{
    internal static class DocumentExtensions
    {
        public static async Task<Document> ReplaceAsync(this Document document, SyntaxNode oldSyntax,
            SyntaxNode newSyntax, CancellationToken cancellationToken)
        {
            var oldRoot = await document.GetSyntaxRootAsync(cancellationToken).ConfigureAwait(false);
            var newRoot = oldRoot.ReplaceNode(oldSyntax, newSyntax);
            var newDocument = document.WithSyntaxRoot(newRoot);
            return newDocument;
        }
    }
}
