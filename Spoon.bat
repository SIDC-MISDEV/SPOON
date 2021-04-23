@ECHO OFF
set SRC_ROOT=%CD%\Spoon
xcopy /e /v %SRC_ROOT% C:\Spoon\Files /y
rd %SRC_ROOT%\ /s
start "windowTitle" C:\Spoon\Spoon.exe
echo Press wait, Spoon is coming...
echo Press enter to exit...
pause >nul