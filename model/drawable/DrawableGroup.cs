using System.Collections.Generic;
using System.Drawing;
using drawing_toolkit.common;

namespace drawing_toolkit.model.drawable {
    internal class DrawableGroup : Drawable {
        private readonly LinkedList<Drawable> drawables = new LinkedList<Drawable>();

        public void Add(Drawable drawable) {
            drawables.AddLast(drawable);
        }

        public override void DrawItem(Graphics graphics) {
            foreach (var drawable in drawables) drawable.DrawItem(graphics);
        }

        public override void DrawGuide(Graphics graphics) {
            foreach (var drawable in drawables) drawable.DrawGuide(graphics);
        }

        public override bool Intersect(PointO point) {
            foreach (var drawable in drawables)
                if (drawable.Intersect(point))
                    return true;
            return false;
        }

        public override void Move(PointO offset) {
            foreach (var drawable in drawables) drawable.Move(offset);
        }
    }
}
