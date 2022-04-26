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
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxRunde = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxSkov = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dateTimePickerDato = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxType = new System.Windows.Forms.TextBox();
            this.textBoxBeskriv = new System.Windows.Forms.TextBox();
            this.textBoxKreds = new System.Windows.Forms.TextBox();
            this.textBoxDivision = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnLoad = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkedListClubs = new System.Windows.Forms.CheckedListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtMatcher = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBoxKredsId = new System.Windows.Forms.TextBox();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.textBoxKredsId);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.textBoxRunde);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.textBoxSkov);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.dateTimePickerDato);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.textBoxType);
            this.groupBox3.Controls.Add(this.textBoxBeskriv);
            this.groupBox3.Controls.Add(this.textBoxKreds);
            this.groupBox3.Controls.Add(this.textBoxDivision);
            this.groupBox3.Location = new System.Drawing.Point(30, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(900, 99);
            this.groupBox3.TabIndex = 34;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Stævnedetaljer";
            this.groupBox3.Enter += new System.EventHandler(this.groupBox3_Enter);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(551, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 51;
            this.label2.Text = "Runde";
            // 
            // textBoxRunde
            // 
            this.textBoxRunde.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxRunde.Enabled = false;
            this.textBoxRunde.Location = new System.Drawing.Point(591, 44);
            this.textBoxRunde.Name = "textBoxRunde";
            this.textBoxRunde.Size = new System.Drawing.Size(50, 20);
            this.textBoxRunde.TabIndex = 50;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(10, 74);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(61, 13);
            this.label9.TabIndex = 49;
            this.label9.Text = "Beskrivelse";
            this.label9.Click += new System.EventHandler(this.label9_Click);
            // 
            // textBoxSkov
            // 
            this.textBoxSkov.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSkov.Enabled = false;
            this.textBoxSkov.Location = new System.Drawing.Point(320, 44);
            this.textBoxSkov.Name = "textBoxSkov";
            this.textBoxSkov.Size = new System.Drawing.Size(225, 20);
            this.textBoxSkov.TabIndex = 41;
            this.textBoxSkov.TextChanged += new System.EventHandler(this.textBoxSkov_TextChanged);
            this.textBoxSkov.Leave += new System.EventHandler(this.textBoxSkov_Leave);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(270, 47);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(32, 13);
            this.label7.TabIndex = 40;
            this.label7.Text = "Skov";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(551, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 13);
            this.label6.TabIndex = 48;
            this.label6.Text = "Kreds";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(270, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 47;
            this.label4.Text = "Division";
            this.label4.Click += new System.EventHandler(this.label4_Click_1);
            // 
            // dateTimePickerDato
            // 
            this.dateTimePickerDato.Enabled = false;
            this.dateTimePickerDato.Location = new System.Drawing.Point(82, 45);
            this.dateTimePickerDato.Name = "dateTimePickerDato";
            this.dateTimePickerDato.Size = new System.Drawing.Size(147, 20);
            this.dateTimePickerDato.TabIndex = 39;
            this.dateTimePickerDato.ValueChanged += new System.EventHandler(this.dateTimePickerDato_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 38;
            this.label1.Text = "Stævnedato";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 19);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(31, 13);
            this.label8.TabIndex = 42;
            this.label8.Text = "Type";
            // 
            // textBoxType
            // 
            this.textBoxType.Enabled = false;
            this.textBoxType.Location = new System.Drawing.Point(82, 19);
            this.textBoxType.Name = "textBoxType";
            this.textBoxType.Size = new System.Drawing.Size(181, 20);
            this.textBoxType.TabIndex = 43;
            this.textBoxType.TextChanged += new System.EventHandler(this.textBox2_TextChanged_1);
            // 
            // textBoxBeskriv
            // 
            this.textBoxBeskriv.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxBeskriv.Enabled = false;
            this.textBoxBeskriv.Location = new System.Drawing.Point(82, 71);
            this.textBoxBeskriv.Name = "textBoxBeskriv";
            this.textBoxBeskriv.Size = new System.Drawing.Size(752, 20);
            this.textBoxBeskriv.TabIndex = 46;
            this.textBoxBeskriv.TextChanged += new System.EventHandler(this.textBoxBeskriv_TextChanged);
            // 
            // textBoxKreds
            // 
            this.textBoxKreds.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxKreds.Enabled = false;
            this.textBoxKreds.Location = new System.Drawing.Point(591, 19);
            this.textBoxKreds.Name = "textBoxKreds";
            this.textBoxKreds.Size = new System.Drawing.Size(243, 20);
            this.textBoxKreds.TabIndex = 45;
            this.textBoxKreds.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // textBoxDivision
            // 
            this.textBoxDivision.Enabled = false;
            this.textBoxDivision.Location = new System.Drawing.Point(320, 19);
            this.textBoxDivision.Name = "textBoxDivision";
            this.textBoxDivision.Size = new System.Drawing.Size(225, 20);
            this.textBoxDivision.TabIndex = 44;
            this.textBoxDivision.TextChanged += new System.EventHandler(this.textBoxDivision_TextChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Enabled = false;
            this.dataGridView1.Location = new System.Drawing.Point(5, 19);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(433, 372);
            this.dataGridView1.TabIndex = 4;
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.dataGridView1);
            this.groupBox2.Controls.Add(this.btnLoad);
            this.groupBox2.Location = new System.Drawing.Point(30, 118);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(445, 443);
            this.groupBox2.TabIndex = 30;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Knyt reglementsklasser og løbsklasser";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // btnLoad
            // 
            this.btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoad.Location = new System.Drawing.Point(36, 397);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(376, 23);
            this.btnLoad.TabIndex = 28;
            this.btnLoad.Text = "&Load løbsklasser fra Tilmelding/Start/Resultaliste/Klasser (XML, CSV), TXT";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.checkedListClubs);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtMatcher);
            this.groupBox1.Location = new System.Drawing.Point(496, 118);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(434, 443);
            this.groupBox1.TabIndex = 29;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Klubber";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // checkedListClubs
            // 
            this.checkedListClubs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkedListClubs.FormattingEnabled = true;
            this.checkedListClubs.Location = new System.Drawing.Point(9, 19);
            this.checkedListClubs.Name = "checkedListClubs";
            this.checkedListClubs.Size = new System.Drawing.Size(208, 139);
            this.checkedListClubs.TabIndex = 3;
            this.checkedListClubs.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListClubs_ItemCheck);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 157);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(133, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "(udeblevet -> fjern flueben)";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 193);
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
            this.txtMatcher.Location = new System.Drawing.Point(9, 209);
            this.txtMatcher.Multiline = true;
            this.txtMatcher.Name = "txtMatcher";
            this.txtMatcher.ReadOnly = true;
            this.txtMatcher.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtMatcher.Size = new System.Drawing.Size(419, 228);
            this.txtMatcher.TabIndex = 4;
            this.txtMatcher.WordWrap = false;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOK.Enabled = false;
            this.btnOK.Location = new System.Drawing.Point(393, 602);
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
            this.btnCancel.Location = new System.Drawing.Point(495, 602);
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
            this.textBox1.Location = new System.Drawing.Point(35, 567);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(352, 58);
            this.textBox1.TabIndex = 37;
            this.textBox1.Text = "hello";
            // 
            // textBoxKredsId
            // 
            this.textBoxKredsId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxKredsId.Enabled = false;
            this.textBoxKredsId.Location = new System.Drawing.Point(840, 19);
            this.textBoxKredsId.Name = "textBoxKredsId";
            this.textBoxKredsId.Size = new System.Drawing.Size(37, 20);
            this.textBoxKredsId.TabIndex = 52;
            // 
            // frmConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(942, 639);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.KeyPreview = true;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(958, 559);
            this.Name = "frmConfig";
            this.Text = "Opsætning";
            this.Load += new System.EventHandler(this.frmConfig_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtMatcher;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.CheckedListBox checkedListClubs;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePickerDato;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxSkov;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxKreds;
        private System.Windows.Forms.TextBox textBoxDivision;
        private System.Windows.Forms.TextBox textBoxType;
        private System.Windows.Forms.TextBox textBoxBeskriv;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxRunde;
        private System.Windows.Forms.TextBox textBoxKredsId;
    }
}