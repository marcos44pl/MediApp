using System;

namespace EntityModels
{
#if DB_CLASS
    [Serializable]
#endif
    public class Illness
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}
