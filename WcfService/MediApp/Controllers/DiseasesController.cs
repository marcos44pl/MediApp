using EntityModels;
using MediApp.Models;
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
            Question[] q = new Question[5];
            q.Initialize();
            for (int i = 0; i < 5; i++)
            {
                q[i] = new Question();
                q[i].Name = "Katar";
                q[i].Content = "Czy masz katar?";
            }
            SurveyModel sm = new SurveyModel(q);
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