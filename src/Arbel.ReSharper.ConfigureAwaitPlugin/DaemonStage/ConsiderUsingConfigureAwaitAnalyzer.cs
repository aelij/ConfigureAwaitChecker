using System;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace Arbel.ReSharper.ConfigureAwaitPlugin.DaemonStage
{
    [ElementProblemAnalyzer(typeof(IAwaitExpression), HighlightingTypes = new [] { typeof(ConsiderUsingConfigureAwaitHighlighting) })]
    public sealed class ConsiderUsingConfigureAwaitAnalyzer : ElementProblemAnalyzer<IAwaitExpression>
    {
        private const string TaskTypeName = "System.Threading.Tasks.Task";
        private const string TaskOfTTypeName = "System.Threading.Tasks.Task`1";
        private const string ValueTaskTypeName = "System.Threading.Tasks.ValueTask";
        private const string ValueTaskOfTTypeName = "System.Threading.Tasks.ValueTask`1";

        protected override void Run(IAwaitExpression element, ElementProblemAnalyzerData data, IHighlightingConsumer consumer)
        {
            var type = element.Task?.GetExpressionType() as IDeclaredType;
            if (type != null && IsTaskType(type))
            {
                consumer.AddHighlighting(new ConsiderUsingConfigureAwaitHighlighting(element));
            }
        }

        private static bool IsTaskType(IDeclaredType type)
        {
            string typeName = type.GetClrName().FullName;
            return string.Equals(typeName, TaskTypeName, StringComparison.Ordinal) ||
                   string.Equals(typeName, TaskOfTTypeName, StringComparison.Ordinal) ||
                   string.Equals(typeName, ValueTaskTypeName, StringComparison.Ordinal) ||
                   string.Equals(typeName, ValueTaskOfTTypeName, StringComparison.Ordinal);
        }
    }
}
