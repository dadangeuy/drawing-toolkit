using System.Drawing;

namespace drawing_toolkit.common {
    class PointO {
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

        public static PointO OffsetOf(PointO from, PointO to) {
            return new PointO(to.X - from.X, to.Y - from.Y);
        }

        public Point GetPoint() {
            return new Point(X, Y);
        }
    }
}
