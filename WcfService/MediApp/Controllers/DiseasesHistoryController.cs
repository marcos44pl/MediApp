﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MediApp.Controllers
{
    public class DiseasesHistoryController : Controller
    {
        // GET: DiseasesHistory
        public ActionResult Index()
        {
            ViewBag.Message = "Historia przebytych chorób";
            return View();
        }
    }
}