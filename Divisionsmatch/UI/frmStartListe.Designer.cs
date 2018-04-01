namespace Divisionsmatch
{
    partial class frmStartListe
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
            this.radioGruppe = new System.Windows.Forms.RadioButton();
            this.radioBane = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.txtXMLFile = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.radioHtml = new System.Windows.Forms.RadioButton();
            this.radioTxt = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.radioAlle = new System.Windows.Forms.RadioButton();
            this.radioDivi = new System.Windows.Forms.RadioButton();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnLavListe = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // radioGruppe
            // 
            this.radioGruppe.AutoSize = true;
            this.radioGruppe.Checked = true;
            this.radioGruppe.Location = new System.Drawing.Point(0, 3);
            this.radioGruppe.Name = "radioGruppe";
            this.radioGruppe.Size = new System.Drawing.Size(60, 17);
            this.radioGruppe.TabIndex = 1;
            this.radioGruppe.TabStop = true;
            this.radioGruppe.Text = "Gruppe";
            this.radioGruppe.UseVisualStyleBackColor = true;
            // 
            // radioBane
            // 
            this.radioBane.AutoSize = true;
            this.radioBane.Location = new System.Drawing.Point(102, 2);
            this.radioBane.Name = "radioBane";
            this.radioBane.Size = new System.Drawing.Size(50, 17);
            this.radioBane.TabIndex = 2;
            this.radioBane.Text = "Bane";
            this.radioBane.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Startliste data (XML, csv)";
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenFile.Location = new System.Drawing.Point(370, 4);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(37, 23);
            this.btnOpenFile.TabIndex = 29;
            this.btnOpenFile.Text = "...";
            this.btnOpenFile.UseVisualStyleBackColor = true;
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // txtXMLFile
            // 
            this.txtXMLFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtXMLFile.Enabled = false;
            this.txtXMLFile.Location = new System.Drawing.Point(140, 6);
            this.txtXMLFile.Name = "txtXMLFile";
            this.txtXMLFile.Size = new System.Drawing.Size(224, 20);
            this.txtXMLFile.TabIndex = 28;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 30;
            this.label2.Text = "Type af liste";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 33;
            this.label3.Text = "Format";
            // 
            // radioHtml
            // 
            this.radioHtml.AutoSize = true;
            this.radioHtml.Location = new System.Drawing.Point(102, 2);
            this.radioHtml.Name = "radioHtml";
            this.radioHtml.Size = new System.Drawing.Size(46, 17);
            this.radioHtml.TabIndex = 32;
            this.radioHtml.Text = "Html";
            this.radioHtml.UseVisualStyleBackColor = true;
            // 
            // radioTxt
            // 
            this.radioTxt.AutoSize = true;
            this.radioTxt.Checked = true;
            this.radioTxt.Location = new System.Drawing.Point(0, 3);
            this.radioTxt.Name = "radioTxt";
            this.radioTxt.Size = new System.Drawing.Size(52, 17);
            this.radioTxt.TabIndex = 31;
            this.radioTxt.TabStop = true;
            this.radioTxt.Text = "Tekst";
            this.radioTxt.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 115);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 36;
            this.label4.Text = "Løbere";
            // 
            // radioAlle
            // 
            this.radioAlle.AutoSize = true;
            this.radioAlle.Location = new System.Drawing.Point(102, 0);
            this.radioAlle.Name = "radioAlle";
            this.radioAlle.Size = new System.Drawing.Size(137, 17);
            this.radioAlle.TabIndex = 35;
            this.radioAlle.Text = "Alle i løbet unanset klub";
            this.radioAlle.UseVisualStyleBackColor = true;
            // 
            // radioDivi
            // 
            this.radioDivi.AutoSize = true;
            this.radioDivi.Checked = true;
            this.radioDivi.Location = new System.Drawing.Point(0, 0);
            this.radioDivi.Name = "radioDivi";
            this.radioDivi.Size = new System.Drawing.Size(96, 17);
            this.radioDivi.TabIndex = 34;
            this.radioDivi.TabStop = true;
            this.radioDivi.Text = "Divisionsmatch";
            this.radioDivi.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(331, 174);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 38;
            this.btnCancel.Text = "&Annuller";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnLavListe
            // 
            this.btnLavListe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLavListe.Enabled = false;
            this.btnLavListe.Location = new System.Drawing.Point(250, 174);
            this.btnLavListe.Name = "btnLavListe";
            this.btnLavListe.Size = new System.Drawing.Size(75, 23);
            this.btnLavListe.TabIndex = 37;
            this.btnLavListe.Text = "&Lav liste";
            this.btnLavListe.UseVisualStyleBackColor = true;
            this.btnLavListe.Click += new System.EventHandler(this.btnLavListe_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radioAlle);
            this.panel1.Controls.Add(this.radioDivi);
            this.panel1.Location = new System.Drawing.Point(140, 111);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(267, 22);
            this.panel1.TabIndex = 39;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.radioHtml);
            this.panel2.Controls.Add(this.radioTxt);
            this.panel2.Location = new System.Drawing.Point(140, 83);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(268, 22);
            this.panel2.TabIndex = 40;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.radioBane);
            this.panel3.Controls.Add(this.radioGruppe);
            this.panel3.Location = new System.Drawing.Point(140, 55);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(267, 22);
            this.panel3.TabIndex = 41;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 13);
            this.label5.TabIndex = 42;
            this.label5.Text = "Løbets nul-tid";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "HH:mm:ss";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePicker1.Location = new System.Drawing.Point(140, 29);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.ShowUpDown = true;
            this.dateTimePicker1.Size = new System.Drawing.Size(84, 20);
            this.dateTimePicker1.TabIndex = 43;
            // 
            // frmStartListe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(418, 220);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnLavListe);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnOpenFile);
            this.Controls.Add(this.txtXMLFile);
            this.Controls.Add(this.label1);
            this.MinimumSize = new System.Drawing.Size(434, 222);
            this.Name = "frmStartListe";
            this.Text = "Startliste";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton radioBane;
        private System.Windows.Forms.RadioButton radioGruppe;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOpenFile;
        private System.Windows.Forms.TextBox txtXMLFile;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton radioHtml;
        private System.Windows.Forms.RadioButton radioTxt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton radioAlle;
        private System.Windows.Forms.RadioButton radioDivi;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnLavListe;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
    }
}