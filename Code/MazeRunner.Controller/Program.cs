using MazeRunner.Controller.Bootstrapping;

namespace MazeRunner.Controller
{
    internal class Program
    {
        static public void Main(string[] args)
        {
            new Bootstrapper().Run(args);
        }
    }
}
