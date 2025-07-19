# PowerShell script to build the entire solution and run PatientVitalSigns.Api and WebApp projects on specific ports

# Set the solution root directory explicitly
$root = Split-Path -Parent $MyInvocation.MyCommand.Definition
$desiredPath = Join-Path $root 'AspNetCoreSQLiteSample'

# Find the solution file
$solution = Get-ChildItem -Path $desiredPath -Filter *.sln | Select-Object -First 1

if (-not $solution) {
    Write-Error "Solution file (*.sln) not found."
    exit 1
}

# Build the solution
Write-Host "Building the entire solution..."
dotnet build "`"$($solution.FullName)`""

# Find the csproj files (case-insensitive for "api" and "WebApp")
$apiProj = Get-ChildItem -Path $root -Recurse -Filter *Api.csproj | Select-Object -First 1
$webProj = Get-ChildItem -Path $root -Recurse -Filter *WebApp.csproj | Select-Object -First 1

if (-not $apiProj) {
    Write-Error "API project (*.api.csproj or *.Api.csproj) not found."
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

Write-Host "The solution has been built. Both API and WebApp have been started in separate windows."
Write-Host "Press any key to exit this script (the applications will keep running)..."
[void][System.Console]::ReadKey($true)