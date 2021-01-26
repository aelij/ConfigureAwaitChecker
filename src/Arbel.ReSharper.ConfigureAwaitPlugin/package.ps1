[CmdletBinding()]
param (
    [Parameter()]
    [switch]
    $Publish
)

$ErrorActionPreference = 'Stop'

function native([ScriptBlock] $ScriptBlock) {
  & $ScriptBlock
  if ($LastExitCode -ne 0) {
    throw "Program exited with code $LastExitCode"
  }
}

native { dotnet pack $PSScriptRoot/Arbel.ReSharper.ConfigureAwaitPlugin.csproj -c Release }
native { dotnet build $PSScriptRoot/Arbel.Rider.ConfigureAwaitPlugin.csproj -c Release }
Compress-Archive $PSScriptRoot/bin/Arbel.Rider.ConfigureAwaitPlugin/Release/net461/plugin $PSScriptRoot/plugin.zip -Force
Copy-Item $PSScriptRoot/bin/Arbel.ReSharper.ConfigureAwaitPlugin/Release/*.nupkg $PSScriptRoot -Force

if ($Publish) {
  $token = $env:JB_TOKEN
  native { curl -i --header "Authorization: Bearer $token" -F pluginId=10948 -F file=@$PSScriptRoot/plugin.zip -F channel=Stable 'https://plugins.jetbrains.com/plugin/uploadPlugin' }
  Get-ChildItem $PSScriptRoot/*.nupkg | ForEach-Object { native { dotnet nuget push $_.FullName $token --source 'https://plugins.jetbrains.com/' } }
}
