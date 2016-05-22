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
echo Build
echo -----------------
set platform=
@echo on
"%msbuild.exe%" /p:Configuration=Release "%cd%\Biocross.Log\Biocross.Log.csproj"
"%msbuild.exe%" /p:Configuration=Release "%cd%\Biocross.Log.ConsoleBackend\Biocross.Log.ConsoleBackend.csproj"
"%msbuild.exe%" /p:Configuration=Release "%cd%\Biocross.Log.DebugBackend\Biocross.Log.DebugBackend.csproj"
"%msbuild.exe%" /p:Configuration=Release "%cd%\Biocross.Log.FileBackend\Biocross.Log.FileBackend.csproj"
"%msbuild.exe%" /p:Configuration=Release "%cd%\Biocross.Data\Biocross.Data.csproj"
"%msbuild.exe%" /p:Configuration=Release "%cd%\Biocross.Core\Biocross.Core.csproj"
"%msbuild.exe%" /p:Configuration=Release "%cd%\Biocross\Biocross.csproj"