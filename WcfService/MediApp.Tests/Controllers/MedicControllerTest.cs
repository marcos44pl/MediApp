using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using MediApp.Controllers;
using System.Data.Services.Client;
using System.Linq;


namespace MediApp.Tests.Controllers
{
    [TestClass]
    public class MedicControllrerTest
    {
        [TestMethod]
        public void CreateIllnessResult()
        {
            var db = new DbServices.PatientsContext(ComunicationControllers.WcfConfig.WcfUri);
            var count = db.TablePatientWasSick.Count();
            SharedModels.IllnessModel model = new SharedModels.IllnessModel { Date = DateTime.Now, Description = "test", Name = "test", IdPatient = 3 };
            var controller = new MedicController();
            var result = (RedirectToRouteResult)controller.CreateIllnessResult(model);
            var countAfter = db.TablePatientWasSick.Count();

            Assert.AreEqual(3, result.RouteValues["id"]);
            Assert.AreEqual(countAfter,count + 1);
        }
        [TestMethod]
        public void CreateIllness()
        {
            var model = new SharedModels.IllnessModel { IdPatient = 3 };
            var controller = new MedicController();
            var result = controller.CreateIllness(3) as ViewResult;
            var modelR = (SharedModels.IllnessModel)result.Model;
            Assert.AreEqual(3, modelR.IdPatient);
        }
    }
}
