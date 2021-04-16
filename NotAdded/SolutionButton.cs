using System;
using System.Drawing;
using System.Windows.Forms;
using NotAdded.Properties;

namespace NotAdded
{
    public class SolutionButton : Button
    {
        public SolutionButton(int top, string content, bool showImage, Action<Button> click)
        {
            this.Initialize(top, content, showImage, click);
        }

        private void Initialize(int top, string content, bool showImage, Action<Button> click)
        {
            this.Text = "      " + content;
            this.Height = 27;
            this.Width = 200;
            this.Top = top;
            this.Left = 0;
            this.Margin = new Padding(0);

            if (showImage)
            {
                this.Image = Resources.Solution;
                this.ImageAlign = ContentAlignment.BottomLeft;
            }

            this.TextAlign = ContentAlignment.MiddleLeft;
            this.Click += (EventHandler) ((o, e) => click(this));
        }
    }
}