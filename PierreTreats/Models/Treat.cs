using System;
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations; 

namespace PierreTreats.Models
{
  public class Treat
  {

    public Treat()
    {
      this.JoinEntities = new HashSet<FlavorTreat>();
    }

    [Display(Name = "Treat Name")]
    public string TreatName { get; set; }

    //more properties

    public virtual ICollection<FlavorTreat> JoinEntities { get; }
    
  }
}
