using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ADASO_AgreementApp.Models.Entity;
using ADASO_AgreementApp.Models.ViewModel;
using System.Web.Security;
using System.Net.Mail;
using System.Net;
using ADASO_AgreementApp.Helper;

namespace ADASO_AgreementApp.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        // GET: Login
        ADASOEntities4 db = new ADASOEntities4();

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string Mail, string Password, bool RememberMe = false)
        {
            var user = db.Adminns.FirstOrDefault(m => m.Mail == Mail && m.Password == Password);

            if (user != null)
            {
                // RememberMe parametresine göre çerez süresini ayarlıyoruz
                FormsAuthentication.SetAuthCookie(user.Id.ToString(), RememberMe);

                // Kullanıcı oturum bilgilerini saklayın
                UserHelper.SetCurrentUser(user);
                Session["currentUser"] = user;
                    return RedirectToAction("Index", "Admin");
                
            }
            else
            {
                TempData["Error"] = "Kullanıcı adı veya şifre hatalı.";
                return View("Login");
            }
        }



        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(Adminn model)
        {
            if (ModelState.IsValid)
            {
                var user = db.Adminns.FirstOrDefault(x => x.Mail == model.Mail);
                if (user != null)
                {
                    string newPassword = Guid.NewGuid().ToString().Substring(0, 8);

                    // Password hashleme (örnek olarak düz metin)
                    user.Password = newPassword;  // Şifreleme yapmayı unutmayın.
                    db.SaveChanges();

                    string Name = user.Name; 
                    string Surname = user.Surname;
                    string emailBody = $@"
<html lang=""tr"">
<head>
    <meta charset=""utf-8"" />
    <style type=""text/css"">
        .rollover:hover.rollover-first {{
            max-height: 0px!important;
            display: none!important;
        }}
        .rollover:hover.rollover-second {{
            max-height: none!important;
            display: block!important;
        }}
        .rollover span {{
            font-size: 0px;
        }}
        u + .body img ~div div {{
            display: none;
        }}
        #outlook a {{
            padding: 0;
        }}
        span.MsoHyperlink,
        span.MsoHyperlinkFollowed {{
            color: inherit;
        }}
        a.es-button {{
            text-decoration: none!important;
        }}
        a[x-apple-data-detectors],
        #MessageViewBody a {{
            color: inherit!important;
            text-decoration: none!important;
            font-size: inherit!important;
            font-family: inherit!important;
            font-weight: inherit!important;
            line-height: inherit!important;
        }}
        .es-desk-hidden {{
            display: none;
            float: left;
            overflow: hidden;
            width: 0;
            max-height: 0;
            line-height: 0;
        }}
        .es-button-border:hover {{
            border-color: #3d5ca3 !important;
            background: #ffffff !important;
        }}
        .es-button-border:hover a.es-button,
        .es-button-border:hover button.es-button {{
            background: #ffffff !important;
        }}
        @media only screen and (max-width:600px) {{
            .es-m-p20b {{
                padding-bottom: 20px!important;
            }}
            .es-m-p0l {{
                padding-left: 0px!important;
            }}
            .es-m-txt-c {{
                text-align: center!important;
            }}
            a.es-button, button.es-button {{
                font-size: 14px!important;
                padding: 10px 20px!important;
                line-height: 120% !important;
            }}
            .es-header-body, .es-content-body, .es-footer-body {{
                max-width: 600px!important;
            }}
            .adapt-img {{
                width: 100% !important;
                height: auto!important;
            }}
            .es-mobile-hidden, .es-hidden {{
                display: none!important;
            }}
            .es-desk-hidden {{
                width: auto!important;
                overflow: visible!important;
                float: none!important;
                max-height: inherit!important;
                line-height: inherit!important;
            }}
        }}
        @media screen and (max-width:384px) {{
            .mail-message-content {{
                width: 414px!important;
            }}
        }}
    </style>
