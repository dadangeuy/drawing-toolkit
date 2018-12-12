using System.Drawing;

namespace drawing_toolkit.model.canvas.state {
    class SelectionToolState : CanvasState {
        public static readonly SelectionToolState Instance = new SelectionToolState();
        private SelectionToolState() { }
    }
}
