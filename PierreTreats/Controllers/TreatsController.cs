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
    
  }
}
