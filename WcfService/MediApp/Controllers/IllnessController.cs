using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EntityModels;
using ComunicationControllers;

namespace MediApp.Controllers
{
    public class IllnessController : ApiController
    {

        // GET api/Illness/5
        public IEnumerable<SharedModels.IllnessModel> Get(int id)
        {
            return WcfController.getIllness(id);
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

    }
}