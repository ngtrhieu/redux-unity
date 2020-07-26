# Redux Unity

[![Build Status](https://travis-ci.com/ngtrhieu/redux-unity.svg?branch=master)](https://travis-ci.com/ngtrhieu/redux-unity)
[![Conventional Commits](https://img.shields.io/badge/Conventional%20Commits-1.0.0-yellow.svg)](https://conventionalcommits.org)
![Package Version](https://img.shields.io/github/package-json/v/ngtrhieu/redux-unity)

uRedux is a Redux state management for Unity3D, inspired by [Redux](https://redux.js.org/) and [Redux.NET](https://github.com/GuillaumeSalles/redux.NET).

The project is in active development. Pull requests are welcome.

## Examples

### Store implementations:

- [SimpleStore](uRedux/Assets/Scripts/Redux/Samples/Stores/SimpleStore) - a simple store that manage only 1 counting integer.
- [TodoStore](uRedux/Assets/Scripts/Redux/Samples/Stores/TodoStore) - implementation of Redux's [Todo List](https://redux.js.org/basics/example)
- [RedditAPI](uRedux/Assets/Scripts/Redux/Samples/Stores/RedditAPI) - implementation of Redux's [RedditAPI](https://redux.js.org/advanced/example-reddit-api) with async actions.

### Middleware implementations:

Basic middleware implementations can be found [here](uRedux/Assets/Scripts/Redux/Samples/Stores/SimpleStore/Middlewares.cs)

## Credits

Unity docker images used are provided at [gableroux/unity3d](https://gitlab.com/gableroux/unity3d). Check out his [Unity CI Example project](https://github.com/GabLeRoux/unity3d-ci-example) if you are looking for a quick and painless way to setup a Unity CI/CD pipeline on cloud server.
