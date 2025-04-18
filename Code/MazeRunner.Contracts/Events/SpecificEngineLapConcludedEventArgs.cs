﻿using System;

namespace MazeRunner.Contracts.Events;

[Serializable]
public readonly struct SpecificEngineLapConcludedEventArgs : IMazeRunnerEventArgs
{
    public readonly int BenchmarkId;

    public readonly int LapIndex;
    public readonly TimeSpan Duration;
    public readonly IMazeRunnerEngine Engine;
    public readonly ConclusionStatusTypeEnum Status;

    public SpecificEngineLapConcludedEventArgs(int benchmarkId, int lapIndex, TimeSpan duration, IMazeRunnerEngine engine, ConclusionStatusTypeEnum status)
    {
        BenchmarkId = benchmarkId;
        LapIndex = lapIndex;
        Duration = duration;
        Engine = engine;
        Status = status;
    }
    
    [Obsolete("This constructor should not be used")]
    public SpecificEngineLapConcludedEventArgs() => throw new NotImplementedException("This constructor should not be used");
}
