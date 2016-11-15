using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EntityModels;
using MediApp.Controllers;

namespace MediApp.Security
{
    public static class SessionPersister
    {
        static string usernameSessionvar = "username";
        public static User User
        {
            get
            {
                if (!string.IsNullOrEmpty(Username))
                   return WcfController.findUser(Username);

                   return null;
            }
            set
            {

            }
        }
        public static string Username
        {
            get
            {
                if (HttpContext.Current == null)
                    return string.Empty;

                var sessionVar = HttpContext.Current.Session[usernameSessionvar];
                if (sessionVar != null)
                {
                    return sessionVar as string;
                }

                return null;
            }
            set
            {
                HttpContext.Current.Session[usernameSessionvar] = value;
            }
        }
    }
}