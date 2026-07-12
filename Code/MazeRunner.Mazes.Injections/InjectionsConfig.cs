using Autofac;
using JetBrains.Annotations;
using MazeRunner.Mazes.Contracts;

namespace MazeRunner.Mazes.Injections;

[UsedImplicitly]
public class InjectionsConfig : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<MazesFactory>().As<IMazesFactory>().SingleInstance();
    }
}