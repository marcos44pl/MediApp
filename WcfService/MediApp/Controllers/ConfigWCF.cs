using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MediApp.Controllers
{
    public class ConfigWCF
    {
        public static int WcfPort = 2670;
        public static string WcfName = "WcfDataService.svc";
        public static string WcfAdress = string.Format("http://localhost:{1}/{2}",WcfPort,WcfName);
        public static Uri WcfUri = new Uri(WcfAdress);
    }
}