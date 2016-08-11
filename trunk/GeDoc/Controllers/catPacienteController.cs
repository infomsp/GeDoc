using System;
using System.Collections.Generic;
using System.Json;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using GeDoc.Filters;
using GeDoc.Models;
using Telerik.Web.Mvc;

namespace GeDoc.Controllers
{
    //using System.Net.Http.HttpResponseMessage;


    public partial class catPacienteController : Controller
    {       
        private efAccesoADatosEntities context = new efAccesoADatosEntities();

       
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ViewDetails(int pacienteId)
        {
            var _MetodosUtiles = new MetodosUtiles();

            //Registra log de usuario
            new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catPaciente", "Examinar", "");
            var _csId = (Session["UsuarioCentroDeSalud"] as sisUsuariosCentroDeSalud).csId;
            catCentroDeSalud _csItem = (from d in context.catCentroDeSalud.Where(w => w.csId == _csId).ToList()
                        select new catCentroDeSalud()
                        {
                            csCodigo = d.csCodigo,
                            csNombre = d.csNombre,
                            csTipologia = d.csTipologia
                        }).FirstOrDefault();

            catPacientes _Item = (from d in context.getDatosDePaciente((short)_csId, pacienteId).ToList()
                                  select new catPacientes()
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
                                      pacFechaNacimientoTexto = d.pacFechaNacimiento.Value.ToString("dd/MM/yyyy"),
                                      pacEdad = d.pacFechaNacimiento == null ? (short)0 : (short)_MetodosUtiles.Edad(d.pacFechaNacimiento),
                                      DescEstadoCivil = d.tipoEstadoCivilNombre,
                                     // OcupacionDescripcion = d.tipoIdOcupacion,
                                      pacDonaOrganos = (bool)(d.pacDonaOrganos ?? false),                                           
                                      tipoIdGrupoSanguineo = d.tipoIdGrupoSanguineo,
                                      pacCalle = d.pacCalle == null ? "" : d.pacCalle.ToUpper(),
                                      pacCalleNumero = Convert.ToInt16(d.pacCalleNumero),
                                      pacDomicilioReferencias = d.pacDomicilioReferencias == null ? d.pacDomicilioReferencias : d.pacDomicilioReferencias.ToUpper(),
                                      depId = d.depId,
                                      depNombre = d.depNombre,
                                      locId = Convert.ToInt16(d.locId),
                                      locNombre = d.locNombre,
                                      barId = Convert.ToInt16(d.barId),
                                      barNombre = d.barNombre,
                                      pacTelefonoCasa = d.pacTelefonoCasa,
                                      pacTelefonoTrabajo = d.pacTelefonoTrabajo,
                                      pacTelefonoCelular = d.pacTelefonoCelular,
                                      pacNotificarXSMS = (bool)(d.pacNotificarXSMS ?? false),                                         
                                      osId = (short)d.osId,
                                      osNombre = d.osDenominacion,
                                      osSigla = d.osSigla,
                                      pacHospitalizado = d.pacHospitalizado ?? false,
                                      pacImagenHospitalizado = getHospitalizado(d.pacHospitalizado ?? false),
                                      pacTransfusionesDeSangre = d.pacTransfusionesDeSangre,
                                      pacTransfusionesDeSangreUltima = d.pacTransfusionesDeSangreUltima ?? DateTime.Now,
                                      pacMail = d.pacMail,
                                      csId = d.csId == null ? (short)0 : (short)d.csId,
                                      nroHC = getNroHc(d.pacId, _csId),
                                      csNombre =  _csItem.csNombre,                                      
                                  }).FirstOrDefault();

            return Json(new { InfoPaciente = _Item });
        }

