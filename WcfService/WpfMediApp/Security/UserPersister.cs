using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EntityModels;
using ComunicationControllers;

namespace MediApp.Security
{
    public static class UserPersister
    {
        static string usernameVar = "username";
        static WpfMediApp.DbServices.PatientsContext db = new WpfMediApp.DbServices.PatientsContext(WcfConfig.WcfUri);
        public static User User
        {
            get
            {
                if (!string.IsNullOrEmpty(Username))
                {
                    var user = db.TableUser.Where(e => e.Email == Username).First();
                    var usrRoles = db.Execute<WpfMediApp.DbServices.Role>(new Uri(WcfConfig.getUserRole(user.Id), UriKind.Relative));
                    var roles = new List<Role>();
                    foreach (var r in usrRoles)
                        roles.Add(new Role { Id = r.Id, Name = r.Name });

                    User user1 = new User
                    {
                        Email = user.Email,
                        Id = user.Id,
                        FstName = user.FstName,
                        Surname = user.Surname,
                        Pass = user.Pass,
                        Pesel = user.Pesel,
                        Roles = roles
                    };
                    return user1;
                }

                   return null;
            }
            set
            {

            }
        }
        public static string Username { get; set; }
    }
}