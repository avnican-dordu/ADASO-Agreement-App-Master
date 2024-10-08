﻿using ADASO_AgreementApp.Filters;
using System.Web;
using System.Web.Mvc;

namespace ADASO_AgreementApp
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new AuthorizeAttribute());
        }
    }
}
