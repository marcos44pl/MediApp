using System;
using System.Collections.Generic;
using System.Text;

namespace SharedModels
{
    public class IllnessModel
    {
        public int IdPatient { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}
