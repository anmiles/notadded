using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace NotAdded.Config
{
    [Serializable]
    public class GlobalExcludesElement
    {
        public GlobalExcludesElement()
        {
            this.Folders = new List<string>();
            this.Files = new List<string>();
            this.Extensions = new List<string>();
        }

        [XmlArrayItem("Folder", typeof(string))]
        [XmlArray("Folders")]
        public List<string> Folders { get; set; }

        [XmlArray("Files")]
        [XmlArrayItem("File", typeof(string))]
        public List<string> Files { get; set; }

        [XmlArrayItem("Extension", typeof(string))]
        [XmlArray("Extensions")]
        public List<string> Extensions { get; set; }
    }
}