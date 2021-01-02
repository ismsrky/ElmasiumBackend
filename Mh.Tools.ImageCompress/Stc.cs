using Mh.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mh.Tools.ImageCompress
{
    public static class Stc
    {
        public static string ConnStr { get; private set; }
        public static string TinifyKey { get; set; }
        public static string ImageSourceUrl { get; set; }
        public static int StartHour { get; set; }

        public static void ReadConfigs()
        {
            var appSettings = ConfigurationManager.AppSettings;

            ConnStr = appSettings.Get("ConnStr");
            TinifyKey = appSettings.Get("TinifyKey");
            ImageSourceUrl = appSettings.Get("ImageSourceUrl");
            StartHour = appSettings.Get("StartHour").ToInt32();
        }
    }
}