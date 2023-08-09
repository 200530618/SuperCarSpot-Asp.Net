using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperCarSpot.Data;

namespace SuperCarSpot.Controllers
{
    public class ShopController : Controller
    {
        private ApplicationDbContext _context;

        public ShopController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var brands = await _context.Brands 
                .OrderBy(brand => brand.Name)
                .ToListAsync();
            return View(brands);
        }

        public async Task<IActionResult> Details(int? id)
        {
            var brandWithCars = await _context.Brands
                .Include(brand => brand.Cars)
                .FirstOrDefaultAsync(brand => brand.Id == id);

            return View(brandWithCars);
        }

        public async Task<IActionResult> CarDetails(int? id)
        {
            var car = await _context.Cars
                .Include(car => car.Brand)
                .FirstOrDefaultAsync(car => car.Id == id);

            return View(car);
        }
    }
}
