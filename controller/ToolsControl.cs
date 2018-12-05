using System;
using System.Windows.Forms;

namespace drawing_toolkit.controller {
    class ToolsControl : ToolStrip {
        public event EventHandler SelectCurveTool;
        public event EventHandler SelectLineTool;
        private readonly ToolStripItem curveTool = new ToolStripButton("Curve");
        private readonly ToolStripItem lineTool = new ToolStripButton("Line");

        public ToolsControl() {
            InitializeUi();
            InitializeEvent();
        }

        private void InitializeUi() {
            Items.Add(lineTool);
            Items.Add(curveTool);
        }

        private void InitializeEvent() {
            lineTool.Click += (sender, args) => SelectLineTool?.Invoke(this, args);
            curveTool.Click += (sender, args) => SelectCurveTool?.Invoke(this, args);
        }
    }
}
