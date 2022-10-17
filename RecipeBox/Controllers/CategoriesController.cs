using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;
using RecipeBox.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace RecipeBox.Controllers
{
  public class CategoriesController : Controller
  {
    private readonly RecipeBoxContext _db;
    public CategoriesController(RecipeBoxContext db)
    {
      _db = db;
    }
    
    public ActionResult Index()
    {
      return View(_db.Categories.ToList());
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Category category)
    {
      _db.Categories.Add(category);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisCategory = _db.Categories
          .Include(category => category.JoinEntities)
          .ThenInclude(join => join.Recipe)
          .FirstOrDefault(category => category.CategoryId == id);
          return View(thisCategory);
    }

    [HttpPost]
    public ActionResult Delete(int id)
    {
      Category thisCategory = _db.Categories.FirstOrDefault(category => category.CategoryId == id);
      _db.Categories.Remove(thisCategory);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

  }
}