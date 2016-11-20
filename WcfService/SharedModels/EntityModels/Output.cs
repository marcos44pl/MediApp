using System;


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
        public Response[] Answer { get; set; }
    }
}