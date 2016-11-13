//------------------------------------------------------------------------------
// <copyright file="WebDataService.svc.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data.Services;
using System.Data.Services.Common;
using System.Data.Services.Providers;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Web;
using WcfService.DbContext;
using EntityModels;

namespace WcfService
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class WcfDataService : EntityFrameworkDataService<PatientsContext>
    {
        // This method is called only once to initialize service-wide policies.
        public static void InitializeService(DataServiceConfiguration config)
        {
            config.SetEntitySetAccessRule("*", EntitySetRights.All);
            config.SetServiceOperationAccessRule("*", ServiceOperationRights.All);
            config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V3;
        }
        
        // Methods to get data
        [WebGet]
        public IEnumerable<Illness> GetIllness()
        {
            return CurrentDataSource.TableIllness;
        }

        [WebGet(UriTemplate = "GetPatient")]
]
        public IEnumerable<Patient> GetPatient()
        {
            return CurrentDataSource.TablePatient;
        }

        [WebGet]
        public IEnumerable<Symptom> GetSymptom()
        {
            return CurrentDataSource.TableSymptom;
        }

        [WebGet]
        public IEnumerable<Question> GetQuestion()
        {
            return CurrentDataSource.TableQuestion;
        }

        [WebGet]
        public IEnumerable<Output> GetOutput()
        {
            return CurrentDataSource.TableOutput;
        }

        // Method to fill database
        [WebGet]
        public void fill()
        {
            FillDb fillDb = new FillDb();
        }
    }
}
