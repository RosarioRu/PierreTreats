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

    [Display(Name = "ID")]
    public int FlavorId { get; set; }

    [Display(Name = "Flavor Name")]
    public string FlavorName { get; set; }

    public virtual ApplicationUser User { get; set; }

    public virtual ICollection<FlavorTreat> JoinEntities { get; set; }
    
  }
}