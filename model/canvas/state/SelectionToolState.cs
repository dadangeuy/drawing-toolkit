using drawing_toolkit.model.drawable;
using drawing_toolkit.model.drawable.state;
using System.Drawing;

namespace drawing_toolkit.model.canvas.state {
    class SelectionToolState : CanvasState {
        public static readonly SelectionToolState Instance = new SelectionToolState();
        private static ToolMode Mode { get; set; } = ToolMode.SELECT;
        private static Drawable Drawable { get; set; }
        private static Point MDLocation { get; set; }

        public override void MouseDown(Canvas canvas, Point location) {
            switch (Mode) {
                case ToolMode.SELECT:
                    Drawable = GetDrawableAtLocation(canvas, location);
                    if (Drawable != null) {
                        Drawable.State = EditState.Instance;
                        MDLocation = location;
                        Mode = ToolMode.MOVE;
                    }
                    break;
            }
        }

        public override void MouseMove(Canvas canvas, Point location) {
            switch (Mode) {
                case ToolMode.MOVE:
                    var offset = GetOffset(MDLocation, location);
                    Drawable.Move(offset);
                    MDLocation = location;
                    break;
            }
        }

        public override void MouseUp(Canvas canvas, Point location) {
            switch (Mode) {
                case ToolMode.MOVE:
                    Drawable.State = LockState.Instance;
                    Mode = ToolMode.SELECT;
                    break;
            }
        }

        private Drawable GetDrawableAtLocation(Canvas canvas, Point location) {
            foreach (var drawable in canvas.Drawables)
                if (drawable.Intersect(location))
                    return drawable;
            return null;
        }

        private Point GetOffset(Point from, Point to) {
            return new Point(to.X - from.X, to.Y - from.Y);
        }

        private enum ToolMode {
            SELECT, MOVE
        }
    }
}
