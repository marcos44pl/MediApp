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
using WcfService.Entities;

namespace WcfService
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class WcfDataService : EntityFrameworkDataService<PatientsContext>
    {
        // This method is called only once to initialize service-wide policies.
        public static void InitializeService(DataServiceConfiguration config)
        {
            config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V3;
        }
        
        // Sample method to get data
        [WebInvoke(Method = "POST")]
        public IQueryable<Illness> GetIllness()
        {
            return CurrentDataSource.TableIllness;
        }
        [WebGet]
        public int GetIdTest()
        {
            return 123;
        }
        [WebGet]
        public void GetTest()
        {
            int i = 5;
            i += 5;
        }
    }
}
