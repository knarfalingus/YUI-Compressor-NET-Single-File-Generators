## YUI-Compressor-NET-Single-File-Generators ##

A Visual Studio 2013 project for creating single file generators (custom build tools) that compress Javascript and Css via the YUICompressor.NET library, which is located over on ~~CodePlex at [https://yuicompressor.codeplex.com](https://yuicompressor.codeplex.com/)~~

now Github at [YUICompressor.NET](https://github.com/PureKrome/YUICompressor.NET)

**Instructions**

- Load the project in Visual Studio
- Update Nuget packages so the latest YUICompressor.NET binary is downloaded
- Build
- Exit Visual Studio
- Run the .VSIX installer (now located in the projects /bin folder)
- Restart Visual Studio
- Load or start a new web application project (or windows class library)

- For CSS files, select the file in solution explorer, and in the properties Window set Custom Tool to **YuiNetCssCompressor**
- For Javascript files, select the file in solution explorer, and in the properties Window set Custom Tool to **YuiNetJsCompressor**
