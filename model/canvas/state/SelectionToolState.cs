using drawing_toolkit.common;
using drawing_toolkit.model.drawable;
using drawing_toolkit.model.drawable.state;

namespace drawing_toolkit.model.canvas.state {
    internal class SelectionToolState : CanvasState {
        public static readonly SelectionToolState Instance = new SelectionToolState();
        private static ToolMode Mode { get; set; } = ToolMode.Select;
        private static Drawable Drawable { get; set; }
        private static PointO StartLocation { get; set; }

        public override void MouseDown(Canvas canvas, PointO location) {
            switch (Mode) {
                case ToolMode.Select:
                    Drawable = GetDrawableAtLocation(canvas, location);
                    if (Drawable != null) {
                        Drawable.State = EditState.Instance;
                        StartLocation = location;
                        Mode = ToolMode.Move;
                    }
                    break;
            }
        }

        public override void MouseMove(Canvas canvas, PointO location) {
            switch (Mode) {
                case ToolMode.Move:
                    var offset = PointO.FromOffset(StartLocation, location);
                    Drawable.Move(offset);
                    StartLocation = location;
                    break;
            }
        }

        public override void MouseUp(Canvas canvas, PointO location) {
            switch (Mode) {
                case ToolMode.Move:
                    Drawable.State = LockState.Instance;
                    Mode = ToolMode.Select;
                    break;
            }
        }

        private Drawable GetDrawableAtLocation(Canvas canvas, PointO location) {
            foreach (var drawable in canvas.Drawables)
                if (drawable.Intersect(location))
                    return drawable;
            return null;
        }

        private enum ToolMode {
            Select, Move
        }
    }
}
