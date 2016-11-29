using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using EntityModels;
using WcfControllers;

namespace MediApp.Security
{
    public class RolePrincipal : IPrincipal
    {
        private User User;
        static WpfMediApp.DbServices.PatientsContext db = new WpfMediApp.DbServices.PatientsContext(WcfConfig.WcfUri);

        public RolePrincipal(string userName)
        {
            var user = db.TableUser.Where(e => e.Email == userName).First();
            var usrRoles = db.Execute<WpfMediApp.DbServices.Role>(new Uri(WcfConfig.getUserRole(user.Id), UriKind.Relative));
            var roles = new List<Role>();
            foreach (var r in usrRoles)
                roles.Add(new Role { Id = r.Id, Name = r.Name });

            User = new User
            {
                Email = user.Email,
                Id = user.Id,
                FstName = user.FstName,
                Surname = user.Surname,
                Pass = user.Pass,
                Pesel = user.Pesel,
                Roles = roles
            };

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