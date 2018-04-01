namespace Divisionsmatch
{
    partial class frmNytDivisionsResultat
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dateTimePickerÅr = new System.Windows.Forms.DateTimePicker();
            this.comboBoxDivision = new System.Windows.Forms.ComboBox();
            this.comboBoxKreds = new System.Windows.Forms.ComboBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "År";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(37, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Division";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(37, 119);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Kreds";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.dateTimePickerÅr);
            this.groupBox2.Controls.Add(this.comboBoxDivision);
            this.groupBox2.Controls.Add(this.comboBoxKreds);
            this.groupBox2.Location = new System.Drawing.Point(12, 47);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(291, 112);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Detaljer";
            // 
            // dateTimePickerÅr
            // 
            this.dateTimePickerÅr.CustomFormat = "yyyy";
            this.dateTimePickerÅr.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerÅr.Location = new System.Drawing.Point(77, 20);
            this.dateTimePickerÅr.Name = "dateTimePickerÅr";
            this.dateTimePickerÅr.Size = new System.Drawing.Size(83, 20);
            this.dateTimePickerÅr.TabIndex = 3;
            // 
            // comboBoxDivision
            // 
            this.comboBoxDivision.FormattingEnabled = true;
            this.comboBoxDivision.Location = new System.Drawing.Point(77, 43);
            this.comboBoxDivision.Name = "comboBoxDivision";
            this.comboBoxDivision.Size = new System.Drawing.Size(208, 21);
            this.comboBoxDivision.TabIndex = 2;
            // 
            // comboBoxKreds
            // 
            this.comboBoxKreds.FormattingEnabled = true;
            this.comboBoxKreds.Location = new System.Drawing.Point(77, 69);
            this.comboBoxKreds.Name = "comboBoxKreds";
            this.comboBoxKreds.Size = new System.Drawing.Size(208, 21);
            this.comboBoxKreds.TabIndex = 1;
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(223, 165);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 31;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(142, 165);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 32;
            this.buttonCancel.Text = "Annuller";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(201, 26);
            this.label4.TabIndex = 33;
            this.label4.Text = "Start et nyt divisionsresultat, hvor \r\ndet indeværende løb bliver første stævne\r\n" +
    "";
            // 
            // frmNytDivisionsResultat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(310, 196);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmNytDivisionsResultat";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Nyt Divisionsresultat";
            this.Load += new System.EventHandler(this.frmDivisionsResultat_Load);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.DateTimePicker dateTimePickerÅr;
        private System.Windows.Forms.ComboBox comboBoxDivision;
        private System.Windows.Forms.ComboBox comboBoxKreds;
        private System.Windows.Forms.Label label4;
    }
}