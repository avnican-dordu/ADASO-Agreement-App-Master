using ADASO_AgreementApp.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ADASO_AgreementApp.Controllers
{
    public class AgreementController : Controller
    {
        // GET: Agreement
        ADASOEntities3 db = new ADASOEntities3();
        public ActionResult Index()
        {
            var list = db.Agreementt.ToList();
            return View(list);
        }

        public ActionResult Guncelle(Agreementt p)
        {
            var soz = db.Agreementt.Find(p.AgreementNo);
            soz.Title = p.Title;
            soz.Content = p.Content;
            soz.EndDate = p.EndDate;
            soz.StartDate = p.StartDate;
            soz.File = p.File;
            soz.CompanyName = p.CompanyName;
            //soz.AdminID = p.AdminID;
            //var admin = db.TBLAdmin.Where(m => m.AdminID == p.TBLAdmin.AdminID).FirstOrDefault();
            //soz.AdminID = admin.AdminID;
            soz.Email = p.Email;

            db.SaveChanges();
            return RedirectToAction("Index");


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
            return View();
        }


    }
}