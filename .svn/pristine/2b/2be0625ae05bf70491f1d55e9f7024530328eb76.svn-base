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

    public partial class catAvisoController : Controller
    {
        private efAccesoADatosEntities context = new efAccesoADatosEntities();

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult getDatos()
        {
            if (Session["UsuarioCentroDeSalud"] == null)
            {
                RedirectToAction("LogOff", "Account");
                return null;
            }

            int usrId = ((sisUsuario)Session["Usuario"]).usrId ;
            var _Datos = context.catAviso.Where(r => r.usrId == usrId && r.aviRestantes > 0 && r.aviActivo && r.catAvisoMensaje.avimActivo).Select(r => new catAvisoMsg()
                {
                    aviId = r.aviId,
                    usrId = r.usrId,
                    aviTitulo = r.catAvisoMensaje.avimTitulo,
                    aviContenido = r.catAvisoMensaje.avimContenido,
                    aviRestantes = r.aviRestantes,
                    aviActivo = r.aviActivo
                }).OrderByDescending(r => r.aviId).AsEnumerable();

            if (_Datos.Count()!=0)
            {
                Session["AvisosUsuario"] = _Datos;
            }


            return Json(new { count = _Datos.Count(), data = _Datos.FirstOrDefault() });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult decrementaAviso(int aviId, bool aviActivo)
        {
            var entidadAviso = context.catAviso.Where(r => r.aviId == aviId).FirstOrDefault();

            int cantRestante;
            if (entidadAviso!=null && entidadAviso.aviRestantes>0)
            {
                entidadAviso.aviRestantes--;
                entidadAviso.aviActivo = aviActivo;

                context.SaveChanges();
            }
            cantRestante = entidadAviso.aviRestantes;
            return Json(new { cantRestante = cantRestante });
        }

        //private void Create(catAgenda pItem)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            pItem.agActivo = false;
        //            /*valida si una persona de planta o contratado tiene una agenda asignada*/
        //            if (pItem.perId < 0)
        //            {
        //                pItem.conId = (pItem.perId * -1);
        //                pItem.perId = null;

        //                if (context.catAgenda.Where(w => w.conId == pItem.conId && w.espId == pItem.espId && w.csId == pItem.csId).Count() > 0)
        //                {
        //                    int agId = pItem.agId;
        //                    var agh_Item = context.catAgendaHorario.Where(p => p.agId == pItem.agId).ToList();
        //                    // if(context.catAgendaHorario(agh => agh.aghVigenciaDesde == pItem.
        //                    //  ModelState.AddModelError("perId", "Ya tiene agenda asignada en este centro de salud, especialidad, dia y horario");
        //                }
        //            }
        //            else
        //            {
        //                if (context.catAgenda.Where(w => w.perId == pItem.perId && w.espId == pItem.espId && w.csId == pItem.csId).Count() > 0)
        //                {
        //                    // ModelState.AddModelError("perId", "Ya tiene agenda asignada en este centro de salud, especialidad, dia y horario");
        //                    // return;
        //                }
        //            }

        //            context.catAgenda.AddObject(pItem);

        //            new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catAgenda", "Agregar", "Agrega agenda");

        //            context.SaveChanges();
        //        }
        //        catch (Exception ex)
        //        {
        //            ModelState.AddModelError("agDuracion", ex.Message);
        //        }
        //    }

        //    return;
        //}

        //private void Edit(catAgenda pItem)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            if (pItem.perId < 0)
        //            {
        //                pItem.conId = (pItem.perId * -1);
        //                pItem.perId = null;
        //            }

        //            //Registra log de usuario
        //            new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catAgenda", "Modificar", "Modifica agenda");

        //            context.SaveChanges();
        //        }
        //        catch (Exception ex)
        //        {
        //            ModelState.AddModelError("agDuracion", ex.Message);
        //        }
        //    }
        //    return;
        //}

        //private void DeleteConfirmed(int id, bool pEstado)
        //{
        //    try
        //    {
        //        catAgenda _Item = context.catAgenda.Single(x => x.agId == id);

        //        var _ItemAgendaHorarios = _Item.catAgendaHorario.ToList();



        //        var _AgendaDatos = context.catAgenda.Where(x => (x.csId == _Item.csId) && ((x.perId == _Item.perId) || (x.conId == _Item.conId))).ToList();



        //        _Item.agActivo = pEstado;
        //        //Registra log de usuario
        //        if (pEstado == false)
        //        {
        //            new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catAgenda", "Eliminar", "Elimina agenda");
        //            context.SaveChanges();
        //            // eliminaTurnos(id);
        //        }
        //        else
        //        {
        //            if (_AgendaDatos.Count() == 0)
        //            {
        //                // var _AgendaHorariosDatos = context.catAgendaHorario.SkipWhile(m => m.agId != ());
        //                //ModelState.AddModelError("agDuracion", "Esta agenda ya esta activada");
        //                //  var agh_Item = context.catAgendaHorario.Where(p => p.agId == id).ToList();    
        //            }
        //            else
        //            {
        //                new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catAgenda", "Activar", "Activar Agenda");
        //                _Item.agActivo = true;
        //                context.SaveChanges();
        //                //generaTurnos(id);
        //            }
        //        }
        //        //context.SaveChanges();
        //    }
        //    catch (Exception ex)
        //    {
        //        ModelState.AddModelError("agDuracion", ex.Message);
        //    }
        //}

    }
}