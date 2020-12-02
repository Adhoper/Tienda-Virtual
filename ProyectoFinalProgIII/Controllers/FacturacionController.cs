using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoFinalProgIII.Data;
using ProyectoFinalProgIII.Models;
using ProyectoFinalProgIII.VIewModels;

namespace ProyectoFinalProgIII.Controllers
{
    [Authorize]
    public class FacturacionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FacturacionController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Facturacion
        public async Task<IActionResult> Index()
        {
            var result = (await _context.Facturacion.Where(f => f.UsuarioId == Models.UtilityModel.UserId).Select(s => new FacturaListVM
            {
                Cantidad = s.Cantidad,
                FacturacionId = s.FacturacionId,
                Itbis = s.Itbis,
                NombreCliente = _context.Clientes.Where(c => c.ClienteId == s.ClienteId).FirstOrDefault().Nombre,
                NombreProducto = _context.Productos.Where(c => c.ProductosId == s.ProductosId).FirstOrDefault().NombreP,
                NombreServicio = _context.Servicios.Where(c => c.ServiciosId == s.ServiciosId).FirstOrDefault().NombreS,
                NombreUsuario = _context.Usuarios.Where(c => c.Id.Equals(UtilityModel.UserId.ToString())).FirstOrDefault().Nombre,
                TipoFactura = "Prueba"

            }).ToListAsync());


            //if (result.Count>0)
            //{
                return View(result);

            //}
            //else{
            //    return View(new List<FacturaVM>());
            //}

        }

        // GET: Facturacion/Details/5
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

        // GET: Facturacion/Create
        public IActionResult Create()
        {
            var result = new FacturaVM
            {
                Productos = _context.Productos.ToList(),
                Clientes = _context.Clientes.ToList(),
                Servicios = _context.Servicios.ToList(),
                Facturacion = new Facturacion()
            };
            return View(result);
        }

        // POST: Facturacion/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FacturacionId,TipoFactura,Cantidad,Itbis,ClienteId,UsuarioId,ProductosId,ServiciosId")] Facturacion facturacion)
        {
            if (ModelState.IsValid)
            {
                facturacion.FacturacionId = Guid.NewGuid();
                facturacion.UsuarioId = UtilityModel.UserId;
                _context.Add(facturacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(facturacion);
        }

        // GET: Facturacion/Edit/5
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

        // POST: Facturacion/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("FacturacionId,TipoFactura,Cantidad,Itbis,ClienteId,UsuarioId,ProductosId,ServiciosId")] Facturacion facturacion)
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

        // GET: Facturacion/Delete/5
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

        // POST: Facturacion/Delete/5
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
