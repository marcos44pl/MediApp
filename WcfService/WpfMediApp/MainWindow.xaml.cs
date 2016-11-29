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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfMediApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            illnesses ill = new illnesses();
            ill.Show();
        }
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            symptoms sym = new symptoms();
            sym.Show();
        }
        private void button3_Click(object sender, RoutedEventArgs e)
        {
            addingIllness addIll = new addingIllness();
            addIll.Show();
        }
        private void button4_Click(object sender, RoutedEventArgs e)
        {
            addingSymptoms addSym = new addingSymptoms();
            addSym.Show();
        }
        private void button5_Click(object sender, RoutedEventArgs e)
        {
            login log = new login();
            log.Show();
        }
    }
}
