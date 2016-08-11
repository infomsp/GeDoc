using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using GeDoc.Filters;
using GeDoc.Models;
using Telerik.Web.Mvc;

namespace GeDoc.Controllers
{
    public partial class catEncuestaSinAdmisionPlanillaController : Controller
    {

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _SelectEditingEncuestasSinAdmisionPersonas(int? _plaId)
        {
            //  ViewBag.Catalogo = "Personas encuestadas de la encuesta Nro " + _encId;
            //return View(new GridModel(AllEncuestasAPSPersonas(_encId)));
            return View(new GridModel<catEncuestaSinadmisionPersonas>
            {
                Data = (from a in context.getEncuestaSinAdmisionEncuestados(_plaId).ToList()
                       select new catEncuestaSinadmisionPersonas()
                       {
                           ApellidoyNombre = a.pacApellido + ' ' + a.pacNombre,
                           TipoDocumento = a.tipoIdTipoDocumento,
                           Documento = a.pacNumeroDocumento,
                           encId = a.encId,
                           pacId = a.pacId,
                           cantesp = a.cantEsp,
                           encCausa = a.encCausa,
                           causaNombre = context.catEncuestaSinAdmisionCausa.Single(x => x.causaId == a.encCausa).causaNombre,
                           AtendidoLocal = getAtendidoLocalxEsp(a.encId),
                           tipoDescripcion = getAtendidoLocalxEsp(a.encId)=="rojo.png"? "NO" :"SI"

                       })
            });
        }

        public string getAtendidoLocalxEsp(int? _peId)
        {
            var _tipoeId = context.sisTipoEntidad.FirstOrDefault(a => a.tipoeCodigo == "AL").tipoeId;
            var _tipoIdNO = context.sisTipo.FirstOrDefault(a => a.tipoeId == _tipoeId && a.tipoDescripcion == "NO").tipoId;
            var _tipoIdSI = context.sisTipo.FirstOrDefault(a => a.tipoeId == _tipoeId && a.tipoDescripcion == "SI").tipoId;
            var _AL_SI = context.catEncuestaSinadmisionPersonaEspecialidad.FirstOrDefault(a => a.encId == _peId && a.atendidoLocal == _tipoIdSI);
            var _AL_NO = context.catEncuestaSinadmisionPersonaEspecialidad.FirstOrDefault(a => a.encId == _peId && a.atendidoLocal == _tipoIdNO);
           
              
                if (_AL_SI == null && _AL_NO == null)
                {
                    return "rojo.png";
                }
                if (_AL_SI != null && _AL_NO == null)
                   {

                       return "azul.png";
                   }
                //if (_AL_SI != null )
                //   {
                //       if (_AL_NO.derId == 2)
                //       {
                //           return "azul.png";
                //       }
                //       else
                //       {
                //           return "rojo.png";
                //       }
                //   }
                if (_AL_NO != null)
                {
                    if (_AL_NO.programado == 1)
                    {
                        return "azul.png";
                    }
                    else
                    {
                        return "rojo.png";
                    }
                }
            return "rojo.png";
        }

        public IList<catEncuestaSinadmisionPersonas> AllEncuestasSinadmisionPersonas(int _encId)
        {
            return getDatosEncuestasSinadmisionPersonas(_encId).ToList();
        }

        private IEnumerable<catEncuestaSinadmisionPersonas> getDatosEncuestasSinadmisionPersonas(int _encId)
        {
            var _Datos = (from d in context.getEncuestaSinAdmisionEncuestados(_encId)
                          select new catEncuestaSinadmisionPersonas()
                          {
                              ApellidoyNombre = d.pacApellido + ' ' + d.pacNombre,
                              TipoDocumento = d.tipoIdTipoDocumento,
                              Documento = d.pacNumeroDocumento,
                              //enfId = d.enfId,
                              encId = d.encId,
                             // encPerId = d.encPerId,
                              pacId = d.pacId,
                              //encRedesPuntaje = d.encRedesPuntaje,
                              //encDolencias = d.dolencias
                          }).ToList();

            return _Datos.AsEnumerable();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _InsertEditingEncuestasAPSPersonas()
        {
            catEncuestaSinAdmisionPersona _Item = new catEncuestaSinAdmisionPersona();
            TryUpdateModel(_Item);
            var _datos =
                  context.catEncuestaSinAdmisionPersona.FirstOrDefault(p => p.plaId == _Item.plaId & p.pacId == _Item.pacId );
            if (_datos == null)
            {
                if (TryUpdateModel(_Item))
                {
                    Create(_Item);
                }
            }
            return _SelectEditingEncuestasSinAdmisionPersonas(_Item.encId);
        }

        private void Create(catEncuestaSinAdmisionPersona pItem)
        {
            if (ModelState.IsValid)
            {
                context.catEncuestaSinAdmisionPersona.AddObject(pItem);
                context.SaveChanges();
            }
            return;
        }
    
     
        [AcceptVerbs(HttpVerbs.Post)]
        public int AsignarPacienteEncuesta(long pacId, int plaId,int encCausa)
        {
            catEncuestaSinAdmisionPersona _Item = new catEncuestaSinAdmisionPersona();

            try
            {
                _Item.pacId = pacId;
                _Item.plaId = plaId;
                _Item.encCausa = encCausa;
               // _Item.espId = (short)espId;
                context.catEncuestaSinAdmisionPersona.AddObject(_Item);
                context.SaveChanges();
                //Registra log de usuario              
                //new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catEncuestaSinAdmisionPersona", "Asignar Paciente", "Asigna un Paciente a la encuesta ");

            }
            catch (Exception ex)
            {
                return 0;
            }

            //return Json(new
            //{
            //    IsValid = true,
            //    Mensaje = "OK",
            //    encId = _Item.encId
            //});
            return _Item.encId;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public int Actualiza_PacienteEncuesta(long pacId, int plaId, int encCausa,int encId)
        {
         //   catEncuestaSinAdmisionPersona _Item = new catEncuestaSinAdmisionPersona();
            catEncuestaSinAdmisionPersona _Item = context.catEncuestaSinAdmisionPersona.FirstOrDefault(p => p.encId == encId);
            try
            {
                _Item.encCausa = encCausa;
               // _Item.espId = (short)espId;
                context.SaveChanges();
                //Registra log de usuario              
                //new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catEncuestaSinAdmisionPersona", "Asignar Paciente", "Asigna un Paciente a la encuesta ");

            }
            catch (Exception ex)
            {
                return 0;
            }

          
            return _Item.encId;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _DeleteEditingEncuestaSinAdmisionPersonas(int id)
        {
            int plaId = (int)context.catEncuestaSinAdmisionPersona.Single(w => w.encId == id).plaId;
            
            DeleteConfirmedEncuestaSinAdmisionPersonas(id);
            return _SelectEditingEncuestasSinAdmisionPersonas(plaId);
        }


        private void DeleteConfirmedEncuestaSinAdmisionPersonas(int id)
        {
            catEncuestaSinAdmisionPersona _Item = context.catEncuestaSinAdmisionPersona.Single(x => x.encId == id);
           // DeleteConfirmedenlEncuestasAPSPersonasEnfxPersona(_Item.encId);
            context.catEncuestaSinAdmisionPersona.DeleteObject(_Item);
            context.SaveChanges();
        }

       

    }
}

