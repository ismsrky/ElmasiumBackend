using Mh.Business.Bo.Sys;
using System.Collections.Generic;
using System.Configuration;

namespace Mh.Service.PostMan
{
    public static class Stc
    {
        public static List<long> StartedList { get; set; }
        public static int sayi { get; set; }
        public static string ConnStr { get; private set; }
        public static string PdfFileDirectory { get; set; }
        public static void ReadConfigs()
        {
            var appSettings = ConfigurationManager.AppSettings;

            ConnStr = appSettings.Get("ConnStr");
            PdfFileDirectory = appSettings.Get("PdfFileDirectory");
        }

        public static List<SysMailBo> SysMailList { get; set; }
    }
}