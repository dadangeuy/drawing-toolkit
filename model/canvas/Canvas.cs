using drawing_toolkit.common;
using drawing_toolkit.model.canvas.state;
using drawing_toolkit.model.drawable;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using drawing_toolkit.model.drawable.state;

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

        public void KeyDown(bool shift, bool ctrl, Keys keys) {
            State.KeyDown(this, shift, ctrl, keys);
        }

        public void KeyUp(bool shift, bool ctrl, Keys keys) {
            State.KeyUp(this, shift, ctrl, keys);
        }

        public void LockDrawables() {
            foreach (var drawable in Drawables) drawable.State = LockState.Instance;
        }

        public void Draw(Graphics graphics) {
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            foreach (var drawable in Drawables) drawable.Draw(graphics);
        }
    }
}
