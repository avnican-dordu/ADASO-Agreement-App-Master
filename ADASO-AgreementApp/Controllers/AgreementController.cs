using ADASO_AgreementApp.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace ADASO_AgreementApp.Controllers
{
    public class AgreementController : Controller
    {
        // GET: Agreement
        ADASOEntities3 db = new ADASOEntities3();
        private readonly EmailService _emailService;


        public AgreementController()
        {
            // EmailService nesnesini SMTP ayarlarıyla başlatıyoruz
           _emailService = new EmailService("smtp.gmail.com", 587, "gulserenzamir@gmail.com", "123456");
        }

        public ActionResult NotifyExpiringContracts()
        {
            using (var context = new ADASOEntities3())
            {
                // Bugünün tarihini alıyoruz
                var today = DateTime.Today;

                // Sözleşme bitiş tarihine 15 gün kalanları sorguluyoruz
                var upcomingContracts = context.Agreementt
                    .Where(c => DbFunctions.DiffMinutes(today, c.EndDate) == 1)
                    .ToList();

                // Her bir sözleşme için e-posta gönderiyoruz
                foreach (var contract in upcomingContracts)
                {
                    var emailBody = $"Sevgili Müşterimiz,  {contract.Title} adlı sözleşmenizin bitmesine {contract.EndDate} gün kaldı. Hatırlatmak ister iyi günler dileriz.";
                    _emailService.SendMail(contract.Email, "Contract Expiration Notice", emailBody);
                }
            }

            // İşlem sonrası yönlendirme veya bildirim
            return View("Index");
        }
        public ActionResult Index()
        {
            
            var list = db.Agreementt.ToList();
            foreach (var agreement in list)
            {
                if (agreement.EndDate < DateTime.Today)
                {
                    agreement.Status = "Pasif"; // Bitiş tarihi geçmişse Pasif
                }
                else
                {
                    agreement.Status = "Aktif"; // Aksi takdirde Aktif
                }
            }
            return View(list);
        }

        public ActionResult Guncelle(Agreementt p)
        {
            var item = db.Agreementt.Find(p.Id);
            item.Title = p.Title;
            item.Content = p.Content;
            item.EndDate = p.EndDate;
            item.StartDate = p.StartDate;
            item.File = p.File;
            item.CompanyName = p.CompanyName;
            item.Email = p.Email;

            db.SaveChanges();
            return RedirectToAction("Index");
}
        public ActionResult SozlesmeGetir(int Id)
        {
            var item = db.Agreementt.Find(Id);
            return View("SozlesmeGetir", item);

        }
        // yeni sözleşme ekleme
        [HttpGet]
        public ActionResult NewAgrement()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NewAgrement(Agreementt p1)
        {
            db.Agreementt.Add(p1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}