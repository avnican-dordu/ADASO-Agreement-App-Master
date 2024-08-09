using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Hangfire;

namespace ADASO_AgreementApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

          
            //GlobalConfiguration.Configuration.UseSqlServerStorage("DefaultConnection");

            //// Hangfire'� ba�lat�yoruz
            //app.UseHangfireDashboard();
            //app.UseHangfireServer();

            //// Zamanlanm�� g�revi tan�ml�yoruz (her g�n �al��acak �ekilde)
            //RecurringJob.AddOrUpdate<AgreementController>(job => job.NotifyExpiringContracts(), Cron.Daily);
        }
    }
}
