using System;

namespace MazeRunner.Contracts.Events;

[Serializable]
public readonly struct LapStartingEventArgs : IMazeRunnerEventArgs
{
    public override string ToString() => "Lap Starting";
}