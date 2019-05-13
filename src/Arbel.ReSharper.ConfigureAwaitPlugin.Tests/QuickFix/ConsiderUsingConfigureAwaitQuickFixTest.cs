using Arbel.ReSharper.ConfigureAwaitPlugin.QuickFix;
using JetBrains.ReSharper.FeaturesTestFramework.Intentions;
using NUnit.Framework;

namespace Arbel.ReSharper.ConfigureAwaitPlugin.Tests.QuickFix
{
    [PluginTest]
    public class ConsiderUsingConfigureAwaitFalseQuickFixTest : QuickFixTestBase<ConsiderUsingConfigureAwaitFalseQuickFix>
    {
        protected override string RelativeTestDataPath => @"QuickFixes\ConsiderUsingConfigureAwait";

        [Test]
        public void TestConfigureAwaitFalseForTask() => DoNamedTest2();
        
        [Test]
        public void TestConfigureAwaitFalseForValueTask() => DoNamedTest2();
    }

    [PluginTest]
    public class ConsiderUsingConfigureAwaitTrueQuickFixTest : QuickFixTestBase<ConsiderUsingConfigureAwaitTrueQuickFix>
    {
        protected override string RelativeTestDataPath => @"QuickFixes\ConsiderUsingConfigureAwait";

        [Test]
        public void TestConfigureAwaitTrueForTask() => DoNamedTest2();
        
        [Test]
        public void TestConfigureAwaitTrueForValueTask() => DoNamedTest2();
    }
}