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
    public class StocksLocalesController : Controller
    {

        private readonly BDSistemasContext _context;
        public StocksLocalesController(BDSistemasContext context)
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
            return RedirectToAction("ListarStocks", "StocksLocales", new { Codigo = pCodigo });

        }

        public ActionResult ListarStocks(string Codigo)
        {
            if (Codigo == null | Codigo == "")
            {
                ModelState.AddModelError(string.Empty, "Debe Ingresar Un codigo");
                ViewBag.Error = "Debe Ingresar Un codigo";
                return RedirectToAction("Index", "StocksLocales", null);

            }
            var idParam = new SqlParameter
            {
                ParameterName = "Codigo",
                Value = Codigo
            };

            var stocksLocales = _context.StockLocales.FromSqlRaw("Stocks_Locales_Propios @Codigo", idParam).ToList();
          
            return View(stocksLocales);
        }


    }
}