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
        ADASOEntities3 db = new ADASOEntities3();
        Class cs = new Class();

        //[Authorize]
        public ActionResult Index()
        {
            var list = db.Adminn.ToList();
            return View(list);
        }
        [HttpGet]
        public ActionResult KullanıcıGetir(int id)
        {
            var item = db.Adminn.Find(id);
            return View("KullanıcıGetir", item);

        }

        //[HttpPost]
        public ActionResult Guncelle(Adminn p)
        {
            var admin = db.Adminn.Find(p.Id);
            admin.Name = p.Name;
            admin.Surname = p.Surname;
            admin.Tel = p.Tel;
            admin.Mail = p.Mail;
            admin.TC = p.TC;
            admin.Image = p.Image;
            admin.Role = p.Role;
            admin.Password = p.Password;

            db.SaveChanges();

            return RedirectToAction("Index");

        }
    }
}