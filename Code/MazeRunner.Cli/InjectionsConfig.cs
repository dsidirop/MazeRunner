using System;
using Autofac;
using JetBrains.Annotations;
using MazeRunner.Cli.Engine;
using MazeRunner.EnginesFactory.Contracts;
using MazeRunner.Mazes.Contracts;

namespace MazeRunner.Cli;

[UsedImplicitly]
public class InjectionsConfig : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder
            .Register(c => new CliControllerEngine(
                mazesFactory: c.Resolve<IMazesFactory>(),
                enginesFactory: c.Resolve<IGrandMazeRunnersEnginesFactory>(),
                enginesTestbench: c.Resolve<IEnginesTestbench>(),
                standardError: Console.Error,
                standardOutput: Console.Out
            ))
            .As<ICliControllerEngine>()
            .SingleInstance();
    }
}