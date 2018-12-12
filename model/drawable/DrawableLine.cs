using System.Drawing;

namespace drawing_toolkit.model.drawable {
    class DrawableLine : IDrawable {
        public Point From { get; set; }
        public Point To { get; set; }

        public void Draw(Graphics graphics) {
            graphics.DrawLine(Pens.Black, From, To);
        }

        public void DrawGuide(Graphics graphics) {

        }
    }
}
