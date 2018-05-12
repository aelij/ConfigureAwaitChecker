using Arbel.ReSharper.ConfigureAwaitPlugin.QuickFix;
using JetBrains.ReSharper.FeaturesTestFramework.Intentions;
using NUnit.Framework;

namespace Arbel.ReSharper.ConfigureAwaitPlugin.Tests.QuickFix
{
    [TestFixture]
    public class ConsiderUsingConfigureAwaitFalseQuickFixTest : QuickFixTestBase<ConsiderUsingConfigureAwaitFalseQuickFix>
    {
        protected override string RelativeTestDataPath => @"QuickFixes\ConsiderUsingConfigureAwait";

        [Test]
        public void Test01()
        {
            DoTestFiles("test01.cs");
        }
    }

    [TestFixture]
    public class ConsiderUsingConfigureAwaitTrueQuickFixTest : QuickFixTestBase<ConsiderUsingConfigureAwaitTrueQuickFix>
    {
        protected override string RelativeTestDataPath => @"QuickFixes\ConsiderUsingConfigureAwait";

        [Test]
        public void Test01()
        {
            DoTestFiles("test02.cs");
        }
    }
}