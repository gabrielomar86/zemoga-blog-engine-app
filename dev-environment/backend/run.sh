#!/bin/sh

PATH_DOCS="dev-environment/backend"

docker-compose -f $PATH_DOCS/docker-compose.yml \
               -f $PATH_DOCS/docker-compose.override.yml \
               up --build