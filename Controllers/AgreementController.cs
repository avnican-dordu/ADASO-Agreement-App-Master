using ADASO_AgreementApp.Models.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using ADASO_AgreementApp.Models.ViewModel;
using System.Data.Entity;
using System.Security.Claims;
using System.Web.Security;
using ADASO_AgreementApp.Helper;

namespace ADASO_AgreementApp.Controllers
{
    [Authorize]
    public class AgreementController : Controller
    {
        // GET: Agreement
        ADASOEntities4 db = new ADASOEntities4();
        Class cs =new Class();


        public ActionResult Index()
        {
            var userId = GetUserIdFromCookie();
            var agreements = db.Agreementts.Where(a => a.AdminID.ToString() == userId).ToList();

            var maills = db.Maills.ToList();

            var viewModel = new Class
            {
                Agreements = agreements,
                Maills = maills 
            };

            return View(viewModel);
        }


        
        private string GetUserIdFromCookie()
        {
            var cookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (cookie != null)
            {
                var authTicket = FormsAuthentication.Decrypt(cookie.Value);
                return authTicket.Name; 
            }
            return null; 
        }

        public ActionResult Guncelle(Agreementt p, string FilePath)
        {
            var item = db.Agreementts.Find(p.Id);
            item.Title = p.Title;
            item.Content = p.Content;
            item.EndDate = p.EndDate;
            item.StartDate = p.StartDate;
            item.File = p.File;
            item.CompanyName = p.CompanyName;
            item.Email = p.Email;
            item.Status = p.Status;

          

            db.SaveChanges();
            return RedirectToAction("Index");
}
        public ActionResult SozlesmeGetir(int id)
        {
            var currentUser = UserHelper.GetCurrentUser();

            // ID ile sözleşmeyi bul
            var item = db.Agreementts.Find(id);

            // Eğer sözleşme yoksa veya kullanıcı yetkisi yoksa
            if (item == null || item.AdminID != currentUser.Id)
            {
                return new HttpUnauthorizedResult("Yetkiniz yok");
            }

            return View("SozlesmeGetir", item);
        }

        // yeni sözleşme ekleme
        [HttpGet]
        public ActionResult NewAgrement()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NewAgrement(Agreementt p1)
        {
            var userId = GetUserIdFromCookie();
            p1.AdminID = int.Parse(userId);
            p1.Status = "Aktif";
            db.Agreementts.Add(p1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ReNewAgreement(Agreementt a)
        {
            if (ModelState.IsValid)
            {
                var existingAgreement = db.Agreementts.Find(a.Id);
                if (existingAgreement != null)
                {
                    existingAgreement.Title = a.Title;
                    existingAgreement.Content = a.Content;
                    existingAgreement.StartDate = a.StartDate;
                    existingAgreement.EndDate = a.EndDate;
                    existingAgreement.CompanyName = a.CompanyName;
                    existingAgreement.Email = a.Email;
                    a.Status = "Aktif";
                    existingAgreement.AdminID = a.AdminID;
                    db.Agreementts.Add(a);
                    db.SaveChanges();
                }

                TempData["Message"] = "Sözleşme başarıyla yenilendi.";
                return RedirectToAction("Index");
            }

            // Hata durumunda aynı sayfaya geri dön
            return View("Index");
        }

        public ActionResult UpdateUsers(int agreementId)
        {
            
            var selectedMaills = db.AgreementMaills
                .Where(am => am.AgreementID == agreementId)
                .Select(am => am.MaillID)
                .ToList();

            
            var availableMaills = db.Maills.ToList();

            
            var model = new Class
            {
                AgreementMaills = db.AgreementMaills.Where(am => am.AgreementID == agreementId).ToList(),
                Maills = availableMaills,
                Agreements = db.Agreementts.Where(a => a.Id == agreementId).ToList()
            };

            return PartialView("_UpdateUsersModal", model);
        }

        [HttpPost]
        public ActionResult UpdateUsers(int agreementId, List<int?> selectedUsers)
        {
            
            var currentUsers = db.AgreementMaills
                .Where(am => am.AgreementID == agreementId)
                .Select(am => am.MaillID)
                .ToList();

            
            var selectedUsersNullable = selectedUsers.Select(id => (int?)id).ToList();

            
            var usersToRemove = currentUsers.Except(selectedUsersNullable).ToList();
            foreach (var userId in usersToRemove)
            {
                var agreementMail = db.AgreementMaills
                    .FirstOrDefault(am => am.AgreementID == agreementId && am.MaillID == userId);
                if (agreementMail != null)
                {
                    db.AgreementMaills.Remove(agreementMail);
                }
            }

            
            var usersToAdd = selectedUsersNullable.Except(currentUsers).ToList();
            foreach (var userId in usersToAdd)
            {
                if (userId.HasValue) 
                {
                    db.AgreementMaills.Add(new AgreementMaill
                    {
                        AgreementID = agreementId,
                        MaillID = userId.Value
                    });
                }
            }

            db.SaveChanges();
            return Json(new { success = true });
        }



        public JsonResult GetSelectedUsers(int agreementId)
        {
            try
            {
                // Seçili kullanıcıları al
                var selectedUsers = db.AgreementMaills
                    .Where(am => am.AgreementID == agreementId)
                    .Select(am => new
                    {
                        id = am.MaillID,
                        nameSurname = am.Maill.Name + " " + am.Maill.Surname
                    })
                    .ToList();

                
                var currentUser = (Adminn)Session["currentUser"]; 
                var availableUsers = db.Maills
                    .Where(m => m.AdminID == currentUser.Id) 
                    .Select(u => new
                    {
                        id = u.Id,
                        nameSurname = u.Name + " " + u.Surname
                    })
                    .ToList();

                return Json(new { selectedUsers, availableUsers }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in GetSelectedUsers: " + ex.Message);
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult ToggleStatus(int id, string status)
        {
            var agreement = db.Agreementts.Find(id);
            if (agreement != null)
            {
                agreement.Status = status;
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }



















    }
}