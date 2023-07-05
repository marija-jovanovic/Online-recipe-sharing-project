using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Recepti.Data;
using Recepti.Models;
using Recepti.ViewModels;

namespace Recepti.Controllers
{
    public class ReceptsController : Controller
    {
        private readonly ReceptiContext _context;

        private readonly IWebHostEnvironment webHostEnvironment;

        public ReceptsController(ReceptiContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            webHostEnvironment = hostEnvironment;
        }

        // GET: Recepts
        public async Task<IActionResult> Index(string searchTip, string searchVegan, string searchDeca, string searchOwn)
        {
            var receptiContext = _context.Recept.Include(b => b.Recenzii);
            var recepti = from m in receptiContext
                        select m;

            ViewBag.Tipovi = (from m in _context.Recept select m.Tip).Distinct();
            ViewBag.Vegan = (from m in _context.Recept select m.Vegan).Distinct();
            ViewBag.Deca = (from m in _context.Recept select m.Za_deca).Distinct();
            ViewBag.Kreatori = (from m in _context.Recept select m.Kreator).Distinct();



            if (!String.IsNullOrEmpty(searchTip) && searchTip != "All")
            {
                recepti = recepti.Where(a =>
a.Tip.Contains(searchTip));
            }



            if (!String.IsNullOrEmpty(searchVegan) && searchVegan != "All")
            {
                recepti = recepti.Where(a =>
a.Vegan.Contains(searchVegan));
            }

            if (!String.IsNullOrEmpty(searchDeca) && searchDeca != "All")
            {
                recepti = recepti.Where(a =>
a.Za_deca.Contains(searchDeca));
            }

            if (!String.IsNullOrEmpty(searchOwn) && searchOwn != "All")
            {
                recepti = recepti.Where(a =>
a.Kreator.Equals(searchOwn));
            }


            return View(await recepti.ToListAsync());
        }

        // GET: Recepts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Recept == null)
            {
                return NotFound();
            }

            var recept = await _context.Recept.Include(s => s.Recenzii)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recept == null)
            {
                return NotFound();
            }

            return View(recept);
        }

        // GET: Recepts/Create
        [Authorize(Roles = "User")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Recepts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Create([FromForm] ReceptCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadedFile(model);

                Recept recept = new Recept
                {
                    Naslov = model.Naslov,
                    Vreme = model.Vreme,
                    Tekst = model.Tekst,
                    Vegan = model.Vegan,
                    Za_deca = model.Za_deca,
                    Slika = uniqueFileName,
                    Tip = model.Tip,
                    Kreator = HttpContext.User.Identity.Name
                };
                _context.Add(recept);

                //_context.Add(recept);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: Recepts/Edit/5
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null || _context.Recept == null)
            {
                return NotFound();
            }

            var recept = await _context.Recept.FindAsync(id);
            if (recept == null)
            {
                return NotFound();
            }

            ReceptEditViewModel viewmodel = new ReceptEditViewModel
            {
                Recept = recept,
            };

            if (HttpContext.User.Identity.Name == "admin@workshopimproved.com")
            {

                return View(viewmodel);
            }

            if(recept.Kreator != HttpContext.User.Identity.Name)
            {
                return NotFound();
            }

          /*  ReceptEditViewModel viewmodel = new ReceptEditViewModel
            {
                Recept = recept,
            }; */


            return View(viewmodel);
        }

        // POST: Recepts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Edit(int id, ReceptEditViewModel viewmodel)
        {
            if (id != viewmodel.Recept.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    string uniqueFileName = null;  //to contain the filename
                    if (viewmodel.slika != null)  //handle iformfile
                    {
                        string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                        uniqueFileName = viewmodel.slika.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            viewmodel.slika.CopyTo(fileStream);
                        }

                        viewmodel.Recept.Slika = uniqueFileName; //fill the image property
                    }

                    else
                    {

                        var receptiContext = _context.Recept;
                        var recepti = from m in receptiContext select m;
                        // var odbrano = recepti.Where(a => a.Id == id);
                        var odbrano = _context.Recept.Where(p => p.Id == id).Select(p => p.Slika).Single();


                        viewmodel.Recept.Slika = odbrano;
                    }





                    _context.Update(viewmodel.Recept);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReceptExists(viewmodel.Recept.Id))
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
            return View(viewmodel);
        }

        // GET: Recepts/Delete/5
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Recept == null)
            {
                return NotFound();
            }

            var recept = await _context.Recept
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recept == null)
            {
                return NotFound();
            }

            if(HttpContext.User.Identity.Name == "admin@workshopimproved.com")
            {
                return View(recept);
            }

            if (recept.Kreator != HttpContext.User.Identity.Name)
            {
                return NotFound();
            }

            return View(recept);
        }

        // POST: Recepts/Delete/5
        [Authorize(Roles = "User,Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Recept == null)
            {
                return Problem("Entity set 'ReceptiContext.Recept'  is null.");
            }
            var recept = await _context.Recept.FindAsync(id);
            if (recept != null)
            {
                _context.Recept.Remove(recept);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReceptExists(int id)
        {
          return (_context.Recept?.Any(e => e.Id == id)).GetValueOrDefault();
        }


        private string UploadedFile(ReceptCreateViewModel model)
        {
            string uniqueFileName = null;

            if (model.Slikaa != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Slikaa.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Slikaa.CopyTo(fileStream);

                }
            }


            return uniqueFileName;
        }
    }
}
