using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityModels;
using ComunicationControllers;
using SharedModels;


namespace PhoneMediApp.Controllers
{
    class PatientController
    {
        public static async void AddMeasure(LifeFuncMeasure measure)
        {
            string pesel = UserController.getUserPesel();
            RestController<Patient> cont = new RestController<Patient>();
            RestController<LifeFuncMeasure> contMeas = new RestController<LifeFuncMeasure> ();
            List<Patient> list = await cont.getObjects(WcfConfig.getPatient(pesel));
            measure.PatientId = list.First().Id;
            await contMeas.insertObject(measure, WcfConfig.TableMeasure);
        }

        public static async Task<IEnumerable<IllnessModel>> GetIllnesses(int id)
        {
            var rest = new RestController<IllnessModel>();
            return await rest.getObjectsApi(WebApiConfig.GetIlnness(id));
        }

    }
}
