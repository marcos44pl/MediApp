using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MediApp.Models
{
    public class PatientPartialModel
    {
        public bool IsPatient { get; set; }
    }

    public class IllnessModel
    {
        public int                    IdPatient { get; set; }
        [Display(Name = "Nazwa: ")]
        public string                 Name { get; set; }
        [Display(Name = "Opis choroby: ")]
        public string                 Description { get; set; }
        [Display(Name = "Data: ")]
        public DateTime               Date { get; set; }
    }
}
