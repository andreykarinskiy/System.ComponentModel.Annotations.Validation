@ECHO OFF
REM Remove all output BIN and OBJ folders.
FOR /d /r . %%d IN (bin,obj) DO @IF EXIST "%%d" RD /s/q "%%d"

REM Delete all filed in BUILD\OUT folder.
DEL /S /Q BUILD\OUT\*