namespace MazeRunner.Controller.Engine
{
    public partial class ControllerEngine
    {
        internal int? TryListEngines(string[] args)
        {
            if (args.Length != 1 || args.FindParameter("listengines") == null) return null;

            _standardOutput.WriteLine($"Available Engines:{nl}{nl}{string.Join(nl, _enginesFactory.EnginesNames)}{nl}");
            return 0;
        }
    }
}
