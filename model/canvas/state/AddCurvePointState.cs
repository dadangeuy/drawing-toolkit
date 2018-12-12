using System;
using System.Drawing;

namespace drawing_toolkit.model.canvas.state {
    class AddCurvePointState : CanvasState {
        public static readonly AddCurvePointState Instance = new AddCurvePointState();
        private AddCurvePointState() { }

        // add new curve point
        public override void MouseDown(Canvas canvas, Point location) {
            var curve = canvas.AddCurvePointState_Curve;
            if (curve.Intersect(location)) {
                int curveId = curve.AddCurve(location);
                canvas.AddCurvePointState_CurveId = curveId;
            }
        }

        // move curve point
        public override void MouseMove(Canvas canvas, Point location) {
            var curveId = canvas.AddCurvePointState_CurveId;
            if (curveId == -1) return;
            var curve = canvas.AddCurvePointState_Curve;
            curve.MoveCurve(curveId, location);
        }

        public override void MouseUp(Canvas canvas, Point location) {
            canvas.AddCurvePointState_CurveId = -1;
        }
    }
}
