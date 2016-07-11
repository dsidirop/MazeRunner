namespace MazeRunner.Shared.Helpers
{
    public enum MazeHitTestEnum
    {
        Roadblock = 0, //includes outofbounds
        Free = 1,
        Exitpoint = 2,
        Entrypoint = 3
    }
}
