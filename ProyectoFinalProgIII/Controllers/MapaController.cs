using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProyectoFinalProgIII.Data;
using ProyectoFinalProgIII.VIewModels;

namespace ProyectoFinalProgIII.Controllers
{
    public class MapaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MapaController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {


            return View(_context.Clientes);
        }
    }
}