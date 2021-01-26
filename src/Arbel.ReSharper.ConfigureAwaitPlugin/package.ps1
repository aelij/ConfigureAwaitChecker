dotnet pack $PSScriptRoot/Arbel.ReSharper.ConfigureAwaitPlugin.csproj -c Release
dotnet build $PSScriptRoot/Arbel.Rider.ConfigureAwaitPlugin.csproj -c Release
Copy-Item $PSScriptRoot/bin/Arbel.ReSharper.ConfigureAwaitPlugin/Release/*.nupkg $PSScriptRoot -Force
Compress-Archive $PSScriptRoot/bin/Arbel.Rider.ConfigureAwaitPlugin/Release/net461/plugin $PSScriptRoot/plugin.zip -Force
