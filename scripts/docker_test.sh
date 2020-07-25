#!/usr/bin/env bash

echo -e "\033[0;32m:: Running $0\033[0m"

docker run \
  -e TEST_PLATFORM \
  -w /project/ \
  -v $(pwd):/project/ \
  $IMAGE_NAME \
  /bin/bash -c /projects/scripts/ci/test.sh

echo -e "\033[0;32m:: $0 completed\033[0m"