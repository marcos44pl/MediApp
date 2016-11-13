﻿using System;


namespace EntityModels
{
#if  DB_CLASS
    [Serializable]
#endif
    public class IllnessHasSymptom
    {
        public int IllnessHasSymptomId { get; set; }
        public int Priority { get; set; }
        public int IllnessId { get; set; }
        public virtual Illness Illness { get; set; }
        public int SymptomId { get; set; }
        public virtual Symptom Symptom { get; set; }
    }
}
