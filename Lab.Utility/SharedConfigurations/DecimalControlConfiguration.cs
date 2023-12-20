
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab.Utility.SharedConfigurations
{
    public class DecimalControlConfiguration : ISharedConfiguration
    {
        public DecimalControlConfiguration(DecimalControlConfigurationDto dto, DateTime lastUpdated)
        {
            this.Variables = new DecimalControlConfigurationVariableList(dto.Variables.Variables);
            this.LastUpdated = lastUpdated;
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
