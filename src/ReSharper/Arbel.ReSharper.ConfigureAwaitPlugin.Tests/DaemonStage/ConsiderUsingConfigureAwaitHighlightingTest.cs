using Arbel.ReSharper.ConfigureAwaitPlugin.DaemonStage;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.FeaturesTestFramework.Daemon;
using JetBrains.ReSharper.Psi;
using NUnit.Framework;

namespace Arbel.ReSharper.ConfigureAwaitPlugin.Tests.DaemonStage
{
    [TestFixture]
    public class ConsiderUsingConfigureAwaitHighlightingTest : CSharpHighlightingTestBase
    {
        protected override bool HighlightingPredicate(IHighlighting highlighting, IPsiSourceFile sourceFile)
        {
            return highlighting is ConsiderUsingConfigureAwaitHighlighting;
        }

        protected override string RelativeTestDataPath => @"Daemon\ConsiderUsingConfigureAwait";

        [Test]
        [TestCase("Case1.cs")]
        public void Test(string testName)
        {
            DoTestSolution(testName);
        }
    }
}