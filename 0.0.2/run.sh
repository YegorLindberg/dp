#!/bin/bash

# get current dir
currentDir=$PWD

cd "$currentDir/src/Frontend/bin/Release/netcoreapp2.2/publish"
dotnet Frontend.dll&

cd "$currentDir/src/Backend/bin/Release/netcoreapp2.2/publish"
dotnet Backend.dll&

cd "$currentDir/src/TextListener/bin/Release/netcoreapp2.2/publish"
dotnet TextListener.dll&
