# MazeRunner

Quickstart (Command Line Flavor):

	0. Open the project using Code\MazeRunner.sln (Make sure you have Visual Studio 2015+ installed)
	1. Open the nuget package manager console (Tools -> Nuget package manager -> Package Manager Console) and restore all missing packages (NUnit, Castle.Core, Microsoft.Net.Compilers)
	2. Build the project
	3. Open a command prompt and navigate to <project root>\bin\Debug\
	4. Generate a maze via (you may edit by hand the generated mazefile once you run this command):

	      MazeRunner.Controller.exe  --generatemaze  --width=10  --height=10   --walldensity=0.03  --output=maze10x10.txt

	5. Use the maze you just generated to benchmark the engines:

	      MazeRunner.Controller.exe  --engines=all  --mazefile=maze10x10.txt  --repeat=30

# External Dependendencies

	- Moq
	- NUnit
	- Castle.Core
	- FluentAssertions
	- Microsoft.Net.Compilers

All these libraries are needed for unit testing except the last one which is there to ensure that the solution will build just fine even in platforms that do not have the latest and greatest C# 6.0 compiler installed (typically build servers that lack the latest version of visual studio).
