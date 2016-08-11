using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using GeDoc.Filters;
using GeDoc.Models;
using Telerik.Web.Mvc;

namespace GeDoc.Controllers
{
    public partial class catEncuestaAPSController : Controller
    {

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _SelectEditingEncuestasAPSPersonas(int? _encId)
        {
          //  ViewBag.Catalogo = "Personas encuestadas de la encuesta Nro " + _encId;
            //return View(new GridModel(AllEncuestasAPSPersonas(_encId)));
            return View(new GridModel<catEncuestasAPSPersonas>
            {
                Data = from a in context.getEncuestaAPSencuestados(_encId).ToList()
                       select new catEncuestasAPSPersonas()
                       {
                           ApellidoyNombre = a.pacApellido + ' ' + a.pacNombre,
                           TipoDocumento = a.tipoIdTipoDocumento,
                           Documento = a.pacNumeroDocumento,
                           encId = a.encId,
                           encPerId = a.encPerId,
                           pacId = a.pacId,
                           encRedesPuntaje = a.encRedesPuntaje,
                           encDolencias = a.dolencias
                       }
            });

        }

        public IList<catEncuestasAPSPersonas> AllEncuestasAPSPersonas(int _encId)
        {
            return getDatosEncuestasAPSPersonas(_encId).ToList();
        }

        private IEnumerable<catEncuestasAPSPersonas> getDatosEncuestasAPSPersonas(int _encId)
        {
            var _Datos = (from d in context.getEncuestaAPSencuestados(_encId)
                          select new catEncuestasAPSPersonas()
                          {
                              ApellidoyNombre = d.pacApellido + ' ' + d.pacNombre,
                              TipoDocumento = d.tipoIdTipoDocumento,
                              Documento = d.pacNumeroDocumento,
                              //enfId = d.enfId,
                              encId = d.encId,
                              encPerId = d.encPerId,
                              pacId = d.pacId,
                              encRedesPuntaje = d.encRedesPuntaje,
                              encDolencias = d.dolencias
                          }).ToList();

            return _Datos.AsEnumerable();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _InsertEditingEncuestasAPSPersonas()
        {
            catEncuestaAPSPersonas _Item = new catEncuestaAPSPersonas();


            TryUpdateModel(_Item);

            if (TryUpdateModel(_Item))
            {
                Create(_Item);
            }

            return _SelectEditingEncuestasAPSPersonas(_Item.encId);
        }

        private void Create(catEncuestaAPSPersonas pItem)
        {
            if (ModelState.IsValid)
            {
                context.catEncuestaAPSPersonas.AddObject(pItem);              
                context.SaveChanges();
            }
            return;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AsignarPersonaEnfermedad(int enfId, int encPerId,string encEnfDescripcionComentario="")
        {
            enlEncuestaAPSPersonaEnfermedad _Item = new enlEncuestaAPSPersonaEnfermedad();
            if (encPerId > 0)
            {
               
                //if (ModelState.IsValid)
                //{
                    try
                    {
                        _Item.encPerId = encPerId;
                        _Item.enfId = enfId;
                        _Item.encEnfDescripcionComentario = encEnfDescripcionComentario;
                        context.enlEncuestaAPSPersonaEnfermedad.AddObject(_Item);
                        context.SaveChanges();
                        //  Registra log de usuario
                        //  new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catTurno", "Agregar", "Agrega Diagnostico " + pItem.catDiagnostico.Descripcion + " al paciente del Turno " + pItem.turId);
                    }
                    catch (Exception ex)
                    {
                        return Json(new { IsValid = false, Mensaje = ex.Message });
                    }

                //}
            }
            else
            {
                _Item.encPerId = encPerId;
                _Item.enfId = enfId;
                _Item.encEnfDescripcionComentario = encEnfDescripcionComentario;

                context.ExecuteStoreCommand("select encPerId,enfId,encEnfDescripcionComentario");
            }

               return Json(new { IsValid = true, Mensaje = "OK" });

        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AsignarPersonaEnfermedad2(List<enlEncuestaAPSPersonaEnfermedad> _Datos)
        {
            enlEncuestaAPSPersonaEnfermedad _Item = new enlEncuestaAPSPersonaEnfermedad();
            if (1> 0)
            {

                if (ModelState.IsValid)
                {
                    try
                    {
                        //_Item.encPerId = encPerId;
                        //_Item.enfId = enfId;
                        //_Item.encEnfDescripcionComentario = encEnfDescripcionComentario;
                        context.enlEncuestaAPSPersonaEnfermedad.AddObject(_Item);
                        context.SaveChanges();
                        //  Registra log de usuario
                        //  new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catTurno", "Agregar", "Agrega Diagnostico " + pItem.catDiagnostico.Descripcion + " al paciente del Turno " + pItem.turId);
                    }
                    catch (Exception ex)
                    {
                        return Json(new { IsValid = false, Mensaje = ex.Message });
                    }

                }
            }
            else
            {
                //_Item.encPerId = encPerId;
                //_Item.enfId = enfId;
                //_Item.encEnfDescripcionComentario = encEnfDescripcionComentario;

                context.ExecuteStoreCommand("select encPerId,enfId,encEnfDescripcionComentario");
            }

            return Json(new { IsValid = true, Mensaje = "OK" });

        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AsignarPacienteEncuesta(long pacId, int encId)
        {

            catEncuestaAPSPersonas _Item = new catEncuestaAPSPersonas();
            
                try
                {
                    _Item.pacId = pacId;
                    _Item.encId = encId;
                    context.catEncuestaAPSPersonas.AddObject(_Item);
                    context.SaveChanges();
                    //Registra log de usuario              
                    new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catEncuestaAPS", "Asignar Paciente", "Asigna un Paciente a la encuesta ");

                }
                catch (Exception ex)
                {
                    return Json(new { IsValid = false, Mensaje = ex.Message });
                }
            
            return Json(new { 
                IsValid = true,
                Mensaje = "OK",
               encPerId =  _Item.encPerId           
            });
        }
       
        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _DeleteEditingEncuestasAPSPersonas(int id)
        {
            int encId = context.catEncuestaAPSPersonas.Single(w => w.encPerId == id).encId.Value;
            DeleteConfirmedEncuestasAPSPersonas(id);

            return _SelectEditingEncuestasAPSPersonas(encId);
        }


        private void DeleteConfirmedEncuestasAPSPersonas(int id)
        {
            catEncuestaAPSPersonas _Item = context.catEncuestaAPSPersonas.Single(x => x.encPerId == id);
            DeleteConfirmedenlEncuestasAPSPersonasEnfxPersona(_Item.encPerId);
            context.catEncuestaAPSPersonas.DeleteObject(_Item);
            context.SaveChanges();         
        }

       


    }
}

