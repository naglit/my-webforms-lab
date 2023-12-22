using System;
using System.Xml.Serialization;

namespace Lab.Utility.SharedConfigurations
{
    [Serializable, XmlRoot("EnvironmentConfiguration")]
    public class EnvironmentConfigurationDto
    {
        [XmlElement("envconfig")]
        public EnvironmentConfigurationValueDto[] Values { get; set; }
    }

    public class EnvironmentConfigurationValueDto
    {
        [XmlAttribute("key")]
        public string Key { get; set; }

        [XmlAttribute("value")]
        public string Value { get; set; }
    }
}
