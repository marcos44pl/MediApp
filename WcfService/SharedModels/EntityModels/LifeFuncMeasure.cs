using System;


namespace EntityModels
{
#if  DB_CLASS
    [Serializable]
#endif
    public class LifeFuncMeasure
    {
        public int Id { get; set; }
        public double Temp { get; set; }
        public int LowPressure { get; set; }
        public int HighPressure { get; set; }
        public int Pulse { get; set; }
        public DateTime Date { get; set; }
        public int PatientId { get; set; }
        public virtual Patient Patient {get;set;}
    }
}