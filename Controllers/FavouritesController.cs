using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SuperCarSpot.Data;
using SuperCarSpot.Models;

namespace SuperCarSpot.Controllers
{
    public class FavouritesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FavouritesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Favourites
        public async Task<IActionResult> Index()
        {
              return _context.Favorites != null ? 
                          View(await _context.Favorites.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Favorites'  is null.");
        }

        // GET: Favourites/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Favorites == null)
            {
                return NotFound();
            }

            var favourite = await _context.Favorites
                .FirstOrDefaultAsync(m => m.Id == id);
            if (favourite == null)
            {
                return NotFound();
            }

            return View(favourite);
        }

        // GET: Favourites/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Favourites/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId")] Favourite favourite)
        {
            if (ModelState.IsValid)
            {
                _context.Add(favourite);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(favourite);
        }

        // GET: Favourites/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Favorites == null)
            {
                return NotFound();
            }

            var favourite = await _context.Favorites.FindAsync(id);
            if (favourite == null)
            {
                return NotFound();
            }
            return View(favourite);
        }

        // POST: Favourites/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId")] Favourite favourite)
        {
            if (id != favourite.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(favourite);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FavouriteExists(favourite.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(favourite);
        }

        // GET: Favourites/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Favorites == null)
            {
                return NotFound();
            }

            var favourite = await _context.Favorites
                .FirstOrDefaultAsync(m => m.Id == id);
            if (favourite == null)
            {
                return NotFound();
            }

            return View(favourite);
        }

        // POST: Favourites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Favorites == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Favorites'  is null.");
            }
            var favourite = await _context.Favorites.FindAsync(id);
            if (favourite != null)
            {
                _context.Favorites.Remove(favourite);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FavouriteExists(int id)
        {
          return (_context.Favorites?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
