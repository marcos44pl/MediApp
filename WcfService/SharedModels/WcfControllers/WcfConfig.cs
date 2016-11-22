﻿using System;


namespace WcfControllers
{
    public class WcfConfig
    {
        public static int WcfPort = 2670;
        public static string WcfName = "WcfDataService.svc";
        public static string WcfAdress = string.Format(@"http://localhost:{0}/{1}/", WcfPort, WcfName);
        public static Uri WcfUri = new Uri(WcfAdress);
        public static string TableUser = "TableUser";
        public static string TablePatient = "TablePatient";
        public static string TableMeasure = "TableLifeFuncMeasure";

        public static string getUserUrl(string email)
        {
            return (WcfAdress + string.Format("/GetUser?email='{0}'", email));
        }
        public static string getRole(string role)
        {
            return (WcfAdress + string.Format("/GetRole?role='{0}'",role));

        }
        public static string getPatientMeasure(string pesel)
        {
            return (WcfAdress + string.Format("/GetPatientMeasures?pesel='{0}'", pesel));
        }
        public static string getPatient(string pesel)
        {
            return (WcfAdress + string.Format("/GetPatient?pesel='{0}'", pesel));
        }
        public static string getUserRole(int id)
        {
            return (string.Format("GetUserRole?userid={0}", id));

        }
    }

}