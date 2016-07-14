namespace MazeRunner.Shared
{
    public enum MazeHitTestEnum
    {
        Roadblock = 0, //includes outofbounds
        Free = 1,
        Exitpoint = 2,
        Entrypoint = 3
    }

    public enum ConclusionStatusTypeEnum
    {
        Crashed = 0,
        Cancelled = 1,
        Completed = 2
    }
}
