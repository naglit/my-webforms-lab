
using Lab.Utility.MyXmlSerialization;
using Lab.Utility.Reader;
using System;

namespace Lab.Utility.SharedConfigurations
{
    internal class SharedConfigurationService
    {
        
        private ISharedConfigurationRepository _sharedConfigurationRepository;

        //private static (DecimalControlConfiguration, DateTime) s_decimalControlConfiguration;

        public SharedConfigurationService(ISharedConfigurationRepository sharedConfigurationRepository)
        {
            _sharedConfigurationRepository = sharedConfigurationRepository;
        }

        public DecimalControlConfiguration StoreDecimalControlConfiguration()
        {
            lock ()
            {
                // Check if this is the first time to read.
                var config = SharedConfigurationContainer.Get();
                var isFirstTimeToRead = (config == null);

                // Check if the config file has been updated
                var fileLastUpdated = _sharedConfigurationRepository.GetFileLastUpdatedDateTime();
                var hasBeenUpdated = config.AAA(fileLastUpdated);
                var needsConfigUpdate = isFirstTimeToRead || hasBeenUpdated;
                if (needsConfigUpdate == false) return config;

                // Get the latest config values from the xml config file.
                var result = _sharedConfigurationRepository.GetDecimalControlConfiguration();
                SharedConfigurationContainer.Store()= result, fileLastUpdated);
                return s_decimalControlConfiguration.Item1;
            }

        }
    }
}
