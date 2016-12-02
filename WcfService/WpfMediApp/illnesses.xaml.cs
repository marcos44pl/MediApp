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
using ComunicationControllers;

namespace WpfMediApp
{
    /// <summary>
    /// Interaction logic for ilnesses.xaml
    /// </summary>
    public partial class illnesses : Window
    {
        static DbServices.PatientsContext db = new DbServices.PatientsContext(WcfConfig.WcfUri);
        public static List<Illness> myIllnesses;
        public static bool downloadedIllnesses = false;

        public illnesses()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            if (myIllnesses == null)
            {
                myIllnesses = new List<Illness>();
            }
            if (UserPersister.User != null && !downloadedIllnesses)
            {
                var illness = db.TablePatientWasSick.Expand("Illness,Illness").Where(i => i.PatientId == UserPersister.User.Id);

                foreach (var i in illness)
                {
                    myIllnesses.Add(new Illness
                    (
                       i.Illness.Name, i.Date
                    ));
                }
                downloadedIllnesses = true;
            }
            illList.ItemsSource = myIllnesses;
        }
    }
}
