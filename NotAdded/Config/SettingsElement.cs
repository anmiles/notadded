using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace NotAdded.Config
{
    [Serializable]
    public class SettingsElement
    {
        [XmlArray("Includes")]
        [XmlArrayItem("Include", typeof(string))]
        public List<string> Includes { get; set; }
    }
}