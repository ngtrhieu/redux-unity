module.exports = {
  bumpFiles: [
    {
      filename: "package.json",
      type: "json",
    },
    {
      filename: "./uRedux/package.json",
      type: "json",
    },
  ],
  scripts: {
    postchangelog:
      "cp CHANGELOG.md ./uRedux/CHANGELOG.md && cp README.md ./uRedux/README.md && cp LICENSE ./uRedux/LICENSE",
    postcommit:
      "git commit ./uRedux/CHANGELOG.md ./uRedux/README.md ./uRedux/LICENSE --amend --no-edit --no-verify",
  },
};
