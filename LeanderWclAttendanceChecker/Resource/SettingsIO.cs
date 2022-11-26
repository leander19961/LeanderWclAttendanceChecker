using LeanderWclAttendanceChecker.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeanderWclAttendanceChecker.Resource
{
    public static class SettingsIO
    {
        public static void SaveSettings(Dictionary<string, string> settings)
        {
            Settings defaultSettings = Settings.Default;
            defaultSettings.baseUrl = settings["baseUrl"];
            defaultSettings.useStartTime = settings["useStartTime"];
            defaultSettings.useEndTime = settings["useEndTime"];
        }
    }
}
