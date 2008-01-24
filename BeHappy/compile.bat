@ECHO OFF
rem %windir%\Microsoft.NET\Framework\v2.0.50727\csc /platform:x86 /linkres:*.res /res:App.ico /target:winexe /out:BeHappy.exe /win32icon:app.ico *.cs /o
%windir%\Microsoft.NET\Framework\v2.0.50727\MSBuild BeHappy.csproj /t:Rebuild /p:Configuration=Release /p:Platform=x86 /p:OutputPath=.\Dist

md .\Dist\src
copy *.cs*  .\Dist\src
copy *.ext*  .\Dist\src
copy *.zip  .\Dist\src
copy *.sln  .\Dist\src
copy *.bat  .\Dist\src
copy *.resx  .\Dist\src
copy *.ico  .\Dist\src
copy *.txt  .\Dist\src
copy *.config  .\Dist\src
copy *.dll  .\Dist\src





rem Copy App.config BeHappy.exe.config
PAUSE
	