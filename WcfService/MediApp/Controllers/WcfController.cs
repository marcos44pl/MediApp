using System;
using System.Collections.Generic;
using System.Data.Services.Client;
using System.Linq;
using System.Web;
using EntityModels;
using WcfControllers;
namespace MediApp.Controllers
{
    public static class WcfController
    {
        static DbServices.PatientsContext db = new DbServices.PatientsContext(WcfConfig.WcfUri);

        public static User findUser(string email)
        {
            var user = db.TableUser.Where(e => e.Email == email).First();

            List<Role> usrRoles = new List<Role>();
            foreach (var r in user.Roles)
                usrRoles.Add(new Role { Id = r.Id, Name = r.Name });  

            return new User { Email = user.Email, Id = user.Id, FstName = user.FstName,
                               Surname = user.Surname, Pass = user.Pass ,
                               Pesel = user.Pesel, Roles = usrRoles };
        }


        public static bool checkIfExist(string email)
        {
            try
            {
                if (1 == db.TableUser.Where(p => p.Email == email.ToLower()).Count())
                    return true;

                return false;
            }
            catch(InvalidOperationException e)
            {
                return false;
            }            
        }
        public static bool authenticateUser(string login, byte[] password)
        {
            if (!checkIfExist(login))
                return false;

            var pat = db.TableUser.Where(p => p.Email == login.ToLower()).First();

            if (null == pat.Pass)
                return false;

            return pat.Pass.SequenceEqual(password);
        }

        public static void createPatient(User user)
        {
            DbServices.Role role = db.TableRole.Where(r => r.Name == RolesKind.PATIENT).FirstOrDefault();

            var userWcf = new DbServices.User
            {
                FstName = user.FstName,
                Surname = user.Surname,
                Email = user.Email,
                Pass = user.Pass,
                Roles = { role }
            };

            try
            {
                db.AddToTableUser(userWcf);

                DataServiceResponse response = db.SaveChanges();
                foreach (ChangeOperationResponse change in response)
                {
                    EntityDescriptor descriptor = change.Descriptor as EntityDescriptor;

                    if (descriptor != null)
                    {
                        DbServices.User added= descriptor.Entity as DbServices.User;

                        if (added != null)
                        {
                            Console.WriteLine("New patient added with email {0}.",
                                added.Email);
                        }
                    }
                }

            }
            catch (DataServiceRequestException ex)
            {
                throw new ApplicationException(
                    "An error occurred when saving changes.", ex);
            }
        }

        public static void saveSurvey(Output output)
        {
            var outputWcf = new DbServices.Output
            {
                Answer = output.Answer
            };
            try
            {
                db.AddToTableOutput(outputWcf);

                DataServiceResponse response = db.SaveChanges();
                foreach (ChangeOperationResponse change in response)
                {
                    EntityDescriptor descriptor = change.Descriptor as EntityDescriptor;

                    if (descriptor != null)
                    {
                        DbServices.Output added = descriptor.Entity as DbServices.Output;

                        if (added != null)
                        {
                            Console.WriteLine("Ankieta została zapisana");
                        }
                    }
                }

            }
            catch (DataServiceRequestException ex)
            {
                throw new ApplicationException(
                    "An error occurred when saving changes.", ex);
            }
        }
    }
}