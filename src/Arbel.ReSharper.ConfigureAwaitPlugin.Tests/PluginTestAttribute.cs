using System;
using JetBrains.Application.platforms;
using JetBrains.ReSharper.TestFramework;
using JetBrains.Util.Dotnet.TargetFrameworkIds;

namespace Arbel.ReSharper.ConfigureAwaitPlugin.Tests
{
    public class PluginTestAttribute : TestPackagesAttribute, ITestPlatformProvider
    {
        public PluginTestAttribute() : base("System.Threading.Tasks.Extensions")
        {
        }
        
        public TargetFrameworkId GetTargetFrameworkId() => TargetFrameworkId.Create(FrameworkIdentifier.NetFramework, new Version(4, 5));
    }
}