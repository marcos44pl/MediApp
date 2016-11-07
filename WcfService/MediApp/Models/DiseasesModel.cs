using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MediApp.Models
{
    public class SurveyModel
    {
        public string Question { get; set; }
        public bool Chosen { get; set; }
    }

    public class DiseasesHistoryModel
    {
        public string Disease { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string Medicine { get; set; }
    }
}