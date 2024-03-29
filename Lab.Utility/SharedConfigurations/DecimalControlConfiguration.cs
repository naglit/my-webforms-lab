﻿
using Lab.Utility.MyCsharp;
using Lab.Utility.MyCustomAttribute;
using Lab.Utility.MyXmlSerialization;
using System;
using System.Collections.Generic;
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
    public class DecimalControlConfiguration : IDecimalControlConfiguration
    {
        /// <summary>Decimal Control Configuration file path</summary>
        private static string CONFIG_KEY = "DecimalControlConfigurationFilePath";
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
                var path = EnvironmentConfiguration.Get().Values.GetByKey(CONFIG_KEY)?.Value;
                dto = XmlDeserializationTest.Deserialize<DecimalControlConfigurationDto>(
                    path);
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
        public static DateTime GetFileLastUpdatedDateTime()
        {
            try
            {
                var path = EnvironmentConfiguration.Get().Values.GetByKey(CONFIG_KEY)?.Value;
                var fileInfo = new FileInfo(path);
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

        public DecimalControlConfigurationVariableList Variables { get; }
        private DateTime LastUpdated { get; }

        #region Nested class for singleton
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
        #endregion
    }

    public class DecimalControlConfigurationVariableList
    {
        public DecimalControlConfigurationVariableList(ICollection<VariableDto> variableDtos)
        {
            this.Variables = variableDtos.Select(v => new DecimalControlConfigurationVariable(v)).ToArray();
        }

        public DecimalControlConfigurationVariable[] GetAll() => this.Variables;

        public DecimalControlConfigurationVariable GetByName(string name)
        {
            var result = this.Variables.FirstOrDefault(v => v.Name == name);
            return result;
        }

        private DecimalControlConfigurationVariable[] Variables { get; }
    }

    public class DecimalControlConfigurationVariable
    {
        public DecimalControlConfigurationVariable(VariableDto v)
        {
            this.Name = v.Name;
            this.FractionalDigits = v.FractionalDigits;
            this.RoundingType = EnumHelper<DecimalRoundingType>.Parse(v.RoundingType);
        }

        public string Name { get; }

        public int FractionalDigits { get; }

        public DecimalRoundingType RoundingType { get; }
    }

    public enum DecimalRoundingType
    {
        [ResourceValue("0")]
        Floor,
        [ResourceValue("1")]
        Round,
        [ResourceValue("2")]
        Ceiling,
    }
}
