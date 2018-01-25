# eppz.Networking [![Build Status](https://travis-ci.org/eppz/Unity.Test.eppz.png?branch=master)](https://travis-ci.org/eppz/Unity.Test.eppz)
> part of [**Unity.Library.eppz**](https://github.com/eppz/Unity.Library.eppz)


Unity networking for the everyday. Library uses [`System.Net.Http`](https://msdn.microsoft.com/en-us/library/system.net.http.httpclient(v=vs.118).aspx) threaded to background. It won't introduce any lag, like [`UnityEngine.WWW`](https://docs.unity3d.com/ScriptReference/WWW.html) does occasionally.

## Simple usage

```csharp
Networking.Request("http://maps.googleapis.com/maps/api/geocode/json?address=Siofok", (string result) =>
{
    // Do something useful with `result`.
});
```


## License

> Licensed under the [MIT license](http://en.wikipedia.org/wiki/MIT_License).
