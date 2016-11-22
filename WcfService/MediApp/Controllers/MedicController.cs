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
        public ActionResult Patients()
        {
            ViewBag.Message = "Dane pacjentów";
            return View(new MedicPartialModel());
        }
    }
}