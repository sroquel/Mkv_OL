using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MkvOL.Data;
using MkvOL.Data.Entities;

namespace MkvOL.Controllers
{
    public class SectoresController : Controller
    {
        private readonly BDSistemasContext _context;

        public SectoresController(BDSistemasContext context)
        {
            _context = context;
        }

        // GET: Sectores
        public async Task<IActionResult> Index()
        {
            return View(await _context.Sectores.ToListAsync());
        }

        // GET: Sectores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sectoresEntity = await _context.Sectores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sectoresEntity == null)
            {
                return NotFound();
            }

            return View(sectoresEntity);
        }

        // GET: Sectores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sectores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Sector")] SectoresEntity sectoresEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sectoresEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sectoresEntity);
        }

        // GET: Sectores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sectoresEntity = await _context.Sectores.FindAsync(id);
            if (sectoresEntity == null)
            {
                return NotFound();
            }
            return View(sectoresEntity);
        }

        // POST: Sectores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Sector")] SectoresEntity sectoresEntity)
        {
            if (id != sectoresEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sectoresEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SectoresEntityExists(sectoresEntity.Id))
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
            return View(sectoresEntity);
        }

        // GET: Sectores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sectoresEntity = await _context.Sectores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sectoresEntity == null)
            {
                return NotFound();
            }

            return View(sectoresEntity);
        }

        // POST: Sectores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sectoresEntity = await _context.Sectores.FindAsync(id);
            _context.Sectores.Remove(sectoresEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SectoresEntityExists(int id)
        {
            return _context.Sectores.Any(e => e.Id == id);
        }
    }
}
