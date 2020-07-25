#!/usr/bin/env bash

# Prepare Unity license

echo -e "\033[0;32m:: Running step $0\033[0m"

mkdir -p /root/.cache/unity3d
mkdir -p /root/.local/share/unity3d/Unity/

LICENSE="UNITY_LICENSE_CONTENT_"$UPPERCASE_BUILD_TARGET

if [ -z "${!LICENSE}" ]; then
  echo "$LICENSE env var not found, using default UNITY_LICENSE_CONTENT env var"
  LICENSE=UNITY_LICENSE_CONTENT
else
  echo "Using $LICENSE env var"
fi

echo "Writing $LICENSE to license file /root/.local/share/unity3d/Unity/Unity_lic.ulf"
echo "${!LICENSE}" | tr -d '\r' >/root/.local/share/unity3d/Unity/Unity_lic.ulf

echo -e "\033[0;32m:: Step $0 completed\033[0m"
