@ECHO OFF
echo Press any key to start initialization...
set SRC_ROOT=%CD%
cd /d E:\FILES\HERE\SPOON 
cd\
echo %CD%
set SRC_SPOON="C:\SIDC-PROGRAMS\Spoon"


echo Checking of path...
if not exist %SRC_SPOON%\Extractor md %SRC_SPOON%\Extractor
if not exist %SRC_SPOON%\Loader md %SRC_SPOON%\Loader
echo %SRC_ROOT%

cd /d %SRC_ROOT%
echo %SRC_ROOT%

set SRC_Extractor=%SRC_ROOT%\Extractor
set SRC_Loader=%SRC_ROOT%\Loader
echo Spoon - Extractor copying to %SRC_SPOON%
xcopy /e /v Extractor %SRC_SPOON%\Extractor /y

echo Spoon - Loader copying to %SRC_SPOON%
xcopy /e /v Loader %SRC_SPOON%\Loader /y


echo Spoon - Extractor creating shortcut
set SCRIPT="%TEMP%\%RANDOM%-%RANDOM%-%RANDOM%-%RANDOM%.vbs"
echo Set oWS = WScript.CreateObject("WScript.Shell") >> %SCRIPT%
echo sLinkFile = "%USERPROFILE%\Desktop\SPOON-EXTRACTOR.lnk" >> %SCRIPT%
echo Set oLink = oWS.CreateShortcut(sLinkFile) >> %SCRIPT%
echo oLink.TargetPath = "C:\SIDC-PROGRAMS\Spoon\Extractor\Spoon.exe" >> %SCRIPT%
echo oLink.Save >> %SCRIPT%
cscript /nologo %SCRIPT%
del %SCRIPT%

if exist %USERPROFILE%\Desktop\SPOON-EXTRACTOR.lnk (
	echo Spoon-EXTRACTOR link is created.
) else (
	echo Spoon-EXTRACTOR link is already exist.
)

echo Spoon - Loader creating shortcut
set SCRIPT="%TEMP%\%RANDOM%-%RANDOM%-%RANDOM%-%RANDOM%.vbs"
echo Set oWS = WScript.CreateObject("WScript.Shell") >> %SCRIPT%
echo sLinkFile = "%USERPROFILE%\Desktop\SPOON-LOADER.lnk" >> %SCRIPT%
echo Set oLink = oWS.CreateShortcut(sLinkFile) >> %SCRIPT%
echo oLink.TargetPath = "C:\SIDC-PROGRAMS\Spoon\Loader\Spoon.exe" >> %SCRIPT%
echo oLink.Save >> %SCRIPT%
cscript /nologo %SCRIPT%
del %SCRIPT%

if exist %USERPROFILE%\Desktop\SPOON-LOADER.lnk (
	echo Spoon-LOADER link is created.
) else (
	echo Spoon-LOADER link is already exist.
)


Echo Done, Press enter to exit...
pause >nul
