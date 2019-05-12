using Arbel.ReSharper.ConfigureAwaitPlugin.DaemonStage;
using JetBrains.Application.Settings;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.FeaturesTestFramework.Daemon;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.TestFramework;
using NUnit.Framework;

namespace Arbel.ReSharper.ConfigureAwaitPlugin.Tests.DaemonStage
{
    [TestNetFramework45]
    public class ConsiderUsingConfigureAwaitHighlightingTest : CSharpHighlightingTestBase
    {
        protected override bool HighlightingPredicate(IHighlighting highlighting, IPsiSourceFile sourceFile, IContextBoundSettingsStore settingsStore) =>
            highlighting is ConsiderUsingConfigureAwaitHighlighting;

        protected override string RelativeTestDataPath => @"Daemon\ConsiderUsingConfigureAwait";

        [Test]
        public void TestHighlightingForTask() => DoNamedTest2();
    }
}