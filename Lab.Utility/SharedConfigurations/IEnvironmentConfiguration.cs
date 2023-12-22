
using System;

namespace Lab.Utility.SharedConfigurations
{
    public interface IEnvironmentConfiguration
    {
        bool NeedsToUpdate(DateTime fileLastUpdated);

        EnvironmentConfigurationList Values { get; }
    }
}
