using ComunicationControllers;
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
    public class DiseasesController : Controller
    {
        static DbServices.PatientsContext db = new DbServices.PatientsContext(WcfConfig.WcfUri);
        // GET: Diseases
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult Survey()
        {
            ViewBag.Message = "Wstępna diagnoza chorób";
            return View(new SurveyModel());
        }
        
        [HttpPost]
        public ActionResult Survey(SurveyModel model)
        {
            ViewBag.Message = "Wstępna diagnoza chorób";
            WcfController.saveSurvey(new EntityModels.Output
            {
                Answer = model.responses
            });

            return RedirectToAction("Index", "Home");
        }

        public ActionResult DiseasesHistory()
        {
            ViewBag.Message = "Historia przebytych chorób";

            return View(new SharedModels.IllnessModel());
        }

        [MediAuthorize(Roles = RolesKind.PATIENT)]
        public ActionResult Illnesses(int id)
        {
            ViewBag.Message = "Historia chorób";
            //var pat = db.TablePatient.Where(e => e.Pesel == SessionPersister.User.Pesel);
            //int id = pat.First().Id;
            var model = new SharedModels.IllnessModel { IdPatient = id };
            return View(model);
        }
    }
}