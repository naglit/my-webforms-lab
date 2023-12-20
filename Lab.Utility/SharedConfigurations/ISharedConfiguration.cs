using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.Utility.SharedConfigurations
{
    public interface ISharedConfiguration
    {
        DateTime LastUpdated { get; set; }
    }
}
