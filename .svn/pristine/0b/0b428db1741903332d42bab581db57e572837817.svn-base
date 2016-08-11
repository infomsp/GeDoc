using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Json;
using System.Linq;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.UI.WebControls.Expressions;
using GeDoc.Filters;
using GeDoc.Models;
using Telerik.Web.Mvc;

namespace GeDoc.Controllers
{
    public partial class catEncuestaAPSController : Controller
    {
       
        
        private efAccesoADatosEntities context = new efAccesoADatosEntities();

        
        public ActionResult Index()
        {
          
          
           // ViewBag.OpenOnFocus = false;
           
            ViewData["StyleComboBox"] = "width: 210px;";
            ViewData["depEncId_dropDownStyle"] = "width: 210px";
            ViewData["depEncId_style"] = "width: 210px";
            ViewData["Encuesta"] = new List<catEncuestasAPS>();
            ViewData["EncuestaAPSPersonas"] = new List<catEncuestaAPSPersonas>();
            ViewData["Pacientes"] = new List<catEncuestasPacientes>();
            PopulateDropDownList();
           // PopulateDropDownListPaciente();
            //PopulateDropDownListEnlPacEnf();

            ViewData["FiltroContains"] = true;
            return PartialView();
        }
       
     //   [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _SelectEditing(bool SoloMisEncuestas)
        {
            return View(new GridModel(All(SoloMisEncuestas)));
        }

