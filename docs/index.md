<img src="./images/tabIcon.png" align="right" style="height: 128px"/>

# SimAirport Script Modding

Documentation and guides for the SimAirport Scripting API.

For basic modding (airlines, translation, material, component configs, etc...), use the [wiki](https://github.com/lvgamedev/simairport-modding/wiki) instead.

## Requirements

- Some IDE like [Visual Studio](https://visualstudio.microsoft.com/downloads), a normal text editor does the job too but it's way harder.
- The `SimAirport.Modding` library.
- ~~The guts and will to do so.~~
- Experience with c# class libraries and [.Net framework 4.7.1](https://dotnet.microsoft.com/en-us/download/dotnet-framework/net471).
- Unity (optional, check your log file for the current version, it's one of the first lines).

## The `SimAirport.Modding` Library

The library is required to load scriptmods in. It also contains helper functions to make scriptmodding easier!

### Localy Obtaining

You can localy obtain a compiled version at `steamapps\common\SimAirport\SimAirport_Data\Managed\SimAirport.Modding.Base.dll`.

### Downloading

The API can be downloaded from [NuGet](https://www.nuget.org/packages/SimAirport.Modding/1.0.0).

### Manual Compilation

You can compile the repository's code to build a `DLL` which you can reference on your scriptmod. By default, the project file should be able to find the dependencies. If this fails, you can manually reference them by going to your `steamapps\common\SimAirport\SimAirport_Data\Managed` and manually referencing it.
