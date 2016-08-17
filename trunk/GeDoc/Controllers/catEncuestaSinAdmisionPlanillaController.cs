using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Telerik.Web.Mvc;
using Telerik.Web.Mvc.Extensions;
using GeDoc.Filters;
using GeDoc.Models;
using System.IO;
using System.Web.DynamicData;
using NPOI.HSSF.Record.Formula.Functions;

namespace GeDoc.Controllers
{

    
    public partial class catEncuestaSinAdmisionPlanillaController : Controller
    {
        private efAccesoADatosEntities context = new efAccesoADatosEntities();
        private int _getValue;

        [GridAction]
        public ActionResult _SelectEditing()
        {
            return View(new GridModel(All()));
        }

        private IEnumerable<catEncuestasSinAdmisionPlanilla> All()
        {
            return getDatos().ToList();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _SaveEditing(int id)
        {
            catEncuestaSinAdmisionPlanilla _Item = context.catEncuestaSinAdmisionPlanilla.Where(p => p.plaId == id).FirstOrDefault();
            var _csId = (Session["UsuarioCentroDeSalud"] as GeDoc.sisUsuariosCentroDeSalud).csId;
            TryUpdateModel(_Item);

            var _datos =
                   context.catEncuestaSinAdmisionPlanilla.FirstOrDefault(p => p.csId == _Item.csId & p.plaFechaPlanilla == _Item.plaFechaPlanilla & p.encuestadorId == _Item.encuestadorId & p.csId == _csId);
            if (_datos == null)
            {
                Edit(_Item);
            }
            //return Json(new
            //{
            //    Error = true,
            //    Mensaje = "Ya existe este paciente en la planilla Nro. ",
            //   // Foco = "pacNombre"
            //});
            return View(new GridModel(All()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _InsertEditing()
        {
            catEncuestaSinAdmisionPlanilla _Item = new catEncuestaSinAdmisionPlanilla();

            if (TryUpdateModel(_Item))
            {
                var _csId = (Session["UsuarioCentroDeSalud"] as GeDoc.sisUsuariosCentroDeSalud).csId;
                var _datos =
                   context.catEncuestaSinAdmisionPlanilla.FirstOrDefault(p => p.plaFechaPlanilla == _Item.plaFechaPlanilla & p.encuestadorId == _Item.encuestadorId & p.csId == _csId);
                if (_datos == null)
                {
                    _Item.csId = _csId;
                    Create(_Item);
                }
                //else
                //{

                //    return Json(new
                //    {
                //        Error = true,
                //        Mensaje = "Ya existe otra Planilla con igual fecha para el mismo encuestador",
                //        Foco = "pacNombre"
                //    });
                //}
            }

            return View(new GridModel(All()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _DeleteEditing(int id)
        {
            DeleteConfirmed(id);

            return View(new GridModel(All()));
        }
        private IEnumerable<catEncuestasSinAdmisionPlanilla> getDatos()
        {
            var _csId = (Session["UsuarioCentroDeSalud"] as GeDoc.sisUsuariosCentroDeSalud).csId;
            var _Datos = (from d in context.vwProturPlanillasCantPersonas.ToList()
                          where d.csId == _csId
                          orderby d.plaId descending
                          select new catEncuestasSinAdmisionPlanilla()
                          {
                              plaId = d.plaId,
                              csId = d.csId,
                              centroSaludTexto = context.catCentroDeSalud.First(f => f.csId == d.csId).csNombre,
                              plaFechaPlanilla = d.plaFechaPlanilla,
                              usrId = d.usrId,
                              usrNombre = context.sisUsuario.First(f => f.usrId == d.usrId).usrNombreDeUsuario,
                              encuestadorApyNom = context.catEncuestaSinAdmisionEncuestador.First(f => f.encuestadorId == d.encuestadorId).encuestadorApyNom,
                              encuestadorId = d.encuestadorId,
                              cantPacientes = d.cantPersonas,
                              resuelto = getResuelto(d.plaId),

                          }).ToList();

            return _Datos.AsEnumerable();
        }


        public int? getResuelto(int plaid) {
            var result = context.spGetPlanillaResueltos(plaid).ToList();
            int? resultint = result[0];
            return resultint;

            //var cantidad = 0;

            //var encuestados = (
            //    from a in context.catEncuestaSinAdmisionPersona
            //    where a.plaId == plaid
            //    select a.encId
            //                  ).ToList();


            //for (int i = 0; i < encuestados.Count(); i++) { 
                
            //    var especEncuestados = (
            //        from a in context.catEncuestaSinadmisionPersonaEspecialidad
            //        where a.encId == encuestados[i] 
            //        select a

            //        ).ToList();

            //}

            //    return 1;
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult jqGrid (int plaId,string fecha,string csNombre,string sidx, string sord, int page, int rows)
        {
            
            if (Session["UsuarioCentroDeSalud"] == null)
            {
                RedirectToAction("LogOff", "Account");
                return null;
            }
           
            var _NombreUsuario = (Session["Usuario"] as sisUsuario).usrNombreDeUsuario.ToString();

            var query = (from d in context.rptProturReportePlanilla(plaId).ToList()
                         select new rptProturReportePlanilla()
                         {
                             pacId = d.pacId.Value,
                             TELEFONO = d.TELEFONO.ToString(),
                             NOMBRE = d.NOMBRE,
                             APELLIDO = d.APELLIDO,
                             DNI = d.DNI.Value,
                             CAUSA = d.CAUSA,
                             CENTRO_DE_SALUD = d.CENTRO_DE_SALUD,
                             DEPARTAMENTO = d.DEPARTAMENTO,
                             DERIVADO = d.DERIVADO,
                             CUANDO = d.CUANDO,
                             DONDE = d.DONDE,
                             SEXO = d.SEXO,
                             ATENDIDO_LOCAL = d.ATENDIDO_LOCAL,
                             PROGRAMADO = d.PROGRAMADO,
                             INTERCONSULTA = d.INTERCONSULTA,
                             RESUELTO = d.RESUELTO,
                             ESPECIALIDAD = d.ESPECIALIDAD,
                             LOCALIDAD = d.LOCALIDAD,
                             EDAD = d.EDAD
                         }).ToList(); 

            var totalRecords = query.Count();

            var ret = Json(new {
                total = (totalRecords + rows - 1) / rows,
                page,
                _NombreUsuario,
                records = totalRecords,
                rows = (from item in query
                        select new {
                            id = item.pacId.Value,
                            cell = new string[] {
                                item.pacId.ToString(),
                                item.DNI.ToString(),
                                item.APELLIDO,
                                item.NOMBRE,
                                item.SEXO,
                                item.EDAD,
                                item.DEPARTAMENTO,
                                item.LOCALIDAD,
                                item.TELEFONO,
                                item.CENTRO_DE_SALUD,
                                item.CAUSA,
                                item.ESPECIALIDAD,
                                item.DERIVADO,
                                item.INTERCONSULTA,
                                item.ATENDIDO_LOCAL,
                                item.PROGRAMADO,
                                item.DONDE,
                                item.CUANDO,
                                item.RESUELTO
                            }
                        }).ToList()
            },
            JsonRequestBehavior.AllowGet);
            return ret;
        }
        

        public ActionResult ReporteProtur()
        {
            return PartialView();
        }

        public ActionResult Index()
        {
            ViewBag.Catalogo = "Encuesta personas sin admisionar";
            ViewData["FiltroContains"] = true;
            ViewData["Encuesta"] = new List<catEncuestasSinAdmisionPlanilla>();
            ViewData["EncuestaAPSPersonas"] = new List<catEncuestaSinadmisionPersonas>();
            ViewData["Pacientes"] = new List<catEncuestaSinAdmisionPacientes>();
            PopulateDropDownList();
            return PartialView();
        }

        //[AcceptVerbs(HttpVerbs.Post)]
        //[GridAction]


        private void Create(catEncuestaSinAdmisionPlanilla pItem)
        {
            if (ModelState.IsValid)
            {
                pItem.usrId = (Session["Usuario"] as sisUsuario).usrId;
                context.catEncuestaSinAdmisionPlanilla.AddObject(pItem);
                context.SaveChanges();
            }

            return;
        }

        private void Edit(catEncuestaSinAdmisionPlanilla pItem)
        {
            if (ModelState.IsValid)
            {
                //pItem.sisUsuario.usrId = (Session["Usuario"] as sisUsuario).usrId;
                context.SaveChanges();
            }
            return;
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ProcesaPlanilla(int plaId)
        {
            var _csId = (Session["UsuarioCentroDeSalud"] as GeDoc.sisUsuariosCentroDeSalud).csId;
            System.Data.Objects.ObjectResult<spPROTUR_procesaAtendidoLocal_Mensaje_Result> Datos;


            if (_csId != 8 && _csId != 14)
            {

                Datos = context.spPROTUR_procesaAtendidoLocal_Mensaje(plaId, 0);
            }
            else
            {

                Datos = context.spPROTUR_procesaAtendidoLocal_Mensaje(plaId, 1);
            }


            var x = Datos.ToList();

            var msj = "Pacientes procesados: " + x[0].cantPersonas.ToString() + "\nAtencion local: " + x[0].cantPersonasAtencionLocal.ToString() + "\nProgramado: " + x[0].cantPersonasProgramadas.ToString() + "\nSin programar: " + x[0].cantSinProgramar.ToString();

            return Json(new
            {
                Error = true,
                Mensaje = msj,
                Foco = "pacNombre"
            });

        }
        private void DeleteConfirmed(int id)
        {
            catEncuestaSinAdmisionPlanilla _Item = context.catEncuestaSinAdmisionPlanilla.Single(x => x.plaId == id);
            context.catEncuestaSinAdmisionPlanilla.DeleteObject(_Item);
            context.SaveChanges();
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult getValidaSiEstaAsignado(int _plaId)
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

        //YA ENCUESTADO : Busca un paciente encuestado
        // [AcceptVerbs(HttpVerbs.Post)]
        public string getValidaSiPacienteAsignado(DateTime? _fecha, long _pacId = 0, int _pacNumeroDocumento = 0, int _tipoDoc = 0, int plaId = 0)
        {
            var _csId = (Session["UsuarioCentroDeSalud"] as GeDoc.sisUsuariosCentroDeSalud).csId;
            try
            {
                if (_pacNumeroDocumento > 0 && _tipoDoc > 0)
                {
                    var _itemPac = context.catPaciente.FirstOrDefault(d => d.pacNumeroDocumento == _pacNumeroDocumento &&
                                                                           d.tipoIdTipoDocumento == (short)_tipoDoc && d.csId == _csId);
                    _pacId = _itemPac.pacId;
                }
                var _Datos = (from d in context.vwEncuestaSinAdmisionencuestados
                              where d.pacId == _pacId
                              & d.plaFechaPlanilla == _fecha
                              & d.csId == _csId
                              select new catEncuestaSinadmisionPersonas()
                              {
                                  pacId = d.pacId,
                                  encId = d.encId,
                                  ApellidoyNombre = d.pacApellido + "  " + d.pacNombre,
                                  // usrId = d.usrId,
                                  plaId = d.Expr3,
                                  // fechadeCarga = d.plaFechaPlanilla,
                                  Documento = d.pacNumeroDocumento,
                                  EncuestadorApellidoyNombre = d.encuestadorApyNom
                              }).ToList();


                if (_Datos.Any())
                {
                    string Mensaje;
                    if (plaId == _Datos.First().plaId)
                    {
                        Mensaje = _Datos.First().ApellidoyNombre +
                                        "\n \n se encuentra asignado a esta planilla.";
                        //  Foco = "pacNumeroDocumento";
                    }
                    else
                    {
                        Mensaje = _Datos.First().ApellidoyNombre +
                                  "\n \n se encuentra asignado a la planilla Nro. " + _Datos.First().plaId + " del encuestador " + _Datos.First().EncuestadorApellidoyNombre;
                        //    Foco = "pacNumeroDocumento";
                    }
                    return Mensaje;
                }
            }
            catch (Exception ex)
            {
                //return Json(new
                //{
                //    Error = false
                //});
                return "";
            }
            return "";
            //return Json(new
            //{
            //    Error = false
            //});
        }

        private string getNombreUsuario(int? _usrId)
        {

            var item = context.sisUsuario.Single(d => d.usrId == _usrId);
            return item.usrApellidoyNombre;

        }
        //Datos para el dropdown
        protected void PopulateDropDownList()
        {
            dcAccesoCompadDataContext _Centros = new dcAccesoCompadDataContext();
            var _csId = (Session["UsuarioCentroDeSalud"] as GeDoc.sisUsuariosCentroDeSalud).csId;

            var _CS = (from s in context.catCentroDeSalud.ToList()
                       where s.csId == _csId
                       select new catCentroDeSalud()
                       {
                           csId = s.csId,
                           csNombre = s.csNombre
                       }
                ).ToList();
            var _ENC = (from s in context.catEncuestaSinAdmisionEncuestador.ToList()
                        where s.csId == _csId
                        orderby s.encuestadorApyNom ascending
                        select new catEncuestaSinAdmisionEncuestadores()
                        {
                            encuestadorId = s.encuestadorId,
                            encuestadorApyNom = s.encuestadorApyNom
                        }
         ).ToList();
            var _CTROS = (from a in context.catCentroDeSalud.ToList()
                          //  join b in context.catDepartamento on a.depId equals b.depId
                          where a.csId == _csId
                          //  where a.cszId != null && a.cszId >= 1 && a.cszId <= 5
                          select new
                          {
                              a.csId,
                              csNombre = a.csNombre.TrimEnd()
                          }).ToList();
            var _Esp = (from s in context.catEspecialidad.ToList()
                        // where s.espIdPadre == null
                        orderby s.espNombre ascending
                        select new catEspecialidades
                        {
                            espId = s.espId,
                            espNombre = s.espCodigo + " - " + s.espNombre
                        }).ToList();
            var _Der = (from s in context.catEncuestaSinAdmisionDerivado.ToList()
                        // where s.espIdPadre == null
                        orderby s.Descripcion ascending
                        select new catEncuestaSinAdmisionDerivado
                        {
                            derId = s.derId,
                            Descripcion = s.Descripcion
                        }).ToList();
            
            //Atendido local
            var _AL = (from s in context.sisTipo.ToList()
                       where s.sisTipoEntidad.tipoeCodigo == "AL" //& s.tipoeId == 42
                       orderby s.tipoId descending
                       select new sisTipos()
                       {
                           tipoId = s.tipoId,
                           tipoDescripcion = s.tipoDescripcion
                       });

            //Programado en
            var _PEN = (from p in context.catCentroDeSalud.ToList()
                        where p.csId == _csId
                        select new catCentroDeSalud
                        {
                            csId = p.csId,
                            csNombre = p.csNombre
                        }).ToList();
            _PEN.AddRange((from a in context.catCentroDeSalud.ToList()
                           where a.csPublico == 1
                           select new catCentroDeSalud
                       {
                           csId = a.csId,
                           csNombre = a.csNombre
                       }).ToList());

            //Interconsulta
            var _IC = (from s in context.sisTipo.ToList()
                       where s.sisTipoEntidad.tipoeCodigo == "IC" //& s.tipoeId == 42
                       orderby s.tipoId ascending
                       select new sisTipos()
                       {
                           tipoId = s.tipoId,
                           tipoDescripcion = s.tipoDescripcion
                       });

            //Derivacion
            var _DI = (from s in context.sisTipo.ToList()
                       where s.sisTipoEntidad.tipoeCodigo == "DI" //& s.tipoeId == 42
                       orderby s.tipoId ascending
                       select new sisTipos()
                       {
                           tipoId = s.tipoId,
                           tipoDescripcion = s.tipoDescripcion
                       });

            ViewData["atendidoLocal_Data"] = new SelectList(_AL, "tipoId", "tipoDescripcion");
            ViewData["programadoEn_Data"] = new SelectList(_PEN, "csId", "csNombre");
            ViewData["espId_Data"] = new SelectList(_Esp, "espId", "espNombre");
            ViewData["programado_Data"] = new SelectList(_Der, "derId", "Descripcion");
            ViewData["encuestadorId_Data"] = new SelectList(_ENC, "encuestadorId", "encuestadorApyNom");
            ViewData["csId_Data"] = new SelectList(_CTROS, "csId", "csNombre");
            ViewData["interconsulta_Data"] = new SelectList(_IC, "tipoId", "tipoDescripcion");
            ViewData["derivado_Data"] = new SelectList(_DI, "tipoId", "tipoDescripcion");
        }


        [GridAction]
        public ActionResult TabPaciente(string Accion = "", int pacId = 0, int encId = 0)
        {
            var _Datos = new catEncuestaSinAdmisionPacientes();
            ViewData["FiltroContains"] = true;
            _Datos.pacAccion = Accion;
            PopulateDropDownListPaciente();
            if (encId > 0)
            {
                //Cargamos los datos de la encuesta
                var _DatosActuales_persona = context.catEncuestaSinAdmisionPersona.Single(w => w.encId == encId);
                var DatosActuales_planilla = context.catEncuestaSinAdmisionPlanilla.Single(w => w.plaId == _DatosActuales_persona.plaId);
                int plaId = DatosActuales_planilla.plaId;
                var centroSalud = context.catCentroDeSalud.Single(w => w.csId == DatosActuales_planilla.csId);
                Session["Encuesta_csId"] = DatosActuales_planilla.csId;
                if (Accion == "Agregar")
                {
                    _Datos.provId = 17;
                    _Datos.depId = 0;
                    _Datos.locId = 0;
                    _Datos.barId = 0;
                    _Datos.csId = (short)DatosActuales_planilla.csId;
                    _Datos.pacCalle = null;
                    _Datos.pacCalleNumero = null;
                    _Datos.pacDepto = null;
                    _Datos.pacManzana = null;
                    _Datos.pacPiso = null;
                    _Datos.pacTelefonoCasa = null;
                    _Datos.pacTelefonoCelular = null;
                    _Datos.csNombre = centroSalud.csNombre;
                    _Datos.csaId = (short)centroSalud.csId;
                    _Datos.plaId = DatosActuales_planilla.plaId;
                }
                if (Accion == "Modificar")
                {
                    // PopulateDropDownListPaciente();
                    if (pacId > 0)
                    {
                        var _encuestado = (from d in context.catEncuestaSinAdmisionPersona
                                           where d.plaId == plaId && d.pacId == pacId
                                           select new catEncuestaSinadmisionPersonas()
                                           {
                                               encId = d.encId
                                           }).FirstOrDefault();
                        Session["encId"] = _encuestado.encId;
                        var _Paciente = (from d in context.spGetBusquedaEnPacientes_PADRON(null, pacId)
                                         select new catEncuestaSinAdmisionPacientes()
                                         {
                                             depId = d.depId,
                                             locId = d.locId,
                                             barId = d.barId,
                                             provId = (short)d.provId,
                                             csId = (short)d.csId,
                                             encId = encId,
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
                                             // tipoIdEstadoCivil = d.tipoIdEstadoCivil,
                                             pacCUIL = d.pacCUIL,
                                             plaId = plaId,
                                             paisId = d.paisId,
                                             // espId = (short)_DatosActuales_persona.espId,
                                             encCausa = (short)_DatosActuales_persona.encCausa,
                                             pacFechaNacimiento = d.pacFechaNacimiento ?? DateTime.Now.Date,
                                             // pacFechaNacimientoTexto = d.pacFechaNacimiento == null ? DateTime.Now.Date.ToString("dd/MM/yyyy") : d.pacFechaNacimiento.Value.ToString("dd/MM/yyyy"),
                                             pacTelefonoTrabajo = d.pacTelefonoTrabajo,
                                             csNombre = getCentroSaludNombre((short)d.csId),
                                             //pacNotificarXSMS = (bool)(d.pacNotificarXSMS == null ? false : d.pacNotificarXSMS),
                                             osId = d.osId,
                                             pacMail = d.pacMail,
                                             etnId = (short)d.etnId,
                                             // PacienteEnfermedad = getDatosenlEncuestasAPSPersonasEnf((short)_encuestado.encId).ToList(),
                                             pacAccion = "Modificar"
                                         }).FirstOrDefault();
                        _Datos = _Paciente;
                    }
                }
            }
            //Session["PacienteEnfermedades"] = _Datos.PacienteEnfermedad;
            return PartialView("CRUDencuestaPaciente", _Datos);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult setDatosPaciente(catEncuestaSinAdmisionPacientes Datos)
        {
            var _tipoAgregar = 0;

            var encId = 0;
            var _csId = (Session["UsuarioCentroDeSalud"] as GeDoc.sisUsuariosCentroDeSalud).csId;
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
                                ItemPaciente.provId = 17;
                                ItemPaciente.osId = 305;
                                ItemPaciente.tipoIdOcupacion = 65;
                                ItemPaciente.tipoIdGrupoSanguineo = 45;
                                ItemPaciente.paisId = 1;
                                ItemPaciente.barId = 0;
                                ItemPaciente.etnId = 0;
                                ItemPaciente.csaId = Datos.csaId;
                                ItemPaciente.locId = Datos.locId == null ? 0 : Datos.locId;
                                ItemPaciente.depId = Datos.depId == null ? 0 : Datos.depId;
                                ItemPaciente.pacDonaOrganos = Datos.pacDonaOrganos == null ? false : Datos.pacDonaOrganos;
                                ItemPaciente.tipoIdEstadoCivil = Datos.tipoIdEstadoCivil == null ? 29 : Datos.tipoIdEstadoCivil;
                                ItemPaciente.pacNotificarXSMS = Datos.pacNotificarXSMS == null ? false : Datos.pacNotificarXSMS;
                                ItemPaciente.tipoIdSexo = Convert.ToInt16(Datos.tipoIdSexo == null || Datos.tipoIdSexo == 0 ? 186 : Datos.tipoIdSexo);
                                var _datosP = (from d in context.catEncuestaSinAdmisionPersona.ToList()
                                               where d.pacId == Datos.pacId
                                               select new catEncuestaSinAdmisionPersona()
                                               {
                                                   plaId = d.plaId,
                                                   pacId = d.pacId
                                               });


                                context.SaveChanges();
                                //Asigna el paciente a la encuesta
                                int _encId = (int)AsignarPacienteEncuesta(ItemPaciente.pacId, Datos.plaId, (short)Datos.encCausa);
                                encId = _encId;
                                //Asigna enfermedad al paciente                             
                                // List<enlEncuestaAPSPersonaEnfermedad> _enf = ObtenerEnfermedades(_DatosPacEnf, encPerId).ToList();
                            }
                            else
                            {
                                //Agrega el nuevo paciente en tabla pacientes
                                TryUpdateModel(ItemPaciente);
                                //ItemPaciente.pacFechaNacimiento = Convert.ToDateTime(Datos.pacFechaNacimientoTexto);
                                ItemPaciente.provId = 17;
                                ItemPaciente.osId = 305;
                                ItemPaciente.tipoIdOcupacion = 65;
                                ItemPaciente.tipoIdGrupoSanguineo = 45;
                                ItemPaciente.paisId = 1;
                                ItemPaciente.csId = _csId;
                                ItemPaciente.pacDonaOrganos = Datos.pacDonaOrganos == null ? false : Datos.pacDonaOrganos;
                                ItemPaciente.tipoIdEstadoCivil = Datos.tipoIdEstadoCivil == null ? 29 : Datos.tipoIdEstadoCivil;
                                ItemPaciente.pacNotificarXSMS = Datos.pacNotificarXSMS == null ? false : Datos.pacNotificarXSMS;
                                // ItemPaciente.csaId = Datos.csaId;
                                //ItemPaciente.locId = 0;
                                ItemPaciente.barId = 0;
                                ItemPaciente.etnId = 0;
                                ItemPaciente.locId = Datos.locId == null ? 0 : Datos.locId;
                                ItemPaciente.depId = Datos.depId == null ? 0 : Datos.depId;
                                ItemPaciente.tipoIdSexo = Convert.ToInt16(Datos.tipoIdSexo == null || Datos.tipoIdSexo == 0 ? 186 : Datos.tipoIdSexo);
                                context.catPaciente.AddObject(ItemPaciente);
                                context.SaveChanges();
                                //Asigna el paciente a la encuesta
                                int _encId = (int)AsignarPacienteEncuesta(ItemPaciente.pacId, Datos.plaId, (short)Datos.encCausa);
                                encId = _encId;

                            }
                        }
                        break;
                    case "Modificar"://modifica paciente asignado a una encuesta
                        //var NewPaciente = new catPaciente();
                        ItemPaciente = context.catPaciente.Single(x => x.pacId == Datos.pacId);
                        if (Datos.pacAccion == "Modificar")
                        {
                            TryUpdateModel(ItemPaciente);
                            ItemPaciente.locId = Datos.locId == null ? 0 : Datos.locId;
                            ItemPaciente.depId = Datos.depId == null ? 0 : Datos.depId;
                            ItemPaciente.tipoIdSexo = Convert.ToInt16(Datos.tipoIdSexo == null || Datos.tipoIdSexo == 0 ? 186 : Datos.tipoIdSexo);
                            ItemPaciente.csId = Datos.csId == null ? 0 : Datos.csId;
                            ItemPaciente.tipoIdEstadoCivil = ItemPaciente.tipoIdEstadoCivil == null ? 29 : ItemPaciente.tipoIdEstadoCivil;
                            ItemPaciente.csaId = ItemPaciente.csId;
                            ItemPaciente.pacDonaOrganos = ItemPaciente.pacDonaOrganos == null ? false : ItemPaciente.pacDonaOrganos;
                            ItemPaciente.pacNotificarXSMS = ItemPaciente.pacNotificarXSMS == null ? false : ItemPaciente.pacNotificarXSMS;
                            //ItemPaciente.pacFechaNacimiento = Convert.ToDateTime(Datos.pacFechaNacimientoTexto);
                            ItemPaciente.pacFechaNacimiento = Datos.pacFechaNacimiento;
                            encId = context.catEncuestaSinAdmisionPersona.Single(x => x.encId == Datos.encId && x.pacId == Datos.pacId).encId;
                            int _encCausa = Convert.ToInt32(Datos.encCausa == null ? 6 : Datos.encCausa);
                            Actualiza_PacienteEncuesta(ItemPaciente.pacId, Datos.plaId, _encCausa, (short)encId);
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
                _encId = encId
            });
        }


        private string getCentroSaludNombre(int _csId)
        {
            var CentroSalud = context.catCentroDeSalud.Single(w => w.csId == _csId);
            return CentroSalud.csNombre;
        }


        [GridAction]
        public ActionResult BuscaxDNI(int doc, int tipoDoc, int plaId, string Accion)
        {
            catPaciente error = new catPaciente();
            var _csId = (Session["UsuarioCentroDeSalud"] as sisUsuariosCentroDeSalud).csId;
            //var _Paciente = context.spGetListadoDePacientes2((short)_csId).Where(s => s.pacNumeroDocumento == doc && s.tipoIdTipoDocumento == (short)tipoDoc && s.pacNumeroDocumento != 11111).FirstOrDefault();
            var _MetodosUtiles = new MetodosUtiles();

            var _Paciente = new catEncuestaSinAdmisionPacientes();
            ViewData["FiltroContains"] = true;
            _Paciente.pacAccion = Accion;
            _Paciente.ErrorMessage = "";
            //Cargamos los datos de la encuesta
            var DatosActuales = context.catEncuestaSinAdmisionPlanilla.Single(w => w.plaId == plaId);
            var centroSalud = context.catCentroDeSalud.Single(w => w.csId == DatosActuales.csId);
            Session["Encuesta_csId"] = DatosActuales.csId;
            PopulateDropDownListPaciente();
            //  _Datos.PacienteEnfermedad = new List<enlEncuestaAPSPersonasEnfermedades>();
            var _encuestado = (from d in context.catEncuestaSinAdmisionPersona
                               where d.catPaciente.pacNumeroDocumento == doc & d.catPaciente.tipoIdTipoDocumento == tipoDoc
                               select new catEncuestaSinadmisionPersonas()
                               {
                                   encId = d.encId,
                                   encCausa = d.encCausa
                               }).FirstOrDefault();
            //SOLO PACIENTES : Busca en la tabla Pacientes
            _Paciente = (from d in context.spGetBusquedaEnPacientes_PADRON(tipoDoc, doc).ToList()
                         // where d.pacNumeroDocumento == doc && d.tipoIdTipoDocumento == (short)tipoDoc 
                         select new catEncuestaSinAdmisionPacientes()
                         {
                             pacId = d.pacId,
                             pacApellido = d.pacApellido.ToUpper(),
                             pacNombre = d.pacNombre.ToUpper(),
                             pacNumeroDocumento = d.pacNumeroDocumento,
                             tipoIdTipoDocumento = d.tipoIdTipoDocumento,
                             tipoIdSexo = d.tipoIdSexo,
                             tipoIdOcupacion = d.tipoIdOcupacion ?? 65,
                             tipoIdGrupoSanguineo = d.tipoIdGrupoSanguineo ?? 45,
                             tipoIdEstadoCivil = d.tipoIdEstadoCivil ?? 29,
                             //pacCUIL = d.pacCUIL ?? "",
                             // paisId = d.paisId ?? 1,
                             pacFechaNacimiento = d.pacFechaNacimiento ?? DateTime.Now.Date,
                             pacFechaNacimientoTexto = d.pacFechaNacimiento == null ? DateTime.Now.Date.ToString("dd/MM/yyyy") : d.pacFechaNacimiento.Value.ToString("dd/MM/yyyy"),
                             provId = 17,
                             depId = d.depId,
                             locId = d.locId,
                             /* barId = null,*/
                             csId = d.csId == null ? (short)DatosActuales.csId : (short)d.csId,
                             csNombre = centroSalud.csNombre,
                             pacTelefonoCelular = d.pacTelefonoCelular,
                             // pacNotificarXSMS = (bool)(d.pacNotificarXSMS ?? false),
                             osId = d.osId ?? 305,
                             csaId = d.csaId == null ? (short)0 : (short)d.csaId,
                             pacAccion = _Paciente.pacAccion,
                             ErrorMessage = "Paciente Encontrado"
                         }).FirstOrDefault();
            if (_Paciente != null)
            {
                // var _Msj = "El Paciente " + _Paciente.pacNumeroDocumento + " - " + _Paciente.pacNombre + " " + _Paciente.pacApellido + " ya existe en la tabla Pacientes.";
                _Paciente.pacFechaNacimiento = _Paciente.pacFechaNacimiento ?? DateTime.Now.Date;
                string validacion = getValidaSiPacienteAsignado(DatosActuales.plaFechaPlanilla, _Paciente.pacId,
                    (int)_Paciente.pacNumeroDocumento, _Paciente.tipoIdTipoDocumento, plaId);
                if (validacion == "")
                {
                    return PartialView("CRUDencuestaPaciente", _Paciente);
                }
                else
                {
                    return Json(new
                   {
                       Error = true,
                       Mensaje = validacion,
                       Foco = "pacNumeroDocumento"
                   });
                }
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
                                 select new catEncuestaSinAdmisionPacientes()
                                 {
                                     pacApellido = d.ppApellido.ToUpper(),
                                     pacNombre = d.ppNombre.ToUpper(),
                                     pacNumeroDocumento = d.ppNroDoc,
                                     tipoIdTipoDocumento = 10,
                                     tipoIdSexo = d.ppSexoId == "F" ? (short)9 : (short)10,
                                     tipoIdOcupacion = 65,
                                     tipoIdGrupoSanguineo = 45,
                                     tipoIdEstadoCivil = 29,
                                     pacCUIL = d.ppCUIL ?? "",
                                     paisId = (short)1,
                                     pacFechaNacimiento = d.ppFechaNacimiento ?? DateTime.Now.Date,
                                     pacFechaNacimientoTexto = d.ppFechaNacimiento == null ? DateTime.Now.Date.ToString("dd/MM/yyyy") : d.ppFechaNacimiento.Value.ToString("dd/MM/yyyy"),
                                     pacTelefonoCasa = d.ppTelefono1,
                                     pacTelefonoCelular = d.ppTelefono2,
                                     // PacienteEnfermedad = _encuestado == null ? getDatosenlEncuestasAPSPersonasEnf(0).ToList() : getDatosenlEncuestasAPSPersonasEnf((short)_encuestado.encPerId).ToList(),
                                     etnId = 0,
                                     osId = (short)305,
                                     provId = 17,
                                     depId = d.depId,
                                     locId = 0,
                                     barId = 0,
                                     csId = (short)DatosActuales.csId,
                                     pacCalle = null,
                                     pacCalleNumero = null,
                                     pacDepto = null,
                                     pacManzana = null,
                                     pacPiso = null,
                                     csNombre = centroSalud.csNombre,
                                     pacAccion = Accion,
                                     csaId = 0,
                                     ErrorMessage = "Paciente Encontrado"
                                 }).FirstOrDefault();
                    if (_Paciente != null)
                    {
                        return PartialView("CRUDencuestaPaciente", _Paciente);
                    }
                    if (_Paciente == null)
                    {
                        //NUEVO PACIENTE : No esta en tabla paciente ni padron
                        _Paciente = new catEncuestaSinAdmisionPacientes();
                        _Paciente.pacNumeroDocumento = doc;
                        _Paciente.provId = 17;
                        _Paciente.depId = 0;
                        _Paciente.locId = 0;
                        _Paciente.barId = 0;
                        _Paciente.csId = (short)_csId;
                        _Paciente.pacCalle = null;
                        _Paciente.pacCalleNumero = null;
                        _Paciente.pacDepto = null;
                        _Paciente.pacManzana = null;
                        _Paciente.pacPiso = null;
                        _Paciente.csNombre = centroSalud.csNombre;
                        _Paciente.pacFechaNacimiento = _Paciente.pacFechaNacimiento ?? DateTime.Now.Date;
                        _Paciente.csaId = (short)_csId;
                        _Paciente.pacAccion = Accion;
                        _Paciente.ErrorMessage = "Paciente Nuevo";
                        return Json(new
                        {
                            Error = true,
                            Mensaje = "Nuevo Paciente",
                            Foco = "pacApellido",
                            _data = _Paciente
                        });

                        // return PartialView("CRUDencuestaPaciente", _Paciente);

                    }
                }
            }
            return PartialView("CRUDencuestaPaciente", _Paciente);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult getPadron(int doc, int tipoDoc, int plaId, string Accion)
        {
            catPaciente error = new catPaciente();
            var _csId = (Session["UsuarioCentroDeSalud"] as sisUsuariosCentroDeSalud).csId;
            //var _Paciente = context.spGetListadoDePacientes2((short)_csId).Where(s => s.pacNumeroDocumento == doc && s.tipoIdTipoDocumento == (short)tipoDoc && s.pacNumeroDocumento != 11111).FirstOrDefault();
            var _MetodosUtiles = new MetodosUtiles();

            var _Datos = new catEncuestasPacientes();
            _Datos.pacAccion = Accion;

            //Cargamos los datos de la encuesta
            var DatosActuales = context.catEncuestaSinAdmisionPlanilla.Single(w => w.plaId == plaId);
            var centroSalud = context.catCentroDeSalud.Single(w => w.csId == DatosActuales.csId);
            Session["Encuesta_csId"] = DatosActuales.csId;
            //  _Datos.PacienteEnfermedad = new List<enlEncuestaAPSPersonasEnfermedades>();
            var _encuestado = (from d in context.catEncuestaSinAdmisionPersona
                               where d.catPaciente.pacNumeroDocumento == doc & d.catPaciente.tipoIdTipoDocumento == tipoDoc
                               select new catEncuestaSinadmisionPersonas()
                               {
                                   encId = d.encId,
                                   encCausa = d.encCausa
                               }).FirstOrDefault();
            //SOLO PACIENTES : Busca en la tabla Pacientes
            var _Paciente = (from d in context.spGetBusquedaEnPacientes_PADRON(tipoDoc, doc).ToList()
                             // where d.pacNumeroDocumento == doc && d.tipoIdTipoDocumento == (short)tipoDoc 
                             select new catEncuestaSinAdmisionPacientes()
                             {
                                 pacId = d.pacId,
                                 pacApellido = d.pacApellido.ToUpper(),
                                 pacNombre = d.pacNombre.ToUpper(),
                                 pacNumeroDocumento = d.pacNumeroDocumento,
                                 tipoIdTipoDocumento = d.tipoIdTipoDocumento,
                                 tipoIdSexo = d.tipoIdSexo,
                                 tipoIdOcupacion = d.tipoIdOcupacion ?? 65,
                                 tipoIdGrupoSanguineo = d.tipoIdGrupoSanguineo ?? 45,
                                 tipoIdEstadoCivil = d.tipoIdEstadoCivil ?? 29,
                                 //pacCUIL = d.pacCUIL ?? "",
                                 // paisId = d.paisId ?? 1,
                                 pacFechaNacimiento = d.pacFechaNacimiento ?? DateTime.Now.Date,
                                 pacFechaNacimientoTexto = d.pacFechaNacimiento == null ? DateTime.Now.Date.ToString("dd/MM/yyyy") : d.pacFechaNacimiento.Value.ToString("dd/MM/yyyy"),
                                 provId = 17,
                                 depId = d.depId,
                                 locId = d.locId,
                                 /* barId = null,*/
                                 csId = d.csId == null ? (short)DatosActuales.csId : (short)d.csId,
                                 csNombre = centroSalud.csNombre,
                                 pacTelefonoCelular = d.pacTelefonoCelular,
                                 // pacNotificarXSMS = (bool)(d.pacNotificarXSMS ?? false),
                                 osId = d.osId ?? 305,
                                 csaId = d.csaId == null ? (short)0 : (short)d.csaId,
                                 pacAccion = "Agregar"
                             }).FirstOrDefault();
            if (_Paciente != null)
            {
                // var _Msj = "El Paciente " + _Paciente.pacNumeroDocumento + " - " + _Paciente.pacNombre + " " + _Paciente.pacApellido + " ya existe en la tabla Pacientes.";
                _Paciente.pacFechaNacimiento = _Paciente.pacFechaNacimiento ?? DateTime.Now.Date;
                //  _Paciente.protur = _encuestado;
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
                                 select new catEncuestaSinAdmisionPacientes()
                                 {
                                     pacApellido = d.ppApellido.ToUpper(),
                                     pacNombre = d.ppNombre.ToUpper(),
                                     pacNumeroDocumento = d.ppNroDoc,
                                     tipoIdTipoDocumento = 10,
                                     tipoIdSexo = d.ppSexoId == "F" ? (short)9 : (short)10,
                                     tipoIdOcupacion = 65,
                                     tipoIdGrupoSanguineo = 45,
                                     tipoIdEstadoCivil = 29,
                                     pacCUIL = d.ppCUIL ?? "",
                                     paisId = (short)1,
                                     pacFechaNacimiento = d.ppFechaNacimiento ?? DateTime.Now.Date,
                                     pacFechaNacimientoTexto = d.ppFechaNacimiento == null ? DateTime.Now.Date.ToString("dd/MM/yyyy") : d.ppFechaNacimiento.Value.ToString("dd/MM/yyyy"),
                                     pacTelefonoCasa = d.ppTelefono1,
                                     pacTelefonoCelular = d.ppTelefono2,
                                     // PacienteEnfermedad = _encuestado == null ? getDatosenlEncuestasAPSPersonasEnf(0).ToList() : getDatosenlEncuestasAPSPersonasEnf((short)_encuestado.encPerId).ToList(),
                                     etnId = 0,
                                     osId = (short)305,
                                     provId = 17,
                                     depId = d.depId,
                                     locId = null,
                                     barId = null,
                                     csId = (short)DatosActuales.csId,
                                     pacCalle = null,
                                     pacCalleNumero = null,
                                     pacDepto = null,
                                     pacManzana = null,
                                     pacPiso = null,
                                     csNombre = centroSalud.csNombre,
                                     pacAccion = "Agregar",
                                     csaId = 0
                                 }).FirstOrDefault();
                    if (_Paciente != null)
                    {

                        return (Json(_Paciente));

                    }
                    if (_Paciente == null)
                    {
                        //NUEVO PACIENTE : No esta en tabla paciente ni padron
                        _Paciente = new catEncuestaSinAdmisionPacientes();
                        _Paciente.provId = 17;
                        _Paciente.depId = 0;
                        _Paciente.locId = 0;
                        _Paciente.barId = 0;
                        _Paciente.csId = (short)DatosActuales.csId;
                        _Paciente.pacCalle = null;
                        _Paciente.pacCalleNumero = null;
                        _Paciente.pacDepto = null;
                        _Paciente.pacManzana = null;
                        _Paciente.pacPiso = null;
                        _Paciente.csNombre = centroSalud.csNombre;
                        _Paciente.pacFechaNacimiento = _Paciente.pacFechaNacimiento ?? DateTime.Now.Date;
                        _Paciente.csaId = 0;
                        return (Json(_Paciente));

                    }
                }
            }
            return (Json(_Paciente));
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


            var _DN = (from s in context.catDepartamento.ToList()
                       where s.provId == 17
                       orderby s.depNombre ascending
                       select new catDepartamento()
                       {
                           depId = s.depId,
                           depNombre = s.depNombre
                       }).ToList();
            var _LN = (from s in context.catLocalidad.ToList()
                       // where s.depId == _dpto.depId
                       orderby s.locNombre descending
                       select new catLocalidad()
                       {
                           locId = s.locId,
                           locNombre = s.locNombre
                       }).ToList();

            var _CSA = (from p in context.catCentroDeSalud.ToList()
                        where p.csId == _csId
                        select new catCentroDeSalud
                        {
                            csId = p.csId,
                            csNombre = p.csNombre
                        }).ToList();
            _CSA.AddRange((from a in context.catCentroDeSalud.ToList()
                           where a.csPublico == 1
                           select new catCentroDeSalud
                           {
                               csId = a.csId,
                               csNombre = a.csNombre
                           }).ToList());

            var _CAU = (from s in context.catEncuestaSinAdmisionCausa.ToList()
                        orderby s.causaNombre ascending
                        select new catEncuestaSinAdmisionCausa()
                        {
                            causaId = s.causaId,
                            causaNombre = s.causaId + " - " + s.causaNombre
                        }).ToList();


            ViewData["encCausa_Data"] = new SelectList(_CAU, "causaId", "causaNombre");
            ViewData["tipoIdTipoDocumento_Data"] = new SelectList(_TipoDocumento, "tipoId", "tipoDescripcion");
            ViewData["tipoIdSexo_Data"] = new SelectList(_Sexo, "tipoId", "tipoDescripcion");
            ViewData["depId_Data"] = new SelectList(_DN, "depId", "depNombre");
            ViewData["locId_Data"] = new SelectList(_LN, "locId", "locNombre");
            ViewData["csaId_Data"] = new SelectList(_CSA, "csId", "csNombre");
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult BindingLocalidades(int depId)
        {
            ViewData["locId_Data"] = new SelectList(getLocalidades(depId), "locId", "locNombre");
            return Json((SelectList)ViewData["LocId_Data"]);
        }
        private SelectList ReorderList<T>(List<T> listObject, string valueField, string textField)
        {
            var zeroIdItem = listObject.Single<T>(a => int.Parse(a.GetType().GetProperty(valueField).GetValue(a, null).ToString()) == 0);
            listObject = listObject.Where(a => int.Parse(a.GetType().GetProperty(valueField).GetValue(a, null).ToString()) != 0)
                                   .OrderBy(a => a.GetType().GetProperty(textField).GetValue(a, null)).ToList();
            listObject.Insert(0, zeroIdItem);
            return new SelectList(listObject, valueField, textField);
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

        //para verificar especialidades repetidas al agregar o editar
        [AcceptVerbs(HttpVerbs.Post)]
        public int verificarEspecialidad(int? paciente, int? especialidad)
        {

            var result = (
                            from a in context.catEncuestaSinAdmisionPersona
                            join b in context.catEncuestaSinadmisionPersonaEspecialidad
                            on a.encId equals b.encId into c
                            from d in c
                            where d.espId == especialidad &&
                                  a.pacId == paciente
                            select (d)
                         ).Count();
            if (result > 0)
            {

                return 0;
            }
            else
                return 1;
        }

        //trae los valores de NO para la verificación en la entrada de especialidades (gracias telerik otra vez)
        [AcceptVerbs(HttpVerbs.Post)]
        public string valoresDeNo()
        {

            var interconsulta = context.sisTipo.Where(a => a.tipoCodigo.Equals("IC") && a.tipoDescripcion.Equals("NO")).FirstOrDefault().tipoId;

            var derivado = context.sisTipo.Where(a => a.tipoCodigo.Equals("DI") && a.tipoDescripcion.Equals("NO")).FirstOrDefault().tipoId;

            //var atendidoLocal = context.sisTipo.Where(a => a.tipoCodigo.Equals("AL") && a.tipoDescripcion.Equals("NO")).FirstOrDefault().tipoId;

            //select st.tipoId from sisTipo st inner join sisTipoEntidad te
            //on st.tipoeId = te.tipoeId 
            //where te.tipoeCodigo = 'AL' and st.tipoDescripcion = 'NO'

            var atendidoLocal = (
                from st in context.sisTipo
                join te in context.sisTipoEntidad on st.tipoeId equals te.tipoeId
                where te.tipoeCodigo == "AL" && st.tipoDescripcion == "NO"
                select st.tipoId
                ).FirstOrDefault();

            var programado = context.catEncuestaSinAdmisionDerivado.Where(a => a.Descripcion.Equals("NO")).FirstOrDefault().derId;

            string result = "[\"" + interconsulta + "\",\"" + derivado + "\",\"" + atendidoLocal + "\",\"" + programado + "\"]";

            return result;

        }

        //trae fecha de la planilla
        [AcceptVerbs(HttpVerbs.Post)]
        public string fechaDePlanilla(int? numero)
        {

            var resultado = (
                from esap in context.catEncuestaSinAdmisionPlanilla
                where esap.plaId == numero
                select esap.plaFechaPlanilla
                ).FirstOrDefault();

            var fechaString = resultado.ToString();

            return fechaString;

        }

    }
}