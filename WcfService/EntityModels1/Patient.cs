using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EntityModels
{
    [Serializable]
    public class Patient
    {
        public int Id { get; set; }
        public int Pesel { get; set; }
        public string Sex { get; set; }
        public int Height { get; set; }
    }
}