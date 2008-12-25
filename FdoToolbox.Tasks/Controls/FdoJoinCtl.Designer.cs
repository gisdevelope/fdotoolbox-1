namespace FdoToolbox.Tasks.Controls
{
    partial class FdoJoinCtl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.cmbGeometryProperty = new System.Windows.Forms.ComboBox();
            this.numBatchSize = new System.Windows.Forms.NumericUpDown();
            this.label18 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.grdJoin = new System.Windows.Forms.DataGridView();
            this.COL_LEFT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.COL_RIGHT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chkJoinPredicate = new System.Windows.Forms.CheckBox();
            this.cmbSpatialPredicate = new System.Windows.Forms.ComboBox();
            this.cmbJoinTypes = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnDeleteJoin = new System.Windows.Forms.Button();
            this.btnAddJoin = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtTargetClass = new System.Windows.Forms.TextBox();
            this.cmbTargetConnection = new System.Windows.Forms.ComboBox();
            this.cmbTargetSchema = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.chkRightProperties = new System.Windows.Forms.CheckedListBox();
            this.cmbRightClass = new System.Windows.Forms.ComboBox();
            this.cmbRightConnection = new System.Windows.Forms.ComboBox();
            this.cmbRightSchema = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkLeftProperties = new System.Windows.Forms.CheckedListBox();
            this.label14 = new System.Windows.Forms.Label();
            this.cmbLeftClass = new System.Windows.Forms.ComboBox();
            this.cmbLeftSchema = new System.Windows.Forms.ComboBox();
            this.cmbLeftConnection = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.txtRightPrefix = new System.Windows.Forms.TextBox();
            this.txtLeftPrefix = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numBatchSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdJoin)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtName);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(640, 50);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "General";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Name";
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Location = new System.Drawing.Point(60, 19);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(574, 20);
            this.txtName.TabIndex = 0;
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.checkBox1);
            this.groupBox5.Controls.Add(this.cmbGeometryProperty);
            this.groupBox5.Controls.Add(this.numBatchSize);
            this.groupBox5.Controls.Add(this.label18);
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Controls.Add(this.grdJoin);
            this.groupBox5.Controls.Add(this.chkJoinPredicate);
            this.groupBox5.Controls.Add(this.cmbSpatialPredicate);
            this.groupBox5.Controls.Add(this.cmbJoinTypes);
            this.groupBox5.Controls.Add(this.label2);
            this.groupBox5.Location = new System.Drawing.Point(3, 240);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(639, 167);
            this.groupBox5.TabIndex = 6;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Join Options";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(243, 48);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(170, 17);
            this.checkBox1.TabIndex = 11;
            this.checkBox1.Text = "Designated Geometry Property";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // cmbGeometryProperty
            // 
            this.cmbGeometryProperty.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbGeometryProperty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGeometryProperty.FormattingEnabled = true;
            this.cmbGeometryProperty.Location = new System.Drawing.Point(430, 46);
            this.cmbGeometryProperty.Name = "cmbGeometryProperty";
            this.cmbGeometryProperty.Size = new System.Drawing.Size(198, 21);
            this.cmbGeometryProperty.TabIndex = 10;
            // 
            // numBatchSize
            // 
            this.numBatchSize.Location = new System.Drawing.Point(106, 47);
            this.numBatchSize.Name = "numBatchSize";
            this.numBatchSize.Size = new System.Drawing.Size(120, 20);
            this.numBatchSize.TabIndex = 8;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(10, 49);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(87, 13);
            this.label18.TabIndex = 7;
            this.label18.Text = "Batch Insert Size";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 77);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Joined Properties";
            // 
            // grdJoin
            // 
            this.grdJoin.AllowUserToAddRows = false;
            this.grdJoin.AllowUserToDeleteRows = false;
            this.grdJoin.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdJoin.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdJoin.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.COL_LEFT,
            this.COL_RIGHT});
            this.grdJoin.Location = new System.Drawing.Point(13, 102);
            this.grdJoin.Name = "grdJoin";
            this.grdJoin.RowHeadersVisible = false;
            this.grdJoin.Size = new System.Drawing.Size(621, 59);
            this.grdJoin.TabIndex = 5;
            // 
            // COL_LEFT
            // 
            this.COL_LEFT.HeaderText = "Left Property";
            this.COL_LEFT.Name = "COL_LEFT";
            // 
            // COL_RIGHT
            // 
            this.COL_RIGHT.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.COL_RIGHT.HeaderText = "Right Property";
            this.COL_RIGHT.Name = "COL_RIGHT";
            // 
            // chkJoinPredicate
            // 
            this.chkJoinPredicate.AutoSize = true;
            this.chkJoinPredicate.Location = new System.Drawing.Point(285, 19);
            this.chkJoinPredicate.Name = "chkJoinPredicate";
            this.chkJoinPredicate.Size = new System.Drawing.Size(128, 17);
            this.chkJoinPredicate.TabIndex = 4;
            this.chkJoinPredicate.Text = "Spatial Join Predicate";
            this.chkJoinPredicate.UseVisualStyleBackColor = true;
            // 
            // cmbSpatialPredicate
            // 
            this.cmbSpatialPredicate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbSpatialPredicate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSpatialPredicate.FormattingEnabled = true;
            this.cmbSpatialPredicate.Location = new System.Drawing.Point(430, 17);
            this.cmbSpatialPredicate.Name = "cmbSpatialPredicate";
            this.cmbSpatialPredicate.Size = new System.Drawing.Size(198, 21);
            this.cmbSpatialPredicate.TabIndex = 3;
            // 
            // cmbJoinTypes
            // 
            this.cmbJoinTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbJoinTypes.FormattingEnabled = true;
            this.cmbJoinTypes.Location = new System.Drawing.Point(77, 17);
            this.cmbJoinTypes.Name = "cmbJoinTypes";
            this.cmbJoinTypes.Size = new System.Drawing.Size(149, 21);
            this.cmbJoinTypes.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Join Type";
            // 
            // btnDeleteJoin
            // 
            this.btnDeleteJoin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDeleteJoin.Location = new System.Drawing.Point(97, 413);
            this.btnDeleteJoin.Name = "btnDeleteJoin";
            this.btnDeleteJoin.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteJoin.TabIndex = 8;
            this.btnDeleteJoin.Text = "Delete Join";
            this.btnDeleteJoin.UseVisualStyleBackColor = true;
            // 
            // btnAddJoin
            // 
            this.btnAddJoin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddJoin.Location = new System.Drawing.Point(16, 413);
            this.btnAddJoin.Name = "btnAddJoin";
            this.btnAddJoin.Size = new System.Drawing.Size(75, 23);
            this.btnAddJoin.TabIndex = 7;
            this.btnAddJoin.Text = "Add Join";
            this.btnAddJoin.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.txtTargetClass);
            this.groupBox4.Controls.Add(this.cmbTargetConnection);
            this.groupBox4.Controls.Add(this.cmbTargetSchema);
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Location = new System.Drawing.Point(433, 59);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(209, 96);
            this.groupBox4.TabIndex = 9;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Target";
            // 
            // txtTargetClass
            // 
            this.txtTargetClass.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTargetClass.Location = new System.Drawing.Point(77, 66);
            this.txtTargetClass.Name = "txtTargetClass";
            this.txtTargetClass.Size = new System.Drawing.Size(121, 20);
            this.txtTargetClass.TabIndex = 17;
            // 
            // cmbTargetConnection
            // 
            this.cmbTargetConnection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbTargetConnection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTargetConnection.FormattingEnabled = true;
            this.cmbTargetConnection.Location = new System.Drawing.Point(77, 19);
            this.cmbTargetConnection.Name = "cmbTargetConnection";
            this.cmbTargetConnection.Size = new System.Drawing.Size(121, 21);
            this.cmbTargetConnection.TabIndex = 15;
            this.cmbTargetConnection.SelectionChangeCommitted += new System.EventHandler(this.cmbTargetConnection_SelectionChangeCommitted);
            // 
            // cmbTargetSchema
            // 
            this.cmbTargetSchema.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbTargetSchema.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTargetSchema.FormattingEnabled = true;
            this.cmbTargetSchema.Location = new System.Drawing.Point(77, 43);
            this.cmbTargetSchema.Name = "cmbTargetSchema";
            this.cmbTargetSchema.Size = new System.Drawing.Size(121, 21);
            this.cmbTargetSchema.TabIndex = 16;
            this.cmbTargetSchema.SelectionChangeCommitted += new System.EventHandler(this.cmbTargetSchema_SelectionChangeCommitted);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(10, 22);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(61, 13);
            this.label13.TabIndex = 12;
            this.label13.Text = "Connection";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(10, 46);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(46, 13);
            this.label12.TabIndex = 13;
            this.label12.Text = "Schema";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(10, 69);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(32, 13);
            this.label11.TabIndex = 14;
            this.label11.Text = "Class";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.chkRightProperties);
            this.groupBox3.Controls.Add(this.cmbRightClass);
            this.groupBox3.Controls.Add(this.cmbRightConnection);
            this.groupBox3.Controls.Add(this.cmbRightSchema);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Location = new System.Drawing.Point(218, 59);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(209, 175);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Right Source";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(10, 92);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(54, 13);
            this.label15.TabIndex = 8;
            this.label15.Text = "Properties";
            // 
            // chkRightProperties
            // 
            this.chkRightProperties.FormattingEnabled = true;
            this.chkRightProperties.Location = new System.Drawing.Point(77, 92);
            this.chkRightProperties.Name = "chkRightProperties";
            this.chkRightProperties.Size = new System.Drawing.Size(121, 79);
            this.chkRightProperties.TabIndex = 8;
            // 
            // cmbRightClass
            // 
            this.cmbRightClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRightClass.FormattingEnabled = true;
            this.cmbRightClass.Location = new System.Drawing.Point(77, 66);
            this.cmbRightClass.Name = "cmbRightClass";
            this.cmbRightClass.Size = new System.Drawing.Size(121, 21);
            this.cmbRightClass.TabIndex = 11;
            this.cmbRightClass.SelectionChangeCommitted += new System.EventHandler(this.cmbRightClass_SelectionChangeCommitted);
            // 
            // cmbRightConnection
            // 
            this.cmbRightConnection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRightConnection.FormattingEnabled = true;
            this.cmbRightConnection.Location = new System.Drawing.Point(77, 19);
            this.cmbRightConnection.Name = "cmbRightConnection";
            this.cmbRightConnection.Size = new System.Drawing.Size(121, 21);
            this.cmbRightConnection.TabIndex = 9;
            this.cmbRightConnection.SelectionChangeCommitted += new System.EventHandler(this.cmbRightConnection_SelectionChangeCommitted);
            // 
            // cmbRightSchema
            // 
            this.cmbRightSchema.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRightSchema.FormattingEnabled = true;
            this.cmbRightSchema.Location = new System.Drawing.Point(77, 43);
            this.cmbRightSchema.Name = "cmbRightSchema";
            this.cmbRightSchema.Size = new System.Drawing.Size(121, 21);
            this.cmbRightSchema.TabIndex = 10;
            this.cmbRightSchema.SelectionChangeCommitted += new System.EventHandler(this.cmbRightSchema_SelectionChangeCommitted);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(10, 22);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(61, 13);
            this.label10.TabIndex = 6;
            this.label10.Text = "Connection";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(10, 46);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(46, 13);
            this.label9.TabIndex = 7;
            this.label9.Text = "Schema";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 69);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(32, 13);
            this.label8.TabIndex = 8;
            this.label8.Text = "Class";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkLeftProperties);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.cmbLeftClass);
            this.groupBox2.Controls.Add(this.cmbLeftSchema);
            this.groupBox2.Controls.Add(this.cmbLeftConnection);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(3, 59);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(209, 175);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Left Source";
            // 
            // chkLeftProperties
            // 
            this.chkLeftProperties.FormattingEnabled = true;
            this.chkLeftProperties.Location = new System.Drawing.Point(77, 92);
            this.chkLeftProperties.Name = "chkLeftProperties";
            this.chkLeftProperties.Size = new System.Drawing.Size(121, 79);
            this.chkLeftProperties.TabIndex = 7;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(10, 92);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(54, 13);
            this.label14.TabIndex = 6;
            this.label14.Text = "Properties";
            // 
            // cmbLeftClass
            // 
            this.cmbLeftClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLeftClass.FormattingEnabled = true;
            this.cmbLeftClass.Location = new System.Drawing.Point(77, 64);
            this.cmbLeftClass.Name = "cmbLeftClass";
            this.cmbLeftClass.Size = new System.Drawing.Size(121, 21);
            this.cmbLeftClass.TabIndex = 5;
            this.cmbLeftClass.SelectionChangeCommitted += new System.EventHandler(this.cmbLeftClass_SelectionChangeCommitted);
            // 
            // cmbLeftSchema
            // 
            this.cmbLeftSchema.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLeftSchema.FormattingEnabled = true;
            this.cmbLeftSchema.Location = new System.Drawing.Point(77, 41);
            this.cmbLeftSchema.Name = "cmbLeftSchema";
            this.cmbLeftSchema.Size = new System.Drawing.Size(121, 21);
            this.cmbLeftSchema.TabIndex = 4;
            this.cmbLeftSchema.SelectionChangeCommitted += new System.EventHandler(this.cmbLeftSchema_SelectionChangeCommitted);
            // 
            // cmbLeftConnection
            // 
            this.cmbLeftConnection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLeftConnection.FormattingEnabled = true;
            this.cmbLeftConnection.Location = new System.Drawing.Point(77, 17);
            this.cmbLeftConnection.Name = "cmbLeftConnection";
            this.cmbLeftConnection.Size = new System.Drawing.Size(121, 21);
            this.cmbLeftConnection.TabIndex = 3;
            this.cmbLeftConnection.SelectionChangeCommitted += new System.EventHandler(this.cmbLeftConnection_SelectionChangeCommitted);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 69);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(32, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Class";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 44);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Schema";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Connection";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(575, 413);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(67, 23);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox6.Controls.Add(this.txtRightPrefix);
            this.groupBox6.Controls.Add(this.txtLeftPrefix);
            this.groupBox6.Controls.Add(this.label17);
            this.groupBox6.Controls.Add(this.label16);
            this.groupBox6.Location = new System.Drawing.Point(433, 161);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(209, 73);
            this.groupBox6.TabIndex = 11;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Property Prefixes";
            // 
            // txtRightPrefix
            // 
            this.txtRightPrefix.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRightPrefix.Location = new System.Drawing.Point(77, 40);
            this.txtRightPrefix.Name = "txtRightPrefix";
            this.txtRightPrefix.Size = new System.Drawing.Size(121, 20);
            this.txtRightPrefix.TabIndex = 3;
            // 
            // txtLeftPrefix
            // 
            this.txtLeftPrefix.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLeftPrefix.Location = new System.Drawing.Point(77, 17);
            this.txtLeftPrefix.Name = "txtLeftPrefix";
            this.txtLeftPrefix.Size = new System.Drawing.Size(121, 20);
            this.txtLeftPrefix.TabIndex = 2;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(10, 43);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(32, 13);
            this.label17.TabIndex = 1;
            this.label17.Text = "Right";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(10, 20);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(25, 13);
            this.label16.TabIndex = 0;
            this.label16.Text = "Left";
            // 
            // FdoJoinCtl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnDeleteJoin);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.btnAddJoin);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox1);
            this.Name = "FdoJoinCtl";
            this.Size = new System.Drawing.Size(646, 439);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numBatchSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdJoin)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnDeleteJoin;
        private System.Windows.Forms.Button btnAddJoin;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView grdJoin;
        private System.Windows.Forms.DataGridViewTextBoxColumn COL_LEFT;
        private System.Windows.Forms.DataGridViewTextBoxColumn COL_RIGHT;
        private System.Windows.Forms.CheckBox chkJoinPredicate;
        private System.Windows.Forms.ComboBox cmbSpatialPredicate;
        private System.Windows.Forms.ComboBox cmbJoinTypes;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox cmbTargetConnection;
        private System.Windows.Forms.ComboBox cmbTargetSchema;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cmbRightClass;
        private System.Windows.Forms.ComboBox cmbRightConnection;
        private System.Windows.Forms.ComboBox cmbRightSchema;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cmbLeftClass;
        private System.Windows.Forms.ComboBox cmbLeftSchema;
        private System.Windows.Forms.ComboBox cmbLeftConnection;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.CheckedListBox chkLeftProperties;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.CheckedListBox chkRightProperties;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TextBox txtRightPrefix;
        private System.Windows.Forms.TextBox txtLeftPrefix;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.NumericUpDown numBatchSize;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ComboBox cmbGeometryProperty;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox txtTargetClass;

    }
}
