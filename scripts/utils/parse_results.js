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
  const duration = Math.ceil(parseFloat(testCase.$.duration) * 1000);

  console.log(
    indent(level),
    colorize(tick),
    testCase.$.name,
    duration > 500
      ? chalk.red(`(${duration}ms)`)
      : duration > 200
      ? chalk.yellow(`(${duration}ms)`)
      : ""
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

  return parseInt(testRun.$.failed) === 0;
};

const parseResult = async (resultPath) => {
  try {
    const xml = fs.readFileSync(resultPath, { encoding: "utf-8" });
    const testResult = await XML.parseStringPromise(xml);
    const passed = renderTestRun(testResult["test-run"]);
    return passed;
  } catch (e) {
    return false;
  }
};

(async () => {
  if (process.env.TEST_PLATFORM === "editmode") {
    console.log();
    console.log(chalk.blue("~~~ EditMode Results ~~~"));
    const passed = await parseResult(
      path.join("test_results", "editmode-results.xml")
    );
    console.log(chalk.blue("~~~~~~~~~~~~~~~~~~~~~~~"));
    if (!passed) process.exit(1);
  }

  if (process.env.TEST_PLATFORM === "playmode") {
    console.log();
    console.log(chalk.blue("~~~ PlayMode Results ~~~"));
    const passed = await parseResult(
      path.join("test_results", "playmode-results.xml")
    );
    console.log(chalk.blue("~~~~~~~~~~~~~~~~~~~~~~~"));
    if (!passed) process.exit(1);
  }
})();
