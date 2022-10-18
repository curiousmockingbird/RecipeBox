using System;

namespace RecipeBox.ViewModels
{
  public class LoginViewModel
  {
    public string UserName { get; set; }
    public string Password { get; set; }
    public virtual bool LockoutEnabled {get; set;}
    public virtual DateTimeOffset? LockoutEnd {get; set;}
    public virtual int AccessFailedCount {get; set;}
  }
}