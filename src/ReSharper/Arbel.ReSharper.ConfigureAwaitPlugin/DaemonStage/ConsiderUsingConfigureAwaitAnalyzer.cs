using System;
using JetBrains.ReSharper.Daemon.CSharp.Stages;
#if RS_V8
using JetBrains.ReSharper.Daemon.Stages;
#endif
using JetBrains.ReSharper.Daemon.Stages.Dispatcher;
#if RS_V9
using JetBrains.ReSharper.Feature.Services.Daemon;
#endif
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace Arbel.ReSharper.ConfigureAwaitPlugin.DaemonStage
{
    [ElementProblemAnalyzer(typeof(IAwaitExpression), HighlightingTypes = new [] { typeof(ConsiderUsingConfigureAwaitHighlighting) })]
    public sealed class ConsiderUsingConfigureAwaitAnalyzer : ElementProblemAnalyzer<IAwaitExpression>
    {
        private const string TaskTypeName = "System.Threading.Tasks.Task";
        private const string TaskOfTTypeName = "System.Threading.Tasks.Task`1";

        protected override void Run(IAwaitExpression element, ElementProblemAnalyzerData data, IHighlightingConsumer consumer)
        {
            if (element.Task != null)
            {
                var type = element.Task.GetExpressionType() as IDeclaredType;
                if (type != null && IsTaskType(type))
                {
                    consumer.AddHighlighting(new ConsiderUsingConfigureAwaitHighlighting(element));
                }
            }
        }

        private static bool IsTaskType(IDeclaredType type)
        {
            var clrName = type.GetClrName().FullName;
            return string.Equals(clrName, TaskTypeName, StringComparison.Ordinal) ||
                   string.Equals(clrName, TaskOfTTypeName, StringComparison.Ordinal);
        }
    }
}
