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
    public class InternosController : Controller
    {
        private readonly BDSistemasContext _context;

        public InternosController(BDSistemasContext context)
        {
            _context = context;
        }

        // GET: Internos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Internos.ToListAsync());
        }

        // GET: Internos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var internosEntity = await _context.Internos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (internosEntity == null)
            {
                return NotFound();
            }

            return View(internosEntity);
        }

        // GET: Internos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Internos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NroInterno,Usuario,Celular")] InternosEntity internosEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(internosEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(internosEntity);
        }

        // GET: Internos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var internosEntity = await _context.Internos.FindAsync(id);
            if (internosEntity == null)
            {
                return NotFound();
            }
            return View(internosEntity);
        }

        // POST: Internos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NroInterno,Usuario,Celular")] InternosEntity internosEntity)
        {
            if (id != internosEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(internosEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InternosEntityExists(internosEntity.Id))
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
            return View(internosEntity);
        }

        // GET: Internos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var internosEntity = await _context.Internos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (internosEntity == null)
            {
                return NotFound();
            }

            return View(internosEntity);
        }

        // POST: Internos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var internosEntity = await _context.Internos.FindAsync(id);
            _context.Internos.Remove(internosEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InternosEntityExists(int id)
        {
            return _context.Internos.Any(e => e.Id == id);
        }
    }
}
