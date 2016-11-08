using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MediApp.Controllers
{
    public class DiseasesController : Controller
    {
        // GET: Diseases
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Survey()
        {
            ViewBag.Message = "Wstępna diagnoza chorób";
            return View();
        }
        public ActionResult DiseasesHistory()
        {
            ViewBag.Message = "Historia przebytych chorób";
            return View();
        }
    }
}