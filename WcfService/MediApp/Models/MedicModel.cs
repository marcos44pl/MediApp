using EntityModels;
using System;
using System.Collections;
using EntityModels;
using System.Collections.Generic;
using WcfControllers;
using System.ComponentModel.DataAnnotations;

namespace MediApp.Models
{
    public class MedicPartialModel
    {
        public bool IsMedic { get; set; }

    }
    public class PatientFull
    {
        public int Id { get; set; }
        [Display(Name = "Imię: ")]
        public string FirstName { get; set; }
        [Display(Name = "Nazwisko: ")]
        public string SurnName { get; set; }
        [Display(Name = "Pesel: ")]
        public string Pesel { get; set; }
        [Display(Name = "Email: ")]
        public string Email { get; set; }
        [Display(Name = "Płeć: ")]
        public string Sex { get; set; }
        [Display(Name = "Waga: ")]
        public int Weight { get; set; }
        [Display(Name = "Wzrost: ")]
        public int Height { get; set; }
    }
    
}