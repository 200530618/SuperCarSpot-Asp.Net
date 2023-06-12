using Microsoft.AspNetCore.Mvc;
using SuperCarSpot.Models;

namespace SuperCarSpot.Controllers
{
    public class CompaniesController : Controller
    {
        private static List<Company> companies {  get; set; }
        public CompaniesController() 
        {
            if(companies == null)
            {
                companies = new List<Company>
                {
                    new Company { Id = 1, Name = "Lamborghini", Description ="Huucane Sports Car"}
                    , new Company { Id = 2, Name = "Buggati", Description = "Chiron The Best"}
                    , new Company { Id = 3, Name = "Ferrari", Description = "499 In Cherry Color"},

                };

            }
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
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Company company)
        {
            if(ModelState.IsValid)
            {
                companies.Add(company);

                return RedirectToAction("Index");
            }
            return View(company);
        }
    }
}
