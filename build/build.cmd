@ECHO OFF
dotnet build %proj% -v q --nologo --no-restore -c "Release" -p:Version=%AssemblySemVer%