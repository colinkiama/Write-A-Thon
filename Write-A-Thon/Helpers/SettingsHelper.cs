using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Write_A_Thon.Helpers
{
    public class SettingsHelper
    {
        public const string appReviewedSettingName = "wasAppReviewed";
        static ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
        public static bool checkIfSettingIsTrue(string settingsValue)
        {
            bool settingsIsTrue = false;
            if (localSettings.Values[settingsValue] != null)
            {
                bool valueFromSettings = (bool)localSettings.Values[settingsValue];
                if (valueFromSettings == true)
                {
                    settingsIsTrue = true;
                }
            }
            else
            {
                localSettings.Values[settingsValue] = false;
            }

            return settingsIsTrue;
        }

        public static void setSettingToTrue(string settingsValue)
        {
            localSettings.Values[settingsValue] = true;
        }

        public static void setNumericalSetting(string settingName, int value)
        {
            localSettings.Values[settingName] = value;
        }

        public static int getNumericalSetting(string settingName)
        {
            if (localSettings.Values[settingName] != null)
            {
                return (int)localSettings.Values[settingName];
            }
            else
            {
                return 0;
            }
        }
    }
}
