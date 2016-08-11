using GeDoc.Models;
using GeDoc.Models.JuntaMedica.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telerik.Web.Mvc;

namespace GeDoc.Controllers.JuntaMedica.Estadisticas
{
    public class TableroComandoController : Controller
    {
        List<getDatosTableroDeComandoDCRM_Result> _Datos = new List<getDatosTableroDeComandoDCRM_Result>();

        private efAccesoADatosJMJuntaMedicaEntities context = new efAccesoADatosJMJuntaMedicaEntities();
        private readonly efAccesoADatosEntities contextGD = new efAccesoADatosEntities();
        //
        // GET: /TableroDeComando/

        public ActionResult Index()
        {
            
            getDatosTableroDeComandoDCRM();
            return View();
        }

        public ActionResult TableroDeComando()
        {
            cargaUmbrales();
            ViewData["Especialidades"] = new List<catEspecialidad>();
            ViewBag.Catalogo = "Tablero de Comandos";
            CargarEspecialidades();
            return PartialView("~/Views/JuntaMedica/TableroDeComando.cshtml");
            //return View();
        }

        private void CargarEspecialidades()
        {
            var _csId = (Session["UsuarioCentroDeSalud"] as sisUsuariosCentroDeSalud).csId;
            var list = new List<SelectListItem>();

            var _Especialistas=((     from s in context.CatEspecialidad //.ToList()
                                      select new catEspecialidades
                                      {
                                          espId = s.espId,
                                          espNombre = s.espNombre
                                      }).Distinct().OrderBy(o => o.espNombre));

            list.AddRange(new SelectList(_Especialistas, "espId", "espNombre"));
            ViewData["Especialidades"] = list;
        }

