using Arbel.ReSharper.ConfigureAwaitPlugin.DaemonStage;
using JetBrains.Application.Settings;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.FeaturesTestFramework.Intentions;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.TestFramework;
using NUnit.Framework;

namespace Arbel.ReSharper.ConfigureAwaitPlugin.Tests.QuickFix
{
    [TestNetFramework45]
    public class ConsiderUsingConfigureAwaitQuickFixAvailabilityTest : QuickFixAvailabilityTestBase
    {
        protected override string RelativeTestDataPath => @"QuickFixes\ConsiderUsingConfigureAwait";

        protected override bool HighlightingPredicate(IHighlighting highlighting, IPsiSourceFile psiSourceFile, IContextBoundSettingsStore boundSettingsStore) =>
            highlighting is ConsiderUsingConfigureAwaitHighlighting;

        [Test]
        public void TestAvailabilityForTask() => DoNamedTest2();
    }
}