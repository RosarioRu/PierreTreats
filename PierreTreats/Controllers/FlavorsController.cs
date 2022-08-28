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
  public class FlavorsController : Controller
  {
    private readonly PierreTreatsContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public FlavorsController(UserManager<ApplicationUser> userManager, PierreTreatsContext db) 
    {
      _userManager = userManager;
      _db = db;
    }

    //below we are able to access all Flavor(s) in list form by usign LINQ ToList()
    //_db's value is db, which is an instance of DbContext class. It holds reference to the database.
    public ActionResult Index()
    {
      return View(_db.Flavors.ToList()); 
    } 

    [Authorize] 
    [HttpGet]
    public ActionResult Create()
    {
      return View();
    }

    [Authorize] 
    [HttpPost] 
    public ActionResult Create(Flavor flavorSubmittedByForm)
    {
      _db.Flavors.Add(flavorSubmittedByForm);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpGet] 
    public ActionResult Details(int id)
    {
      ViewBag.TreatId = new SelectList(_db.Treats, "TreatId", "TreatName");
      var thisFlavor = _db.Flavors
        .Include(Flavor => Flavor.JoinEntities)
        .ThenInclude(join => join.Treat)
        .FirstOrDefault(Flavor => Flavor.FlavorId == id);
      return View(thisFlavor);
    }

    [Authorize]
    [HttpGet]
    public ActionResult Delete(int id)
    {
      var thisFlavor = _db.Flavors.FirstOrDefault(Flavor => Flavor.FlavorId == id);
      return View(thisFlavor);
    }

    [Authorize]
    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var flavorToDelete = _db.Flavors.FirstOrDefault(Flavor => Flavor.FlavorId == id);
      _db.Flavors.Remove(flavorToDelete);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

  }
}