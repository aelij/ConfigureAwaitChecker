using Arbel.ReSharper.ConfigureAwaitPlugin.DaemonStage;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.FeaturesTestFramework.Intentions;
using JetBrains.ReSharper.Psi;
using NUnit.Framework;

namespace Arbel.ReSharper.ConfigureAwaitPlugin.Tests.QuickFix
{
    [TestFixture]
    public class ConsiderUsingConfigureAwaitQuickFixAvailabilityTest : QuickFixAvailabilityTestBase
    {
        protected override string RelativeTestDataPath => @"QuickFixes\ConsiderUsingConfigureAwait";

        protected override bool HighlightingPredicate(IHighlighting highlighting, IPsiSourceFile psiSourceFile)
        {
            return highlighting is ConsiderUsingConfigureAwaitHighlighting;
        }

        [Test]
        public void Availability01()
        {
            DoTestFiles("Availability01.cs");
        }
    }
}