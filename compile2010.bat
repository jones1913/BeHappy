@ECHO OFF
pushd "%~dp0"
rmdir /S /Q dist\
%windir%\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe BeHappyVS2010.csproj /t:Rebuild /p:Configuration=Release /p:PlatformTarget=x86 /p:OutputPath=.\dist
xcopy /S extensions\*.* dist\extensions\*.*
copy LICENSE.txt dist\

PAUSE
