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
    [Authorize]
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
            var totalDiaPropios = ventaDash.Where(t => t.CoSucursal > 100 && t.CoSucursal < 200).Sum(v => v.VentaDia);
            var totalPronPropios = ventaDash.Where(t => t.CoSucursal > 100 && t.CoSucursal < 200).Sum(v => v.Pronostico);
            decimal totalCumplDiaPropios = 0;
            if (totalPronPropios != 0 & totalDiaPropios != 0)
            {
                if (totalPronPropios > totalDiaPropios)
                {
                    totalCumplDiaPropios = -Math.Round((totalPronPropios - totalDiaPropios) / totalPronPropios * 100, 2);
                }
                else
                {
                    totalCumplDiaPropios = Math.Round((totalDiaPropios - totalPronPropios) / totalDiaPropios * 100, 2);
                }
            }
            var totalDiaAnioAntPropios = ventaDash.Where(t => t.CoSucursal > 100 && t.CoSucursal < 200).Sum(v => v.VentaAnt);
            var totalAcumAnioAntPropios = ventaDash.Where(t => t.CoSucursal > 100 && t.CoSucursal < 200).Sum(v => v.AcumAnt);
            var acumPropios = ventaDash.Where(t => t.CoSucursal > 100 && t.CoSucursal < 200).Sum(v => v.Acumulado);
            var pronAcumPropios = ventaDash.Where(t => t.CoSucursal > 100 && t.CoSucursal < 200).Sum(v => v.PronAcum);

            var totalDiaEcommerce = ventaDash.Where(t => t.CoSucursal == 80 | t.CoSucursal == 1080).Sum(v => v.VentaDia);
            var totalPronEcommerce = ventaDash.Where(t => t.CoSucursal == 80 | t.CoSucursal == 1080).Sum(v => v.Pronostico);
            decimal totalCumplDiaEcommerce = 0;
            if (totalPronEcommerce != 0 & totalDiaEcommerce != 0)
            {
                if (totalPronEcommerce > totalDiaEcommerce)
                {
                    totalCumplDiaEcommerce = -Math.Round((totalPronEcommerce - totalDiaEcommerce) / totalPronEcommerce * 100, 2);
                }
                else
                {
                    totalCumplDiaEcommerce = Math.Round((totalDiaEcommerce - totalPronEcommerce) / totalDiaEcommerce * 100, 2);
                }
            }
         ;
            var acumEcommerce = ventaDash.Where(t => t.CoSucursal == 80 || t.CoSucursal == 1080).Sum(v => v.Acumulado);
            var pronAcumEcommerce = ventaDash.Where(t => t.CoSucursal == 80 || t.CoSucursal == 1080).Sum(v => v.PronAcum);
            var totalDiaAnioAntEcommerce = ventaDash.Where(t => t.CoSucursal == 80 || t.CoSucursal == 1080).Sum(v => v.VentaAnt);
            var totalAcumAnioAntEcommerce = ventaDash.Where(t => t.CoSucursal == 80 || t.CoSucursal == 1080).Sum(v => v.AcumAnt);

            var totalDiaConsignacion = ventaDash.Where(t => t.Consignacion == true).Sum(v => v.VentaDia);
            var totalPronConsignacion = ventaDash.Where(t => t.Consignacion == true).Sum(v => v.Pronostico);
            decimal totalCumplDiaConsignacion = 0;
            if (totalDiaConsignacion != 0 & totalPronConsignacion != 0)
            {
                if (totalPronConsignacion > totalDiaConsignacion)
                {
                    totalCumplDiaConsignacion = -Math.Round((totalPronConsignacion - totalDiaConsignacion) / totalPronConsignacion * 100, 2);
                }
                else
                {
                    totalCumplDiaConsignacion = Math.Round((totalDiaConsignacion - totalPronConsignacion) / totalDiaConsignacion * 100, 2);
                }
            }
            var acumConsignacion = ventaDash.Where(t => t.Consignacion == true).Sum(v => v.Acumulado);
            var pronAcumConsignacion = ventaDash.Where(t => t.Consignacion == true).Sum(v => v.PronAcum);
            var totalDiaAnioAntConsignacion = ventaDash.Where(t => t.Consignacion == true).Sum(v => v.VentaAnt);
            var totalAcumAnioAntConsignacion = ventaDash.Where(t => t.Consignacion == true).Sum(v => v.AcumAnt);

            var totalDiaFranquicias = ventaDash.Where(t => t.Franquicia == true && t.Consignacion == false).Sum(v => v.VentaDia);
            var totalPronFranquicias = ventaDash.Where(t => t.Franquicia == true && t.Consignacion == false).Sum(v => v.Pronostico);
            decimal totalCumplDiaFranquicias = 0;
            if (totalPronFranquicias != 0 & totalDiaFranquicias != 0)
            {
                if (totalPronFranquicias > totalDiaFranquicias)
                {
                    totalCumplDiaFranquicias = -Math.Round((totalPronFranquicias - totalDiaFranquicias) / totalPronFranquicias * 100, 2);
                }
                else
                {
                    totalCumplDiaFranquicias = Math.Round((totalDiaFranquicias - totalPronFranquicias) / totalDiaFranquicias * 100, 2);
                }
            }
            var acumFranquicias = ventaDash.Where(t => t.Franquicia == true && t.Consignacion == false).Sum(v => v.Acumulado);
            var pronAcumFranquicias = ventaDash.Where(t => t.Franquicia == true && t.Consignacion == false).Sum(v => v.PronAcum);
            var totalDiaAnioAntFranquicias = ventaDash.Where(t => t.Franquicia == true && t.Consignacion == false).Sum(v => v.VentaAnt);
            var totalAcumAnioAntFranquicias = ventaDash.Where(t => t.Franquicia == true && t.Consignacion == false).Sum(v => v.AcumAnt);

            var acumTotal = acumPropios + acumConsignacion + acumEcommerce;
            var totalVentaDia = totalDiaConsignacion + totalDiaPropios + totalDiaEcommerce;
            var totalDiaPropEcom = totalDiaPropios + totalDiaEcommerce;
            var acumPropEcom = acumPropios + acumEcommerce;

            decimal cumplAcumPropios = 0;
            if (acumPropios != 0 & pronAcumPropios != 0)
            {
                if (pronAcumPropios > acumPropios)
                {
                    cumplAcumPropios = -Math.Round((pronAcumPropios - acumPropios) / pronAcumPropios * 100, 2);
                }
                else
                {
                    cumplAcumPropios = Math.Round((acumPropios - pronAcumPropios) / acumPropios * 100, 2);
                }
            }
            decimal cumplAcumEcommerce = 0;
            if (acumEcommerce != 0 & pronAcumEcommerce != 0)
            {
                if (pronAcumEcommerce > acumEcommerce)
                {
                    cumplAcumEcommerce = -Math.Round((pronAcumEcommerce - acumEcommerce) / pronAcumEcommerce * 100, 2);
                }
                else
                {
                    cumplAcumEcommerce = Math.Round((acumEcommerce - pronAcumEcommerce) / acumEcommerce * 100, 2);
                }
            }
            decimal cumplAcumConsignacion = 0;
            if (acumConsignacion != 0 & pronAcumConsignacion != 0)
            {
                if (pronAcumConsignacion > acumConsignacion)
                {
                    cumplAcumConsignacion = -Math.Round((pronAcumConsignacion - acumConsignacion) / pronAcumConsignacion * 100, 2);
                }
                else
                {
                    cumplAcumConsignacion = Math.Round((acumConsignacion - pronAcumConsignacion) / acumConsignacion * 100, 2);
                }
            }
            decimal cumplAcumFranquicias = 0;
            if (acumFranquicias != 0 & pronAcumFranquicias != 0)
            {
                if (pronAcumFranquicias > acumFranquicias)
                {
                    cumplAcumFranquicias = -Math.Round((pronAcumFranquicias - acumFranquicias) / pronAcumFranquicias * 100, 2);
                }
                else
                {
                    cumplAcumFranquicias = Math.Round((acumFranquicias - pronAcumFranquicias) / acumFranquicias * 100, 2);
                }
            }

            ViewData["TotalPropiosEcom"] = totalDiaPropEcom.ToString("C2");
            ViewData["TotalDiaPropios"] = totalDiaPropios.ToString("C2");
            ViewData["TotalPronPropios"] = totalPronPropios.ToString("C2");
            ViewData["TotalCumplDiaPropios"] = totalCumplDiaPropios.ToString("N0");
            ViewData["AcumPropios"] = acumPropios.ToString("C2");
            ViewData["PronAcumPropios"] = pronAcumPropios.ToString("C2");
            ViewData["CumplAcumPropios"] = cumplAcumPropios.ToString("N0");
            ViewData["TotalDiaAnioAntPropios"] = totalDiaAnioAntPropios.ToString("C2");
            ViewData["TotalAcumAnioAntPropios"] = totalAcumAnioAntPropios.ToString("C2");
            ViewData["TotalConsignacion"] = totalDiaConsignacion.ToString("C2");
            ViewData["TotalPronConsignacion"] = totalPronConsignacion.ToString("C2");
            ViewData["TotalCumplDiaConsignacion"] = totalCumplDiaConsignacion.ToString("N0");
            ViewData["AcumConsignacion"] = acumConsignacion.ToString("C2");
            ViewData["PronAcumConsignacion"] = pronAcumConsignacion.ToString("C2");
            ViewData["CumplAcumConsignacion"] = cumplAcumConsignacion.ToString("N0");
            ViewData["TotalDiaAnioAntConsignacion"] = totalDiaAnioAntConsignacion.ToString("C2");
            ViewData["TotalAcumAnioAntConsignacion"] = totalAcumAnioAntConsignacion.ToString("C2");
            ViewData["TotalFranquicias"] = totalDiaFranquicias.ToString("C2");
            ViewData["TotalPronFranquicias"] = totalPronFranquicias.ToString("C2");
            ViewData["TotalCumplDiaFranquicias"] = totalCumplDiaFranquicias.ToString("N0");
            ViewData["AcumFranquicias"] = acumFranquicias.ToString("C2");
            ViewData["PronAcumFranquicias"] = pronAcumFranquicias.ToString("C2");
            ViewData["CumplAcumFranquicias"] = cumplAcumFranquicias.ToString("N0");
            ViewData["TotalDiaAnioAntFranquicias"] = totalDiaAnioAntFranquicias.ToString("C2");
            ViewData["TotalAcumAnioAntFranquicias"] = totalAcumAnioAntFranquicias.ToString("C2");
            ViewData["TotalEcommerce"] = totalDiaEcommerce.ToString("C2");
            ViewData["TotalPronEcommerce"] = totalPronEcommerce.ToString("C2");
            ViewData["TotalCumplDiaEcommerce"] = totalCumplDiaEcommerce.ToString("N0");
            ViewData["AcumEcommerce"] = acumEcommerce.ToString("C2");
            ViewData["PronAcumEcommerce"] = pronAcumEcommerce.ToString("C2");
            ViewData["TotalDiaAnioAntEcommerce"] = totalDiaAnioAntEcommerce.ToString("C2");
            ViewData["TotalAcumAnioAntEcommerce"] = totalAcumAnioAntEcommerce.ToString("C2");
            ViewData["CumplAcumEcommerce"] = cumplAcumEcommerce.ToString("N0");
            ViewData["TotalDia"] = totalVentaDia.ToString("C2");
            ViewData["AcumPropiosEcom"] = acumPropEcom.ToString("C2");
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

            var totalDiaPropios = ventaDash.Where(t => t.CoSucursal > 100 && t.CoSucursal < 200).Sum(v => v.VentaDia);
            var totalPronPropios = ventaDash.Where(t => t.CoSucursal > 100 && t.CoSucursal < 200).Sum(v => v.Pronostico);
            decimal totalCumplDiaPropios = 0;
            if (totalPronPropios != 0 & totalDiaPropios != 0)
            {
                if (totalPronPropios > totalDiaPropios)
                {
                    totalCumplDiaPropios = -Math.Round((totalPronPropios - totalDiaPropios) / totalPronPropios * 100, 2);
                }
                else
                {
                    totalCumplDiaPropios = Math.Round((totalDiaPropios - totalPronPropios) / totalDiaPropios * 100, 2);
                }
            }
            var totalDiaAnioAntPropios = ventaDash.Where(t => t.CoSucursal > 100 && t.CoSucursal < 200).Sum(v => v.VentaAnt);
            var totalAcumAnioAntPropios = ventaDash.Where(t => t.CoSucursal > 100 && t.CoSucursal < 200).Sum(v => v.AcumAnt);
            var acumPropios = ventaDash.Where(t => t.CoSucursal > 100 && t.CoSucursal < 200).Sum(v => v.Acumulado);
            var pronAcumPropios = ventaDash.Where(t => t.CoSucursal > 100 && t.CoSucursal < 200).Sum(v => v.PronAcum);

            var totalDiaEcommerce = ventaDash.Where(t => t.CoSucursal == 80 | t.CoSucursal == 1080).Sum(v => v.VentaDia);
            var totalPronEcommerce = ventaDash.Where(t => t.CoSucursal == 80 | t.CoSucursal == 1080).Sum(v => v.Pronostico);
            decimal totalCumplDiaEcommerce = 0;
            if (totalPronEcommerce != 0 & totalDiaEcommerce != 0)
            {
                if (totalPronEcommerce > totalDiaEcommerce)
                {
                    totalCumplDiaEcommerce = -Math.Round((totalPronEcommerce - totalDiaEcommerce) / totalPronEcommerce * 100, 2);
                }
                else
                {
                    totalCumplDiaEcommerce = Math.Round((totalDiaEcommerce - totalPronEcommerce) / totalDiaEcommerce * 100, 2);
                }
            }
         ;
            var acumEcommerce = ventaDash.Where(t => t.CoSucursal == 80 || t.CoSucursal == 1080).Sum(v => v.Acumulado);
            var pronAcumEcommerce = ventaDash.Where(t => t.CoSucursal == 80 || t.CoSucursal == 1080).Sum(v => v.PronAcum);
            var totalDiaAnioAntEcommerce = ventaDash.Where(t => t.CoSucursal == 80 || t.CoSucursal == 1080).Sum(v => v.VentaAnt);
            var totalAcumAnioAntEcommerce = ventaDash.Where(t => t.CoSucursal == 80 || t.CoSucursal == 1080).Sum(v => v.AcumAnt);

            var totalDiaConsignacion = ventaDash.Where(t => t.Consignacion == true).Sum(v => v.VentaDia);
            var totalPronConsignacion = ventaDash.Where(t => t.Consignacion == true).Sum(v => v.Pronostico);
            decimal totalCumplDiaConsignacion = 0;
            if (totalDiaConsignacion != 0 & totalPronConsignacion != 0)
            {
                if (totalPronConsignacion > totalDiaConsignacion)
                {
                    totalCumplDiaConsignacion = -Math.Round((totalPronConsignacion - totalDiaConsignacion) / totalPronConsignacion * 100, 2);
                }
                else
                {
                    totalCumplDiaConsignacion = Math.Round((totalDiaConsignacion - totalPronConsignacion) / totalDiaConsignacion * 100, 2);
                }
            }
            var acumConsignacion = ventaDash.Where(t => t.Consignacion == true).Sum(v => v.Acumulado);
            var pronAcumConsignacion = ventaDash.Where(t => t.Consignacion == true).Sum(v => v.PronAcum);
            var totalDiaAnioAntConsignacion = ventaDash.Where(t => t.Consignacion == true).Sum(v => v.VentaAnt);
            var totalAcumAnioAntConsignacion = ventaDash.Where(t => t.Consignacion == true).Sum(v => v.AcumAnt);

            var totalDiaFranquicias = ventaDash.Where(t => t.Franquicia == true && t.Consignacion == false).Sum(v => v.VentaDia);
            var totalPronFranquicias = ventaDash.Where(t => t.Franquicia == true && t.Consignacion == false).Sum(v => v.Pronostico);
            decimal totalCumplDiaFranquicias = 0;
            if (totalPronFranquicias != 0 & totalDiaFranquicias != 0)
            {
                if (totalPronFranquicias > totalDiaFranquicias)
                {
                    totalCumplDiaFranquicias = -Math.Round((totalPronFranquicias - totalDiaFranquicias) / totalPronFranquicias * 100, 2);
                }
                else
                {
                    totalCumplDiaFranquicias = Math.Round((totalDiaFranquicias - totalPronFranquicias) / totalDiaFranquicias * 100, 2);
                }
            }
            var acumFranquicias = ventaDash.Where(t => t.Franquicia == true && t.Consignacion == false).Sum(v => v.Acumulado);
            var pronAcumFranquicias = ventaDash.Where(t => t.Franquicia == true && t.Consignacion == false).Sum(v => v.PronAcum);
            var totalDiaAnioAntFranquicias = ventaDash.Where(t => t.Franquicia == true && t.Consignacion == false).Sum(v => v.VentaAnt);
            var totalAcumAnioAntFranquicias = ventaDash.Where(t => t.Franquicia == true && t.Consignacion == false).Sum(v => v.AcumAnt);

            var acumTotal = acumPropios + acumConsignacion + acumEcommerce;
            var totalVentaDia = totalDiaConsignacion + totalDiaPropios + totalDiaEcommerce;
            var totalDiaPropEcom = totalDiaPropios + totalDiaEcommerce;
            var acumPropEcom = acumPropios + acumEcommerce;

            decimal cumplAcumPropios = 0;
            if (acumPropios != 0 & pronAcumPropios != 0)
            {
                if (pronAcumPropios > acumPropios)
                {
                    cumplAcumPropios = -Math.Round((pronAcumPropios - acumPropios) / pronAcumPropios * 100, 2);
                }
                else
                {
                    cumplAcumPropios = Math.Round((acumPropios - pronAcumPropios) / acumPropios * 100, 2);
                }
            }
            decimal cumplAcumEcommerce = 0;
            if (acumEcommerce != 0 & pronAcumEcommerce != 0)
            {
                if (pronAcumEcommerce > acumEcommerce)
                {
                    cumplAcumEcommerce = -Math.Round((pronAcumEcommerce - acumEcommerce) / pronAcumEcommerce * 100, 2);
                }
                else
                {
                    cumplAcumEcommerce = Math.Round((acumEcommerce - pronAcumEcommerce) / acumEcommerce * 100, 2);
                }
            }
            decimal cumplAcumConsignacion = 0;
            if (acumConsignacion != 0 & pronAcumConsignacion != 0)
            {
                if (pronAcumConsignacion > acumConsignacion)
                {
                    cumplAcumConsignacion = -Math.Round((pronAcumConsignacion - acumConsignacion) / pronAcumConsignacion * 100, 2);
                }
                else
                {
                    cumplAcumConsignacion = Math.Round((acumConsignacion - pronAcumConsignacion) / acumConsignacion * 100, 2);
                }
            }
            decimal cumplAcumFranquicias = 0;
            if (acumFranquicias != 0 & pronAcumFranquicias != 0)
            {
                if (pronAcumFranquicias > acumFranquicias)
                {
                    cumplAcumFranquicias = -Math.Round((pronAcumFranquicias - acumFranquicias) / pronAcumFranquicias * 100, 2);
                }
                else
                {
                    cumplAcumFranquicias = Math.Round((acumFranquicias - pronAcumFranquicias) / acumFranquicias * 100, 2);
                }
            }

            ViewData["TotalPropiosEcom"] = totalDiaPropEcom.ToString("C2");
            ViewData["TotalDiaPropios"] = totalDiaPropios.ToString("C2");
            ViewData["TotalPronPropios"] = totalPronPropios.ToString("C2");
            ViewData["TotalCumplDiaPropios"] = totalCumplDiaPropios.ToString("N0");
            ViewData["AcumPropios"] = acumPropios.ToString("C2");
            ViewData["PronAcumPropios"] = pronAcumPropios.ToString("C2");
            ViewData["CumplAcumPropios"] = cumplAcumPropios.ToString("N0");
            ViewData["TotalDiaAnioAntPropios"] = totalDiaAnioAntPropios.ToString("C2");
            ViewData["TotalAcumAnioAntPropios"] = totalAcumAnioAntPropios.ToString("C2");
            ViewData["TotalConsignacion"] = totalDiaConsignacion.ToString("C2");
            ViewData["TotalPronConsignacion"] = totalPronConsignacion.ToString("C2");
            ViewData["TotalCumplDiaConsignacion"] = totalCumplDiaConsignacion.ToString("N0");
            ViewData["AcumConsignacion"] = acumConsignacion.ToString("C2");
            ViewData["PronAcumConsignacion"] = pronAcumConsignacion.ToString("C2");
            ViewData["CumplAcumConsignacion"] = cumplAcumConsignacion.ToString("N0");
            ViewData["TotalDiaAnioAntConsignacion"] = totalDiaAnioAntConsignacion.ToString("C2");
            ViewData["TotalAcumAnioAntConsignacion"] = totalAcumAnioAntConsignacion.ToString("C2");
            ViewData["TotalFranquicias"] = totalDiaFranquicias.ToString("C2");
            ViewData["TotalPronFranquicias"] = totalPronFranquicias.ToString("C2");
            ViewData["TotalCumplDiaFranquicias"] = totalCumplDiaFranquicias.ToString("N0");
            ViewData["AcumFranquicias"] = acumFranquicias.ToString("C2");
            ViewData["PronAcumFranquicias"] = pronAcumFranquicias.ToString("C2");
            ViewData["CumplAcumFranquicias"] = cumplAcumFranquicias.ToString("N0");
            ViewData["TotalDiaAnioAntFranquicias"] = totalDiaAnioAntFranquicias.ToString("C2");
            ViewData["TotalAcumAnioAntFranquicias"] = totalAcumAnioAntFranquicias.ToString("C2");
            ViewData["TotalEcommerce"] = totalDiaEcommerce.ToString("C2");
            ViewData["TotalPronEcommerce"] = totalPronEcommerce.ToString("C2");
            ViewData["TotalCumplDiaEcommerce"] = totalCumplDiaEcommerce.ToString("N0");
            ViewData["AcumEcommerce"] = acumEcommerce.ToString("C2");
            ViewData["PronAcumEcommerce"] = pronAcumEcommerce.ToString("C2");
            ViewData["TotalDiaAnioAntEcommerce"] = totalDiaAnioAntEcommerce.ToString("C2");
            ViewData["TotalAcumAnioAntEcommerce"] = totalAcumAnioAntEcommerce.ToString("C2");
            ViewData["CumplAcumEcommerce"] = cumplAcumEcommerce.ToString("N0");
            ViewData["TotalDia"] = totalVentaDia.ToString("C2");
            ViewData["AcumPropiosEcom"] = acumPropEcom.ToString("C2");
            ViewData["AcumConsignacion"] = acumConsignacion.ToString("C2");
            ViewData["TotalAcum"] = acumTotal.ToString("C2");
            ViewData["Fecha"] = fechaConsulta.ToString("yyyy-MM-dd");

            return View(ventaDash);
        }

        public async Task<IActionResult> VentaAnual(int? Anio, int? AnioAnt)
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

            #region "Propios"

            decimal totalImportePropios = anualDash.Where(c => c.CoSucursal > 100 && c.CoSucursal < 200).Sum(i => i.Importe);
            decimal totalImpAntPropios = anualDash.Where(c => c.CoSucursal > 100 && c.CoSucursal < 200).Sum(i => i.ImporteAnt);
            decimal totalArticulosPropios = anualDash.Where(c => c.CoSucursal > 100 && c.CoSucursal < 200).Sum(i => i.Articulos);
            decimal totalArticulosPropiosAnt = anualDash.Where(c => c.CoSucursal > 100 && c.CoSucursal < 200).Sum(i => i.ArticAnt);
            decimal totalTicketsPropios = anualDash.Where(c => c.CoSucursal > 100 && c.CoSucursal < 200).Sum(i => i.Tickets);
            decimal totalTicketsPropiosAnt = anualDash.Where(c => c.CoSucursal > 100 && c.CoSucursal < 200).Sum(i => i.TicketsAnt);

            decimal totalTkPromPropios = 0;
            decimal totalAxTPropios = 0;
            if (totalTicketsPropios != 0)
            {
                totalTkPromPropios = totalImportePropios / totalTicketsPropios;
                totalAxTPropios = totalArticulosPropios / totalTicketsPropios;
            }
            decimal totalTkPromPropiosAnt = 0;
            decimal totalAxTPropiosAnt = 0;
            if (totalTicketsPropiosAnt != 0)
            {
                totalTkPromPropiosAnt = totalImpAntPropios / totalTicketsPropiosAnt;
                totalAxTPropiosAnt = totalArticulosPropiosAnt / totalTicketsPropiosAnt;
            }

            decimal varImportePropios = 0;
            if (totalImpAntPropios > totalImportePropios)
            {
                if (totalImportePropios != 0)
                {
                    varImportePropios = -Math.Round((totalImpAntPropios - totalImportePropios) / totalImportePropios * 100, 0);
                }
            }
            else
            {
                if (totalImpAntPropios != 0)
                {
                    varImportePropios = Math.Round((totalImportePropios - totalImpAntPropios) / totalImpAntPropios * 100, 0);
                }
            }

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

            decimal varTicketsPropios = 0;
            if (totalTicketsPropiosAnt > totalTicketsPropios)
            {
                if (totalTicketsPropios != 0)
                {
                    varTicketsPropios = -Math.Round((totalTicketsPropiosAnt - totalTicketsPropios) / totalTicketsPropios * 100, 0);
                }
            }
            else
            {
                if (totalTicketsPropiosAnt != 0)
                {
                    varTicketsPropios = Math.Round((totalTicketsPropios - totalTicketsPropiosAnt) / totalTicketsPropiosAnt * 100, 0);
                }
            }

            decimal varTkPromPropios = 0;
            if (totalTkPromPropiosAnt > totalTkPromPropios)
            {
                if (totalTkPromPropios != 0)
                {
                    varTkPromPropios = -Math.Round((totalTkPromPropiosAnt - totalTkPromPropios) / totalTkPromPropios * 100, 2);
                }
            }
            else
            {
                if (totalTkPromPropiosAnt != 0)
                {
                    varTkPromPropios = Math.Round((totalTkPromPropios - totalTkPromPropiosAnt) / totalTkPromPropiosAnt * 100, 0);
                }
            }

            decimal varAxtPropios = 0;
            if (totalAxTPropiosAnt > totalAxTPropios)
            {
                if (totalAxTPropios != 0)
                {
                    varAxtPropios = -Math.Round((totalAxTPropiosAnt - totalAxTPropios) / totalAxTPropios * 100, 2);
                }
            }
            else
            {
                if (totalAxTPropiosAnt != 0)
                {
                    varAxtPropios = Math.Round((totalAxTPropios - totalAxTPropiosAnt) / totalAxTPropiosAnt * 100, 2);
                }
            }


            ViewData["TotalImportePropiosAnt"] = totalImpAntPropios;
            ViewData["totalImportePropio"] = totalImportePropios;
            ViewData["TotalArticPropiosAnt"] = totalArticulosPropiosAnt;
            ViewData["TotalArticulosPropios"] = totalArticulosPropios;
            ViewData["TotalPropiosTk"] = totalTicketsPropios;
            ViewData["TkPromPropios"] = totalTkPromPropios;
            ViewData["ArtxTkPropios"] = totalAxTPropios;
            ViewData["VarImporte"] = varImportePropios;
            ViewData["VarArticPropios"] = varArticPropios;
            ViewData["VarTicketsPropios"] = varTicketsPropios;
            ViewData["VarTkPromPropios"] = varTkPromPropios;
            ViewData["VarAxTPropios"] = varAxtPropios;

            #endregion

            #region "Ecommerce"
            decimal totalImporteEcommerce = anualDash.Where(c => c.CoSucursal == 80 || c.CoSucursal == 1080).Sum(i => i.Importe);
            decimal totalImporteEcommerceAnt = anualDash.Where(c => c.CoSucursal == 80 || c.CoSucursal == 1080).Sum(i => i.ImporteAnt);
            decimal totalArticulosEcommerce = anualDash.Where(c => c.CoSucursal == 80 || c.CoSucursal == 1080).Sum(i => i.Articulos);
            decimal totalArticulosEcommerceAnt = anualDash.Where(c => c.CoSucursal == 80 || c.CoSucursal == 1080).Sum(i => i.ArticAnt);
            decimal totalTicketsEcommerce = anualDash.Where(c => c.CoSucursal == 80 || c.CoSucursal == 1080).Sum(i => i.Tickets);
            decimal totalTicketsEcommerceAnt = anualDash.Where(c => c.CoSucursal == 80 || c.CoSucursal == 1080).Sum(i => i.TicketsAnt);

            decimal totalTkPromEcommerce = 0;
            decimal totalAxTEcommerce = 0;
            if (totalTicketsEcommerce != 0)
            {
                totalTkPromEcommerce = totalImporteEcommerce / totalTicketsEcommerce;
                totalAxTEcommerce = totalArticulosEcommerce / totalTicketsEcommerce;
            }
            decimal totalTkPromEcommerceAnt = 0;
            decimal totalAxTEcommerceAnt = 0;
            if (totalTicketsEcommerceAnt != 0)
            {
                totalTkPromEcommerceAnt = totalImporteEcommerceAnt / totalTicketsEcommerceAnt;
                totalAxTEcommerceAnt = totalArticulosEcommerceAnt / totalTicketsEcommerceAnt;
            }

            decimal varImporteEcommerce = 0;
            if (totalImporteEcommerceAnt > totalImporteEcommerce)
            {
                if (totalImporteEcommerce != 0)
                {
                    varImporteEcommerce = -Math.Round((totalImporteEcommerceAnt - totalImporteEcommerce) / totalImporteEcommerce * 100, 0);
                }
            }
            else
            {
                if (totalImporteEcommerceAnt != 0)
                {
                    varImporteEcommerce = Math.Round((totalImporteEcommerce - totalImporteEcommerceAnt) / totalImporteEcommerceAnt * 100, 0);
                }
            }

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

            decimal varTicketsEcommerce = 0;
            if (totalTicketsEcommerceAnt > totalTicketsEcommerce)
            {
                if (totalTicketsEcommerce != 0)
                {
                    varTicketsEcommerce = -Math.Round((totalTicketsEcommerceAnt - totalTicketsEcommerce) / totalTicketsEcommerce * 100, 0);
                }
            }
            else
            {
                if (totalTicketsEcommerceAnt != 0)
                {
                    varTicketsEcommerce = Math.Round((totalTicketsEcommerce - totalTicketsEcommerceAnt) / totalTicketsEcommerceAnt * 100, 0);
                }
            }

            decimal varTkPromEcommerce = 0;
            if (totalTkPromEcommerceAnt > totalTkPromEcommerce)
            {
                if (totalTkPromEcommerce != 0)
                {
                    varTkPromEcommerce = -Math.Round((totalTkPromEcommerceAnt - totalTkPromEcommerce) / totalTkPromEcommerce * 100, 2);
                }
            }
            else
            {
                if (totalTkPromEcommerceAnt != 0)
                {
                    varTkPromEcommerce = Math.Round((totalTkPromEcommerce - totalTkPromEcommerceAnt) / totalTkPromEcommerceAnt * 100, 0);
                }
            }

            decimal varAxtEcommerce = 0;
            if (totalAxTEcommerceAnt > totalAxTEcommerce)
            {
                if (totalAxTEcommerce != 0)
                {
                    varAxtEcommerce = -Math.Round((totalAxTEcommerceAnt - totalAxTEcommerce) / totalAxTEcommerce * 100, 2);
                }
            }
            else
            {
                if (totalAxTEcommerceAnt != 0)
                {
                    varAxtEcommerce = Math.Round((totalAxTEcommerce - totalAxTEcommerceAnt) / totalAxTEcommerceAnt * 100, 2);
                }
            }

            ViewData["TotalImporteEcommerceAnt"] = totalImporteEcommerceAnt;
            ViewData["totalImporteEcommerce"] = totalImporteEcommerce;
            ViewData["TotalArticEcommerceAnt"] = totalArticulosEcommerceAnt;
            ViewData["TotalArticulosEcommerce"] = totalArticulosEcommerce;
            ViewData["TotalEcommerceTk"] = totalTicketsEcommerce;
            ViewData["TkPromEcommerce"] = totalTkPromEcommerce;
            ViewData["ArtxTkEcommerce"] = totalAxTEcommerce;
            ViewData["VarImporteEcommerce"] = varImporteEcommerce;
            ViewData["VarArticEcommerce"] = varArticEcommerce;
            ViewData["VarTicketsEcommerce"] = varTicketsEcommerce;
            ViewData["VarTkPromEcommerce"] = varTkPromEcommerce;
            ViewData["VarAxTEcommerce"] = varAxtEcommerce;

            #endregion

            #region "Consignacion"

            decimal totalImporteConsig = anualDash.Where(c => c.Consignacion).Sum(i => i.Importe);
            decimal totalImporteConsigAnt = anualDash.Where(c => c.Consignacion).Sum(i => i.ImporteAnt);
            decimal totalArticulosConsig = anualDash.Where(c => c.Consignacion).Sum(i => i.Articulos);
            decimal totalArticulosConsigAnt = anualDash.Where(c => c.Consignacion).Sum(i => i.ArticAnt);
            decimal totalTicketsConsig = anualDash.Where(c => c.Consignacion).Sum(i => i.Tickets);
            decimal totalTicketsConsigAnt = anualDash.Where(c => c.Consignacion).Sum(i => i.TicketsAnt);

            decimal totalTkPromConsig = 0;
            decimal totalAxTConsig = 0;
            if (totalTicketsConsig != 0)
            {
                totalTkPromConsig = totalImporteConsig / totalTicketsConsig;
                totalAxTConsig = totalArticulosConsig / totalTicketsConsig;
            }
            decimal totalTkPromConsigAnt = 0;
            decimal totalAxTConsigAnt = 0;
            if (totalTicketsConsigAnt != 0)
            {
                totalTkPromConsigAnt = totalImporteConsigAnt / totalTicketsConsigAnt;
                totalAxTConsigAnt = totalArticulosConsigAnt / totalTicketsConsigAnt;
            }

            decimal varImporteConsig = 0;
            if (totalImporteConsigAnt > totalImporteConsig)
            {
                if (totalImporteConsig != 0)
                {
                    varImporteConsig = -Math.Round((totalImporteConsigAnt - totalImporteConsig) / totalImporteConsig * 100, 0);
                }
            }
            else
            {
                if (totalImporteConsigAnt != 0)
                {
                    varImporteConsig = Math.Round((totalImporteConsig - totalImporteConsigAnt) / totalImporteConsigAnt * 100, 0);
                }
            }

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

            decimal varTicketsConsig = 0;
            if (totalTicketsConsigAnt > totalTicketsConsig)
            {
                if (totalTicketsConsig != 0)
                {
                    varTicketsConsig = -Math.Round((totalTicketsConsigAnt - totalTicketsConsig) / totalTicketsConsig * 100, 0);
                }
            }
            else
            {
                if (totalTicketsConsigAnt != 0)
                {
                    varTicketsConsig = Math.Round((totalTicketsConsig - totalTicketsConsigAnt) / totalTicketsConsigAnt * 100, 0);
                }
            }

            decimal varTkPromConsig = 0;
            if (totalTkPromConsigAnt > totalTkPromConsig)
            {
                if (totalTkPromConsig != 0)
                {
                    varTkPromConsig = -Math.Round((totalTkPromConsigAnt - totalTkPromConsig) / totalTkPromConsig * 100, 2);
                }
            }
            else
            {
                if (totalTkPromConsigAnt != 0)
                {
                    varTkPromConsig = Math.Round((totalTkPromConsig - totalTkPromConsigAnt) / totalTkPromConsigAnt * 100, 0);
                }
            }

            decimal varAxtConsig = 0;
            if (totalAxTConsigAnt > totalAxTConsig)
            {
                if (totalAxTConsig != 0)
                {
                    varAxtConsig = -Math.Round((totalAxTConsigAnt - totalAxTConsig) / totalAxTConsig * 100, 2);
                }
            }
            else
            {
                if (totalAxTConsigAnt != 0)
                {
                    varAxtConsig = Math.Round((totalAxTConsig - totalAxTConsigAnt) / totalAxTConsigAnt * 100, 2);
                }
            }

            ViewData["TotalImporteConsigAnt"] = totalImporteConsigAnt;
            ViewData["totalImporteConsig"] = totalImporteConsig;
            ViewData["TotalArticConsigAnt"] = totalArticulosConsigAnt;
            ViewData["TotalArticulosConsig"] = totalArticulosConsig;
            ViewData["TotalConsigTk"] = totalTicketsConsig;
            ViewData["TkPromConsig"] = totalTkPromConsig;
            ViewData["ArtxTkConsig"] = totalAxTConsig;
            ViewData["VarImporteConsig"] = varImporteConsig;
            ViewData["VarArticConsig"] = varArticConsig;
            ViewData["VarTicketsConsig"] = varTicketsConsig;
            ViewData["VarTkPromConsig"] = varTkPromConsig;
            ViewData["VarAxTConsig"] = varAxtConsig;

            #endregion

            #region "Franquicias"

            decimal totalArticulosFranq = anualDash.Where(c => c.Consignacion == false && c.Franquicia == true).Sum(i => i.Articulos);
            decimal totalArticulosFranqAnt = anualDash.Where(c => c.Consignacion == false && c.Franquicia == true).Sum(i => i.ArticAnt);
            decimal totalTicketsFranq = anualDash.Where(c => c.Consignacion == false && c.Franquicia == true).Sum(i => i.Tickets);
            decimal totalTicketsFranqAnt = anualDash.Where(c => c.Consignacion == false && c.Franquicia == true).Sum(i => i.TicketsAnt);
            decimal totalImporteFranq = anualDash.Where(c => c.Consignacion == false && c.Franquicia == true).Sum(i => i.Importe);
            decimal totalImporteFranqAnt = anualDash.Where(c => c.Consignacion == false && c.Franquicia == true).Sum(i => i.ImporteAnt);
            decimal totalTkPromFranq = 0;
            decimal totalAxTFranq = 0;
            if (totalTicketsFranq != 0)
            {
                totalTkPromFranq = totalImporteFranq / totalTicketsFranq;
                totalAxTFranq = totalArticulosFranq / totalTicketsFranq;
            }
            decimal totalTkPromFranqAnt = 0;
            decimal totalAxTFranqAnt = 0;
            if (totalTicketsFranqAnt != 0)
            {
                totalTkPromFranqAnt = totalImporteFranqAnt / totalTicketsFranqAnt;
                totalAxTFranqAnt = totalArticulosFranqAnt / totalTicketsFranqAnt;
            }

            decimal varImporteFranq = 0;
            if (totalImporteFranqAnt > totalImporteFranq)
            {
                if (totalImporteFranq != 0)
                {
                    varImporteFranq = -Math.Round((totalImporteFranqAnt - totalImporteFranq) / totalImporteFranq * 100, 0);
                }
            }
            else
            {
                if (totalImporteFranqAnt != 0)
                {
                    varImporteFranq = Math.Round((totalImporteFranq - totalImporteFranqAnt) / totalImporteFranqAnt * 100, 0);
                }
            }

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

            decimal varTicketsFranq = 0;
            if (totalTicketsFranqAnt > totalTicketsFranq)
            {
                if (totalTicketsFranq != 0)
                {
                    varTicketsFranq = -Math.Round((totalTicketsFranqAnt - totalTicketsFranq) / totalTicketsFranq * 100, 0);
                }
            }
            else
            {
                if (totalTicketsFranqAnt != 0)
                {
                    varTicketsFranq = Math.Round((totalTicketsFranq - totalTicketsFranqAnt) / totalTicketsFranqAnt * 100, 0);
                }
            }

            decimal varTkPromFranq = 0;
            if (totalTkPromFranqAnt > totalTkPromFranq)
            {
                if (totalTkPromFranq != 0)
                {
                    varTkPromFranq = -Math.Round((totalTkPromFranqAnt - totalTkPromFranq) / totalTkPromFranq * 100, 2);
                }
            }
            else
            {
                if (totalTkPromFranqAnt != 0)
                {
                    varTkPromFranq = Math.Round((totalTkPromFranq - totalTkPromFranqAnt) / totalTkPromFranqAnt * 100, 0);
                }
            }

            decimal varAxtFranq = 0;
            if (totalAxTFranqAnt > totalAxTFranq)
            {
                if (totalAxTFranq != 0)
                {
                    varAxtFranq = -Math.Round((totalAxTFranqAnt - totalAxTFranq) / totalAxTFranq * 100, 2);
                }
            }
            else
            {
                if (totalAxTFranqAnt != 0)
                {
                    varAxtFranq = Math.Round((totalAxTFranq - totalAxTFranqAnt) / totalAxTFranqAnt * 100, 2);
                }
            }

            ViewData["TotalImporteFranqAnt"] = totalImporteFranqAnt;
            ViewData["totalImporteFranq"] = totalImporteFranq;
            ViewData["TotalArticFranqAnt"] = totalArticulosFranqAnt;
            ViewData["TotalArticulosFranq"] = totalArticulosFranq;
            ViewData["TotalFranqTk"] = totalTicketsFranq;
            ViewData["TkPromFranq"] = totalTkPromFranq;
            ViewData["ArtxTkFranq"] = totalAxTFranq;
            ViewData["VarImporteFranq"] = varImporteFranq;
            ViewData["VarArticFranq"] = varArticFranq;
            ViewData["VarTicketsFranq"] = varTicketsFranq;
            ViewData["VarTkPromFranq"] = varTkPromFranq;
            ViewData["VarAxTFranq"] = varAxtFranq;

            #endregion

            ViewData["Anio"] = Anio;
            ViewData["AnioAnt"] = AnioAnt;


            return View(anualDash);
        }

        [HttpPost]
        public async Task<IActionResult> VentaAnual(int Anio, int AnioAnt)
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

            #region "Propios"

            decimal totalImportePropios = anualDash.Where(c => c.CoSucursal > 100 && c.CoSucursal < 200).Sum(i => i.Importe);
            decimal totalImpAntPropios = anualDash.Where(c => c.CoSucursal > 100 && c.CoSucursal < 200).Sum(i => i.ImporteAnt);
            decimal totalArticulosPropios = anualDash.Where(c => c.CoSucursal > 100 && c.CoSucursal < 200).Sum(i => i.Articulos);
            decimal totalArticulosPropiosAnt = anualDash.Where(c => c.CoSucursal > 100 && c.CoSucursal < 200).Sum(i => i.ArticAnt);
            decimal totalTicketsPropios = anualDash.Where(c => c.CoSucursal > 100 && c.CoSucursal < 200).Sum(i => i.Tickets);
            decimal totalTicketsPropiosAnt = anualDash.Where(c => c.CoSucursal > 100 && c.CoSucursal < 200).Sum(i => i.TicketsAnt);

            decimal totalTkPromPropios = 0;
            decimal totalAxTPropios = 0;
            if (totalTicketsPropios != 0)
            {
                totalTkPromPropios = totalImportePropios / totalTicketsPropios;
                totalAxTPropios = totalArticulosPropios / totalTicketsPropios;
            }
            decimal totalTkPromPropiosAnt = 0;
            decimal totalAxTPropiosAnt = 0;
            if (totalTicketsPropiosAnt != 0)
            {
                totalTkPromPropiosAnt = totalImpAntPropios / totalTicketsPropiosAnt;
                totalAxTPropiosAnt = totalArticulosPropiosAnt / totalTicketsPropiosAnt;
            }

            decimal varImportePropios = 0;
            if (totalImpAntPropios > totalImportePropios)
            {
                if (totalImportePropios != 0)
                {
                    varImportePropios = -Math.Round((totalImpAntPropios - totalImportePropios) / totalImportePropios * 100, 0);
                }
            }
            else
            {
                if (totalImpAntPropios != 0)
                {
                    varImportePropios = Math.Round((totalImportePropios - totalImpAntPropios) / totalImpAntPropios * 100, 0);
                }
            }

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

            decimal varTicketsPropios = 0;
            if (totalTicketsPropiosAnt > totalTicketsPropios)
            {
                if (totalTicketsPropios != 0)
                {
                    varTicketsPropios = -Math.Round((totalTicketsPropiosAnt - totalTicketsPropios) / totalTicketsPropios * 100, 0);
                }
            }
            else
            {
                if (totalTicketsPropiosAnt != 0)
                {
                    varTicketsPropios = Math.Round((totalTicketsPropios - totalTicketsPropiosAnt) / totalTicketsPropiosAnt * 100, 0);
                }
            }

            decimal varTkPromPropios = 0;
            if (totalTkPromPropiosAnt > totalTkPromPropios)
            {
                if (totalTkPromPropios != 0)
                {
                    varTkPromPropios = -Math.Round((totalTkPromPropiosAnt - totalTkPromPropios) / totalTkPromPropios * 100, 2);
                }
            }
            else
            {
                if (totalTkPromPropiosAnt != 0)
                {
                    varTkPromPropios = Math.Round((totalTkPromPropios - totalTkPromPropiosAnt) / totalTkPromPropiosAnt * 100, 0);
                }
            }

            decimal varAxtPropios = 0;
            if (totalAxTPropiosAnt > totalAxTPropios)
            {
                if (totalAxTPropios != 0)
                {
                    varAxtPropios = -Math.Round((totalAxTPropiosAnt - totalAxTPropios) / totalAxTPropios * 100, 2);
                }
            }
            else
            {
                if (totalAxTPropiosAnt != 0)
                {
                    varAxtPropios = Math.Round((totalAxTPropios - totalAxTPropiosAnt) / totalAxTPropiosAnt * 100, 2);
                }
            }


            ViewData["TotalImportePropiosAnt"] = totalImpAntPropios;
            ViewData["totalImportePropio"] = totalImportePropios;
            ViewData["TotalArticPropiosAnt"] = totalArticulosPropiosAnt;
            ViewData["TotalArticulosPropios"] = totalArticulosPropios;
            ViewData["TotalPropiosTk"] = totalTicketsPropios;
            ViewData["TkPromPropios"] = totalTkPromPropios;
            ViewData["ArtxTkPropios"] = totalAxTPropios;
            ViewData["VarImporte"] = varImportePropios;
            ViewData["VarArticPropios"] = varArticPropios;
            ViewData["VarTicketsPropios"] = varTicketsPropios;
            ViewData["VarTkPromPropios"] = varTkPromPropios;
            ViewData["VarAxTPropios"] = varAxtPropios;

            #endregion

            #region "Ecommerce"
            decimal totalImporteEcommerce = anualDash.Where(c => c.CoSucursal == 80 || c.CoSucursal == 1080).Sum(i => i.Importe);
            decimal totalImporteEcommerceAnt = anualDash.Where(c => c.CoSucursal == 80 || c.CoSucursal == 1080).Sum(i => i.ImporteAnt);
            decimal totalArticulosEcommerce = anualDash.Where(c => c.CoSucursal == 80 || c.CoSucursal == 1080).Sum(i => i.Articulos);
            decimal totalArticulosEcommerceAnt = anualDash.Where(c => c.CoSucursal == 80 || c.CoSucursal == 1080).Sum(i => i.ArticAnt);
            decimal totalTicketsEcommerce = anualDash.Where(c => c.CoSucursal == 80 || c.CoSucursal == 1080).Sum(i => i.Tickets);
            decimal totalTicketsEcommerceAnt = anualDash.Where(c => c.CoSucursal == 80 || c.CoSucursal == 1080).Sum(i => i.TicketsAnt);

            decimal totalTkPromEcommerce = 0;
            decimal totalAxTEcommerce = 0;
            if (totalTicketsEcommerce != 0)
            {
                totalTkPromEcommerce = totalImporteEcommerce / totalTicketsEcommerce;
                totalAxTEcommerce = totalArticulosEcommerce / totalTicketsEcommerce;
            }
            decimal totalTkPromEcommerceAnt = 0;
            decimal totalAxTEcommerceAnt = 0;
            if (totalTicketsEcommerceAnt != 0)
            {
                totalTkPromEcommerceAnt = totalImporteEcommerceAnt / totalTicketsEcommerceAnt;
                totalAxTEcommerceAnt = totalArticulosEcommerceAnt / totalTicketsEcommerceAnt;
            }

            decimal varImporteEcommerce = 0;
            if (totalImporteEcommerceAnt > totalImporteEcommerce)
            {
                if (totalImporteEcommerce != 0)
                {
                    varImporteEcommerce = -Math.Round((totalImporteEcommerceAnt - totalImporteEcommerce) / totalImporteEcommerce * 100, 0);
                }
            }
            else
            {
                if (totalImporteEcommerceAnt != 0)
                {
                    varImporteEcommerce = Math.Round((totalImporteEcommerce - totalImporteEcommerceAnt) / totalImporteEcommerceAnt * 100, 0);
                }
            }

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

            decimal varTicketsEcommerce = 0;
            if (totalTicketsEcommerceAnt > totalTicketsEcommerce)
            {
                if (totalTicketsEcommerce != 0)
                {
                    varTicketsEcommerce = -Math.Round((totalTicketsEcommerceAnt - totalTicketsEcommerce) / totalTicketsEcommerce * 100, 0);
                }
            }
            else
            {
                if (totalTicketsEcommerceAnt != 0)
                {
                    varTicketsEcommerce = Math.Round((totalTicketsEcommerce - totalTicketsEcommerceAnt) / totalTicketsEcommerceAnt * 100, 0);
                }
            }

            decimal varTkPromEcommerce = 0;
            if (totalTkPromEcommerceAnt > totalTkPromEcommerce)
            {
                if (totalTkPromEcommerce != 0)
                {
                    varTkPromEcommerce = -Math.Round((totalTkPromEcommerceAnt - totalTkPromEcommerce) / totalTkPromEcommerce * 100, 2);
                }
            }
            else
            {
                if (totalTkPromEcommerceAnt != 0)
                {
                    varTkPromEcommerce = Math.Round((totalTkPromEcommerce - totalTkPromEcommerceAnt) / totalTkPromEcommerceAnt * 100, 0);
                }
            }

            decimal varAxtEcommerce = 0;
            if (totalAxTEcommerceAnt > totalAxTEcommerce)
            {
                if (totalAxTEcommerce != 0)
                {
                    varAxtEcommerce = -Math.Round((totalAxTEcommerceAnt - totalAxTEcommerce) / totalAxTEcommerce * 100, 2);
                }
            }
            else
            {
                if (totalAxTEcommerceAnt != 0)
                {
                    varAxtEcommerce = Math.Round((totalAxTEcommerce - totalAxTEcommerceAnt) / totalAxTEcommerceAnt * 100, 2);
                }
            }

            ViewData["TotalImporteEcommerceAnt"] = totalImporteEcommerceAnt;
            ViewData["totalImporteEcommerce"] = totalImporteEcommerce;
            ViewData["TotalArticEcommerceAnt"] = totalArticulosEcommerceAnt;
            ViewData["TotalArticulosEcommerce"] = totalArticulosEcommerce;
            ViewData["TotalEcommerceTk"] = totalTicketsEcommerce;
            ViewData["TkPromEcommerce"] = totalTkPromEcommerce;
            ViewData["ArtxTkEcommerce"] = totalAxTEcommerce;
            ViewData["VarImporteEcommerce"] = varImporteEcommerce;
            ViewData["VarArticEcommerce"] = varArticEcommerce;
            ViewData["VarTicketsEcommerce"] = varTicketsEcommerce;
            ViewData["VarTkPromEcommerce"] = varTkPromEcommerce;
            ViewData["VarAxTEcommerce"] = varAxtEcommerce;

            #endregion

            #region "Consignacion"

            decimal totalImporteConsig = anualDash.Where(c => c.Consignacion).Sum(i => i.Importe);
            decimal totalImporteConsigAnt = anualDash.Where(c => c.Consignacion).Sum(i => i.ImporteAnt);
            decimal totalArticulosConsig = anualDash.Where(c => c.Consignacion).Sum(i => i.Articulos);
            decimal totalArticulosConsigAnt = anualDash.Where(c => c.Consignacion).Sum(i => i.ArticAnt);
            decimal totalTicketsConsig = anualDash.Where(c => c.Consignacion).Sum(i => i.Tickets);
            decimal totalTicketsConsigAnt = anualDash.Where(c => c.Consignacion).Sum(i => i.TicketsAnt);

            decimal totalTkPromConsig = 0;
            decimal totalAxTConsig = 0;
            if (totalTicketsConsig != 0)
            {
                totalTkPromConsig = totalImporteConsig / totalTicketsConsig;
                totalAxTConsig = totalArticulosConsig / totalTicketsConsig;
            }
            decimal totalTkPromConsigAnt = 0;
            decimal totalAxTConsigAnt = 0;
            if (totalTicketsConsigAnt != 0)
            {
                totalTkPromConsigAnt = totalImporteConsigAnt / totalTicketsConsigAnt;
                totalAxTConsigAnt = totalArticulosConsigAnt / totalTicketsConsigAnt;
            }

            decimal varImporteConsig = 0;
            if (totalImporteConsigAnt > totalImporteConsig)
            {
                if (totalImporteConsig != 0)
                {
                    varImporteConsig = -Math.Round((totalImporteConsigAnt - totalImporteConsig) / totalImporteConsig * 100, 0);
                }
            }
            else
            {
                if (totalImporteConsigAnt != 0)
                {
                    varImporteConsig = Math.Round((totalImporteConsig - totalImporteConsigAnt) / totalImporteConsigAnt * 100, 0);
                }
            }

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

            decimal varTicketsConsig = 0;
            if (totalTicketsConsigAnt > totalTicketsConsig)
            {
                if (totalTicketsConsig != 0)
                {
                    varTicketsConsig = -Math.Round((totalTicketsConsigAnt - totalTicketsConsig) / totalTicketsConsig * 100, 0);
                }
            }
            else
            {
                if (totalTicketsConsigAnt != 0)
                {
                    varTicketsConsig = Math.Round((totalTicketsConsig - totalTicketsConsigAnt) / totalTicketsConsigAnt * 100, 0);
                }
            }

            decimal varTkPromConsig = 0;
            if (totalTkPromConsigAnt > totalTkPromConsig)
            {
                if (totalTkPromConsig != 0)
                {
                    varTkPromConsig = -Math.Round((totalTkPromConsigAnt - totalTkPromConsig) / totalTkPromConsig * 100, 2);
                }
            }
            else
            {
                if (totalTkPromConsigAnt != 0)
                {
                    varTkPromConsig = Math.Round((totalTkPromConsig - totalTkPromConsigAnt) / totalTkPromConsigAnt * 100, 0);
                }
            }

            decimal varAxtConsig = 0;
            if (totalAxTConsigAnt > totalAxTConsig)
            {
                if (totalAxTConsig != 0)
                {
                    varAxtConsig = -Math.Round((totalAxTConsigAnt - totalAxTConsig) / totalAxTConsig * 100, 2);
                }
            }
            else
            {
                if (totalAxTConsigAnt != 0)
                {
                    varAxtConsig = Math.Round((totalAxTConsig - totalAxTConsigAnt) / totalAxTConsigAnt * 100, 2);
                }
            }

            ViewData["TotalImporteConsigAnt"] = totalImporteConsigAnt;
            ViewData["totalImporteConsig"] = totalImporteConsig;
            ViewData["TotalArticConsigAnt"] = totalArticulosConsigAnt;
            ViewData["TotalArticulosConsig"] = totalArticulosConsig;
            ViewData["TotalConsigTk"] = totalTicketsConsig;
            ViewData["TkPromConsig"] = totalTkPromConsig;
            ViewData["ArtxTkConsig"] = totalAxTConsig;
            ViewData["VarImporteConsig"] = varImporteConsig;
            ViewData["VarArticConsig"] = varArticConsig;
            ViewData["VarTicketsConsig"] = varTicketsConsig;
            ViewData["VarTkPromConsig"] = varTkPromConsig;
            ViewData["VarAxTConsig"] = varAxtConsig;

            #endregion

            #region "Franquicias"

            decimal totalArticulosFranq = anualDash.Where(c => c.Consignacion == false && c.Franquicia == true).Sum(i => i.Articulos);
            decimal totalArticulosFranqAnt = anualDash.Where(c => c.Consignacion == false && c.Franquicia == true).Sum(i => i.ArticAnt);
            decimal totalTicketsFranq = anualDash.Where(c => c.Consignacion == false && c.Franquicia == true).Sum(i => i.Tickets);
            decimal totalTicketsFranqAnt = anualDash.Where(c => c.Consignacion == false && c.Franquicia == true).Sum(i => i.TicketsAnt);
            decimal totalImporteFranq = anualDash.Where(c => c.Consignacion == false && c.Franquicia == true).Sum(i => i.Importe);
            decimal totalImporteFranqAnt = anualDash.Where(c => c.Consignacion == false && c.Franquicia == true).Sum(i => i.ImporteAnt);
            decimal totalTkPromFranq = 0;
            decimal totalAxTFranq = 0;
            if (totalTicketsFranq != 0)
            {
                totalTkPromFranq = totalImporteFranq / totalTicketsFranq;
                totalAxTFranq = totalArticulosFranq / totalTicketsFranq;
            }
            decimal totalTkPromFranqAnt = 0;
            decimal totalAxTFranqAnt = 0;
            if (totalTicketsFranqAnt != 0)
            {
                totalTkPromFranqAnt = totalImporteFranqAnt / totalTicketsFranqAnt;
                totalAxTFranqAnt = totalArticulosFranqAnt / totalTicketsFranqAnt;
            }

            decimal varImporteFranq = 0;
            if (totalImporteFranqAnt > totalImporteFranq)
            {
                if (totalImporteFranq != 0)
                {
                    varImporteFranq = -Math.Round((totalImporteFranqAnt - totalImporteFranq) / totalImporteFranq * 100, 0);
                }
            }
            else
            {
                if (totalImporteFranqAnt != 0)
                {
                    varImporteFranq = Math.Round((totalImporteFranq - totalImporteFranqAnt) / totalImporteFranqAnt * 100, 0);
                }
            }

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

            decimal varTicketsFranq = 0;
            if (totalTicketsFranqAnt > totalTicketsFranq)
            {
                if (totalTicketsFranq != 0)
                {
                    varTicketsFranq = -Math.Round((totalTicketsFranqAnt - totalTicketsFranq) / totalTicketsFranq * 100, 0);
                }
            }
            else
            {
                if (totalTicketsFranqAnt != 0)
                {
                    varTicketsFranq = Math.Round((totalTicketsFranq - totalTicketsFranqAnt) / totalTicketsFranqAnt * 100, 0);
                }
            }

            decimal varTkPromFranq = 0;
            if (totalTkPromFranqAnt > totalTkPromFranq)
            {
                if (totalTkPromFranq != 0)
                {
                    varTkPromFranq = -Math.Round((totalTkPromFranqAnt - totalTkPromFranq) / totalTkPromFranq * 100, 2);
                }
            }
            else
            {
                if (totalTkPromFranqAnt != 0)
                {
                    varTkPromFranq = Math.Round((totalTkPromFranq - totalTkPromFranqAnt) / totalTkPromFranqAnt * 100, 0);
                }
            }

            decimal varAxtFranq = 0;
            if (totalAxTFranqAnt > totalAxTFranq)
            {
                if (totalAxTFranq != 0)
                {
                    varAxtFranq = -Math.Round((totalAxTFranqAnt - totalAxTFranq) / totalAxTFranq * 100, 2);
                }
            }
            else
            {
                if (totalAxTFranqAnt != 0)
                {
                    varAxtFranq = Math.Round((totalAxTFranq - totalAxTFranqAnt) / totalAxTFranqAnt * 100, 2);
                }
            }

            ViewData["TotalImporteFranqAnt"] = totalImporteFranqAnt;
            ViewData["totalImporteFranq"] = totalImporteFranq;
            ViewData["TotalArticFranqAnt"] = totalArticulosFranqAnt;
            ViewData["TotalArticulosFranq"] = totalArticulosFranq;
            ViewData["TotalFranqTk"] = totalTicketsFranq;
            ViewData["TkPromFranq"] = totalTkPromFranq;
            ViewData["ArtxTkFranq"] = totalAxTFranq;
            ViewData["VarImporteFranq"] = varImporteFranq;
            ViewData["VarArticFranq"] = varArticFranq;
            ViewData["VarTicketsFranq"] = varTicketsFranq;
            ViewData["VarTkPromFranq"] = varTkPromFranq;
            ViewData["VarAxTFranq"] = varAxtFranq;

            #endregion

            ViewData["Anio"] = Anio;
            ViewData["AnioAnt"] = AnioAnt;


            return View(anualDash);
        }

        public async Task<IActionResult> SucDetail(int Id, int Anio, int AnioAnt)
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

            decimal totalImporte = Math.Round(anualDash.Sum(i => i.Importe), 0);
            decimal totalImporteAnt = Math.Round(anualDash.Sum(i => i.ImporteAnt), 0);
            var totalArticulos = anualDash.Sum(a => a.Articulos);
            var totalArticAnt = anualDash.Sum(a => a.ArticAnt);
            decimal totalTk = anualDash.Sum(t => t.Tickets);
            decimal totalTkAnt = anualDash.Sum(t => t.TicketsAnt);
            decimal tkProm = 0;
            decimal tkPromAnt = 0;
            decimal varArtic = 0;
            decimal varImporte = 0;
            decimal varTkProm = 0;
            decimal varTickets = 0;
            decimal varAxT = 0;
            decimal totalAxT = 0;
            decimal totalAxTAnt = 0;
            if (totalTk != 0)
            {
                tkProm = Math.Round(totalImporte / totalTk, 0);
                totalAxT = Math.Round(totalArticulos / totalTk, 2);
            }
            if (totalTkAnt != 0)
            {
                tkPromAnt = Math.Round(totalImporteAnt / totalTkAnt, 0);
                totalAxTAnt = Math.Round(totalArticAnt / totalTkAnt, 2);
            }

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

            if (totalTkAnt > totalTk)
            {
                if (totalTk != 0)
                {
                    varTickets = -Math.Round((totalTkAnt - totalTk) / totalTk * 100, 0);
                }
            }
            else
            {
                if (totalTkAnt != 0)
                {
                    varTickets = Math.Round((totalTk - totalTkAnt) / totalTkAnt * 100, 0);
                }
            }

            if (totalImporteAnt > totalImporte)
            {
                if (totalImporte != 0)
                {
                    varImporte = -Math.Round((totalImporteAnt - totalImporte) / totalImporte * 100, 0);
                }
            }
            else
            {
                if (totalImporteAnt != 0)
                {
                    varImporte = Math.Round((totalImporte - totalImporteAnt) / totalImporteAnt * 100, 0);
                }
            }

            if (tkPromAnt > tkProm)
            {
                if (tkProm != 0)
                {
                    varTkProm = -Math.Round((tkPromAnt - tkProm) / tkProm * 100, 2);
                }
            }
            else
            {
                if (tkPromAnt != 0)
                {
                    varTkProm = Math.Round((tkProm - tkPromAnt) / tkPromAnt * 100, 0);
                }
            }

            if (totalAxTAnt > totalAxT)
            {
                if (totalAxT != 0)
                {
                    varAxT = -Math.Round((totalAxTAnt - totalAxT) / totalAxT * 100, 2);
                }
            }
            else
            {
                if (totalAxTAnt != 0)
                {
                    varAxT = Math.Round((totalAxT - totalAxTAnt) / totalAxTAnt * 100, 2);
                }
            }

            ViewData["Sucursal"] = sucursal.Nombre;
            ViewData["CoSucursal"] = Id;
            ViewData["Anio"] = Anio;
            ViewData["AnioAnt"] = AnioAnt;
            ViewData["TotalImporte"] = totalImporte;
            ViewData["TotalImporteAnt"] = totalImporteAnt;
            ViewData["TotalArticulos"] = totalArticulos;
            ViewData["TotalArticAnt"] = totalArticAnt;
            ViewData["TotalTk"] = totalTk;
            ViewData["TkProm"] = tkProm;
            ViewData["VarImporte"] = varImporte;
            ViewData["VarTkProm"] = varTkProm;
            ViewData["VarArtic"] = varArtic;
            ViewData["VarTickets"] = varTickets;
            ViewData["VarAxT"] = varAxT;
            ViewData["ArtxTk"] = totalAxT;
            return View(anualDash);

        }

        public async Task<IActionResult> DetalleDiario(int Id, int Anio, int Mes, string MesTexto, string Sucursal)
        {
            var anio = new SqlParameter
            {
                ParameterName = "Anio",
                Value = Anio
            };
            var mes = new SqlParameter
            {
                ParameterName = "Mes",
                Value = Mes
            };
            var coSucursal = new SqlParameter
            {
                ParameterName = "CoSucursal",
                Value = Id
            };

            var diariaDash = _context.EstadisticaDiaria.FromSqlRaw("Dashboard_Estadistica_Diaria @CoSucursal, @Mes, @Anio", coSucursal, mes, anio).ToList();
            ViewData["Sucursal"] = Sucursal;
            ViewData["MesTexto"] = MesTexto;
            ViewData["Año"] = Anio;

            return View(diariaDash);
        }

    }
}
