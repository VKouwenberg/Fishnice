using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
//using DAL_DataAccessLayer.DTO.Models;
using Fishnice.Data;
using Fishnice.Models;
using DAL_DataAccessLayer.DTO.Models;

namespace Fishnice.Controllers
{
    public class FishController : Controller
    {
        private readonly FishniceContext _context;

        public FishController(FishniceContext context)
        {
            _context = context;
        }

        [HttpPost]
        public string Index(string searchString, bool notUsed)
        {
            return "From [HttpPost]Index: filter on " + searchString;
        }

        // GET: Fish
        public async Task<IActionResult> Index(string fishGenre, string searchString)
        {
            if (_context.Fish == null)
            {
                return Problem("Entity set 'FishniceContext.Fish'  is null.");
            }

            // Use LINQ to get list of genres.
            IQueryable<string> genreQuery = from f in _context.Fish
                                            orderby f.Genre
                                            select f.Genre;

            var fishes = from f in _context.Fish
                         select f;

            if (!String.IsNullOrEmpty(searchString))
            {
                fishes = fishes.Where(s => s.Title!.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(fishGenre))
            {
                fishes = fishes.Where(x => x.Genre == fishGenre);
            }

            var fishGenreVM = new Models.FishGenreViewModel
            {
                Genres = new SelectList(await genreQuery.Distinct().ToListAsync()),
                Fishes = await fishes.ToListAsync()
            };

            return View(fishGenreVM);
        }

        // GET: Fish/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Fish == null)
            {
                return NotFound();
            }

            var fish = await _context.Fish
                .FirstOrDefaultAsync(f => f.Id == id);
            if (fish == null)
            {
                return NotFound();
            }

            return View(fish);
        }

        // GET: Fish/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Fish/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,CatchDate,Genre,Price")] Fish fish)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fish);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fish);
        }

        // GET: Fish/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Fish == null)
            {
                return NotFound();
            }

            var fish = await _context.Fish.FindAsync(id);
            if (fish == null)
            {
                return NotFound();
            }
            return View(fish);
        }

        // POST: Fish/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,CatchDate,Genre,Price")] Fish fish)
        {
            if (id != fish.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fish);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FishExists(fish.Id))
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
            return View(fish);
        }

        // GET: Fish/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Fish == null)
            {
                return NotFound();
            }

            var fish = await _context.Fish
                .FirstOrDefaultAsync(f => f.Id == id);
            if (fish == null)
            {
                return NotFound();
            }

            return View(fish);
        }

        // POST: Fish/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Fish == null)
            {
                return Problem("Entity set 'FishniceContext.Fish'  is null.");
            }
            var fish = await _context.Fish.FindAsync(id);
            if (fish != null)
            {
                _context.Fish.Remove(fish);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FishExists(int id)
        {
          return (_context.Fish?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
