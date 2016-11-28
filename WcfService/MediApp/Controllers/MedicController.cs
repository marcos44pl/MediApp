using EntityModels;
using MediApp.Models;
using MediApp.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MediApp.Controllers
{
    public class MedicController : Controller
    {
        [MediAuthorize(Roles = RolesKind.MEDIC)]
        public ActionResult Index()
        {
            return View();
        }

        [MediAuthorize(Roles = RolesKind.MEDIC)]
        public ActionResult Patients()
        {
            ViewBag.Message = "Dane pacjentów";
            return View(WcfController.getAllPatients().AsEnumerable());
        }
        [MediAuthorize(Roles = RolesKind.MEDIC)]
        public ActionResult PatientDetails(int id)
        {
            ViewBag.Message = "Szczegóły pacjenta:";
            return View(WcfController.getPatient(id));
        }
        [MediAuthorize(Roles = RolesKind.MEDIC)]
        public ActionResult PatientsIlnessPartial(int id)
        {
            ViewBag.Message = "Choroby pacjenta:";
            return PartialView(WcfController.getIllness(id));
        }
        [MediAuthorize(Roles = RolesKind.MEDIC)]
        public ActionResult CreateIllness(int id)
        {
            IllnessModel model = new IllnessModel { IdPatient = id };
            return View(model);
        }
        [MediAuthorize(Roles = RolesKind.MEDIC)]
        public ActionResult CreateIllnessResult(IllnessModel illModel)
        {
            illModel.Date = DateTime.Now;
          //  illModel.IdPatient = id;
            WcfController.addIlnnessToDb(illModel);
            return RedirectToAction("PatientDetails",new { id = illModel.IdPatient });
        }

    }
}