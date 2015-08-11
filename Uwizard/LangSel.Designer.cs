namespace Uwizard {
    partial class LangSel {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LangSel));
            this.pleaseseltext = new System.Windows.Forms.Label();
            this.lang = new System.Windows.Forms.ComboBox();
            this.OKbutton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // pleaseseltext
            // 
            this.pleaseseltext.Dock = System.Windows.Forms.DockStyle.Top;
            this.pleaseseltext.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.pleaseseltext.Location = new System.Drawing.Point(0, 0);
            this.pleaseseltext.Name = "pleaseseltext";
            this.pleaseseltext.Size = new System.Drawing.Size(292, 87);
            this.pleaseseltext.TabIndex = 0;
            this.pleaseseltext.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lang
            // 
            this.lang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.lang.FormattingEnabled = true;
            this.lang.Items.AddRange(new object[] {
            "English",
            "Français",
            "Português",
            "Deutsch",
            "Español"});
            this.lang.Location = new System.Drawing.Point(12, 90);
            this.lang.Name = "lang";
            this.lang.Size = new System.Drawing.Size(268, 21);
            this.lang.TabIndex = 1;
            // 
            // OKbutton
            // 
            this.OKbutton.Location = new System.Drawing.Point(205, 117);
            this.OKbutton.Name = "OKbutton";
            this.OKbutton.Size = new System.Drawing.Size(75, 23);
            this.OKbutton.TabIndex = 2;
            this.OKbutton.Text = "OK";
            this.OKbutton.UseVisualStyleBackColor = true;
            this.OKbutton.Click += new System.EventHandler(this.OKbutton_Click);
            // 
            // LangSel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 152);
            this.Controls.Add(this.OKbutton);
            this.Controls.Add(this.lang);
            this.Controls.Add(this.pleaseseltext);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon) (resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LangSel";
            this.Text = "Uwizard";
            this.Load += new System.EventHandler(this.LangSel_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label pleaseseltext;
        private System.Windows.Forms.ComboBox lang;
        private System.Windows.Forms.Button OKbutton;
    }
}