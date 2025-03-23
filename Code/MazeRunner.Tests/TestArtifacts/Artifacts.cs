using System.Drawing;
using MazeRunner.Contracts;
using Moq;

namespace MazeRunner.Tests.TestArtifacts
{
    static public class Artifacts
    {
        static public IMaze Minimal_1X2_SG //SG
        {
            get
            {
                var size = new Size(width: 1, height: 1);
                var exitpoint = new Point(x: 1, y: 0);
                var entrypoint = new Point(x: 0, y: 0);

                var mazemock = new Mock<IMaze>();
                mazemock.Setup(x => x.Size).Returns(size);
                mazemock.Setup(c => c.HitTest(It.IsAny<Point>())).Returns<Point>(p =>
                {
                    if (p == exitpoint) return MazeHitTestEnum.Exitpoint;
                    if (p == entrypoint) return MazeHitTestEnum.Entrypoint;
                    return MazeHitTestEnum.Roadblock;
                });
                mazemock.Setup(x => x.Exitpoint).Returns(exitpoint);
                mazemock.Setup(x => x.Entrypoint).Returns(entrypoint);

                return mazemock.Object;
            }
        }

        static public IMaze Minimal_1X3_S_G //S_G horizontal
        {
            get
            {
                var size = new Size(width: 3, height: 1);
                var exitpoint = new Point(x: 2, y: 0);
                var freepoint = new Point(x: 1, y: 0);
                var entrypoint = new Point(x: 0, y: 0);

                var mazemock = new Mock<IMaze>();
                mazemock.Setup(x => x.Size).Returns(size);
                mazemock.Setup(c => c.HitTest(It.IsAny<Point>())).Returns<Point>(p =>
                {
                    if (p == freepoint) return MazeHitTestEnum.Free;
                    if (p == exitpoint) return MazeHitTestEnum.Exitpoint;
                    if (p == entrypoint) return MazeHitTestEnum.Entrypoint;

                    return MazeHitTestEnum.Roadblock;
                });
                mazemock.Setup(x => x.Exitpoint).Returns(exitpoint);
                mazemock.Setup(x => x.Entrypoint).Returns(entrypoint);
                return mazemock.Object;
            }
        }

        static public IMaze Minimal_1X3_SXG //SXG horizontal
        {
            get
            {
                var size = new Size(width: 3, height: 1);
                var exitpoint = new Point(x: 2, y: 0);
                var entrypoint = new Point(x: 0, y: 0);

                var mazemock = new Mock<IMaze>();
                mazemock.Setup(x => x.Size).Returns(size);
                mazemock.Setup(c => c.HitTest(It.IsAny<Point>())).Returns<Point>(p =>
                {
                    if (p == exitpoint) return MazeHitTestEnum.Exitpoint;
                    if (p == entrypoint) return MazeHitTestEnum.Entrypoint;

                    return MazeHitTestEnum.Roadblock;
                });
                mazemock.Setup(x => x.Exitpoint).Returns(exitpoint);
                mazemock.Setup(x => x.Entrypoint).Returns(entrypoint);
                return mazemock.Object;
            }
        }

        static public IMaze Minimal_3X1_S_G //S_G vertical
        {
            get
            {
                var size = new Size(width: 1, height: 3);
                var exitpoint = new Point(x: 0, y: 2);
                var freepoint = new Point(x: 0, y: 1);
                var entrypoint = new Point(x: 0, y: 0);

                var mazemock = new Mock<IMaze>();
                mazemock.Setup(x => x.Size).Returns(size);
                mazemock.Setup(c => c.HitTest(It.IsAny<Point>())).Returns<Point>(p =>
                {
                    if (p == freepoint) return MazeHitTestEnum.Free;
                    if (p == exitpoint) return MazeHitTestEnum.Exitpoint;
                    if (p == entrypoint) return MazeHitTestEnum.Entrypoint;

                    return MazeHitTestEnum.Roadblock;
                });
                mazemock.Setup(x => x.Exitpoint).Returns(exitpoint);
                mazemock.Setup(x => x.Entrypoint).Returns(entrypoint);
                return mazemock.Object;
            }
        }

        static public IMaze Minimal_3X1_SXG //SXG vertical
        {
            get
            {
                var size = new Size(width: 1, height: 3);
                var exitpoint = new Point(x: 0, y: 2);
                var entrypoint = new Point(x: 0, y: 0);

                var mazemock = new Mock<IMaze>();
                mazemock.Setup(x => x.Size).Returns(size);
                mazemock.Setup(c => c.HitTest(It.IsAny<Point>())).Returns<Point>(p =>
                {
                    if (p == exitpoint) return MazeHitTestEnum.Exitpoint;
                    if (p == entrypoint) return MazeHitTestEnum.Entrypoint;

                    return MazeHitTestEnum.Roadblock;
                });
                mazemock.Setup(x => x.Exitpoint).Returns(exitpoint);
                mazemock.Setup(x => x.Entrypoint).Returns(entrypoint);
                return mazemock.Object;
            }
        }
    }
}
