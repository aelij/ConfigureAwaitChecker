using System;
using System.Collections.Generic;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Files;
using JetBrains.ReSharper.Psi.Tree;

namespace Arbel.ReSharper.ConfigureAwaitPlugin.DaemonStage
{
    public sealed class ConsiderUsingConfigureAwaitDaemonProcess : IDaemonStageProcess
    {
        private static readonly string TaskTypeName = typeof(System.Threading.Tasks.Task).FullName;
        private static readonly string TaskOfTTypeName = typeof(System.Threading.Tasks.Task<>).FullName;

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
            file.ProcessChildren<IAwaitExpression>(
                expression =>
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
                });

            commiter(new DaemonStageResult(highlightings));
        }

        private static bool IsTaskType(IDeclaredType type)
        {
            var clrName = type.GetClrName().FullName;
            return string.Equals(clrName, TaskTypeName, StringComparison.Ordinal) ||
                   string.Equals(clrName, TaskOfTTypeName, StringComparison.Ordinal);
        }
    }
}
