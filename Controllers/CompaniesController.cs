using Microsoft.AspNetCore.Mvc;
using SuperCarSpot.Models;

namespace SuperCarSpot.Controllers
{
    public class CompaniesController : Controller
    {
        private List<Company> companies;
        public CompaniesController() 
        {
            companies = new List<Company>
            {
                new Company { Id = 1, Name = "Lamborghini", Description ="Huucane Sports Car"}
                , new Company { Id = 2, Name = "Buggati", Description = "Chiron The Best"}
                , new Company { Id = 3, Name = "Ferrari", Description = "499 In Cherry Color"},

            };
        }
        public IActionResult Index()
        {
            return View(companies);
        }

        public IActionResult Browse(int id)
        {
            var company = companies.Find(company => company.Id == id);
            if(company == null)
            {
                return RedirectToAction("Index");
            }
            return View(company);
        }
    }
}
