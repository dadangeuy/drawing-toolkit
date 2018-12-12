using drawing_toolkit.model.drawable;
using System.Drawing;

namespace drawing_toolkit.model.canvas.state {
    class CreateCurveState : CanvasState {
        public static readonly CreateCurveState Instance = new CreateCurveState();
        private CreateCurveState() { }

        // set from
        public override void MouseDown(Canvas canvas, Point location) {
            var curve = new DrawableCurve(location, location);
            canvas.CreateDrawableState_Curve = curve;
            canvas.AddDrawable(curve);
        }

        // set temporary to
        public override void MouseMove(Canvas canvas, Point location) {
            if (canvas.CreateDrawableState_Curve == null) return;
            canvas.CreateDrawableState_Curve.SetEndPoint(location);
        }

        // set to
        public override void MouseUp(Canvas canvas, Point location) {
            canvas.CreateDrawableState_Curve.SetEndPoint(location);
            canvas.AddCurvePointState_Curve = canvas.CreateDrawableState_Curve;
            canvas.CreateDrawableState_Curve = null;
            canvas.State = AddCurvePointState.Instance;
        }
    }
}
