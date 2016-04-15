using Arbel.ReSharper.ConfigureAwaitPlugin.QuickFix;
using JetBrains.ReSharper.FeaturesTestFramework.Intentions;
using NUnit.Framework;

namespace Arbel.ReSharper.ConfigureAwaitPlugin.Tests.QuickFix
{
    [TestFixture]
    public class ConsiderUsingConfigureAwaitQuickFixTest : QuickFixTestBase<ConsiderUsingConfigureAwaitQuickFix>
    {
        protected override string RelativeTestDataPath => @"QuickFixes\ConsiderUsingConfigureAwait";

        [Test]
        public void Test01()
        {
            DoTestFiles("test01.cs");
        }
    }
}