using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MkvOL.Data;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MkvOL.Controllers
{
    //[Authorize(Roles = "MARKOVA\\Gerentes" )]
    public class DashVentaController : Controller
    {
        private readonly BDSistemasContext _context;

        public DashVentaController(BDSistemasContext context)
        {
            _context = context;
        }

        // GET: DashVenta
        public async Task<IActionResult> Index()
        {
            var fechaOriginal = DateTime.Today.AddDays(-1);

            var fechaHta = new SqlParameter
            {
                ParameterName = "FechaHta",
                Value = fechaOriginal
            };


            var fechaDde = new SqlParameter
            {
                ParameterName = "FechaDde",
                Value = Convert.ToDateTime($"{fechaOriginal.Year}/{fechaOriginal.Month}/01")
            };

            var ventaDash = _context.DashVenta.FromSqlRaw("Dashboard_Venta @FechaDde, @FechaHta", fechaDde, fechaHta).ToList();
            var totalDiaPropios = ventaDash.Where(t => t.Franquicia == false).Sum(v => v.VentaDia);
            var acumPropios = ventaDash.Where(t => t.Franquicia == false).Sum(v => v.Acumulado);
            var acumConsignacion = ventaDash.Where(t => t.Consignacion == true).Sum(v => v.Acumulado);
            var acumTotal = acumPropios + acumConsignacion;
            var totalConsignacion = ventaDash.Where(t => t.Consignacion == true).Sum(v => v.VentaDia);
            var totalVentaDia = totalConsignacion + totalDiaPropios;
            ViewData["TotalPropios"] = totalDiaPropios.ToString("C2");
            ViewData["TotalConsignacion"] = totalConsignacion.ToString("C2");
            ViewData["TotalDia"] = totalVentaDia.ToString("C2");
            ViewData["AcumPropios"] = acumPropios.ToString("C2");
            ViewData["AcumConsignacion"] = acumConsignacion.ToString("C2");
            ViewData["TotalAcum"] = acumTotal.ToString("C2");
            ViewData["Fecha"] = fechaOriginal.ToString("yyyy-MM-dd");
            return View(ventaDash);
        }

        [HttpPost]
        public ActionResult Index(DateTime fechaConsulta)
        {
            if (fechaConsulta == null)
            {
                fechaConsulta = DateTime.Today;
            }

            var fechaHta = new SqlParameter
            {
                ParameterName = "FechaHta",
                Value = fechaConsulta
            };

            DateTime dFechaDde = new DateTime(fechaConsulta.Year, fechaConsulta.Month, 01);
            var fechaDde = new SqlParameter
            {
                ParameterName = "FechaDde",
                Value = Convert.ToDateTime(dFechaDde.ToString("yyyy/MM/dd"))
            };

            var ventaDash = _context.DashVenta.FromSqlRaw("Dashboard_Venta @FechaDde, @FechaHta", fechaDde, fechaHta).ToList();
            var totalDiaPropios = ventaDash.Where(t => t.Franquicia == false).Sum(v => v.VentaDia);
            var acumPropios = ventaDash.Where(t => t.Franquicia == false).Sum(v => v.Acumulado);
            var acumConsignacion = ventaDash.Where(t => t.Consignacion == true).Sum(v => v.Acumulado);
            var acumTotal = acumPropios + acumConsignacion;
            var totalConsignacion= ventaDash.Where(t => t.Consignacion == true).Sum(v => v.VentaDia);
            var totalVentaDia = totalConsignacion + totalDiaPropios;
            ViewData["TotalPropios"] = totalDiaPropios.ToString("C2");
            ViewData["TotalConsignacion"] = totalConsignacion.ToString("C2");
            ViewData["TotalDia"] = totalVentaDia.ToString("C2");
            ViewData["AcumPropios"]= acumPropios.ToString("C2");
            ViewData["AcumConsignacion"]=acumConsignacion.ToString("C2");
            ViewData["TotalAcum"]=acumTotal.ToString("C2");
            ViewData["Fecha"] = fechaConsulta.ToString("yyyy-MM-dd");
            return View(ventaDash);
        }

    }
}
