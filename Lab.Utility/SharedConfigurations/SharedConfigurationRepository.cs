using Lab.Utility.MyXmlSerialization;
using Lab.Utility.Reader;
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
            var dto = XmlDeserializationTest.Deserialize<DecimalControlConfigurationDto>(
                DECIMAL_CONTROL_CONFIGURATION_FILEPATH);
            var result = new DecimalControlConfiguration(dto);
            return result;
        }

        public DateTime GetFileLastUpdatedDateTime()
        {
            try
            {
                var fileInfo = new FileInfo(DECIMAL_CONTROL_CONFIGURATION_FILEPATH);
                var lastUpdated = fileInfo.LastWriteTime;
                return lastUpdated;
            }
            catch
            {
                throw new Exception();
            }
        }
    }
}
