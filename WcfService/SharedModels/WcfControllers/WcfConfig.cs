using System;


namespace WcfControllers
{
    public class WcfConfig
    {
        public static int WcfPort = 2670;
        public static string WcfName = "WcfDataService.svc";
        public static string WcfAdress = string.Format("http://localhost:{0}/{1}", WcfPort, WcfName);
        public static Uri WcfUri = new Uri(WcfAdress);

        public static string getUserUrl(string email)
        {
            return (WcfAdress + string.Format("/GetUser?email='{0}'", email));
        }
            
    }
}