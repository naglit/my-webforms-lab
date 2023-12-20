using Lab.Utility.MyXmlSerialization;
using System;

namespace Lab.Utility.SharedConfigurations
{
    public interface ISharedConfigurationRepository
    {
        DecimalControlConfiguration GetDecimalControlConfiguration();
        DateTime GetFileLastUpdatedDateTime();
    }
}
