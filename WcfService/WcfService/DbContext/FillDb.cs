using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EntityModels;

namespace WcfService.DbContext
{
    public class FillDb
    {
        public FillDb()
        {
            using (var db = new PatientsContext())
            {
                var users = db.Set<User>();
                User u = new User { Email = "abc@rak.pl" };
                User u1 = new User { Email = "efg@hiv.pl" };

                users.Add(u);
                users.Add(u1);

                var roles = db.Set<Role>();
                Role r = new Role { Name = "Pacjent" };
                Role r1 = new Role { Name = "Lekarz" };

                roles.Add(r);
                roles.Add(r1);

                Patient patient = new Patient {  Height = 180, Sex = true };
                Patient patient1 = new Patient { Height = 160, Sex = true };
                var patients = db.Set<Patient>();
                patients.Add(patient);
                patients.Add(patient1);

                var uhrs = db.Set<UserHasRole>();
                UserHasRole uhr = new UserHasRole { Role = r, User = u };
                UserHasRole uhr1 = new UserHasRole { Role = r, User = u1 };

                uhrs.Add(uhr);
                uhrs.Add(uhr1);

                db.SaveChanges();

                Illness i1 = new Illness { Name = "Grypa" };
                Illness i2 = new Illness { Name = "Astma" };
                Illness i3 = new Illness { Name = "Angina" };
                Illness i4 = new Illness { Name = "Nieżyt nosa" };
                Illness i5 = new Illness { Name = "Przewlekłe zapalenie krtani" };
                Illness i6 = new Illness { Name = "Zapalenie oskrzeli" };
                Illness i7 = new Illness { Name = "Katar sienny" };

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
        }
    }
}