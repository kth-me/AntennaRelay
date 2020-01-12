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

:: Check for valid parameter and bump version accordingly
:ParamaterChecks
if [%1]==[] goto Fail
if %1%==major (
    set /a newMajor=currentMajor+1
    set /a newMinor=0
    set /a newPatch=0
) else if %1%==minor (
    set /a newMajor=currentMajor
    set /a newMinor=currentMinor+1
    set /a newPatch=0
) else if %1%==patch (
    set /a newMajor=currentMajor
    set /a newMinor=currentMinor
    set /a newPatch=currentPatch+1
) else (
    goto Fail
)
(
    echo %newMajor%.%newMinor%.%newPatch%
)>VERSION
goto Success

:: Confirm success and set version within all project files in lower directories (dependency: https://github.com/TAGC/dotnet-setversion)
:Success
setversion -r @VERSION
goto EndProcess

:: Confirm failure and exit
:Fail
echo You must supply a valid bump parameter (major, minor, or patch)
goto EndProcess

:EndProcess
cd tools