# Skapa en utvecklingsmiljö för en .NET MAUI ElevApp. 

I den här guiden får du hjälp med att konfigurera en utvecklingsmiljö i Visual Studio och få en studentapplikation skriven i C# och .NET MAUI att kompileras och köras på antingen en Android-emulator eller en telefon som är ansluten till din dator.


## Bekräftelser

 - [Visual Studio är installerat på din dator.](https://visualstudio.microsoft.com/downloads/)

 - [Elev-app.net-projekten](https://github.com/kolplattformen/elev-app.net)


 - [En Android-emulator eller en Android-telefon som är ansluten till din dator](https://developer.android.com/studio/run/device)

## Bidrag

Bidrag är alltid välkomna!

Rapportera fel och begär funktioner på [Github issue tracker](https://github.com/kolplattformen/elev-app.net/issues)

- Skicka in pull requests för att åtgärda fel eller lägga till nya funktioner.
- Hjälp till med dokumentation.


## Steg

    1. Starta Visual Studio.

    2. Klicka på "Öppna ett projekt eller en lösning" på startsidan.

    3. I dialogrutan Öppna projekt navigerar du till den plats där ditt projekt för studentprogrammet är lagrat.

    4. Välj studentapplikationsprojektet (med ett .csproj-tillägg) och klicka på knappen "Öppna".

    5. Projektet kommer nu att laddas i Visual Studio.

    6. Anslut din Android-telefon till datorn eller starta en Android-emulator.

    7. I Visual Studio väljer du rullgardinsmenyn "Debug" och väljer sedan antingen "Start Debugging" eller "Start Without Debugging" för att kompilera och köra programmet på din enhet eller emulator.

    8. Om det är första gången du kör applikationen på enheten kan du bli ombedd att tillåta Visual Studio att distribuera de nödvändiga komponenterna. Följ instruktionerna på skärmen för att slutföra installationsprocessen.

    9. När applikationen har kompilerat färdigt kommer den att starta på din enhet eller emulator.


## Ytterligare konfiguration 

Om du använder en Android-emulator kan du behöva konfigurera emulatorn för att köra med hårdvaruacceleration. För att göra detta följer du de här stegen:
1. Starta Android-emulatorn.
2. Gå till "Inställningar" och välj sedan "System". 
3. Välj "Om telefonen" och välj sedan "Programvaruinformation".

4. Scrolla nedåt och välj "Byggnadsnummer" sju gånger.

5. Gå tillbaka till huvudinställningssidan och välj "Utvecklingsalternativ".

6. Scrolla ner till "Hardware accelerator" och aktivera "VM acceleration".

7. Starta om emulatorn.




## Slutsats

Med de här stegen bör du nu ha en utvecklingsmiljö i Visual Studio och kunna kompilera och köra en .NET MAUI-studentapplikation på antingen en Android-emulator eller en Android-telefon som är ansluten till din dator. Om du har några frågor eller funderingar, tveka inte att be om hjälp.

# Setting Up a Development Environment for a .NET MAUI ElevApp. 

This guide will walk you through the process of setting up a development environment in Visual Studio and getting a student application written in C# and .NET MAUI to compile and run on either an Android emulator or a phone connected to your computer.


## Acknowledgements

 - [Visual Studio installed on your machine.](https://visualstudio.microsoft.com/downloads/)
 - [The Elev-app.net projects](https://github.com/kolplattformen/elev-app.net)


 - [An Android emulator or an Android phone connected to your computer](https://developer.android.com/studio/run/device)

## Contributing

Contributions are always welcome!

Report bugs and request features on the [Github issue tracker](https://github.com/kolplattformen/elev-app.net/issues)

- Submit pull requests to fix bugs or add new features.
- Help with documentation.


## Steps

    1. Launch Visual Studio.

    2. On the start page, click on "Open a project or solution".

    3. In the Open Project dialog box, navigate to the location where your student application project is stored.

    4. Select the student application project (with a .csproj extension) and click on the "Open" button.

    5. The project will now be loaded in Visual Studio.

    6. Connect your Android phone to your computer or launch an Android emulator.

    7. In Visual Studio, select the "Debug" drop-down menu and then choose either "Start Debugging" or "Start Without Debugging" to compile and run the application on your device or emulator.

    8. If this is the first time you're running the application on your device, you may be prompted to allow Visual Studio to deploy the necessary components. Follow the on-screen instructions to complete the setup process.

    9. Once the application has finished compiling, it will launch on your device or emulator.


## Additional configuration 

If you are using an Android emulator, you may need to configure the emulator to run with hardware acceleration. To do this, follow these steps:
1. Launch the Android emulator.
2. Go to "Settings" and then select "System". 
3. Choose "About phone" and then select "Software information".

4. Scroll down and select "Build number" seven times.

5. Go back to the main settings page and select "Developer options".

6. Scroll down to "Hardware accelerator" and enable "VM acceleration".

7. Restart the emulator.

## Conclusion

With these steps, you should now have a development environment set up in Visual Studio and be able to compile and run a .NET MAUI student application on either an Android emulator or an Android phone connected to your computer. If you have any questions or concerns, please don't hesitate to ask for help.
