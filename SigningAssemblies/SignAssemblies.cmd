ECHO Signing assemblies...

SET basedir=C:\Program Files (x86)\Windows Kits\10\bin\
SET newestSdkDir=10.0.17763.0
FOR /F "tokens=*" %%a in ('dir /b /on "%baseDir%10*"') DO SET newestSdkDir=%%a
REM "%baseDir%%newestSdkDir%\x64\signtool.exe" sign /?
"%baseDir%%newestSdkDir%\x64\signtool.exe" sign /f "%1certificate.pfx" /p "pass&1234" /t "http://timestamp.comodoca.com/authenticode" "%2"

ECHO Assemblies signed...
