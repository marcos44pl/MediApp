using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfService.Entities
{
    public class LifeFuncMeasure
    {
        public int LifeFuncMeasureId { get; set; }
        public double Temp { get; set; }
        public int LowPressure { get; set; }
        public int HighPressure { get; set; }
        public int Pulse { get; set; }
        public int PatientId { get; set; }
        public virtual Patient Patient {get;set;}
    }
}