using Microsoft.AspNetCore.Identity;
using System;

namespace RecipeBox.Models
{
  public class ApplicationUser : IdentityUser
  {
    // [PersonalData]
    public string ? FullName { get; set; }
    // [PersonalData]
    public DateTime DOB { get; set; }
  }
}