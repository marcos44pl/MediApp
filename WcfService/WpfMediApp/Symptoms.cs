using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMediApp
{
    public struct BloodPressure
    {
        public int Systolic { get; set; }
        public int Diastolic { get; set; }
    }
    public class Symptoms
    {
        public int Id { get; set; }
        public double Temperature { get; set; }
        public BloodPressure Pressure { get; set; }
        public string Text { get; set; }

        public Symptoms(double a, BloodPressure b)
        {
            Temperature = a;
            Pressure = b;
            Text = "Objawy \ntemperatura: " + a.ToString() + ", ciśnienie: " + b.Systolic.ToString() + "/" + b.Diastolic.ToString();
        }
    }
}
