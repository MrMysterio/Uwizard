using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;

namespace Uwizard {
    partial class AboutBox1:Form {
        public AboutBox1(Form1 userform) {
            InitializeComponent();
            labelCopyright.Text = userform.uwiz_langtext[126];
            linkLabel1.Text = userform.uwiz_langtext[127];
            linkLabel1.Links.Add(linkLabel1.Text.IndexOf("wiiubrew.net"), 12);
            textBoxDescription.Text = userform.uwiz_langtext[147];
            this.Text = userform.uwiz_langtext[101];
            labelVersion.Text = "Version: " + Form1.getVerText(Form1.myversion);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            System.Diagnostics.Process.Start((string) linkLabel1.Tag);
        }

        private void logoPictureBox_Click(object sender, EventArgs e) {
            System.Diagnostics.Process.Start("http://gbatemp.net/members/mr-mysterio.354138/");
        }

        private void pictureBox1_Click(object sender, EventArgs e) {
            System.Diagnostics.Process.Start("https://www.paypal.com/us/cgi-bin/webscr?cmd=_flow&SESSION=GvptKGufGZx-O_7ZzSAFTII_Vo28cAVbniwqJkBF95FeTTU0meTbQaL5FPK&dispatch=5885d80a13c0db1f8e263663d3faee8d99e4111b56ef0eae45e68b8988f5b2dd");
        }
    }
}
