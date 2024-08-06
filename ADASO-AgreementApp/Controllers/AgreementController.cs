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
        ADASOEntities1 db = new ADASOEntities1 ();
        public ActionResult Index()
        {
            return View();
        }
    }
}