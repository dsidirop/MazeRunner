using System.Threading;
using System.Threading.Tasks;

namespace MazeRunner.Mazes.Contracts;

public interface IMazesFactory
{
    Task<IMaze> FromFileAsync(string filepath, bool suppressExceptions = true);
    IMaze SpawnRandom(int width, int height, double roadblocksDensity = 0.5, CancellationToken? cancellationToken = null);
}