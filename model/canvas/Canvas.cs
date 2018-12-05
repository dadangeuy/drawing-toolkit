using drawing_toolkit.model.canvas.state;
using drawing_toolkit.model.drawable;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace drawing_toolkit.model.canvas {
    class Canvas {
        public DrawableLine CreateLineState_Line { get; set; }
        public CanvasState State { get; set; } = CreateLineState.Instance;
        private readonly LinkedList<IDrawable> drawables = new LinkedList<IDrawable>();

        public void AddDrawable(IDrawable drawable) {
            drawables.AddLast(drawable);
        }

        public void RemoveDrawable(IDrawable drawable) {
            drawables.AddLast(drawable);
        }

        public void MouseDown(Point location) {
            State.MouseDown(this, location);
        }

        public void MouseMove(Point location) {
            State.MouseMove(this, location);
        }

        public void MouseUp(Point location) {
            State.MouseUp(this, location);
        }

        public void Draw(Graphics graphics) {
            foreach (var drawable in drawables)
                drawable.Draw(graphics);
        }
    }
}
