using Microsoft.AspNetCore.Mvc;
using SuperCarSpot.Models;
using System.Net.NetworkInformation;

namespace SuperCarSpot.Components.ViewComponents
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke() 
        {
                 var menuItems = new List<MenuItem>
                {
                    new MenuItem { Controller = "Home", Action = "Index", Label = "Home"},
                    new MenuItem {Controller = "Shop", Action = "Index", Label = "Shop"},
                    new MenuItem {Controller = "Shop", Action = "ViewMyFavourite", Label = "Favourite", Authorized = true},
                    new MenuItem {Controller = "Shop", Action = "Orders", Label = "Orders", Authorized = true},
                    new MenuItem { Controller = "Orders", Action = "Index", Label = "Admin", Authorized = true, AllowedRoles = new List<string> { "Administrator" },
                      DropdownItems = new List<MenuItem> {
                       new MenuItem { Controller = "Brands", Action = "Index", Label = "Brands"},
                       new MenuItem { Controller = "Cars", Action = "Index", Label = "Cars"},
                       new MenuItem { Controller = "Orders", Action = "Index", Label = "Orders"},
                       new MenuItem { Controller = "Favourites", Action = "Index", Label = "Favourites"},
                      }},
                  
                    new MenuItem { Controller = "Home", Action = "About", Label = "About"},
                    new MenuItem { Controller = "Home", Action = "Privacy", Label = "Privacy"},
                };
            
            return View(menuItems); 
        }   
    }
}
