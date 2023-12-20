
using System;

namespace Lab.Utility.SharedConfigurations
{
    public class DecimalControlConfigurationCache
    {
        private static DecimalControlConfigurationCache s_instance = Nested.s_instance;
        private static DecimalControlConfiguration s_config;
        private static readonly object s_lock = new object();

        private DecimalControlConfigurationCache() { }
        public static void Store(DecimalControlConfiguration config)
        {
            lock (s_lock)
            {
                s_config = config;
            }
        }

        public static bool HasBeenUpdated(DateTime fileLastUpdated) => s_config.LastUpdated < fileLastUpdated;
        public static DecimalControlConfiguration Get() => s_config;
        public static bool IsFirstTimeToRead() => s_config == null;
        private class Nested
        {
            internal static readonly DecimalControlConfigurationCache s_instance = new DecimalControlConfigurationCache();

            static Nested()
            {
            }
        }
    }
}
