const XML = require("xml2js");
const fs = require("fs");
const path = require("path");
const chalk = require("chalk");
const _ = require("lodash");

const indent = (level) => Array(level).fill(" ").join("");

const renderInnerResult = (result, level = 0) => {
  _.each(result, (value, key) => {
    if (key === "test-run") {
      _.each(value, (run) => renderTestRun(run, level + 1));
    } else if (key === "test-suite") {
      _.each(value, (suite) => renderTestSuite(suite, level + 1));
    } else if (key === "test-case") {
      _.each(value, (testCase) => renderTestCase(testCase, level + 1));
    }
  });
};

const renderTestCase = (testCase, level = 0) => {
  const colorize = testCase.failure ? chalk.red : chalk.green;
  const tick = testCase.failure ? "✗" : "✓";
  const duration = parseInt(testCase.$.duration);

  console.log(
    indent(level),
    colorize(tick),
    testCase.$.name,
    duration > 0.5
      ? chalk.red(`(${duration}ms)`)
      : duration > 0.2
      ? chalk.yellow(`(${duration}ms)`)
      : null
  );
};

const renderTestSuite = (testSuite, level = 0) => {
  const colorize = parseInt(testSuite.$.failed) > 0 ? chalk.red : chalk.green;

  console.log(
    indent(level),
    "-",
    testSuite.$.name,
    colorize(`[${testSuite.$.passed} / ${testSuite.$.total}]`)
  );

  renderInnerResult(testSuite, level);
};

const renderTestRun = (testRun, level = 0) => {
  renderInnerResult(testRun, level);

  console.log();
  console.log(
    indent(level),
    ":: Passed:",
    chalk.green(`${testRun.$.passed} / ${testRun.$.total}`)
  );
  console.log(
    indent(level),
    ":: Failed:",
    chalk.red(`${testRun.$.failed} / ${testRun.$.total}`)
  );
};

(async () => {
  const xml = fs.readFileSync(
    path.join("test_results", "editmode-results.xml"),
    { encoding: "utf-8" }
  );

  const testResult = await XML.parseStringPromise(xml);
  renderTestRun(testResult["test-run"]);
})();
