using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MkvOL.Data;
using MkvOL.Data.Entities;

namespace MkvOL.Controllers
{
    public class StockEcommerceController : Controller
    {
        private readonly BDSistemasContext _context;

        public StockEcommerceController(BDSistemasContext context)
        {
            _context = context;
        }

        // GET: StockEcommerce
        public async Task<IActionResult> Index()
        {
            var stock = new[] {new StockEcommerceEntity
            {
                CoArticulo = "",
                CoBarra = "",
                Descripcion = "",
                Stock = 0
            } };
            return View(stock);
        }

        [HttpPost]
        public ActionResult Index(string Codigo)
        {
            if (Codigo=="") { return View(); }

            var stock = _context.StockEcommerceEntity.FromSqlRaw($"Select * from Stock_Web_Resultado where CoBarra like '{Codigo}%'");
            return View(stock);

        }
    }
}
