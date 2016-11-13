using System;

namespace EntityModels
{
#if DB_CLASS
    [Serializable]
#endif
    public class Question
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
    }
}