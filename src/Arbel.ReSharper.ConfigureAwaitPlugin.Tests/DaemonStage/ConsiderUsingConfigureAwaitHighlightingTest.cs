using Arbel.ReSharper.ConfigureAwaitPlugin.DaemonStage;
using JetBrains.Application.Settings;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Daemon.CSharp;
using NUnit.Framework;

namespace Arbel.ReSharper.ConfigureAwaitPlugin.Tests.DaemonStage
{
    [TestFixture]
    public class ConsiderUsingConfigureAwaitHighlightingTest : CSharpHighlightingTestBase
    {
        protected override bool HighlightingPredicate(IHighlighting highlighting, IContextBoundSettingsStore settingsstore)
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