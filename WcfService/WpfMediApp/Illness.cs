using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMediApp
{
    public class Illness
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }

        public Illness(string a, DateTime b)
        {
            Name = a;
            Date = b;
            Text = "Choroba \nnazwa: " + a.ToString() + "\npoczątek choroby: " + b.ToLongDateString();
        }
    }
}
