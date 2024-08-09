using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ADASO_AgreementApp.Models.Entity;
using ADASO_AgreementApp.Models.ViewModel;
using System.Web.Security;

namespace ADASO_AgreementApp.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        ADASOEntities3 db = new ADASOEntities3();
        Class cs = new Class();

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Adminn a)
        {
            Class admin = new Class();
            var admininfo = admin.Admins.FirstOrDefault(m => m.Mail == a.Mail && m.Password == a.Password);
            if (admininfo == null)
            {
                FormsAuthentication.SetAuthCookie(admininfo.Mail, false);
                // Kullanıcı bilgilerini session'a kaydet
                Session["Id"] = admininfo.Id;
                Session["Email"] = admininfo.Mail;
                Session["Password"] = admininfo.Password;

                return RedirectToAction("Index","Admin");
            }
            else
            {
                ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı");
                return RedirectToAction("Index");
            }
            //return View();
        }

        // Çıkış
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            Session.Clear(); // Tüm oturum değişkenlerini temizle
            Session.Abandon(); // Oturumu sonlandır

            return RedirectToAction("Login", "Login");
        }
    }
}