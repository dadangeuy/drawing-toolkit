using System.Drawing;

namespace drawing_toolkit.model.drawable {
    abstract class DrawableState {
        public virtual void Draw(Drawable drawable, Graphics graphics) { }
    }
}
