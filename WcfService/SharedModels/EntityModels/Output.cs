using System;
using System.Collections.Generic;

namespace EntityModels
{
#if DB_CLASS
    [Serializable]
#endif
    public struct Response
    {
        public int Id { get; set; }
        public Question Question { get; set; }
        public bool Chosen { get; set; }
    }
#if  DB_CLASS
    [Serializable]
#endif
    public class Output
    {
        public int Id { get; set; }
        public List<Response> Answer { get; set; }
    }
}