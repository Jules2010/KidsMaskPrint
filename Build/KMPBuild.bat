@Echo off

Echo 1.Clearing Build directory....
Del "KMP\*.*" /Q
Del "KMP\Temp\*.*" /Q
Del "KMP\Logs\*.*" /Q
echo .

Echo 2. Copy Dlls....

copy "DLLs\beside02.exe" "KMP\Beside02.exe" > nul    
copy "DLLs\beside03.exe" "KMP\Beside03.exe" > nul    
copy "DLLs\AppBasic.dll" "KMP\AppBasic.dll" > nul
copy "DLLs\WinOnly.dll" "KMP\WinOnly.dll" > nul
copy "DLLs\MCLCore.dll" "KMP\MCLCore.dll" > nul
copy "DLLs\ProbHand.dll" "KMP\ProbHand.dll" > nul
copy "DLLs\UIStyle.dll" "KMP\UIStyle.dll" > nul

copy "DLLs\AppBasic.dll" "KMP\Temp\AppBasic.dll" > nul
copy "DLLs\WinOnly.dll" "KMP\Temp\WinOnly.dll" > nul
copy "DLLs\MCLCore.dll" "KMP\Temp\MCLCore.dll" > nul
copy "DLLs\ProbHand.dll" "KMP\Temp\ProbHand.dll" > nul
copy "DLLs\UIStyle.dll" "KMP\Temp\UIStyle.dll" > nul
copy "..\CodeLibrary\Components\SharpZipLib.dll" "KMP\SharpZipLib.dll" > nul
copy "..\CodeLibrary\Components\SharpZipLib.dll" "KMP\Temp\SharpZipLib.dll" > nul

echo .

Echo 3. Compile and copy KidsMaskPrint Data Files....
"C:\Program Files (x86)\Microsoft Visual Studio .NET 2003\Common7\IDE\devenv.exe" "..\CodeLibrary\Games\KMPDataFiles\KMPDataFiles.sln" /rebuild release /out KMP\Logs\KidsMaskPrintdata.txt

echo .

Echo 4. Build packs
echo :-
"..\CodeLibrary\Games\KMPDataFiles\bin\KidsMaskPrint.exe"
echo .

Echo 5. Compile and copy KidsMaskPrint.exe....
"C:\Program Files (x86)\Microsoft Visual Studio .NET 2003\Common7\IDE\devenv.exe" "..\CodeLibrary\Games\KidsMaskPrint\KidsMaskPrint.sln" /rebuild release /out KMP\Logs\KidsMaskPrint.txt
copy "..\CodeLibrary\Games\KidsMaskPrint\bin\KidsMaskPrint.exe" "KMP\Temp\KidsMaskPrint.exe"
echo .

echo 6. Turn off validation with public keys..
"C:\Program Files (x86)\Microsoft Visual Studio .NET 2003\SDK\v1.1\Bin\sn.exe" -Vr "KMP\Temp\KidsMaskPrint.exe"
echo .

Echo 7. Obfuscate KidsMaskPrint....
"BuildTools\ObfuscatorBook\Executables\QNDObfuscate.exe" "KMP\Temp\KidsMaskPrint.exe" "KMP\KidsMaskPrint.exe"
echo .

Del "KMP\Temp\*.*" /Q

Echo 8. Signing KidsMaskPrint with MCL Key....
"C:\Program Files (x86)\Microsoft Visual Studio .NET 2003\SDK\v1.1\Bin\sn.exe" -R "KMP\KidsMaskPrint.exe" "..\CodeLibrary\SharewareProjs\IdeasPad\IdeasPad.snk"
echo .

Echo 9. Adding CRC Footer KidsMaskPrint....
"..\CodeLibrary\SharewareProjs\CRCStamp\bin\CRCStamp.exe" "KMP\KidsMaskPrint.exe"

Echo 10. check your EXE at this point see if CRC has worked OK!!
echo.

echo -------- Results --------

type KMP\Logs\KidsMaskPrintdata.txt
echo .

type KMP\Logs\KidsMaskPrint.txt
echo.

echo -------- Results --------


echo !!! CHECK TO SEE IF CLOWN SHOWS!!! YOU MAY HAVE TO REDRAW AND SAVE IT !!!

echo.

pause
