
# Sätt upp en utvecklingsmiljö för en .NET MAUI Elevapp

Denna guide kommer att gå igenom processen att sätta upp en utvecklingsmiljö i Visual Studio och få Elevappen som är skriven i C# och .NET MAUI att kompilera och köra på en Android-emulator eller en telefon som är ansluten till din dator.

<img src="screenshots/1.png" width="200"><img src="screenshots/2.png" width="200"><img src="screenshots/3.png" width="200"><img src="screenshots/4.png" width="200"><img src="screenshots/5.png" width="200">

## Förutsättningar
Before you begin, make sure that you have the following:


 - [Visual Studio installerad på din dator.](https://visualstudio.microsoft.com/downloads/)

 - [Guide för att ladda ned .NET MAUI.](https://learn.microsoft.com/en-us/dotnet/maui/get-started/installation?view=net-maui-7.0&tabs=vswin)

- Elevapp-projektet som du vill öppna.
- En Android-telefon ansluten till din dator eller tillgång till en Android-emulator.



## Steg för steg

1. Starta Visual Studio.

2. På startsidan klickar du på "Öppna ett projekt eller en lösning".

3. I dialogrutan Öppna projekt navigerar du till platsen där ditt studentappprojekt är lagrat.

4. Välj studentappprojektet (med en .csproj-förlängning) och klicka på "Öppna"-knappen.

5. Projektet kommer nu att läsas in i Visual Studio.

6. Anslut din Android-telefon till din dator eller starta en Android-emulator. Om du använder en emulator erbjuder Visual Studio sin egen version av Android-emulatorn som är snabbare än Googles egen.

7. I Visual Studio väljer du "Debug"-rullgardinsmenyn och väljer sedan antingen "Start Debugging" eller "Start Without Debugging" för att kompilera och köra appen på din enhet eller emulator.

8. Om detta är första gången du kör appen på din enhet kan du bli ombedd att tillåta Visual Studio att distribuera nödvändiga komponenter. Följ skärminstruktionerna för att genomföra installationsprocessen.

9. När appen har kompilerats färdigt kommer den att starta på din enhet eller emulator.


## Kompatibilitets anmärkning 

Observera att om du redan har satt upp en emulator i en miljö (t.ex. Android Studio) kan den inte vara kompatibel med Visual Studios emulator. Men du kan fortfarande använda emulatorn från den andra miljön om du vill.

## Sammanfattning 

Med dessa steg bör du nu ha satt upp en utvecklingsmiljö i Visual Studio och kunna kompilera och köra en .NET MAUI. 


# Setting Up a Development Environment for a .NET MAUI Elevapp

This guide will walk you through the process of setting up a development environment in Visual Studio and getting a student application written in C# and .NET MAUI to compile and run on an Android emulator or a phone connected to your computer.

<img src="screenshots/1.png" width="200"><img src="screenshots/2.png" width="200"><img src="screenshots/3.png" width="200"><img src="screenshots/4.png" width="200"><img src="screenshots/5.png" width="200">


## Prerequisites
Before you begin, make sure that you have the following:


 - [Visual Studio installed on your machine.](https://visualstudio.microsoft.com/downloads/)

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

