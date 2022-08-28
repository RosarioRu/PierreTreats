using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc; 
using Microsoft.AspNetCore.Mvc.Rendering; //need this for SelectList.
using Microsoft.EntityFrameworkCore; 
using PierreTreats.Models;
using System;
using System.Collections.Generic;
using System.Linq; //this allows us to use ToList() 
using System.Security.Claims;
using System.Threading.Tasks;



namespace PierreTreats.Controllers
{
  [Authorize]
  public class TreatsController : Controller
  {
    private readonly PierreTreatsContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public TreatsController(UserManager<ApplicationUser> userManager, PierreTreatsContext db) 
    {
      _userManager = userManager;
      _db = db;
    } 


    [AllowAnonymous]
    public async Task<ActionResult> Index()
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      var userTreats = _db.Treats.Where(entry => entry.User.Id == currentUser.Id).ToList();
      return View(userTreats);
    }

    [HttpGet]
    public ActionResult Create()
    {
      ViewBag.FlavorId = new SelectList(_db.Flavors, "FlavorId", "FlavorName");
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Treat Treat, int FlavorId)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      Treat.User = currentUser;
      _db.Treats.Add(Treat);
      _db.SaveChanges();
      if (FlavorId != 0)
      {
        _db.FlavorTreats.Add(new FlavorTreat() { FlavorId = FlavorId, TreatId = Treat.TreatId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
}

    [AllowAnonymous]
    [HttpGet]
    public ActionResult Details(int id)
    { 
      ViewBag.FlavorId = new SelectList(_db.Flavors, "FlavorId", "FlavorName");
      var thisTreat = _db.Treats
        .Include(Treat => Treat.JoinEntities)
        .ThenInclude(join => join.Flavor)
        .FirstOrDefault(Treat => Treat.TreatId == id);
      return View(thisTreat);
    }

    [HttpGet]
    public ActionResult Edit(int id)
    { 
      ViewBag.FlavorId = new SelectList(_db.Flavors, "FlavorId", "FlavorName");
      var treatToEdit = _db.Treats
        .Include(Treat => Treat.JoinEntities)
        .ThenInclude(join => join.Flavor)
        .FirstOrDefault(Treat => Treat.TreatId == id);
      return View(treatToEdit);
    }

    [HttpPost]
    public ActionResult Edit(Treat treatToEdit, int FlavorId)
    {
      if (FlavorId != 0)
      {
        _db.FlavorTreats.Add(new FlavorTreat() { FlavorId = FlavorId, TreatId = treatToEdit.TreatId});
      }
      _db.Entry(treatToEdit).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpGet]
    public ActionResult Delete(int id)
    {
      var thisTreat = _db.Treats.FirstOrDefault(Treat => Treat.TreatId == id);
      return View(thisTreat);
    }


    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var treatToDelete = _db.Treats.FirstOrDefault(Treat => Treat.TreatId == id);
      _db.Treats.Remove(treatToDelete);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost, ActionName("DeleteFlavorTreat")]
    public ActionResult DetailsAgain(int joinId)
    {
      var joinEntry = _db.FlavorTreats.FirstOrDefault(entry => entry.FlavorTreatId == joinId);
      _db.FlavorTreats.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }   

    
    
  }
}
