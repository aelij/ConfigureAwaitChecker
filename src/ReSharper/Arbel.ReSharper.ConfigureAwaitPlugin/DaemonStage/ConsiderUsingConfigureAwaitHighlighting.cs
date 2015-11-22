using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using Arbel.ReSharper.ConfigureAwaitPlugin.DaemonStage;
using JetBrains.DocumentModel;
#if RS_V8
using JetBrains.ReSharper.Daemon.Impl;
#endif
#if RS_V9
using JetBrains.ReSharper.Feature.Services.Daemon;
#endif

[assembly: RegisterConfigurableSeverity(ConsiderUsingConfigureAwaitHighlighting.SeverityId,
  null,
  HighlightingGroupIds.BestPractice,
  "Consider adding ConfigureAwait",
  "Library code should use ConfigureAwait with every await. Always specifying ConfigureAwait makes it clearer how the continuation is invoked and avoids synchonization bugs.",
  Severity.SUGGESTION,
  false)]

namespace Arbel.ReSharper.ConfigureAwaitPlugin.DaemonStage
{
    [ConfigurableSeverityHighlighting(SeverityId, CSharpLanguage.Name, OverlapResolve = OverlapResolveKind.WARNING)]
    public sealed class ConsiderUsingConfigureAwaitHighlighting :
#if RS_V8
        IHighlightingWithRange
#endif
#if RS_V9
        IHighlighting
#endif
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