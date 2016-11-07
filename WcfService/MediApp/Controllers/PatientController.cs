using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MediApp.Security;
using EntityModels;

namespace MediApp.Controllers
{
    public class PatientController : Controller
    {
        [MediAuthorize(Roles = RolesKind.PATIENT)]
        public ActionResult Index()
        {
            return View();
        }
    }
}