        private IList<catEncuestasAPS> All(bool SoloMisEncuestas)
        {
            return getDatos(SoloMisEncuestas).ToList();
        }
        private IEnumerable<catEncuestasAPS> getDatos(bool SoloMisEncuestas)
        {
            if (Session["UsuarioCentroDeSalud"] == null)
            {
                RedirectToAction("LogOff", "Account");
                return null;
            }         
            var _MetodosUtiles = new MetodosUtiles();
            try
            {               
                var _csId = (Session["UsuarioCentroDeSalud"] as sisUsuariosCentroDeSalud).csId;
                var _usrId = (Session["Usuario"] as sisUsuario).usrId;
                if (!SoloMisEncuestas)//si false muestra las encuestas de todos los usuarios
                {
                    _usrId = -1;
                }


                var user = (Session["Usuario"] as GeDoc.Models.sisUsuario);
                if (user.esExterno==true)
                {
                    var _Datos = (from d in context.spGetListadoEncuestaAPSExternos(_usrId).ToList()
                                  orderby d.encId descending
                                  select new catEncuestasAPS()
                                  {
                                      encId = d.encId,
                                      depEncId = d.depId,
                                      locEncId = (short)d.locId,
                                      depNombre = d.depNombre,
                                      barEncId = d.barId,
                                      barNombre = d.barNombre,
                                      encDomCalle = d.encDomCalle,
                                      encDomNro = d.encDomNro ?? 0,
                                      encDomMzna = d.encDomMzna,
                                      encDomSector = d.encDomSector,
                                      encDomMonoblock = d.encDomMonoblock,
                                      encDomPiso = d.encDomPiso,
                                      encDomDpto = d.encDomDpto,
                                      encTelFijo = d.encTelFijo,
                                      encTelCel = d.encTelCel,
                                      csEncId = (short)d.csId,
                                      csNombre = d.centroNombre,
                                      encuestadorId = d.encuestadorId,
                                      encuestadorNombre = d.encuestadorApyNom,
                                      encFechaRetira = d.encFechaRetira,
                                      encFechaCarga = d.encFechaCarga,
                                      zonaNombre = d.cszNombre,
                                      usrNombre = d.usrApellidoyNombre,
                                      usrId = d.usrId,
                                      cantidad = d.cantidad,
                                      encRedesPuntaje = d.encRedesPuntaje
                                  }).ToList();
                    return _Datos.AsEnumerable();
                }
                else
                {
                    var _Datos = (from d in context.spGetListadoEncuestaAPS(_usrId).ToList()
                                  orderby d.encId descending
                                  select new catEncuestasAPS()
                                  {
                                      encId = d.encId,
                                      depEncId = d.depId,
                                      locEncId = (short)d.locId,
                                      depNombre = d.depNombre,
                                      barEncId = d.barId,
                                      barNombre = d.barNombre,
                                      encDomCalle = d.encDomCalle,
                                      encDomNro = d.encDomNro ?? 0,
                                      encDomMzna = d.encDomMzna,
                                      encDomSector = d.encDomSector,
                                      encDomMonoblock = d.encDomMonoblock,
                                      encDomPiso = d.encDomPiso,
                                      encDomDpto = d.encDomDpto,
                                      encTelFijo = d.encTelFijo,
                                      encTelCel = d.encTelCel,
                                      csEncId = (short)d.csId,
                                      csNombre = d.centroNombre,
                                      encuestadorId = d.encuestadorId,
                                      encuestadorNombre = d.encuestadorApyNom,
                                      encFechaRetira = d.encFechaRetira,
                                      encFechaCarga = d.encFechaCarga,
                                      zonaNombre = d.cszNombre,
                                      usrNombre = d.usrApellidoyNombre,
                                      usrId = d.usrId,
                                      cantidad = d.cantidad,
                                      encRedesPuntaje = d.encRedesPuntaje
                                  }).ToList();
                    return _Datos.AsEnumerable();
                }


                
            }
            catch (Exception ex)
            { }

            return null;

        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _SaveEditing(int id, bool SoloMisEncuestas)
        {
            var updatePatients = from a in context.catEncuestaAPSPersonas
                                 join b in context.catPaciente on a.pacId equals b.pacId
                                 where a.encId == id
                                 select b;

            updatePatients.ToList().ForEach(c =>
            {
                c.depId =           !String.IsNullOrEmpty(Request["depEncId"])     ? short.Parse(Request["depEncId"]) : c.depId;
                c.locId =           !String.IsNullOrEmpty(Request["locEncId"])     ? short.Parse(Request["locEncId"]) : c.locId;
                c.barId =           !String.IsNullOrEmpty(Request["barId"])        ? short.Parse(Request["barId"])    : c.barId;
                c.csId =            !String.IsNullOrEmpty(Request["csEncId"])      ? int.Parse(Request["csEncId"])    : c.csId;
                c.pacCalle =        !String.IsNullOrEmpty(Request["encDomCalle"])  ? Request["encDomCalle"]           : c.pacCalle;
                c.pacCalleNumero =  !String.IsNullOrEmpty(Request["encDomNro"])    ? short.Parse(Request["encDomNro"]): c.pacCalleNumero;
                c.pacManzana =      !String.IsNullOrEmpty(Request["encDomMzna"])   ? Request["encDomMzna"]            : c.pacManzana;
                c.pacDepto =        !String.IsNullOrEmpty(Request["encDomDpto"])   ? Request["encDomDpto"]            : c.pacDepto;
                c.pacTelefonoCasa = !String.IsNullOrEmpty(Request["encTelFijo"])   ? Request["encTelFijo"]            : c.pacTelefonoCasa;
                c.pacPiso =         !String.IsNullOrEmpty(Request["encDomPiso"])       || 
                                    !String.IsNullOrEmpty(Request["encDomMonoblock"])  ||
                                    !String.IsNullOrEmpty(Request["encDomSector"])  ? "Piso: "   + Request["encDomPiso"]      + " - " +
                                                                                      "Mblock: " + Request["encDomMonoblock"] + " - " +
                                                                                      "Sector: " + Request["encDomSector"] 
                                                                                                                      : c.pacPiso;
            });


            var _usrId = (Session["Usuario"] as sisUsuario).usrId;
            if (id > 0)
            {
                var _Item = context.catEncuestaAPS.Single(x => x.encId == id);
                TryUpdateModel(_Item);
                _Item.usrId = _usrId;
                _Item.depId = short.Parse(Request["depEncId"]);
                _Item.locId = short.Parse(Request["locEncId"]);
                _Item.barId = int.Parse(Request["barEncId"]);
                _Item.csId = int.Parse(Request["csEncId"]);
                _Item.encFechaCarga = DateTime.Now;
                _Item.encDomCalle = _Item.encDomCalle == null ? null : _Item.encDomCalle.ToUpper();
                _Item.encDomMzna = _Item.encDomMzna == null ? null : _Item.encDomMzna.ToUpper();
                _Item.encDomMonoblock = _Item.encDomMonoblock == null ? null : _Item.encDomMonoblock.ToUpper();
                _Item.encDomPiso = _Item.encDomPiso == null ? null : _Item.encDomPiso.ToUpper();
                Edit(_Item);
            }
            return View(new GridModel(All(SoloMisEncuestas)));

        }
        

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _InsertEditing(bool SoloMisEncuestas)
        {
            catEncuestaAPS _Item = new catEncuestaAPS();
            var _usrId = (Session["Usuario"] as sisUsuario).usrId;
            _Item.depId = short.Parse(Request["depEncId"]);
            _Item.locId = short.Parse(Request["locEncId"]);
            _Item.barId = int.Parse(Request["barEncId"]);
            _Item.csId = int.Parse(Request["csEncId"]);
            if (TryUpdateModel(_Item))
            {
                new catEncuestaAPS();
                _Item.usrId = _usrId;
                _Item.encFechaCarga = DateTime.Now;  
                //_Item.depId = 
                    Create(_Item);
                    return View(new GridModel(All(SoloMisEncuestas)));
            }
            else
            {
                var errores = new JsonArray();
                var erroresModelo = ModelState.Values
                            .Where(e => e.Errors.Any())
                            .Select(e => (JsonValue)e.Errors.First().ErrorMessage)
                            .ToArray();
                errores.AddRange(erroresModelo);
                return View(new GridModel(All(SoloMisEncuestas)));

            }
        }
        private void Create(catEncuestaAPS pItem)
        {
            catEncuestaAPS err = TempData["error"] as catEncuestaAPS;
            if (!ModelState.IsValid)
            {
                var errores = new JsonArray();
                var erroresModelo = ModelState.Values
                            .Where(e => e.Errors.Any())
                            .Select(e => (JsonValue)e.Errors.First().ErrorMessage)
                            .ToArray();
                errores.AddRange(erroresModelo);

            }
            else
            {                
                try
                {
                    context.catEncuestaAPS.AddObject(pItem);
                    //Registra log de usuario
                    //new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catPaciente", "Agregar", "Agrega al paciente " + pItem.pacApellido + " " + pItem.pacNombre + " pacDoc " + pItem.pacNumeroDocumento);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("encId", ex.Message);
                }
            }


            return;
        }

    

        private void Edit(catEncuestaAPS pItem)
        {
            if (ModelState.IsValid)
            {
                context.SaveChanges();
            }
            return;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _DeleteEditing(int id,bool SoloMisEncuestas)
        {
            if (Session["UsuarioCentroDeSalud"] == null)
            {
                RedirectToAction("LogOff", "Account");
                return null;
            }
            //DeleteConfirmed(id, false);

            return View(new GridModel(All(SoloMisEncuestas)));
        }
        private void DeleteConfirmed(int encId)
        {
            catEncuestaAPS _Item = context.catEncuestaAPS.Single(x => x.encId == encId);
            context.catEncuestaAPS.DeleteObject(_Item);
            context.SaveChanges();
        }


        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _SaveEditingPersona(int id)
        {
            if (id > 0)
            {
                if (Session["UsuarioCentroDeSalud"] == null)
                {
                    RedirectToAction("LogOff", "Account");
                    return null;
                }
                catPaciente _Item = context.catPaciente.FirstOrDefault(p => p.pacId == id);

                if (TryUpdateModel(_Item))
                {

                   // Edit(_Item);

                }
                if (Session["upArchivo"] != null)
                {
                    Session["upArchivo"] = null;
                }
            }
            return View(new GridModel(AllPacientes2()));



        }
        private IList<catTurnosPacientes> AllPacientes2()
        {
            return getDatosPersonas().ToList();
        }
        private IEnumerable<catTurnosPacientes> getDatosPersonas()
        {
            if (Session["UsuarioCentroDeSalud"] == null)
            {
                RedirectToAction("LogOff", "Account");
                return null;
            }
            //var _csId = (Session["UsuarioCentroDeSalud"] as GeDoc.sisUsuariosCentroDeSalud).csId;
            var _MetodosUtiles = new MetodosUtiles();
            try
            {
                // var _Sector = (Session["UsuarioCentroDeSalud"] as sisUsuariosCentroDeSalud).csId;
                var _csId = (Session["UsuarioCentroDeSalud"] as sisUsuariosCentroDeSalud).csId;
                var _Datos = (from d in context.spGetListadoDePacientes2(_csId).ToList()
                              orderby d.pacId descending
                              select new catTurnosPacientes()
                              {
                                  pacId = d.pacId,
                                  pacApellido = d.pacApellido.ToUpper(),
                                  pacNombre = d.pacNombre.ToUpper(),
                                  pacApellidoyNombre = d.pacApellido + " " + d.pacNombre,
                                  pacNumeroDocumento = d.pacNumeroDocumento,
                                  tipoIdTipoDocumento = d.tipoIdTipoDocumento,
                                  tipoDescDocumento = d.tipoDescDocumento,
                                  pacCUIL = d.pacCUIL,
                                  paisId = d.paisId,
                                  paisNombre = d.paisNombre,
                                  tipoIdSexo = d.tipoIdSexo,
                                  tipoSexoNombre = d.tipoIdSexoTexto,
                                  pacFechaNacimiento = d.pacFechaNacimiento,
                                  //pacEdad = d.pacFechaNacimiento == null ? (short)0 : (short)_MetodosUtiles.Edad(d.pacFechaNacimiento),
                                  tipoIdEstadoCivil = d.tipoIdEstadoCivil,
                                  DescEstadoCivil = d.tipoEstadoCivilNombre,
                                  tipoIdOcupacion = d.tipoIdOcupacion,
                                  pacDonaOrganos = d.pacDonaOrganos ?? false,
                                  tipoIdGrupoSanguineo = d.tipoIdGrupoSanguineo,
                                  pacCalle = d.pacCalle,
                                  pacCalleNumero = d.pacCalleNumero,
                                  pacDomicilioReferencias = d.pacDomicilioReferencias == null ? d.pacDomicilioReferencias : d.pacDomicilioReferencias.ToUpper(),
                                  depId = d.depId,
                                  depNombre = d.depNombre,
                                  locId = d.locId,
                                  locNombre = d.locNombre,
                                  barId = d.barId,
                                  barNombre = d.barNombre,
                                  pacTelefonoCasa = d.pacTelefonoCasa,
                                  pacTelefonoTrabajo = d.pacTelefonoTrabajo,
                                  pacTelefonoCelular = d.pacTelefonoCelular,
                                  pacNotificarXSMS = (bool)(d.pacNotificarXSMS ?? false),
                                  osId = d.osId,
                                  osNombre = d.osDenominacion,
                                  pacHospitalizado = d.pacHospitalizado ?? false,
                                  provId = (short)d.provId,
                                  provNombre = d.provNombre,
                                  pacMail = d.pacMail,
                                  etnId = (short)d.etnId,
                                  csaId = d.csaId == null ? (short)0 : (short)d.csaId,
                                  nroHC = d.nroHistClin,

                              }).ToList();
                return _Datos.AsEnumerable();
            }
            catch (Exception ex)
            { }

            return null;

        }

        public ActionResult getPaciente(int pacienteId)
        {
            var _MetodosUtiles = new MetodosUtiles();
            //Registra log de usuario
            new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catPaciente", "Examinar", "");
            var _csId = (Session["UsuarioCentroDeSalud"] as sisUsuariosCentroDeSalud).csId;

            catEncuestasPacientes _Datos = (from d in context.spGetListadoDePacientes2((short)_csId).ToList()
                                   where d.pacId == (short)pacienteId
                                   select new catEncuestasPacientes()
                                   {
                                       pacId = d.pacId,
                                       pacApellido = d.pacApellido.ToUpper(),
                                       pacNombre = d.pacNombre.ToUpper(),
                                       pacNumeroDocumento = d.pacNumeroDocumento,
                                       tipoIdTipoDocumento = d.tipoIdTipoDocumento,
                                       tipoDescDocumento = d.tipoDescDocumento,
                                       pacCUIL = d.pacCUIL,
                                       paisId = d.paisId,
                                       paisNombre = d.paisNombre,
                                       provNombre = d.provNombre,
                                       tipoSexoNombre = d.tipoIdSexoTexto,
                                       pacFechaNacimiento = d.pacFechaNacimiento,
                                       //pacFechaNacimientoTexto = d.pacFechaNacimiento.Value.ToString("dd/MM/yyyy"),
                                       pacEdad = d.pacFechaNacimiento == null ? (short)0 : (short)_MetodosUtiles.Edad(d.pacFechaNacimiento),
                                       DescEstadoCivil = d.tipoEstadoCivilNombre,
                                       //OcupacionDescripcion = d.des,
                                       //  pacDonaOrganos = d.pacDonaOrganos,
                                       // tipoIdGrupoSanguineo = d.tipoIdGrupoSanguineo,
                                       pacCalle = d.pacCalle == null ? "" : d.pacCalle.ToUpper(),
                                       pacCalleNumero = Convert.ToInt16(d.pacCalleNumero),
                                       //  pacDomicilioReferencias = d.pacDomicilioReferencias == null ? d.pacDomicilioReferencias : d.pacDomicilioReferencias.ToUpper(),
                                       depId = d.depId,
                                       depNombre = d.depNombre,
                                       locId = Convert.ToInt16(d.locId),
                                       locNombre = d.locNombre,
                                       barId = Convert.ToInt16(d.barId),
                                       barNombre = d.barNombre,
                                       pacTelefonoCasa = d.pacTelefonoCasa,
                                       pacTelefonoTrabajo = d.pacTelefonoTrabajo,
                                       pacTelefonoCelular = d.pacTelefonoCelular,
                                       //  pacNotificarXSMS = (bool)(d.pacNotificarXSMS == null ? false : d.pacNotificarXSMS),                                         osId = d.osId,
                                       osNombre = d.osDenominacion,
                                       pacHospitalizado = d.pacHospitalizado == null ? false : (bool)d.pacHospitalizado,
                                       pacImagenHospitalizado = getHospitalizado(d.pacHospitalizado == null ? false : (bool)d.pacHospitalizado),
                                       // pacTransfusionesDeSangre = d.pacTransfusionesDeSangre,
                                       /* pacTransfusionesDeSangreUltima = d.pacTransfusionesDeSangreUltima == null ? DateTime.Now : (DateTime)d.pacTransfusionesDeSangreUltima,*/
                                       pacMail = d.pacMail,
                                       csId = d.csId == null ? (short)0 : (short)d.csId,
                                       // nroHC = getNroHc(d.pacId, _csId),
                                       nroHC = d.nroHistClin,
                                   }).FirstOrDefault();

            return Json(_Datos, JsonRequestBehavior.AllowGet);
        }
         private bool disposed = false;
        protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                // Free other state (managed objects).
            }
            // Free your own state (unmanaged objects).
            // Set large fields to null.
            disposed = true;
        }
    }    


