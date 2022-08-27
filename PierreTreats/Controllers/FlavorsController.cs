using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq; 
using PierreTreats.Models;
using Microsoft.EntityFrameworkCore; 

namespace PierreTreats.Controllers
{
  public class FlavorsController : Controller
  {
    
    private readonly PierreTreatsContext _db;

    public FlavorsController(PierreTreatsContext db) 
    {
      _db = db;
    }

    //below we are able to access all Flavor(s) in list form by usign LINQ ToList()
    //_db's value is db, which is an instance of DbContext class. It holds reference to the database.
    public ActionResult Index()
    {
      return View(_db.Flavors.ToList()); 
    } 

    [HttpGet]
    public ActionResult Create()
    {
      return View();
    }

  }
}