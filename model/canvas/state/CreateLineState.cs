using drawing_toolkit.model.drawable;
using System.Drawing;

namespace drawing_toolkit.model.canvas.state {
    class CreateLineState : CanvasState {
        public static readonly CreateLineState Instance = new CreateLineState();
        private CreateLineState() { }

        // set from
        public override void MouseDown(Canvas canvas, Point location) {
            var line = new DrawableLine();
            line.From = line.To = location;
            canvas.CreateLineState_Line = line;
            canvas.AddDrawable(line);
        }

        // set temporary to
        public override void MouseMove(Canvas canvas, Point location) {
            if (canvas.CreateLineState_Line != null) {
                canvas.CreateLineState_Line.To = location;
            }
        }

        // set to
        public override void MouseUp(Canvas canvas, Point location) {
            canvas.CreateLineState_Line.To = location;
            canvas.CreateLineState_Line = null;
        }
    }
}
