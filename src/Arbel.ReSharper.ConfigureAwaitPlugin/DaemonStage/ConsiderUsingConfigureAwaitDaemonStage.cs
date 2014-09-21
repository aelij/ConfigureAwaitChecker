using System.Collections.Generic;
using System.Linq;
using JetBrains.Application.Settings;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.Files;
using JetBrains.ReSharper.Psi.Tree;

namespace Arbel.ReSharper.ConfigureAwaitPlugin.DaemonStage
{
    [DaemonStage(StagesBefore = new[] { typeof(LanguageSpecificDaemonStage) })]
    public sealed class ConsiderUsingConfigureAwaitDaemonStage : IDaemonStage
    {
        public ErrorStripeRequest NeedsErrorStripe(IPsiSourceFile sourceFile, IContextBoundSettingsStore settingsStore)
        {
            return ErrorStripeRequest.STRIPE_AND_ERRORS;
        }

        public IEnumerable<IDaemonStageProcess> CreateProcess(IDaemonProcess process, IContextBoundSettingsStore settings, DaemonProcessKind processKind)
        {
            var sourceFile = process.SourceFile;

            var psiServices = sourceFile.GetPsiServices();

            IFile psiFile = psiServices.Files.GetDominantPsiFile<CSharpLanguage>(sourceFile);
            if (psiFile == null)
                return Enumerable.Empty<IDaemonStageProcess>();

            return new[] { new ConsiderUsingConfigureAwaitDaemonProcess(process) };
        }
    }
}