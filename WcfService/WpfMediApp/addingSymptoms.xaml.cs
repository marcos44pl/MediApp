using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for addingSymptoms.xaml
    /// </summary>
    public partial class addingSymptoms : Window
    {
        public addingSymptoms()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (TBTemp.Text.CompareTo("") != 0 && TBPress1.Text.CompareTo("") != 0 && TBPress2.Text.CompareTo("") != 0)
            {
                BloodPressure bloodP = new BloodPressure { Diastolic = int.Parse(TBPress2.Text), Systolic = int.Parse(TBPress1.Text) };
                if (symptoms.mySymptoms == null)
                {
                    symptoms.mySymptoms = new List<Symptoms>();
                }
                symptoms.mySymptoms.Add(new Symptoms(int.Parse(TBTemp.Text), bloodP));
                Close();
            }
        }
    }
}
