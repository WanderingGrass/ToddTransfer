#!/bin/bash
echo Executing after success scripts on branch $TRAVIS_BRANCH
echo Triggering Nuget package build

cd src/Transfer.Tracing.Jaeger.RabbitMQ/src/Transfer.Tracing.Jaeger.RabbitMQ
dotnet pack -c release /p:PackageVersion=1.0.$TRAVIS_BUILD_NUMBER --no-restore -o .

echo Uploading Transfer.Tracing.Jaeger.RabbitMQ package to Nuget using branch $TRAVIS_BRANCH

case "$TRAVIS_BRANCH" in
  "master")
    dotnet nuget push *.nupkg -k $NUGET_API_KEY -s https://api.nuget.org/v3/index.json
    ;;
esac