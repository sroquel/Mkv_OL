using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MkvOL.Data;

namespace MkvOL.Controllers
{
    public class DespachosController : Controller
    {
        private readonly BDSistemasContext _context;
        public DespachosController(BDSistemasContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string pedido)
        {
            if (pedido == null)
            {
                ViewBag.Error = "Debe Ingresar Un Nro. de Pedido";
                return View();
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return RedirectToAction("ConsultaPedido", "Despachos", new { consulta = pedido });

        }

        public ActionResult ConsultaPedido(string consulta)
        {
            string sSeguimiento;
            if (consulta == null)
            {
                ModelState.AddModelError(string.Empty, "Debe Ingresar Un Nro. de Pedido");
                ViewBag.Error = "Debe Ingresar Un Nro de Pedido";
                return RedirectToAction("Index", "Despachos", null);

            }

            if (!int.TryParse(consulta,out int noPedido)) 
            { 
                noPedido = 0;
                sSeguimiento = consulta;
            }
            else
            {
                sSeguimiento = "";
            }

            var pedido = new SqlParameter
            {
                ParameterName = "NoPedido",
                Value = noPedido
            };
            var seguimiento = new SqlParameter
            {
                ParameterName = "Seguimiento",
                Value = sSeguimiento
            };

            var status = _context.Despachos.FromSqlRaw("Status_Pedido_Ecommerce @NoPedido, @Seguimiento", pedido, seguimiento).ToList();

            return View(status);
        }
    }
}
