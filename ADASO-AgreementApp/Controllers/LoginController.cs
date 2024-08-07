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