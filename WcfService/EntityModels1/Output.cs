using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EntityModels
{
    public struct Response
    {
        public Question Question { get; set; }
        public bool Chosen { get; set; }
    }

    [Serializable]
    public class Output
    {
        public int Id { get; set; }
        public Response[] Answer { get; set; }
    }
}