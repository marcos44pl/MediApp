//------------------------------------------------------------------------------
// <copyright file="WebDataService.svc.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data.Services;
using System.Data.Services.Common;
using System.Linq;
using System.ServiceModel.Web;
using System.Web;
using WcfService.Entities;

namespace WcfService
{
    public class WcfDataService : DataService<PatientsContext>
    {
        // This method is called only once to initialize service-wide policies.
        public static void InitializeService(DataServiceConfiguration config)
        {
            config.SetEntitySetAccessRule("Illness", EntitySetRights.All);
            config.SetEntitySetAccessRule("IllnessHasSymptom", EntitySetRights.All);
            config.SetEntitySetAccessRule("Patient", EntitySetRights.All);
            config.SetEntitySetAccessRule("PatientWasSick", EntitySetRights.All);
            config.SetEntitySetAccessRule("LifeFuncMeasure", EntitySetRights.All);
            config.SetEntitySetAccessRule("Symptom", EntitySetRights.All);

            config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V3;
        }
        // Sample method to get data
        [WebGet()]
        public IEnumerable<Illness> GetIllness(int id)
        {
            return CurrentDataSource.TableIllness.Where(illness => illness.IllnessId == id);
        }

    }
}
