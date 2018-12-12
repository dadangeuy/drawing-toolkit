﻿using drawing_toolkit.model.canvas.state;
using drawing_toolkit.model.drawable;
using System.Collections.Generic;
using System.Drawing;

namespace drawing_toolkit.model.canvas {
    class Canvas {
        public CanvasState State { get; set; } = SelectionToolState.Instance;
        public LinkedList<Drawable> Drawables { get; set; } = new LinkedList<Drawable>();

        public void MouseDown(Point location) {
            State.MouseDown(this, location);
        }

        public void MouseMove(Point location) {
            State.MouseMove(this, location);
        }

        public void MouseUp(Point location) {
            State.MouseUp(this, location);
        }

        public void Draw(Graphics graphics) {
            foreach (var drawable in Drawables) drawable.Draw(graphics);
        }

        // State Context
        public DrawableCurve CreateDrawableState_Curve { get; set; }
        public DrawableCurve AddCurvePointState_Curve { get; set; }
        public int AddCurvePointState_CurveId = -1;
    }
}
