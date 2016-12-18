using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using Arbel.ReSharper.ConfigureAwaitPlugin.DaemonStage;
using JetBrains.DocumentModel;
using JetBrains.ReSharper.Feature.Services.Daemon;

[assembly: RegisterConfigurableSeverity(
    ConsiderUsingConfigureAwaitHighlighting.SeverityId,
    null,
    HighlightingGroupIds.BestPractice,
    "Consider adding ConfigureAwait",
    "Library code should use ConfigureAwait with every await. Always specifying ConfigureAwait makes it clearer how the continuation is invoked and avoids synchonization bugs.",
    Severity.SUGGESTION)]

namespace Arbel.ReSharper.ConfigureAwaitPlugin.DaemonStage
{
    [ConfigurableSeverityHighlighting(SeverityId, CSharpLanguage.Name, OverlapResolve = OverlapResolveKind.WARNING)]
    public sealed class ConsiderUsingConfigureAwaitHighlighting : IHighlighting
    {
        public const string SeverityId = "ConsiderUsingConfigureAwait";

        public ConsiderUsingConfigureAwaitHighlighting(IAwaitExpression expression)
        {
            Expression = expression;
        }

        public DocumentRange CalculateRange()
        {
            return Expression.GetHighlightingRange();
        }

        public string ToolTip => "Await used without ConfigureAwait";

        public string ErrorStripeToolTip => ToolTip;

        public int NavigationOffsetPatch => 0;

        public bool IsValid()
        {
            return Expression == null || Expression.IsValid();
        }

        public IAwaitExpression Expression { get; }
    }
}