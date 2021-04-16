using System;
using System.ComponentModel;
using System.Windows.Forms;
using NotAdded.Config;

namespace NotAdded
{
    public partial class Form1 : Form
    {
        internal const int CONST_buttonWidth = 200;
        internal const int CONST_buttonHeight = 27;

        public Form1()
        {
            this.InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int top = 0;
            NotAddedConfig config = ConfigHelper.Load();
            
            foreach (SolutionElement solution in config.Solutions)
            {
                SolutionElement solution1 = solution;
                this.Controls.Add(new SolutionButton(top, solution.Name, true, button => this.SelectExcludes(config, solution1)));
                top += 27;
            }

            this.Controls.Add(new SolutionButton(top, "Add new...", false, button => this.SelectSolution(filename =>
            {
                SolutionElement newSolution = new SolutionElement(filename);
                config.Solutions.Add(newSolution);
                config.Save();
                SolutionButton solutionButton = new SolutionButton(top, newSolution.Name, true, button2 => this.SelectExcludes(config, newSolution));
                this.Controls.Add(solutionButton);
                top += 27;
                button.Top = top;
                solutionButton.PerformClick();
            })));
        }

        private void SelectSolution(Action<string> callback)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Solution|*.sln";
            dialog.FileOk += (CancelEventHandler) ((sender1, e1) => callback(dialog.FileName));
            dialog.ShowDialog();
        }

        private void SelectExcludes(NotAddedConfig config, SolutionElement solution)
        {
            new ExcludesForm(config, solution).Show();
        }
    }
}