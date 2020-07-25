echo -e "\033[0;32m:: Running step $0\033[0m"

echo "Testing $BUILD_NAME $TEST_PLATFORM"

CODE_COVERAGE_PACKAGE="com.unity.testtools.codecoverage"
PACKAGE_MANIFEST_PATH="Packages/manifest.json"

${UNITY_EXECUTABLE:-xvfb-run --auto-servernum --server-args='-screen 0 640x480x24' /opt/Unity/Editor/Unity} \
  -projectPath ${PROJECT_PATH} \
  -runTests \
  -testPlatform ${TEST_PLATFORM} \
  -testResults "${RESULT_PATH}/${TEST_PLATFORM}-results.xml" \
  -logFile "/dev/stdout" \
  -batchmode \
  -enableCodeCoverage \
  -coverageResultsPath "${RESULT_PATH}/${TEST_PLATFORM}-coverage" \
  -coverageOptions "generateAdditionalMetrics;generateHtmlReport;generateHtmlReportHistory;generateBadgeReport;assemblyFilters:+Assembly-CSharp" \
  -debugCodeOptimization

echo -e "\033[0;32m:: Step $0 completed\033[0m"
exit $UNITY_EXIT_CODE
