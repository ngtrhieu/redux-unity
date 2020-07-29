#!/usr/bin/env bash
set -e

echo -e "\033[0;32m:: Running step $0\033[0m"

echo "Testing $BUILD_NAME $TEST_PLATFORM"

${UNITY_EXECUTABLE:-xvfb-run --auto-servernum --server-args='-screen 0 640x480x24' /opt/Unity/Editor/Unity} \
  -projectPath ${PROJECT_PATH} \
  -runTests \
  -testPlatform ${TEST_PLATFORM} \
  -testResults "${RESULT_PATH}/${TEST_PLATFORM}-results.xml" \
  -logFile "/dev/stdout" \
  -batchmode \
  -debugCodeOptimization

UNITY_EXIT_CODE=$?
if [ $UNITY_EXIT_CODE -eq 0 ]; then
  echo "Run succeeded, no failures occurred"
elif [ $UNITY_EXIT_CODE -eq 2 ]; then
  echo "Run succeeded, some tests failed"
elif [ $UNITY_EXIT_CODE -eq 3 ]; then
  echo "Run failure (other failure)"
else
  echo "Unexpected exit code $UNITY_EXIT_CODE"
fi

echo -e "\033[0;32m:: Step $0 completed\033[0m"
exit $UNITY_EXIT_CODE
