@echo off

Echo Clearing Build directory....
Del "D:\HelpBuild\KidsMaskPrint\*.html" /Q

echo .
Echo Generating Files...
"..\..\..\Build\BuildTools\htmlsqueeze.exe" http://localhost/helpkmp/support.asp "..\support.html" NONE
"..\..\..\Build\BuildTools\htmlsqueeze.exe" http://localhost/helpkmp/welcome.asp "..\welcome.html" NONE
"..\..\..\Build\BuildTools\htmlsqueeze.exe" http://localhost/helpkmp/buy.asp "..\buy.html" NONE
"..\..\..\Build\BuildTools\htmlsqueeze.exe" http://localhost/helpkmp/quickstart.asp "..\quickstart.html" NONE
"..\..\..\Build\BuildTools\htmlsqueeze.exe" http://localhost/helpkmp/signin.asp "..\signin.html" NONE
"..\..\..\Build\BuildTools\htmlsqueeze.exe" http://localhost/helpkmp/slots.asp "..\slots.html" NONE
"..\..\..\Build\BuildTools\htmlsqueeze.exe" http://localhost/helpkmp/printing.asp "..\printing.html NONE
"..\..\..\Build\BuildTools\htmlsqueeze.exe" http://localhost/helpkmp/parentoptions.asp "..\parentoptions.html" NONE
"..\..\..\Build\BuildTools\htmlsqueeze.exe" http://localhost/helpkmp/mainscreen.asp "..\mainscreen.html" NONE
"..\..\..\Build\BuildTools\htmlsqueeze.exe" http://localhost/helpkmp/facepartselect.asp "..\facepartselect.html" NONE
"..\..\..\Build\BuildTools\htmlsqueeze.exe" http://localhost/helpkmp/elasticcard.asp "..\elasticcard.html" NONE
"..\..\..\Build\BuildTools\htmlsqueeze.exe" http://localhost/helpkmp/afterpurchase.asp "..\afterpurchase.html" NONE

Echo Finished Generating....
echo .
pause