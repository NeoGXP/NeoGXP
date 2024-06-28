# NeoGXP (Formerly: GXPEngine)
_Last update: 24-06-2024_  
_Repo link: https://github.com/NeoGXP/NeoGXP_

This is NeoGXP. NeoGXP is a C# engine for simple 2D game creation.

It has been tested to work on Linux and Windows. (64-bit only)

## History
NeoGXP used to be called GXPEngine, and was used for learning about game development
at Saxion University of Applied Sciences.

It has now been retired as a learning tool, and is being reworked into a more
proper, full-featured engine.

## How do I get set up?
1. Clone the latest version of the master branch
2. Open the "GXPEngine.sln" file to open the project using Rider, Visual Studio or whichever other IDE you prefer

## Possible improvements (actively) being worked on
- Warn user (with an exception?) when an asset could not be found.
  - This should help when porting Legacy GXP games to NeoGXP due to the asset folder location change.
- Run ReSharper to clean up the code, and make it all proper and consistent. 
- Write and publish nice documentation
- Turn this repo into a library & NuGet package
- Create a new GitHub Template Project that uses NeoGXP as a library
- Create a template for the `dotnet new` tool
- Write and publish a few small tutorial series
- Switch from Legacy OpenGL to OpenGL 3.3
- Switch from GLFW3 to SDL3
- Switch from OpenGL to WebGPU(?)
- Support exporting games to Web
- Support exporting games to mobile platforms
- Various API changes and improvements
- Include a proper Vector2 struct (look into piggy-backing off of System.Numerics and possibly Silk.NET.Maths)
- Include an Angle struct
- Include a Colour struct

## I have some ideas for new features!
- Please [create an issue](https://github.com/TechnicJelle/NeoGXP/issues) and describe your idea in detail
- You can implement it yourself and [create a pull request](https://github.com/TechnicJelle/NeoGXP/fork)
