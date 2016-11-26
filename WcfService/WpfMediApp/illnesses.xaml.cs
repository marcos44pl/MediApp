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
    /// Interaction logic for ilnesses.xaml
    /// </summary>
    public partial class illnesses : Window
    {
        public List<Illness> myIllnesses;

        public illnesses()
        {
            InitializeComponent();

            myIllnesses = new List<Illness>();
            DateTime dt1 = new DateTime(2015, 10, 5);
            DateTime dt2 = new DateTime(2016, 4, 2);
            myIllnesses.Add(new Illness("grypa", dt1));
            myIllnesses.Add(new Illness ("ospa", dt2));
            illList.ItemsSource = myIllnesses;
        }
    }
}
