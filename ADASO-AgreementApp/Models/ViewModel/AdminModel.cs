using ADASO_AgreementApp.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADASO_AgreementApp.Models.ViewModel
{
    public class AdminModel
    {
        public IEnumerable<TBLAdmin> Admins { get; set; }
    }
}