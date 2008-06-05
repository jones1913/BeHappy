@ECHO OFF
rem %windir%\Microsoft.NET\Framework\v2.0.50727\csc /platform:x86 /linkres:*.res /res:App.ico /target:winexe /out:BeHappy.exe /win32icon:app.ico *.cs /o
rem %windir%\Microsoft.NET\Framework\v2.0.50727\MSBuild BeHappy.csproj /t:Rebuild /p:Configuration=Release /p:Platform=x86 /p:OutputPath=.\Dist
rem Now it will actually compile for x86 platform (checked onto Windows Server 2003 64bit)
%windir%\Microsoft.NET\Framework\v2.0.50727\MSBuild BeHappy.csproj /t:Rebuild /p:Configuration=Release /p:OutputPath=.\Dist


rem md .\Dist\src
rem md .\Dist\src\extensions
rem copy .\extensions\*.ext*  .\Dist\src\extensions
rem copy *.cs*  .\Dist\src
rem copy *.zip  .\Dist\src
rem copy *.sln  .\Dist\src
rem copy *.bat  .\Dist\src
rem copy *.dll  .\Dist\src
rem copy *.ico  .\Dist\src
rem copy *.txt  .\Dist\src
rem copy *.resx  .\Dist\src
rem copy *.config  .\Dist\src
PAUSE
	
