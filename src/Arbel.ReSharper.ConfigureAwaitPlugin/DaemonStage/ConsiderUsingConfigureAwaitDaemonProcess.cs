using System;
using System.Collections.Generic;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Files;
using JetBrains.ReSharper.Psi.Tree;

namespace Arbel.ReSharper.ConfigureAwaitPlugin.DaemonStage
{
    public sealed class ConsiderUsingConfigureAwaitDaemonProcess : IDaemonStageProcess
    {
        private const string TaskTypeName = "System.Threading.Tasks.Task";
        private const string TaskOfTTypeName = "System.Threading.Tasks.Task`1";

        private readonly IDaemonProcess _daemonProcess;

        public ConsiderUsingConfigureAwaitDaemonProcess(IDaemonProcess daemonProcess)
        {
            _daemonProcess = daemonProcess;
        }

        public IDaemonProcess DaemonProcess
        {
            get { return _daemonProcess; }
        }

        public void Execute(Action<DaemonStageResult> commiter)
        {
            if (!_daemonProcess.FullRehighlightingRequired)
                return;

            var highlightings = new List<HighlightingInfo>();

            var sourceFile = _daemonProcess.SourceFile;

            var psiServices = sourceFile.GetPsiServices();

            IFile file = psiServices.Files.GetDominantPsiFile<CSharpLanguage>(sourceFile);
            if (file == null)
                return;

#if RS_V8
            file.ProcessChildren<IAwaitExpression>(expression => ProcessAwait(expression, highlightings));
#endif
#if RS_V9
            foreach (var expression in file.ThisAndDescendants<IAwaitExpression>())
            {
                ProcessAwait(expression, highlightings);
            }
#endif

            commiter(new DaemonStageResult(highlightings));
        }

        private static void ProcessAwait(IAwaitExpression expression, List<HighlightingInfo> highlightings)
        {
            if (expression.Task != null)
            {
                var type = expression.Task.GetExpressionType() as IDeclaredType;
                if (type != null && IsTaskType(type))
                {
                    highlightings.Add(new HighlightingInfo(expression.GetDocumentRange(),
                        new ConsiderUsingConfigureAwaitHighlighting(expression)));
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
