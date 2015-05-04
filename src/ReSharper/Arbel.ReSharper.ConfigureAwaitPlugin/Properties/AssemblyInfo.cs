using System.Reflection;
#if RS_V8
using JetBrains.Application.PluginSupport;
#endif

[assembly: AssemblyTitle("ReSharper ConfigureAwait Checker")]
[assembly: AssemblyVersion("0.4.0.0")]

#if RS_V8
[assembly: PluginTitle("ReSharper ConfigureAwait Checker")]
[assembly: PluginDescription("ReSharper ConfigureAwait Checker")]
[assembly: PluginVendor("Eli Arbel")]
#endif
