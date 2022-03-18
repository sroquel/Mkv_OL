using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MkvOL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MkvOL.Controllers
{
    public class StocksFranquiciasController : Controller
    {
        private readonly BDSistemasContext _context;

        public StocksFranquiciasController(BDSistemasContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Index(string pCodigo)
        {
            if (pCodigo == null | pCodigo == "")
            {
                ViewBag.Error = "Debe Ingresar Un codigo";
                return View();
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return RedirectToAction("ListarStocks", "StocksFranquicias", new { Codigo = pCodigo });

        }

        public ActionResult ListarStocks(string Codigo)
        {
            if (Codigo == null | Codigo == "")
            {
                ModelState.AddModelError(string.Empty, "Debe Ingresar Un codigo");
                ViewBag.Error = "Debe Ingresar Un codigo";
                return RedirectToAction("Index", "StocksFranquicias", null);

            }
            var idParam = new SqlParameter
            {
                ParameterName = "Codigo",
                Value = Codigo
            };

            var stocksLocales = _context.StocksFranquicias.FromSqlRaw("Stocks_Franquicias @Codigo", idParam).ToList();

            return View(stocksLocales);
        }
    }
}
