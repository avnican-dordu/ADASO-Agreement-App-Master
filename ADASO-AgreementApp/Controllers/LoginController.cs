using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ADASO_AgreementApp.Models.Entity;
using ADASO_AgreementApp.Models.ViewModel;

namespace ADASO_AgreementApp.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        ADASOEntities2 db = new ADASOEntities2();
        Class cs = new Class();

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(TBLAdmin a)
        {
            Class admin = new Class();
            var admininfo = admin.Admins.FirstOrDefault(m => m.AdminMail == a.AdminMail && m.Password == a.Password);
            if (admininfo == null)
            {
                return RedirectToAction("Index","Admin");
            }
            else
            {
                return RedirectToAction("Index");
            }
            //return View();
        }
    }
}