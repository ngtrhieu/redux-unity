#!/usr/bin/env bash

source ./scripts/ci/env.sh
./scripts/ci/prepare_license.sh
./scripts/ci/test.sh
