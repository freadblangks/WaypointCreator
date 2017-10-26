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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WaypointForm));
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.WaypointContainerGridView = new System.Windows.Forms.DataGridView();
            this.SelectedDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.DataGridViewTextBoxColumnIndex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GuidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EntryDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WaypointSouceTypeDataGridViewTextColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InCombatDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.selectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectAllToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.selectWithSameGuidToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deselectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mergeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mergeAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mergeUniqueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateAverageWaypointToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateAverageWaitTimeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateExactWaitTimeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportSpawnWithCreatureMovementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportSpawnOnlyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copySpawnWithCreatureMovementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copySpawnOnlyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.WaypointGridView = new System.Windows.Forms.DataGridView();
            this.IndexDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EstimatedSpawnDistanceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsAverageDataGridViewCheckBox = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.XDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.YDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ZDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ODataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WaitTImeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyGoXYZToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.toolStripButtonLoadSniff = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.filterToolStripComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripComboBoxFilterType = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripEdit = new System.Windows.Forms.ToolStrip();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.WaypointContainerGridView)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.WaypointGridView)).BeginInit();
            this.contextMenuStrip2.SuspendLayout();
            this.toolStripEdit.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel,
            this.toolStripProgressBar1});
            this.statusStrip.Location = new System.Drawing.Point(0, 690);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1287, 22);
            this.statusStrip.TabIndex = 27;
            this.statusStrip.Text = "statusStrip";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(86, 17);
            this.toolStripStatusLabel.Text = "No File Loaded";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
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
            this.splitContainer1.Size = new System.Drawing.Size(1287, 665);
            this.splitContainer1.SplitterDistance = 513;
            this.splitContainer1.TabIndex = 28;
            // 
            // WaypointContainerGridView
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.WaypointContainerGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.WaypointContainerGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.WaypointContainerGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SelectedDataGridViewCheckBoxColumn,
            this.DataGridViewTextBoxColumnIndex,
            this.GuidDataGridViewTextBoxColumn,
            this.EntryDataGridViewTextBoxColumn,
            this.NameDataGridViewTextBoxColumn,
            this.WaypointSouceTypeDataGridViewTextColumn,
            this.InCombatDataGridViewCheckBoxColumn});
            this.WaypointContainerGridView.ContextMenuStrip = this.contextMenuStrip1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.WaypointContainerGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.WaypointContainerGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WaypointContainerGridView.Location = new System.Drawing.Point(0, 0);
            this.WaypointContainerGridView.Name = "WaypointContainerGridView";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.WaypointContainerGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.WaypointContainerGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.WaypointContainerGridView.Size = new System.Drawing.Size(513, 665);
            this.WaypointContainerGridView.TabIndex = 26;
            this.WaypointContainerGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.WaypointContainerGridView_CellDoubleClick);
            this.WaypointContainerGridView.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.GridView_CellMouseDown);
            this.WaypointContainerGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.WaypointContainerGridView_CellValueChanged);
            this.WaypointContainerGridView.SelectionChanged += new System.EventHandler(this.WaypointContainerGridView_SelectionChanged);
            // 
            // SelectedDataGridViewCheckBoxColumn
            // 
            this.SelectedDataGridViewCheckBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.SelectedDataGridViewCheckBoxColumn.DataPropertyName = "Selected";
            this.SelectedDataGridViewCheckBoxColumn.FalseValue = "False";
            this.SelectedDataGridViewCheckBoxColumn.HeaderText = "Selected";
            this.SelectedDataGridViewCheckBoxColumn.IndeterminateValue = "";
            this.SelectedDataGridViewCheckBoxColumn.Name = "SelectedDataGridViewCheckBoxColumn";
            this.SelectedDataGridViewCheckBoxColumn.TrueValue = "True";
            this.SelectedDataGridViewCheckBoxColumn.Width = 55;
            // 
            // DataGridViewTextBoxColumnIndex
            // 
            this.DataGridViewTextBoxColumnIndex.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.DataGridViewTextBoxColumnIndex.DataPropertyName = "Index";
            this.DataGridViewTextBoxColumnIndex.HeaderText = "Index";
            this.DataGridViewTextBoxColumnIndex.Name = "DataGridViewTextBoxColumnIndex";
            this.DataGridViewTextBoxColumnIndex.ReadOnly = true;
            this.DataGridViewTextBoxColumnIndex.Width = 58;
            // 
            // GuidDataGridViewTextBoxColumn
            // 
            this.GuidDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.GuidDataGridViewTextBoxColumn.DataPropertyName = "GUID";
            this.GuidDataGridViewTextBoxColumn.HeaderText = "GUID";
            this.GuidDataGridViewTextBoxColumn.Name = "GuidDataGridViewTextBoxColumn";
            this.GuidDataGridViewTextBoxColumn.ReadOnly = true;
            this.GuidDataGridViewTextBoxColumn.Width = 21;
            // 
            // EntryDataGridViewTextBoxColumn
            // 
            this.EntryDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.EntryDataGridViewTextBoxColumn.DataPropertyName = "Entry";
            this.EntryDataGridViewTextBoxColumn.HeaderText = "Entry";
            this.EntryDataGridViewTextBoxColumn.Name = "EntryDataGridViewTextBoxColumn";
            this.EntryDataGridViewTextBoxColumn.ReadOnly = true;
            this.EntryDataGridViewTextBoxColumn.Width = 56;
            // 
            // NameDataGridViewTextBoxColumn
            // 
            this.NameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.NameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.NameDataGridViewTextBoxColumn.HeaderText = "Name";
            this.NameDataGridViewTextBoxColumn.Name = "NameDataGridViewTextBoxColumn";
            this.NameDataGridViewTextBoxColumn.ReadOnly = true;
            this.NameDataGridViewTextBoxColumn.Width = 21;
            // 
            // WaypointSouceTypeDataGridViewTextColumn
            // 
            this.WaypointSouceTypeDataGridViewTextColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.WaypointSouceTypeDataGridViewTextColumn.DataPropertyName = "WaypointSouceType";
            this.WaypointSouceTypeDataGridViewTextColumn.HeaderText = "WaypointSouceType";
            this.WaypointSouceTypeDataGridViewTextColumn.Name = "WaypointSouceTypeDataGridViewTextColumn";
            this.WaypointSouceTypeDataGridViewTextColumn.ReadOnly = true;
            this.WaypointSouceTypeDataGridViewTextColumn.Width = 132;
            // 
            // InCombatDataGridViewCheckBoxColumn
            // 
            this.InCombatDataGridViewCheckBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.InCombatDataGridViewCheckBoxColumn.DataPropertyName = "InCombat";
            this.InCombatDataGridViewCheckBoxColumn.HeaderText = "In Combat";
            this.InCombatDataGridViewCheckBoxColumn.Name = "InCombatDataGridViewCheckBoxColumn";
            this.InCombatDataGridViewCheckBoxColumn.ReadOnly = true;
            this.InCombatDataGridViewCheckBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.InCombatDataGridViewCheckBoxColumn.Width = 80;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectToolStripMenuItem,
            this.deselectAllToolStripMenuItem,
            this.toolStripMenuItem1,
            this.mergeToolStripMenuItem,
            this.generateToolStripMenuItem,
            this.exportToolStripMenuItem,
            this.copyToClipboardToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(175, 142);
            // 
            // selectToolStripMenuItem
            // 
            this.selectToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectAllToolStripMenuItem1,
            this.selectWithSameGuidToolStripMenuItem});
            this.selectToolStripMenuItem.Name = "selectToolStripMenuItem";
            this.selectToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.selectToolStripMenuItem.Text = "Select";
            // 
            // selectAllToolStripMenuItem1
            // 
            this.selectAllToolStripMenuItem1.Name = "selectAllToolStripMenuItem1";
            this.selectAllToolStripMenuItem1.Size = new System.Drawing.Size(159, 22);
            this.selectAllToolStripMenuItem1.Text = "All";
            this.selectAllToolStripMenuItem1.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
            // 
            // selectWithSameGuidToolStripMenuItem
            // 
            this.selectWithSameGuidToolStripMenuItem.Name = "selectWithSameGuidToolStripMenuItem";
            this.selectWithSameGuidToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.selectWithSameGuidToolStripMenuItem.Text = "With Same Guid";
            this.selectWithSameGuidToolStripMenuItem.Click += new System.EventHandler(this.selectWithSameGuidToolStripMenuItem_Click);
            // 
            // deselectAllToolStripMenuItem
            // 
            this.deselectAllToolStripMenuItem.Name = "deselectAllToolStripMenuItem";
            this.deselectAllToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.deselectAllToolStripMenuItem.Text = "Deselect All";
            this.deselectAllToolStripMenuItem.Click += new System.EventHandler(this.deselectAllToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(171, 6);
            // 
            // mergeToolStripMenuItem
            // 
            this.mergeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mergeAllToolStripMenuItem,
            this.mergeUniqueToolStripMenuItem});
            this.mergeToolStripMenuItem.Name = "mergeToolStripMenuItem";
            this.mergeToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.mergeToolStripMenuItem.Text = "Merge";
            // 
            // mergeAllToolStripMenuItem
            // 
            this.mergeAllToolStripMenuItem.Name = "mergeAllToolStripMenuItem";
            this.mergeAllToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.mergeAllToolStripMenuItem.Text = "All";
            this.mergeAllToolStripMenuItem.Click += new System.EventHandler(this.mergeAllToolStripMenuItem_Click);
            // 
            // mergeUniqueToolStripMenuItem
            // 
            this.mergeUniqueToolStripMenuItem.Name = "mergeUniqueToolStripMenuItem";
            this.mergeUniqueToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.mergeUniqueToolStripMenuItem.Text = "Unique";
            this.mergeUniqueToolStripMenuItem.Click += new System.EventHandler(this.mergeUniqueToolStripMenuItem_Click);
            // 
            // generateToolStripMenuItem
            // 
            this.generateToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.generateAverageWaypointToolStripMenuItem,
            this.generateAverageWaitTimeToolStripMenuItem,
            this.generateExactWaitTimeToolStripMenuItem});
            this.generateToolStripMenuItem.Name = "generateToolStripMenuItem";
            this.generateToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.generateToolStripMenuItem.Text = "Generat";
            // 
            // generateAverageWaypointToolStripMenuItem
            // 
            this.generateAverageWaypointToolStripMenuItem.Name = "generateAverageWaypointToolStripMenuItem";
            this.generateAverageWaypointToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.generateAverageWaypointToolStripMenuItem.Text = " Average Waypoint";
            this.generateAverageWaypointToolStripMenuItem.Click += new System.EventHandler(this.generateAverageWaypointToolStripMenuItem_Click);
            // 
            // generateAverageWaitTimeToolStripMenuItem
            // 
            this.generateAverageWaitTimeToolStripMenuItem.Name = "generateAverageWaitTimeToolStripMenuItem";
            this.generateAverageWaitTimeToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.generateAverageWaitTimeToolStripMenuItem.Text = "Average WaitTime";
            this.generateAverageWaitTimeToolStripMenuItem.Click += new System.EventHandler(this.generateAverageWaitTimeToolStripMenuItem_Click);
            // 
            // generateExactWaitTimeToolStripMenuItem
            // 
            this.generateExactWaitTimeToolStripMenuItem.Name = "generateExactWaitTimeToolStripMenuItem";
            this.generateExactWaitTimeToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.generateExactWaitTimeToolStripMenuItem.Text = "Exact WaitTime";
            this.generateExactWaitTimeToolStripMenuItem.Click += new System.EventHandler(this.generateExactWaitTimeToolStripMenuItem_Click);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportSpawnWithCreatureMovementToolStripMenuItem,
            this.exportSpawnOnlyToolStripMenuItem});
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.exportToolStripMenuItem.Text = "Export";
            this.exportToolStripMenuItem.Click += new System.EventHandler(this.exportToUDBSQLToolStripMenuItem_Click);
            // 
            // exportSpawnWithCreatureMovementToolStripMenuItem
            // 
            this.exportSpawnWithCreatureMovementToolStripMenuItem.Name = "exportSpawnWithCreatureMovementToolStripMenuItem";
            this.exportSpawnWithCreatureMovementToolStripMenuItem.Size = new System.Drawing.Size(246, 22);
            this.exportSpawnWithCreatureMovementToolStripMenuItem.Text = "Spawn With Creature Movement";
            this.exportSpawnWithCreatureMovementToolStripMenuItem.Click += new System.EventHandler(this.exportSpawnWithCreatureMovementToolStripMenuItem_Click);
            // 
            // exportSpawnOnlyToolStripMenuItem
            // 
            this.exportSpawnOnlyToolStripMenuItem.Name = "exportSpawnOnlyToolStripMenuItem";
            this.exportSpawnOnlyToolStripMenuItem.Size = new System.Drawing.Size(246, 22);
            this.exportSpawnOnlyToolStripMenuItem.Text = "Spawn Only";
            this.exportSpawnOnlyToolStripMenuItem.Click += new System.EventHandler(this.exportSpawnOnlyToolStripMenuItem_Click);
            // 
            // copyToClipboardToolStripMenuItem
            // 
            this.copyToClipboardToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copySpawnWithCreatureMovementToolStripMenuItem,
            this.copySpawnOnlyToolStripMenuItem});
            this.copyToClipboardToolStripMenuItem.Name = "copyToClipboardToolStripMenuItem";
            this.copyToClipboardToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.copyToClipboardToolStripMenuItem.Text = "Copy To Clipboard";
            // 
            // copySpawnWithCreatureMovementToolStripMenuItem
            // 
            this.copySpawnWithCreatureMovementToolStripMenuItem.Name = "copySpawnWithCreatureMovementToolStripMenuItem";
            this.copySpawnWithCreatureMovementToolStripMenuItem.Size = new System.Drawing.Size(246, 22);
            this.copySpawnWithCreatureMovementToolStripMenuItem.Text = "Spawn With Creature Movement";
            this.copySpawnWithCreatureMovementToolStripMenuItem.Click += new System.EventHandler(this.copySpawnWithCreatureMovementToolStripMenuItem_Click);
            // 
            // copySpawnOnlyToolStripMenuItem
            // 
            this.copySpawnOnlyToolStripMenuItem.Name = "copySpawnOnlyToolStripMenuItem";
            this.copySpawnOnlyToolStripMenuItem.Size = new System.Drawing.Size(246, 22);
            this.copySpawnOnlyToolStripMenuItem.Text = "Spawn Only";
            this.copySpawnOnlyToolStripMenuItem.Click += new System.EventHandler(this.copySpawnOnlyToolStripMenuItem_Click);
            // 
            // WaypointGridView
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.WaypointGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.WaypointGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.WaypointGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IndexDataGridViewTextBoxColumn,
            this.EstimatedSpawnDistanceDataGridViewTextBoxColumn,
            this.IsAverageDataGridViewCheckBox,
            this.XDataGridViewTextBoxColumn,
            this.YDataGridViewTextBoxColumn,
            this.ZDataGridViewTextBoxColumn,
            this.ODataGridViewTextBoxColumn,
            this.TimeDataGridViewTextBoxColumn,
            this.WaitTImeDataGridViewTextBoxColumn});
            this.WaypointGridView.ContextMenuStrip = this.contextMenuStrip2;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.WaypointGridView.DefaultCellStyle = dataGridViewCellStyle5;
            this.WaypointGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WaypointGridView.Location = new System.Drawing.Point(0, 0);
            this.WaypointGridView.Name = "WaypointGridView";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.WaypointGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.WaypointGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.WaypointGridView.Size = new System.Drawing.Size(770, 665);
            this.WaypointGridView.TabIndex = 1;
            this.WaypointGridView.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.GridView_CellMouseDown);
            this.WaypointGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.WaypointGridView_CellValueChanged);
            this.WaypointGridView.SelectionChanged += new System.EventHandler(this.WaypointGridView_SelectionChanged);
            this.WaypointGridView.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.WaypointGridView_UserAddedRow);
            this.WaypointGridView.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.WaypointGridView_UserDeletedRow);
            // 
            // IndexDataGridViewTextBoxColumn
            // 
            this.IndexDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.IndexDataGridViewTextBoxColumn.DataPropertyName = "Index";
            this.IndexDataGridViewTextBoxColumn.HeaderText = "Index";
            this.IndexDataGridViewTextBoxColumn.Name = "IndexDataGridViewTextBoxColumn";
            this.IndexDataGridViewTextBoxColumn.Width = 58;
            // 
            // EstimatedSpawnDistanceDataGridViewTextBoxColumn
            // 
            this.EstimatedSpawnDistanceDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.EstimatedSpawnDistanceDataGridViewTextBoxColumn.DataPropertyName = "EstimatedSpawnDistance";
            this.EstimatedSpawnDistanceDataGridViewTextBoxColumn.HeaderText = "Est. Spawn Dist";
            this.EstimatedSpawnDistanceDataGridViewTextBoxColumn.Name = "EstimatedSpawnDistanceDataGridViewTextBoxColumn";
            this.EstimatedSpawnDistanceDataGridViewTextBoxColumn.ReadOnly = true;
            this.EstimatedSpawnDistanceDataGridViewTextBoxColumn.Width = 98;
            // 
            // IsAverageDataGridViewCheckBox
            // 
            this.IsAverageDataGridViewCheckBox.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.IsAverageDataGridViewCheckBox.DataPropertyName = "IsAverage";
            this.IsAverageDataGridViewCheckBox.HeaderText = "IsAverage";
            this.IsAverageDataGridViewCheckBox.Name = "IsAverageDataGridViewCheckBox";
            this.IsAverageDataGridViewCheckBox.ReadOnly = true;
            this.IsAverageDataGridViewCheckBox.Width = 61;
            // 
            // XDataGridViewTextBoxColumn
            // 
            this.XDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.XDataGridViewTextBoxColumn.DataPropertyName = "X";
            this.XDataGridViewTextBoxColumn.HeaderText = "X";
            this.XDataGridViewTextBoxColumn.Name = "XDataGridViewTextBoxColumn";
            this.XDataGridViewTextBoxColumn.Width = 21;
            // 
            // YDataGridViewTextBoxColumn
            // 
            this.YDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.YDataGridViewTextBoxColumn.DataPropertyName = "Y";
            this.YDataGridViewTextBoxColumn.HeaderText = "Y";
            this.YDataGridViewTextBoxColumn.Name = "YDataGridViewTextBoxColumn";
            this.YDataGridViewTextBoxColumn.Width = 21;
            // 
            // ZDataGridViewTextBoxColumn
            // 
            this.ZDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.ZDataGridViewTextBoxColumn.DataPropertyName = "Z";
            this.ZDataGridViewTextBoxColumn.HeaderText = "Z";
            this.ZDataGridViewTextBoxColumn.Name = "ZDataGridViewTextBoxColumn";
            this.ZDataGridViewTextBoxColumn.Width = 21;
            // 
            // ODataGridViewTextBoxColumn
            // 
            this.ODataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.ODataGridViewTextBoxColumn.DataPropertyName = "O";
            this.ODataGridViewTextBoxColumn.HeaderText = "O";
            this.ODataGridViewTextBoxColumn.Name = "ODataGridViewTextBoxColumn";
            this.ODataGridViewTextBoxColumn.Width = 21;
            // 
            // TimeDataGridViewTextBoxColumn
            // 
            this.TimeDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.TimeDataGridViewTextBoxColumn.DataPropertyName = "Time";
            this.TimeDataGridViewTextBoxColumn.HeaderText = "Time";
            this.TimeDataGridViewTextBoxColumn.Name = "TimeDataGridViewTextBoxColumn";
            this.TimeDataGridViewTextBoxColumn.Width = 55;
            // 
            // WaitTImeDataGridViewTextBoxColumn
            // 
            this.WaitTImeDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.WaitTImeDataGridViewTextBoxColumn.DataPropertyName = "WaitTime";
            this.WaitTImeDataGridViewTextBoxColumn.HeaderText = "WaitTime";
            this.WaitTImeDataGridViewTextBoxColumn.Name = "WaitTImeDataGridViewTextBoxColumn";
            this.WaitTImeDataGridViewTextBoxColumn.Width = 77;
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyGoXYZToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(154, 26);
            // 
            // copyGoXYZToolStripMenuItem
            // 
            this.copyGoXYZToolStripMenuItem.Name = "copyGoXYZToolStripMenuItem";
            this.copyGoXYZToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.copyGoXYZToolStripMenuItem.Text = "Copy .Go X Y Z";
            this.copyGoXYZToolStripMenuItem.Click += new System.EventHandler(this.copyGoXYZToolStripMenuItem_Click);
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
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(66, 22);
            this.toolStripButton2.Text = "Save Work";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(68, 22);
            this.toolStripButton1.Text = "Load Work";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(33, 22);
            this.toolStripLabel1.Text = "Filter";
            // 
            // filterToolStripComboBox
            // 
            this.filterToolStripComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.filterToolStripComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.filterToolStripComboBox.DropDownWidth = 300;
            this.filterToolStripComboBox.Name = "filterToolStripComboBox";
            this.filterToolStripComboBox.Size = new System.Drawing.Size(300, 25);
            this.filterToolStripComboBox.TextChanged += new System.EventHandler(this.filterToolStripComboBox_TextChanged);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
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
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripEdit
            // 
            this.toolStripEdit.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonLoadSniff,
            this.toolStripSeparator5,
            this.toolStripButton2,
            this.toolStripSeparator6,
            this.toolStripButton1,
            this.toolStripSeparator4,
            this.toolStripLabel1,
            this.toolStripSeparator3,
            this.filterToolStripComboBox,
            this.ToolStripComboBoxFilterType,
            this.toolStripSeparator1});
            this.toolStripEdit.Location = new System.Drawing.Point(0, 0);
            this.toolStripEdit.Name = "toolStripEdit";
            this.toolStripEdit.Size = new System.Drawing.Size(1287, 25);
            this.toolStripEdit.TabIndex = 24;
            this.toolStripEdit.Text = "toolStrip1";
            // 
            // WaypointForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1287, 712);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStripEdit);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "WaypointForm";
            this.Text = "Waypoint Manager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WaypointForm_FormClosing);
            this.Load += new System.EventHandler(this.WaypointForm_Load);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.WaypointContainerGridView)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.WaypointGridView)).EndInit();
            this.contextMenuStrip2.ResumeLayout(false);
            this.toolStripEdit.ResumeLayout(false);
            this.toolStripEdit.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView WaypointContainerGridView;
        private System.Windows.Forms.DataGridView WaypointGridView;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ToolStripMenuItem selectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deselectAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripMenuItem mergeToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem copyGoXYZToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mergeAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mergeUniqueToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exportSpawnWithCreatureMovementToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportSpawnOnlyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem selectWithSameGuidToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generateAverageWaypointToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToClipboardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copySpawnWithCreatureMovementToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copySpawnOnlyToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButtonLoadSniff;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox filterToolStripComboBox;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripComboBox ToolStripComboBoxFilterType;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStrip toolStripEdit;
        private System.Windows.Forms.ToolStripMenuItem generateAverageWaitTimeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generateExactWaitTimeToolStripMenuItem;
        private System.Windows.Forms.DataGridViewCheckBoxColumn SelectedDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataGridViewTextBoxColumnIndex;
        private System.Windows.Forms.DataGridViewTextBoxColumn GuidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn EntryDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn NameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn WaypointSouceTypeDataGridViewTextColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn InCombatDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn IndexDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn EstimatedSpawnDistanceDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsAverageDataGridViewCheckBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn XDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn YDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ZDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ODataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn TimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn WaitTImeDataGridViewTextBoxColumn;
    }
}