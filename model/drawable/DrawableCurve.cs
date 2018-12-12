using drawing_toolkit.model.drawable.state;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace drawing_toolkit.model.drawable {
    class DrawableCurve : Drawable {
        private static readonly double IntersectDistanceLimit = 20;
        private static readonly Pen LineGuidePen = new Pen(Color.Red) {
            DashStyle = DashStyle.Dot
        };
        private static readonly Pen CurvePointGuidePen = Pens.Red;
        private static readonly Pen DrawPen = Pens.Black;
        private readonly List<Point> points = new List<Point>();

        public DrawableCurve(Point from, Point to) {
            points.Add(from);
            points.Add(to);
        }

        public void SetStartPoint(Point newStartPoint) {
            points[0] = newStartPoint;
        }

        public void SetEndPoint(Point newEndPoint) {
            points[points.Count - 1] = newEndPoint;
        }

        public int AddCurve(Point point) {
            int position = FindBestPosition(point);
            if (position != -1) points.Insert(position, point);
            return position;
        }

        public void MoveCurve(int curveId, Point destination) {
            points[curveId] = destination;
        }

        public override bool Intersect(Point point) {
            double minDistance = Double.MaxValue;
            for (int i = 1; i < points.Count; i++) {
                var a = points[i - 1];
                var b = points[i];
                if (InBetween(a, point, b)) {
                    double distance = PerpendicularDistance(point, a, b);
                    minDistance = Math.Min(minDistance, distance);
                }
            }
            return minDistance <= IntersectDistanceLimit;
        }

        public override void DrawItem(Graphics graphics) {
            graphics.DrawCurve(DrawPen, points.ToArray());
        }

        public override void DrawGuide(Graphics graphics) {
            graphics.DrawLines(LineGuidePen, points.ToArray());
            foreach (var point in points) graphics.DrawEllipse(CurvePointGuidePen, point.X - 2, point.Y - 2, 4, 4);
        }

        private int FindBestPosition(Point point) {
            int bestPosition = -1;
            double minDistance = Double.MaxValue;
            for (int i = 1; i < points.Count; i++) {
                var a = points[i - 1];
                var b = points[i];
                if (InBetween(a, point, b)) {
                    double distance = PerpendicularDistance(point, a, b);
                    if (distance < minDistance) {
                        bestPosition = i;
                        minDistance = distance;
                    }
                }
            }
            return bestPosition;
        }

        private bool InBetween(Point start, Point mid, Point end) {
            double alpha = FindAngle(mid, start, end);
            double beta = FindAngle(mid, end, start);
            return (alpha <= 90 && beta <= 90);
        }

        private double FindAngle(Point a, Point b, Point c) {
            // reference: https://stackoverflow.com/questions/19729831/angle-between-3-points-in-3d-space
            double[] v1 = new double[2] { a.X - b.X, a.Y - b.Y };
            double[] v2 = new double[2] { c.X - b.X, c.Y - b.Y };
            double v1mag = Math.Sqrt(v1[0] * v1[0] + v1[1] * v1[1]);
            double v2mag = Math.Sqrt(v2[0] * v2[0] + v2[1] * v2[1]);
            double[] v1norm = new double[2] { v1[0] / v1mag, v1[1] / v1mag };
            double[] v2norm = new double[2] { v2[0] / v2mag, v2[1] / v2mag };
            double res = v1norm[0] * v2norm[0] + v1norm[1] * v2norm[1];
            return Math.Acos(res) * 180.0 / 3.141592653589793;
        }

        private double PerpendicularDistance(Point point, Point lineA, Point lineB) {
            // reference: https://math.stackexchange.com/questions/637922/how-can-i-find-coefficients-a-b-c-given-two-points
            int a = lineA.Y - lineB.Y;
            int b = -(lineA.X - lineB.X);
            int c = lineA.X * lineB.Y - lineB.X * lineA.Y;
            return PerpendicularDistance(point, a, b, c);
        }

        private double PerpendicularDistance(Point p, float a, float b, float c) {
            // reference: https://www.geeksforgeeks.org/perpendicular-distance-between-a-point-and-a-line-in-2-d/
            return Math.Abs(a * p.X + b * p.Y + c) / Math.Sqrt(a * a + b * b);
        }
    }
}