      [AcceptVerbs(HttpVerbs.Post)]
      public ActionResult getPadron(int doc, int tipoDoc,int encId,string Accion)
      {
          catPaciente error = new catPaciente();
            var _csId = (Session["UsuarioCentroDeSalud"] as sisUsuariosCentroDeSalud).csId;
            //var _Paciente = context.spGetListadoDePacientes2((short)_csId).Where(s => s.pacNumeroDocumento == doc && s.tipoIdTipoDocumento == (short)tipoDoc && s.pacNumeroDocumento != 11111).FirstOrDefault();
            var _MetodosUtiles = new MetodosUtiles();

            var _Datos = new catEncuestasPacientes();
            _Datos.pacAccion = Accion;

            //Cargamos los datos de la encuesta
            var DatosActuales = context.catEncuestaAPS.Single(w => w.encId == encId);
            var centroSalud = context.catCentroDeSalud.Single(w => w.csId == DatosActuales.csId);
            Session["Encuesta_csId"] = DatosActuales.csId;
          
           
            _Datos.PacienteEnfermedad = new List<enlEncuestaAPSPersonasEnfermedades>();

            var _encuestado = (from d in context.catEncuestaAPSPersonas
                             where d.catPaciente.pacNumeroDocumento == doc & d.catPaciente.tipoIdTipoDocumento == tipoDoc
                             select new catEncuestasAPSPersonas()
                             {
                                 encPerId = d.encPerId
                             }).FirstOrDefault();
          //SOLO PACIENTES : Busca en la tabla Pacientes
            var _Paciente = (from d in context.spGetBusquedaEnPacientes_PADRON(tipoDoc,doc).ToList()
                            // where d.pacNumeroDocumento == doc && d.tipoIdTipoDocumento == (short)tipoDoc 
                             select new catEncuestasPacientes()
                             {
                                 pacId = d.pacId,
                                 pacApellido = d.pacApellido.ToUpper(),
                                 pacNombre = d.pacNombre.ToUpper(),
                                 pacNumeroDocumento = d.pacNumeroDocumento,
                                 tipoIdTipoDocumento = d.tipoIdTipoDocumento,
                                 tipoIdSexo = d.tipoIdSexo ,
                                 tipoIdOcupacion = d.tipoIdOcupacion ?? 65,
                                 tipoIdGrupoSanguineo = d.tipoIdGrupoSanguineo ?? 45,
                                 tipoIdEstadoCivil = d.tipoIdEstadoCivil ?? 29,                               
                                 pacCUIL = d.pacCUIL ?? "",
                                 paisId = d.paisId ?? 1,     
                          
                                 pacFechaNacimiento = d.pacFechaNacimiento ?? DateTime.Now.Date,
                                 pacFechaNacimientoTexto = d.pacFechaNacimiento == null ? DateTime.Now.Date.ToString("dd/MM/yyyy"): d.pacFechaNacimiento.Value.ToString("dd/MM/yyyy"),                                                                                          
                                 provId = 17,
                                 depId = DatosActuales.depId,
                                 locId = DatosActuales.locId ,
                                 barId = DatosActuales.barId,
                                 csId = (short)DatosActuales.csId,
                                 pacCalle = DatosActuales.encDomCalle,
                                 pacCalleNumero = DatosActuales.encDomNro ?? 0,
                                 pacDepto = DatosActuales.encDomDpto,
                                 pacManzana = DatosActuales.encDomMzna,
                                 pacPiso = null, //"Piso: " + DatosActuales.encDomPiso + " - Mblock: " + DatosActuales.encDomMonoblock + " - Sector: " + DatosActuales.encDomSector,
                                 csNombre = centroSalud.csNombre,                                           
                                 pacTelefonoCasa = d.pacTelefonoCasa,
                                 pacTelefonoTrabajo = d.pacTelefonoTrabajo,
                                 pacTelefonoCelular = d.pacTelefonoCelular,
                                 pacNotificarXSMS = (bool)(d.pacNotificarXSMS ?? false),                                         
                                 osId = d.osId ?? 305,
                                 pacMail = d.pacMail,
                                 etnId = (short)d.etnId == null? (short)0 :(short) d.etnId ,
                                 PacienteEnfermedad = _encuestado == null ? getDatosenlEncuestasAPSPersonasEnf(0).ToList() : getDatosenlEncuestasAPSPersonasEnf((short)_encuestado.encPerId).ToList(),
                                 csaId = d.csaId == null ? (short)0 : (short)d.csaId,
                                 pacAccion = "Agregar"
                             }).FirstOrDefault();
            if (_Paciente != null)
            {
               // var _Msj = "El Paciente " + _Paciente.pacNumeroDocumento + " - " + _Paciente.pacNombre + " " + _Paciente.pacApellido + " ya existe en la tabla Pacientes.";

                _Paciente.pacFechaNacimiento = _Paciente.pacFechaNacimiento ?? DateTime.Now.Date;
                return (Json(_Paciente));
              
            }
            else
            {               
                if (doc != null)
                {
                  //SOLO PADRON : Busca en la tabla padron 
                     tipoDoc = tipoDoc - 36;
                     _Paciente = (from d in context.spGetBusquedaPadron(tipoDoc, doc).ToList()
                                /*  where doc == d.ppNroDoc
                                  && (tipoDoc - 36) == (int)d.tdocId*/
                                  select new catEncuestasPacientes()
                                  {                                   
                                   pacApellido = d.ppApellido.ToUpper(),
                                   pacNombre = d.ppNombre.ToUpper(),
                                   pacNumeroDocumento = d.ppNroDoc,
                                   tipoIdTipoDocumento = 10,
                                   tipoIdSexo = d.ppSexoId == "F" ? (short) 9 : (short) 10,  
                                   tipoIdOcupacion = 65,
                                   tipoIdGrupoSanguineo = 45,
                                   tipoIdEstadoCivil = 29,                                  
                                   pacCUIL = d.ppCUIL ?? "",
                                   paisId = (short) 1,
                                   pacFechaNacimiento = d.ppFechaNacimiento ?? DateTime.Now.Date,
                                   pacFechaNacimientoTexto = d.ppFechaNacimiento == null ? DateTime.Now.Date.ToString("dd/MM/yyyy") : d.ppFechaNacimiento.Value.ToString("dd/MM/yyyy"),                                                                                                
                                   pacTelefonoCasa = d.ppTelefono1,                                 
                                   pacTelefonoCelular = d.ppTelefono2,                               
                                   PacienteEnfermedad = _encuestado == null ? getDatosenlEncuestasAPSPersonasEnf(0).ToList() : getDatosenlEncuestasAPSPersonasEnf((short)_encuestado.encPerId).ToList(),                                    
                                   etnId = 0,
                                   osId = (short)305,
                                   provId = 17,
                                  depId= DatosActuales.depId,
                                  locId = DatosActuales.locId,
                                  barId = DatosActuales.barId,
                                  csId = (short)DatosActuales.csId,
                                  pacCalle = DatosActuales.encDomCalle,
                                  pacCalleNumero = DatosActuales.encDomNro ?? 0,
                                  pacDepto = DatosActuales.encDomDpto,
                                  pacManzana = DatosActuales.encDomMzna,
                                  pacPiso = null, //"Piso: " + DatosActuales.encDomPiso + " - Mblock: " + DatosActuales.encDomMonoblock + " - Sector: " + DatosActuales.encDomSector,
                                  csNombre = centroSalud.csNombre,                                                                            
                                 pacAccion = "Agregar",   
                                  }).FirstOrDefault();
                     if (_Paciente != null )
                     {    
                          
                         return (Json(_Paciente));

                     }
                     if (_Paciente == null)
                     {
                         //NUEVO PACIENTE : No esta en tabla paciente ni padron
                         _Paciente = new catEncuestasPacientes();
                         _Paciente.provId = 17;
                         _Paciente.depId = DatosActuales.depId;
                         _Paciente.locId = DatosActuales.locId;
                         _Paciente.barId = DatosActuales.barId;
                         _Paciente.csId = (short)DatosActuales.csId;
                         _Paciente.pacCalle = DatosActuales.encDomCalle;
                         _Paciente.pacCalleNumero = DatosActuales.encDomNro ?? 0;
                         _Paciente.pacDepto = DatosActuales.encDomDpto;
                         _Paciente.pacManzana = DatosActuales.encDomMzna;
                         _Paciente.pacPiso = null; //"Piso: " + DatosActuales.encDomPiso + " - Mblock: " + DatosActuales.encDomMonoblock + " - Sector: " + DatosActuales.encDomSector;
                         _Paciente.csNombre = centroSalud.csNombre;
                         _Paciente.pacFechaNacimiento = _Paciente.pacFechaNacimiento ?? DateTime.Now.Date;
                         return (Json(_Paciente));

                     }
                }
            }
            return (Json(_Paciente));
        }

