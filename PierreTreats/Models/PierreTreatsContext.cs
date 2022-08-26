using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PierreTreats.Models
{
  public class PierreTreatsContext : IdentityDbContext<ApplicationUser>
  {
    public DbSet<Flavor> Flavors { get; set; }
    public DbSet<Treat> Treats { get; set; }
    public DbSet<FlavorTreat> FlavorTreats {get; set; }

    public PierreTreatsContext(DbContextOptions options) : base(options) { }
    
    //below OnConfiguing enables lazy-loading
    protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseLazyLoadingProxies();
    }
  }
}