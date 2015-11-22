using Arbel.ReSharper.ConfigureAwaitPlugin.QuickFix;
#if RS_V9
using JetBrains.ReSharper.FeaturesTestFramework.Intentions;
#endif
#if RS_V8
using JetBrains.ReSharper.IntentionsTests;
#endif
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