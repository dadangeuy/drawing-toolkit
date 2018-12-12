using drawing_toolkit.model.drawable;
using drawing_toolkit.model.drawable.state;
using System.Drawing;

namespace drawing_toolkit.model.canvas.state {
    class CurveToolState : CanvasState {
        public static readonly CurveToolState Instance = new CurveToolState();
        private static ToolMode Mode { get; set; } = ToolMode.CREATE;
        private static DrawableCurve Curve { get; set; } = null;
        private static int CurveId;

        public override void MouseDown(Canvas canvas, Point location) {
            switch (Mode) {
                case ToolMode.CREATE:
                    Curve = new DrawableCurve(location, location);
                    canvas.AddDrawable(Curve);
                    Mode = ToolMode.RESIZE;
                    break;
                case ToolMode.ADD_CURVE:
                    if (Curve.Intersect(location)) {
                        CurveId = Curve.AddCurve(location);
                        Mode = ToolMode.MOVE_CURVE;
                    } else {
                        Curve.State = LockState.Instance;
                        Mode = ToolMode.CREATE;
                    }
                    break;
            }
        }

        public override void MouseMove(Canvas canvas, Point location) {
            switch (Mode) {
                case ToolMode.RESIZE:
                    Curve.SetEndPoint(location);
                    break;
                case ToolMode.MOVE_CURVE:
                    Curve.MoveCurve(CurveId, location);
                    break;
            }
        }

        public override void MouseUp(Canvas canvas, Point location) {
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
