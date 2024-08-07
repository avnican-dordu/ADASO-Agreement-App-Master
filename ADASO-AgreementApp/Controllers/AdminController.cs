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
            return View();
        }
    }
}