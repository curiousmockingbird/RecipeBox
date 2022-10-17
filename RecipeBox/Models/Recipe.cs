using System.Collections.Generic;
using System;

namespace RecipeBox.Models
{
  public class Recipe
  {
    public Recipe()
  {
    this.JoinEntities = new HashSet<RecipeCategory>();
  }
  public int RecipeId { get; set; }
  public string Name { get; set; }
  public string Ingredients { get; set; }
  public string Directions { get; set; }
  public virtual ICollection<RecipeCategory> JoinEntities { get; set; }
  }
}