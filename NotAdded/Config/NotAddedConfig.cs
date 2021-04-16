using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace NotAdded.Config
{
    [Serializable]
    public class NotAddedConfig
    {
        public NotAddedConfig()
        {
            this.Solutions = new List<SolutionElement>();
            this.GlobalExcludes = new GlobalExcludesElement();
        }

        [XmlArray("Solutions")]
        [XmlArrayItem("Solution", typeof(SolutionElement))]
        public List<SolutionElement> Solutions { get; set; }

        [XmlElement("GlobalExcludes")] public GlobalExcludesElement GlobalExcludes { get; set; }

        [XmlElement("Settings")] public SettingsElement Settings { get; set; }

        public void Save()
        {
            ConfigHelper.Save(this);
        }
    }
}