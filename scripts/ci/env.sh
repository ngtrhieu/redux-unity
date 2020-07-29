#!/usr/bin/env bash

set -e

# Setup environment variables if it have not been set
#
# NOTE:
# - need to execute with `source`, i.e: `source ./env.sh` to load these variable into the environment.
# - use only VARIABLE=${VARIABLE:-DefaultValue} expansion form https://wiki.bash-hackers.org/syntax/pe to set default
# value so that we can preset from outside environment.

echo -e "\033[0;32m:: Running step $0\033[0m"

####################
# build settings
####################

# Unity docker image
export IMAGE_NAME=${IMAGE_NAME:-"ngtrhieu/unity3d"}
echo "IMAGE_NAME ${IMAGE_NAME}"

# Unity project folder
export UNITY_PROJECT_FOLDER=${UNITY_PROJECT_FOLDER:-"uRedux"}
echo "UNITY_PROJECT_FOLDER ${UNITY_PROJECT_FOLDER}"

# Build target, https://docs.unity3d.com/ScriptReference/BuildTarget.html
export BUILD_TARGET=${BUILD_TARGET:-"StandaloneLinux64"}
echo "BUILD_TARGET ${BUILD_TARGET}"

# Test platform, can either be 'editmode' or 'playmode'.
export TEST_PLATFORM=${TEST_PLATFORM:-"editmode"}
echo "TEST_PLATFORM ${TEST_PLATFORM}"

# Name of the build file, without file extension
export BUILD_NAME=${BUILD_NAME:-"Build_$BUILD_TARGET"}
echo "BUILD_NAME ${BUILD_NAME}"

# The bucket to upload build artifacts / test reports to
export ARTIFACT_BUCKET=${ARTIFACT_BUCKET:-"redux-unity-artifacts"}
echo "ARTIFACT_BUCKET ${ARTIFACT_BUCKET}"

# Unity content license
export UNITY_LICENSE_CONTENT=${UNITY_LICENSE_CONTENT:-"$(cat ./secrets/Unity_lic.ulf)"}

####################
# generated variables (do not modify this section)
####################

# required by fastlane
export LC_ALL='en_US.UTF-8'
export LANG='en_US.UTF-8'

# Absolute path to Unity profile folder
export PROJECT_PATH="$(pwd)/${UNITY_PROJECT_FOLDER}"
echo "PROJECT_PATH ${PROJECT_PATH}"

# Absolute path to test report folder
export RESULT_PATH="$(pwd)/test_results"
echo "RESULT_PATH ${RESULT_PATH}"

# Absolute path to build artifact folder
export BUILD_PATH="$(pwd)/builds"
echo "BUILD_PATH ${BUILD_PATH}"

echo -e "\033[0;32m:: Step $0 completed\033[0m"
