﻿using ADASO_AgreementApp.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADASO_AgreementApp.Models.ViewModel
{
    public class Class
    {
        public IEnumerable<Adminn> Admins { get; set; }
        public IEnumerable<Agreementt> Agreements { get; set; }
    }
}