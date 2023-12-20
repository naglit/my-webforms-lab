using Lab.Utility.MyXmlSerialization;
using System;

namespace Lab.Utility.SharedConfigurations
{
    public class SharedConfigurationContainer
    {
        private const string DECIMAL_CONTROL_CONFIGURATION_FILEPATH = "";
        private static DecimalControlConfiguration s_decimalControlConfiguration;
        private static readonly object s_lock = new object();

        public static void Store(DecimalControlConfiguration config, DateTime lastUpdated)
        {
            lock (s_lock)
            {
                s_decimalControlConfiguration = config;
            }
        }

        public static DecimalControlConfiguration Get() => s_decimalControlConfiguration;
    }
}
