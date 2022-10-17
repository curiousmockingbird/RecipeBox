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
  public class RecipesController : Controller
  {
    private readonly RecipeBoxContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public RecipesController(UserManager<ApplicationUser> userManager, RecipeBoxContext db)
    {
      _userManager = userManager;
      _db = db;
    }

		public async Task<ActionResult> Index()
		{
			var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			var currentUser = await _userManager.FindByIdAsync(userId);
			var userRecipes = _db.Recipes.Where(entry => entry.User.Id == currentUser.Id).ToList();
			return View(userRecipes);
		}

		public ActionResult Create()
		{
			ViewBag.CategoryId = new SelectList(_db.Categories, "CategoryId", "Name");
			return View();
		}

		[HttpPost]
		public async Task<ActionResult> Create(Recipe recipe, int CategoryId)
		{
			var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			var currentUser = await _userManager.FindByIdAsync(userId);
			recipe.User = currentUser;
			_db.Recipes.Add(recipe);
			_db.SaveChanges();
			if (CategoryId != 0)
			{
				_db.RecipeCategory.Add(new RecipeCategory() { CategoryId = CategoryId, RecipeId = recipe.RecipeId});
			}
			_db.SaveChanges();
			return RedirectToAction("Index");
		}

		public ActionResult Details(int id)
		{
			Recipe thisRecipe = _db.Recipes
				.Include(recipe => recipe.JoinEntities)
				.ThenInclude(join => join.Recipe)
				.FirstOrDefault(recipe => recipe.RecipeId == id);
			return View(thisRecipe);
		}

		public ActionResult Edit(int id)
		{
			Recipe thisRecipe = _db.Recipes.FirstOrDefault(recipe => recipe.RecipeId == id);
			ViewBag.CategoryId = new SelectList(_db.Categories, "CategoryId", "Name");
			return View(thisRecipe);
		}

		[HttpPost]
		public ActionResult Edit(Recipe recipe, int CategoryId)
		{
			_db.Entry(recipe).State = EntityState.Modified;
			_db.SaveChanges();
			if (CategoryId != 0)
			{
				_db.RecipeCategory.Add(new RecipeCategory() { CategoryId = CategoryId, RecipeId = recipe.RecipeId});
				_db.SaveChanges();
			}
			return RedirectToAction("Details", new {id = recipe.RecipeId});
		}

		[HttpPost]
		public ActionResult Delete(int id)
		{
			Recipe thisRecipe = _db.Recipes.FirstOrDefault(recipe => recipe.RecipeId == id);
			_db.Recipes.Remove(thisRecipe);
			_db.SaveChanges();
			return RedirectToAction("Index");
		}
  }
}


