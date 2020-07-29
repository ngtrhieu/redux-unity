#!/usr/bin/env bash

set -e

echo -e "\033[0;32m:: Running $0\033[0m"

docker run \
  -e TEST_PLATFORM \
  -w /project/ \
  -v $(pwd):/project/ \
  $IMAGE_NAME \
  /bin/bash -c "/project/scripts/test.sh"

echo -e "\033[0;32m:: $0 completed\033[0m"
