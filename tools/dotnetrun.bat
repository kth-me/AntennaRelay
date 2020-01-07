@echo off
@title .NET Build & Run Tool: Antenna Relay

:BeginProcess
timeout /t 3

cd /d "..\src"
dotnet restore
dotnet build --configuration Release
cd /d "..\src\AntennaRelay.ConsoleApp"
dotnet run --configuration Release

goto BeginProcess