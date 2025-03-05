#!/bin/bash

set -e

PROJECT="WiringXDemo"

if git rev-parse --is-inside-work-tree > /dev/null 2>&1; then
   git config --global --add safe.directory /src
   GIT_HASH=$(git rev-parse HEAD)
   GIT_BRANCH=$(git rev-parse --abbrev-ref HEAD)
   GIT_TIME=$(git show -s --date='unix' --format=%cd HEAD)
   GIT_TAG=$(git describe --tags 2>/dev/null | sed 's/-g.*//' || echo "NOTAG")
   echo "$GIT_TAG $GIT_BRANCH $GIT_HASH"
else
    GIT_HASH="NOGIT"
    GIT_BRANCH="NOGIT"
    GIT_TIME="NOGIT"
    GIT_TAG="NOGIT"
fi


mkdir -p /src/bin

dotnet publish $PROJECT --configuration Release \
          -p:GitHash=$GIT_HASH \
          -p:GitBranch=$GIT_BRANCH \
          -p:GitDateTime=$GIT_TIME \
          -p:GitTag=$GIT_TAG \
          -p:PublishDir=/src/bin/arm64 \
          -r linux-arm64 -p:PublishSingleFile=true --self-contained true --publish-aot
          
