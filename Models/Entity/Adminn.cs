//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ADASO_AgreementApp.Models.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class Adminn
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Adminn()
        {
            this.Agreementts = new HashSet<Agreementt>();
            this.Maills = new HashSet<Maill>();
            this.AgreementMaills = new HashSet<AgreementMaill>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Tel { get; set; }
        public string Mail { get; set; }
        public string TC { get; set; }
        public string Image { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Agreementt> Agreementts { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Maill> Maills { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AgreementMaill> AgreementMaills { get; set; }
    }
}
