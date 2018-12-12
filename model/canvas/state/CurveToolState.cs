using drawing_toolkit.common;
using drawing_toolkit.model.drawable;
using drawing_toolkit.model.drawable.state;

namespace drawing_toolkit.model.canvas.state {
    class CurveToolState : CanvasState {
        public static readonly CurveToolState Instance = new CurveToolState();
        private static ToolMode Mode { get; set; } = ToolMode.CREATE;
        private static DrawableCurve Curve { get; set; } = null;
        private static PointO CurvePoint { get; set; } = null;

        public override void MouseDown(Canvas canvas, PointO location) {
            switch (Mode) {
                case ToolMode.CREATE:
                    Curve = new DrawableCurve(location, location);
                    canvas.Drawables.AddLast(Curve);
                    Mode = ToolMode.RESIZE;
                    break;
                case ToolMode.ADD_CURVE:
                    if (Curve.Intersect(location)) {
                        Curve.AddCurve(location);
                        CurvePoint = location;
                        Mode = ToolMode.MOVE_CURVE;
                    } else {
                        Curve.State = LockState.Instance;
                        Mode = ToolMode.CREATE;
                    }
                    break;
            }
        }

        public override void MouseMove(Canvas canvas, PointO location) {
            switch (Mode) {
                case ToolMode.RESIZE:
                    Curve.SetEndPoint(location);
                    break;
                case ToolMode.MOVE_CURVE:
                    var offset = PointO.OffsetOf(CurvePoint, location);
                    CurvePoint.Offset(offset);
                    break;
            }
        }

        public override void MouseUp(Canvas canvas, PointO location) {
            switch (Mode) {
                case ToolMode.RESIZE:
                    Mode = ToolMode.ADD_CURVE;
                    break;
                case ToolMode.MOVE_CURVE:
                    Mode = ToolMode.ADD_CURVE;
                    break;
            }
        }

        private enum ToolMode {
            CREATE, RESIZE, ADD_CURVE, MOVE_CURVE
        }
    }
}
