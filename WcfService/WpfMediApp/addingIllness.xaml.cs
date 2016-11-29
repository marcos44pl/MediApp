using MediApp.Security;
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
using WcfControllers;

namespace WpfMediApp
{
    /// <summary>
    /// Interaction logic for addingIllness.xaml
    /// </summary>
    public partial class addingIllness : Window
    {
        static DbServices.PatientsContext db = new DbServices.PatientsContext(WcfConfig.WcfUri);
        public addingIllness()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (TBIllness.Text.CompareTo("") != 0 && datePicker1.Text.CompareTo("") != 0)
            {
                Illness illness = new Illness(TBIllness.Text, datePicker1.DisplayDate);
                if (illnesses.myIllnesses == null)
                {
                    illnesses.myIllnesses = new List<Illness>();
                }
                if (UserPersister.User != null)
                {
                    var illDb = db.TableIllness.Where(i => i.Name == illness.Name).ToList();

                    var ilnessDb = new DbServices.Illness();

                    if (illDb.Count == 0)
                    {
                        ilnessDb.Name = illness.Name;
                        db.AddToTableIllness(ilnessDb);
                        db.SaveChanges();
                    }
                    else
                    {
                        ilnessDb = illDb.First();
                    }

                    var pWasSick = new DbServices.PatientWasSick
                    {
                        Date = illness.Date,
                        Illness = ilnessDb,
                        IllnessId = ilnessDb.Id,
                        PatientId = UserPersister.User.Id
                    };
                    db.AddToTablePatientWasSick(pWasSick);
                    db.SetLink(pWasSick, "Illness", ilnessDb);
                    db.SaveChanges();
                }
                illnesses.myIllnesses.Add(illness);
                Close();
            }
        }
    }
}
