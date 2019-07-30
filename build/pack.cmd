REM dotnet pack %proj% --output %out% -v m --no-build --no-restore -c Release
REM build\tools\nuget.exe pack "%packageProj%" -OutputDirectory "%out%" -Properties Configuration=Release;Version=%NuGetVersionV2% -IncludeReferencedProjects -verbosity quiet
dotnet pack %proj% --output %out% -p:PackageVersion=%NuGetVersionV2% -v m --no-build --no-restore -c Release