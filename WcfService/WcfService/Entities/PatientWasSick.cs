using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfService.Entities
{
    [Serializable]
    public class PatientWasSick
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int PatientId { get; set; }
        public virtual Patient Patient { get; set; }
        public int IllnessId { get; set; }
        public virtual Illness Illness { get; set; }
    }
}
