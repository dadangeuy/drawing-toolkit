using System.Drawing;

namespace drawing_toolkit.model.drawable.state {
    class EditState : DrawableState {
        public static readonly EditState Instance = new EditState();

        public override void Draw(Drawable drawable, Graphics graphics) {
            drawable.DrawItem(graphics);
            drawable.DrawGuide(graphics);
        }
    }
}
