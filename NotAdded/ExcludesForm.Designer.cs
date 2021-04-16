using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using NotAdded.Properties;

namespace NotAdded
{
    partial class ExcludesForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ExcludesForm));
            this.listBox1 = new ListBox();
            this.label1 = new Label();
            this.label2 = new Label();
            this.listBox2 = new ListBox();
            this.SuspendLayout();
            this.listBox1.BackColor = SystemColors.Control;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new Point(15, 25);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new Size(957, 342);
            this.listBox1.TabIndex = 0;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new Size(78, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Not added files";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(12, 392);
            this.label2.Name = "label2";
            this.label2.Size = new Size(83, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Not existing files";
            this.listBox2.BackColor = SystemColors.Control;
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Location = new Point(15, 408);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new Size(957, 342);
            this.listBox2.TabIndex = 0;
            this.listBox2.MouseUp += new MouseEventHandler(this.listBox2_MouseUp);
            this.AutoScaleDimensions = new SizeF(6f, 13f);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(984, 762);
            this.Controls.Add((Control) this.label2);
            this.Controls.Add((Control) this.label1);
            this.Controls.Add((Control) this.listBox2);
            this.Controls.Add((Control) this.listBox1);
            this.Icon = Resources.Icon;
            this.Name = nameof (ExcludesForm);
            this.Text = "Excludes";
            this.TopMost = true;
            this.Load += new EventHandler(this.ExcludesForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}