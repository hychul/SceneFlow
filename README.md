# SceneFlow

![](https://img.shields.io/badge/Unity-2018.1-blue.svg?style=flat-square) ![](https://img.shields.io/badge/License-MIT-blue.svg?style=flat-square)

Unity plugin to handle scene flows

# Usage

## Load Previous Scene

Loading scenes with SceneFlowManager make loaded scene history stack. This stack is used to load previous scene with `LoadPreviousScene()` method.

## Load Scene With Caching

When use small scene to avoid memory overflow, scene name or index have to be cached. SceneFlowManager provide scene caching by using `LoadScene()` and `LoadPreviousScene()` method's optional parameter : `viaScene`.

To load cached scene, use `LoadCachedScene()` method.
