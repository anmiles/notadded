using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using NotAdded.Config;

namespace NotAdded
{
    public partial class ExcludesForm : Form
    {
        private readonly NotAddedConfig config;
        private readonly Regex includesRegex;
        private Label label1;
        private Label label2;
        private ListBox listBox1;
        private ListBox listBox2;
        private readonly Regex projectsRegex = new Regex("Project\\(.*?=.*?,\\s*\"(.*?)\"", RegexOptions.Compiled);
        private readonly Regex sdkRegex = new Regex("<Project[^>]+Sdk=\"Microsoft\\.NET\\.Sdk\">", RegexOptions.Compiled);
        private readonly SolutionElement solution;

        public ExcludesForm(NotAddedConfig config, SolutionElement solution)
        {
            this.InitializeComponent();
            this.config = config;
            this.solution = solution;
            this.includesRegex = new Regex($"<({string.Join("|", config.Settings.Includes)}) Include=\"(.*?)\"");
        }

        private void ExcludesForm_Load(object sender, EventArgs e)
        {
            this.listBox1.MouseUp += this.listBox1_MouseUp;
            this.FillListBox();
        }

        private void FillListBox()
        {
            this.listBox1.Items.Clear();
            this.listBox2.Items.Clear();
            string input = File.ReadAllText(Path.Combine(this.solution.Folder, this.solution.Name + ".sln"));
            
            List<string> list1 = this.solution.Excludes
                .Select(ex => Path.Combine(this.solution.Folder, ex.TrimStart(Path.DirectorySeparatorChar)))
                .ToList();    
            
            foreach (Match match in this.projectsRegex.Matches(input))
            {
                FileInfo projectFile = new FileInfo(Path.Combine(this.solution.Folder, match.Groups[1].Value.TrimStart(Path.DirectorySeparatorChar)));

                string projectXml = File.ReadAllText(projectFile.FullName);
                if (this.sdkRegex.IsMatch(projectXml)) continue;
                
                List<string> list2 = this.includesRegex
                    .Matches(File.ReadAllText(projectFile.FullName))
                    .Cast<Match>()
                    .Select(m => Path.Combine(projectFile.DirectoryName, Uri.UnescapeDataString(m.Groups[2].Value.TrimStart(Path.DirectorySeparatorChar)))).ToList();
                
                this.CheckDirectory(projectFile.Directory, ref list2, list1);

                foreach (string path in list2)
                {
                    if (!File.Exists(path))
                    {
                        this.listBox2.Items.Add(path);
                    }
                }
            }
        }

        private void listBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;
            int num = this.listBox1.IndexFromPoint(e.Location);
            
            if (num >= 0)
            {
                this.listBox1.SelectedIndex = num;
                string path = this.listBox1.SelectedItem.ToString().Replace(this.solution.Folder, "");
                ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
                contextMenuStrip.Items.Add("Refresh", null, this.DoRefresh());
                contextMenuStrip.Items.Add(new ToolStripSeparator());
                
                for (string str = path; str.Contains(Path.DirectorySeparatorChar); str = str.Substring(0, str.LastIndexOf(Path.DirectorySeparatorChar)))
                {
                    contextMenuStrip.Items.Add($"Local exclude '{str}'", null, this.LocalExclude(str));
                }

                contextMenuStrip.Items.Add(new ToolStripSeparator());
                string str1 = Path.GetDirectoryName(path).Replace(this.solution.Folder, "");
                char[] chArray = new char[1] {Path.DirectorySeparatorChar};
                
                foreach (string folder in str1.Split(chArray))
                {
                    contextMenuStrip.Items.Add($"Global exclude folder '{folder}'", null, this.GlobalExcludeFolder(folder));
                }

                contextMenuStrip.Items.Add(new ToolStripSeparator());
                contextMenuStrip.Items.Add($"Global exclude file '{Path.GetFileName(path)}'", null, this.GlobalExcludeFile(Path.GetFileName(path)));
                contextMenuStrip.Items.Add(new ToolStripSeparator());
                contextMenuStrip.Items.Add($"Global exclude extension '{Path.GetExtension(path)}'", null, this.GlobalExcludeExt(Path.GetExtension(path)));
                contextMenuStrip.Items.Add(new ToolStripSeparator());
                contextMenuStrip.Items.Add("Exit", null, (o2, e2) => this.Close());
                contextMenuStrip.Show(this.listBox1, e.Location);
            }
        }

        private EventHandler DoRefresh()
        {
            return this.ActionWrapper();
        }

        private EventHandler LocalExclude(string filename)
        {
            return this.ActionWrapper(() => this.solution.Excludes.Add(filename));
        }

        private EventHandler GlobalExcludeFolder(string folder)
        {
            return this.ActionWrapper(() => this.config.GlobalExcludes.Folders.Add(folder));
        }

        private EventHandler GlobalExcludeFile(string file)
        {
            return this.ActionWrapper(() => this.config.GlobalExcludes.Files.Add(file));
        }

        private EventHandler GlobalExcludeExt(string ext)
        {
            return this.ActionWrapper(() => this.config.GlobalExcludes.Extensions.Add(ext));
        }

        private EventHandler ActionWrapper(Action action = null)
        {
            return (o, e) =>
            {
                action?.Invoke();
                this.config.Save();
                this.FillListBox();
            };
        }

        private void CheckDirectory(DirectoryInfo directory, ref List<string> files, List<string> excludes)
        {
            if (this.config.GlobalExcludes.Folders.Contains(directory.Name) || excludes.Contains(directory.FullName)) return;
            
            foreach (DirectoryInfo directory1 in directory.GetDirectories())
            {
                this.CheckDirectory(directory1, ref files, excludes);
            }

            foreach (FileInfo file in directory.GetFiles())
            {
                this.CheckFile(file, ref files, excludes);
            }
        }

        private void CheckFile(FileInfo fileInfo, ref List<string> files, List<string> excludes)
        {
            if (this.config.GlobalExcludes.Extensions.Contains(fileInfo.Extension)) return;
            if (this.config.GlobalExcludes.Files.Contains(fileInfo.Name)) return;
            if (excludes.Contains(fileInfo.FullName)) return;

            if (!files.Contains(fileInfo.FullName))
            {
                this.listBox1.Items.Add(fileInfo.FullName);
            }
            else
            {
                files.Remove(fileInfo.FullName);
            }
        }

        private void listBox2_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right || this.listBox2.IndexFromPoint(e.Location) < 0) return;
            ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
            contextMenuStrip.Items.Add("Refresh", null, this.DoRefresh());
            contextMenuStrip.Items.Add(new ToolStripSeparator());
            contextMenuStrip.Items.Add("Exit", null, (o2, e2) => this.Close());
            contextMenuStrip.Show(this.listBox2, e.Location);
        }
    }
}