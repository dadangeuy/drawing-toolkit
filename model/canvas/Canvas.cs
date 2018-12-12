using drawing_toolkit.common;
using drawing_toolkit.model.canvas.state;
using drawing_toolkit.model.drawable;
using System.Collections.Generic;
using System.Drawing;

namespace drawing_toolkit.model.canvas {
    internal class Canvas {
        public CanvasState State { get; set; } = SelectionToolState.Instance;
        public LinkedList<Drawable> Drawables { get; set; } = new LinkedList<Drawable>();

        public void MouseDown(Point location) {
            State.MouseDown(this, new PointO(location));
        }

        public void MouseMove(Point location) {
            State.MouseMove(this, new PointO(location));
        }

        public void MouseUp(Point location) {
            State.MouseUp(this, new PointO(location));
        }

        public void Draw(Graphics graphics) {
            foreach (var drawable in Drawables) drawable.Draw(graphics);
        }
    }
}