        //Edición de datos
        public string getNroHc(long _pacId, long _csId)
        {
            var item = (from s in context.enlPacienteCtroSalud.ToList()
                         where s.csId == _csId && s.pacId == _pacId
                         select new enlPacientesCtroSalud()
                         {
                             nroHC = s.nroHC
                         }).FirstOrDefault();
            return item == null ? " " : item.nroHC;
        }
        public int getPcId(long _pacId, long _csId)
        {
            var _Item = (from s in context.enlPacienteCtroSalud.ToList()
                         where s.csId == _csId && s.pacId == _pacId
                         select new enlPacientesCtroSalud()
                         {
                             pcId = (short)s.pcId
                         }).FirstOrDefault();
            if (_Item == null)
            {
                return 0;
            }
            return _Item.pcId;
        }
        [HttpGet]
        public ActionResult _BindingPaciente(int id)
        {
            if (id > 0)
            {
                var _MetodosUtiles = new MetodosUtiles();

                //Registra log de usuario
                new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catPaciente", "Examinar", "");
                var _csId = (Session["UsuarioCentroDeSalud"] as sisUsuariosCentroDeSalud).csId;

                catPacientes _Datos = (from d in context.spGetListadoDePacientes2((short)_csId).ToList()
                                       where d.pacId == id
                                       select new catPacientes()
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
                                           tipoIdSexo = d.tipoIdSexo,
                                           pacFechaNacimiento = d.pacFechaNacimiento,
                                           tipoIdEstadoCivil = d.tipoIdEstadoCivil,
                                           tipoIdOcupacion = d.tipoIdOcupacion,
                                           //pacEdad = _MetodosUtiles.Edad(d.pacFechaNacimiento),
                                          // DescEstadoCivil = d.tipoEstadoCivilNombre,                                         
                                          // pacDonaOrganos = (bool)d.pacDonaOrganos == null ? false : (bool)d.pacDonaOrganos,
                                          // tipoIdGrupoSanguineo = (short)d.tipoIdGrupoSanguineo,
                                            pacCalle = d.pacCalle == null ? "" : d.pacCalle.ToUpper(),
                                            pacCalleNumero = d.pacCalleNumero,
                                            pacDomicilioReferencias = d.pacDomicilioReferencias == null ? d.pacDomicilioReferencias : d.pacDomicilioReferencias.ToUpper(),
                                            depId = d.depId,
                                            depNombre = d.depNombre,
                                            locId = Convert.ToInt16(d.locId),
                                            locNombre = d.locNombre,
                                            barId = Convert.ToInt16(d.barId),
                                            barNombre = d.barNombre,
                                            pacTelefonoCasa = d.pacTelefonoCasa,
                                            pacTelefonoTrabajo = d.pacTelefonoTrabajo,
                                            pacTelefonoCelular = d.pacTelefonoCelular,
                                            pacNotificarXSMS = (bool)(d.pacNotificarXSMS == null ? false : d.pacNotificarXSMS),                        
                                            osId = (short)d.osId,
                                            osNombre = d.osDenominacion,
                                            pacHospitalizado = d.pacHospitalizado == null ? false : (bool)d.pacHospitalizado,
                                           // pacImagenHospitalizado = getHospitalizado(d.pacHospitalizado == null ? false : (bool)d.pacHospitalizado),
                                            // pacTransfusionesDeSangre = d.pacTransfusionesDeSangre,
                                            //pacTransfusionesDeSangreUltima = d.pacTransfusionesDeSangreUltima == null ? DateTime.Now : (DateTime)d.pacTransfusionesDeSangreUltima,
                                            pacMail = d.pacMail,                                      
                                           csId = d.csId == null ? (short)0 : (short)d.csId,
                                          // nroHC = getNroHc(d.pacId, _csId),
                                          nroHC = d.nroHistClin,
                                       }).FirstOrDefault();

                return Json(_Datos, JsonRequestBehavior.AllowGet);
            }
            return View(new GridModel(All()));
        }
        public ActionResult getDiagnostico(int pacienteId)
        {
            var _MetodosUtiles = new MetodosUtiles();

            //Registra log de usuario
            new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catPaciente", "Examinar", "");


            catPacientes _Datos = (from d in context.vwPacienteDiagnostico.ToList()
                                   where d.pacId == (short)pacienteId
                                   select new catPacientes()
                                   {
                                       pacId = d.pacId,                                  
                                   }).FirstOrDefault();

            return Json(_Datos, JsonRequestBehavior.AllowGet);
        }

        public ActionResult getPaciente(int pacienteId)
        {
            var _MetodosUtiles = new MetodosUtiles();

            //Registra log de usuario
            new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catPaciente", "Examinar", "");
            var _csId = (Session["UsuarioCentroDeSalud"] as sisUsuariosCentroDeSalud).csId;

            catPacientes _Datos = (from d in context.spGetListadoDePacientes2((short)_csId).ToList()
                                  where d.pacId == (short)pacienteId
                                  select new catPacientes()
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
                                      pacFechaNacimientoTexto = d.pacFechaNacimiento.Value.ToString("dd/MM/yyyy"),
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
                                       pacHospitalizado = d.pacHospitalizado ?? false,
                                       pacImagenHospitalizado = getHospitalizado(d.pacHospitalizado ?? false),
                                       // pacTransfusionesDeSangre = d.pacTransfusionesDeSangre,
                                      /* pacTransfusionesDeSangreUltima = d.pacTransfusionesDeSangreUltima == null ? DateTime.Now : (DateTime)d.pacTransfusionesDeSangreUltima,*/
                                       pacMail = d.pacMail,
                                      csId = d.csId == null ? (short)0 : (short)d.csId,
                                     // nroHC = getNroHc(d.pacId, _csId),
                                      nroHC = d.nroHistClin,
                                  }).FirstOrDefault();

            return Json(_Datos, JsonRequestBehavior.AllowGet);
        }



       // [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _SelectEditing()
        {            
            return View(new GridModel(All()));
        }
       
        private IList<catPacientes> All()
        {
           return getDatos().ToList();
        }

         [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult getPadron(int doc, int tipoDoc)
        {
            catPaciente error = new catPaciente();
            var _csId = (Session["UsuarioCentroDeSalud"] as sisUsuariosCentroDeSalud).csId;
            var _Paciente = context.spGetBusquedaEnPacientes_PADRON(tipoDoc, doc).FirstOrDefault(s => s.pacNumeroDocumento == doc && s.tipoIdTipoDocumento == (short)tipoDoc && s.pacNumeroDocumento != 11111);
            if (_Paciente != null)
            {
                var _Msj = "El Paciente " + _Paciente.pacNumeroDocumento + " - " + _Paciente.pacNombre + " " + _Paciente.pacApellido + " ya existe en la tabla Pacientes.";
                return Json(_Msj);
               

            }
            else
            {
            if (doc != null)
            {
                var _Datos = (from d in context.spGetBusquedaPadron(tipoDoc-36,doc)
                             // where doc == d.ppNroDoc
                             // && (tipoDoc -36) == d.tdocId
                              select new conpadPadronDePersonas()
                              {
                                  ppApellido = d.ppApellido,
                                  ppNombre = d.ppNombre,
                                  ppCUIL = d.ppCUIL,
                                  ppDepartamento = d.ppDepartamento,
                                  ppDomBarrio_Loc = d.ppDomBarrio_Loc,
                                  locDepartamento = d.ppDepartamento,
                                  ppDomDepto = d.ppDomDepto,
                                  locId = d.locId,
                                  ppDomCalle = d.ppDomCalle,
                                  ppDomNroCalle = d.ppDomNroCalle,
                                  ppFechaNacimiento = d.ppFechaNacimiento ?? DateTime.Now,
                                  ppObraSocial = d.ppObraSocial,
                                  ppSexo = d.ppSexoId,
                                  ppTelefono1 = d.ppTelefono1,
                                  ppCP = d.ppCP,
                                  depId =(short)d.depId,
                                  ppId = d.ppId
                              });

                return Json(_Datos);

            }
          }
            return View(new GridModel(All()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult BindingLocalidades(int depId)
        {
            ViewData["locId_Data"] = ReorderList(getLocalidades(depId), "locId", "locNombre");        
            return Json((SelectList)ViewData["LocId_Data"]);
        }

        private List<catLocalidades> getLocalidades(int depId)
        {
            return (from a in context.catLocalidad
                    where a.depId == depId || a.locId == 0
                    select new catLocalidades()
                    {
                        locId = a.locId,
                        locNombre = a.locNombre,
                    }).ToList();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult BindingBarrios(int depId)
        {            
            ViewData["barId_Data"] = ReorderList(getBarrios(depId), "barId", "barNombre");
            return Json((SelectList)ViewData["barId_Data"]);
        }

        private List<catBarrios> getBarrios(int depId)
        {
            return (from a in context.catBarrio
                    where a.depId == depId || a.barId == 0
                    select new catBarrios()
                    {
                        barId = a.barId,
                        barNombre = a.barNombre,
                    }).ToList();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult BindingDepartamento(int provId)
        {
            ViewData["depId_Data"] = ReorderList(getDepartamentos(provId), "depId", "depNombre");
            return Json((SelectList)ViewData["depId_Data"]);
        }

        private List<catDepartamentos> getDepartamentos(int provId)
        {
            return (from a in context.catDepartamento
                where a.provId == provId || a.provId == 0
                select new catDepartamentos()
                {
                    depId = a.depId,
                    depNombre = a.depNombre,
                }).ToList();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _SaveEditing(int id)
        {

            if (Session["UsuarioCentroDeSalud"] == null)
            {
                RedirectToAction("LogOff", "Account");
                return null;
            }
            catPaciente _Item = context.catPaciente.FirstOrDefault(p => p.pacId == id);

            if (TryUpdateModel(_Item))
            {

                Edit(_Item);

               
            }
                if (Session["upArchivo"] != null)
                {                
                    Session["upArchivo"] = null;
                }

               return View(new GridModel(All()));
                
        }
        private void Edit(catPaciente pItem)
        {
            enlPacienteCtroSalud _ItemEnl = new enlPacienteCtroSalud();
            var _csId = ((sisUsuariosCentroDeSalud) Session["UsuarioCentroDeSalud"]).csId;
            if (ModelState.IsValid)
            {
                //Registra log de usuario
                new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catPaciente", "Modificar", "Modifica el Paciente "+ pItem.pacApellido + " " + pItem.pacNombre + " pacId " + pItem.pacId);

                try
                {
                  //  catPaciente _Item = context.catPaciente.Where(p => p.pacId == pItem.pacId).FirstOrDefault();
                    if (TryUpdateModel(pItem))
                    {
                        pItem.pacNombre = pItem.pacNombre.ToUpper();
                        if (pItem.tipoIdTipoDocumento == 10)
                        {
                            pItem.pacNumeroDocumento = 11111111;
                        }
                       // pItem.pacCalle = pItem.pacCalle.ToUpper();
                      //  pItem.pacDomicilioReferencias = pItem.pacDomicilioReferencias.ToUpper();
                        context.SaveChanges();
                        _ItemEnl.pacId = pItem.pacId;
                        _ItemEnl.csId = _csId;
                        _ItemEnl.nroHC = pItem.nroHC;
                        _ItemEnl.pcId = getPcId(pItem.pacId, _csId);
                        editEnlPCS(_ItemEnl);
                        
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("pacApellido", ex.Message);
                }
            }
            return;
        }
        private void EditConpadPadronPersonas(catConpadPadronPersonas pItem)
        {
            if (ModelState.IsValid)
            {
                //Registra log de usuario
                new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catPaciente", "Modificar", "");

                try
                {
                    //  catPaciente _Item = context.catPaciente.Where(p => p.pacId == pItem.pacId).FirstOrDefault();
                if (TryUpdateModel(pItem))
                    {

                        context.SaveChanges();
                    }
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
        public ActionResult _InsertEditing()
        {               
           catPaciente _Item = new catPaciente();         
            enlPacienteCtroSalud _ItemEnl = new enlPacienteCtroSalud();
            var _csId = (Session["UsuarioCentroDeSalud"] as sisUsuariosCentroDeSalud).csId;
           
           if (TryUpdateModel(_Item))
           {             
               catPaciente error = new catPaciente();

               var _Paciente = context.spGetListadoDePacientes2((short)_csId).FirstOrDefault(s => s.pacNumeroDocumento == _Item.pacNumeroDocumento && s.tipoIdTipoDocumento == (short)_Item.tipoIdTipoDocumento && s.pacNumeroDocumento != 11111);
               if (_Paciente != null && _Paciente.tipoIdTipoDocumento != 182)
               {
                   ModelState.AddModelError("pacNumeroDocumento", "El Paciente " + _Paciente.pacNombre + " " + _Paciente.pacApellido + " ya existe.");
                  
               }
               else
               {
                   _Item.pacApellido = _Item.pacApellido.ToUpper();
                   _Item.pacNombre = _Item.pacNombre.ToUpper();
                   if (_Item.tipoIdTipoDocumento == 182)
                   {
                       _Item.pacNumeroDocumento = 11111;
                   }                
                   Create(_Item);
                   _ItemEnl.pacId = _Item.pacId;
                   _ItemEnl.csId = _csId;
                   _ItemEnl.nroHC = _Item.nroHC;
                   _ItemEnl.pcId = getPcId(_Item.pacId, _csId);
                   CreateEnlPCS(_ItemEnl);
                   return View(new GridModel(All())); 
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
               return View(new GridModel(All()));               
              
           }
             return View(new GridModel(All())); 

        }
        private void Create(catPaciente pItem)
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
            else{
                //  return new HttpResponseMessage<JsonValue>(errores, HttpStatusCode.BadRequest);       
                  try
                 {
                    context.catPaciente.AddObject(pItem);     
                    //Registra log de usuario
                    new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catPaciente", "Agregar", "Agrega al paciente "+ pItem.pacApellido + " " + pItem.pacNombre + " pacDoc " + pItem.pacNumeroDocumento);
                    context.SaveChanges();
                }
                catch (Exception ex)
                 {
                     ModelState.AddModelError("pacApellido", ex.Message);
                 }
            }


            return;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _DeleteEditing(int id)
        {
            if (Session["UsuarioCentroDeSalud"] == null)
            {
                RedirectToAction("LogOff", "Account");
                return null;
            }
            //DeleteConfirmed(id, false);

            return View(new GridModel(All()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _ActivaRegistro(int id)
        {
            //DeleteConfirmed(id, true);

            return View(new GridModel(All()));
        }
        private IEnumerable<catPacientes> getDatos()
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
                var _Sector = (Session["UsuarioCentroDeSalud"] as sisUsuariosCentroDeSalud).csId;
                var _csId = (Session["UsuarioCentroDeSalud"] as sisUsuariosCentroDeSalud).csId;


                //   var _Datos = (from d in context.spGetListadoDePacientes2.ToList()
                              //where ZonaSanitaria == null ? true : (d.secId == 10 || d.secId == 

               var  _Datos = (from d in context.spGetListadoDePacientes2((short)_csId).ToList()
                              orderby d.pacId descending
                              select new catPacientes()
                                        {
                                            pacId = d.pacId,
                                            pacApellido = d.pacApellido.ToUpper(),
                                            pacNombre = d.pacNombre.ToUpper(),
                                            pacApellidoyNombre = d.pacApellido.ToUpper() + " " + d.pacNombre.ToUpper(),
                                            pacNumeroDocumento = d.pacNumeroDocumento,
                                            tipoIdTipoDocumento = d.tipoIdTipoDocumento,
                                            tipoDescDocumento = d.tipoDescDocumento,
                                            pacCUIL = d.pacCUIL,
                                            paisId = d.paisId,
                                            paisNombre = d.paisNombre,
                                            tipoIdSexo = d.tipoIdSexo,
                                            tipoSexoNombre = d.tipoIdSexoTexto,
                                            pacFechaNacimiento =d.pacFechaNacimiento,
                                            pacEdad = d.pacFechaNacimiento == null ? (short)0 :(short) _MetodosUtiles.Edad(d.pacFechaNacimiento),
                                            tipoIdEstadoCivil = d.tipoIdEstadoCivil,
                                            DescEstadoCivil = d.tipoEstadoCivilNombre,
                                            tipoIdOcupacion = d.tipoIdOcupacion,
                                            pacDonaOrganos =(bool)(d.pacDonaOrganos ?? false),
                                            tipoIdGrupoSanguineo = d.tipoIdGrupoSanguineo,
                                            pacCalle = d.pacCalle,
                                            pacCalleNumero = d.pacCalleNumero,
                                            pacDomicilioReferencias = d.pacDomicilioReferencias == null ? d.pacDomicilioReferencias : d.pacDomicilioReferencias.ToUpper(),
                                            depId = d.depId,
                                            depNombre = d.depNombre,
                                            locId = d.locId,
                                            locNombre = d.locNombre,
                                            barId =d.barId,
                                            barNombre = d.barNombre,
                                            pacTelefonoCasa = d.pacTelefonoCasa,
                                            pacTelefonoTrabajo = d.pacTelefonoTrabajo,
                                            pacTelefonoCelular = d.pacTelefonoCelular,
                                            pacNotificarXSMS = (bool)(d.pacNotificarXSMS ?? false),
                                            osId = (short)d.osId,
                                            osNombre = d.osDenominacion,
                                            pacHospitalizado = d.pacHospitalizado ?? false,
                                            pacImagenHospitalizado = getHospitalizado(d.pacHospitalizado ?? false),
                                            // pacTransfusionesDeSangre = d.pacTransfusionesDeSangre,
                                            //pacTransfusionesDeSangreUltima = d.pacTransfusionesDeSangreUltima == null ? DateTime.Now : (DateTime)d.pacTransfusionesDeSangreUltima,  
                                            // pacTransfusionesDeSangre = 0,
                                            // pacTransfusionesDeSangreUltima = d.pacTransfusionesDeSangreUltima,
                                            provId = (short)d.provId,
                                            provNombre = d.provNombre,
                                            pacMail = d.pacMail,
                                            etnId = (short)d.etnId,
                                            csId = d.csId == null ? (short)0 : (short)d.csId,
                                            //nroHC = getNroHc(d.pacId, _csId),
                                            nroHC = d.nroHistClin,
                                            pacPadron = d.pacPadron
                                        }).ToList();
                return _Datos.AsEnumerable();
            }
            catch (Exception ex)
            {
                // ignored
            }

            return null;           
         
        }
        public ActionResult Index()
        {
            ViewBag.Catalogo = "Pacientes";
            ViewData["FiltroContains"] = true;
            ViewData["Pacientes"] = new catPacientes();
           // ViewData["catPacienteHabitos"] = new catPacientesHabitos();                
          //  ViewData["antMedicos"] = new List<enlPacientesAntecedenteMedico>();       
            PopulateDropDownList();
            return PartialView();      
        }
        private void Edit(catAgenda pItem)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (pItem.perId < 0)
                    {
                        pItem.conId = (pItem.perId * -1);
                        pItem.perId = null;
                    }

                    //Registra log de usuario
                    new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catAgenda", "Modificar", "Modifica agenda");

                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("agDuracion", ex.Message);
                }
            }
            return;
        }
        
        private void DeleteConfirmed(int id, bool pEstado)
        {
            try
            {
                catAgenda _Item = context.catAgenda.Single(x => x.agId == id);
                _Item.agActivo = pEstado;
                //Registra log de usuario
                new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catAgenda", "Eliminar", "Elimina agenda");
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("agDuracion", ex.Message);
            }
        }
         //Devolver SelectedList para el Html.DropDownList
       
 
      
 
    
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
                           osDenominacion = "( " + s.osCodigo + ") ( "+ s.osSigla + " ) " + s.osDenominacion
                       }).ToList();
             _OS.AddRange((from s in context.catObraSocial.ToList()
                           where s.osCodigo != 0
                       orderby s.osDenominacion                          
                         select new catObraSocial()
                         {
                             osId = s.osId,
                             osDenominacion = "( " + s.osCodigo + ") ( " + s.osSigla + " ) " + s.osDenominacion
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
                       where s.provId == 17 && s.depId == _dpto.depId
                       orderby s.depNombre ascending
                       select new catDepartamento()
                       {
                           depId = s.depId,
                           depNombre = s.depNombre
                       }).ToList();

             _DN.AddRange ((from s in context.catDepartamento.ToList()
                            where (s.provId == 17 && s.depId != _dpto.depId) || s.depId == 0
                       orderby s.depNombre ascending
                       select new catDepartamento()
                       {                           
                           depId = s.depId,                           
                           depNombre = s.depNombre
                       }).ToList());

            var _LN = (from s in context.catLocalidad.ToList()
                       where s.depId == _dpto.depId || s.locId == 0
                       orderby s.locId descending
                       select new catLocalidad()
                       {
                           locId = s.locId,
                           locNombre = s.locNombre
                       }).ToList();
            var _BN = (from s in context.catBarrio.ToList()
                       where s.depId == _dpto.depId || s.barId == 0
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

            var _CS = (from s in context.catCentroDeSalud.ToList()
                       where s.csId == _csId
                       select new catCentroDeSalud()
                       {
                           csId = s.csId,
                           csNombre = s.csNombre
                       }
                        ).ToList();
              _CS.AddRange ((from s in context.catCentroDeSalud.ToList()      
                      where s.csId != _csId
                       select new catCentroDeSalud()
                       {
                           csId = s.csId,
                           csNombre = s.csNombre
                       }
                        ).ToList());

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
                       where s.provId == 17
                       orderby s.provNombre ascending
                       select new catProvincia()
                       {
                           provId = s.provId,
                           provNombre = s.provNombre
                       }
           ).ToList();
             _PR.AddRange ((from s in context.catProvincia.ToList()
                            where s.provId != 17
                      orderby s.provNombre ascending
                      select new catProvincia()
                      {
                          provId = s.provId,
                          provNombre = s.provNombre
                      }
           ).ToList());
            ViewData["tipoIdTipoDocumento_Data"] = new SelectList(_TipoDocumento, "tipoId", "tipoDescripcion");
            ViewData["tipoIdSexo_Data"] = new SelectList(_Sexo, "tipoId", "tipoDescripcion");
            ViewData["paisId_Data"] = ReorderList(context.catPais.ToList(), "paisId", "paisNombre");
            
            ViewData["osId_Data"] = new SelectList(_OS, "osId", "osDenominacion");
            ViewData["tipoIdEstadoCivil_Data"] = new SelectList(_EsCi, "tipoId", "tipoDescripcion");
            ViewData["tipoIdGrupoSanguineo_Data"] = new SelectList(_GS, "tipoId", "tipoDescripcion");
            ViewData["tipoIdOcupacion_Data"] = new SelectList(_OC, "tipoId", "tipoDescripcion");
            ViewData["depId_Data"] = ReorderList(_DN, "depId", "depNombre");
            
            ViewData["locId_Data"] = ReorderList(_LN, "locId", "locNombre");
            
            ViewData["barId_Data"] = ReorderList(_BN, "barId", "barNombre");
            
            ViewData["csId_Data"] = new SelectList(_CS, "csId", "csNombre");
            ViewData["etnId_Data"] = new SelectList((from t in _ET
                                                     select new
                                                     {
                                                         Valor = t.etnId,
                                                         Texto = t.etnNombreComunidad == t.etnPueblo ? t.etnNombreComunidad : t.etnPueblo + " - " + t.etnNombreComunidad
                                                     }), "Valor", "Texto");
            ViewData["provId_Data"] = ReorderList(_PR, "provId", "provNombre");

            ViewData["provId_eventOnChange"] = "provIdOnChange";
            ViewData["depId_eventOnChange"] = "depIdOnChange";
            ViewData["provId_SelectedIndex"] = (ViewData["provId_Data"] as SelectList).ToList().FindIndex(p => p.Text.ToLower() == "san juan");
            ViewData["depId_SelectedIndex"] = (ViewData["depId_Data"] as SelectList).ToList().FindIndex(p => p.Value.ToLower() == _dpto.depId.ToString());
        }

        /// <summary>
        /// Funcion para ubicar el elemento con id '0' al principio de las listas destinadas a ComboBox y no perder el orden alfabetico.
        /// </summary>
        /// <typeparam name="T">Tipo de los objetos en la lista.</typeparam>
        /// <param name="listObject">Lista a ordenar.</param>
        /// <param name="valueField">String con el nombre del campo que contienen los valores.</param>
        /// <param name="textField">String con el nombre del campo que contiene el texto a mostrar.</param>
        /// <returns></returns>
        private SelectList ReorderList<T>(List<T> listObject, string valueField, string textField)
        {
            var zeroIdItem = listObject.Single<T>(a => int.Parse(a.GetType().GetProperty(valueField).GetValue(a, null).ToString()) == 0);
            listObject = listObject.Where(a => int.Parse(a.GetType().GetProperty(valueField).GetValue(a, null).ToString()) != 0)
                                   .OrderBy(a => a.GetType().GetProperty(textField).GetValue(a, null)).ToList();
            listObject.Insert(0, zeroIdItem);
            return new SelectList(listObject, valueField, textField);
        }

        //devuelve el nombre de la imagen a mostrar, dependiendo del parametro
        private string getHospitalizado(bool _hospitalizado)
        {       
            return _hospitalizado ? "Rojo.png" : "Gris.png";
        }

        public ActionResult Paciente(int pacId = -1, int pacPadron = -1)
        {
            var _csId = (Session["UsuarioCentroDeSalud"] != null ? (Session["UsuarioCentroDeSalud"] as sisUsuariosCentroDeSalud).csId : -1);
            catPaciente paciente;
            if (pacPadron != -1)
            {   
                var pacTemp = context.spGetBusquedaPadron(-7,pacPadron).Single();
                paciente = new catPaciente()
                {
                    pacPadron = pacTemp.ppId,
                    pacCUIL = pacTemp.ppCUIL,
                    pacApellido = pacTemp.ppApellido,
                    pacNombre = pacTemp.ppNombre,
                    pacNumeroDocumento = pacTemp.ppNroDoc,
                    tipoIdTipoDocumento = (short) (pacTemp.tdocId + 36),
                    tipoIdSexo = (short) (pacTemp.ppSexoId == "M" ? 10 : 9),
                    pacFechaNacimiento = pacTemp.ppFechaNacimiento,
                    pacCalle = pacTemp.ppDomCalle,
                    pacCalleNumero = String.IsNullOrEmpty(pacTemp.ppDomNroCalle) || !Regex.IsMatch(pacTemp.ppDomNroCalle, @"^\d+$") ? (short)0 : short.Parse(pacTemp.ppDomNroCalle),
                    pacPiso = pacTemp.ppDomPiso,
                    pacDepto = pacTemp.ppDomDepto,
                    depId = pacTemp.depId,
                    pacTelefonoCasa = pacTemp.ppTelefono1,
                    pacTelefonoCelular = pacTemp.ppTelefono2,
                    paisId = 1, // POR DEFECTO
                    provId = 17, // POR DEFECTO
                    csId = pacTemp.csId
                };
            }
            else
            {
                paciente = context.catPaciente.SingleOrDefault(s => s.pacId == pacId) ?? new catPaciente();
            }
             

            ViewData["select_tipoIdSexo"] = new SelectList(context.sisTipo.Where(w => w.tipoeId == 4),"tipoId","tipoDescripcion", paciente.tipoIdSexo);
            ViewData["select_tipoIdTipoDocumento"] = new SelectList(context.sisTipo.Where(w => w.tipoeId == 10), "tipoId", "tipoDescripcion",paciente.tipoIdTipoDocumento);
            ViewData["select_tipoIdEstadoCivil"] = new SelectList(context.sisTipo.Where(w => w.tipoeId == 8), "tipoId", "tipoDescripcion",paciente.tipoIdEstadoCivil);
            ViewData["select_tipoIdOcupacion"] = new SelectList(context.sisTipo.Where(w => w.tipoeId == 16), "tipoId", "tipoDescripcion",paciente.tipoIdOcupacion);
            ViewData["select_paisId"] = new SelectList(context.catPais.OrderByDescending(o => o.paisNombre == "Argentina").ThenBy(t => t.paisId != 0).ThenBy(t => t.paisNombre), "paisId", "paisNombre",paciente.paisId);
            ViewData["select_osId"] = new SelectList(context.catObraSocial.Select(s => new
            {
                s.osId,
                osDenominacion = string.IsNullOrEmpty(s.osSigla) || s.osId == 305 ? s.osDenominacion : s.osSigla + " - " + s.osDenominacion
            }).OrderByDescending(o => o.osId == 305).ThenBy(t => t.osDenominacion), "osId", "osDenominacion",paciente.osId);
            ViewData["select_provId"] = new SelectList(context.catProvincia.OrderByDescending(o => o.provNombre == "San Juan").ThenBy(t => t.provId != 0).ThenBy(t => t.provNombre), "provId", "provNombre",paciente.provId);
            ViewData["select_tipoIdGrupoSanguineo"] = new SelectList(context.sisTipo.Where(w => w.tipoeId == 12).OrderByDescending(o => o.tipoId == 188), "tipoId", "tipoDescripcion",paciente.tipoIdGrupoSanguineo);           
            ViewData["select_etnId"] = new SelectList(context.catEtnia.Select(s => new
            {
                s.etnId, 
                tipoDescripcion = s.etnId < 2 ? s.etnPueblo : s.etnPueblo +" - "+s.etnNombreComunidad
            }).OrderByDescending(o => o.etnId < 2).ThenBy(t => t.tipoDescripcion), "etnId", "tipoDescripcion",paciente.etnId);
            var enlPacienteCtroSalud = context.enlPacienteCtroSalud.SingleOrDefault(s => s.pacId == paciente.pacId && s.csId == _csId);
            paciente.nroHC = enlPacienteCtroSalud != null ? enlPacienteCtroSalud.nroHC : "";

            return PartialView(paciente);
        }

        public ActionResult GuardarPaciente(int? pacId = -1)
        {
            var _csId = (Session["UsuarioCentroDeSalud"] != null ? (Session["UsuarioCentroDeSalud"] as sisUsuariosCentroDeSalud).csId : -1);
            var paciente = context.catPaciente.SingleOrDefault(s => s.pacId == pacId) ?? new catPaciente();
            pacId = pacId == -1 ? null : pacId;
            TryUpdateModel(paciente);
            paciente.pacNombre = paciente.pacNombre.ToUpper();
            paciente.pacApellido = paciente.pacApellido.ToUpper();
            paciente.csId = _csId;
            paciente.csaId = paciente.csId;
            paciente.pacNotificarXSMS = paciente.pacNotificarXSMS == null ? false : paciente.pacNotificarXSMS;
            paciente.paisId = paciente.paisId == null || paciente.paisId == -1 ? 0 : paciente.paisId;
            paciente.provId = paciente.provId == null || paciente.provId == -1 ? 0 : paciente.provId;
            paciente.depId = paciente.depId == null || paciente.depId == -1 ? 0 : paciente.depId;
            paciente.locId = paciente.locId == null || paciente.locId == -1 ? 0 : paciente.locId;
            paciente.barId = paciente.barId == null || paciente.barId == -1 ? 0 : paciente.barId;
            paciente.osId = Convert.ToInt16(paciente.osId == null || paciente.osId == 0 || paciente.osId == -1 ? 305 : paciente.osId);
            paciente.etnId = paciente.etnId == null || paciente.etnId == -1 ? 0 : paciente.etnId;
            paciente.tipoIdEstadoCivil = paciente.tipoIdEstadoCivil == null ? 29 : paciente.tipoIdEstadoCivil;
            if (pacId == 0)
                context.catPaciente.AddObject(paciente);
            var updates = context.SaveChanges();
            var enlPacCs = context.enlPacienteCtroSalud.SingleOrDefault(s => s.csId == _csId && s.pacId == paciente.pacId);
            if (enlPacCs == null)
            {
                enlPacCs = new enlPacienteCtroSalud {pacId = paciente.pacId, nroHC = Request["nroHC"], csId = _csId};
                context.enlPacienteCtroSalud.AddObject(enlPacCs);
            }
            else
                enlPacCs.nroHC = Request["nroHC"];
            context.SaveChanges();
            return Json(updates);
        }

        public ActionResult _GetComboBoxcsId(int depId = 0, int provId = -1, int selected = -1)
        {
            var _csId = selected == -1 ? (Session["UsuarioCentroDeSalud"] != null ? (Session["UsuarioCentroDeSalud"] as sisUsuariosCentroDeSalud).csId : -1) : selected;
            IQueryable<catCentroDeSalud> data;
            if (depId == 0 && provId != 0)
            {
                var deps = context.catDepartamento.Where(w => w.provId == provId).Select(s => (short?) s.depId);
                data = context.catCentroDeSalud.Where(w => w.csPublico == 1 && w.cszId <= 5 && deps.Contains(w.depId)).OrderBy(o => o.csNombre);               
            }
            else
            {
                data = context.catCentroDeSalud.Where(w => w.csPublico == 1 && w.cszId <= 5 && w.depId == depId).OrderBy(o => o.csNombre);
            }
            var list = data.Any()
                ? new SelectList(data.OrderBy(o => o.csNombre), "csId", "csNombre",_csId)
                : new SelectList(
                    new List<SelectListItem>()
                    {
                        new SelectListItem() {Text = "No Aplica", Value = "-1", Selected = true}
                    }, "Value", "Text");          
            return Json(list);
        }

        public ActionResult _GetComboBoxdepId(int provId, short selected = -1)
        {
            var _csId = Session["UsuarioCentroDeSalud"] != null ? (Session["UsuarioCentroDeSalud"] as sisUsuariosCentroDeSalud).csId : -1;
            if (selected != -1 || _csId != -1)
            {
                var depId = selected != -1 ? selected : context.catCentroDeSalud.Single(s => s.csId == _csId).depId;
                return Json(new SelectList(context.catDepartamento.Where(w => w.provId == provId || w.provId == 0).OrderByDescending(o => o.depId == 0).ThenBy(t => t.depNombre), "depId", "depNombre",depId));
            }
            return Json(new SelectList(context.catDepartamento.Where(w => w.provId == provId || w.provId == 0).OrderByDescending(o => o.depId == 0).ThenBy(t => t.depNombre), "depId", "depNombre"));           
        }

        public ActionResult _GetComboBoxlocId(int depId,int selected = -1)
        {
            return Json(new SelectList(context.catLocalidad.Where(w => w.depId == depId || w.depId == 0).OrderByDescending(o => o.locId == 0).ThenBy(t => t.locNombre), "locId", "locNombre",selected));
        }

        public ActionResult _GetComboBoxbarId(int depId, int selected = -1)
        {
            return Json(new SelectList(context.catBarrio.Where(w => w.depId == depId || w.depId == 0).OrderByDescending(o => o.barId == 0).ThenBy(t => t.barNombre), "barId", "barNombre",selected));
        }
        
        public dynamic PacienteDuplicado(int dni)
        {
            var csId = Session["UsuarioCentroDeSalud"] != null ? ((sisUsuariosCentroDeSalud) Session["UsuarioCentroDeSalud"]).csId : -1;
            ViewBag.duplicados = from a in context.catPaciente
                                 from b in context.enlPacienteCtroSalud.Where(w => w.pacId == a.pacId && w.csId == csId).DefaultIfEmpty()
                                 where a.pacNumeroDocumento == dni
                                 select new catPacienteMin
                                 {
                                     pacId = a.pacId,
                                     pacApellido = a.pacApellido,
                                     pacNombre = a.pacNombre,
                                     pacFechaNacimiento = a.pacFechaNacimiento,
                                     nroHC = b.nroHC,
                                     pacApellidoYNombre = a.pacApellido + ", " + a.pacNombre
                                 };
            ViewBag.dni = dni;
            return PartialView();
        }

        public ActionResult ExistePaciente(int dni)
        {
            return Json(context.catPaciente.Any(a => a.pacNumeroDocumento == dni), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ExisteDocumentoEnPadron(int dni, int tipoDni)
        {
            return Json(context.spGetBusquedaPadron(tipoDni-36,dni).Any(), JsonRequestBehavior.AllowGet);
        }

        public dynamic DatosPadron(int dni,short tipoDni)
        {
            ViewBag.datosPadron = (from a in context.spGetBusquedaPadron(tipoDni-36,dni)
                                 select new catPacienteMin
                                 {                                   
                                     pacPadron = a.ppId,
                                     pacApellido = a.ppApellido,
                                     pacNombre = a.ppNombre,
                                     pacFechaNacimiento = a.ppFechaNacimiento,
                                     pacApellidoYNombre = a.ppApellido + ", " + a.ppNombre
                                 }).ToList<catPacienteMin>();
            ViewBag.dni = dni;           
            return PartialView();
        }

    }
}