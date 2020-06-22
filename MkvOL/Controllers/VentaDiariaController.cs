using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MkvOL.Data;
using MkvOL.Data.Entities;

namespace MkvOL.Controllers
{
    public class VentaDiariaController : Controller
    {
        private readonly BDSistemasContext _context;

        public VentaDiariaController(BDSistemasContext context)
        {
            _context = context;
        }

        // GET: VentaDiaria
        public async Task<IActionResult> Index()
        {
            var fechaParam = new SqlParameter
            {
                ParameterName = "Fecha",
                Value = DateTime.Today
            };

            var ventaDiaria = _context.VentaDiaria.FromSqlRaw("Venta_Diaria @Fecha", fechaParam).ToList();
            ViewData["Fecha"] = DateTime.Today.ToString("yyyy-MM-dd");
            return View(ventaDiaria);
        }

        [HttpPost]
        public ActionResult Index(DateTime fechaConsulta)
        {
            if (fechaConsulta == null)
            {
                fechaConsulta = DateTime.Today;
            }

            var fechaParam = new SqlParameter
            {
                ParameterName = "Fecha",
                Value = fechaConsulta
            };

            var ventaDiaria = _context.VentaDiaria.FromSqlRaw("Venta_Diaria @Fecha", fechaParam).ToList();
            ViewData["Fecha"] = fechaConsulta.ToString("yyyy-MM-dd");
            return View(ventaDiaria);
        }


    }
}
