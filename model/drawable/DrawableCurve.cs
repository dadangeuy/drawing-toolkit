using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using drawing_toolkit.common;

namespace drawing_toolkit.model.drawable {
    internal class DrawableCurve : Drawable {
        private const double IntersectDistanceLimit = 20;
        private static readonly Pen LineGuidePen = new Pen(Color.Red) {
            DashStyle = DashStyle.Dot
        };
        private static readonly Pen CurvePointGuidePen = Pens.Red;
        private static readonly Pen DrawPen = Pens.Black;
        private readonly List<PointO> points = new List<PointO>();

        public DrawableCurve(PointO from, PointO to) {
            points.Add(from);
            points.Add(to);
        }

        public void SetStartPoint(PointO newStartPoint) {
            points[0] = newStartPoint;
        }

        public void SetEndPoint(PointO newEndPoint) {
            points[points.Count - 1] = newEndPoint;
        }

        public int AddCurve(PointO point) {
            var position = FindBestPosition(point);
            if (position != -1) points.Insert(position, point);
            return position;
        }

        public override void DrawItem(Graphics graphics) {
            graphics.DrawCurve(DrawPen, GetPrimitivePoints());
        }

        public override void DrawGuide(Graphics graphics) {
            graphics.DrawLines(LineGuidePen, GetPrimitivePoints());
            foreach (var point in points) graphics.DrawEllipse(CurvePointGuidePen, point.X - 2, point.Y - 2, 4, 4);
        }

        public override void Move(PointO offset) {
            foreach (var point in points)
                point.Offset(offset);
        }

        public override bool Intersect(PointO point) {
            var minDistance = double.MaxValue;
            for (var i = 1; i < points.Count; i++) {
                var a = points[i - 1];
                var b = points[i];
                if (InBetween(a, point, b)) {
                    var distance = PerpendicularDistance(point, a, b);
                    minDistance = Math.Min(minDistance, distance);
                }
            }
            return minDistance <= IntersectDistanceLimit;
        }

        private int FindBestPosition(PointO point) {
            var bestPosition = -1;
            var minDistance = double.MaxValue;
            for (var i = 1; i < points.Count; i++) {
                var a = points[i - 1];
                var b = points[i];
                if (!InBetween(a, point, b)) continue;
                var distance = PerpendicularDistance(point, a, b);
                if (!(distance < minDistance)) continue;
                bestPosition = i;
                minDistance = distance;
            }
            return bestPosition;
        }

        private bool InBetween(PointO start, PointO mid, PointO end) {
            var alpha = FindAngle(mid, start, end);
            var beta = FindAngle(mid, end, start);
            return (alpha <= 90 && beta <= 90);
        }

        private double FindAngle(PointO a, PointO b, PointO c) {
            // reference: https://stackoverflow.com/questions/19729831/angle-between-3-points-in-3d-space
            var v1 = new double[] { a.X - b.X, a.Y - b.Y };
            var v2 = new double[] { c.X - b.X, c.Y - b.Y };
            var v1Mag = Math.Sqrt(v1[0] * v1[0] + v1[1] * v1[1]);
            var v2Mag = Math.Sqrt(v2[0] * v2[0] + v2[1] * v2[1]);
            var v1Norm = new[] { v1[0] / v1Mag, v1[1] / v1Mag };
            var v2Norm = new[] { v2[0] / v2Mag, v2[1] / v2Mag };
            var res = v1Norm[0] * v2Norm[0] + v1Norm[1] * v2Norm[1];
            return Math.Acos(res) * 180.0 / 3.141592653589793;
        }

        private double PerpendicularDistance(PointO point, PointO lineA, PointO lineB) {
            // reference: https://math.stackexchange.com/questions/637922/how-can-i-find-coefficients-a-b-c-given-two-points
            var a = lineA.Y - lineB.Y;
            var b = -(lineA.X - lineB.X);
            var c = lineA.X * lineB.Y - lineB.X * lineA.Y;
            return PerpendicularDistance(point, a, b, c);
        }

        private double PerpendicularDistance(PointO p, float a, float b, float c) {
            // reference: https://www.geeksforgeeks.org/perpendicular-distance-between-a-point-and-a-line-in-2-d/
            return Math.Abs(a * p.X + b * p.Y + c) / Math.Sqrt(a * a + b * b);
        }

        private Point[] GetPrimitivePoints() {
            var pPoints = new Point[points.Count];
            for (var i = 0; i < points.Count; i++) pPoints[i] = points[i].GetPoint();
            return pPoints;
        }
    }
}
