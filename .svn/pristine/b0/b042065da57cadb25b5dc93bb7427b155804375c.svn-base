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
        public ActionResult _SelectEditingTurnosDiagnosticos(int turId)
        {
            var _Datos = (from d in context.enlTurnoDiagnostico
                          where d.turId == turId
                          select new enlTurnosDiagnosticos()
                          {
                              tdId = d.tdId,
                              diagId = d.diagId,
                              turId = d.turId,
                              diagnosticoDescripcion = d.catDiagnosticoPadron.diagDescripcion,
                              diagnosticoTipo1_Nombre = d.catDiagnosticoPadron.catDiagnosticoSubCapitulo.catDiagnosticoCapitulo.tdDescripcion,
                              diagnosticoTipo2_Nombre = d.catDiagnosticoPadron.catDiagnosticoSubCapitulo.subDescripcion
                          }).ToList();

            return View(new GridModel(_Datos));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult setTurnoDiagnostico(int diagId, int turId, string pAccion)
        {
            try
            {
                //Se valida el Diagnóstico\\
                if (pAccion == "Asignar")
                {
                    var CantidadDiagnosticos =
                        context.enlTurnoDiagnostico.Count(c => c.diagId == diagId && c.turId == turId);
                    if (CantidadDiagnosticos > 0)
                    {
                        return
                            Json(
                                new
                                {
                                    IsValid = false,
                                    Mensaje = "Este turno ya tiene cargado el diagnóstico que intenta asignar."
                                });
                    }
                    CantidadDiagnosticos = context.enlTurnoDiagnostico.Count(c => c.turId == turId);
                    if (CantidadDiagnosticos >= 5)
                    {
                        return
                            Json(
                                new
                                {
                                    IsValid = false,
                                    Mensaje = "No es posible asignar más de 5 diagnóstico al mismo turno."
                                });
                    }

                    var turno = context.catTurno.First(w => w.turId == turId);
                    var Diagnostico = context.catDiagnosticoPadron.First(w => w.diagId == diagId);
                    string codigoAdmision = context.sisTipo.First(d => d.tipoId == turno.tipoId_Admision).tipoCodigo;
                    if (codigoAdmision == "AT")
                    {
                        var _Item = new enlTurnoDiagnostico();
                        _Item.diagId = diagId;
                        _Item.turId = turId;
                        context.enlTurnoDiagnostico.AddObject(_Item);
                        //  Registra log de usuario
                        new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catTurno",
                            "Agregar",
                            "Agrega Diagnostico " + Diagnostico.diagDescripcion + " al paciente del Turno " +
                            _Item.turId);
                        context.SaveChanges();
                    }
                    else
                    {
                        return Json(new { IsValid = false, Mensaje = "El paciente debe estar en estado Atendido por Profesional" });
                    }
                }
                else
                {
                    var TurnoDiag = context.enlTurnoDiagnostico.Single(s => s.diagId == diagId && s.turId == turId);
                    //  Registra log de usuario
                    new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catTurno",
                        "Eliminar",
                        "Elimina Diagnóstico " + TurnoDiag.catDiagnosticoPadron.diagDescripcion + " del Turno " +
                        turId);

                    context.enlTurnoDiagnostico.DeleteObject(TurnoDiag);
                    context.SaveChanges();
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