      private IEnumerable<enlEncuestaAPSPersonaEnfermedad> ObtenerEnfermedades(string _Datos, int _encPerId)
      {
         enlEncuestaAPSPersonaEnfermedad _Item = new enlEncuestaAPSPersonaEnfermedad();        
         List<enlEncuestaAPSPersonaEnfermedad> _listadoEnfermedades = new List<enlEncuestaAPSPersonaEnfermedad>();
         if (_Datos != null)
         {
             string[] split = _Datos.Split(new Char[] { '-' });
             for (int i = 0; i < split.Count() - 1; i++)
             {
                 split[i] = split[i].Trim();
                 split[i] = split[i].Remove(0, 1);
                 split[i] = split[i].Remove(split[i].Length - 1, 1);
                 var split2 = split[i].Split(new Char[] { ',' });
                 if (split2.Count() > 3)
                 {
                     _Item.encPerId = _encPerId;
                     _Item.enfId = Int32.Parse(split2[2]);
                     _Item.encEnfDescripcionComentario = split2[3];
                     AsignarPersonaEnfermedad((int)_Item.enfId, (int)_Item.encPerId, _Item.encEnfDescripcionComentario);
                     // _listadoEnfermedades.Add(_Item);
                 }
             }
         }
       // var _listadoEnfermedades3 = _Datos.Split('-').ToList();

         return _listadoEnfermedades;
      }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult setDatosPaciente(catEncuestasPacientes Datos)
        {
             var _tipoAgregar=0;
            var _DatosPacEnf = Request["ListadoEnfermedadesPaciente[]"];
            var encPerId =0;
            if (Datos.pacAccion == "Modificar" || Datos.pacAccion == "Agregar")
            {

                if (Datos.pacNumeroDocumento == null)
                {
                    return Json(new
                    {
                        Error = true,
                        Mensaje = "Debe ingresar el numero de documento.",
                        Foco = "pacNumeroDocumento"
                    });
                }
                if (Datos.pacId > 0)// EL PACIETE SI EXISTE 
                {                  
                    _tipoAgregar = 2;
                }
                else//VALIDAMOS SU NRO DE DOCUMENTO YA EXISTE
                {
                   
                    var _Paciente = context.catPaciente.FirstOrDefault(s => s.pacNumeroDocumento == Datos.pacNumeroDocumento && 
                                                                            s.tipoIdTipoDocumento == (short)Datos.tipoIdTipoDocumento && 
                                                                            s.pacNumeroDocumento != 11111);
                    _tipoAgregar = 1;
                    if (_Paciente != null && Datos.pacAccion == "Agregar")
                    {
                        return Json(new
                        {
                            Error = true,
                            Mensaje = "El Paciente " + _Paciente.pacNombre + " " + _Paciente.pacApellido + ", ya existe.",
                            Foco = "pacNumeroDocumento"
                        });
                    }
                }
                if (Datos.pacApellido == null)
                {
                    return Json(new
                    {
                        Error = true,
                        Mensaje = "Debe ingresar el Apellido",
                        Foco = "pacApellido"
                    });
                }

                if (Datos.pacNombre == null)
                {
                    return Json(new
                    {
                        Error = true,
                        Mensaje = "Debe ingresar el Nombre",
                        Foco = "pacNombre"
                    });
                }
                if (Datos.pacFechaNacimiento == null)
                {
                    return Json(new
                    {
                        Error = true,
                        Mensaje = "Debe ingresar fecha de nacimiento",
                        Foco = "pacFechaNacimiento"
                    });
                }
            }
            var ItemPaciente = new catPaciente();
            try
            {
                switch (Datos.pacAccion)
                {
                    case "Agregar":
                        if (Datos.pacAccion == "Agregar")
                        {
                            if (Datos.pacId > 0)
                            {
                                ItemPaciente = context.catPaciente.Single(x => x.pacId == Datos.pacId);    
                                //Actualiza Paciente
                                TryUpdateModel(ItemPaciente);
                                ItemPaciente.pacFechaNacimiento = Datos.pacFechaNacimiento;
                                ItemPaciente.pacDomicilioReferencias = Datos.pacDomicilioReferencias;
                                ItemPaciente.pacManzana = Datos.pacManzana;
                                ItemPaciente.pacPiso = Datos.pacPiso;
                                ItemPaciente.provId = 17;
                                ItemPaciente.pacDonaOrganos = false;
                                ItemPaciente.pacNotificarXSMS = false;
                                ItemPaciente.paisId = Datos.paisId == null || Datos.paisId == -1 ? 0 : Datos.paisId;
                                ItemPaciente.depId = Datos.depId == null || Datos.depId == -1 ? 0 : Datos.depId;
                                ItemPaciente.locId = Datos.locId == null || Datos.locId == -1 ? 0 : Datos.locId;
                                ItemPaciente.barId = Convert.ToInt16(Datos.barId == null || Datos.barId == -1 ? 0 : Datos.barId);
                                ItemPaciente.osId = Convert.ToInt16(Datos.osId == null || Datos.osId == 0 || Datos.osId == -1 ? 305 : Datos.osId);
                                ItemPaciente.etnId = Datos.etnId == null || Datos.etnId == -1 ? 0 : Datos.etnId;
                                ItemPaciente.tipoIdEstadoCivil = Datos.tipoIdEstadoCivil == null ? 29 : Datos.tipoIdEstadoCivil;
                                context.SaveChanges();

                                //Asigna el paciente a la encuesta
                              JsonResult  _encPerId = (JsonResult)AsignarPacienteEncuesta(Datos.pacId, Datos.encId);
                               encPerId = (int)_encPerId.Data.GetType().GetProperty("encPerId").GetValue(_encPerId.Data,null);
                              //Asigna enfermedad al paciente                             
                             // List<enlEncuestaAPSPersonaEnfermedad> _enf = ObtenerEnfermedades(_DatosPacEnf, encPerId).ToList();
                            }
                            else
                            {
                                //Agrega el nuevo paciente en tabla pacientes
                                TryUpdateModel(ItemPaciente);
                                //ItemPaciente.pacFechaNacimiento = Convert.ToDateTime(Datos.pacFechaNacimientoTexto);
                                ItemPaciente.pacFechaNacimiento = Datos.pacFechaNacimiento;
                                ItemPaciente.pacDomicilioReferencias = Datos.pacDomicilioReferencias;
                                ItemPaciente.pacManzana = Datos.pacManzana;
                                ItemPaciente.pacPiso = Datos.pacPiso;
                                ItemPaciente.provId = 17;
                                ItemPaciente.pacDonaOrganos = false;
                                ItemPaciente.pacNotificarXSMS = false;
                                ItemPaciente.paisId = Datos.paisId == null || Datos.paisId == -1 ? 0 : Datos.paisId;
                                ItemPaciente.depId = Datos.depId == null || Datos.depId == -1 ? 0 : Datos.depId;
                                ItemPaciente.locId = Datos.locId == null || Datos.locId == -1 ? 0 : Datos.locId;
                                ItemPaciente.barId = Convert.ToInt16(Datos.barId == null || Datos.barId == -1 ? 0 : Datos.barId);
                                ItemPaciente.osId = Convert.ToInt16(Datos.osId == null || Datos.osId == 0 || Datos.osId == -1 ? 305 : Datos.osId);
                                ItemPaciente.etnId = Datos.etnId == null || Datos.etnId == -1 ? 0 : Datos.etnId;
                                ItemPaciente.tipoIdEstadoCivil = Datos.tipoIdEstadoCivil == null ? 29 : Datos.tipoIdEstadoCivil;
                                context.catPaciente.AddObject(ItemPaciente);
                                context.SaveChanges();
                                //Asigna el paciente a la encuesta
                                JsonResult _encPerId = (JsonResult)AsignarPacienteEncuesta(ItemPaciente.pacId, Datos.encId);
                                encPerId = (int)_encPerId.Data.GetType().GetProperty("encPerId").GetValue(_encPerId.Data, null);                             
                            }
                        }
                        break;
                    case "Modificar"://modifica paciente asignado a una encuesta
                        //var NewPaciente = new catPaciente();
                        ItemPaciente = context.catPaciente.Single(x => x.pacId == Datos.pacId);    
                        if (Datos.pacAccion == "Modificar"){
                            TryUpdateModel(ItemPaciente);                           
                            //ItemPaciente.pacFechaNacimiento = Convert.ToDateTime(Datos.pacFechaNacimientoTexto);
                            ItemPaciente.pacFechaNacimiento = Datos.pacFechaNacimiento;
                            ItemPaciente.pacFechaNacimiento = Datos.pacFechaNacimiento;
                            ItemPaciente.pacDomicilioReferencias = Datos.pacDomicilioReferencias;
                            ItemPaciente.pacManzana = Datos.pacManzana;
                            ItemPaciente.pacPiso = Datos.pacPiso;
                            ItemPaciente.provId = 17;
                            ItemPaciente.pacDonaOrganos = false;
                            ItemPaciente.pacNotificarXSMS = false;
                            ItemPaciente.paisId = Datos.paisId == null || Datos.paisId == -1 ? 0 : Datos.paisId;
                            ItemPaciente.depId = Datos.depId == null || Datos.depId == -1 ? 0 : Datos.depId;
                            ItemPaciente.locId = Datos.locId == null || Datos.locId == -1 ? 0 : Datos.locId;
                            ItemPaciente.barId = Convert.ToInt16(Datos.barId == null || Datos.barId == -1 ? 0 : Datos.barId);
                            ItemPaciente.osId = Convert.ToInt16(Datos.osId == null || Datos.osId == 0 || Datos.osId == -1 ? 305 : Datos.osId);
                            ItemPaciente.etnId = Datos.etnId == null || Datos.etnId == -1 ? 0 : Datos.etnId;
                            ItemPaciente.tipoIdEstadoCivil = Datos.tipoIdEstadoCivil == null ? 29 : Datos.tipoIdEstadoCivil;
                             encPerId = context.catEncuestaAPSPersonas.Single(x => x.encId == Datos.encId && x.pacId == Datos.pacId).encPerId;                            
                            context.SaveChanges();                            
                           }
                        break;
                }
            }

            catch (Exception ex)
            {
                return Json(new
                {
                    Error = true,
                    Mensaje = ex.InnerException == null ? ex.Message : ex.InnerException.Message,
                    Foco = "pacNumeroDocumento"
                });
            }
            return Json(new
                {
                    Error = false,
                    Mensaje = "Se actualizaron los datos en forma correcta",
                    Foco = "pacNumeroDocumento",
                    pacId = ItemPaciente.pacId,
                    pacNombre = ItemPaciente.pacApellido + ", " + ItemPaciente.pacNombre,
                    _encPerId = encPerId
                });
        }


