namespace MazeRunner.Mazes.Contracts;

public readonly record struct MazeSpecs
{
    public int Width { get; init; }
    public int Height { get; init; }
    public double RoadblockDensity { get; init; }
}