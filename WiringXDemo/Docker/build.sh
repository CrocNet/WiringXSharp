#!/bin/bash
set -e

PROJECT="WiringXDemo"
IMAGE="build-dotnet-arm64"
OUTPUT="/bin/arm64"


# Loop through arguments
for arg in "$@"; do
    # Check if the argument matches the pattern xxx.xxx.xxx.xxx
    if [[ $arg =~ ^[0-9]+\.[0-9]+\.[0-9]+\.[0-9]+$ ]]; then
        IP=$arg
        break
    fi
done


# Check if "build" is in arguments or build-dotnet-arm64 image doesn't exist
if [[ "$*" == *"build"* ]] || ! docker images | grep -q $IMAGE; then
    echo "Building Docker image..."
    docker build . --build-arg UID=$(id -u) --build-arg GID=$(id -g) -t $IMAGE
fi

current_dir=$(pwd)

# Loop to traverse up the directory tree
while [ "$current_dir" != "/" ]; do
    # Check if a .sln file exists in the current directory
    sln_file=$(find "$current_dir" -maxdepth 1 -name "*.sln" | head -n 1)
    if [ -n "$sln_file" ]; then
        # If found, set PROJECTPATH to the directory containing the .sln file
        PROJECTPATH="$current_dir"
        break
    fi
    # Move up one directory level
    current_dir=$(dirname "$current_dir")
done

if [ -n "$PROJECTPATH" ]; then
     echo "Project directory found: $PROJECTPATH"
 else
     echo "No .sln file found in the directory tree"
 fi

set +e

OUTPUTPATH=$PROJECTPATH/$OUTPUT
OUTPUTEXE=$PROJECTPATH/$OUTPUT/$PROJECT

rm $OUTPUTEXE*
docker run --rm -it --net=host -v $PROJECTPATH:/src $IMAGE


if [[ -n $IP && -f $OUTPUTEXE ]]; then
     cat $OUTPUTEXE | ssh root@$IP "cat > /root/$PROJECT"
     #cat $OUTPUTEXE.pdb | ssh root@$IP "cat > /root/$PROJECT.pdb"
fi

