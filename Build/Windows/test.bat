REM BUILD FIRST
call build.bat

@echo off
echo Locate MS Build
echo -----------------
set build.msbuild=
for /D %%D in (%SYSTEMROOT%\Microsoft.NET\Framework\v4.0.30319) do set msbuild.exe=%%D\MSBuild.exe
echo MSBuild: %msbuild.exe%
echo.
echo Sanity Checks
echo -----------------
if not defined msbuild.exe echo error: can't find MSBuild.exe & goto :eof
if not exist "%msbuild.exe%" echo error: %msbuild.exe%: not found & goto :eof
echo %msbuild.exe% exists!
echo.
echo NuGet
echo -----------------

WHERE nuget.exe 
IF %ERRORLEVEL% NEQ 0 ECHO nuget.exe wasn't found, please download and place in this directory or in PATH & goto :eof

nuget restore Biocross.sln
nuget install NUnit.Runners -Version 3.2.1 -OutputDirectory testrunner

echo.

echo Build
echo -----------------
@echo on
"%msbuild.exe%" /p:Configuration=Release "%cd%\Biocross.Data.NUnit\Biocross.Data.NUnit.csproj"
"%msbuild.exe%" /p:Configuration=Release "%cd%\Biocross.Core\Biocross.Core.NUnit\Biocross.Core.NUnit.csproj"
@echo off
clear
cd "%cd%\Biocross.Data.NUnit\bin\Release\"
@echo on
"%cd%\..\..\..\testrunner\NUnit.ConsoleRunner.3.2.1\tools\nunit3-console.exe" "%cd%\Biocross.Data.NUnit.dll"
@echo off
cd "%cd%\..\..\..\Biocross.Core\Biocross.Core.NUnit\bin\Release"
@echo on
"%cd%\..\..\..\..\testrunner\NUnit.ConsoleRunner.3.2.1\tools\nunit3-console.exe" "%cd%\Biocross.Core.NUnit.dll"
