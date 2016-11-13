using System;

namespace EntityModels
{
#if  DB_CLASS
    [Serializable]
#endif
    public class PatientWasSick
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int PatientId { get; set; }
        public virtual Patient Patient { get; set; }
        public int IllnessId { get; set; }
        public virtual Illness Illness { get; set; }
    }
}
