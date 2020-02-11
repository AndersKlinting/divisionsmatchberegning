using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Divisionsmatch
{
    public partial class frmNancy : Form
    {
        delegate void SetTextCallback(string text);
        private HostingAPI nancy;

        public frmNancy()
        {
            InitializeComponent();
            setLinks();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnStartStop.Text.StartsWith("Start"))
                {
                    ////start server
                    nancy = new HostingAPI();
                    nancy.Start(txtPort.Text);

                    textBox1.Clear();
                    SetText("Api Running");

                    // disable port
                    txtPort.Enabled = false;

                    // enable link
                    lnkLink1.Visible = lnkLink1.Text != string.Empty;
                    lnkLink2.Visible = lnkLink2.Text != string.Empty;
                    lnkLink3.Visible = lnkLink3.Text != string.Empty;
                    lnkLink4.Visible = lnkLink4.Text != string.Empty;

                    // change start to stop
                    btnStartStop.Text = "Stop Server";
                }
                else
                {
                    // stop service
                    nancy.Stop();

                    // enable port
                    txtPort.Enabled = true;

                    // disable links
                    lnkLink1.Visible = false;
                    lnkLink2.Visible = false;
                    lnkLink3.Visible = false;
                    lnkLink4.Visible = false;

                    // change start to stop
                    btnStartStop.Text = "Start Server";

                    SetText("Api Stopped");
                }
            }
            catch(Exception ex)
            {
                SetText(ex.Message);
            }
        }

        private void txtPort_TextChanged(object sender, EventArgs e)
        {
            setLinks();
        }

        private void txtPort_Leave(object sender, EventArgs e)
        {
            int port = 0; 
            if (string.IsNullOrWhiteSpace(txtPort.Text) || !int.TryParse(txtPort.Text, out port) || port<=0)
            {
                MessageBox.Show("Angiv en port som et heltal større end 0", "Fejl", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPort.Focus();
            }
        }

        private void frmNancy_Load(object sender, EventArgs e)
        {

        }

        private void frmNancy_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (btnStartStop.Text.StartsWith("Stop"))
            {
                // stop server
                if (MessageBox.Show("Dette vil stoppe serveren. Fortsæt?", "?", MessageBoxButtons.OKCancel) != DialogResult.OK)
                {
                    e.Cancel = true;
                    return;
                }

                // OK at lukke
                nancy.Stop();
            }
        }

        private void setLinks()
        {
            lnkLink1.Text = "http://localhost:" + txtPort.Text + "/";

            string strHostName = Dns.GetHostName();

            // Find host by name
            IPHostEntry iphostentry = Dns.GetHostEntry(strHostName);

            // Enumerate IP addresses
            int i = 1;
            foreach (IPAddress ipaddress in iphostentry.AddressList)
            {
                if (ipaddress.IsIPv6LinkLocal)
                    continue;
                i++;
                if (i == 2)
                {
                    lnkLink2.Text = "http://" + ipaddress.ToString() + ":" + txtPort.Text + "/";
                }
                if (i == 3)
                {
                    lnkLink3.Text = "http://" + ipaddress.ToString() + ":" + txtPort.Text + "/";
                }
                if (i == 4)
                {
                    lnkLink4.Text = "http://" + ipaddress.ToString() + ":" + txtPort.Text + "/";
                }
            }
        }

        public void startTestAPI()
        {
            SetText("Api Worked");
        }

        internal void SetText(string text)
        {
            if (this.textBox1.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.textBox1.Text += System.DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss]") + ":" + text + Environment.NewLine;
                this.textBox1.SelectionStart = this.textBox1.Text.Length;
                this.textBox1.ScrollToCaret();
            }
        }

        private void lnkLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(((LinkLabel)sender).Text );
        }
    }
}
