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
goto SaveVariables

:BumpMinor
set /a newMajor=currentMajor
set /a newMinor=currentMinor+1
set /a newPatch=0
goto SaveVariables

:BumpPatch
set /a newMajor=currentMajor
set /a newMinor=currentMinor
set /a newPatch=currentPatch+1
goto SaveVariables

:: Save new version in VERSION file
:SaveVariables
set newVersion=%newMajor%.%newMinor%.%newPatch%
(
    echo %newVersion%
)>VERSION
goto Success

:: Set version based on contents of VERSION file to all project files in lower directories (dependency: https://github.com/TAGC/dotnet-setversion)
:Success
setversion -r @VERSION
goto EndProcess

:: Confirm failure and exit
:Fail
echo You must supply a valid bump parameter (major, minor, or patch)
goto EndProcess

:EndProcess
cd tools