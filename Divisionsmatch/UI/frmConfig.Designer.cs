namespace Divisionsmatch
{
    partial class frmConfig
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.comboBoxKreds = new System.Windows.Forms.ComboBox();
            this.cmbDivision = new System.Windows.Forms.ComboBox();
            this.btnOpenKlasser = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.txtTXTFileKlasser = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lstAllClubs = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.checkedListClubs = new System.Windows.Forms.CheckedListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtMatcher = new System.Windows.Forms.TextBox();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnOpenFileXML = new System.Windows.Forms.Button();
            this.txtXMLFile = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePickerDato = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxSkov = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.comboBoxType = new System.Windows.Forms.ComboBox();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.comboBoxKreds);
            this.groupBox3.Controls.Add(this.cmbDivision);
            this.groupBox3.Location = new System.Drawing.Point(495, 94);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(434, 57);
            this.groupBox3.TabIndex = 34;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Division og Kreds";
            // 
            // comboBoxKreds
            // 
            this.comboBoxKreds.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxKreds.Enabled = false;
            this.comboBoxKreds.FormattingEnabled = true;
            this.comboBoxKreds.Location = new System.Drawing.Point(220, 19);
            this.comboBoxKreds.Name = "comboBoxKreds";
            this.comboBoxKreds.Size = new System.Drawing.Size(208, 21);
            this.comboBoxKreds.TabIndex = 6;
            this.comboBoxKreds.SelectedIndexChanged += new System.EventHandler(this.comboBoxKreds_SelectedIndexChanged);
            // 
            // cmbDivision
            // 
            this.cmbDivision.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDivision.Enabled = false;
            this.cmbDivision.FormattingEnabled = true;
            this.cmbDivision.Location = new System.Drawing.Point(14, 20);
            this.cmbDivision.Name = "cmbDivision";
            this.cmbDivision.Size = new System.Drawing.Size(197, 21);
            this.cmbDivision.TabIndex = 0;
            this.cmbDivision.SelectedIndexChanged += new System.EventHandler(this.cmbDivision_SelectedIndexChanged);
            // 
            // btnOpenKlasser
            // 
            this.btnOpenKlasser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenKlasser.Location = new System.Drawing.Point(692, 61);
            this.btnOpenKlasser.Name = "btnOpenKlasser";
            this.btnOpenKlasser.Size = new System.Drawing.Size(37, 23);
            this.btnOpenKlasser.TabIndex = 33;
            this.btnOpenKlasser.Text = "...";
            this.btnOpenKlasser.UseVisualStyleBackColor = true;
            this.btnOpenKlasser.Click += new System.EventHandler(this.btnOpenKlasser_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Enabled = false;
            this.dataGridView1.Location = new System.Drawing.Point(6, 21);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(433, 377);
            this.dataGridView1.TabIndex = 4;
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            // 
            // txtTXTFileKlasser
            // 
            this.txtTXTFileKlasser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTXTFileKlasser.Enabled = false;
            this.txtTXTFileKlasser.Location = new System.Drawing.Point(239, 63);
            this.txtTXTFileKlasser.Name = "txtTXTFileKlasser";
            this.txtTXTFileKlasser.Size = new System.Drawing.Size(447, 20);
            this.txtTXTFileKlasser.TabIndex = 32;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(35, 66);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(103, 13);
            this.label6.TabIndex = 31;
            this.label6.Text = "Klasser (OE2003 txt)";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.dataGridView1);
            this.groupBox2.Location = new System.Drawing.Point(29, 94);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(445, 403);
            this.groupBox2.TabIndex = 30;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Grupper/Klasser";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.splitContainer1);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtMatcher);
            this.groupBox1.Location = new System.Drawing.Point(495, 157);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(434, 340);
            this.groupBox1.TabIndex = 29;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Klubber";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(6, 20);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lstAllClubs);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.checkedListClubs);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Size = new System.Drawing.Size(422, 152);
            this.splitContainer1.SplitterDistance = 206;
            this.splitContainer1.TabIndex = 6;
            // 
            // lstAllClubs
            // 
            this.lstAllClubs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstAllClubs.Enabled = false;
            this.lstAllClubs.FormattingEnabled = true;
            this.lstAllClubs.Location = new System.Drawing.Point(1, 17);
            this.lstAllClubs.Name = "lstAllClubs";
            this.lstAllClubs.Size = new System.Drawing.Size(204, 121);
            this.lstAllClubs.Sorted = true;
            this.lstAllClubs.TabIndex = 1;
            this.lstAllClubs.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstAllClubs_MouseDoubleClick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(0, 1);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Alle klubber";
            // 
            // checkedListClubs
            // 
            this.checkedListClubs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkedListClubs.FormattingEnabled = true;
            this.checkedListClubs.Location = new System.Drawing.Point(1, 17);
            this.checkedListClubs.Name = "checkedListClubs";
            this.checkedListClubs.Size = new System.Drawing.Size(208, 124);
            this.checkedListClubs.TabIndex = 3;
            this.checkedListClubs.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListClubs_ItemCheck);
            this.checkedListClubs.SelectedIndexChanged += new System.EventHandler(this.checkedListClubs_SelectedIndexChanged);
            this.checkedListClubs.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.checkedListClubs_MouseDoubleClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(204, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Valgte klubber (udeblevet -> fjern flueben)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 180);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Matcher";
            // 
            // txtMatcher
            // 
            this.txtMatcher.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMatcher.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMatcher.Location = new System.Drawing.Point(9, 196);
            this.txtMatcher.Multiline = true;
            this.txtMatcher.Name = "txtMatcher";
            this.txtMatcher.ReadOnly = true;
            this.txtMatcher.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtMatcher.Size = new System.Drawing.Size(419, 139);
            this.txtMatcher.TabIndex = 4;
            this.txtMatcher.WordWrap = false;
            // 
            // btnLoad
            // 
            this.btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoad.Location = new System.Drawing.Point(750, 60);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(138, 23);
            this.btnLoad.TabIndex = 28;
            this.btnLoad.Text = "&Load klubber og klasser";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnOpenFileXML
            // 
            this.btnOpenFileXML.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenFileXML.Location = new System.Drawing.Point(692, 32);
            this.btnOpenFileXML.Name = "btnOpenFileXML";
            this.btnOpenFileXML.Size = new System.Drawing.Size(37, 23);
            this.btnOpenFileXML.TabIndex = 27;
            this.btnOpenFileXML.Text = "...";
            this.btnOpenFileXML.UseVisualStyleBackColor = true;
            this.btnOpenFileXML.Click += new System.EventHandler(this.btnOpenFileXML_Click);
            // 
            // txtXMLFile
            // 
            this.txtXMLFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtXMLFile.Enabled = false;
            this.txtXMLFile.Location = new System.Drawing.Point(239, 34);
            this.txtXMLFile.Name = "txtXMLFile";
            this.txtXMLFile.Size = new System.Drawing.Size(447, 20);
            this.txtXMLFile.TabIndex = 26;
            this.txtXMLFile.TextChanged += new System.EventHandler(this.txtXMLFile_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(198, 13);
            this.label2.TabIndex = 25;
            this.label2.Text = "Tilmelding/Start/Resultaliste (XML, CSV)";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOK.Enabled = false;
            this.btnOK.Location = new System.Drawing.Point(393, 515);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 35;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(495, 515);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 36;
            this.btnCancel.Text = "&Annuller";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.ForeColor = System.Drawing.Color.Red;
            this.textBox1.Location = new System.Drawing.Point(35, 503);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(352, 35);
            this.textBox1.TabIndex = 37;
            this.textBox1.Text = "hello";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 38;
            this.label1.Text = "Stævnedato";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // dateTimePickerDato
            // 
            this.dateTimePickerDato.Location = new System.Drawing.Point(184, 6);
            this.dateTimePickerDato.Name = "dateTimePickerDato";
            this.dateTimePickerDato.Size = new System.Drawing.Size(147, 20);
            this.dateTimePickerDato.TabIndex = 39;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(353, 6);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(32, 13);
            this.label7.TabIndex = 40;
            this.label7.Text = "Skov";
            // 
            // textBoxSkov
            // 
            this.textBoxSkov.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSkov.Location = new System.Drawing.Point(391, 6);
            this.textBoxSkov.Name = "textBoxSkov";
            this.textBoxSkov.Size = new System.Drawing.Size(177, 20);
            this.textBoxSkov.TabIndex = 41;
            this.textBoxSkov.Leave += new System.EventHandler(this.textBoxSkov_Leave);
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(586, 6);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(31, 13);
            this.label8.TabIndex = 42;
            this.label8.Text = "Type";
            // 
            // comboBoxType
            // 
            this.comboBoxType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxType.FormattingEnabled = true;
            this.comboBoxType.Items.AddRange(new object[] {
            "Divisionsmatch",
            "Op/ned",
            "Divisionsmatchfinale"});
            this.comboBoxType.Location = new System.Drawing.Point(636, 6);
            this.comboBoxType.Name = "comboBoxType";
            this.comboBoxType.Size = new System.Drawing.Size(196, 21);
            this.comboBoxType.TabIndex = 43;
            this.comboBoxType.SelectedIndexChanged += new System.EventHandler(this.comboBoxType_SelectedIndexChanged);
            // 
            // frmConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(942, 552);
            this.Controls.Add(this.comboBoxType);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textBoxSkov);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.dateTimePickerDato);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnOpenKlasser);
            this.Controls.Add(this.txtTXTFileKlasser);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.btnOpenFileXML);
            this.Controls.Add(this.txtXMLFile);
            this.Controls.Add(this.label2);
            this.KeyPreview = true;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(958, 559);
            this.Name = "frmConfig";
            this.Text = "Opsætning";
            this.Load += new System.EventHandler(this.frmConfig_Load);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cmbDivision;
        private System.Windows.Forms.Button btnOpenKlasser;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txtTXTFileKlasser;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtMatcher;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnOpenFileXML;
        private System.Windows.Forms.TextBox txtXMLFile;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListBox lstAllClubs;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.CheckedListBox checkedListClubs;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePickerDato;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxSkov;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comboBoxType;
        private System.Windows.Forms.ComboBox comboBoxKreds;
    }
}