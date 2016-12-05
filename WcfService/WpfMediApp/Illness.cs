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
        public string Description { get; set; }
        public string Text { get; set; }

        public Illness(string name, DateTime date, string descrip)
        {
            Name = name;
            Date = date;
            Description = descrip;
            Text = "Choroba \nnazwa: " + name.ToString() + "\npoczątek choroby: " + date.ToLongDateString() + "\nopis: " + descrip;
        }
    }
}
