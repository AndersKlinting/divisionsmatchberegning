namespace Divisionsmatch
{
    partial class frmNancy
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
            this.txtPort = new System.Windows.Forms.TextBox();
            this.btnStartStop = new System.Windows.Forms.Button();
            this.lnkLink1 = new System.Windows.Forms.LinkLabel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.lnkLink4 = new System.Windows.Forms.LinkLabel();
            this.lnkLink3 = new System.Windows.Forms.LinkLabel();
            this.lnkLink2 = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Port";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(45, 10);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(100, 20);
            this.txtPort.TabIndex = 1;
            this.txtPort.Text = "2019";
            this.txtPort.TextChanged += new System.EventHandler(this.txtPort_TextChanged);
            this.txtPort.Leave += new System.EventHandler(this.txtPort_Leave);
            // 
            // btnStartStop
            // 
            this.btnStartStop.Location = new System.Drawing.Point(164, 8);
            this.btnStartStop.Name = "btnStartStop";
            this.btnStartStop.Size = new System.Drawing.Size(75, 23);
            this.btnStartStop.TabIndex = 2;
            this.btnStartStop.Text = "Start Server";
            this.btnStartStop.UseVisualStyleBackColor = true;
            this.btnStartStop.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // lnkLink1
            // 
            this.lnkLink1.AutoSize = true;
            this.lnkLink1.LinkVisited = true;
            this.lnkLink1.Location = new System.Drawing.Point(13, 46);
            this.lnkLink1.Name = "lnkLink1";
            this.lnkLink1.Size = new System.Drawing.Size(0, 13);
            this.lnkLink1.TabIndex = 3;
            this.lnkLink1.Visible = false;
            this.lnkLink1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkLink_LinkClicked);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(12, 138);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(440, 85);
            this.textBox1.TabIndex = 4;
            // 
            // lnkLink4
            // 
            this.lnkLink4.AutoSize = true;
            this.lnkLink4.LinkVisited = true;
            this.lnkLink4.Location = new System.Drawing.Point(13, 112);
            this.lnkLink4.Name = "lnkLink4";
            this.lnkLink4.Size = new System.Drawing.Size(0, 13);
            this.lnkLink4.TabIndex = 5;
            this.lnkLink4.Visible = false;
            this.lnkLink4.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkLink_LinkClicked);
            // 
            // lnkLink3
            // 
            this.lnkLink3.AutoSize = true;
            this.lnkLink3.LinkVisited = true;
            this.lnkLink3.Location = new System.Drawing.Point(13, 90);
            this.lnkLink3.Name = "lnkLink3";
            this.lnkLink3.Size = new System.Drawing.Size(0, 13);
            this.lnkLink3.TabIndex = 6;
            this.lnkLink3.Visible = false;
            this.lnkLink3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkLink_LinkClicked);
            // 
            // lnkLink2
            // 
            this.lnkLink2.AutoSize = true;
            this.lnkLink2.LinkVisited = true;
            this.lnkLink2.Location = new System.Drawing.Point(13, 68);
            this.lnkLink2.Name = "lnkLink2";
            this.lnkLink2.Size = new System.Drawing.Size(0, 13);
            this.lnkLink2.TabIndex = 7;
            this.lnkLink2.Visible = false;
            this.lnkLink2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkLink_LinkClicked);
            // 
            // frmNancy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 235);
            this.Controls.Add(this.lnkLink2);
            this.Controls.Add(this.lnkLink3);
            this.Controls.Add(this.lnkLink4);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.lnkLink1);
            this.Controls.Add(this.btnStartStop);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(480, 274);
            this.Name = "frmNancy";
            this.Text = "Nancy REST API Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmNancy_FormClosing);
            this.Load += new System.EventHandler(this.frmNancy_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Button btnStartStop;
        private System.Windows.Forms.LinkLabel lnkLink1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.LinkLabel lnkLink4;
        private System.Windows.Forms.LinkLabel lnkLink3;
        private System.Windows.Forms.LinkLabel lnkLink2;
    }
}