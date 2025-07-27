using Autofac;
using JetBrains.Annotations;

namespace MazeRunner.TestbedUI;

[UsedImplicitly]
public class InjectionsConfig : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder
            .RegisterType<FormMazeRunnerTester>()
            .AsSelf()
            .SingleInstance();
    }
}