namespace WaypointCreator.Viewer
{
    partial class ViewerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.mnuMain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.selectMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.linkPathsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuMain
            // 
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectMapToolStripMenuItem,
            this.linkPathsToolStripMenuItem});
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.Size = new System.Drawing.Size(153, 70);
            // 
            // selectMapToolStripMenuItem
            // 
            this.selectMapToolStripMenuItem.Name = "selectMapToolStripMenuItem";
            this.selectMapToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.selectMapToolStripMenuItem.Text = "Select Map";
            this.selectMapToolStripMenuItem.DropDownOpening += new System.EventHandler(this.selectMapToolStripMenuItem_DropDownOpening);
            // 
            // linkPathsToolStripMenuItem
            // 
            this.linkPathsToolStripMenuItem.Checked = true;
            this.linkPathsToolStripMenuItem.CheckOnClick = true;
            this.linkPathsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.linkPathsToolStripMenuItem.Name = "linkPathsToolStripMenuItem";
            this.linkPathsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.linkPathsToolStripMenuItem.Text = "Link Paths";
            this.linkPathsToolStripMenuItem.CheckStateChanged += new System.EventHandler(this.linkPathsToolStripMenuItem_CheckStateChanged);
            this.linkPathsToolStripMenuItem.Click += new System.EventHandler(this.linkPathsToolStripMenuItem_Click);
            // 
            // ViewerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(640, 455);
            this.ContextMenuStrip = this.mnuMain;
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(100, 100);
            this.Name = "ViewerForm";
            this.Text = "Waypoint Viewer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ViewerForm_FormClosing);
            this.Load += new System.EventHandler(this.ViewerForm_Load);
            this.ResizeBegin += new System.EventHandler(this.ViewerForm_ResizeBegin);
            this.ResizeEnd += new System.EventHandler(this.ViewerForm_ResizeEnd);
            this.SizeChanged += new System.EventHandler(this.ViewerForm_SizeChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ViewerForm_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ViewerForm_KeyUp);
            this.Resize += new System.EventHandler(this.ViewerForm_Resize);
            this.mnuMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }


        #endregion

        private System.Windows.Forms.ContextMenuStrip mnuMain;
        private System.Windows.Forms.ToolStripMenuItem selectMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem linkPathsToolStripMenuItem;
    }
}