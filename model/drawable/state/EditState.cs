using System.Drawing;

namespace drawing_toolkit.model.drawable.state {
    internal class EditState : DrawableState {
        public static readonly EditState Instance = new EditState();

        public override void Draw(Drawable drawable, Graphics graphics) {
            drawable.DrawItem(graphics);
            drawable.DrawGuide(graphics);
        }
    }
}
