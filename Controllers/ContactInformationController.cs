using ADASO_AgreementApp.Models.Entity;
using ADASO_AgreementApp.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ADASO_AgreementApp.Controllers
{
    [Authorize]
    public class ContactInformationController : Controller
    {
        ADASOEntities4 db = new ADASOEntities4();
        Class cs = new Class();

        public ActionResult Index()
        {
            var userId = GetUserIdFromCookie();

            // Kullanıcının sadece kendi maillerini al
            var maills = db.Maills.Where(m => m.AdminID.ToString() == userId).ToList();

            // Modeli güncelle
            cs.Maills = maills;

            return View(cs);
        }

        private string GetUserIdFromCookie()
        {
            var cookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (cookie != null)
            {
                var authTicket = FormsAuthentication.Decrypt(cookie.Value);
                return authTicket.Name; // Kullanıcı ID'si
            }
            return null; // Kullanıcı kimliği yoksa null döner
        }

        [HttpGet]
        public ActionResult AddContact()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddContact(Maill p)
        {
            var userId = GetUserIdFromCookie();
            p.AdminID = int.Parse(userId);
            db.Maills.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Update(Maill p) 
        {
            var item = db.Maills.Find(p.Id);
            item.Name = p.Name;
            item.Surname = p.Surname;
            item.PhoneNumber = p.PhoneNumber;
            item.MailAdress = p.MailAdress;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult GetContact(int id)
        {
            var item = db.Maills.Find(id);
            return View("GetContact", item);
        }


    }
}