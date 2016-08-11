namespace GeDoc
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Telerik.Web.Mvc;
    using GeDoc.Filters;
    using GeDoc.Models;
    using GeDoc.Models.dsAccesoExpedientesTableAdapters;

    public class expExpedienteController : Controller
    {
        private efAccesoADatosEntities context = new efAccesoADatosEntities();

        //Edición de datos

        [GridAction]
        public ActionResult _SelectEditing()
        {
            return View(new GridModel(All()));
        }

        private IList<expExpedientes> All()
        {
                return getDatos().ToList();
        }

        [GridAction]
        public ActionResult _SelectEditingEntrada()
        {
            return View(new GridModel(AllEntrada()));
        }

        private IList<expExpedientes> AllEntrada()
        {
            return getDatosEntrada().ToList();
        }

        [GridAction]
        public ActionResult _SelectEditingSalida()
        {
            return View(new GridModel(AllSalida()));
        }

        private IList<expExpedientes> AllSalida()
        {
            return getDatosSalida().ToList();
        }

        [GridAction]
        public ActionResult _SelectEditingIniciadosPorMi()
        {
            return View(new GridModel(AllIniciadosPorMi()));
        }

        private IList<expExpedientes> AllIniciadosPorMi()
        {
            return getDatosIniciadosPorMi().ToList();
        }

        private IEnumerable<expExpedientes> getDatos()
        {
            var Oficinas = (from u in context.enlUsuarioOficina.ToList()
                           join o in context.catOficina.ToList() on u.ofiId equals o.ofiId
                           where u.usrId == (Session["Usuario"] as sisUsuario).usrId
                           select o).ToList();
            if ((bool)Session["EsMiBandeja"])
            {
                var _DExpe = (from d in context.vwExpedientes.ToList()
                              join x in Oficinas on d.codigodestino equals x.ofiCodigo
                              join z in Oficinas on d.nombredestino equals z.ofiNombre
                              where x != null && z != null
                              select new expExpedientes()
                              {
                                  Acceso = d.acceso,
                                  Ciclo = d.ciclo,
                                  Comentarios = d.comentarios,
                                  Clave = d.clave,
                                  DestinoCodigo = d.codigodestino,
                                  DestinoNombre = d.nombredestino,
                                  Expediente = d.prefijo.ToString() + "-" + ((int)d.sufijo).ToString("00000") + "-" + d.ciclo.ToString(),
                                  Extracto = d.extracto,
                                  Fecha = d.fecha,
                                  Folios = d.folios,
                                  Id = d.Id,
                                  IniciadorCodigo = d.iniciadorCodigo,
                                  IniciadorNombre = d.iniciadorNombre,
                                  Sufijo = d.sufijo,
                                  Tipo = d.tipo
                              }).ToList();

                return _DExpe.AsEnumerable();
            }
            var _Datos = (from d in context.vwExpedientes.ToList()
                          select new expExpedientes()
                                    {
                                        Acceso = d.acceso,
                                        Ciclo = d.ciclo,
                                        Comentarios = d.comentarios,
                                        Clave = d.clave,
                                        DestinoCodigo = d.codigodestino,
                                        DestinoNombre = d.nombredestino,
                                        Expediente = d.prefijo.ToString() + "-" + ((int)d.sufijo).ToString("00000") + "-" + d.ciclo.ToString(),
                                        Extracto = d.extracto,
                                        Fecha = d.fecha,
                                        Folios = d.folios,
                                        Id = d.Id,
                                        IniciadorCodigo = d.iniciadorCodigo,
                                        IniciadorNombre = d.iniciadorNombre,
                                        Sufijo = d.sufijo,
                                        pagFecha = d.pagFecha,
                                        Tipo = d.tipo//,
                                        //DiasCorridos = getDias(d.Id, "C"),
                                        //DiasHabiles = getDias(d.Id, "H")
                                    }).ToList();

            return _Datos.AsEnumerable();
        }

        private IEnumerable<expExpedientes> getDatosEntrada()
        {
            var Oficinas = (from u in context.enlUsuarioOficina.ToList()
                            join o in context.catOficina.ToList() on u.ofiId equals o.ofiId
                            where u.usrId == (Session["Usuario"] as sisUsuario).usrId
                            select o).ToList();

            List<expExpedientes> _Datos = new List<expExpedientes>();
            foreach(var _Item in Oficinas){
                var _DExpe = (from d in context.spExpedientesBandejas(_Item.ofiCodigo, "E").ToList()
                                select new expExpedientes()
                                {
                                    Acceso = d.acceso,
                                    Ciclo = d.ciclo,
                                    Comentarios = d.comentarios,
                                    Clave = d.clave,
                                    DestinoCodigo = d.codigodestino,
                                    DestinoNombre = d.nombredestino,
                                    Expediente = d.prefijo.ToString() + "-" + ((int)d.sufijo).ToString("00000") + "-" + d.ciclo.ToString(),
                                    Extracto = d.extracto,
                                    Fecha = d.fecha,
                                    Folios = d.folios,
                                    Id = d.Id,
                                    IniciadorCodigo = d.iniciadorCodigo,
                                    IniciadorNombre = d.iniciadorNombre,
                                    Sufijo = d.sufijo,
                                    Tipo = d.tipo,
                                    nombreMovimiento = d.nombreMovimiento,
                                    fechaMovimiento = d.fechaMovimiento
                                }).ToList();
                _Datos.AddRange(_DExpe);
            }

            return _Datos.AsEnumerable();
        }

        private IEnumerable<expExpedientes> getDatosIniciadosPorMi()
        {
            var Oficinas = (from u in context.enlUsuarioOficina.ToList()
                            join o in context.catOficina.ToList() on u.ofiId equals o.ofiId
                            where u.usrId == (Session["Usuario"] as sisUsuario).usrId
                            select o).ToList();

            List<expExpedientes> _Datos = new List<expExpedientes>();
            foreach (var _Item in Oficinas)
            {
                var _DExpe = (from d in context.spExpedientesBandejas(_Item.ofiCodigo, "M").ToList()
                              select new expExpedientes()
                              {
                                  Acceso = d.acceso,
                                  Ciclo = d.ciclo,
                                  Comentarios = d.comentarios,
                                  Clave = d.clave,
                                  DestinoCodigo = d.codigodestino,
                                  DestinoNombre = d.nombredestino,
                                  Expediente = d.prefijo.ToString() + "-" + ((int)d.sufijo).ToString("00000") + "-" + d.ciclo.ToString(),
                                  Extracto = d.extracto,
                                  Fecha = d.fecha,
                                  Folios = d.folios,
                                  Id = d.Id,
                                  IniciadorCodigo = d.iniciadorCodigo,
                                  IniciadorNombre = d.iniciadorNombre,
                                  Sufijo = d.sufijo,
                                  Tipo = d.tipo,
                                  nombreMovimiento = d.nombreMovimiento,
                                  fechaMovimiento = d.fechaMovimiento
                              }).ToList();
                _Datos.AddRange(_DExpe);
            }

            return _Datos.AsEnumerable();
        }

        private IEnumerable<expExpedientes> getDatosSalida()
        {
            var Oficinas = (from u in context.enlUsuarioOficina.ToList()
                            join o in context.catOficina.ToList() on u.ofiId equals o.ofiId
                            where u.usrId == (Session["Usuario"] as sisUsuario).usrId
                            select o).ToList();

            List<expExpedientes> _Datos = new List<expExpedientes>();
            foreach (var _Item in Oficinas)
            {
                var _DExpe = (from d in context.spExpedientesBandejas(_Item.ofiCodigo, "S").ToList()
                              select new expExpedientes()
                              {
                                  Acceso = d.acceso,
                                  Ciclo = d.ciclo,
                                  Comentarios = d.comentarios,
                                  Clave = d.clave,
                                  DestinoCodigo = d.codigodestino,
                                  DestinoNombre = d.nombredestino,
                                  Expediente = d.prefijo.ToString() + "-" + ((int)d.sufijo).ToString("00000") + "-" + d.ciclo.ToString(),
                                  Extracto = d.extracto,
                                  Fecha = d.fecha,
                                  Folios = d.folios,
                                  Id = d.Id,
                                  IniciadorCodigo = d.iniciadorCodigo,
                                  IniciadorNombre = d.iniciadorNombre,
                                  Sufijo = d.sufijo,
                                  Tipo = d.tipo,
                                  nombreMovimiento = d.nombreMovimiento,
                                  fechaMovimiento = d.fechaMovimiento
                              }).ToList();
                _Datos.AddRange(_DExpe);
            }

            return _Datos.AsEnumerable();
        }

        public ActionResult Index()
        {
            ViewBag.Catalogo = "Mesa de Entradas y Salidas (Expedientes)";
            ViewData["expPases"] = new List<expExpedientesPases>();
            Session["EsMiBandeja"] = false;
            ViewBag.AlturaGrilla = (int)Session["AlturaGrilla"] - 5;

            return PartialView();
        }

        public ActionResult Bandejas()
        {
            ViewData["expPases"] = new List<expExpedientesPases>();
            Session["EsMiBandeja"] = true;
            ViewBag.AlturaGrilla = (int)Session["AlturaGrilla"] - 33;

            var Oficinas = (from u in context.enlUsuarioOficina.ToList()
                            join o in context.catOficina.ToList() on u.ofiId equals o.ofiId
                            where u.usrId == (Session["Usuario"] as sisUsuario).usrId
                            select o).ToList();

            ViewBag.Catalogo = "Bandejas de Expedientes de " + Oficinas.First().ofiNombre;

            return PartialView();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult getPases(int pExpedienteId)
        {
            List<expExpedientesPases> _Resultado = new List<expExpedientesPases>();

            try
            {
                context.CommandTimeout = 500;

                _Resultado = (from x in context.spPases(pExpedienteId).ToList()
                              orderby x.origenFecha ascending
                              select new expExpedientesPases()
                                  {
                                      NumeroPase = 1,
                                      origenFecha = x.origenFecha,
                                      origenNombre = x.origenNombre,
                                      destinoNombre = x.destinoNombre,
                                      destinoFecha = x.destinoFecha,
                                      origenAlias = x.origenAlias,
                                      destinoAlias = x.destinoAlias
                                  }).ToList();

                int _Numero = 0;
                var _Expediente = context.getExpediente(0, 0, 0, pExpedienteId).First();
                var _Anterior = _Resultado.First();

                foreach (var _Item in _Resultado)
                {
                    _Item.NumeroPase = _Numero + 1;
                    if (_Numero == 0)
                    {
                        _Item.origenNombre += " (" +
                                              (_Item.destinoFecha.Value.Date - _Expediente.fecha.Value.Date).Days
                                                                                                            .ToString() +
                                              " día/s, ";
                        var _Dias = context.getContarDiasHabiles(_Expediente.fecha.Value.Date, _Item.destinoFecha.Value.Date).First().Value;
                        //var _Dias = ContarDias(0, _Expediente.fecha.Value.Date, _Item.destinoFecha.Value.Date);
                        _Item.origenNombre += " " + _Dias + " día/s hábiles)";
                    }
                    else
                    {
                        _Item.origenNombre += " (" +
                                              (_Item.origenFecha.Value.Date - _Anterior.origenFecha.Value.Date).Days
                                                                                                               .ToString
                                                  () + " día/s, ";
                        var _Dias = context.getContarDiasHabiles(_Anterior.origenFecha.Value.Date, _Item.origenFecha.Value.Date).First().Value;
                        //var _Dias = ContarDias(0, _Anterior.origenFecha.Value.Date, _Item.origenFecha.Value.Date);
                        _Item.origenNombre += " " + _Dias + " día/s hábiles)";
                    }

                    _Anterior = _Item;
                    _Numero++;
                }

                _Anterior.destinoNombre += " (" + (DateTime.Now.Date - _Anterior.origenFecha.Value.Date).Days.ToString() +
                                           " día/s, ";
                _Anterior.destinoNombre += " " +
                                            context.getContarDiasHabiles(_Anterior.origenFecha.Value.Date, DateTime.Now.Date).First().Value +
//                                           ContarDias(0, _Anterior.origenFecha.Value.Date, DateTime.Now.Date).ToString() +
                                           " día/s hábiles)";

                return Json(new
                    {
                        Data = _Resultado.OrderByDescending(o => o.NumeroPase).ToList(),
                        Encabezado = new {UbicacionActual = _Anterior.destinoNombre}
                    });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Data = new List<expExpedientesPases>(),
                    Encabezado = new { UbicacionActual = "NO SE PUDO OBTENER LA INFORMACIÓN DE LOS PASES" }
                });
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult getExisteExpediente(string Comprobante)
        {
            if (Comprobante.Length == 0)
            {
                return Json(new
                {
                    Existe = true
                });
            }

            int _Prefijo = int.Parse(Comprobante.Substring(0, 3));
            int _Sufijo = int.Parse(Comprobante.Substring(4, 5));
            int _Ciclo = int.Parse(Comprobante.Substring(10, 4));
            var Resultado = context.getExpediente(_Prefijo, _Sufijo, _Ciclo, -1).ToList();
            bool _Existe = Resultado.Any();

            Session["ExpedienteEncontrado"] = Resultado.FirstOrDefault();

            return Json(new
            {
                Existe = _Existe
            });
        }

        public int getDias(int pExpedienteId, string pTipo)
        {
            List<expExpedientesPases> _Resultado = new List<expExpedientesPases>();

            _Resultado = (from x in context.spPases(pExpedienteId).ToList()
                          //where x.expedienteId == pExpedienteId
                          orderby x.origenFecha descending
                          select new expExpedientesPases()
                          {
                              NumeroPase = 1,
                              origenFecha = x.origenFecha,
                              origenNombre = x.origenNombre,
                              destinoNombre = x.destinoNombre,
                              destinoFecha = x.destinoFecha,
                              origenAlias = x.origenAlias,
                              destinoAlias = x.destinoAlias
                          }).ToList();

            if (_Resultado.Count == 0)
            {
                return 0;
            }

            var _Anterior = _Resultado.First();

            if (pTipo == "H")
            {
                return ContarDias(0, _Anterior.origenFecha.Value.Date, DateTime.Now.Date);
            }

            return (DateTime.Now.Date - _Anterior.origenFecha.Value.Date).Days;
        }

        protected int ContarDias(int Dias, DateTime FechaInicial, DateTime FechaFinal)
        {
            if (FechaInicial.Year <= 1900 || FechaFinal.Year <= 1900)
            {
                return 5000;
            }
            FechaInicial = FechaInicial.AddDays(1);
            bool _Seguir = context.catFeriado.ToList().Where(w => w.ferFecha.Date == FechaInicial.Date).Count() > 0;
            while (FechaInicial.DayOfWeek == DayOfWeek.Saturday || FechaInicial.DayOfWeek == DayOfWeek.Sunday || _Seguir)
            {
                FechaInicial = FechaInicial.AddDays(1);
                _Seguir = context.catFeriado.ToList().Where(w => w.ferFecha.Date == FechaInicial.Date).Count() > 0;
            }
            if (FechaInicial.Date > FechaFinal.Date)
            {
                return Dias;
            }

            return ContarDias(Dias + 1, FechaInicial, FechaFinal);
        }

        //protected DateTime DateAgregarLaborales(Int32 add, DateTime FechaInicial)
        //{
        //    if (FechaInicial.DayOfWeek == DayOfWeek.Saturday) { FechaInicial = FechaInicial.AddDays(2); }
        //    if (FechaInicial.DayOfWeek == DayOfWeek.Sunday) { FechaInicial = FechaInicial.AddDays(1); }
        //    Int32 weeks = add / 5;
        //    add += weeks * 2;
        //    if (FechaInicial.DayOfWeek > FechaInicial.AddDays(add).DayOfWeek) { add += 2; }
        //    if (FechaInicial.AddDays(add).DayOfWeek == DayOfWeek.Saturday) { add += 2; }
        //    //Int32 libres = LibresEntre(FechaInicial, FechaInicial.AddDays(add));

        //    if (libres > 0) { return DateAgregarLaborales(0, FechaInicial.AddDays(libres + add)); }
        //    else { return FechaInicial.AddDays(add); }
        //}
    }
}