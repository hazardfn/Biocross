#! /bin/bash

## Build
bash build.sh
clear

## Test
command -v nuget > /dev/null 2>&1 || { echo >&2 "I require nuget but it's not installed.  Aborting."; exit 1; }

if [[ -z ${IS_TRAVIS} ]]; then
nuget restore Biocross.sln
nuget install NUnit.Runners -Version 3.2.1-OutputDirectory testrunner
fi

xbuild /p:Configuration=Release "./Biocross.Data.NUnit/Biocross.Data.NUnit.csproj"
xbuild /p:Configuration=Release "./Biocross.Core/Biocross.Core.NUnit/Biocross.Core.NUnit.csproj"
clear
cd "./Biocross.Data.NUnit/bin/Release/"
mono ./../../../testrunner/NUnit.ConsoleRunner.3.2.1/tools/nunit3-console.exe ./Biocross.Data.NUnit.dll
cd "./../../../Biocross.Core/Biocross.Core.NUnit/bin/Release"
mono ./../../../../testrunner/NUnit.ConsoleRunner.3.2.1/tools/nunit3-console.exe ./Biocross.Core.NUnit.dll
cd "./../../../../"