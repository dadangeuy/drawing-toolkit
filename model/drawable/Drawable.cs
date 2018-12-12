using drawing_toolkit.common;
using System.Drawing;
using drawing_toolkit.model.drawable.state;

namespace drawing_toolkit.model.drawable {
    internal abstract class Drawable {
        public DrawableState State { get; set; } = EditState.Instance;
        public void Draw(Graphics graphics) { State.Draw(this, graphics); }
        public abstract void DrawItem(Graphics graphics);
        public abstract void DrawGuide(Graphics graphics);
        public abstract bool Intersect(PointO point);
        public abstract void Move(PointO offset);
    }
}
