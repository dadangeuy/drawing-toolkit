using System.Drawing;

namespace drawing_toolkit.common {
    internal class PointO {
        public int X { get; set; }
        public int Y { get; set; }

        public PointO(int x, int y) {
            X = x;
            Y = y;
        }

        public PointO(Point point) {
            X = point.X;
            Y = point.Y;
        }

        public void Offset(PointO offset) {
            X += offset.X;
            Y += offset.Y;
        }

        public void Offset(Point offset) {
            X += offset.X;
            Y += offset.Y;
        }

        public Point GetPoint() {
            return new Point(X, Y);
        }

        public static PointO FromOffset(PointO from, PointO to) {
            return new PointO(to.X - from.X, to.Y - from.Y);
        }

        public static Point[] ToPrimitiveArray(PointO[] points) {
            var primitivePoints = new Point[points.Length];
            for (var i = 0; i < points.Length; i++) primitivePoints[i] = points[i].GetPoint();
            return primitivePoints;
        }
    }
}
