namespace ADASO_AgreementApp.Helper
{

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using ADASO_AgreementApp.Models.Entity;


    public static class UserHelper
    {
        public static void SetCurrentUser(Adminn user)
        {
            HttpContext.Current.Session["CurrentUser"] = user;
        }

        public static Adminn GetCurrentUser()
        {
            return HttpContext.Current.Session["CurrentUser"] as Adminn;
        }
    }

}