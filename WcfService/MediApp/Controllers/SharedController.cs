using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MediApp.Models;
using MediApp.Security;
using EntityModels;

namespace MediApp.Controllers
{
    public class SharedController : Controller
    {
        
        [ChildActionOnly]
        public ActionResult _PatientPartial()
        {
            PatientPartialModel model = new PatientPartialModel();

            if (null != SessionPersister.User &&
               SessionPersister.User.Roles.Any(r => r.Name == RolesKind.PATIENT))
                model.IsPatient = true;
            else
                model.IsPatient = false;

            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult _MedicPartial()
        {
            MedicPartialModel model = new MedicPartialModel();

            if (null != SessionPersister.User &&
               SessionPersister.User.Roles.Any(r => r.Name == RolesKind.MEDIC))
                model.IsMedic = true;
            else
                model.IsMedic = false;

            return PartialView(model);
        }
    }
}