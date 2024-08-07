using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ADASO_AgreementApp.Models.Entity;
using ADASO_AgreementApp.Models.ViewModel;

namespace ADASO_AgreementApp.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        ADASOEntities2 db = new ADASOEntities2();
        Class cs = new Class();

        [Authorize]
        public ActionResult Index()
        {
            var list = db.TBLAdmin.ToList();
            return View(list);
        }

        public ActionResult Guncelle(TBLAdmin p)
        {
            var admin = db.TBLAdmin.Find(p.AdminID);
            admin.AdminName = p.AdminName;
            admin.AdminSurname = p.AdminSurname;
            admin.Tel = p.Tel;
            admin.AdminMail = p.AdminMail;
            admin.TC = p.TC;
            admin.Image = p.Image;
            //admin.AgreementNo = p.AgreementNo;
            var sozlesme = db.TBLAgreement.Where(m => m.AgreementNo == p.TBLAgreement.AgreementNo).FirstOrDefault();
            admin.AgreementNo = sozlesme.AgreementNo;
            admin.Role = p.Role;
            admin.Password = p.Password;

            db.SaveChanges();

            return RedirectToAction("Index");

        }
    }
}