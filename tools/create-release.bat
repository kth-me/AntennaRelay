@echo off

for %%G in ("%~dp0\..") do set repoName=%%~nxG
set startUpProject="ConsoleApp"

:BeginProcess
cd /d "..\src\%repoName%.%startUpProject%"
dotnet run --configuration Release
goto BeginProcess



::Creating a release branch
git checkout -b release-1.2.0 develop
bump-version minor
git commit -a -m "Prepare release v1.2.0"


::Finishing a release branch
git checkout master
git merge --no-ff release-1.2.0
git tag -a v1.2.0 -m "Release v1.2.0"
git push origin master
git push --tags

git checkout develop
git merge --no-ff release-1.2.0