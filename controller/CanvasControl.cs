using drawing_toolkit.model.canvas;
using drawing_toolkit.model.canvas.state;
using System.Drawing;
using System.Windows.Forms;
using Timer = System.Timers.Timer;

namespace drawing_toolkit.controller {
    internal class CanvasControl : Control {
        private readonly Canvas canvas = new Canvas();

        public CanvasControl() {
            InitializeUi();
            InitializeEvent();
            InitializeRefreshRate();
        }

        public void SwitchToSelectionTool() {
            canvas.State = SelectionToolState.Instance;
        }

        public void SwitchToCurveTool() {
            canvas.State = CurveToolState.Instance;
        }

        private void InitializeUi() {
            Dock = DockStyle.Fill;
            BackColor = Color.White;
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        }

        private void InitializeEvent() {
            Paint += (sender, args) => canvas.Draw(args.Graphics);
            MouseDown += (sender, args) => canvas.MouseDown(args.Location);
            MouseMove += (sender, args) => canvas.MouseMove(args.Location);
            MouseUp += (sender, args) => canvas.MouseUp(args.Location);
            KeyDown += (sender, args) => canvas.KeyDown(args.Shift, args.Control, args.KeyCode);
            KeyUp += (sender, args) => canvas.KeyUp(args.Shift, args.Control, args.KeyCode);
        }

        private void InitializeRefreshRate() {
            var timer = new Timer(1000 / 60);
            timer.Elapsed += (s, e) => Invalidate();
            timer.Start();
        }
    }
}
