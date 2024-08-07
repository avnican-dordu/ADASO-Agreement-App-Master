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
        ADASOEntities2 db = new ADASOEntities2();
        public ActionResult Index()
        {
            var list = db.TBLAgreement.ToList();
            return View(list);
        }

        public ActionResult Guncelle(TBLAgreement p)
        {
            var soz = db.TBLAgreement.Find(p.AgreementNo);
            soz.AgrTitle = p.AgrTitle;
            soz.AgrContent = p.AgrContent;
            soz.AgrFinishDate = p.AgrFinishDate;
            soz.AgrStartDate = p.AgrStartDate;
            soz.Image = p.Image;
            soz.CompanyName = p.CompanyName;
            soz.AdminID = p.AdminID;
            //var admin = db.TBLAdmin.Where(m => m.AdminID == p.TBLAdmin.AdminID).FirstOrDefault();
            //soz.AdminID = admin.AdminID;
            soz.Email = p.Email;

            db.SaveChanges();
            return RedirectToAction("Index");


        }
    }
}