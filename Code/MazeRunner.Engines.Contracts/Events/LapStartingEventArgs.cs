using System;
using MazeRunner.Contracts.Events;

namespace MazeRunner.Engines.Contracts.Events;

[Serializable]
public readonly struct LapStartingEventArgs : IMazeRunnerEventArgs
{
    public override string ToString() => "Lap Starting";
}