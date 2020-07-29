#!/usr/bin/env bash

set -e

source ./scripts/ci/env.sh
./scripts/ci/prepare_license.sh
./scripts/ci/test.sh
node ./scripts/utils/parse_results.js
