using System;


namespace EntityModels
{
#if DB_CLASS
    [Serializable]
#endif
    public class Patient
    {
        public int Id { get; set; }
        public int Pesel { get; set; }
        public string Sex { get; set; }
        public int Height { get; set; }
    }
}