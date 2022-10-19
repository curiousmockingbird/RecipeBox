# Recipe Box

### Created by Shaniza Lingle, Harold Mesa, and Noah Lundquist.

## Description

An interface for storing recipes. Has login functionality with unique Categories/Recipes for individual users, ability to search by listed ingredient, ability to rate and sort by rating and name, and other lil nice bits. CSS styling is basic at the time of this writing but that may change.

## Features

* Stores Categories and Recipes in tables.
* Allows for creation of user accounts.
* Has search, sort, and other functionalities.
* Full CRUD implementation for recipes.


## Technologies Used

* Built in VS Code (v.1.70.1) using the following languages:
	* C#
	* HTML
	* CSS
	* Bootstrap

	* ASP.Net Core
	* MySQL Database
	* MySQL Workbench
	* Entity Framework
	* Identity tool
	
## Installation

* Download [Git Bash](https://git-scm.com/downloads)

* Input the following into Git Bash to clone this repository onto your computer:

		git clone https://github.com/curiousmockingbird/RecipeBox

* Enter the cloned project folder "/RecipeBox/RecipeBox" and type:

		dotnet restore

followed by

		dotnet build

*After this, create an appsettings.json file in the root /RecipeBox folder (sub in your own set username and password for the bracketed bits)

		{
  		"ConnectionStrings": {
      	"DefaultConnection": "Server=localhost;Port=3306;database=recipeBox;uid=[YOUR-USERNAME];pwd=[YOUR-PASSWORD];"
  		}
		}

* Next, type into console in the root directory:
		
		dotnet ef migrations add Initial
followed by		

		dotnet ef database update

* Finally, navigate to the /RecipeBox directory in through your terminal (if you navigated away) and type  

		dotnet run

Follwing this you can navigate to http://localhost:5000 in the browser of your choice to navigate around the interface.  

## Known Bugs

For some reason, User.Identity.ApplicationUserProperty does not point to what we generally understand to be the default table (RecipeBox.Models.ApplicationUser in this case) and as such we were unable to call any ApplicationUser property more than a Name field for the user on the account index page.

## License

Licensed under [GNU GPL 3.0](https://www.gnu.org/licenses/gpl-3.0.en.html)
