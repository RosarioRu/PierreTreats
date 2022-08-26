using System;
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations; 

namespace PierreTreats.Models
{
  public class Flavor
  {

    public Flavor()
    {
      this.JoinEntities = new HashSet<FlavorTreat>();
    }

    [Display(Name = "Flavor Name")]
    public string FlavorName { get; set; }

    //more properties

    public virtual ICollection<FlavorTreat> JoinEntities { get; }
    
  }
}