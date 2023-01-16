#!/bin/bash

docker run -d --name cassandra \
    --network crushdb_devcontainer_default \
    bitnami/cassandra:latest