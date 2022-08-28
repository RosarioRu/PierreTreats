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

    [Display(Name = "Id")]
    public int TreatId { get; set; }

    [Display(Name = "Treat Name")]
    public string TreatName { get; set; }

    [Display(Name = "Price")]
    public int TreatPrice { get; set; }

    public virtual ApplicationUser User { get; set; }


    public virtual ICollection<FlavorTreat> JoinEntities { get; set; }
    
  }
}
