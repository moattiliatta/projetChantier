//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WpfChantierApp1._2
{
    using System;
    using System.Collections.Generic;
    
    public partial class Sous_Traitant
    {
        public int SousTraitantID { get; set; }
        public string DomainSousTraitant { get; set; }
        public Nullable<int> OuvrageID { get; set; }
        public string Date_Debut_SousTraitant { get; set; }
        public string Date_Fin_SousTraitant { get; set; }
    
        public virtual Ouvrage Ouvrage { get; set; }
    }
}
