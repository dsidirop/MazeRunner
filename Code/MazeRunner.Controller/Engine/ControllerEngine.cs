using System.IO;
using MazeRunner.Mazes;
using MazeRunner.Shared.Interfaces;

namespace MazeRunner.Controller.Engine
{
    public partial class ControllerEngine
    {
        private readonly TextWriter _standardError;
        private readonly TextWriter _standardOutput;
        private readonly IMazesFactory _mazesFactory;
        private readonly IEnginesFactory _enginesFactory;
        private readonly IEnginesTestbench _enginesBenchmarker;

        public ControllerEngine(IEnginesFactory enginesFactory, IMazesFactory mazesFactory, IEnginesTestbench enginesBenchmarker, TextWriter standardOutput, TextWriter standardError)
        {
            _mazesFactory = mazesFactory;
            _standardError = standardError;
            _standardOutput = standardOutput;
            _enginesFactory = enginesFactory;
            _enginesBenchmarker = enginesBenchmarker;
        }

        public int Run(string[] args)
        {
            var exitcode = (int?) 0;
            if ((exitcode = TryRunEngine(args)) != null) return exitcode.Value;
            if ((exitcode = TryListEngines(args)) != null) return exitcode.Value;
            if ((exitcode = TryGenerateRandomMaze(args)) != null) return exitcode.Value;

            return PrintUsageMessage(args);
        }
    }
}
