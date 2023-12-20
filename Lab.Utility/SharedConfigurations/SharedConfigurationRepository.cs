using Lab.Utility.MyXmlSerialization;
using Lab.Utility.SharedConfigurations;
using System;
using System.IO;

namespace Lab.Utility.Configurations
{
    internal class SharedConfigurationRepository : ISharedConfigurationRepository
    {
        private const string DECIMAL_CONTROL_CONFIGURATION_FILEPATH = "";

        public SharedConfigurationRepository()
        {
        }

        /// <summary>
        /// Get the latest config values from the xml config file.
        /// </summary>
        /// <returns>Deserialized Configuration Object</returns>
        public DecimalControlConfiguration GetDecimalControlConfiguration()
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

        /// <summary>
        /// Get the last updated datetime of the physical xml file, not cache.
        /// </summary>
        /// <returns></returns>
        public DateTime GetFileLastUpdatedDateTime()
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
    }
}
