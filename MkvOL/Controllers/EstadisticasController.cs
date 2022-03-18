using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MkvOL.Data;
using MkvOL.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MkvOL.Controllers
{
    public class EstadisticasController : Controller
    {
        private readonly BDSistemasContext _context;
        public EstadisticasController(BDSistemasContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var depositos = _context.DatosDepositos.FromSqlRaw("DatosDepositos").ToList();
            var sucursales = depositos.Where(d => d.Activo == true).Where(p => p.Propio == true).ToList();

            List<SelectListItem> depos = sucursales.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Nombre.ToString(),
                    Value = d.Base.ToString(),
                    Selected = false,

                };

            });

            var fechaDde = $"{DateTime.Today.Year}-{DateTime.Today.Month}-01";
            DateTime dFechaDde = Convert.ToDateTime(fechaDde);
            ViewData["FechaDde"] = dFechaDde.ToString("yyyy-MM-dd");
            ViewData["FechaHta"] = DateTime.Today.ToString("yyyy-MM-dd");
            ViewBag.Depositos = depos;

            return View();
        }

        [HttpPost]
        public IActionResult Index(string selectedBase, DateTime fechaDde, DateTime fechaHta)
        {
            if (selectedBase == null | selectedBase == "")
            {
                ViewBag.Error = "Debe Seleccionar una Sucursal";
                return View();
            }
            if (fechaDde > fechaHta)
            {
                ViewBag.Error = "error en las Fechas";
                return View();
            }


            return RedirectToAction("VentaVendedoras", "Estadisticas", new { selectedBase, fechaDde, fechaHta });
        }


        public IActionResult VentaVendedoras(string selectedBase, DateTime fechaDde, DateTime fechaHta)
        {
            var sqlFechaHta = new SqlParameter
            {
                ParameterName = "FechaHta",
                Value = fechaHta
            };


            var sqlFechaDde = new SqlParameter
            {
                ParameterName = "FechaDde",
                Value = fechaDde
            };

            var sqlBase = new SqlParameter
            {
                ParameterName = "Base",
                Value = selectedBase
            };
            var ventaVendedoras = _context.EstadisticaVendedora.FromSqlRaw("DashBoard_Estadistica_Vendedor @Base, @FechaDde, @FechaHta", sqlBase, sqlFechaDde, sqlFechaHta).ToList();

            return View(ventaVendedoras);
        }

        public async Task<IActionResult> EstadisticaArticulos(int? Anio, int? AnioAnt)
        {
            if (Anio == null) { Anio = DateTime.Today.Year; }
            if (AnioAnt == null) { AnioAnt = DateTime.Today.AddYears(-1).Year; }

            var anio = new SqlParameter
            {
                ParameterName = "Anio",
                Value = Anio
            };
            var anioant = new SqlParameter
            {
                ParameterName = "AnioAnt",
                Value = AnioAnt
            };

            var anualDash = _context.EstadisticaAnual.FromSqlRaw("Dashboard_Estadistica_Anual @Anio, @AnioAnt", anio, anioant).ToList();

            decimal totalArticulosPropios = anualDash.Where(c => c.CoSucursal > 100 && c.CoSucursal < 200).Sum(i => i.Articulos);
            decimal totalArticulosPropiosAnt = anualDash.Where(c => c.CoSucursal > 100 && c.CoSucursal < 200).Sum(i => i.ArticAnt);
            decimal varArticPropios = 0;
            if (totalArticulosPropiosAnt > totalArticulosPropios)
            {
                if (totalArticulosPropios != 0)
                {
                    varArticPropios = -Math.Round(((totalArticulosPropiosAnt - totalArticulosPropios) / totalArticulosPropios) * 100, 0);
                }
            }
            else
            {
                if (totalArticulosPropiosAnt != 0)
                {
                    varArticPropios = Math.Round(((totalArticulosPropios - totalArticulosPropiosAnt) / totalArticulosPropiosAnt) * 100, 0);
                }
            }
            ViewData["TotalArticPropiosAnt"] = totalArticulosPropiosAnt;
            ViewData["TotalArticulosPropios"] = totalArticulosPropios;
            ViewData["VarArticPropios"] = varArticPropios;


            decimal totalArticulosEcommerce = anualDash.Where(c => c.CoSucursal == 80 || c.CoSucursal == 1080).Sum(i => i.Articulos);
            decimal totalArticulosEcommerceAnt = anualDash.Where(c => c.CoSucursal == 80 || c.CoSucursal == 1080).Sum(i => i.ArticAnt);

            decimal varArticEcommerce = 0;
            if (totalArticulosEcommerceAnt > totalArticulosEcommerce)
            {
                if (totalArticulosEcommerce != 0)
                {
                    varArticEcommerce = -Math.Round(((totalArticulosEcommerceAnt - totalArticulosEcommerce) / totalArticulosEcommerce) * 100, 0);
                }
            }
            else
            {
                if (totalArticulosEcommerceAnt != 0)
                {
                    varArticEcommerce = Math.Round(((totalArticulosEcommerce - totalArticulosEcommerceAnt) / totalArticulosEcommerceAnt) * 100, 0);
                }
            }
            ViewData["TotalArticEcommerceAnt"] = totalArticulosEcommerceAnt;
            ViewData["TotalArticulosEcommerce"] = totalArticulosEcommerce;
            ViewData["VarArticEcommerce"] = varArticEcommerce;

            decimal totalArticulosConsig = anualDash.Where(c => c.Consignacion).Sum(i => i.Articulos);
            decimal totalArticulosConsigAnt = anualDash.Where(c => c.Consignacion).Sum(i => i.ArticAnt);

            decimal varArticConsig = 0;
            if (totalArticulosConsigAnt > totalArticulosConsig)
            {
                if (totalArticulosConsig != 0)
                {
                    varArticConsig = -Math.Round(((totalArticulosConsigAnt - totalArticulosConsig) / totalArticulosConsig) * 100, 0);
                }
            }
            else
            {
                if (totalArticulosConsigAnt != 0)
                {
                    varArticConsig = Math.Round(((totalArticulosConsig - totalArticulosConsigAnt) / totalArticulosConsigAnt) * 100, 0);
                }
            }

            ViewData["TotalArticConsigAnt"] = totalArticulosConsigAnt;
            ViewData["TotalArticulosConsig"] = totalArticulosConsig;
            ViewData["VarArticConsig"] = varArticConsig;

            decimal totalArticulosFranq = anualDash.Where(c => c.Consignacion == false && c.Franquicia == true).Sum(i => i.Articulos);
            decimal totalArticulosFranqAnt = anualDash.Where(c => c.Consignacion == false && c.Franquicia == true).Sum(i => i.ArticAnt);

            decimal varArticFranq = 0;
            if (totalArticulosFranqAnt > totalArticulosFranq)
            {
                if (totalArticulosFranq != 0)
                {
                    varArticFranq = -Math.Round(((totalArticulosFranqAnt - totalArticulosFranq) / totalArticulosFranq) * 100, 0);
                }
            }
            else
            {
                if (totalArticulosFranqAnt != 0)
                {
                    varArticFranq = Math.Round(((totalArticulosFranq - totalArticulosFranqAnt) / totalArticulosFranqAnt) * 100, 0);
                }
            }

            ViewData["TotalArticFranqAnt"] = totalArticulosFranqAnt;
            ViewData["TotalArticulosFranq"] = totalArticulosFranq;
            ViewData["VarArticFranq"] = varArticFranq;
            ViewData["Anio"] = Anio;
            ViewData["AnioAnt"] = AnioAnt;

            return View(anualDash);
        }

        [HttpPost]
        public async Task<IActionResult> EstadisticaArticulos(int Anio, int AnioAnt)
        {

            var anio = new SqlParameter
            {
                ParameterName = "Anio",
                Value = Anio
            };
            var anioant = new SqlParameter
            {
                ParameterName = "AnioAnt",
                Value = AnioAnt
            };

            var anualDash = _context.EstadisticaAnual.FromSqlRaw("Dashboard_Estadistica_Anual @Anio, @AnioAnt", anio, anioant).ToList();

            decimal totalArticulosPropios = anualDash.Where(c => c.CoSucursal > 100 && c.CoSucursal < 200).Sum(i => i.Articulos);
            decimal totalArticulosPropiosAnt = anualDash.Where(c => c.CoSucursal > 100 && c.CoSucursal < 200).Sum(i => i.ArticAnt);
            decimal varArticPropios = 0;
            if (totalArticulosPropiosAnt > totalArticulosPropios)
            {
                if (totalArticulosPropios != 0)
                {
                    varArticPropios = -Math.Round(((totalArticulosPropiosAnt - totalArticulosPropios) / totalArticulosPropios) * 100, 0);
                }
            }
            else
            {
                if (totalArticulosPropiosAnt != 0)
                {
                    varArticPropios = Math.Round(((totalArticulosPropios - totalArticulosPropiosAnt) / totalArticulosPropiosAnt) * 100, 0);
                }
            }
            ViewData["TotalArticPropiosAnt"] = totalArticulosPropiosAnt;
            ViewData["TotalArticulosPropios"] = totalArticulosPropios;
            ViewData["VarArticPropios"] = varArticPropios;


            decimal totalArticulosEcommerce = anualDash.Where(c => c.CoSucursal == 80 || c.CoSucursal == 1080).Sum(i => i.Articulos);
            decimal totalArticulosEcommerceAnt = anualDash.Where(c => c.CoSucursal == 80 || c.CoSucursal == 1080).Sum(i => i.ArticAnt);

            decimal varArticEcommerce = 0;
            if (totalArticulosEcommerceAnt > totalArticulosEcommerce)
            {
                if (totalArticulosEcommerce != 0)
                {
                    varArticEcommerce = -Math.Round(((totalArticulosEcommerceAnt - totalArticulosEcommerce) / totalArticulosEcommerce) * 100, 0);
                }
            }
            else
            {
                if (totalArticulosEcommerceAnt != 0)
                {
                    varArticEcommerce = Math.Round(((totalArticulosEcommerce - totalArticulosEcommerceAnt) / totalArticulosEcommerceAnt) * 100, 0);
                }
            }
            ViewData["TotalArticEcommerceAnt"] = totalArticulosEcommerceAnt;
            ViewData["TotalArticulosEcommerce"] = totalArticulosEcommerce;
            ViewData["VarArticEcommerce"] = varArticEcommerce;

            decimal totalArticulosConsig = anualDash.Where(c => c.Consignacion).Sum(i => i.Articulos);
            decimal totalArticulosConsigAnt = anualDash.Where(c => c.Consignacion).Sum(i => i.ArticAnt);

            decimal varArticConsig = 0;
            if (totalArticulosConsigAnt > totalArticulosConsig)
            {
                if (totalArticulosConsig != 0)
                {
                    varArticConsig = -Math.Round(((totalArticulosConsigAnt - totalArticulosConsig) / totalArticulosConsig) * 100, 0);
                }
            }
            else
            {
                if (totalArticulosConsigAnt != 0)
                {
                    varArticConsig = Math.Round(((totalArticulosConsig - totalArticulosConsigAnt) / totalArticulosConsigAnt) * 100, 0);
                }
            }

            ViewData["TotalArticConsigAnt"] = totalArticulosConsigAnt;
            ViewData["TotalArticulosConsig"] = totalArticulosConsig;
            ViewData["VarArticConsig"] = varArticConsig;

            decimal totalArticulosFranq = anualDash.Where(c => c.Consignacion == false && c.Franquicia == true).Sum(i => i.Articulos);
            decimal totalArticulosFranqAnt = anualDash.Where(c => c.Consignacion == false && c.Franquicia == true).Sum(i => i.ArticAnt);

            decimal varArticFranq = 0;
            if (totalArticulosFranqAnt > totalArticulosFranq)
            {
                if (totalArticulosFranq != 0)
                {
                    varArticFranq = -Math.Round(((totalArticulosFranqAnt - totalArticulosFranq) / totalArticulosFranq) * 100, 0);
                }
            }
            else
            {
                if (totalArticulosFranqAnt != 0)
                {
                    varArticFranq = Math.Round(((totalArticulosFranq - totalArticulosFranqAnt) / totalArticulosFranqAnt) * 100, 0);
                }
            }

            ViewData["TotalArticFranqAnt"] = totalArticulosFranqAnt;
            ViewData["TotalArticulosFranq"] = totalArticulosFranq;
            ViewData["VarArticFranq"] = varArticFranq;
            ViewData["Anio"] = Anio;
            ViewData["AnioAnt"] = AnioAnt;

            return View(anualDash);
        }

        public async Task<IActionResult> SucDetailArtic(int Id, int Anio, int AnioAnt)
        {
            var anio = new SqlParameter
            {
                ParameterName = "Anio",
                Value = Anio
            };
            var anioant = new SqlParameter
            {
                ParameterName = "AnioAnt",
                Value = AnioAnt
            };
            var coSucursal = new SqlParameter
            {
                ParameterName = "CoSucursal",
                Value = Id
            };
            var anualDash = _context.EstadAnualSucursal.FromSqlRaw("Dashboard_Estadistica_Anual_Sucursal @Anio, @AnioAnt, @CoSucursal", anio, anioant, coSucursal).ToList();
            var sucursal = anualDash.FirstOrDefault();
            var totalArticulos = anualDash.Sum(a => a.Articulos);
            var totalArticAnt = anualDash.Sum(a => a.ArticAnt);
            decimal varArtic = 0;
            if (totalArticAnt > totalArticulos)
            {
                if (totalArticulos != 0)
                {
                    varArtic = -Math.Round(((totalArticAnt - totalArticulos) / totalArticulos) * 100, 0);
                }
            }
            else
            {
                if (totalArticAnt != 0)
                {
                    varArtic = Math.Round(((totalArticulos - totalArticAnt) / totalArticAnt) * 100, 0);
                }
            }
            ViewData["Sucursal"] = sucursal.Nombre;
            ViewData["CoSucursal"] = Id;
            ViewData["Anio"] = Anio;
            ViewData["AnioAnt"] = AnioAnt;
            ViewData["TotalArticulos"] = totalArticulos;
            ViewData["TotalArticAnt"] = totalArticAnt;
            ViewData["VarArtic"] = varArtic;
            return View(anualDash);
        }

        public async Task<IActionResult> FamiliasMensual(int Id, int Anio, int AnioAnt, int Mes, string MesTexto, string Sucursal)
        {
            DateTime fechaDde1, fechaDde2, fechaHta1, fechaHta2;
            fechaDde1 = Convert.ToDateTime($"01/{(Mes).ToString("00")}/{Anio}");
            fechaDde2 = Convert.ToDateTime($"01/{(Mes).ToString("00")}/{AnioAnt}");
            if (Mes == DateTime.Today.Month & Anio == DateTime.Today.Year)
            {
                fechaHta1 = Convert.ToDateTime($"{(DateTime.Today.Day).ToString("00")}/{(Mes).ToString("00")}/{Anio}");
            }
            else
            {
                fechaHta1 = Convert.ToDateTime($"01/{(Mes + 1).ToString("00")}/{Anio}").AddDays(-1);
            }
            if (Mes == DateTime.Today.Month & AnioAnt == DateTime.Today.Year)
            {
                fechaHta2 = Convert.ToDateTime($"{(DateTime.Today.Day).ToString("00")}/{(Mes).ToString("00")}/{AnioAnt}");
            }
            else
            {
                fechaHta2 = Convert.ToDateTime($"01/{(Mes + 1).ToString("00")}/{AnioAnt}").AddDays(-1);

            }

            var pFechaDde1 = new SqlParameter
            {
                ParameterName = "FechaDde1",
                Value = fechaDde1
            };
            var pFechaDde2 = new SqlParameter
            {
                ParameterName = "FechaDde2",
                Value = fechaDde2
            };
            var pFechaHta1 = new SqlParameter
            {
                ParameterName = "FechaHta1",
                Value = fechaHta1
            };
            var pFechaHta2 = new SqlParameter
            {
                ParameterName = "FechaHta2",
                Value = fechaHta2
            };
            var coSucursal = new SqlParameter
            {
                ParameterName = "CoSucursal",
                Value = Id
            };
            var usuarioID = new SqlParameter
            {
                ParameterName = "UsuarioID",
                Value = User.Identity.Name
            };

            var estFamilia = _context.EstadisticaFamilia.FromSqlRaw("Estadistica_Familia_Articulos @CoSucursal, @FechaDde1, @FechaHta1, @FechaDde2, @FechaHta2, @UsuarioID", coSucursal, pFechaDde1, pFechaHta1, pFechaDde2, pFechaHta2, usuarioID).ToList();

            var totalAnio1 = estFamilia.Sum(c => c.Anio1);
            var totalAnio2 = estFamilia.Sum(c => c.Anio2);

            ViewData["Mes"] = MesTexto;
            ViewData["NumeroMes"] = Mes;
            ViewData["Sucursal"] = Sucursal;
            ViewData["CoSucursal"] = Id;
            ViewData["Anio"] = Anio;
            ViewData["AnioAnt"] = AnioAnt;
            ViewData["TotalAño1"] = totalAnio1;
            ViewData["TotalAño2"] = totalAnio2;
            return View(estFamilia);
        }

        public async Task<IActionResult> DetalleFamilia(int Id, int Anio, int AnioAnt, string CoFamilia, int Mes, string MesTexto, string Sucursal)
        {
            DateTime fechaDde1, fechaDde2, fechaHta1, fechaHta2;
            fechaDde1 = Convert.ToDateTime($"01/{(Mes).ToString("00")}/{Anio}");
            fechaDde2 = Convert.ToDateTime($"01/{(Mes).ToString("00")}/{AnioAnt}");
            if (Mes == DateTime.Today.Month & Anio == DateTime.Today.Year)
            {
                fechaHta1 = Convert.ToDateTime($"{(DateTime.Today.Day).ToString("00")}/{(Mes).ToString("00")}/{Anio}");
            }
            else
            {
                fechaHta1 = Convert.ToDateTime($"01/{(Mes + 1).ToString("00")}/{Anio}").AddDays(-1);
            }
            if (Mes == DateTime.Today.Month & AnioAnt == DateTime.Today.Year)
            {
                fechaHta2 = Convert.ToDateTime($"{(DateTime.Today.Day).ToString("00")}/{(Mes).ToString("00")}/{AnioAnt}");
            }
            else
            {
                fechaHta2 = Convert.ToDateTime($"01/{(Mes + 1).ToString("00")}/{AnioAnt}").AddDays(-1);

            }

            var pCoFamilia = new SqlParameter
            {
                ParameterName = "CoFamilia",
                Value = CoFamilia
            };
            var pFechaDde1 = new SqlParameter
            {
                ParameterName = "FechaDde1",
                Value = fechaDde1
            };
            var pFechaDde2 = new SqlParameter
            {
                ParameterName = "FechaDde2",
                Value = fechaDde2
            };
            var pFechaHta1 = new SqlParameter
            {
                ParameterName = "FechaHta1",
                Value = fechaHta1
            };
            var pFechaHta2 = new SqlParameter
            {
                ParameterName = "FechaHta2",
                Value = fechaHta2
            };
            var coSucursal = new SqlParameter
            {
                ParameterName = "CoSucursal",
                Value = Id
            };
            var usuarioID = new SqlParameter
            {
                ParameterName = "UsuarioID",
                Value = User.Identity.Name
            };

            var estArticulo = _context.EstadisticaArticulo.FromSqlRaw("Estadistica_Familia_Articulos_detalle @CoSucursal, @FechaDde1, @FechaHta1, @FechaDde2, @FechaHta2, @CoFamilia, @UsuarioID", coSucursal, pFechaDde1, pFechaHta1, pFechaDde2, pFechaHta2, pCoFamilia, usuarioID).ToList();

            var totalAnio1 = estArticulo.Sum(c => c.Anio1);
            var totalAnio2 = estArticulo.Sum(c => c.Anio2);

            ViewData["Mes"] = MesTexto;
            ViewData["NumeroMes"] = Mes;
            ViewData["Sucursal"] = Sucursal;
            ViewData["CoSucursal"] = Id;
            ViewData["Anio"] = Anio;
            ViewData["AnioAnt"] = AnioAnt;
            ViewData["TotalAño1"] = totalAnio1;
            ViewData["TotalAño2"] = totalAnio2;
            return View(estArticulo);
        }
    }

}

