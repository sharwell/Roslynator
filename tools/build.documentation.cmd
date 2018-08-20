@echo off

set _msbuildPath="C:\Program Files\Microsoft Visual Studio\2017\Community\MSBuild\15.0\Bin\msbuild"

%_msbuildPath% "..\src\Roslynator.sln" ^
 /t:Clean ^
 /p:Configuration=Debug ^
 /v:minimal ^
 /m

%_msbuildPath% "..\src\Roslynator.sln" ^
 /t:Clean,Build ^
 /p:Configuration=ReleaseDoc,TreatWarningsAsErrors=true,WarningsNotAsErrors=1591 ^
 /v:normal ^
 /m

if errorlevel 1 (
 pause
 exit
)

dotnet pack -c Release --no-build -v normal "..\src\Documentation\Documentation.csproj"
dotnet pack -c Release --no-build -v normal "..\src\Documentation.CommandLine\Documentation.CommandLine.csproj"

echo OK
pause
