@ECHO OFF

SET proj=source\All.sln
SET packageProj=source\Validation\Validation.csproj
SET publishDir=D:\Packages
SET out=build\out
SET ver=version.json

REM SET build=build
REM PATH %PATH%;%build%

ECHO Clean up previuous build artefacts...
CALL build\clean.cmd
IF /I "%ERRORLEVEL%" NEQ "0" GOTO fail

ECHO Restoring NuGet packages...
CALL build\restore.cmd
IF /I "%ERRORLEVEL%" NEQ "0" GOTO fail

ECHO Getting latest GIT version...
CALL build\version.cmd

ECHO Building soution...
CALL build\build.cmd
IF /I "%ERRORLEVEL%" NEQ "0" GOTO fail

ECHO Running tests...
CALL build\test.cmd
IF /I "%ERRORLEVEL%" NEQ "0" GOTO fail

ECHO Creation NuGet package...
CALL build\pack.cmd
IF /I "%ERRORLEVEL%" NEQ "0" GOTO fail

ECHO Publishing...
CALL build\publish.cmd
IF /I "%ERRORLEVEL%" NEQ "0" GOTO fail

:success
ECHO -------------------------------------
ECHO BUILDING DONE.
EXIT

:fail
ECHO -------------------------------------
ECHO BUILDING FAILED!
PAUSE
EXIT