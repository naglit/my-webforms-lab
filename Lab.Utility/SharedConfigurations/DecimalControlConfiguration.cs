
using Lab.Utility.MyXmlSerialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Lab.Utility.SharedConfigurations
{
    public class DecimalControlConfiguration : IDecimalControlConfiguration
    {
        /// <summary>Decimal Control Configuration file path</summary>
        private const string DECIMAL_CONTROL_CONFIGURATION_FILEPATH = "";
        /// <summary>The configuration instance for singleton pattern</summary>
        private static DecimalControlConfiguration s_instance = Nested.s_instance;
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
        private DecimalControlConfiguration(DecimalControlConfigurationDto dto, DateTime lastUpdated)
        {
            this.Variables = new DecimalControlConfigurationVariableList(dto.Variables.Variables);
            this.LastUpdated = lastUpdated;
        }

        /// <summary>
        /// Get the latest config values from the xml config file.
        /// </summary>
        /// <returns>Deserialized Configuration Object</returns>
        public static DecimalControlConfiguration Get()
        {
            // Reload if this is the first time to read.
            Func<DecimalControlConfiguration> reloadFunc = new Func<DecimalControlConfiguration>(() =>
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

        private static DecimalControlConfiguration DeserializeConfigXml()
        {
            DecimalControlConfigurationDto dto;
            DateTime lastUpdated;
            try
            {
                dto = XmlDeserializationTest.Deserialize<DecimalControlConfigurationDto>(
                DECIMAL_CONTROL_CONFIGURATION_FILEPATH);
                lastUpdated = GetFileLastUpdatedDateTime();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            var result = new DecimalControlConfiguration(dto, lastUpdated);
            return result;
        }

        private static void Store(DecimalControlConfiguration config)
        {
            lock (s_lock)
            {
                s_instance = config;
            }
        }

        /// <summary>
        /// Get the last updated datetime of the physical xml file, not cache.
        /// </summary>
        /// <returns></returns>
        public static DateTime GetFileLastUpdatedDateTime()
        {
            try
            {
                var fileInfo = new FileInfo(DECIMAL_CONTROL_CONFIGURATION_FILEPATH);
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

        internal DecimalControlConfigurationVariableList Variables { get; }
        private DateTime LastUpdated { get; }

        /// <summary>
        /// The class for singleton
        /// </summary>
        private class Nested
        {
            internal static readonly DecimalControlConfiguration s_instance;

            static Nested()
            {
            }
        }
    }

    internal class DecimalControlConfigurationVariableList
    {
        public DecimalControlConfigurationVariableList(ICollection<VariableDto> variableDtos)
        {
            this.Variables = variableDtos.Select(v => new DecimalControlConfigurationVariable(v)).ToArray();
        }

        public DecimalControlConfigurationVariable[] Variables { get; }
    }

    internal class DecimalControlConfigurationVariable
    {
        public DecimalControlConfigurationVariable(VariableDto v)
        {
            this.Name = v.Name;
            this.FractionalDigits = v.FractionalDigits;
            this.RoundingType = v.RoundingType;
        }



        public string Name { get; }

        public int FractionalDigits { get; }

        public int RoundingType { get; }
    }

    public enum RoundingType
    {
        Floor = 0,
        Round = 1,
        Ceiling = 2,
    }
}
