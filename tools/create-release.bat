@echo off

:BeginProcess
cd ..

:: Generate new VERSION file if one doesn't exist
if exist %cd%\VERSION goto SetVariables

set currentMajor=0
set currentMinor=0
set currentPatch=0
(
    echo %currentMajor%.%currentMinor%.%currentPatch%
)>VERSION

:: Set variables based on contents of VERSION file
:SetVariables
for /f "usebackq tokens=1,2,3 delims=." %%A in ("VERSION") do (
  set currentMajor=%%A
  set currentMinor=%%B
  set currentPatch=%%C
)

:: Check for valid parameter (none defaults to patch) and bump version accordingly
:ParamaterChecks
if [%1]==[] goto BumpPatch
if %1%==major goto BumpMajor
if %1%==minor goto BumpMinor
if %1%==patch goto BumpPatch
goto Fail

:BumpMajor
set /a newMajor=currentMajor+1
set /a newMinor=0
set /a newPatch=0
goto CreateRelease

:BumpMinor
set /a newMajor=currentMajor
set /a newMinor=currentMinor+1
set /a newPatch=0
goto CreateRelease

:BumpPatch
set /a newMajor=currentMajor
set /a newMinor=currentMinor
set /a newPatch=currentPatch+1
goto CreateRelease

:: Create release branch, save new version in VERSION file, 
:: set version based on contents of VERSION file to all 
:: project files in lower directories (dependency: https://github.com/TAGC/dotnet-setversion)
:CreateRelease
set newVersion=%newMajor%.%newMinor%.%newPatch%
git checkout -b release-%newVersion% develop
(
    echo %newVersion%
)>VERSION
setversion -r @VERSION
git commit -a -m "Bump version to %newVersion%"
git push --set-upstream origin release-%newVersion%
goto EndProcess

:EndProcess
cd tools