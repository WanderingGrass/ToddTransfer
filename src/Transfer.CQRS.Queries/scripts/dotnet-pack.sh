#!/bin/bash
echo Executing after success scripts on branch $TRAVIS_BRANCH
echo Triggering Nuget package build

cd src/Transfer.CQRS.Queries/src/Transfer.CQRS.Queries
dotnet pack -c release /p:PackageVersion=0.5.$TRAVIS_BUILD_NUMBER --no-restore -o .

echo Uploading Transfer.CQRS.Queries package to Nuget using branch $TRAVIS_BRANCH

case "$TRAVIS_BRANCH" in
  "master")
    dotnet nuget push *.nupkg -k $NUGET_API_KEY -s https://api.nuget.org/v3/index.json
    ;;
esac