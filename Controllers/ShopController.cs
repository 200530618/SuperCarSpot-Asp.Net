using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Stripe;
using Stripe.BillingPortal;
using Stripe.Checkout;
using SuperCarSpot.Data;
using SuperCarSpot.Models;
using System.Security.Claims;

namespace SuperCarSpot.Controllers
{
    public class ShopController : Controller
    {
        private ApplicationDbContext _context;
        private IConfiguration _configuration;

        public ShopController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
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
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddToFavourite(int carId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var favourite = await _context.Favorites
                .FirstOrDefaultAsync(favourite => favourite.UserId == userId && favourite.Active == true);

            if (favourite == null)
            {
                favourite = new Models.Favourite { UserId = userId };
                await _context.AddAsync(favourite);
                await _context.SaveChangesAsync();
            }

            var car = await _context.Cars
                .FirstOrDefaultAsync(car => car.Id == carId);

            if (car == null)
            {
                return NotFound();
            }


            var favouriteItem = new FavouriteItem
            {
                Favourite = favourite,
                Car = car,
                Price = car.Price,

            };

            if (ModelState.IsValid)
            {
                await _context.AddAsync(favouriteItem);
                await _context.SaveChangesAsync();
                return RedirectToAction("ViewMyFavourite");
            }

            return NotFound();
        }
        [Authorize]
        public async Task<IActionResult> ViewMyFavourite()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var favourite = await _context.Favorites
                .Include(favourite => favourite.User)
                .Include(favourite => favourite.FavouriteItems)
                .ThenInclude(favouriteItem => favouriteItem.Car)
                .FirstOrDefaultAsync(favourite => favourite.UserId == userId && favourite.Active == true);

            return View(favourite);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeleteFavouriteItem(int favouriteItemId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var favourite = await _context.Favorites
                .FirstOrDefaultAsync(favourite => favourite.UserId == userId && favourite.Active == true);

            if(favourite == null) NotFound();

            var favouriteItem = await _context.FavouriteItems
                .FirstOrDefaultAsync(favouriteItem => favouriteItem.Favourite == favourite && favouriteItem.Id == favouriteItemId);

            if (favouriteItem != null)
            {
                _context.FavouriteItems.Remove(favouriteItem);
                await _context.SaveChangesAsync();

                return RedirectToAction("ViewMyFavourite");
            }

            return NotFound();
        }

        [Authorize]
        public async Task<IActionResult> Checkout()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var favourite = await _context.Favorites
                .Include(favourite => favourite.User)
                .Include(favourite => favourite.FavouriteItems)
                .ThenInclude(favouriteItem => favouriteItem.Car)
                .FirstOrDefaultAsync(favourite => favourite.UserId == userId && favourite.Active == true);

            var order = new Models.Order
            {
                UserId = userId,
                Favourite = favourite,
                Total = favourite.FavouriteItems.Sum(favouriteItem => favouriteItem.Price),
                ShippingAddress = "",
                PaymentMethod = Models.PaymentMethods.VISA,
            };

            ViewData["PaymentMethods"] = new SelectList(Enum.GetValues(typeof(PaymentMethods)));
            return View(order);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Payment(string shippingAddress, PaymentMethods paymentMethod)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var favourite = await _context.Favorites
                .Include(favourite => favourite.FavouriteItems)
                .FirstOrDefaultAsync(favourite => favourite.UserId == userId && favourite.Active == true);

            if (favourite == null) return NotFound();

            HttpContext.Session.SetString("ShippingAddress", shippingAddress);
            HttpContext.Session.SetString("PaymentMethod", paymentMethod.ToString());

            StripeConfiguration.ApiKey = _configuration.GetSection("Stripe")["SecretKey"];

            var options = new Stripe.Checkout.SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = (long)(favourite.FavouriteItems.Sum(favouriteItem =>  favouriteItem.Price)*100),
                            Currency = "cad",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = "SuperCarSpot Purchase",
                            },
                        },
                        Quantity = 1,
                    },
                },
                PaymentMethodTypes = new List<string>
                {
                    "card"
                },
                Mode = "payment",
                SuccessUrl = "https://" + Request.Host + "/Shop/SaveOrder",
                CancelUrl = "https://" + Request.Host + "/Shop/ViewMyFavourite",
            };

            var service = new Stripe.Checkout.SessionService();
            Stripe.Checkout.Session session = service.Create(options);

            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
      
        }
        public async Task <IActionResult> SaveOrder()
        {

        }
    }
}
