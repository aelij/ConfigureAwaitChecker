using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using Arbel.ReSharper.ConfigureAwaitPlugin.DaemonStage;

[assembly: RegisterConfigurableSeverity(ConsiderUsingConfigureAwaitHighlighting.SeverityId,
  null,
  HighlightingGroupIds.BestPractice,
  "Consider adding ConfigureAwait",
  "Library code should usually call ConfigureAwait ubiquitously. Always specifying ConfigureAwait makes it clearer how the continuation is invoked and avoids synchonization bugs.",
  Severity.SUGGESTION,
  false)]

namespace Arbel.ReSharper.ConfigureAwaitPlugin.DaemonStage
{
    [ConfigurableSeverityHighlighting(SeverityId, CSharpLanguage.Name, OverlapResolve = OverlapResolveKind.WARNING)]
    public sealed class ConsiderUsingConfigureAwaitHighlighting : IHighlighting
    {
        public const string SeverityId = "ConsiderUsingConfigureAwait";

        private readonly IAwaitExpression _expression;

        public ConsiderUsingConfigureAwaitHighlighting(IAwaitExpression expression)
        {
            _expression = expression;
        }

        public string ToolTip
        {
            get { return "Await used without ConfigureAwait"; }
        }

        public string ErrorStripeToolTip
        {
            get { return ToolTip; }
        }

        public int NavigationOffsetPatch
        {
            get { return 0; }
        }

        public bool IsValid()
        {
            return _expression == null || _expression.IsValid();
        }

        public IAwaitExpression Expression
        {
            get { return _expression; }
        }
    }
}