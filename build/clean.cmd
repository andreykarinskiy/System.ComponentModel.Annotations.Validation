@ECHO OFF
dotnet clean %proj% -v q --nologo

DEL /S /Q BUILD\OUT\*