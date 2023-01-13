using WpfChantierApp1._2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfChantierApp1._2
{
    public  class SousTraitant : BindableBase { 

public int SousTraitantId { get; set; }
   

    private string domainSousTraitant = "";
    public string DomainSousTraitant
        {
        get
        {
            return this.domainSousTraitant;
        }

        set
        {
            if (value != this.domainSousTraitant)
            {
                this.domainSousTraitant = value;
                OnPropertyChanged();
            }
        }
    }
    public string OuvrageID { get; set; } = "";
    public DateTime Date_Debut { get; set; }
    public DateTime Date_Fin { get; set; }
        
}
}
