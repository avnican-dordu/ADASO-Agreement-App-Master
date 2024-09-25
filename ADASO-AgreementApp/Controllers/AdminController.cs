using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ADASO_AgreementApp.Helper;
using ADASO_AgreementApp.Models.Entity;
using ADASO_AgreementApp.Models.ViewModel;

namespace ADASO_AgreementApp.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        // GET: Admin
        ADASOEntities4 db = new ADASOEntities4();
        Class cs = new Class();


        public ActionResult Index()
        {
            var model = new Class(); // Class modelini oluştur
            var currentUser = UserHelper.GetCurrentUser();

            if (currentUser.Role == "Admin")
            {
                model.Admins = db.Adminns.ToList(); // Admin için tüm kullanıcıları al
            }
            else
            {
                model.Admins = new List<Adminn> { currentUser }; // Normal kullanıcı için kendi bilgilerini al
            }

            return View(model); // Her iki durumda da Class modelini döndür
        }



        public ActionResult Guncelle(Adminn p)
        {
            var currentUser = UserHelper.GetCurrentUser();

            if (currentUser.Role == "Admin" || currentUser.Id == p.Id)
            {
                var item = db.Adminns.Find(p.Id);
                item.Name = p.Name;
                item.Surname = p.Surname;
                item.Tel = p.Tel;
                item.Mail = p.Mail;
                item.TC = p.TC;
                item.Role = p.Role;
                item.Password = p.Password;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return new HttpStatusCodeResult(HttpStatusCode.Forbidden); // Yetkisiz erişim
        }


        public ActionResult KullanıcıGetir(int id)
        {
            var currentUser = UserHelper.GetCurrentUser(); // Giriş yapan kullanıcıyı al
            var userToEdit = db.Adminns.Find(id); // Düzenlenecek kullanıcıyı al

            if (userToEdit == null)
            {
                return HttpNotFound(); // Kullanıcı bulunamazsa 404 dön
            }

            // Eğer giriş yapan kullanıcı admin değilse ve başkasının bilgilerini düzenlemeye çalışıyorsa
            if (currentUser.Role != "Admin" && currentUser.Id != id)
            {
                return new HttpUnauthorizedResult("Yetkiniz yok"); // Yetki yok hatası
            }

            return View("KullanıcıGetir", userToEdit); // Yetkili ise kullanıcıyı getir
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult CreateUser() { return View(); }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult CreateUser(Adminn p)
        {
            db.Adminns.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear(); 
            Session.Abandon();
            return RedirectToAction("Login", "Login");
        }

    }
}