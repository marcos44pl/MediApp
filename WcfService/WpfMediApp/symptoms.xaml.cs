using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfMediApp
{
    /// <summary>
    /// Interaction logic for symptoms.xaml
    /// </summary>
    public partial class symptoms : Window
    {
        public List<Symptoms> mySymptoms;
        public symptoms()
        {
            InitializeComponent();

            mySymptoms = new List<Symptoms>();
            BloodPressure bp = new BloodPressure { Systolic = 120, Diastolic = 80 };
            mySymptoms.Add(new Symptoms(38.5, bp));
            mySymptoms.Add(new Symptoms(36.2, bp));
            symList.ItemsSource = mySymptoms;
        }
    }
}
