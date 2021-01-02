using Mh.Business.Bo.Sys;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mh.Service.SmsMan
{
    public static class Stc
    {
        public static List<long> StartedList { get; set; }
        public static string ConnStr { get; private set; }
        public static void ReadConfigs()
        {
            var appSettings = ConfigurationManager.AppSettings;

            ConnStr = appSettings.Get("ConnStr");
        }

        public static List<SysSmsBo> SysSmsList { get; set; }
    }
}