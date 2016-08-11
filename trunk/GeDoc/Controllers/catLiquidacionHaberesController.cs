namespace GeDoc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Telerik.Web.Mvc;
    using GeDoc.Models;

    public partial class catLiquidacionHaberesController : Controller
    {
        private efAccesoADatosEntities context = new efAccesoADatosEntities();
        //Edición de datos
        public IList<catLiquidacionHaberesWS> AllcatLiquidacionHaberes()
        {
            return getDatoscatLiquidacionHaberes().ToList();
        }

        private IEnumerable<catLiquidacionHaberesWS> getDatoscatLiquidacionHaberes()
        {
            var _Datos = (from d in context.catLiquidacionHaberes
                          select new catLiquidacionHaberesWS()
                                    {
                                        liqAccion = "",
                                        liqAutos = d.liqAutos,
                                        liqCaratulados = d.liqCaratulados,
                                        liqCuotas = d.liqCuotas,
                                        liqFechaEntrada = d.liqFechaEntrada,
                                        liqId = d.liqId,
                                        liqIdExpediente = d.liqIdExpediente,
                                        liqImporte = d.liqImporte,
                                        liqNumeroExpediente = d.liqNumeroExpediente,
                                        Tipo = d.sisTipo.tipoDescripcion,
                                        Estado = d.sisTipo1.tipoDescripcion,
                                        perId = d.perId,
                                        Empleado = d.catPersona.perApellidoyNombre,
                                        Juzgado = d.catJuzgado.juzDenominacion,
                                        Banco = d.catEntidadBancaria.bcoRazonSocial,
                                        juzId = d.juzId,
                                        bcoId = d.bcoId,
                                        tipoId = d.tipoId,
                                        tipoIdEstado = d.tipoIdEstado,
                                        EmpleadoDNI = d.catPersona.perDNI,
                                        liqImporteSF = d.liqImporteSF
                                    }).ToList();

            return _Datos.AsEnumerable();
        }

        [GridAction]
        public ActionResult _Binding_catLiquidacionHaberes()
        {
            PopulateDropDownListcatLiquidacionHaberes();

            return View(new GridModel<catLiquidacionHaberesWS>
            {
                Data = AllcatLiquidacionHaberes()
            });
        }

        [GridAction]
        public ActionResult Index()
        {
            PopulateDropDownListcatLiquidacionHaberes();

            return PartialView();
        }

        [GridAction]
        public ActionResult getcatLiquidacionHaberes(string pAccion, Int64? liqId)
        {
            var Datos = new catLiquidacionHaberesWS();
            Datos.liqAccion = pAccion;
            Datos.liqId = (int)liqId;

            if (pAccion != "Agregar")
            {
                //Cargamos los datos de la oferta
                var DatosActuales = context.catLiquidacionHaberes.Single(w => w.liqId == liqId);

                Datos.liqAutos = DatosActuales.liqAutos;
                Datos.liqCaratulados = DatosActuales.liqCaratulados;
                Datos.liqCuotas = DatosActuales.liqCuotas;
                Datos.liqFechaEntrada = DatosActuales.liqFechaEntrada;
                Datos.liqIdExpediente = DatosActuales.liqIdExpediente;
                Datos.liqImporte = DatosActuales.liqImporte;
                Datos.liqNumeroExpediente = DatosActuales.liqNumeroExpediente;
                Datos.Tipo = DatosActuales.sisTipo.tipoDescripcion;
                Datos.Estado = DatosActuales.sisTipo1.tipoDescripcion;
                Datos.perId = DatosActuales.perId;
                Datos.Empleado = DatosActuales.catPersona.perApellidoyNombre;
                Datos.Juzgado = DatosActuales.catJuzgado.juzDenominacion;
                Datos.Banco = DatosActuales.catEntidadBancaria.bcoRazonSocial;
                Datos.juzId = DatosActuales.juzId;
                Datos.bcoId = DatosActuales.bcoId;
                Datos.tipoId = DatosActuales.tipoId;
                Datos.tipoIdEstado = DatosActuales.tipoIdEstado;
                Datos.EmpleadoDNI = DatosActuales.catPersona.perDNI;
                Datos.liqImporteSF = DatosActuales.liqImporteSF;
            }

            PopulateDropDownListcatLiquidacionHaberes();

            return PartialView("CRUDLiquidacionHaberes", Datos);
        }

        //Datos para el dropdown
        public void PopulateDropDownListcatLiquidacionHaberes()
        {
            var _Tipo = context.sisTipo.ToList();
            var _Empleados = (from p in context.getListaDesplegablePersonas()
                              orderby p.perApellidoyNombre
                              select new catPersonas
                              {
                                  perId = p.perId,
                                  perApellidoyNombre = p.perApellidoyNombre
                              }).ToList();

            ViewData["tipoId_Data"] = new SelectList(_Tipo.Where(w => w.sisTipoEntidad.tipoeCodigo == "TL"), "tipoId", "tipoDescripcion");
            ViewData["perId_Data"] = new SelectList(_Empleados, "perId", "perApellidoyNombre");
            ViewData["juzId_Data"] = new SelectList(context.catJuzgado, "juzId", "juzDenominacion");
            ViewData["bcoId_Data"] = new SelectList(context.catEntidadBancaria, "bcoId", "bcoRazonSocial");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult getInfoPersona(int? perId)
        {
            if (perId == null)
            {
                return Json(new
                {
                    Padron = "",
                    Sector = ""
                });
            }

            var InfoPersona = context.catPersona.Single(s => s.perId == perId);

            return Json(new
            {
                Padron = InfoPersona.perPadron,
                Sector = "(" + InfoPersona.catSector.secCodigo + ") " + InfoPersona.catSector.secNombre
            });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult setcatLiquidacionHaberes(catLiquidacionHaberesWS Datos)
        {
            var TipoDescuento = new sisTipo();

            if (Datos.liqAccion == "Modificar" || Datos.liqAccion == "Agregar")
            {
                Datos.liqCuotas = Datos.liqCuotas < 0 ? 0 : Datos.liqCuotas;
                Datos.liqImporteSF = Datos.liqImporteSF < 0 ? 0 : Datos.liqImporteSF;
                Datos.liqCaratulados = Datos.liqCaratulados ?? "";
                Datos.liqNumeroExpediente = Datos.liqNumeroExpediente ?? "";
                Datos.liqAutos = Datos.liqAutos ?? "";

                if (Datos.tipoId <= 0 || Datos.tipoId == null)
                {
                    return Json(new
                    {
                        Error = true,
                        Mensaje = "Debe ingresar el tipo de descuento.",
                        Foco = "tipoId"
                    });
                }

                TipoDescuento = context.sisTipo.Single(s => s.tipoId == Datos.tipoId);

                if (Datos.perId < 0 || Datos.perId == null)
                {
                    return Json(new
                    {
                        Error = true,
                        Mensaje = "Debe ingresar el empleado.",
                        Foco = "perId"
                    });
                }

                if (Datos.liqNumeroExpediente.Trim().Length <= 0)
                {
                    return Json(new
                        {
                            Error = true,
                            Mensaje = "Debe ingresar el Número de Expediente.",
                            Foco = "liqNumeroExpediente"
                        });
                }
                Datos.liqIdExpediente = (Session["ExpedienteEncontrado"] as vwExpedientes).Id;

                if (Datos.liqAutos.Trim().Length <= 0)
                {
                    return Json(new
                    {
                        Error = true,
                        Mensaje = "Debe ingresar el Autos correspondiente.",
                        Foco = "liqAutos"
                    });
                }

                if (Datos.liqCaratulados.Trim().Length <= 0)
                {
                    return Json(new
                    {
                        Error = true,
                        Mensaje = "Debe ingresar el Texto de Caratulados.",
                        Foco = "liqCaratulados"
                    });
                }

                if (Datos.juzId <= 0 || Datos.juzId == null)
                {
                    return Json(new
                    {
                        Error = true,
                        Mensaje = "Debe ingresar el Juzgado.",
                        Foco = "juzId"
                    });
                }

                if (Datos.bcoId < 0 || Datos.bcoId == null)
                {
                    return Json(new
                    {
                        Error = true,
                        Mensaje = "Debe ingresar el Banco.",
                        Foco = "bcoId"
                    });
                }

                if (Datos.liqImporte <= 0 || Datos.liqImporte == null)
                {
                    return Json(new
                    {
                        Error = true,
                        Mensaje = "Debe ingresar el Importe.",
                        Foco = "liqImporte"
                    });
                }

                if (TipoDescuento.tipoCodigo == "EM")
                {
                    if (Datos.liqCuotas <= 0 || Datos.liqCuotas == null)
                    {
                        return Json(new
                        {
                            Error = true,
                            Mensaje = "Debe ingresar las Cuotas.",
                            Foco = "liqCuotas"
                        });
                    }
                }

                //Se verifica la duplicidad del embargo
                if (context.catLiquidacionHaberes.Count(c => c.liqAutos == Datos.liqAutos && c.perId == Datos.perId && c.liqImporte == Datos.liqImporte && c.liqNumeroExpediente == Datos.liqNumeroExpediente && c.liqId != Datos.liqId) > 0)
                {
                    return Json(new
                    {
                        Error = true,
                        Mensaje = "Registro Duplicado, revise la información por favor.",
                        Foco = "perId"
                    });
                }

                Datos.tipoIdEstado = context.sisTipo.Single(s => s.tipoCodigo == "SL" && s.sisTipoEntidad.tipoeCodigo == "ED").tipoId;
            }
            else
            {
                if (Datos.liqAccion != "Eliminar")
                {
                    TipoDescuento = context.sisTipo.Single(s => s.tipoId == Datos.tipoId);
                }
                else
                {
                    TipoDescuento.tipoDescripcion = "Embargo/Cuota Alimentaria";
                }
            }

            try
            {
                switch (Datos.liqAccion)
                {
                    case "Agregar":
                    case "Modificar":
                        var Registro = new catLiquidacionHaberes();

                        //Actualizamos los datos personales del paciente\\
                        if (Datos.liqAccion != "Agregar")
                        {
                            Registro = context.catLiquidacionHaberes.Single(s => s.liqId == Datos.liqId);
                        }

                        Registro.liqAutos = Datos.liqAutos;
                        Registro.liqCaratulados = Datos.liqCaratulados;
                        Registro.liqCuotas = (short)Datos.liqCuotas;
                        Registro.liqFechaEntrada = Datos.liqFechaEntrada;
                        Registro.liqIdExpediente = Datos.liqIdExpediente;
                        Registro.liqImporte = Datos.liqImporte;
                        Registro.liqNumeroExpediente = Datos.liqNumeroExpediente;
                        Registro.perId = Datos.perId;
                        Registro.juzId = Datos.juzId;
                        Registro.bcoId = (short)Datos.bcoId;
                        Registro.tipoId = (short)Datos.tipoId;
                        Registro.tipoIdEstado = (short)Datos.tipoIdEstado;
                        Registro.liqImporteSF = Datos.liqImporteSF;

                        if (Datos.liqAccion == "Agregar")
                        {
                            context.catLiquidacionHaberes.AddObject(Registro);
                        }
                        break;
                    case "Eliminar":
                        var DeleteRegistro = context.catLiquidacionHaberes.Single(w => w.liqId == Datos.liqId);
                        context.catLiquidacionHaberes.DeleteObject(DeleteRegistro);
                        break;
                }

                new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catLiquidacionHaberes", Datos.liqAccion, Datos.liqAccion + " " + TipoDescuento.tipoDescripcion);
                context.SaveChanges();

                return Json(new
                {
                    Error = false,
                    Mensaje = "Se actualizó información del Proveedor en forma correcta.",
                    Foco = "liqIdExpediente"
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Error = true,
                    Mensaje = ex.InnerException == null ? ex.Message : ex.InnerException.Message,
                    Foco = "liqIdExpediente"
                });
            }
        }
    }
}