using System;
using System.Collections.Generic;
using System.Data.Services.Client;
using System.Linq;
using System.Web;
using EntityModels;
using ComunicationControllers;
using MediApp.Models;
using MediApp.Security;

namespace MediApp.Controllers
{   
    public static class WcfController
    {
        static DbServices.PatientsContext db = new DbServices.PatientsContext(WcfConfig.WcfUri);

        public static User findUser(string email)
        {
            var user = db.TableUser.Where(e => e.Email == email).First();
            var usrRoles = db.Execute<DbServices.Role>(new Uri(WcfConfig.getUserRole(user.Id),UriKind.Relative));
            var roles = new List<Role>();
            foreach (var r in usrRoles)
                roles.Add(new Role { Id = r.Id, Name = r.Name });  

            return new User { Email = user.Email, Id = user.Id, FstName = user.FstName,
                               Surname = user.Surname, Pass = user.Pass ,
                               Pesel = user.Pesel, Roles = roles };
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
                Pesel = user.Pesel, 
                Roles = { role }             
            };
            var patient = new DbServices.Patient();
            patient.Pesel = user.Pesel;

            try
            {
                db.AddToTablePatient(patient);
                db.AddRelatedObject(role, "Users", userWcf);
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
                    "Wystąpił błąd podczas zapisu.", ex);
            }
        }
        public static PatientFull getPatient(int id)
        {
            var patient = db.TablePatient.Where(p => p.Id == id).First();
            var user = db.TableUser.Where(p => p.Pesel == patient.Pesel).First();
            var pFull = new PatientFull
            {
                Id = id,
                Pesel = patient.Pesel,
                Sex = patient.Sex,
                Email = user.Email,
                FirstName = user.FstName,
                SurnName = user.Surname,
                Height = patient.Height,
            };
            return pFull;
        }
        public static IEnumerable<SharedModels.IllnessModel> getIllness(int id)
        {
            var illness = db.TablePatientWasSick.Expand("Illness,Illness").Where(i => i.PatientId == id);

            var models = new List<SharedModels.IllnessModel>();
            foreach(var i in illness)
            {
                models.Add(new SharedModels.IllnessModel
                {
                    Date = i.Date,
                    Description = i.Description,
                    Name = i.Illness.Name,
                    IdPatient = id });
            }


            return models.AsEnumerable();
        }

        public static IEnumerable<DbServices.LifeFuncMeasure> getMeasures(int id)
        {
            var measures = db.TableLifeFuncMeasure.Where(m => m.PatientId == id);

           // var list = new List<LifeFuncMeasure>();

          /*  foreach (var it in measures)
            {
                list.Add(new LifeFuncMeasure
                {
                    Date = it.Date,
                    HighPressure = it.HighPressure,
                    LowPressure = it.LowPressure,
                    Id = it.Id,
                    Pulse = it.Pulse,
                    Temp = it.Temp
                });
            }*/

            return measures.AsEnumerable();
        }

        public static List<PatientFull> getAllPatients()
        {
            var patients = db.TablePatient.ToList();
            var users = db.TableUser.ToList();
            var result = from patient in patients
                        join usr in users on patient.Pesel equals usr.Pesel
                        select new { Key = usr,  Value = patient };

            List<PatientFull> all = new List<PatientFull>();

            foreach(var a in result)
            {
                all.Add(new PatientFull
                {
                    Id = a.Value.Id,
                    Email = a.Key.Email,
                    FirstName = a.Key.FstName,
                    SurnName = a.Key.Surname,
                    Height = a.Value.Height,
                    Sex = a.Value.Sex,
                    Pesel = a.Value.Pesel
                });
            }
            return all;
        }
        public static void addIlnnessToDb(SharedModels.IllnessModel illness)
        {
            var illDb = db.TableIllness.Where(i => i.Name == illness.Name).ToList();

            var ilnessDb = new DbServices.Illness();

            if (illDb.Count == 0)
            {
                ilnessDb.Name = illness.Name;
                db.AddToTableIllness(ilnessDb);
                db.SaveChanges();
            }
            else
            {
                ilnessDb = illDb.First();
            }

            var pWasSick = new DbServices.PatientWasSick
            {
                Date = illness.Date,
                Description = illness.Description,
                PatientId = illness.IdPatient,
                Illness = ilnessDb,
                IllnessId = ilnessDb.Id
            };
            db.AddToTablePatientWasSick(pWasSick);
            db.SetLink(pWasSick, "Illness", ilnessDb);
            db.SaveChanges();
        }
    }
}