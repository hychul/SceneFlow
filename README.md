# SceneFlow

![](https://img.shields.io/badge/Unity-2018.1-blue.svg?style=flat-square) ![](https://img.shields.io/badge/License-MIT-blue.svg?style=flat-square)

Unity plugin to handle scene flows

# Usage

## Load Scene with Parameter

By using SceneParam class, parameters could be passed between scenes. This class can handle any type of object with name to use when get parameter.

```c#
SceneParam param = new SceneParam();
param.PutParam("key_name", 0);
param.PutParam("int_param", 1);
param.PutParam("string_param", "Hello SceneFlow");
param.PutParam("class_param", new Something());
SceneFlowManager.LoadScene("NextSceneName", sceneParam: param);
```

And scene parameter can be gotten by using SceneFlowManager's `GetParam()` method. SceneParam is initialized when SceneFlowManager's scene loading method is called.

```c#
SceneParam param = SceneFlowManager.GetParam();
int intKeyParam = param.GetParam<int>("key_name");
int intParam = param.GetParam<int>("int_param");
string stringParam = param.GetParam<string>("string_param");
Something classParam = param.GetParam<Something>("class_param");
```

## Load Previous Scene

SceneFlowManger hold scene history stack and the stack managed with explicit methods : `PushCurrentScene()` and `PopPreviousScene()`.

Default size of scene stack is 5. This value can be change with `SetSceneStackSize()` method.