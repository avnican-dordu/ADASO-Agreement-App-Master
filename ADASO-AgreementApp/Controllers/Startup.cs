using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.Owin;
using Owin;
using ADASO_AgreementApp.Controllers;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Data.Entity.Core.EntityClient;

[assembly: OwinStartup(typeof(ADASO_AgreementApp.Startup))]

namespace ADASO_AgreementApp
{
   
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Hangfire'ı yapılandırıyoruz
            GlobalConfiguration.Configuration
                .UseSqlServerStorage("HangfireConnection");

            app.UseHangfireDashboard();
            app.UseHangfireServer();

            // Zamanlanmış görevi tanımlıyoruz (her gün çalışacak şekilde)
            RecurringJob.AddOrUpdate<AgreementController>(
                "notify-expiring-contracts",  // İş kimliği
                job => job.NotifyExpiringContracts(),  // Çağırılacak yöntem
                Cron.Daily,  // Günlük olarak çalışacak şekilde zamanlanıyor
                new RecurringJobOptions
                {
                    TimeZone = TimeZoneInfo.Local // Yerel saat dilimi
                                                  // Diğer RecurringJobOptions ayarlarını da buraya ekleyebilirsiniz
                });
        }
    }
    }

//Artık Hangfire'ı OWIN pipeline'ı ile birlikte başlatmış oldunuz. Startup sınıfında, Hangfire Dashboard'u ve Server'ı yapılandırdık, ve her gün çalışacak şekilde zamanlanmış bir görevi tanımladık.
