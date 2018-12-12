using drawing_toolkit.model.drawable.state;
using System.Drawing;

namespace drawing_toolkit.model.canvas.state {
    class SelectionToolState : CanvasState {
        public static readonly SelectionToolState Instance = new SelectionToolState();

        public override void MouseDown(Canvas canvas, Point location) {
            LockCanvasDrawables(canvas);
            EditDrawableAtLocation(canvas, location);
        }

        private void LockCanvasDrawables(Canvas canvas) {
            foreach (var drawable in canvas.Drawables)
                drawable.State = LockState.Instance;
        }

        private void EditDrawableAtLocation(Canvas canvas, Point location) {
            foreach (var drawable in canvas.Drawables)
                if (drawable.Intersect(location))
                    drawable.State = EditState.Instance;
        }
    }
}
