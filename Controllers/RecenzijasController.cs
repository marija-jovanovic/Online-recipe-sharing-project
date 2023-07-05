using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Recepti.Data;
using Recepti.Models;

namespace Recepti.Controllers
{
    public class RecenzijasController : Controller
    {
        private readonly ReceptiContext _context;

        public RecenzijasController(ReceptiContext context)
        {
            _context = context;
        }

        // GET: Recenzijas
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Index()
        {
            var receptiContext = _context.Recenzija.Include(r => r.Recept);
            return View(await receptiContext.ToListAsync());
        }

        // GET: Recenzijas/Details/5
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Recenzija == null)
            {
                return NotFound();
            }

            var recenzija = await _context.Recenzija
                .Include(r => r.Recept)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recenzija == null)
            {
                return NotFound();
            }

            return View(recenzija);
        }

        // GET: Recenzijas/Create
        [Authorize(Roles = "User")]
        public IActionResult Create()

        {

            var receptiContext = _context.Recept.Include(s => s.Recenzii);
            var recepti = from m in receptiContext
                          select m;

            ViewData["ReceptId"] = new SelectList(receptiContext.Where(a => a.Kreator != HttpContext.User.Identity.Name), "Id", "Naslov");

            return View();
        }

        // POST: Recenzijas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Create([Bind("Id,Korisnik,Komentar,Ocena,ReceptId")] Recenzija recenzija)
        {

            var recenziiContext = _context.Recenzija;
            var recenzii = from n in recenziiContext select n;
           recenzii = recenzii.Where(x => x.Korisnik == HttpContext.User.Identity.Name);

            if (ModelState.IsValid)
            {
                foreach(var i in recenzii)
                {
                    if (recenzija.ReceptId == i.ReceptId)
                    {
                        //Console.WriteLine("Veke imate prethodno napisano recenzija za toj recept!");
                        TempData["message"] = "Veke imate prethodno napisano recenzija za toj recept!";
                        return RedirectToAction(nameof(Index));
                    }
                }
               
                if(recenzija.Korisnik != HttpContext.User.Identity.Name)
                {
                    TempData["poraka"] = "Ve molam napisete go vaseto pravo korisnicko ime!";
                    return RedirectToAction(nameof(Index));
                }

                _context.Add(recenzija);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var receptiContext = _context.Recept;
            var recepti = from m in receptiContext
                          select m;

          
            ViewData["ReceptId"] = new SelectList(receptiContext.Where(a => a.Kreator != HttpContext.User.Identity.Name), "Id", "Naslov", recenzija.ReceptId);
            return View(recenzija);
        }

        // GET: Recenzijas/Edit/5
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Recenzija == null)
            {
                return NotFound();
            }

            var recenzija = await _context.Recenzija.FindAsync(id);
            if (recenzija == null)
            {
                return NotFound();
            }

            if (recenzija.Korisnik != HttpContext.User.Identity.Name)
            {
                return NotFound();
            }

                ViewData["ReceptId"] = new SelectList(_context.Recept, "Id", "Naslov", recenzija.ReceptId);
            return View(recenzija);
        }

        // POST: Recenzijas/Edit/5
        [Authorize(Roles = "User")]
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Korisnik,Komentar,Ocena,ReceptId")] Recenzija recenzija)
        {
            if (id != recenzija.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recenzija);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecenzijaExists(recenzija.Id))
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
            ViewData["ReceptId"] = new SelectList(_context.Recept, "Id", "Naslov", recenzija.ReceptId);
            return View(recenzija);
        }

        // GET: Recenzijas/Delete/5
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Recenzija == null)
            {
                return NotFound();
            }

            var recenzija = await _context.Recenzija
                .Include(r => r.Recept)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recenzija == null)
            {
                return NotFound();
            }

            if (recenzija.Korisnik != HttpContext.User.Identity.Name)
            {
                return NotFound();
            }

            return View(recenzija);
        }

        // POST: Recenzijas/Delete/5
        [Authorize(Roles = "User")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Recenzija == null)
            {
                return Problem("Entity set 'ReceptiContext.Recenzija'  is null.");
            }
            var recenzija = await _context.Recenzija.FindAsync(id);
            if (recenzija != null)
            {
                _context.Recenzija.Remove(recenzija);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecenzijaExists(int id)
        {
          return (_context.Recenzija?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
