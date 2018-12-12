using drawing_toolkit.common;
using drawing_toolkit.model.drawable;
using drawing_toolkit.model.drawable.state;
using System.Drawing;

namespace drawing_toolkit.model.canvas.state {
    class SelectionToolState : CanvasState {
        public static readonly SelectionToolState Instance = new SelectionToolState();
        private static ToolMode Mode { get; set; } = ToolMode.SELECT;
        private static Drawable Drawable { get; set; }
        private static PointO StartLocation { get; set; }

        public override void MouseDown(Canvas canvas, PointO location) {
            switch (Mode) {
                case ToolMode.SELECT:
                    Drawable = GetDrawableAtLocation(canvas, location);
                    if (Drawable != null) {
                        Drawable.State = EditState.Instance;
                        StartLocation = location;
                        Mode = ToolMode.MOVE;
                    }
                    break;
            }
        }

        public override void MouseMove(Canvas canvas, PointO location) {
            switch (Mode) {
                case ToolMode.MOVE:
                    var offset = GetOffset(StartLocation, location);
                    Drawable.Move(offset);
                    StartLocation = location;
                    break;
            }
        }

        public override void MouseUp(Canvas canvas, PointO location) {
            switch (Mode) {
                case ToolMode.MOVE:
                    Drawable.State = LockState.Instance;
                    Mode = ToolMode.SELECT;
                    break;
            }
        }

        private Drawable GetDrawableAtLocation(Canvas canvas, PointO location) {
            foreach (var drawable in canvas.Drawables)
                if (drawable.Intersect(location))
                    return drawable;
            return null;
        }

        private PointO GetOffset(PointO from, PointO to) {
            return new PointO(to.X - from.X, to.Y - from.Y);
        }

        private enum ToolMode {
            SELECT, MOVE
        }
    }
}
