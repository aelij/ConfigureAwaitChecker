using Arbel.ReSharper.ConfigureAwaitPlugin.QuickFix;
using JetBrains.ReSharper.FeaturesTestFramework.Intentions;
using JetBrains.ReSharper.TestFramework;
using NUnit.Framework;

namespace Arbel.ReSharper.ConfigureAwaitPlugin.Tests.QuickFix
{
    [TestNetFramework45]
    public class ConsiderUsingConfigureAwaitFalseQuickFixTest : QuickFixTestBase<ConsiderUsingConfigureAwaitFalseQuickFix>
    {
        protected override string RelativeTestDataPath => @"QuickFixes\ConsiderUsingConfigureAwait";

        [Test]
        public void TestConfigureAwaitFalseForTask() => DoNamedTest2();
    }

    [TestNetFramework45]
    public class ConsiderUsingConfigureAwaitTrueQuickFixTest : QuickFixTestBase<ConsiderUsingConfigureAwaitTrueQuickFix>
    {
        protected override string RelativeTestDataPath => @"QuickFixes\ConsiderUsingConfigureAwait";

        [Test]
        public void TestConfigureAwaitTrueForTask() => DoNamedTest2();
    }
}