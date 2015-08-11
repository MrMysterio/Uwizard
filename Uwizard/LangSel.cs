using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Uwizard {
    public partial class LangSel:Form {
        public LangSel() {
            InitializeComponent();
        }

        public Form1.uwiz_lang ShowDialog(Form1.uwiz_lang slang) {
            lang.SelectedIndex = (int)(slang)-1;
            wasgoodclose = false;
            this.ShowDialog();
            if (!wasgoodclose) Environment.Exit(0);
            return (Form1.uwiz_lang) (lang.SelectedIndex+1);
        }

        bool wasgoodclose = false;

        private void OKbutton_Click(object sender, EventArgs e) {
            wasgoodclose = true;
            this.Close();
        }

        private void LangSel_Load(object sender, EventArgs e) {
            pleaseseltext.Text = "";
            for (int c = 0; c < Langs.texts.Length; c++) {
                pleaseseltext.Text = pleaseseltext.Text + Langs.texts[c][171] + "\r\n";
            }
        }
    }
}