        private void cargaUmbrales()
        {
            var _csId = (Session["UsuarioCentroDeSalud"] as sisUsuariosCentroDeSalud).csId;
            ViewData["Umbrales"] = contextGD.catUmbralTiemposTurno.First(w => w.csId == _csId);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult getDatosTableroDeComandoDCRM()
        {
            _Datos = (from x in context.getDatosTableroDeComandoDCRM()
                      select new getDatosTableroDeComandoDCRM_Result()
                      {
                          cmedId = x.cmedId,
                          cmedFecha = x.cmedFecha,
                          espNombre = x.espNombre,
                          Medico = x.Medico,
                          UsuarioMedico = x.UsuarioMedico,
                          Emitida = x.Emitida,
                          EnEspera = x.EnEspera,
                          ListaParaAtencion = x.ListaParaAtencion,
                          AtencionMedica = x.AtencionMedica,
                          Atendido = x.Atendido,
                          estEstado = x.estEstado,
                          EspecialidadMedico = x.EspecialidadMedico,
                          Ocio = x.Ocio,
                          Fecha = x.Fecha,
                          TiempoDeEspera = x.TiempoDeEspera,
                          TiempoDeAtencion = x.TiempoDeAtencion,
                          TiempoOcioso = x.TiempoOcioso
                      }
                          ).ToList();
            Session["DatosTablero"] = _Datos;
            cargaComboEspecialistas();
            return Json(new { Data = _Datos });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult getTop5()
        {
            //var _Datos = (Session["DatosTablero"] as getDatosTableroDeComandoDCRM_Result);
            var listBackFromSession = context.getDatosTableroDeComandoDCRM_Top5Espera().ToList();
            //var listBackFromSession = (List<getDatosTableroDeComandoDCRM_Result>)Session["DatosTablero"];
            if (listBackFromSession.Count>0)
            {
                var Aux = (from d in listBackFromSession
                           select new Top5()
                           {
                               Medico = d.agtApellidoyNombre,
                               Especialidad = d.espNombre,
                               TiempoDeEspera = d.Minutos
                           }).OrderByDescending(p => p.TiempoDeEspera).Take(5);

                return Json(new { Data = Aux });
            }
            else
                return Json(new { Data = "" });

            //return View(new GridModel(Aux));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult getPromedioOciosoPorMedico()
        {
            var listBackFromSession = (List<getDatosTableroDeComandoDCRM_Result>)Session["DatosTablero"];
            if (listBackFromSession.Count > 0)
            {
                var Aux = (from d in listBackFromSession
                           group d by new
                           {
                               d.Medico
                           } into gpb
                           select new Top5()
                           {
                               Medico = gpb.Key.Medico,
                               TiempoPromedioOcioso = Math.Round(Convert.ToDecimal(gpb.Average(x => x.TiempoOcioso)), 2)
                           });

                return Json(new { Data = Aux });
            }
            else
                return Json(new { Data = "" });

            //return View(new GridModel(Aux));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult getPromedioTiempoDeEsperaPorMedico(string usrMedico, string grafico)
        {
            var listBackFromSession = (List<getDatosTableroDeComandoDCRM_Result>)Session["DatosTablero"];
            if (listBackFromSession.Count > 0)
            {
                if (grafico.Equals("1"))
                {
                    var Aux = (from d in listBackFromSession
                               where d.UsuarioMedico.ToString().Equals(usrMedico)
                               group d by new
                               {
                                   d.UsuarioMedico,
                                   d.cmedFecha.Hour
                               } into gpb
                               select new Top5()
                               {
                                   UsuarioMedico = gpb.Key.UsuarioMedico,
                                   TiempoPromedio = Math.Round(Convert.ToDecimal(gpb.Average(x => x.TiempoDeEspera)), 2),
                                   TiempoMaximo = gpb.Max(x => x.TiempoDeEspera),
                                   TiempoMinimo = gpb.Min(x => x.TiempoDeEspera),
                                   Hora = gpb.Key.Hour
                               }).OrderBy(x=>x.Hora);

                    return Json(new { Data = Aux });
                }
                else if (grafico.Equals("2"))
                {
                    var Aux = (from d in listBackFromSession
                               where d.UsuarioMedico.ToString().Equals(usrMedico)
                               group d by new
                               {
                                   d.UsuarioMedico,
                                   d.cmedFecha.Hour
                               } into gpb
                               select new Top5()
                               {
                                   UsuarioMedico = gpb.Key.UsuarioMedico,
                                   TiempoPromedio = Math.Round(Convert.ToDecimal(gpb.Average(x => x.TiempoDeAtencion)), 2),
                                   TiempoMaximo = gpb.Max(x => x.TiempoDeAtencion),
                                   TiempoMinimo = gpb.Min(x => x.TiempoDeAtencion),
                                   Hora = gpb.Key.Hour
                               }).OrderBy(x => x.Hora);

                    return Json(new { Data = Aux });
                }
                else
                {
                    var Aux = (from d in listBackFromSession
                               where d.UsuarioMedico.ToString().Equals(usrMedico)
                               group d by new
                               {
                                   d.UsuarioMedico,
                                   d.cmedFecha.Hour
                               } into gpb
                               select new Top5()
                               {
                                   UsuarioMedico = gpb.Key.UsuarioMedico,
                                   TiempoPromedio = Math.Round(Convert.ToDecimal(gpb.Average(x => x.TiempoOcioso)), 2),
                                   TiempoMaximo = gpb.Max(x => x.TiempoOcioso),
                                   TiempoMinimo = gpb.Min(x => x.TiempoOcioso),
                                   Hora = gpb.Key.Hour
                               }).OrderBy(x => x.Hora);

                    return Json(new { Data = Aux });
                }
            }
            else
                return Json(new { Data = "" });

            //return View(new GridModel(Aux));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult getCantidadDeAtencionesPorTipoGrafico(string grafico)
        {
            var listBackFromSession = (List<getDatosTableroDeComandoDCRM_Result>)Session["DatosTablero"];
            if (listBackFromSession.Count > 0)
            {
                if (grafico.Equals("1"))
                {
                    var Aux = (from d in listBackFromSession
                               where d.estEstado.Equals("AA") || d.estEstado.Equals("PE")
                               group d by new
                               {
                                   d.Medico
                               } into gpb
                               select new Top5()
                               {
                                   Medico = gpb.Key.Medico,
                                   Cantidad = gpb.Count()
                               });

                    return Json(new { Data = Aux });
                }
                else
                {
                    var Aux = (from d in listBackFromSession
                               where d.estEstado.Equals("AA") || d.estEstado.Equals("PE")
                               group d by new
                               {
                                   d.espNombre
                               } into gpb
                               select new Top5()
                               {
                                   Especialidad = gpb.Key.espNombre,
                                   Cantidad = gpb.Count()
                               });

                    return Json(new { Data = Aux });
                }
            }
            else
                return Json(new { Data = "" });

            //return View(new GridModel(Aux));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult getPatologiasPorDepartamento(int _espId, DateTime _fDesde, DateTime _fHasta, int _diagId)
        {
            var listBackFromSession = context.getDatosTableroDeComandoDCRM_Torta_Pato(_fDesde, _fHasta, _espId, _diagId).ToList();
            if (listBackFromSession.Count > 0)
            {
                var Aux = (from d in listBackFromSession
                           select new DiagPorDepartamento()
                           {
                               DepId = d.depId,
                               DepNombre = d.depNombre,
                               Promedio = d.Promedio
                           });

                return Json(new { Data = Aux });
            }
            else
                return Json(new { Data = "" });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult getPatologiasAcumuladasPorMes(int _espId, DateTime _fDesde, DateTime _fHasta, int _diagId)
        {
            var listBackFromSession = context.getDatosTableroDeComandoDCRM_Linea_Pato(_fDesde, _fHasta, _espId, _diagId).ToList();
            if (listBackFromSession.Count > 0)
            {
                var Aux = (from d in listBackFromSession
                           select new DiagAcuPorMes()
                           {
                               Periodo = d.Periodo,
                               Cantidad = d.Cantidad
                           });

                return Json(new { Data = Aux });
            }
            else
                return Json(new { Data = "" });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult getCantidadEnEspera()
        {
            //var listBackFromSession = (List<getDatosTableroDeComandoDCRM_Result>)Session["DatosTablero"];
            var Cantidad = context.gesCartaMedicaUltimoEstado.Count(c => c.estEstado == "ES");
            //if (listBackFromSession.Count > 0)
            //{
            //    var Aux = (from d in listBackFromSession
            //               where d.estEstado.Equals("ES")
            //               group d by new
            //               {
            //                   d.estEstado
            //               } into gpb
            //               select new Top5()
            //               {
            //                   Estado = gpb.Key.estEstado,
            //                   Cantidad = gpb.Count()
            //               });

            //    return Json(new { Data = Aux });
            //}
            //else
            return Json(new { Cantidad = Cantidad });

            //return View(new GridModel(Aux));
        }

        public ActionResult cargaComboEspecialistas()
        {
            var list = new List<SelectListItem>();
            var listBackFromSession = (List<getDatosTableroDeComandoDCRM_Result>)Session["DatosTablero"];
            if (listBackFromSession.Count > 0)
            {
                var Aux = (from d in listBackFromSession
                           group d by new
                           {
                               d.UsuarioMedico,
                               d.Medico
                           } into gpb
                           select new Top5()
                           {
                               Medico = gpb.Key.Medico,
                               UsuarioMedico = gpb.Key.UsuarioMedico
                           });

                return new JsonResult { Data = new SelectList(Aux.ToList(), "UsuarioMedico", "Medico") };
                //list.AddRange(new SelectList(Aux.Where(w => w.UsuarioMedico != -1), "UsuarioMedico", "Medico"));
            }
            else
            {
                return new JsonResult { Data = new SelectList(Enumerable.Empty<SelectListItem>(), "UsuarioMedico", "Medico") };
            }
        }

        [GridAction]
        public ActionResult cargaTop5PatologiasPorEspecialidad(int _espId, DateTime _fDesde, DateTime _fHasta)
        {
            var _Datos = context.getDatosTableroDeComandoDCRM_Top10(_fDesde, _fHasta, _espId).ToList();
            return PartialView(new GridModel(_Datos));
        }

        public class DiagAcuPorMes
        {
            string periodo;
            int? cantidad;

            public string Periodo
            {
                get { return periodo; }
                set { periodo = value; }
            }

            public int? Cantidad
            {
                get { return cantidad; }
                set { cantidad = value; }
            }
        }

        public class DiagPorDepartamento
        {
            int? depId;
            string depNombre;
            decimal? promedio;

            public int? DepId
            {
                get { return depId; }
                set { depId = value; }
            }

            public string DepNombre
            {
                get { return depNombre; }
                set { depNombre = value; }
            }

            public decimal? Promedio
            {
                get { return promedio; }
                set { promedio = value; }
            }
        }


        public class Top5
        {
            string medico;
            string especialidad;
            int? tiempoDeEspera;
            string estado;
            int cantidad;
            decimal? tiempoPromedioOcioso;
            decimal? tiempoPromedio;
            decimal? tiempoMaximo;
            decimal? tiempoMinimo;
            int? usuarioMedico;
            int hora;

            public decimal? TiempoMaximo
            {
                get { return tiempoMaximo; }
                set { tiempoMaximo = value; }
            }

            public decimal? TiempoMinimo
            {
                get { return tiempoMinimo; }
                set { tiempoMinimo = value; }
            }

            public decimal? TiempoPromedio
            {
                get { return tiempoPromedio; }
                set { tiempoPromedio = value; }
            }

            public decimal? TiempoPromedioOcioso
            {
                get { return tiempoPromedioOcioso; }
                set { tiempoPromedioOcioso = value; }
            }

            public int Cantidad
            {
                get { return cantidad; }
                set { cantidad = value; }
            }

            public int Hora
            {
                get { return hora; }
                set { hora = value; }
            }

            public int? UsuarioMedico
            {
                get { return usuarioMedico; }
                set { usuarioMedico = value; }
            }

            public string Estado
            {
                get { return estado; }
                set { estado = value; }
            }

            public string Medico
            {
                get { return medico; }
                set { medico = value; }
            }

            public string Especialidad
            {
                get { return especialidad; }
                set { especialidad = value; }
            }

            public int? TiempoDeEspera
            {
                get { return tiempoDeEspera; }
                set { tiempoDeEspera = value; }
            }
        }
    }
}
