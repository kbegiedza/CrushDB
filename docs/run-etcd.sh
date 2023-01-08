#!/bin/bash

docker run -d --name etcd \
    --network crushdb_devcontainer_default \
    --env ALLOW_NONE_AUTHENTICATION=yes \
    --env ETCD_ADVERTISE_CLIENT_URLS=http://etcd:2379 \
    bitnami/etcd:latest