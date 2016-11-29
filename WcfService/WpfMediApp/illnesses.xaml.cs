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
using MediApp.Security;
using WcfControllers;

namespace WpfMediApp
{
    /// <summary>
    /// Interaction logic for ilnesses.xaml
    /// </summary>
    public partial class illnesses : Window
    {
        static DbServices.PatientsContext db = new DbServices.PatientsContext(WcfConfig.WcfUri);
        public static List<Illness> myIllnesses;

        public illnesses()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            if (myIllnesses == null)
            {
                myIllnesses = new List<Illness>();
                DateTime dt1 = new DateTime(2015, 10, 5);
                DateTime dt2 = new DateTime(2016, 4, 2);
                myIllnesses.Add(new Illness("grypa", dt1));
                myIllnesses.Add(new Illness ("ospa", dt2));
            }
            if (UserPersister.User != null)
            {
                var illness = db.TablePatientWasSick.Expand("Illness,Illness").Where(i => i.PatientId == UserPersister.User.Id);

                foreach (var i in illness)
                {
                    myIllnesses.Add(new Illness
                    (
                       i.Illness.Name, i.Date
                    ));
                }


                /*var patientDb = db.TablePatientWasSick.Where(i => i.Patient.Pesel == UserPersister.User.Pesel).ToList();
                List<DbServices.Illness> dbIllnesses = new List<DbServices.Illness>();
                foreach (DbServices.PatientWasSick p in patientDb)
                {
                    dbIllnesses = db.TableIllness.Where(i => i.Id == p.IllnessId).ToList();

                    foreach (DbServices.Illness dbill in dbIllnesses) {
                        myIllnesses.Add(new Illness (dbill.Name, DateTime.Now));
                    }
                }*/
            }
            illList.ItemsSource = myIllnesses;
        }
    }
}
