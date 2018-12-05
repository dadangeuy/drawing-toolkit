using System.Drawing;
using System.Windows.Forms;

namespace drawing_toolkit.controller {
    class CanvasControl : Panel {
        public CanvasControl() {
            Dock = DockStyle.Fill;
            BackColor = Color.White;
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        }
    }
}
