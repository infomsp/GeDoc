namespace GeDoc.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using Telerik.Web.Mvc;
    using GeDoc.Models;
    public partial class catTurnoController : Controller
    {
        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _SelectEditingTurnosPracticas(int turId)
        {
            var _Datos = (from d in context.enlTurnoPractica
                          where d.turId == turId
                          select new enlTurnosPracticas()
                          {
                              pracId = d.pracId,
                              turId = d.turId,
                              PracticaDescripcion = d.catPractica.pracDescripcion,
                              NomencladorDescripcion = d.catPractica.catNomenclador.nomDescripcion,
                              turpracId = d.turpracId,
                              pracCosto = d.pracCosto,
                              pracUop = d.pracUop,
                              turpracCantidad = d.turpracCantidad
                          }).ToList();

            return View(new GridModel(_Datos));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult setTurnoPractica(int pracId, int turId, int pracCantidad, string pAccion)
        {
            try
            {
                //Se valida la Práctica\\
                switch (pAccion)
                {
                    case "Asignar":
                        var CantidadPracticas =
                            context.enlTurnoPractica.Count(c => c.pracId == pracId && c.turId == turId);
                        if (CantidadPracticas > 0)
                        {
                            return
                                Json(
                                    new
                                    {
                                        IsValid = false,
                                        Mensaje =
                                            "Este turno ya tiene cargada una práctica como la que intenta asignar."
                                    });
                        }
                        var turno = context.catTurno.First(w => w.turId == turId);
                        var Practica = context.catPractica.First(w => w.pracId == pracId);
                        string codigoAdmision = context.sisTipo.First(d => d.tipoId == turno.tipoId_Admision).tipoCodigo;
                        if (codigoAdmision == "AT")
                        {
                            var _Item = new enlTurnoPractica();
                            _Item.pracId = pracId;
                            _Item.turId = turId;
                            _Item.pracCosto = Practica.pracCosto;
                            _Item.pracUop = Practica.pracUop;
                            _Item.turpracCantidad = (short) pracCantidad;
                            context.enlTurnoPractica.AddObject(_Item);
                            //  Registra log de usuario
                            new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catTurno",
                                "Agregar",
                                "Agrega Práctica " + Practica.pracDescripcion + " al paciente del Turno " +
                                _Item.turId);
                            context.SaveChanges();
                        }
                        else
                        {
                            return
                                Json(
                                    new
                                    {
                                        IsValid = false,
                                        Mensaje = "El paciente debe estar en estado Atendido por Profesional"
                                    });
                        }
                        break;
                    case "Eliminar":
                        var TurnoPrac = context.enlTurnoPractica.Single(s => s.pracId == pracId && s.turId == turId);
                        //  Registra log de usuario
                        new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catTurno",
                            "Eliminar",
                            "Elimina Práctica " + TurnoPrac.catPractica.pracDescripcion + " del Turno " +
                            turId);

                        context.enlTurnoPractica.DeleteObject(TurnoPrac);
                        context.SaveChanges();
                        break;
                    case "Modificar":
                        var ModificarPrac = context.enlTurnoPractica.Single(s => s.pracId == pracId && s.turId == turId);
                        //  Registra log de usuario
                        new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catTurno",
                            "Modificar",
                            "Modificar Práctica " + ModificarPrac.catPractica.pracDescripcion + " del Turno " + turId);

                        ModificarPrac.turpracCantidad = (short)pracCantidad;
                        context.SaveChanges();
                        break;
                }
            }

            catch (Exception ex)
            {
                return Json(new { IsValid = false, Mensaje = ex.Message });
            }

            return Json(new { IsValid = true, Mensaje = "OK" });
        }
    }
}

