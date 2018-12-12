using System;
using System.Windows.Forms;

namespace drawing_toolkit.controller {
    class ToolsControl : ToolStrip {
        public event EventHandler SelectCurveTool;
        public event EventHandler SelectSelectionTool;
        private readonly ToolStripItem curveTool = new ToolStripButton("Curve");
        private readonly ToolStripItem selectionTool = new ToolStripButton("Selection");

        public ToolsControl() {
            InitializeUi();
            InitializeEvent();
        }

        private void InitializeUi() {
            Items.Add(selectionTool);
            Items.Add(curveTool);
        }

        private void InitializeEvent() {
            selectionTool.Click += (sender, args) => SelectSelectionTool?.Invoke(this, args);
            curveTool.Click += (sender, args) => SelectCurveTool?.Invoke(this, args);
        }
    }
}
