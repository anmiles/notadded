using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NotAdded.Config
{
    internal static class ConfigHelper
    {
        private const string CONST_configFile = "NotAdded.config";

        private static XmlReaderSettings XmlReaderSettings => new XmlReaderSettings
        {
            CheckCharacters = false,
            CloseInput = true,
            ConformanceLevel = ConformanceLevel.Document,
            DtdProcessing = DtdProcessing.Ignore,
            IgnoreComments = true,
            IgnoreProcessingInstructions = false,
            IgnoreWhitespace = true,
            ValidationFlags = XmlSchemaValidationFlags.AllowXmlAttributes,
            ValidationType = ValidationType.None
        };

        private static XmlWriterSettings XmlWriterSettings => new XmlWriterSettings
        {
            CheckCharacters = false,
            CloseOutput = true,
            ConformanceLevel = ConformanceLevel.Document,
            Encoding = Encoding.UTF8,
            Indent = true,
            IndentChars = "\t",
            NamespaceHandling = NamespaceHandling.OmitDuplicates,
            NewLineChars = "\r\n",
            NewLineHandling = NewLineHandling.Replace,
            NewLineOnAttributes = false,
            OmitXmlDeclaration = false
        };

        public static NotAddedConfig Load()
        {
            using (FileStream fileStream = new FileStream(CONST_configFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (XmlReader xmlReader = XmlReader.Create(fileStream, XmlReaderSettings))
                {
                    return (NotAddedConfig) new XmlSerializer(typeof(NotAddedConfig)).Deserialize(xmlReader);
                }
            }
        }

        public static void Save(NotAddedConfig config)
        {
            using (FileStream fileStream = new FileStream(CONST_configFile, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
            {
                using (XmlWriter xmlWriter = XmlWriter.Create(fileStream, XmlWriterSettings))
                {
                    new XmlSerializer(typeof(NotAddedConfig)).Serialize(xmlWriter, config);
                }
            }
        }
    }
}