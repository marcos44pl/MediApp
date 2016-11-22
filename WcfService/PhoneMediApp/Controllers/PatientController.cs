using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityModels;
using WcfControllers;
namespace PhoneMediApp.Controllers
{
    class PatientController
    {
        public static async void AddMeasure(LifeFuncMeasure measure)
        {
            string pesel = UserController.getUserPesel();
            WcfRestController<Patient> cont = new WcfRestController<Patient>();
            WcfRestController<LifeFuncMeasure> contMeas = new WcfRestController<LifeFuncMeasure> ();
            List<Patient> list = await cont.getObjects(WcfConfig.getPatient(pesel));
            measure.Patient = list.FirstOrDefault();
            await contMeas.insertObject(measure, WcfConfig.TableMeasure);
        }

    }
}
