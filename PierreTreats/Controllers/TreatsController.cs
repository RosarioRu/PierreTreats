using Microsoft.AspNetCore.Mvc.Rendering; //need this for SelectList.
using Microsoft.AspNetCore.Mvc; 
using Microsoft.EntityFrameworkCore; 
using PierreTreats.Models;
using System.Collections.Generic;
using System.Linq; //this allows us to use ToList() 
using System;



namespace PierreTreats.Controllers
{
  public class TreatsController : Controller
  {
    private readonly PierreTreatsContext _db;

    public TreatsController(PierreTreatsContext db) 
    {
      _db = db;
    } 

    //below we are able to access all Treat(s) in list form by usign LINQ ToList()
    //_db's value is db, which is an instance of DbContext class. It holds reference to te database.
    public ActionResult Index()
    {
      return View(_db.Treats.ToList());
    }

    [HttpGet]
    public ActionResult Create()
    {
      ViewBag.FlavorId = new SelectList(_db.Flavors, "FlavorId", "FlavorName");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Treat treatSubmittedByForm, int FlavorId)
    {
      _db.Treats.Add(treatSubmittedByForm);
      _db.SaveChanges();
      if (FlavorId != 0)
      {
        _db.FlavorTreats.Add(new FlavorTreat() { FlavorId = FlavorId, TreatId = treatSubmittedByForm.TreatId });
        _db.SaveChanges();
      }
      return RedirectToAction("Index");
    }

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

    
  }
}
