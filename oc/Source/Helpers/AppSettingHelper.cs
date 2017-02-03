using System;
using oc.Source.ApplicationSettings.Impl;
using oc.Source.ApplicationSettings.SettingsConstants;

namespace oc.Source.Helpers
{
   public static class AppSettingHelper
   {
       static AppSettingsSingleton Instance = AppSettingsSingleton.GetInstance();

        
        public static string UserName
       {
           set
           {
               Instance.Save(AppSettingName.UserName, value);
           }
           get
           {
                return Instance.GetByKey(AppSettingName.UserName, string.Empty);
           }
       }
        public static string ProgramId
        {
            set
            {
                Instance.Save(AppSettingName.ProgramId, value);
            }
            get
            {
                return Instance.GetByKey(AppSettingName.ProgramId, string.Empty);
            }
        }
        public static string ParticipantId
        {
            set
            {
                Instance.Save(AppSettingName.ParticipantId, value);
            }
            get
            {
                return Instance.GetByKey(AppSettingName.ParticipantId, string.Empty);
            }
        }
         

       public static void SaveCurrentTime()
       {
            Instance.Save(AppSettingName.TimeSleep, DateTime.Now.ToString());
       }

      private static string GetTimeSleep => Instance.GetByKey(AppSettingName.TimeSleep, string.Empty);

      public static bool IsSleepTimeLessThan15Minutes
      {
           get
           {
                var previousDate = DateTime.Parse(GetTimeSleep);
                return (DateTime.Now.Subtract(previousDate).Minutes <= 15);

           }
      }   
       
   }
}
