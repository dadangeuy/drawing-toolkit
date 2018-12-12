using System.Drawing;

namespace drawing_toolkit.model.drawable.state {
    class LockState : DrawableState {
        public static readonly LockState Instance = new LockState();

        public override void Draw(Drawable drawable, Graphics graphics) {
            drawable.DrawItem(graphics);
        }
    }
}
