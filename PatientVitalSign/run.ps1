# PowerShell script to dynamically run PatientVitalSigns.Api and WebApp projects on specific ports

# Get the script directory (assumed to be solution root)
$root = Split-Path -Parent $MyInvocation.MyCommand.Definition

# Find the csproj files
$apiProj = Get-ChildItem -Path $root -Recurse -Filter *Api.csproj | Select-Object -First 1
$webProj = Get-ChildItem -Path $root -Recurse -Filter *WebApp.csproj | Select-Object -First 1

if (-not $apiProj) {
    Write-Error "API project (*.Api.csproj) not found."
    exit 1
}
if (-not $webProj) {
    Write-Error "WebApp project (*.WebApp.csproj) not found."
    exit 1
}

# Start API on desired port
Start-Process "dotnet" "run --project `"$($apiProj.FullName)`" --urls `"https://localhost:7198`""
Start-Sleep -Seconds 10
# Start WebApp on desired port
Start-Process "dotnet" "run --project `"$($webProj.FullName)`" --urls `"https://localhost:7049/`""

Write-Host "Both API and WebApp have been started in separate windows."
Write-Host "Press any key to exit this script (the applications will keep running)..."
[void][System.Console]::ReadKey($true)