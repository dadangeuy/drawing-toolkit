using drawing_toolkit.common;
using drawing_toolkit.model.drawable;
using drawing_toolkit.model.drawable.state;

namespace drawing_toolkit.model.canvas.state {
    internal class CurveToolState : CanvasState {
        public static readonly CurveToolState Instance = new CurveToolState();
        private static ToolMode Mode { get; set; } = ToolMode.Create;
        private static DrawableCurve Curve { get; set; }
        private static PointO CurvePoint { get; set; }

        public override void MouseDown(Canvas canvas, PointO location) {
            switch (Mode) {
                case ToolMode.Create:
                    Curve = new DrawableCurve(location, location);
                    canvas.Drawables.AddLast(Curve);
                    Mode = ToolMode.Resize;
                    break;
                case ToolMode.AddCurve:
                    if (Curve.Intersect(location)) {
                        Curve.AddCurve(location);
                        CurvePoint = location;
                        Mode = ToolMode.MoveCurve;
                    } else {
                        Curve.State = LockState.Instance;
                        Mode = ToolMode.Create;
                    }
                    break;
            }
        }

        public override void MouseMove(Canvas canvas, PointO location) {
            switch (Mode) {
                case ToolMode.Resize:
                    Curve.SetEndPoint(location);
                    break;
                case ToolMode.MoveCurve:
                    var offset = PointO.FromOffset(CurvePoint, location);
                    CurvePoint.Offset(offset);
                    break;
            }
        }

        public override void MouseUp(Canvas canvas, PointO location) {
            switch (Mode) {
                case ToolMode.Resize:
                    Mode = ToolMode.AddCurve;
                    break;
                case ToolMode.MoveCurve:
                    Mode = ToolMode.AddCurve;
                    break;
            }
        }

        private enum ToolMode {
            Create, Resize, AddCurve, MoveCurve
        }
    }
}
