using System.Collections.Generic;
using System.Windows.Forms;
using drawing_toolkit.common;
using drawing_toolkit.model.drawable;
using drawing_toolkit.model.drawable.state;

namespace drawing_toolkit.model.canvas.state {
    internal class SelectionToolState : CanvasState {
        public static readonly SelectionToolState Instance = new SelectionToolState();
        private static ToolMode Mode { get; set; } = ToolMode.Select;
        private static PointO moveStartLocation;
        private static LinkedList<Drawable> moveDrawables;

        public override void KeyDown(Canvas canvas, bool shift, bool ctrl, Keys keys) {
            if (keys == Keys.ShiftKey) Mode = ToolMode.MultiSelect;
        }

        public override void KeyUp(Canvas canvas, bool shift, bool ctrl, Keys keys) {
            if (keys == Keys.ShiftKey) Mode = ToolMode.Select;
        }

        public override void MouseDown(Canvas canvas, PointO location) {
            var drawable = GetDrawableAtLocation(canvas, location);
            switch (Mode) {
                case ToolMode.Select:
                    if (drawable == null) {
                        canvas.LockDrawables();
                    }
                    else if (drawable.State == EditState.Instance) {
                        moveDrawables = GetDrawableWithState(canvas, EditState.Instance);
                        moveStartLocation = location;
                        Mode = ToolMode.Move;
                    }
                    else {
                        canvas.LockDrawables();
                        drawable.State = EditState.Instance;
                    }
                    break;
                case ToolMode.MultiSelect:
                    if (drawable == null) break;
                    ToggleDrawableState(drawable);
                    break;
            }
        }

        public override void MouseMove(Canvas canvas, PointO location) {
            switch (Mode) {
                case ToolMode.Move:
                    foreach (var drawable in moveDrawables) {
                        var offset = PointO.FromOffset(moveStartLocation, location);
                        drawable.Move(offset);
                    }
                    moveStartLocation = location;
                    break;
            }
        }

        public override void MouseUp(Canvas canvas, PointO location) {
            switch (Mode) {
                case ToolMode.Move:
                    Mode = ToolMode.Select;
                    break;
            }
        }

        private static Drawable GetDrawableAtLocation(Canvas canvas, PointO location) {
            foreach (var drawable in canvas.Drawables)
                if (drawable.Intersect(location))
                    return drawable;
            return null;
        }

        private static void ToggleDrawableState(Drawable drawable) {
            if (drawable.State == LockState.Instance) drawable.State = EditState.Instance;
            else drawable.State = LockState.Instance;
        }

        private static LinkedList<Drawable> GetDrawableWithState(Canvas canvas, DrawableState state) {
            var drawables = new LinkedList<Drawable>();
            foreach (var drawable in canvas.Drawables)
                if (drawable.State == state)
                    drawables.AddLast(drawable);
            return drawables;
        }

        private enum ToolMode {
            Select,
            MultiSelect,
            Move
        }
    }
}