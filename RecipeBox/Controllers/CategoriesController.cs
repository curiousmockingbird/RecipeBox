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
  [Authorize]
  public class CategoriesController : Controller
  {
    private readonly RecipeBoxContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public CategoriesController(UserManager<ApplicationUser> userManager, RecipeBoxContext db)
    {
      _userManager = userManager;
      _db = db;
    }
    
    public async Task<ActionResult> Index()
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      var userCategories = _db.Categories.Where(entry => entry.User.Id == currentUser.Id).ToList();
      return View(userCategories);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Category category)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			var currentUser = await _userManager.FindByIdAsync(userId);
			category.User = currentUser;

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