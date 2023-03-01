[en](CONTRIBUTE.md) | sv

# Sätt upp en en .NET MAUI utvecklingsmiljö för ElevApp

Denna guide kommer att gå igenom processen att sätta upp en utvecklingsmiljö i Visual Studio och få Elevappen som är skriven i C# och .NET MAUI att kompilera och köra på en Android-emulator eller en telefon som är ansluten till din dator.

## Förutsättningar
Innan du börjar, se till att ha följande:

 - [Visual Studio 2022 v17.4 eller senare installerad på din dator.](https://visualstudio.microsoft.com/downloads/) Den kostnadsfria Community-versionen räcker

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


## Kompatibilitetsanmärkning 

Observera att om du redan har satt upp en emulator i en miljö (t.ex. Android Studio) så kanske den inte är kompatibel med Visual Studios emulator. Men du kan fortfarande använda emulatorn från den andra miljön om du vill.

## Sammanfattning 

Med dessa steg bör du nu ha satt upp en utvecklingsmiljö i Visual Studio och kunna kompilera och köra en .NET MAUI App.
