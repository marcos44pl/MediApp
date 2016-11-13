using System;


namespace EntityModels
{
#if DB_CLASS
    [Serializable]
#endif
    public class Symptom
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
