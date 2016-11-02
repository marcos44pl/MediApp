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
            }
        }
    }
}