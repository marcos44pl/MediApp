using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MediApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string serviceUri = "http://localhost:2670/WcfDataService.svc";
            DbServices.PatientsContext context = new DbServices.PatientsContext(new Uri(serviceUri));
            serviceUri += "/GetIllness";
            var result = context.Execute<DbServices.Illness>(new Uri(serviceUri));
            var resultList = result.ToList();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}