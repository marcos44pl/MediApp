using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using EntityModels;
using MediApp.WcfControllers;

namespace MediApp.Security
{
    public class RolePrincipal : IPrincipal
    {
        private User User;

        public RolePrincipal(string userName)
        {
            User = WcfController.findUser(userName);
            Identity = new GenericIdentity(userName);
        }
        public IIdentity Identity { get; set; }
        public bool IsInRole(string role)
        {
            var roles = role.Split(new char[] { ',' });
            return User.Roles.Any(r => roles.Contains(r.Name));
        }


    }
}