using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace oc.Source.ApplicationSettings.Impl
{
    public sealed class AppSettingsSingleton
    {
        private AppSettingsSingleton()
        {
        }

        private static volatile AppSettingsSingleton _appSettingsSingletonInstance;

        private static readonly object SyncRoot = new object();

        public static AppSettingsSingleton GetInstance()
        {
            if (_appSettingsSingletonInstance == null)
            {
                lock (SyncRoot)
                {
                    if (_appSettingsSingletonInstance == null)
                    {
                        _appSettingsSingletonInstance = new AppSettingsSingleton();
                    }
                }
            }
            return _appSettingsSingletonInstance;
        }

        private ISettings AppSettings => CrossSettings.Current;

        public string GetByKey(string key, string defaultValue = null)
        {
           var x = AppSettings.GetValueOrDefault(key, defaultValue);
            return AppSettings.GetValueOrDefault(key, defaultValue);
        }
        public void Save(string key, string value)
        {
            AppSettings.AddOrUpdateValue(key, value);
        }
    }
}
