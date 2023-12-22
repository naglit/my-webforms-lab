using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Lab.Utility.SharedConfigurations
{
    [Serializable, XmlRoot("DecimalControlConfiguration")]
    public class DecimalControlConfigurationDto
    {
        [XmlElement("Variables")]
        public VariableDtoList Variables { get; set; }
    }

    public class VariableDtoList
    {
        [XmlElement("Variable")]
        public VariableDto[] Variables { get; set; }
    }

    public class VariableDto
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("fractionalDigits")]
        public int FractionalDigits { get; set; }

        [XmlAttribute("roundingType")]
        public string RoundingType { get; set; }
    }
}
