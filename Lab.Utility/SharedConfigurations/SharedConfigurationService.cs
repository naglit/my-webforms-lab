
using System;

namespace Lab.Utility.SharedConfigurations
{
    internal class SharedConfigurationService
    {
        private ISharedConfigurationRepository _sharedConfigurationRepository;

        public SharedConfigurationService(ISharedConfigurationRepository sharedConfigurationRepository)
        {
            _sharedConfigurationRepository = sharedConfigurationRepository;
        }

        public DecimalControlConfiguration GetLatestDecimalControlConfiguration()
        {
            // Reload if this is the first time to read.
            Func<DecimalControlConfiguration> reloadFunc = new Func<DecimalControlConfiguration>(() =>
            {
                var config = _sharedConfigurationRepository.GetDecimalControlConfiguration();
                SharedConfigurationCache<DecimalControlConfiguration>.Store(config);
                return config;
            });
            var isFirstTimeToRead = SharedConfigurationCache<DecimalControlConfiguration>.IsFirstTimeToRead();
            if (isFirstTimeToRead)
            {
                var result = reloadFunc.Invoke();
                return result;
            }

            // Reload if the config file has been updated
            var fileLastUpdated = _sharedConfigurationRepository.GetFileLastUpdatedDateTime();
            var hasBeenUpdated = SharedConfigurationCache<DecimalControlConfiguration>.HasBeenUpdated(fileLastUpdated);
            if (hasBeenUpdated)
            {
                var result = reloadFunc.Invoke();
                return result;
            }

            // Get the latest config values from the xml config file.
            return SharedConfigurationCache<DecimalControlConfiguration>.Get();
        }
    }
}