        private string getCentroSaludNombre(int _csId)
        {
            var CentroSalud = context.catCentroDeSalud.Single(w => w.csId == _csId);
            return CentroSalud.csNombre;
        }
        [GridAction]
        public ActionResult TabPaciente(string Accion="",   int pacId = 0,int encId = 0)
        {
            
            var _Datos = new catEncuestasPacientes();
            ViewData["FiltroContains"] = true;
            PopulateDropDownListPaciente();
            //if (Session["encPerId"] == null)
            //{
                _Datos.PacienteEnfermedad = new List<enlEncuestaAPSPersonasEnfermedades>();
            //}
            //else
            //{
            //    var encPerId = Session["encPerId"];
            //    _Datos.PacienteEnfermedad = getDatosenlEncuestasAPSPersonasEnf(int.Parse(encPerId.ToString())).ToList();
            //}

            if (encId>0){
            _Datos.pacAccion = Accion;
          
            //Cargamos los datos de la encuesta
            var DatosActuales = context.catEncuestaAPS.Single(w => w.encId == encId);
            var centroSalud = context.catCentroDeSalud.Single(w => w.csId == DatosActuales.csId);
            Session["Encuesta_csId"] = DatosActuales.csId;

            if (Accion == "Agregar")
            {

                _Datos.provId = 17;
                _Datos.depId = DatosActuales.depId;
                _Datos.locId = DatosActuales.locId;
                _Datos.barId = DatosActuales.barId;
                _Datos.csId = (short)DatosActuales.csId;
                _Datos.pacCalle = DatosActuales.encDomCalle;
                _Datos.pacCalleNumero = DatosActuales.encDomNro ?? 0;
                _Datos.pacDepto = DatosActuales.encDomDpto;
                _Datos.pacManzana = DatosActuales.encDomMzna;
                _Datos.pacPiso = null;// "Piso: " + DatosActuales.encDomPiso + " - Mblock: " + DatosActuales.encDomMonoblock + " - Sector: " + DatosActuales.encDomSector;
                _Datos.pacTelefonoCasa = null;//DatosActuales.encTelFijo;
                _Datos.pacTelefonoCelular = null;//DatosActuales.encTelCel;
                _Datos.csNombre = centroSalud.csNombre;
                _Datos.csaId = (short)centroSalud.csId;
                _Datos.encId = encId;

            }
            if (Accion == "Modificar")
            {
               // PopulateDropDownListPaciente();
                if (pacId > 0)
                {
                    var _encuestado = (from d in context.catEncuestaAPSPersonas
                                       where d.encId == encId && d.pacId == pacId
                                       select new catEncuestasAPSPersonas()
                                       {
                                           encPerId = d.encPerId
                                       }).FirstOrDefault();
                    Session["encPerId"] = _encuestado.encPerId;
                    var _Paciente = (from d in context.spGetBusquedaEnPacientes_PADRON(null,pacId)
                                     select new catEncuestasPacientes()
                                     {
                                         depId = d.depId,
                                         locId = d.locId,
                                         barId = d.barId,
                                         provId = (short)d.provId,
                                         csId = (short)d.csId,
                                         pacCalle = d.pacCalle ?? "",
                                         pacCalleNumero = d.pacCalleNumero ?? 0,
                                         pacDepto = d.pacDepto ?? "",
                                         pacManzana = d.pacManzana,
                                         pacPiso = d.pacPiso,
                                         pacDomicilioReferencias = d.pacDomicilioReferencias ?? "",
                                         pacTelefonoCasa = d.pacTelefonoCasa,
                                         pacTelefonoCelular = d.pacTelefonoCelular,
                                         csaId = (short)d.csaId,
                                         pacId = d.pacId,
                                         pacApellido = d.pacApellido.ToUpper(),
                                         pacNombre = d.pacNombre.ToUpper(),
                                         pacNumeroDocumento = d.pacNumeroDocumento,
                                         tipoIdTipoDocumento = d.tipoIdTipoDocumento,
                                         tipoIdSexo = d.tipoIdSexo,
                                         tipoIdOcupacion = d.tipoIdOcupacion,
                                         tipoIdGrupoSanguineo = d.tipoIdGrupoSanguineo,
                                         tipoIdEstadoCivil = d.tipoIdEstadoCivil,
                                         pacCUIL = d.pacCUIL,
                                         encId = encId,
                                         paisId = d.paisId ,
                                         pacFechaNacimiento = d.pacFechaNacimiento ?? DateTime.Now.Date,
                                        // pacFechaNacimientoTexto = d.pacFechaNacimiento == null ? DateTime.Now.Date.ToString("dd/MM/yyyy") : d.pacFechaNacimiento.Value.ToString("dd/MM/yyyy"),
                                         pacTelefonoTrabajo = d.pacTelefonoTrabajo,
                                         csNombre = getCentroSaludNombre((short)d.csId),
                                         //pacNotificarXSMS = (bool)(d.pacNotificarXSMS == null ? false : d.pacNotificarXSMS),
                                         osId = d.osId,
                                         pacMail = d.pacMail,
                                         etnId = (short)d.etnId,
                                         PacienteEnfermedad = getDatosenlEncuestasAPSPersonasEnf((short)_encuestado.encPerId).ToList(),
                                         pacAccion = "Modificar"
                                     }).FirstOrDefault();
                    _Datos = _Paciente;
                }
            }

            }

            Session["PacienteEnfermedades"] = _Datos.PacienteEnfermedad;
            return PartialView("CRUDencuestaPaciente",_Datos);
        }
        private string getNombreUsuario(int? _usrId)
        {

            var item = context.sisUsuario.Single(d => d.usrId == _usrId);
            return item.usrApellidoyNombre;
                 
        }


