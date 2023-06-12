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
                    new MenuItem { Controller = "Home", Action = "About", Label = "About"},
                    new MenuItem { Controller = "Companies", Action = "Index", Label = "Companies", DropdownItems = new List<MenuItem> { 
                    new MenuItem { Controller = "Companies", Action = "Index", Label = "List"},
                    new MenuItem { Controller = "Companies", Action = "Create", Label = "Create"},
                    } },
                    new MenuItem { Controller = "Home", Action = "Privacy", Label = "Privacy"},
                };
            
            return View(menuItems); 
        }   
    }
}
