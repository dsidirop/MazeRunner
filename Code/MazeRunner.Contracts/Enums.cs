namespace MazeRunner.Contracts;

public enum MazeHitTestEnum
{
    Roadblock = 0, //includes out-of-bounds
    Free = 1,
    Exitpoint = 2,
    Entrypoint = 3
}

public enum ConclusionStatusTypeEnum
{
    Crashed = 0,
    Stopped = 1,
    Completed = 2
}