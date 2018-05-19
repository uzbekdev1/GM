- Run cmd as admin rule in this folder
- Go to  /GM.App folder	
	a) Build: dotnet build --configuration Release
- Go to  bin/release folder	
	b) Default(Listening on 5555 port): dotnet GM.App.dll
	a) Help(-?,-h): dotnet GM.App.dll --help 
	b) Manually(-p): dotnet GM.App.dll --prefix "http://+:6666/"
