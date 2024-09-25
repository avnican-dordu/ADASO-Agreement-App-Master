using ADASO_AgreementApp.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADASO_AgreementApp.Models.ViewModel
{
    public class MailModel
    {
        public IEnumerable<Maill> Maills { get; set; }
        public IEnumerable<Agreementt> Agreements { get; set; }
        public int AId { get; set; }
        public IEnumerable<Maill> MailList { get; set; }
        public Maill NewMail { get; set; }
    }
}