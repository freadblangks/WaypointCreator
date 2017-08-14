namespace WaypointCreator
{
    partial class WaypointForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WaypointForm));
            this.toolStripEdit = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonLoadSniff = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.ToolStripTextBoxFilterText = new System.Windows.Forms.ToolStripTextBox();
            this.ToolStripComboBoxFilterType = new System.Windows.Forms.ToolStripComboBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.WaypointContainerGridView = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.exportToUDBSQLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copySqlToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.WaypointGridView = new System.Windows.Forms.DataGridView();
            this.IndexDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.XDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.YDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ZDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ODataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WaitTImeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.SelectedDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.EntryDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GuidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataGridViewTextBoxColumnUnitFlags = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStripEdit.SuspendLayout();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.WaypointContainerGridView)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.WaypointGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStripEdit
            // 
            this.toolStripEdit.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonLoadSniff,
            this.toolStripLabel1,
            this.ToolStripTextBoxFilterText,
            this.ToolStripComboBoxFilterType});
            this.toolStripEdit.Location = new System.Drawing.Point(0, 0);
            this.toolStripEdit.Name = "toolStripEdit";
            this.toolStripEdit.Size = new System.Drawing.Size(1260, 25);
            this.toolStripEdit.TabIndex = 24;
            this.toolStripEdit.Text = "toolStrip1";
            // 
            // toolStripButtonLoadSniff
            // 
            this.toolStripButtonLoadSniff.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonLoadSniff.Image")));
            this.toolStripButtonLoadSniff.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonLoadSniff.Name = "toolStripButtonLoadSniff";
            this.toolStripButtonLoadSniff.Size = new System.Drawing.Size(90, 22);
            this.toolStripButtonLoadSniff.Text = "Import Sniff";
            this.toolStripButtonLoadSniff.ToolTipText = "Import a parsed wpp sniff file.";
            this.toolStripButtonLoadSniff.Click += new System.EventHandler(this.toolStripButtonLoadSniff_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(33, 22);
            this.toolStripLabel1.Text = "Filter";
            // 
            // ToolStripTextBoxFilterText
            // 
            this.ToolStripTextBoxFilterText.Name = "ToolStripTextBoxFilterText";
            this.ToolStripTextBoxFilterText.Size = new System.Drawing.Size(100, 25);
            this.ToolStripTextBoxFilterText.TextChanged += new System.EventHandler(this.ToolStripTextBoxFilterText_TextChanged);
            // 
            // ToolStripComboBoxFilterType
            // 
            this.ToolStripComboBoxFilterType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ToolStripComboBoxFilterType.Items.AddRange(new object[] {
            "Contains",
            "StartsWith",
            "EndsWith"});
            this.ToolStripComboBoxFilterType.Name = "ToolStripComboBoxFilterType";
            this.ToolStripComboBoxFilterType.Size = new System.Drawing.Size(121, 25);
            this.ToolStripComboBoxFilterType.SelectedIndexChanged += new System.EventHandler(this.ToolStripComboBoxFilterType_SelectedIndexChanged);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 690);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1260, 22);
            this.statusStrip.TabIndex = 27;
            this.statusStrip.Text = "statusStrip";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(86, 17);
            this.toolStripStatusLabel.Text = "No File Loaded";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.WaypointContainerGridView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.WaypointGridView);
            this.splitContainer1.Size = new System.Drawing.Size(1260, 665);
            this.splitContainer1.SplitterDistance = 503;
            this.splitContainer1.TabIndex = 28;
            // 
            // WaypointContainerGridView
            // 
            this.WaypointContainerGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.WaypointContainerGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SelectedDataGridViewCheckBoxColumn,
            this.EntryDataGridViewTextBoxColumn,
            this.GuidDataGridViewTextBoxColumn,
            this.DataGridViewTextBoxColumnUnitFlags,
            this.NameDataGridViewTextBoxColumn});
            this.WaypointContainerGridView.ContextMenuStrip = this.contextMenuStrip1;
            this.WaypointContainerGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WaypointContainerGridView.Location = new System.Drawing.Point(0, 0);
            this.WaypointContainerGridView.Name = "WaypointContainerGridView";
            this.WaypointContainerGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.WaypointContainerGridView.Size = new System.Drawing.Size(503, 665);
            this.WaypointContainerGridView.TabIndex = 26;
            this.WaypointContainerGridView.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.GridView_CellMouseDown);
            this.WaypointContainerGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.WaypointContainerGridView_CellValueChanged);
            this.WaypointContainerGridView.SelectionChanged += new System.EventHandler(this.WaypointContainerGridView_SelectionChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportToUDBSQLToolStripMenuItem,
            this.copySqlToClipboardToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(194, 48);
            // 
            // exportToUDBSQLToolStripMenuItem
            // 
            this.exportToUDBSQLToolStripMenuItem.Name = "exportToUDBSQLToolStripMenuItem";
            this.exportToUDBSQLToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.exportToUDBSQLToolStripMenuItem.Text = "Export To Sql File";
            this.exportToUDBSQLToolStripMenuItem.Click += new System.EventHandler(this.exportToUDBSQLToolStripMenuItem_Click);
            // 
            // copySqlToClipboardToolStripMenuItem
            // 
            this.copySqlToClipboardToolStripMenuItem.Name = "copySqlToClipboardToolStripMenuItem";
            this.copySqlToClipboardToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.copySqlToClipboardToolStripMenuItem.Text = "Copy Sql To Clipboard";
            this.copySqlToClipboardToolStripMenuItem.Click += new System.EventHandler(this.copySqlToClipboardToolStripMenuItem_Click);
            // 
            // WaypointGridView
            // 
            this.WaypointGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.WaypointGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IndexDataGridViewTextBoxColumn,
            this.XDataGridViewTextBoxColumn,
            this.YDataGridViewTextBoxColumn,
            this.ZDataGridViewTextBoxColumn,
            this.ODataGridViewTextBoxColumn,
            this.TimeDataGridViewTextBoxColumn,
            this.WaitTImeDataGridViewTextBoxColumn});
            this.WaypointGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WaypointGridView.Location = new System.Drawing.Point(0, 0);
            this.WaypointGridView.Name = "WaypointGridView";
            this.WaypointGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.WaypointGridView.Size = new System.Drawing.Size(753, 665);
            this.WaypointGridView.TabIndex = 1;
            this.WaypointGridView.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.GridView_CellMouseDown);
            this.WaypointGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.WaypointGridView_CellValueChanged);
            this.WaypointGridView.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.WaypointGridView_UserAddedRow);
            this.WaypointGridView.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.WaypointGridView_UserDeletedRow);
            // 
            // IndexDataGridViewTextBoxColumn
            // 
            this.IndexDataGridViewTextBoxColumn.DataPropertyName = "Index";
            this.IndexDataGridViewTextBoxColumn.HeaderText = "Index";
            this.IndexDataGridViewTextBoxColumn.Name = "IndexDataGridViewTextBoxColumn";
            // 
            // XDataGridViewTextBoxColumn
            // 
            this.XDataGridViewTextBoxColumn.DataPropertyName = "X";
            this.XDataGridViewTextBoxColumn.HeaderText = "X";
            this.XDataGridViewTextBoxColumn.Name = "XDataGridViewTextBoxColumn";
            // 
            // YDataGridViewTextBoxColumn
            // 
            this.YDataGridViewTextBoxColumn.DataPropertyName = "Y";
            this.YDataGridViewTextBoxColumn.HeaderText = "Y";
            this.YDataGridViewTextBoxColumn.Name = "YDataGridViewTextBoxColumn";
            // 
            // ZDataGridViewTextBoxColumn
            // 
            this.ZDataGridViewTextBoxColumn.DataPropertyName = "Z";
            this.ZDataGridViewTextBoxColumn.HeaderText = "Z";
            this.ZDataGridViewTextBoxColumn.Name = "ZDataGridViewTextBoxColumn";
            // 
            // ODataGridViewTextBoxColumn
            // 
            this.ODataGridViewTextBoxColumn.DataPropertyName = "O";
            this.ODataGridViewTextBoxColumn.HeaderText = "O";
            this.ODataGridViewTextBoxColumn.Name = "ODataGridViewTextBoxColumn";
            // 
            // TimeDataGridViewTextBoxColumn
            // 
            this.TimeDataGridViewTextBoxColumn.DataPropertyName = "Time";
            this.TimeDataGridViewTextBoxColumn.HeaderText = "Time";
            this.TimeDataGridViewTextBoxColumn.Name = "TimeDataGridViewTextBoxColumn";
            // 
            // WaitTImeDataGridViewTextBoxColumn
            // 
            this.WaitTImeDataGridViewTextBoxColumn.DataPropertyName = "WaitTIme";
            this.WaitTImeDataGridViewTextBoxColumn.HeaderText = "WaitTIme";
            this.WaitTImeDataGridViewTextBoxColumn.Name = "WaitTImeDataGridViewTextBoxColumn";
            // 
            // SelectedDataGridViewCheckBoxColumn
            // 
            this.SelectedDataGridViewCheckBoxColumn.DataPropertyName = "Selected";
            this.SelectedDataGridViewCheckBoxColumn.FalseValue = "False";
            this.SelectedDataGridViewCheckBoxColumn.HeaderText = "Selected";
            this.SelectedDataGridViewCheckBoxColumn.IndeterminateValue = "";
            this.SelectedDataGridViewCheckBoxColumn.Name = "SelectedDataGridViewCheckBoxColumn";
            this.SelectedDataGridViewCheckBoxColumn.TrueValue = "True";
            // 
            // EntryDataGridViewTextBoxColumn
            // 
            this.EntryDataGridViewTextBoxColumn.DataPropertyName = "Entry";
            this.EntryDataGridViewTextBoxColumn.HeaderText = "Entry";
            this.EntryDataGridViewTextBoxColumn.Name = "EntryDataGridViewTextBoxColumn";
            this.EntryDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // GuidDataGridViewTextBoxColumn
            // 
            this.GuidDataGridViewTextBoxColumn.DataPropertyName = "GUID";
            this.GuidDataGridViewTextBoxColumn.HeaderText = "GUID";
            this.GuidDataGridViewTextBoxColumn.Name = "GuidDataGridViewTextBoxColumn";
            this.GuidDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // DataGridViewTextBoxColumnUnitFlags
            // 
            this.DataGridViewTextBoxColumnUnitFlags.DataPropertyName = "UnitFlags";
            this.DataGridViewTextBoxColumnUnitFlags.HeaderText = "UnitFlags";
            this.DataGridViewTextBoxColumnUnitFlags.Name = "DataGridViewTextBoxColumnUnitFlags";
            this.DataGridViewTextBoxColumnUnitFlags.ReadOnly = true;
            this.DataGridViewTextBoxColumnUnitFlags.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // NameDataGridViewTextBoxColumn
            // 
            this.NameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.NameDataGridViewTextBoxColumn.HeaderText = "Name";
            this.NameDataGridViewTextBoxColumn.Name = "NameDataGridViewTextBoxColumn";
            this.NameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // WaypointForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1260, 712);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStripEdit);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "WaypointForm";
            this.Text = "Waypoint Manager";
            this.Load += new System.EventHandler(this.WaypointForm_Load);
            this.toolStripEdit.ResumeLayout(false);
            this.toolStripEdit.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.WaypointContainerGridView)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.WaypointGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripEdit;
        private System.Windows.Forms.ToolStripButton toolStripButtonLoadSniff;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView WaypointContainerGridView;
        private System.Windows.Forms.DataGridView WaypointGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn IndexDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn XDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn YDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ZDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ODataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn TimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn WaitTImeDataGridViewTextBoxColumn;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox ToolStripTextBoxFilterText;
        private System.Windows.Forms.ToolStripComboBox ToolStripComboBoxFilterType;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem exportToUDBSQLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copySqlToClipboardToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.DataGridViewCheckBoxColumn SelectedDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn EntryDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn GuidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataGridViewTextBoxColumnUnitFlags;
        private System.Windows.Forms.DataGridViewTextBoxColumn NameDataGridViewTextBoxColumn;
    }
}