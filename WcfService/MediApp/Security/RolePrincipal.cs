using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace MediApp.Security
{
    public class RolePrincipal : IPrincipal
    {
        public RolePrincipal(string userName)
        {

        }
        public IIdentity Identity { get; set; }
        public bool IsInRole(string role)
        {
            var roles = role.Split(new char[] { ',' });
            return true;
        }


    }
}