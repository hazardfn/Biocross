#! /bin/bash

## Simply run xbuild
echo "Build Projects"
echo "------------------"
xbuild /p:Configuration=Release ./Biocross.Log/Biocross.Log.csproj"
xbuild /p:Configuration=Release ./Biocross.Log.ConsoleBackend/Biocross.Log.ConsoleBackend.csproj"
xbuild /p:Configuration=Release ./Biocross.Log.DebugBackend/Biocross.Log.DebugBackend.csproj"
xbuild /p:Configuration=Release ./Biocross.Log.FileBackend/Biocross.Log.FileBackend.csproj"
xbuild /p:Configuration=Release ./Biocross.Data/Biocross.Data.csproj"
xbuild /p:Configuration=Release ./Biocross.Core/Biocross.Core.csproj"