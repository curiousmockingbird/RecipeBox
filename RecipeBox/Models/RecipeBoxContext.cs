using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RecipeBox.Models
{
	public class RecipeBoxContext : IdentityDbContext<ApplicationUser>
	{
		public DbSet<Recipe> Recipes {get; set;}
		public DbSet<Category> Categories {get; set;}
		public DbSet<RecipeCategory> RecipeCategory {get; set;}

		public RecipeBoxContext (DbContextOptions options) : base(options) {}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseLazyLoadingProxies();
		}
	}
}
