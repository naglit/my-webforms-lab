using Lab.Utility.MyXmlSerialization;
using System;
using System.IO;
using System.Linq;

namespace Lab.Utility.SharedConfigurations
{
    /// <summary>
    /// A configuration class that reads and stores configuration values like # of valid digits in a configuration xml file.
    /// </summary>
    /// <remarks>
    /// This class acts like "Repository" class
    /// </remarks>
    public class EnvironmentConfiguration : IEnvironmentConfiguration
    {
        /// <summary>Decimal Control Configuration file path</summary>
        private const string s_SharedConfigurationFilepath = @"C:\Users\nitoga\Documents\GitHub\my-webforms-lab\SharedConfiguration\Configurations\EnvironmentConfiguration.xml";
        /// <summary>The configuration instance for singleton pattern</summary>
        private static EnvironmentConfiguration s_instance = Nested.s_instance;
        /// <summary>object for thread safe processing</summary>
        private static readonly object s_lock = new object();

        /// <summary>
        /// Constractor
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="lastUpdated">The lastest date and time that THIS INSTANCE is updated, not physiacl xml file</param>
        /// <remarks>
        /// This constractor must be private so that only the inner class can instantiate, which means the program consistenly has one instance.
        /// </remarks>
        private EnvironmentConfiguration(EnvironmentConfigurationDto dto, DateTime lastUpdated)
        {
            this.Values = new EnvironmentConfigurationList(dto);
            this.LastUpdated = lastUpdated;
        }

        /// <summary>
        /// Get the latest config values from the xml config file.
        /// </summary>
        /// <returns>Deserialized Configuration Object</returns>
        public static EnvironmentConfiguration Get()
        {
            // Reload if this is the first time to read.
            Func<EnvironmentConfiguration> reloadFunc = new Func<EnvironmentConfiguration>(() =>
            {
                var config = DeserializeConfigXml();
                Store(config);
                return config;
            });
            var isFirstTimeToRead = IsFirstTimeToRead();
            if (isFirstTimeToRead)
            {
                s_instance = reloadFunc.Invoke();
                return s_instance;
            }

            // Reload if the physical config file is updated but not this instance.
            var fileLastUpdated = GetFileLastUpdatedDateTime();
            var hasBeenUpdated = s_instance.NeedsToUpdate(fileLastUpdated);
            if (hasBeenUpdated)
            {
                s_instance = reloadFunc.Invoke();
                return s_instance;
            }

            // Get the latest config values from the xml config file.
            return s_instance;
        }

        /// <summary>
        /// Check if this is the first time to read the config.
        /// </summary>
        public static bool IsFirstTimeToRead() => s_instance == null;

        private static EnvironmentConfiguration DeserializeConfigXml()
        {
            EnvironmentConfigurationDto dto;
            DateTime lastUpdated;
            try
            {
                dto = XmlDeserializationTest.Deserialize<EnvironmentConfigurationDto>(
                    s_SharedConfigurationFilepath);
                lastUpdated = GetFileLastUpdatedDateTime();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            var result = new EnvironmentConfiguration(dto, lastUpdated);
            return result;
        }

        private static void Store(EnvironmentConfiguration config)
        {
            lock (s_lock)
            {
                s_instance = config;
            }
        }

        /// <summary>
        /// Get the last updated datetime of the physical xml file, not cache.
        /// </summary>
        public static DateTime GetFileLastUpdatedDateTime()
        {
            try
            {
                var fileInfo = new FileInfo(s_SharedConfigurationFilepath);
                var lastUpdated = fileInfo.LastWriteTime;
                return lastUpdated;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Check if this instance needs to be updated because the physical config file is updated but not this instance.
        /// </summary>
        /// <param name="fileLastUpdated">The lastest date and time that THE PHYSICAL XML FILE is updated, not this instance.</param>
        public bool NeedsToUpdate(DateTime fileLastUpdated) => this.LastUpdated < fileLastUpdated;

        public EnvironmentConfigurationList Values { get; }
        private DateTime LastUpdated { get; }

        #region Nested class for singleton
        /// <summary>
        /// The class for singleton
        /// </summary>
        private class Nested
        {
            internal static readonly EnvironmentConfiguration s_instance;

            static Nested()
            {
            }
        }
        #endregion
    }

    public class EnvironmentConfigurationList
    {
        public EnvironmentConfigurationList(EnvironmentConfigurationDto dto)
        {
            this.Variables = dto.Values.Select(v => new EnvironmentConfigurationValue(v)).ToArray();
        }

        public EnvironmentConfigurationValue[] GetAll() => this.Variables;

        public EnvironmentConfigurationValue GetByKey(string key)
        {
            var result = this.Variables.FirstOrDefault(v => v.Key == key);
            return result;
        }

        private EnvironmentConfigurationValue[] Variables { get; }
    }

    public class EnvironmentConfigurationValue
    {
        public EnvironmentConfigurationValue(EnvironmentConfigurationValueDto v)
        {
            this.Key = v.Key;
            this.Value = v.Value;
        }

        public string Key { get; }

        public string Value { get; }
    }
}
