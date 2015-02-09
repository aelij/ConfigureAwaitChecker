using Arbel.ReSharper.ConfigureAwaitPlugin.DaemonStage;
using JetBrains.Application.Settings;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Daemon.CSharp;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.FeaturesTestFramework.Daemon;
using JetBrains.ReSharper.Psi;
using NUnit.Framework;

namespace Arbel.ReSharper.ConfigureAwaitPlugin.Tests.DaemonStage
{
    [TestFixture]
    public class ConsiderUsingConfigureAwaitHighlightingTest : CSharpHighlightingTestBase
    {
#if RS_V8
        protected override bool HighlightingPredicate(IHighlighting highlighting, IContextBoundSettingsStore settingsstore)
#endif
#if RS_V9
        protected override bool HighlightingPredicate(IHighlighting highlighting, IPsiSourceFile sourceFile)
#endif
        {
            return highlighting is ConsiderUsingConfigureAwaitHighlighting;
        }

        protected override string RelativeTestDataPath
        {
            get { return @"Daemon\ConsiderUsingConfigureAwait"; }
        }

        [Test]
        [TestCase("Case1.cs")]
        public void Test(string testName)
        {
            DoTestSolution(testName);
        }
    }
}