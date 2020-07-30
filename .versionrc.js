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
  },
};
