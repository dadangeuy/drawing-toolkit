using System.Drawing;

namespace drawing_toolkit.model.drawable.state {
    internal abstract class DrawableState {
        public virtual void Draw(Drawable drawable, Graphics graphics) { }
    }
}
