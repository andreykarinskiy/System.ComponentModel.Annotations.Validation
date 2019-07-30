@ECHO OFF

build\tools\GitVersion.exe > %ver%

FOR /f "tokens=1,2 delims=:, " %%a IN (' find ":" ^< "%ver%" ') DO (
   SET "%%~a=%%~b"
)
SET