@echo off

:BeginProcess
cd /d "..\src"
dotnet restore
dotnet build --configuration Release
cd /d "..\src\AntennaRelay.ConsoleApp"
dotnet run --configuration Release
goto BeginProcess