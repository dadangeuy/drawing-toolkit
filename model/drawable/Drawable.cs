using drawing_toolkit.model.drawable.state;
using System.Drawing;

namespace drawing_toolkit.model.drawable {
    abstract class Drawable {
        public DrawableState State { get; set; } = EditState.Instance;
        public void Draw(Graphics graphics) { State.Draw(this, graphics); }
        public virtual void DrawItem(Graphics graphics) { }
        public virtual void DrawGuide(Graphics graphics) { }
        public virtual bool Intersect(Point point) { return false;  }
    }
}