        //YA ENCUESTADO : Busca un paciente encuestado
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult getValidaSiPacienteAsignado(long _pacId=0, int _pacNumeroDocumento=0,int _tipoDoc = 0)
        {
            try
            {
                if (_pacNumeroDocumento > 0 && _tipoDoc > 0)
                {
                   var _itemPac = context.catPaciente.FirstOrDefault(d => d.pacNumeroDocumento ==  _pacNumeroDocumento && 
                                                                          d.tipoIdTipoDocumento == (short)_tipoDoc);
                   _pacId = _itemPac.pacId;               
                }
                var _Datos = (from d in context.vwEncuestaAPSencuestados
                              where d.pacId == _pacId
                              select new catEncuestasAPSPersonas()
                              {
                                 pacId = (short)d.pacId,
                                 encId = d.encId,                                
                                 ApellidoyNombre = d.pacApellido + ", " + d.pacNombre,
                                 usrId = d.usrId,
                                 encFechaCarga =  d.encFechaCarga,
                                 Documento = d.pacNumeroDocumento
                              }).ToList();


                if (_Datos.Any())
                {
                    _Datos.First().UsuariodeCarga = getNombreUsuario(_Datos.First().usrId);
                    _Datos.First().fechadeCarga =
                        DateTime.Parse(_Datos.First().encFechaCarga.Value.Day + "/" +
                                       _Datos.First().encFechaCarga.Value.Month + "/" +
                                       _Datos.First().encFechaCarga.Value.Year).ToString("dd/MM/yyyy"); 
                    //return Json(false);
                    return Json(new
                    {
                        Error = true,
                        Mensaje = _Datos.First().ApellidoyNombre + "\n \n se encuentra asignado a la encuesta Nro: " + _Datos.First().encId + "\n Usuario de Carga: "+ _Datos.First().UsuariodeCarga+"\n Fecha de Carga: "+ _Datos.First().fechadeCarga ,
                        //Foco = "pacNumeroDocumento"
                    });
                }
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Error = false
                });
            }

            return  Json(new
                    {
                        Error = false
                    });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult getValidaSiEstaAsignado(int _encId)
        {
           /* try
            {
                var catEncuestaAPS = context.catEncuestaAPSPersonasEnf.Single(w => w.encId == _encId);
                if (catEncuestaAPS.encEnfId != null)
                {
                    return Json(false);
                }
            }
            catch (Exception ex)
            {
                return Json(false);
            }
            */
            return Json(true);
        }
        private void CreatePaciente(catPaciente pItem)
        {
            catPaciente err = TempData["error"] as catPaciente;
            if (!ModelState.IsValid)
            {
                var errores = new JsonArray();
                var erroresModelo = ModelState.Values
                            .Where(e => e.Errors.Any())
                            .Select(e => (JsonValue)e.Errors.First().ErrorMessage)
                            .ToArray();
                errores.AddRange(erroresModelo);

            }
            else
            {
                //  return new HttpResponseMessage<JsonValue>(errores, HttpStatusCode.BadRequest);       
                try
                {
                    context.catPaciente.AddObject(pItem);
                    context.SaveChanges();
                    //Registra log de usuario
                    new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catPaciente", "Agregar", "Agrega al paciente " + pItem.pacApellido + " " + pItem.pacNombre + " pacDoc " + pItem.pacNumeroDocumento);

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("pacApellido", ex.Message);
                }
            }


            return;
        }
        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _InsertEditingPersona()
        {
            catPaciente _Item = new catPaciente();

            if (TryUpdateModel(_Item))
            {
                catPaciente error = new catPaciente();
                var _csId = (Session["UsuarioCentroDeSalud"] as sisUsuariosCentroDeSalud).csId;
                var _Paciente = context.spGetListadoDePacientes2(_csId).FirstOrDefault(s => s.pacNumeroDocumento == _Item.pacNumeroDocumento && s.tipoIdTipoDocumento == (short)_Item.tipoIdTipoDocumento && s.pacNumeroDocumento != 11111);
                if (_Paciente != null)
                {
                    ModelState.AddModelError("pacNumeroDocumento", "El Paciente " + _Paciente.pacNombre + " " + _Paciente.pacApellido + " ya existe.");

                }
                else
                {
                    CreatePaciente(_Item);
                    return View(new GridModel(AllPacientes2()));
                }

            }
            else
            {
                var errores = new JsonArray();
                var erroresModelo = ModelState.Values
                            .Where(e => e.Errors.Any())
                            .Select(e => (JsonValue)e.Errors.First().ErrorMessage)
                            .ToArray();
                errores.AddRange(erroresModelo);
                return View(new GridModel(AllPacientes2()));

            }
            return View(new GridModel(AllPacientes2()));

        }

        /*Busca una persona en la tabla Pacientes*/
        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _SelectEditingPersonaBusqueda(string TextoBuscado)
        {
           // var TextoBuscado = "";
            if (Session["UsuarioCentroDeSalud"] == null)
            {
                RedirectToAction("LogOff", "Account");
                return View(new GridModel(new List<catEncuestasPacientes>()));
            }
            try
            {
                var _csId = (Session["UsuarioCentroDeSalud"] as sisUsuariosCentroDeSalud).csId;
                var _Datos = (from d in context.spGetBusquedaPaciente(_csId, TextoBuscado).ToList()
                              orderby d.pacId descending
                              select new catEncuestasPacientes()
                              {
                                  pacId = d.pacId,
                                  pacApellido = d.pacApellido.ToUpper(),
                                  pacNombre = d.pacNombre.ToUpper(),
                                  pacApellidoyNombre = d.pacApellido + " " + d.pacNombre,
                                  pacNumeroDocumento = d.pacNumeroDocumento,
                              }).ToList();

                return View(new GridModel(_Datos));
            }
            catch (Exception ex)
            {
            }

            return View(new GridModel(new List<catEncuestasPacientes>()));
        }

        //Datos para el dropdown
        protected void PopulateDropDownList()
        {
            dcAccesoCompadDataContext _Centros = new dcAccesoCompadDataContext();
            var _csId = (Session["UsuarioCentroDeSalud"] as sisUsuariosCentroDeSalud).csId;
            var _dpto = (from s in context.catCentroDeSalud.ToList()
                         where s.csId == _csId
                         select new catCentroDeSalud()
                         {
                             depId = s.depId,

                         }).First();      
            var _DN = (from s in context.catDepartamento.ToList()
                       where s.provId == 17
                       orderby s.depNombre ascending
                       select new catDepartamento()
                       {
                           depId = s.depId,
                           depNombre = s.depNombre
                       }).ToList();

            var _LO = (from s in context.catLocalidad.ToList()
                       where s.depId == _DN.First().depId || s.locId == 0
                       orderby s.locNombre ascending
                       select new catLocalidad()
                       {
                           locId = s.locId,
                           locNombre = s.locNombre
                       }).ToList();

            var _BN = (from s in context.catBarrio.ToList()
                       where s.depId == _DN.First().depId || s.barId == 0
                       orderby s.barNombre ascending
                       select new catBarrio()
                       {
                           barId = s.barId,
                           barNombre = s.barNombre
                       }).ToList();         

            /*var _CTROS = (from s in context.catCentroDeSalud.ToList()
                          join x in context.catDepartamento on s.depId equals x.depId
                          where s.csId == _csId                       
                           select new catCentroDeSalud()
                           {
                               csId = s.csId,
                               csNombre = s.csNombre.TrimEnd() + " - (" + x.depNombre + ")"
                           }
                            ).ToList();
            _CTROS.AddRange((from s in context.catCentroDeSalud.ToList()
                             join x in context.catDepartamento on s.depId equals x.depId
                             select new catCentroDeSalud()
                             {
                                 csId = s.csId,
                                 csNombre = s.csNombre.TrimEnd() + " - (" + x.depNombre + ")"
                             }
                     ).ToList());*/

            var _CTROS = from a in context.catCentroDeSalud
                join b in context.catDepartamento on a.depId equals b.depId
                where a.cszId != null && a.cszId >= 1 && a.cszId <= 5 
                select new
                {
                    a.csId,
                    csNombre = a.csNombre.TrimEnd() + " (" + b.depNombre + ")"
                };


            System.Collections.Generic.List<GeDoc.catEncuestaAPSEncuestadores> _ENC;
            var user = (Session["Usuario"] as GeDoc.Models.sisUsuario);
            
            if (user.esExterno == true)
            {
                _ENC = (from s in context.catEncuestaAPSEncuestador.ToList()
                        where s.encuestadorEsExterno == true
                        orderby s.encuestadorApyNom ascending
                        select new catEncuestaAPSEncuestadores()
                        {
                            encuestadorId = s.encuestadorId,
                            encuestadorApyNom = s.encuestadorApyNom
                        }).ToList();
            }
            else
            {
                _ENC = (from s in context.catEncuestaAPSEncuestador.ToList()
                        where (s.encuestadorEsExterno == null || s.encuestadorEsExterno == false)
                        orderby s.encuestadorApyNom ascending
                        select new catEncuestaAPSEncuestadores()
                        {
                            encuestadorId = s.encuestadorId,
                            encuestadorApyNom = s.encuestadorApyNom
                        }).ToList();
            }

            




         

            ViewData["depEncId_Data"] = new SelectList(_DN, "depId", "depNombre");
            ViewData["locEncId_Data"] = ReorderList(_LO, "locId", "locNombre");  
            ViewData["barEncId_Data"] = ReorderList(_BN, "barId", "barNombre");
            ViewData["csEncId_Data"] = new SelectList(_CTROS.OrderByDescending(o => o.csId == _csId).ThenBy(t => t.csNombre), "csId", "csNombre");
            ViewData["encuestadorId_Data"] = new SelectList(_ENC, "encuestadorId", "encuestadorApyNom");

            ViewData["depEncId_eventOnChange"] = "depEncIdOnChange";

            //ViewData["depEncId_SelectedIndex"] = _DN.FindIndex(c => c.depId == 0);
            //ViewData["locEncId_SelectedIndex"] = _LO.FindIndex(c => c.locId == 0); 
            //ViewData["barId_SelectedIndex"] = _BN.FindIndex(c => c.barId == 0); 

        }
        //Datos para el dropdown
        protected void PopulateDropDownListPaciente()
        {
            dcAccesoCompadDataContext _Centros = new dcAccesoCompadDataContext();
            var _csId = (Session["UsuarioCentroDeSalud"] as sisUsuariosCentroDeSalud).csId;
            var _dpto = (from s in context.catCentroDeSalud.ToList()
                        // where s.csId == _csId
                         select new catCentroDeSalud()
                         {
                             depId = s.depId,

                         }).First();
            var _TipoDocumento = (from s in context.sisTipo.ToList()
                                  where s.sisTipoEntidad.tipoeCodigo == "TD"
                                  select new sisTipo()
                                  {
                                      tipoId = s.tipoId,
                                      tipoDescripcion = s.tipoDescripcion
                                  }).ToList();

            var _Sexo = (from s in context.sisTipo.ToList()
                         where s.sisTipoEntidad.tipoeCodigo == "SX"
                         select new sisTipo()
                         {
                             tipoId = s.tipoId,
                             tipoDescripcion = s.tipoDescripcion
                         }).ToList();
            var _OS = (from s in context.catObraSocial.ToList()
                       where s.osCodigo == 0
                       select new catObraSocial()
                       {
                           osId = s.osId,
                           osDenominacion =  s.osSigla + " - " + s.osDenominacion
                       }).ToList();
            _OS.AddRange((from s in context.catObraSocial.ToList()
                          where s.osCodigo != 0
                          orderby s.osDenominacion
                          select new catObraSocial()
                          {
                              osId = s.osId,
                              osDenominacion = s.osSigla + " - " + s.osDenominacion
                          }).ToList());
            var _EsCi = (from s in context.sisTipo.ToList()
                         where s.sisTipoEntidad.tipoeCodigo == "EC"
                         select new sisTipo()
                         {
                             tipoId = s.tipoId,
                             tipoDescripcion = s.tipoDescripcion
                         }).ToList();
            var _GS = (from s in context.sisTipo.ToList()
                       where s.sisTipoEntidad.tipoeCodigo == "GS"
                       select new sisTipo()
                       {
                           tipoId = s.tipoId,
                           tipoDescripcion = s.tipoDescripcion
                       }).ToList();

            var _OC = (from s in context.sisTipo.ToList()
                       where s.sisTipoEntidad.tipoeCodigo == "OC"
                       select new sisTipo()
                       {
                           tipoId = s.tipoId,
                           tipoDescripcion = s.tipoDescripcion
                       }).ToList();
            var _DN = (from s in context.catDepartamento.ToList()
                      // where s.provId == 17 && s.depId == _dpto.depId
                      where s.depId != 0
                       orderby s.depNombre ascending
                       select new catDepartamento()
                       {
                           depId = s.depId,
                           depNombre = s.depNombre
                       }).ToList();

            //_DN.AddRange((from s in context.catDepartamento.ToList()
            //              where s.provId == 17 && s.depId != _dpto.depId
            //              orderby s.depNombre ascending
            //              select new catDepartamento()
            //              {
            //                  depId = s.depId,
            //                  depNombre = s.depNombre
            //              }).ToList());

            var _LN = (from s in context.catLocalidad.ToList()
                      // where s.depId == _dpto.depId
                       orderby s.locId descending
                       select new catLocalidad()
                       {
                           locId = s.locId,
                           locNombre = s.locNombre
                       }).ToList();
            var _BN = (from s in context.catBarrio.ToList()
                      // where s.depId == _dpto.depId
                       orderby s.barId descending
                       select new catBarrio()
                       {
                           barId = s.barId,
                           barNombre = s.barNombre
                       }).ToList();
            var _PP = (from s in context.sisTipo.ToList()
                       where s.sisTipoEntidad.tipoeCodigo == "PP"
                       select new sisTipo()
                       {
                           tipoId = s.tipoId,
                           tipoDescripcion = s.tipoDescripcion
                       }).ToList();
            var _TB = (from s in context.sisTipo.ToList()
                       where s.sisTipoEntidad.tipoeCodigo == "TB"
                       select new sisTipo()
                       {
                           tipoId = s.tipoId,
                           tipoDescripcion = s.tipoDescripcion
                       }).ToList();

            //var _CS = (from s in context.catCentroDeSalud.ToList()
            //           where s.csId == _csId
            //           select new catCentroDeSalud()
            //           {
            //               csId = s.csId,
            //               csNombre = s.csNombre
            //           }
             
            //            ).ToList();
            //_CS.AddRange((from s in context.catCentroDeSalud.ToList()
            //              where s.csId != _csId
            //              && s.depId == _dpto.depId
            //              select new catCentroDeSalud()
            //              {
            //                  csId = s.csId,
            //                  csNombre = s.csNombre
            //              }
            //          ).ToList());

            /*var _CSA = (from s in context.catCentroDeSalud.ToList()
                       where s.csId == _csId
                       select new catCentroDeSalud()
                       {
                           csId = s.csId,
                           csNombre = s.csNombre
                       }
             
                        ).ToList();

            _CSA.AddRange((from s in context.catCentroDeSalud.ToList()
                          where s.csId != _csId
                          select new catCentroDeSalud()
                          {
                              csId = s.csId,
                              csNombre = s.csNombre
                          }
                                 ).ToList());*/

            var _CSA = from a in context.catCentroDeSalud
                select new
                {
                    a.csId,
                    a.csNombre
                };

            var _ET = (from s in context.catEtnia.ToList()
                       where s.etnNombreComunidad == s.etnPueblo
                       orderby s.etnId
                       select new catEtnia()
                       {
                           etnId = s.etnId,
                           etnNombreComunidad = s.etnNombreComunidad,
                           etnPueblo = s.etnPueblo
                       }
                      ).ToList();
            _ET.AddRange(from s in context.catEtnia.ToList()
                         where s.etnNombreComunidad != s.etnPueblo
                         orderby s.etnPueblo, s.etnNombreComunidad
                         select new catEtnia()
                         {
                             etnId = s.etnId,
                             etnPueblo = s.etnPueblo,
                             etnNombreComunidad = s.etnNombreComunidad
                         });
            var _PR = (from s in context.catProvincia.ToList()
                       //where s.provId == 17
                       orderby s.provNombre ascending
                       select new catProvincia()
                       {
                           provId = s.provId,
                           provNombre = s.provNombre
                       }
           ).ToList();
          //  _PR.AddRange((from s in context.catProvincia.ToList()
          //                //where s.provId != 17
          //                orderby s.provNombre ascending
          //                select new catProvincia()
          //                {
          //                    provId = s.provId,
          //                    provNombre = s.provNombre
          //                }
          //).ToList());

            //var _EN = (from d in context.catEncuestaAPSEnfermedad.ToList()
            //           orderby d.enfDescripcion ascending
            //           select new catEncuestaAPSEnfermedades()
            //           {
            //               enfId = d.enfId,
            //               enfCodigo = d.enfCodigo,
            //               enfDescripcion = d.enfDescripcion
            //           }).ToList();

            //ViewData["enfId_Data"] = new SelectList(_EN, "enfId", "enfDescripcion");
            ViewData["tipoIdTipoDocumento_Data"] = new SelectList(_TipoDocumento, "tipoId", "tipoDescripcion");
            ViewData["tipoIdSexo_Data"] = new SelectList(_Sexo, "tipoId", "tipoDescripcion");
            ViewData["paisId_Data"] = ReorderList(context.catPais.ToList(), "paisId", "paisNombre");
            
            ViewData["osId_Data"] = new SelectList(_OS, "osId", "osDenominacion");
            ViewData["tipoIdEstadoCivil_Data"] = new SelectList(_EsCi, "tipoId", "tipoDescripcion");
            ViewData["tipoIdGrupoSanguineo_Data"] = new SelectList(_GS, "tipoId", "tipoDescripcion");
            ViewData["tipoIdOcupacion_Data"] = new SelectList(_OC, "tipoId", "tipoDescripcion");
            ViewData["depId_Data"] = new SelectList(_DN, "depId", "depNombre");
            
            ViewData["locId_Data"] = ReorderList(_LN, "locId", "locNombre");
            
            ViewData["barId_Data"] = ReorderList(_BN, "barId", "barNombre");
            
            ViewData["csaId_Data"] = new SelectList(_CSA, "csId", "csNombre");
            ViewData["etnId_Data"] = new SelectList((from t in _ET 
                                                     select new
                                                     {
                                                         Valor = t.etnId,
                                                         Texto = t.etnNombreComunidad == t.etnPueblo ? t.etnNombreComunidad : t.etnPueblo + " - " + t.etnNombreComunidad
                                                     }), "Valor", "Texto");
            ViewData["provId_Data"] = ReorderList(_PR, "provId", "provNombre");
        }

        private SelectList ReorderList<T>(List<T> listObject,string valueField, string textField)
        {
            var unknownItem = listObject.Single<T>(a => int.Parse(a.GetType().GetProperty(valueField).GetValue(a, null).ToString()) == 0);
            listObject = listObject.Where(a => int.Parse(a.GetType().GetProperty(valueField).GetValue(a, null).ToString()) != 0)
                                   .OrderBy(a => a.GetType().GetProperty(textField).GetValue(a, null)).ToList();
            listObject.Insert(0,unknownItem);
            return new SelectList(listObject, valueField, textField);
        }
     
        //devuelve el nombre de la imagen a mostrar, dependiendo del parametro
        private string getHospitalizado(bool _hospitalizado)
        {

            switch (_hospitalizado)
            {
                case true:
                    return "Rojo.png";
                case false:
                    return "Gris.png";

            }


            return _hospitalizado == true ? "Rojo.png" : (_hospitalizado == false ? "Gris.png" : "collapse");
        }



        protected List<catLocalidades> getLocalidades(int depId)
        {
            var _Localidades = (from s in context.catLocalidad.ToList()
                                join x in context.catDepartamento on s.depId equals x.depId
                                where x.depId == depId || s.locId == 0
                                select new catLocalidades()
                                {
                                    locId = s.locId,
                                    locNombre = s.locNombre
                                }).ToList().OrderBy(o => o.locNombre);
            return _Localidades.ToList();

        }
        protected List<catBarrio> getBarrios(int depId)
        {
            var _barrios = (from s in context.catBarrio.ToList()
                            join x in context.catDepartamento on s.depId equals x.depId
                            where x.depId == depId || s.barId == 0
                            select new catBarrio()
                            {
                                barId = s.barId,
                                barNombre = s.barNombre
                            }).ToList().OrderBy(o => o.barNombre);
            return _barrios.ToList();

        }


       
        [GridAction]     
        public ActionResult EliminaOptionCombo(int Value)
        {
            var _EN = (from d in context.catEncuestaAPSEnfermedad.ToList()
                       orderby d.enfDescripcion ascending
                       where d.enfId != Value
                       select new catEncuestaAPSEnfermedades()
                       {
                           enfId = d.enfId,
                           enfCodigo = d.enfCodigo,
                           enfDescripcion = d.enfDescripcion
                       }).ToList();
            ViewData["enfId_Data"] = new SelectList(_EN, "enfId", "enfDescripcion");
            return Json(true);
        }
    }
}
