namespace Divisionsmatch
{
    partial class frmSetup
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
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.btnPage = new System.Windows.Forms.Button();
            this.btnFont = new System.Windows.Forms.Button();
            this.txtFont = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.checkBox5 = new System.Windows.Forms.CheckBox();
            this.lblPrinter = new System.Windows.Forms.Label();
            this.checkBox6 = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.chkUseCss = new System.Windows.Forms.CheckBox();
            this.txtHtmlCss = new System.Windows.Forms.TextBox();
            this.btnCssFile = new System.Windows.Forms.Button();
            this.chkAutoExport = new System.Windows.Forms.CheckBox();
            this.checkBox7 = new System.Windows.Forms.CheckBox();
            this.checkBox8 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Printer";
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(251, 396);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(332, 396);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Annuller";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 13);
            this.label2.TabIndex = 4;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBox1.Location = new System.Drawing.Point(15, 238);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(149, 17);
            this.checkBox1.TabIndex = 5;
            this.checkBox1.Text = "Sideskift per klasse/bane";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // btnPage
            // 
            this.btnPage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPage.Location = new System.Drawing.Point(15, 359);
            this.btnPage.Name = "btnPage";
            this.btnPage.Size = new System.Drawing.Size(395, 23);
            this.btnPage.TabIndex = 9;
            this.btnPage.Text = "Sideopsætning";
            this.btnPage.UseVisualStyleBackColor = true;
            this.btnPage.Click += new System.EventHandler(this.bntPage_Click);
            // 
            // btnFont
            // 
            this.btnFont.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFont.Location = new System.Drawing.Point(332, 161);
            this.btnFont.Name = "btnFont";
            this.btnFont.Size = new System.Drawing.Size(75, 23);
            this.btnFont.TabIndex = 8;
            this.btnFont.Text = "Skift font";
            this.btnFont.UseVisualStyleBackColor = true;
            this.btnFont.Click += new System.EventHandler(this.btnFont_Click);
            // 
            // txtFont
            // 
            this.txtFont.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFont.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFont.Location = new System.Drawing.Point(116, 55);
            this.txtFont.Multiline = true;
            this.txtFont.Name = "txtFont";
            this.txtFont.ReadOnly = true;
            this.txtFont.Size = new System.Drawing.Size(291, 100);
            this.txtFont.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Font";
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBox2.Location = new System.Drawing.Point(204, 238);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(86, 17);
            this.checkBox2.TabIndex = 11;
            this.checkBox2.Text = "Print pr bane";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBox3.Location = new System.Drawing.Point(192, 261);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(98, 17);
            this.checkBox3.TabIndex = 12;
            this.checkBox3.Text = "Print alle løbere";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBox4.Location = new System.Drawing.Point(46, 261);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(118, 17);
            this.checkBox4.TabIndex = 13;
            this.checkBox4.Text = "Print ændrede sider";
            this.checkBox4.UseVisualStyleBackColor = true;
            // 
            // checkBox5
            // 
            this.checkBox5.AutoSize = true;
            this.checkBox5.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBox5.Location = new System.Drawing.Point(332, 238);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new System.Drawing.Size(72, 17);
            this.checkBox5.TabIndex = 14;
            this.checkBox5.Text = "Auto Print";
            this.checkBox5.UseVisualStyleBackColor = true;
            // 
            // lblPrinter
            // 
            this.lblPrinter.AutoSize = true;
            this.lblPrinter.Location = new System.Drawing.Point(116, 21);
            this.lblPrinter.Name = "lblPrinter";
            this.lblPrinter.Size = new System.Drawing.Size(0, 13);
            this.lblPrinter.TabIndex = 15;
            // 
            // checkBox6
            // 
            this.checkBox6.AutoSize = true;
            this.checkBox6.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBox6.Location = new System.Drawing.Point(44, 284);
            this.checkBox6.Name = "checkBox6";
            this.checkBox6.Size = new System.Drawing.Size(120, 17);
            this.checkBox6.TabIndex = 16;
            this.checkBox6.Text = "Print tomme klasser";
            this.checkBox6.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 192);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Html CSS";
            // 
            // chkUseCss
            // 
            this.chkUseCss.AutoSize = true;
            this.chkUseCss.Location = new System.Drawing.Point(92, 191);
            this.chkUseCss.Name = "chkUseCss";
            this.chkUseCss.Size = new System.Drawing.Size(15, 14);
            this.chkUseCss.TabIndex = 18;
            this.chkUseCss.UseVisualStyleBackColor = true;
            this.chkUseCss.CheckedChanged += new System.EventHandler(this.chkUseCss_CheckedChanged);
            // 
            // txtHtmlCss
            // 
            this.txtHtmlCss.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHtmlCss.Enabled = false;
            this.txtHtmlCss.Location = new System.Drawing.Point(116, 192);
            this.txtHtmlCss.Name = "txtHtmlCss";
            this.txtHtmlCss.Size = new System.Drawing.Size(254, 20);
            this.txtHtmlCss.TabIndex = 19;
            // 
            // btnCssFile
            // 
            this.btnCssFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCssFile.Location = new System.Drawing.Point(376, 192);
            this.btnCssFile.Name = "btnCssFile";
            this.btnCssFile.Size = new System.Drawing.Size(31, 23);
            this.btnCssFile.TabIndex = 20;
            this.btnCssFile.Text = "...";
            this.btnCssFile.UseVisualStyleBackColor = true;
            this.btnCssFile.Click += new System.EventHandler(this.btnCssFile_Click);
            // 
            // chkAutoExport
            // 
            this.chkAutoExport.AutoSize = true;
            this.chkAutoExport.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkAutoExport.Location = new System.Drawing.Point(323, 261);
            this.chkAutoExport.Name = "chkAutoExport";
            this.chkAutoExport.Size = new System.Drawing.Size(81, 17);
            this.chkAutoExport.TabIndex = 21;
            this.chkAutoExport.Text = "Auto Export";
            this.chkAutoExport.UseVisualStyleBackColor = true;
            // 
            // checkBox7
            // 
            this.checkBox7.AutoSize = true;
            this.checkBox7.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBox7.Location = new System.Drawing.Point(40, 316);
            this.checkBox7.Name = "checkBox7";
            this.checkBox7.Size = new System.Drawing.Size(124, 17);
            this.checkBox7.TabIndex = 22;
            this.checkBox7.Text = "Print divisionsresultat";
            this.checkBox7.UseVisualStyleBackColor = true;
            this.checkBox7.CheckedChanged += new System.EventHandler(this.checkBox7_CheckedChanged);
            // 
            // checkBox8
            // 
            this.checkBox8.AutoSize = true;
            this.checkBox8.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBox8.Location = new System.Drawing.Point(176, 316);
            this.checkBox8.Name = "checkBox8";
            this.checkBox8.Size = new System.Drawing.Size(114, 17);
            this.checkBox8.TabIndex = 23;
            this.checkBox8.Text = "inkl tidligere runder";
            this.checkBox8.UseVisualStyleBackColor = true;
            // 
            // frmSetup
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(419, 431);
            this.Controls.Add(this.checkBox8);
            this.Controls.Add(this.checkBox7);
            this.Controls.Add(this.chkAutoExport);
            this.Controls.Add(this.btnCssFile);
            this.Controls.Add(this.txtHtmlCss);
            this.Controls.Add(this.chkUseCss);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.checkBox6);
            this.Controls.Add(this.lblPrinter);
            this.Controls.Add(this.checkBox5);
            this.Controls.Add(this.checkBox4);
            this.Controls.Add(this.checkBox3);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtFont);
            this.Controls.Add(this.btnFont);
            this.Controls.Add(this.btnPage);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(435, 420);
            this.Name = "frmSetup";
            this.Text = "Print og side setup";
            this.Load += new System.EventHandler(this.Setup_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button btnPage;
        private System.Windows.Forms.Button btnFont;
        private System.Windows.Forms.TextBox txtFont;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.CheckBox checkBox5;
        private System.Windows.Forms.Label lblPrinter;
        private System.Windows.Forms.CheckBox checkBox6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkUseCss;
        private System.Windows.Forms.TextBox txtHtmlCss;
        private System.Windows.Forms.Button btnCssFile;
        private System.Windows.Forms.CheckBox chkAutoExport;
        private System.Windows.Forms.CheckBox checkBox7;
        private System.Windows.Forms.CheckBox checkBox8;
    }
}