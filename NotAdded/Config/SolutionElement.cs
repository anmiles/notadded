using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace NotAdded.Config
{
    [Serializable]
    public class SolutionElement
    {
        public SolutionElement()
        {
            this.Excludes = new List<string>();
        }

        public SolutionElement(string fileName) : this()
        {
            int length = fileName.LastIndexOf(Path.DirectorySeparatorChar);
            this.Name = fileName.Substring(length + 1).Replace(".sln", "");
            this.Folder = fileName.Substring(0, length);
        }

        public SolutionElement(string name, string folder, IEnumerable<string> excludes) : this()
        {
            this.Name = name;
            this.Folder = folder;
            this.Excludes = new List<string>(excludes);
        }

        [XmlAttribute("Name")] public string Name { get; set; }

        [XmlAttribute("Folder")] public string Folder { get; set; }

        [XmlArray("Excludes")]
        [XmlArrayItem("Exclude", typeof(string))]
        public List<string> Excludes { get; set; }
    }
}