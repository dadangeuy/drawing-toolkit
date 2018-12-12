using System.Drawing;

namespace drawing_toolkit.model.drawable {
    abstract class IDrawable {
        public abstract void Draw(Graphics graphics);
        public virtual void DrawItem(Graphics graphics) { }
        public virtual void DrawGuide(Graphics graphics) { }
    }
}
