using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ADASO_AgreementApp.Models.Entity;

namespace ADASO_AgreementApp.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        ADASOEntities1 db = new ADASOEntities1();
        public ActionResult Index()
        {
            return View();
        }
    }
}