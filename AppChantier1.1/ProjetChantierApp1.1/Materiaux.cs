//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProjetChantierApp1._1
{
    using System;
    using System.Collections.Generic;
    
    public partial class Materiaux
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Materiaux()
        {
            this.Fournirs = new HashSet<Fournir>();
        }
    
        public int MateriauxID { get; set; }
        public string NomMateriaux { get; set; }
        public string DateReception { get; set; }
        public Nullable<int> OuvrageID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Fournir> Fournirs { get; set; }
        public virtual Ouvrage Ouvrage { get; set; }
    }
}
