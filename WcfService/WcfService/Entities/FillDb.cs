using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfService.Entities
{
    public class FillDb
    {
        public FillDb()
        {
            using (var db = new PatientsContext())
            {

                Illness i1 = new Illness { Name = "Grypa" };
                Illness i2 = new Illness { Name = "Astma" };
                Illness i3 = new Illness { Name = "Angina" };

                var illness = db.Set<Illness>();
                illness.Add(i1);
                illness.Add(i2);
                illness.Add(i3);

                db.SaveChanges();

                Patient patient = new Patient { Email = "jankow@pacjent.com", FstName = "Janusz", Surname = "Kowalski", Height = 180, Sex = "mężczyzna" };
                Patient patient1 = new Patient { Email = "marnow@pacjent.com", FstName = "Marian", Surname = "Nowak", Height = 160, Sex = "mężczyzna" };
                var patients = db.Set<Patient>();
                patients.Add(patient);
                patients.Add(patient1);

                db.SaveChanges();

                var patientwassick = db.Set<PatientWasSick>();
                patientwassick.Add(new PatientWasSick { Date = DateTime.Now,Illness = i1, Patient = patient });
                patientwassick.Add(new PatientWasSick { Date = DateTime.Now, Illness = i2, Patient = patient });
                patientwassick.Add(new PatientWasSick { Date = DateTime.Now, Illness = i3, Patient = patient1 });

                db.SaveChanges();

                Question question = new Question { Name = "Kichanie", Content = "Czy kichasz znacznie częściej niż zdarzało Ci się poprzednio?" };
                Question question2 = new Question { Name = "Ból gardła", Content = "Czy boli Cię gardło?" };
                Question question3 = new Question { Name = "Osłabienie", Content = "Czy czujesz się osłabiony?" };
                Question question4 = new Question { Name = "Bóle kostno-stawowe", Content = "Czy odczuwasz bóle kostno-stawowe?" };

                var questions = db.Set<Question>();
                questions.Add(question);
                questions.Add(question2);
                questions.Add(question3);
                questions.Add(question4);
            }
        }
    }
}