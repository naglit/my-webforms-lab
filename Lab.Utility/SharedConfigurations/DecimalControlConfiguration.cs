using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Xml.Serialization;

namespace Lab.Utility.MyXmlSerialization
{
    public class DecimalControlConfiguration
    {
        public DecimalControlConfiguration(DecimalControlConfigurationDto dto)
        {
            this.Variables = new DecimalControlConfigurationVariableList(dto.Variables.Variables);
        }

        public bool AAA(DateTime fileLastUpdated)
        {
            var hasBeenUpdated = this.LastUpdated < fileLastUpdated;
            if (needsConfigUpdate == false) return s_decimalControlConfiguration.Item1;
        }
        public DecimalControlConfigurationVariableList Variables { get; set; }
        public DateTime LastUpdated { get; set; }
    }

    public class DecimalControlConfigurationVariableList
    {
        public DecimalControlConfigurationVariableList(ICollection<VariableDto> variableDtos)
        {
            this.Variables = variableDtos.Select(v => new DecimalControlConfigurationVariable(v)).ToArray();
        }

        public DecimalControlConfigurationVariable[] Variables { get; set; }
    }

    public class DecimalControlConfigurationVariable
    {
        public DecimalControlConfigurationVariable(VariableDto v)
        {
            this.Name= v.Name;
            this.FractionalDigits = v.FractionalDigits;
            this.RoundingType = v.RoundingType;
        }

        public string Name { get; set; }

        public int FractionalDigits { get; set; }

        public int RoundingType { get; set; }
    }

    public enum RoundingType
    {
        Floor = 0,
        Round = 1,
        Ceiling = 2,
    }
}
