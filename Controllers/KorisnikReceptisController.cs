using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Recepti.Data;
using Recepti.Models;

namespace Recepti.Controllers
{
    public class KorisnikReceptisController : Controller
    {
        private readonly ReceptiContext _context;

        public KorisnikReceptisController(ReceptiContext context)
        {
            _context = context;
        }

        // GET: KorisnikReceptis
        public async Task<IActionResult> Index()
        {
              return _context.KorisnikRecepti != null ? 
                          View(await _context.KorisnikRecepti.ToListAsync()) :
                          Problem("Entity set 'ReceptiContext.KorisnikRecepti'  is null.");
        }

        // GET: KorisnikReceptis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.KorisnikRecepti == null)
            {
                return NotFound();
            }

            var korisnikRecepti = await _context.KorisnikRecepti
                .FirstOrDefaultAsync(m => m.Id == id);
            if (korisnikRecepti == null)
            {
                return NotFound();
            }

            return View(korisnikRecepti);
        }

        // GET: KorisnikReceptis/Create
        public IActionResult Create()
        {
            ViewData["ReceptiId"] = new SelectList(_context.Recept, "Id", "Naslov");
            return View();
        }

        // POST: KorisnikReceptis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,korisnickoIme,ReceptiId")] KorisnikRecepti korisnikRecepti)
        {
            if (ModelState.IsValid)
            {
                _context.Add(korisnikRecepti);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ReceptiId"] = new SelectList(_context.Recept, "Id", "Naslov", korisnikRecepti.ReceptiId);
            return View(korisnikRecepti);
        }

        // GET: KorisnikReceptis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.KorisnikRecepti == null)
            {
                return NotFound();
            }

            var korisnikRecepti = await _context.KorisnikRecepti.FindAsync(id);
            if (korisnikRecepti == null)
            {
                return NotFound();
            }
            ViewData["ReceptiId"] = new SelectList(_context.Recept, "Id", "Naslov", korisnikRecepti.ReceptiId);
            return View(korisnikRecepti);
        }

        // POST: KorisnikReceptis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,korisnickoIme,ReceptiId")] KorisnikRecepti korisnikRecepti)
        {
            if (id != korisnikRecepti.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(korisnikRecepti);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KorisnikReceptiExists(korisnikRecepti.Id))
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
            ViewData["ReceptiId"] = new SelectList(_context.Recept, "Id", "Naslov", korisnikRecepti.ReceptiId);
            return View(korisnikRecepti);
        }

        // GET: KorisnikReceptis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.KorisnikRecepti == null)
            {
                return NotFound();
            }

            var korisnikRecepti = await _context.KorisnikRecepti
                .FirstOrDefaultAsync(m => m.Id == id);
            if (korisnikRecepti == null)
            {
                return NotFound();
            }

            return View(korisnikRecepti);
        }

        // POST: KorisnikReceptis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.KorisnikRecepti == null)
            {
                return Problem("Entity set 'ReceptiContext.KorisnikRecepti'  is null.");
            }
            var korisnikRecepti = await _context.KorisnikRecepti.FindAsync(id);
            if (korisnikRecepti != null)
            {
                _context.KorisnikRecepti.Remove(korisnikRecepti);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KorisnikReceptiExists(int id)
        {
          return (_context.KorisnikRecepti?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
