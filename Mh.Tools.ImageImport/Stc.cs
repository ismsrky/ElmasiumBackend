using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mh.Tools.ImageImport
{
    public static class Stc
    {
        public static string ConnStr { get; private set; }
        public static string ProductImageSourceUrl { get; set; }

        public static void ReadConfigs()
        {
            var appSettings = ConfigurationManager.AppSettings;

            ConnStr = appSettings.Get("ConnStr");
            ProductImageSourceUrl = appSettings.Get("ProductImageSourceUrl");
        }
    }
}