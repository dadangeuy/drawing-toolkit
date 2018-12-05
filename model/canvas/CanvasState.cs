using System.Drawing;

namespace drawing_toolkit.model.canvas {
    abstract class CanvasState {
        public virtual void MouseDown(Canvas canvas, Point location) { }
        public virtual void MouseMove(Canvas canvas, Point location) { }
        public virtual void MouseUp(Canvas canvas, Point location) { }
    }
}
