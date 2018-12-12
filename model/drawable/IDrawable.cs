using System.Drawing;

namespace drawing_toolkit.model.drawable {
    interface IDrawable {
        void Draw(Graphics graphics);
        void DrawGuide(Graphics graphics);
    }
}
