using Lab.Utility.MyXmlSerialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.Utility.SharedConfigurations
{
    public interface ISharedConfigurationRepository
    {
        DecimalControlConfiguration GetDecimalControlConfiguration();
        DateTime GetFileLastUpdatedDateTime();
    }
}
