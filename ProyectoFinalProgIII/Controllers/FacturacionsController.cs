using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoFinalProgIII.Data;

namespace ProyectoFinalProgIII.Controllers
{
    public class FacturacionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FacturacionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Facturacions
        public async Task<IActionResult> Index()
        {
            return View(await _context.Facturacion.ToListAsync());
        }

        // GET: Facturacions/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facturacion = await _context.Facturacion
                .FirstOrDefaultAsync(m => m.FacturacionId == id);
            if (facturacion == null)
            {
                return NotFound();
            }

            return View(facturacion);
        }

        // GET: Facturacions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Facturacions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FacturacionId,TipoFactura,Cantidad,Itbis,ClienteId")] Facturacion facturacion)
        {
            if (ModelState.IsValid)
            {
                facturacion.FacturacionId = Guid.NewGuid();
                _context.Add(facturacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(facturacion);
        }

        // GET: Facturacions/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facturacion = await _context.Facturacion.FindAsync(id);
            if (facturacion == null)
            {
                return NotFound();
            }
            return View(facturacion);
        }

        // POST: Facturacions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("FacturacionId,TipoFactura,Cantidad,Itbis,ClienteId")] Facturacion facturacion)
        {
            if (id != facturacion.FacturacionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(facturacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacturacionExists(facturacion.FacturacionId))
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
            return View(facturacion);
        }

        // GET: Facturacions/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facturacion = await _context.Facturacion
                .FirstOrDefaultAsync(m => m.FacturacionId == id);
            if (facturacion == null)
            {
                return NotFound();
            }

            return View(facturacion);
        }

        // POST: Facturacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var facturacion = await _context.Facturacion.FindAsync(id);
            _context.Facturacion.Remove(facturacion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FacturacionExists(Guid id)
        {
            return _context.Facturacion.Any(e => e.FacturacionId == id);
        }
    }
}
