using Autofac;
using JetBrains.Annotations;
using MazeRunner.EnginesFactory.Benchmark;
using MazeRunner.EnginesFactory.Contracts;
using MazeRunner.EnginesFactory.Factory;

namespace MazeRunner.EnginesFactory.Injections;

[UsedImplicitly]
public class InjectionsConfig : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<EnginesTestbench>().As<IEnginesTestbench>(); //better not make this a singleton
        builder.RegisterType<GrandMazeRunnersEnginesFactory>().As<IGrandMazeRunnersEnginesFactory>().SingleInstance();
    }
}