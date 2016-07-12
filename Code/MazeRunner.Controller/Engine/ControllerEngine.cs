using System.IO;
using MazeRunner.EnginesFactory;
using MazeRunner.Mazes;

namespace MazeRunner.Controller.Engine
{
    public partial class ControllerEngine
    {
        private readonly TextWriter _standardError;
        private readonly TextWriter _standardOutput;
        private readonly IMazesFactory _mazesFactory;
        private readonly IEnginesFactory _enginesFactory;

        public ControllerEngine(IEnginesFactory enginesFactory, IMazesFactory mazesFactory, TextWriter standardOutput, TextWriter standardError)
        {
            _mazesFactory = mazesFactory;
            _standardError = standardError;
            _standardOutput = standardOutput;
            _enginesFactory = enginesFactory;
        }

        public int Run(string[] args)
        {
            var exitcode = (int?)0;
            if ((exitcode = TryRunEngine(args)) != null) return exitcode.Value;
            if ((exitcode = TryListEngines(args)) != null) return exitcode.Value;
            if ((exitcode = TryGenerateRandomMaze(args)) != null) return exitcode.Value;

            return PrintUsageMessage(args);
        }
    }
}
