﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EntityModels;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace WcfService.DbContext
{
    public class FillDb
    {
        public FillDb()
        {
            using (var db = new PatientsContext())
            {
                try
                {
                    var users = db.Set<User>();
                    User u = new User { Email = "abc@rak.pl" };
                    User u1 = new User { Email = "efg@hiv.pl" };

                    string pass = "lekarz";
                    SHA256Managed crypt = new SHA256Managed();
                    byte[] bytes = crypt.ComputeHash(Encoding.ASCII.GetBytes(pass), 0,
                                   Encoding.ASCII.GetByteCount(pass));
                    User u2 = new User { Email = "lekarz@lekarz.pl", Pass = bytes };
                    users.Add(u);
                    users.Add(u1);
                    users.Add(u2);
                    db.SaveChanges();
                
                    var roles = db.Set<Role>();
                    Role r = new Role { Name = RolesKind.PATIENT, Users = new List<User>{ u,u1} };
                    r.Users.Add(u);
                    r.Users.Add(u1);
                    Role r1 = new Role { Name = RolesKind.MEDIC, Users = new List<User> { u2 } };
                    Role r2 = new Role { Name = RolesKind.ADMIN };
                    roles.Add(r);
                    roles.Add(r1);
                    roles.Add(r2);

                    db.SaveChanges();

                    Patient patient = new Patient {  Height = 180, Sex = "mężczyzna" };
                    Patient patient1 = new Patient { Height = 160, Sex = "kobieta" };
                    var patients = db.Set<Patient>();
                    patients.Add(patient);
                    patients.Add(patient1);

                    db.SaveChanges();

                    Illness i1 = new Illness { Name = "Grypa", Date = new DateTime(2015, 10, 9), Description = "grypa" };
                    Illness i2 = new Illness { Name = "Astma", Date = new DateTime(2016, 11, 19), Description = "astma" };
                    Illness i3 = new Illness { Name = "Angina", Date = new DateTime(2015, 1, 30), Description = "angina" };
                    Illness i4 = new Illness { Name = "Nieżyt nosa", Date = new DateTime(2015, 2, 3), Description = "katar" };
                    Illness i5 = new Illness { Name = "Przewlekłe zapalenie krtani", Date = new DateTime(2015, 3, 5), Description = "krtań" };
                    Illness i6 = new Illness { Name = "Zapalenie oskrzeli", Date = new DateTime(2015, 1, 1), Description = "oskrzela" };
                    Illness i7 = new Illness { Name = "Katar sienny", Date = new DateTime(2015, 7, 24), Description = "katar" };

                    var illness = db.Set<Illness>();
                    illness.Add(i1);
                    illness.Add(i2);
                    illness.Add(i3);

                    db.SaveChanges();

                    var patientwassick = db.Set<PatientWasSick>();
                    patientwassick.Add(new PatientWasSick { Date = DateTime.Now,Illness = i1, Patient = patient });
                    patientwassick.Add(new PatientWasSick { Date = DateTime.Now, Illness = i2, Patient = patient });
                    patientwassick.Add(new PatientWasSick { Date = DateTime.Now, Illness = i3, Patient = patient1 });

                    db.SaveChanges();

                    // Adding all questions 
                    Question question1 = new Question { Name = "Kichanie", Content = "Czy kichasz znacznie częściej niż zdarzało Ci się poprzednio?" };
                    Question question2 = new Question { Name = "Ból gardła", Content = "Czy boli Cię gardło?" };
                    Question question3 = new Question { Name = "Osłabienie", Content = "Czy czujesz się osłabiony?" };
                    Question question4 = new Question { Name = "Bóle kostno-stawowe", Content = "Czy odczuwasz bóle kostno-stawowe?" };
                    Question question5 = new Question { Name = "Poczucie rozbicia", Content = "Czy czujesz się bardziej zmęczony i osłabiony niż zazwyczaj?" };
                    Question question6 = new Question { Name = "Ból głowy", Content = "Czy odczuwasz nasilony, długotrwały ból głowy?" };
                    Question question7 = new Question { Name = "Dreszcze", Content = "Czy masz dreszcze?" };
                    Question question8 = new Question { Name = "Długotrwały katar", Content = "Czy masz katar trwający dłużej niż 2 tygodnie?" };
                    Question question9 = new Question { Name = "Suchy kaszel", Content = "Czy masz suchy kaszel?" };
                    Question question10 = new Question { Name = "Świszczący oddech", Content = "Czy masz świszczący oddech?" };
                    Question question11 = new Question { Name = "Napady duszności", Content = "Czy masz napady duszności lub mocny ucisk w klatce piersiowej, w szczególności po wysiłku?" };
                    Question question12 = new Question { Name = "Zaczerwienione gardło", Content = "Czy masz zaczerwienione lub opuchnięte gardło?" };
                    Question question13 = new Question { Name = "Spuchnięte gardło", Content = "Czy masz spuchnięte gardło, problemy z przełykaniemm i męczy Cię chrypka?" };

                    var questions = db.Set<Question>();
                    questions.Add(question1);
                    questions.Add(question2);
                    questions.Add(question3);
                    questions.Add(question4);
                    questions.Add(question5);
                    questions.Add(question6);
                    questions.Add(question7);
                    questions.Add(question8);
                    questions.Add(question9);
                    questions.Add(question10);
                    questions.Add(question11);
                    questions.Add(question12);
                    questions.Add(question13);

                    db.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw;
                }
                catch(DbUpdateException e)
                {
                    Debug.WriteLine(e.Message);
                    throw;
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                    throw;
                }

            }
        }
    }
}