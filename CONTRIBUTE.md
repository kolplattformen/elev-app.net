en | [sv](CONTRIBUTE-sv.md)

# Setting Up a .NET MAUI Development Environment for ElevApp
This guide will walk you through the process of setting up a development environment in Visual Studio and getting a student application written in C# and .NET MAUI to compile and run on an Android emulator or a phone connected to your computer.


## Prerequisites
Before you begin, make sure that you have the following:


 - [Visual Studio 2022 v17.4 or later installed on your machine.](https://visualstudio.microsoft.com/downloads/) The free community edition is enough.

 - [Guide to download .NET MAUI.](https://learn.microsoft.com/en-us/dotnet/maui/get-started/installation?view=net-maui-7.0&tabs=vswin)

- The Elevapp project you want to open.
- An Android phone connected to your computer, or access to an Android emulator.



## Steps

1. Launch Visual Studio.

2. On the start page, click on "Open a project or solution".

3. In the Open Project dialog box, navigate to the location where your student application project is stored.

4. Select the student application project (with a .csproj extension) and click on the "Open" button.

5. The project will now be loaded in Visual Studio.

6. Connect your Android phone to your computer, or launch an Android emulator. If you are using an emulator, Visual Studio provides its own version of the Android emulator which is faster than Google's emulator.

7. In Visual Studio, select the "Debug" drop-down menu and then choose either "Start Debugging" or "Start Without Debugging" to compile and run the application on your device or emulator.

8. If this is the first time you're running the application on your device, you may be prompted to allow Visual Studio to deploy the necessary components. Follow the on-screen instructions to complete the setup process.

9. Once the application has finished compiling, it will launch on your device or emulator. 



## Compatibility Note

Please note that if you have already set up an emulator in one environment (e.g. Android Studio), it may not be compatible with Visual Studio's emulator. However, you can still use the emulator from the other environment if desired.


## Conclusion

With these steps, you should now have a development environment set up in Visual Studio and be able to compile and run a .NET MAUI Elevapp on an Android emulator or an Android phone connected to your computer. If you have any questions or concerns, please don't hesitate to ask for help.

