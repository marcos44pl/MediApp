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
                Illness i2 = new Illness { Name = "Rak" };
                Illness i3 = new Illness { Name = "Rzeżączka" };

                var illness = db.Set<Illness>();
                illness.Add(i1);
                illness.Add(i2);
                illness.Add(i3);

                db.SaveChanges();

                Patient patient = new Patient { Email = "abc@rak.pl", FstName = "Janusz", Surname = "Rak", Height = 180, Sex = true };
                Patient patient1 = new Patient { Email = "abc@hiv.pl", FstName = "Marian", Surname = "Hiv", Height = 160, Sex = true };
                var patients = db.Set<Patient>();
                patients.Add(patient);
                patients.Add(patient1);

                db.SaveChanges();

                var patientwassick = db.Set<PatientWasSick>();
                patientwassick.Add(new PatientWasSick { Date = DateTime.Now,Illness = i1,Patient = patient });
                patientwassick.Add(new PatientWasSick { Date = DateTime.Now, Illness = i2, Patient = patient });
                patientwassick.Add(new PatientWasSick { Date = DateTime.Now, Illness = i3, Patient = patient1 });

                db.SaveChanges();
            }
        }
    }
}