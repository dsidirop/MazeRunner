using System.Threading;
using System.Threading.Tasks;
using MazeRunner.Contracts;

namespace MazeRunner.Mazes;

public interface IMazesFactory
{
    Task<IMaze> FromFileAsync(string path, bool suppressExceptions = true);
    IMaze SpawnRandom(int width, int height, double roadblocksDensity = 0.5, CancellationToken? cancellationToken = null);
}