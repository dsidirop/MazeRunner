using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MazeRunner.Contracts;
using MazeRunner.Utils;

// ReSharper disable AccessToModifiedClosure

namespace MazeRunner.Mazes;

public class MazesFactory : IMazesFactory
{
    public IMaze Random(int width, int height, double roadblocksDensity = 0.5, CancellationToken? cancellationToken = null)
    {
        var ct = cancellationToken ?? CancellationToken.None;
        
        if (width <= 0) throw new ArgumentOutOfRangeException(nameof(width));
        if (height <= 0) throw new ArgumentOutOfRangeException(nameof(height));

        var totalSquareCount = ((double) width)*height;
        if (totalSquareCount > Maze.MaximumArea) throw new ArgumentException("Maze area too big");
        if (totalSquareCount < Maze.MinimumArea) throw new ArgumentException("Maze area too small");
        if (roadblocksDensity < 0 || roadblocksDensity > 1) throw new ArgumentOutOfRangeException(nameof(roadblocksDensity));

        var roadblocksCount = (int) (totalSquareCount*roadblocksDensity);
        roadblocksCount = Math.Min(roadblocksCount, (int) totalSquareCount - 2); //0
        var randomIndexes = U.GenerateRandomNumbersWithoutDuplicates(count: roadblocksCount + 2, min: 0, maxExclusive: (int) totalSquareCount, ct); //1
        var exitAndEntrypointIndexes = U.GenerateRandomNumbersWithoutDuplicates(count: 2, min: 0, maxExclusive: randomIndexes.Count, ct).Values.Cast<int>().ToArray(); //2

        var exitPointLinear = randomIndexes[exitAndEntrypointIndexes[0]];
        var exitPointAsCoords = ConvertLinearIndexToCoords(exitPointLinear, width);

        var entryPointLinear = randomIndexes[exitAndEntrypointIndexes[1]];
        var entryPointAsCoords = ConvertLinearIndexToCoords(entryPointLinear, width);

        randomIndexes.Remove(exitPointLinear);
        randomIndexes.Remove(entryPointLinear);
        
        var roadblockPoints = randomIndexes
            .UnderCancellationToken(ct)
            .Select(x => ConvertLinearIndexToCoords(x.Value, width))
            .ToHashSet();

        ct.ThrowIfCancellationRequested();

        return new Maze(
            size: new Size(width, height),
            exitpoint: exitPointAsCoords,
            entrypoint: entryPointAsCoords,
            roadblocks: roadblockPoints
        );
    }
    //0 we make sure that two squares will be available for the entry and exit points even if roadblock density is set close to a hundred percent
    //1 we need to generate roadblocks coordinates plus two more square coordinates   the two extra coordinates are meant for entry and exit squares
    //2 we need to pick two random indexes for the entry and exit points   we could have picked the first two random linear indeces but that would yield slightly less
    //  random results than the current approach

    static private Point ConvertLinearIndexToCoords(int linearIndex, int lineWidth) => new(x: linearIndex % lineWidth, y: linearIndex / lineWidth);

    public async Task<IMaze> FromFileAsync(string path, bool suppressExceptions = true)
    {
        var result = (IMaze) null;
        try
        {
            var exitpoint = (Point?) null;
            var lineIndex = 0;
            var entrypoint = (Point?) null;
            var roadblocks = new HashSet<Point>();
            var mazeWidthBasedOnFirstLine = 0;
            using (var reader = new StreamReader(File.OpenRead(path)))
            {
                for (;!reader.EndOfStream; lineIndex++)
                {
                    var line = await reader.ReadLineAsync();
                    if (string.IsNullOrEmpty(line))
                    {
                        if (reader.EndOfStream) break;

                        throw new InvalidDataException($"Line {lineIndex + 1} is empty (only the very last line is allowed to be empty)");
                    }

                    if (mazeWidthBasedOnFirstLine == 0)
                    {
                        mazeWidthBasedOnFirstLine = line.Length;
                    }
                    else if (mazeWidthBasedOnFirstLine != line.Length)
                    {
                        throw new InvalidDataException($"Line {lineIndex + 1} has different number of columns ({line.Length}) than the first line (which has {mazeWidthBasedOnFirstLine})");
                    }

                    line.Each((c, columnIndex) =>
                    {
                        if (c == '_')
                        {
                            //skip
                        }
                        else if (c == 'G')
                        {
                            if (exitpoint != null) throw new InvalidDataException("Maze has two Exit points");

                            exitpoint = new Point(columnIndex, lineIndex);
                        }
                        else if (c == 'S')
                        {
                            if (entrypoint != null) throw new InvalidDataException("Maze has two Entry points");

                            entrypoint = new Point(columnIndex, lineIndex);
                        }
                        else if (c == 'X')
                        {
                            roadblocks.Add(new Point(columnIndex, lineIndex));
                        }
                        else
                        {
                            throw new InvalidDataException($"Invalid character {c} at line {lineIndex + 1} column {columnIndex + 1}");
                        }
                    });
                }
            }

            if (lineIndex == 0) throw new InvalidDataException("Empty");
            if (exitpoint == null) throw new InvalidDataException("No exitpoint specified");
            if (entrypoint == null) throw new InvalidDataException("No entrypoint specified");
                
            result = new Maze(new Size(mazeWidthBasedOnFirstLine, lineIndex), entrypoint: entrypoint.Value, exitpoint: exitpoint.Value, roadblocks: roadblocks);
        }
        catch (Exception)
        {
            //todo  log
            if (suppressExceptions) return null;

            throw;
        }

        return result;
    }
}