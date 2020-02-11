namespace Divisionsmatch
{
    partial class frmDivi
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDivi));
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnEksport = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.webOutput = new System.Windows.Forms.WebBrowser();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnBeregn = new System.Windows.Forms.Button();
            this.bntOpenResultFile = new System.Windows.Forms.Button();
            this.txtCSVFile = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.nytLøbToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nytLøbToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.åbnLøbToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redigerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gemLøbToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gemsomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.gemDivisionsresultatToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.startlisteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.lukToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printMainToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setupPrinterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.printPreviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.informationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.informationServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hjælpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.indholdToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.skiftFarveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rødToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grønToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gulToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.blåToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.standardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.checkForOpdateringerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.omToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pageSetupDialog1 = new System.Windows.Forms.PageSetupDialog();
            this.progressBarMeOS = new System.Windows.Forms.ProgressBar();
            this.recentsToolStripMenuItem = new RecentsToolStripMenuItem();
            this.panel2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.progressBarMeOS);
            this.panel2.Controls.Add(this.btnEksport);
            this.panel2.Controls.Add(this.tabControl1);
            this.panel2.Controls.Add(this.btnPrint);
            this.panel2.Controls.Add(this.btnBeregn);
            this.panel2.Controls.Add(this.bntOpenResultFile);
            this.panel2.Controls.Add(this.txtCSVFile);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Enabled = false;
            this.panel2.Location = new System.Drawing.Point(12, 27);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(976, 455);
            this.panel2.TabIndex = 0;
            // 
            // btnEksport
            // 
            this.btnEksport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEksport.Enabled = false;
            this.btnEksport.Location = new System.Drawing.Point(891, 10);
            this.btnEksport.Name = "btnEksport";
            this.btnEksport.Size = new System.Drawing.Size(75, 23);
            this.btnEksport.TabIndex = 15;
            this.btnEksport.Text = "&Eksport";
            this.btnEksport.UseVisualStyleBackColor = true;
            this.btnEksport.Click += new System.EventHandler(this.btnEksport_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(9, 42);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(964, 401);
            this.tabControl1.TabIndex = 14;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.splitContainer1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(956, 375);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Text";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BackColor = System.Drawing.Color.DarkRed;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.textBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.textBox3);
            this.splitContainer1.Size = new System.Drawing.Size(956, 375);
            this.splitContainer1.SplitterDistance = 184;
            this.splitContainer1.SplitterWidth = 2;
            this.splitContainer1.TabIndex = 12;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.textBox1.MaxLength = 327670;
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(956, 181);
            this.textBox1.TabIndex = 2;
            this.textBox1.WordWrap = false;
            // 
            // textBox3
            // 
            this.textBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox3.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox3.Location = new System.Drawing.Point(0, 1);
            this.textBox3.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.textBox3.MaxLength = 327670;
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox3.Size = new System.Drawing.Size(956, 218);
            this.textBox3.TabIndex = 3;
            this.textBox3.WordWrap = false;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.webOutput);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(956, 375);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Html";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // webOutput
            // 
            this.webOutput.AllowWebBrowserDrop = false;
            this.webOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.webOutput.IsWebBrowserContextMenuEnabled = false;
            this.webOutput.Location = new System.Drawing.Point(0, 6);
            this.webOutput.MinimumSize = new System.Drawing.Size(20, 20);
            this.webOutput.Name = "webOutput";
            this.webOutput.Size = new System.Drawing.Size(956, 363);
            this.webOutput.TabIndex = 14;
            this.webOutput.WebBrowserShortcutsEnabled = false;
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.Location = new System.Drawing.Point(810, 10);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.TabIndex = 13;
            this.btnPrint.Text = "&Print";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnBeregn
            // 
            this.btnBeregn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBeregn.Enabled = false;
            this.btnBeregn.Location = new System.Drawing.Point(729, 10);
            this.btnBeregn.Name = "btnBeregn";
            this.btnBeregn.Size = new System.Drawing.Size(75, 23);
            this.btnBeregn.TabIndex = 11;
            this.btnBeregn.Text = "&Beregn";
            this.btnBeregn.UseVisualStyleBackColor = true;
            this.btnBeregn.Click += new System.EventHandler(this.btnBeregn_Click);
            // 
            // bntOpenResultFile
            // 
            this.bntOpenResultFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bntOpenResultFile.Location = new System.Drawing.Point(686, 10);
            this.bntOpenResultFile.Name = "bntOpenResultFile";
            this.bntOpenResultFile.Size = new System.Drawing.Size(37, 23);
            this.bntOpenResultFile.TabIndex = 10;
            this.bntOpenResultFile.Text = "...";
            this.bntOpenResultFile.UseVisualStyleBackColor = true;
            this.bntOpenResultFile.Click += new System.EventHandler(this.bntOpenResultFile_Click);
            // 
            // txtCSVFile
            // 
            this.txtCSVFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCSVFile.Enabled = false;
            this.txtCSVFile.Location = new System.Drawing.Point(176, 13);
            this.txtCSVFile.Name = "txtCSVFile";
            this.txtCSVFile.Size = new System.Drawing.Size(504, 20);
            this.txtCSVFile.TabIndex = 9;
            this.txtCSVFile.TextChanged += new System.EventHandler(this.txtCSVFile_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(171, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Resultater (IOF XML, OE2003 csv)";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nytLøbToolStripMenuItem,
            this.printMainToolStripMenuItem,
            this.informationToolStripMenuItem,
            this.hjælpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1000, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // nytLøbToolStripMenuItem
            // 
            this.nytLøbToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nytLøbToolStripMenuItem1,
            this.åbnLøbToolStripMenuItem,
            this.redigerToolStripMenuItem,
            this.gemLøbToolStripMenuItem,
            this.gemsomToolStripMenuItem,
            this.toolStripSeparator5,
            this.gemDivisionsresultatToolStripMenuItem1,
            this.toolStripSeparator7,
            this.recentsToolStripMenuItem,
            this.toolStripSeparator3,
            this.startlisteToolStripMenuItem,
            this.toolStripSeparator4,
            this.lukToolStripMenuItem});
            this.nytLøbToolStripMenuItem.Name = "nytLøbToolStripMenuItem";
            this.nytLøbToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.nytLøbToolStripMenuItem.Text = "&Løb";
            // 
            // nytLøbToolStripMenuItem1
            // 
            this.nytLøbToolStripMenuItem1.Name = "nytLøbToolStripMenuItem1";
            this.nytLøbToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.nytLøbToolStripMenuItem1.Size = new System.Drawing.Size(188, 22);
            this.nytLøbToolStripMenuItem1.Text = "&Nyt løb";
            this.nytLøbToolStripMenuItem1.Click += new System.EventHandler(this.nytLøbToolStripMenuItem1_Click);
            // 
            // åbnLøbToolStripMenuItem
            // 
            this.åbnLøbToolStripMenuItem.Name = "åbnLøbToolStripMenuItem";
            this.åbnLøbToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.åbnLøbToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.åbnLøbToolStripMenuItem.Text = "&Åbn løb";
            this.åbnLøbToolStripMenuItem.Click += new System.EventHandler(this.åbnLøbToolStripMenuItem_Click);
            // 
            // redigerToolStripMenuItem
            // 
            this.redigerToolStripMenuItem.Enabled = false;
            this.redigerToolStripMenuItem.Name = "redigerToolStripMenuItem";
            this.redigerToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.redigerToolStripMenuItem.Text = "&Rediger løb";
            this.redigerToolStripMenuItem.Click += new System.EventHandler(this.redigerToolStripMenuItem_Click);
            // 
            // gemLøbToolStripMenuItem
            // 
            this.gemLøbToolStripMenuItem.Enabled = false;
            this.gemLøbToolStripMenuItem.Name = "gemLøbToolStripMenuItem";
            this.gemLøbToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.gemLøbToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.gemLøbToolStripMenuItem.Text = "&Gem løb";
            this.gemLøbToolStripMenuItem.Click += new System.EventHandler(this.gemLøbToolStripMenuItem_Click);
            // 
            // gemsomToolStripMenuItem
            // 
            this.gemsomToolStripMenuItem.Enabled = false;
            this.gemsomToolStripMenuItem.Name = "gemsomToolStripMenuItem";
            this.gemsomToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.gemsomToolStripMenuItem.Text = "Gem &som ...";
            this.gemsomToolStripMenuItem.Click += new System.EventHandler(this.gemsomToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(185, 6);
            // 
            // gemDivisionsresultatToolStripMenuItem1
            // 
            this.gemDivisionsresultatToolStripMenuItem1.Enabled = false;
            this.gemDivisionsresultatToolStripMenuItem1.Name = "gemDivisionsresultatToolStripMenuItem1";
            this.gemDivisionsresultatToolStripMenuItem1.Size = new System.Drawing.Size(188, 22);
            this.gemDivisionsresultatToolStripMenuItem1.Text = "Gem &Divisionsresultat";
            this.gemDivisionsresultatToolStripMenuItem1.Click += new System.EventHandler(this.gemDivisionsresultatToolStripMenuItem1_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(185, 6);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(185, 6);
            // 
            // startlisteToolStripMenuItem
            // 
            this.startlisteToolStripMenuItem.Enabled = false;
            this.startlisteToolStripMenuItem.Name = "startlisteToolStripMenuItem";
            this.startlisteToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.startlisteToolStripMenuItem.Text = "Startliste...";
            this.startlisteToolStripMenuItem.Click += new System.EventHandler(this.startlisteToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(185, 6);
            // 
            // lukToolStripMenuItem
            // 
            this.lukToolStripMenuItem.Name = "lukToolStripMenuItem";
            this.lukToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.lukToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.lukToolStripMenuItem.Text = "&Luk";
            this.lukToolStripMenuItem.Click += new System.EventHandler(this.lukToolStripMenuItem_Click);
            // 
            // printMainToolStripMenuItem
            // 
            this.printMainToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setupPrinterToolStripMenuItem,
            this.toolStripSeparator2,
            this.printPreviewToolStripMenuItem,
            this.printToolStripMenuItem});
            this.printMainToolStripMenuItem.Enabled = false;
            this.printMainToolStripMenuItem.Name = "printMainToolStripMenuItem";
            this.printMainToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.printMainToolStripMenuItem.Text = "&Print";
            // 
            // setupPrinterToolStripMenuItem
            // 
            this.setupPrinterToolStripMenuItem.Name = "setupPrinterToolStripMenuItem";
            this.setupPrinterToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.setupPrinterToolStripMenuItem.Text = "&Setup...";
            this.setupPrinterToolStripMenuItem.Click += new System.EventHandler(this.setupPrinterToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(149, 6);
            // 
            // printPreviewToolStripMenuItem
            // 
            this.printPreviewToolStripMenuItem.Enabled = false;
            this.printPreviewToolStripMenuItem.Name = "printPreviewToolStripMenuItem";
            this.printPreviewToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.printPreviewToolStripMenuItem.Text = "Print P&review...";
            this.printPreviewToolStripMenuItem.Click += new System.EventHandler(this.printPreviewToolStripMenuItem_Click);
            // 
            // printToolStripMenuItem
            // 
            this.printToolStripMenuItem.Enabled = false;
            this.printToolStripMenuItem.Name = "printToolStripMenuItem";
            this.printToolStripMenuItem.ShortcutKeyDisplayString = "";
            this.printToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.printToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.printToolStripMenuItem.Text = "&Print...";
            this.printToolStripMenuItem.Click += new System.EventHandler(this.printToolStripMenuItem_Click);
            // 
            // informationToolStripMenuItem
            // 
            this.informationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.informationServerToolStripMenuItem});
            this.informationToolStripMenuItem.Enabled = false;
            this.informationToolStripMenuItem.Name = "informationToolStripMenuItem";
            this.informationToolStripMenuItem.Size = new System.Drawing.Size(82, 20);
            this.informationToolStripMenuItem.Text = "Information";
            // 
            // informationServerToolStripMenuItem
            // 
            this.informationServerToolStripMenuItem.Name = "informationServerToolStripMenuItem";
            this.informationServerToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.informationServerToolStripMenuItem.Text = "Information Server";
            this.informationServerToolStripMenuItem.Click += new System.EventHandler(this.informationServerToolStripMenuItem_Click);
            // 
            // hjælpToolStripMenuItem
            // 
            this.hjælpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.indholdToolStripMenuItem,
            this.toolStripSeparator1,
            this.skiftFarveToolStripMenuItem,
            this.toolStripMenuItem1,
            this.checkForOpdateringerToolStripMenuItem,
            this.toolStripMenuItem2,
            this.omToolStripMenuItem});
            this.hjælpToolStripMenuItem.Name = "hjælpToolStripMenuItem";
            this.hjælpToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
            this.hjælpToolStripMenuItem.Text = "&Hjælp";
            // 
            // indholdToolStripMenuItem
            // 
            this.indholdToolStripMenuItem.Name = "indholdToolStripMenuItem";
            this.indholdToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.indholdToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.indholdToolStripMenuItem.Text = "&Indhold";
            this.indholdToolStripMenuItem.Click += new System.EventHandler(this.indholdToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(202, 6);
            // 
            // skiftFarveToolStripMenuItem
            // 
            this.skiftFarveToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rødToolStripMenuItem,
            this.grønToolStripMenuItem,
            this.gulToolStripMenuItem,
            this.blåToolStripMenuItem,
            this.standardToolStripMenuItem});
            this.skiftFarveToolStripMenuItem.Name = "skiftFarveToolStripMenuItem";
            this.skiftFarveToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.skiftFarveToolStripMenuItem.Text = "Skift farve";
            // 
            // rødToolStripMenuItem
            // 
            this.rødToolStripMenuItem.Name = "rødToolStripMenuItem";
            this.rødToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.rødToolStripMenuItem.Text = "Rød";
            this.rødToolStripMenuItem.Click += new System.EventHandler(this.rødToolStripMenuItem_Click);
            // 
            // grønToolStripMenuItem
            // 
            this.grønToolStripMenuItem.Name = "grønToolStripMenuItem";
            this.grønToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.grønToolStripMenuItem.Text = "Grøn";
            this.grønToolStripMenuItem.Click += new System.EventHandler(this.grønToolStripMenuItem_Click);
            // 
            // gulToolStripMenuItem
            // 
            this.gulToolStripMenuItem.Name = "gulToolStripMenuItem";
            this.gulToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.gulToolStripMenuItem.Text = "Gul";
            this.gulToolStripMenuItem.Click += new System.EventHandler(this.gulToolStripMenuItem_Click);
            // 
            // blåToolStripMenuItem
            // 
            this.blåToolStripMenuItem.Name = "blåToolStripMenuItem";
            this.blåToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.blåToolStripMenuItem.Text = "Blå";
            this.blåToolStripMenuItem.Click += new System.EventHandler(this.blåToolStripMenuItem_Click);
            // 
            // standardToolStripMenuItem
            // 
            this.standardToolStripMenuItem.Name = "standardToolStripMenuItem";
            this.standardToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.standardToolStripMenuItem.Text = "Standard";
            this.standardToolStripMenuItem.Click += new System.EventHandler(this.standardToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(202, 6);
            // 
            // checkForOpdateringerToolStripMenuItem
            // 
            this.checkForOpdateringerToolStripMenuItem.Name = "checkForOpdateringerToolStripMenuItem";
            this.checkForOpdateringerToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.checkForOpdateringerToolStripMenuItem.Text = "Check for opdateringer...";
            this.checkForOpdateringerToolStripMenuItem.Click += new System.EventHandler(this.checkForOpdateringerToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(202, 6);
            // 
            // omToolStripMenuItem
            // 
            this.omToolStripMenuItem.Name = "omToolStripMenuItem";
            this.omToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.omToolStripMenuItem.Text = "&Om";
            this.omToolStripMenuItem.Click += new System.EventHandler(this.omToolStripMenuItem_Click);
            // 
            // pageSetupDialog1
            // 
            this.pageSetupDialog1.EnableMetric = true;
            // 
            // progressBarMeOS
            // 
            this.progressBarMeOS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarMeOS.BackColor = System.Drawing.SystemColors.Control;
            this.progressBarMeOS.Location = new System.Drawing.Point(176, 34);
            this.progressBarMeOS.Name = "progressBarMeOS";
            this.progressBarMeOS.Size = new System.Drawing.Size(504, 5);
            this.progressBarMeOS.Step = 1;
            this.progressBarMeOS.TabIndex = 13;
            this.progressBarMeOS.Visible = false;
            // 
            // recentsToolStripMenuItem
            // 
            this.recentsToolStripMenuItem.Enabled = false;
            this.recentsToolStripMenuItem.Name = "recentsToolStripMenuItem";
            this.recentsToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.recentsToolStripMenuItem.Text = "Seneste divi filer";
            this.recentsToolStripMenuItem.ItemClick += new System.EventHandler(this.recentsToolStripMenuItem_ItemClick);
            // 
            // frmDivi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 494);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.menuStrip1);
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "frmDivi";
            this.Text = "Divisionsmatch";
            this.Activated += new System.EventHandler(this.frmDivi_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem nytLøbToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem åbnLøbToolStripMenuItem;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Button btnBeregn;
        private System.Windows.Forms.Button bntOpenResultFile;
        private System.Windows.Forms.TextBox txtCSVFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem gemLøbToolStripMenuItem;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.ToolStripMenuItem nytLøbToolStripMenuItem1;
        private System.Windows.Forms.PageSetupDialog pageSetupDialog1;
        private System.Windows.Forms.ToolStripMenuItem redigerToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem lukToolStripMenuItem;
        private System.Windows.Forms.WebBrowser webOutput;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ToolStripMenuItem printMainToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setupPrinterToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem printPreviewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;
        private System.Windows.Forms.Button btnEksport;
        private System.Windows.Forms.ToolStripMenuItem hjælpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem indholdToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem omToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem checkForOpdateringerToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem skiftFarveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rødToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem grønToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gulToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem blåToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem standardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startlisteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem gemsomToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private RecentsToolStripMenuItem recentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gemDivisionsresultatToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem informationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem informationServerToolStripMenuItem;
        private System.Windows.Forms.ProgressBar progressBarMeOS;
    }
}