﻿using drawing_toolkit.controller;
using System.Windows.Forms;

namespace drawing_toolkit {
    public partial class MainWindow : Form {
        private readonly CanvasControl canvas = new CanvasControl();
        private readonly ToolsControl tools = new ToolsControl();

        public MainWindow() {
            InitializeComponent();
            InitializeUi();
            InitializeEvent();
        }

        private void InitializeUi() {
            WindowState = FormWindowState.Maximized;
            toolStripContainer.ContentPanel.Controls.Add(canvas);
            toolStripContainer.TopToolStripPanel.Controls.Add(tools);
        }

        private void InitializeEvent() {
            tools.SelectSelectionTool += (sender, args) => canvas.SwitchToSelectionTool();
            tools.SelectCurveTool += (sender, args) => canvas.SwitchToCurveTool();
        }
    }
}
