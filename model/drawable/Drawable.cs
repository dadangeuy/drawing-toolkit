using drawing_toolkit.model.drawable.state;
using System.Drawing;

namespace drawing_toolkit.model.drawable {
    abstract class Drawable {
        public DrawableState State { get; set; } = EditState.Instance;
        public void Draw(Graphics graphics) { State.Draw(this, graphics); }
        public abstract void DrawItem(Graphics graphics);
        public abstract void DrawGuide(Graphics graphics);
        public abstract bool Intersect(Point point);
        public abstract void Move(Point offset);
    }
}
