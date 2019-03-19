#!/bin/bash
if [ -z "$1" ]
  then
    echo "No argument supplied"
    exit
fi

# get current dir
currentDir=$PWD

#dotnet configuration
cd "$currentDir/src/Frontend"
dotnet publish -c Release

cd "$currentDir/src/Backend"
dotnet publish -c Release

cd "$currentDir/src/TextListener"
dotnet publish -c Release

mkdir -p "$currentDir/$1/src/Frontend/"
cp -r "$currentDir/src/Frontend/bin" "$currentDir/$1/src/Frontend/"

mkdir -p "$currentDir/$1/src/Backend/"
cp -r "$currentDir/src/Backend/bin" "$currentDir/$1/src/Backend/"

mkdir -p "$currentDir/$1/src/TextListener/"
cp -r "$currentDir/src/TextListener/bin" "$currentDir/$1/src/TextListener/"

cp "$currentDir/run.sh" "$currentDir/$1/run.sh"
cp "$currentDir/stop.sh" "$currentDir/$1/stop.sh"
