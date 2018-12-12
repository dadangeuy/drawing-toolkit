using drawing_toolkit.common;

namespace drawing_toolkit.model.canvas.state {
    internal abstract class CanvasState {
        public virtual void MouseDown(Canvas canvas, PointO location) { }
        public virtual void MouseMove(Canvas canvas, PointO location) { }
        public virtual void MouseUp(Canvas canvas, PointO location) { }
    }
}
