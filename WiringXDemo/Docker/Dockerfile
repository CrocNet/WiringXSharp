# docker build . -t build-dotnet-arm
# docker run --rm -it -v ./:/src build-dotnet-arm


FROM mcr.microsoft.com/dotnet/sdk:9.0 AS builder

ENV PROTOBUF_PROTOC=/usr/bin/protoc
ENV gRPC_PluginFullPath=/usr/bin/grpc_csharp_plugin


# Install necessary tools and the AArch64 cross-compiler
RUN apt update && apt install -y \
    gcc-aarch64-linux-gnu \
    g++-aarch64-linux-gnu \
    binutils-aarch64-linux-gnu \
    git protobuf-compiler libprotobuf-dev \
    && rm -rf /var/lib/apt/lists/*

# Set working directory
WORKDIR /src

# Set environment variables for the AArch64 toolchain
ENV CC=aarch64-linux-gnu-gcc
ENV CXX=aarch64-linux-gnu-g++
ENV AR=aarch64-linux-gnu-ar
ENV LD=aarch64-linux-gnu-ld


COPY startup.sh /startup.sh
RUN chmod +x /startup.sh

ARG UID=1000
ARG GID=1000
RUN groupadd -g ${GID} mygroup && \
    useradd -u ${UID} -g ${GID} -m myuser

# Switch to that user
USER ${UID}:${GID}

ENTRYPOINT ["/bin/bash","/startup.sh"]
#ENTRYPOINT ["/bin/bash"]
