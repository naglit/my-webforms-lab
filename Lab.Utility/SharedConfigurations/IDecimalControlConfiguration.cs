
using System;

namespace Lab.Utility.SharedConfigurations
{
    public interface IDecimalControlConfiguration
    {
        bool NeedsToUpdate(DateTime fileLastUpdated);

        DecimalControlConfigurationVariableList Variables { get; }
    }
}