</head>
<body style=""width:100%;height:100%;padding:0;margin:0;background-color:#FAFAFA"">
    <div class=""es-wrapper-color"" style=""background-color:#FAFAFA"">
        <table width=""100%"" cellspacing=""0"" cellpadding=""0"" style=""width:100%;height:100%;background-color:#FAFAFA"">
            <tr>
                <td valign=""top"" style=""padding:0;margin:0"">
                    <table align=""center"" style=""background-color:transparent;width:600px;"">
                        <tr>
                            <td align=""center"" style=""padding:20px;background-color:#3d5ca3"">
                                <img src=""http://www.adaso.org.tr/Content/Files/fileMenager/2022/2//ADASO_Logo_Alternatif_1.png"" alt=""ADASO Logo"" width=""183"" style=""display:block;border:0;"" />
                            </td>
                        </tr>
                    </table>
                    <table align=""center"" style=""background-color:#fafafa;width:600px;"">
                        <tr>
                            <td align=""center"" style=""background-color:#ffffff;padding:40px 20px 0;"">
                                <img src=""https://kmolid.stripocdn.email/content/guids/CABINET_dd354a98a803b60e2f0411e893c82f56/images/23891556799905703.png"" alt=""Icon"" width=""175"" style=""display:block;border:0;"" />
                                <h1 style=""font-family:arial, 'helvetica neue', helvetica, sans-serif;font-size:20px;line-height:24px;color:#333333;""><strong>ŞİFRE SIFIRLAMA</strong></h1>
                                <p style=""font-family:helvetica, 'helvetica neue', arial, verdana, sans-serif;line-height:24px;color:#666666;font-size:16px;text-align:center;"">Merhaba,<b> {Name} {Surname}</b> </p>
                                <p style=""font-family:helvetica, 'helvetica neue', arial, verdana, sans-serif;line-height:24px;color:#666666;font-size:16px;text-align:center;"">Şifrenizi değiştirme talebinde bulundunuz!</p>
                                <p style=""font-family:helvetica, 'helvetica neue', arial, verdana, sans-serif;line-height:24px;color:#666666;font-size:16px;text-align:center;"">Yeni Şifreniz : <b> {newPassword} </b></p>
                                <p style=""font-family:helvetica, 'helvetica neue', arial, verdana, sans-serif;line-height:24px;color:#666666;font-size:16px;text-align:center;"">Eğer bu talebi siz yapmadıysanız, lütfen bu e-postayı dikkate almayın.</p>
                            </td>
                        </tr>
<tr>
                            <td align=""center"" style=""padding:20px;background-color:#3d5ca3"">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</body>
</html>";

                    try
                    {
                        MailMessage mail = new MailMessage();
                        mail.From = new MailAddress("adasoexample@gmail.com");
                        mail.To.Add(model.Mail);
                        mail.Subject = "Şifre Sıfırlama";
                        mail.Body = emailBody;
                        mail.IsBodyHtml = true;

                        SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                        smtp.Credentials = new NetworkCredential("adasoexample@gmail.com", "ocac xtal zyrn wmwd");
                        smtp.EnableSsl = true;

                        smtp.Send(mail);

                        TempData["Message"] = "Şifre sıfırlama e-postası gönderildi.";
                    }
                    catch (SmtpException smtpEx)
                    {
                        TempData["Error"] = $"E-posta gönderme hatası (SMTP): {smtpEx.Message}";
                    }
                    catch (Exception ex)
                    {
                        TempData["Error"] = $"E-posta gönderme hatası: {ex.Message}";
                    }

                    return RedirectToAction("Login");
                }
                else
                {
                    TempData["Error"] = "Bu e-posta adresi sistemde bulunamadı.";
                    return View(model);
                }
            }
            else
            {
                TempData["Error"] = "Geçerli bir e-posta adresi giriniz.";
                return View(model);
            }
        }
    }
}