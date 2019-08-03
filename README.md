<h1 align=center>
<img src="Graphics/maze_runner.png" width=50%>
</h1>

# [AppVeyor CI](https://ci.appveyor.com/project/dsidirop/mazerunner-6gvdh)

![Build status](https://ci.appveyor.com/api/projects/status/github/dsidirop/mazerunner?svg=true)

![Test status](http://teststatusbadge.azurewebsites.net/api/status/dsidirop/mazerunner-6gvdh?svg=true)

# MazeRunner

Quickstart (Winforms Flavor):
 
	0. Open the project using Code\MazeRunner.sln (Make sure you have Visual Studio 2015+ installed)
	1. Open the nuget package manager console (Tools -> Nuget package manager -> Manage NuGet Packages for Solution) and restore all missing packages (NUnit, Castle.Core, Microsoft.Net.Compilers)
	   Note that C# files inside the solution will appear to have errors in terms of missing dll-references even after you perform step#1. These are phantom errors and will disappear once you build
	   the project for the first time.
	   
	2. Set the project MazeRunner.TestbedUI as your startup project
	3. Run the solution from within Visual Studio (if all goes as planned you should be presented with the testbed window)

Quickstart (Command Line Flavor):

	0. Open the project using Code\MazeRunner.sln (Make sure you have Visual Studio 2015+ installed)
	1. Open the nuget package manager console (Tools -> Nuget package manager -> Manage NuGet Packages for Solution) and restore all missing packages (NUnit, Castle.Core, Microsoft.Net.Compilers)
	   Note that C# files inside the solution will appear to have errors in terms of missing dll-references even after you perform step#1. These are phantom errors and will disappear once you build
	   the project for the first time. 

	2. Build the project
	3. Open a command prompt and navigate to <project root>\bin\Debug\
	4. Generate a maze via (you may edit by hand the generated mazefile once you run this command):

	      MazeRunner.Controller.exe  --generatemaze  --width=10  --height=10   --walldensity=0.03  --output=maze10x10.txt

	5. Use the maze you just generated to benchmark the engines:

	      MazeRunner.Controller.exe  --engines=all  --mazefile=maze10x10.txt  --repeat=30

# Logging / Tracing:

To enable logging simply copy paste the following block into MazeRunner.TestbedUI.exe.config (inside the build directory, after you build the project successfully) replacing its pre-existing contents.
Just make sure to replace 'C:\path\to\your\Desktop' with your preferred output directory:

    <?xml version="1.0" encoding="utf-8" ?>
    <configuration>
      <startup>
        <supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.6.1" />
      </startup>
    
      <!-- quick tip   you may use svctraceviewer to easily inspect any of the generated xml logfiles -->
      <system.diagnostics>
        <trace autoflush="true"/>
        <sources>
          <source name="EnginesTestbench"
                  switchName="EnginesTestbenchSwitch"
                  switchType="System.Diagnostics.SourceSwitch" >
            <listeners>
              <clear/>
              <add name="textwriterListener"
                   type="System.Diagnostics.XmlWriterTraceListener"
                   initializeData="C:\path\to\your\Desktop\EnginesTestbenchLog.xml"
                   traceOutputOptions="DateTime" />
            </listeners>
          </source>
          <source name="EnginesFactorySingleton"
                  switchName="EnginesFactorySingletonSwitch"
                  switchType="System.Diagnostics.SourceSwitch" >
            <listeners>
              <clear/>
              <add name="textwriterListener"
                   type="System.Diagnostics.XmlWriterTraceListener"
                   initializeData="C:\path\to\your\Desktop\EnginesFactorySingletonLog.xml"
                   traceOutputOptions="DateTime" />
            </listeners>
          </source>
          <source name="MazeRunnerSimpleDepthFirstEngine"
                  switchName="MazeRunnerSimpleDepthFirstEngineSwitch"
                  switchType="System.Diagnostics.SourceSwitch" >
            <listeners>
              <clear/>
              <add name="textwriterListener"
                   type="System.Diagnostics.XmlWriterTraceListener"
                   initializeData="C:\path\to\your\Desktop\MazeRunnerSimpleDepthFirstEngineLog.xml"
                   traceOutputOptions="DateTime" />
            </listeners>
          </source>
          <source name="MazeRunnerDepthFirstAvoidPathfoldingEngine"
                  switchName="MazeRunnerDepthFirstAvoidPathfoldingEngineSwitch"
                  switchType="System.Diagnostics.SourceSwitch" >
            <listeners>
              <clear/>
              <add name="textwriterListener"
                   type="System.Diagnostics.XmlWriterTraceListener"
                   initializeData="C:\path\to\your\Desktop\MazeRunnerDepthFirstAvoidPathfoldingEngineLog.xml"
                   traceOutputOptions="DateTime" />
            </listeners>
          </source>
        </sources>
        <switches>
          <add name="EnginesTestbenchSwitch" value="Verbose" />
          <add name="EnginesFactorySingletonSwitch" value="Verbose" />
          <add name="MazeRunnerSimpleDepthFirstEngineSwitch" value="Verbose" />
          <add name="MazeRunnerDepthFirstAvoidPathfoldingEngineSwitch" value="Verbose" />
        </switches>
      </system.diagnostics>
    </configuration>
		  
# External Dependendencies

	- Moq
	- NUnit
	- Castle.Core
	- FluentAssertions
	- Microsoft.Net.Compilers

All these libraries are needed for unit testing except the last one which is there to ensure that the solution will build just fine even in platforms that do not have the latest and greatest C# 6.0 compiler installed (typically build servers that lack the latest version of visual studio).
