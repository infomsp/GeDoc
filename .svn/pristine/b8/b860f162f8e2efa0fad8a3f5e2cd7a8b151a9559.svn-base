namespace GeDoc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Telerik.Web.Mvc;
    using Telerik.Web.Mvc.Extensions;
    using GeDoc.Filters;
    using GeDoc.Models;
    using NPOI.HSSF.UserModel;
    using System.IO;

    public partial class catPersonaController : Controller
    {
        private efAccesoADatosEntities context = new efAccesoADatosEntities();

        //Edición de datos

        [GridAction]
        public ActionResult _SelectEditing()
        {
            return View(new GridModel(All()));
        }

        private IList<catPersonas> All()
        {
                return getDatos().ToList();
        }

        [GridAction]
        public ActionResult _BindingPersonaCertificado(Int64? idPersona)
        {
            idPersona = idPersona == null ? 1 : idPersona;

            return View(new GridModel<catPersonaCertificados>
            {
                Data = AllCertificado(idPersona)
            });
        }

        private IList<catPersonaCertificados> AllCertificado(Int64? idPersona)
        {
            return getDatosCertificados(idPersona).ToList();
        }

        private IEnumerable<catPersonaCertificados> getDatosCertificados(Int64? idPersona)
        {
            var _Datos = (from d in context.catPersonaCertificado
                          where d.perId == idPersona
                          orderby d.percFecha descending
                          select new catPersonaCertificados()
                          {
                              perId = d.perId,
                              tipoId = d.tipoId,
                              percCopias = d.percCopias,
                              percFecha = d.percFecha,
                              percSueldoNeto = d.percSueldoNeto,
                              percSueldoBruto = d.percSueldoBruto,
                              percPoseeEmbargos = d.percPoseeEmbargos,
                              percId = d.percId,
                              percImprimeSueldos = d.sisTipo.tipoCodigo == "CT",
                              percLugarDePresentacion = d.percLugarDePresentacion,
                              percImprimeEmbargos = d.percImprimeEmbargos,
                              percImprimeGuardias = d.percImprimeGuardias,
                              percPoseeGuardias = d.percPoseeGuardias,
                              percAntiguedadVacaciones = d.percAntiguedadVacaciones,
                              Tipo = new sisTipos(){ tipoDescripcion = d.sisTipo.tipoDescripcion }
                          }).ToList();

            return _Datos.AsEnumerable();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _SaveEditing(int id)
        {
            var _Item = context.catPersona.FirstOrDefault(p => p.perId == id);

            TryUpdateModel(_Item);

            var _Persona = context.catPersona.FirstOrDefault(s => s.perDNI == _Item.perDNI && s.perId != id);
            if (_Persona != null)
            {
                ModelState.AddModelError("perDNI", "El Empleado ya existe en " + _Persona.catSector.secNombre + ". Solicite actualización a nivel central");
            }
            else
            {
                if (Session["upArchivo"] != null)
                {
                    _Item.perFoto = DateTime.Now.ToString("yyyyMMddhhmmss") + Session["upArchivo"];
                    System.IO.File.Move(Session["PathArchivos"] + "/" + Session["upArchivo"], Session["PathArchivos"] + "/" + _Item.perFoto);
                    Session["upArchivo"] = null;
                }

                Edit(_Item);
            }

            return View(new GridModel(All()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _InsertEditing()
        {
            var _Item = new catPersona();

            if (TryUpdateModel(_Item))
            {
                var _Persona = context.catPersona.FirstOrDefault(s => s.perDNI == _Item.perDNI);
                if (_Persona != null)
                {
                    ModelState.AddModelError("perDNI", "El Empleado ya existe en " + _Persona.catSector.secNombre + ". Solicite actualización a nivel central");
                }
                else
                {
                    if (Session["upArchivo"] != null)
                    {
                        _Item.perFoto = DateTime.Now.ToString("yyyyMMddhhmmss") + Session["upArchivo"];
                        System.IO.File.Move(Session["PathArchivos"] + "/" + Session["upArchivo"], Session["PathArchivos"] + "/" + _Item.perFoto);
                        Session["upArchivo"] = null;
                    }
                    Create(_Item);
                }
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

        private IEnumerable<catPersonas> getDatos()
        {
            var _MetodosUtiles = new MetodosUtiles();
            
            try
            {
                var ZonaSanitaria = (Session["Usuario"] as sisUsuario).repId;

                //var _Prueba = context.vwPersona.ToList();
                var _Datos = (from d in context.spGetListadoDePersonas(ZonaSanitaria).ToList()
                              //where ZonaSanitaria == null ? true : (d.secId == 10 || d.secId == 11) && (d.secId == 8 || d.secId == 9)
                              select new catPersonas()
                                        {
                                            secId = d.secId,
                                            perAntiguedad = getAntiguedad(d.perAntiguedadAnios, d.perAntiguedadMeses, d.perAntiguedadDias),
                                            perAntiguedadPago = getAntiguedad(d.perAntiguedadAniosPago, d.perAntiguedadMesesPago, d.perAntiguedadDiasPago),
                                            perAntiguedadVacaciones = getAntiguedad(d.perAntiguedadAniosVacaciones, d.perAntiguedadMesesVacaciones, d.perAntiguedadDiasVacaciones),
                                            perCUIL = d.perCUIL,
                                            perDNI = d.perDNI,
                                            perEdad = _MetodosUtiles.Edad(d.perFechaNacimiento),
                                            perFechaNacimiento = d.perFechaNacimiento,
                                            perApellidoyNombre = d.perApellidoyNombre.ToUpper(),
                                            perObservaciones = d.perObservaciones == null ? d.perObservaciones : d.perObservaciones.ToUpper(),
                                            perPadron = d.perPadron,
                                            perTelefono = d.perTelefono,
                                            tipoIdSexo = d.tipoIdSexo,
                                            perEmail = d.perEmail,
                                            perEsContratado = d.perEsContratado,
                                            perId = d.perId,
                                            secCodigo = d.secCodigo,
                                            secNombre = d.secNombre,
                                            perAntiguedadAnios = d.perAntiguedadAnios,
                                            tipoIdSexoTexto = d.tipoIdSexoTexto,
                                            perEstado = getEstado(d.tipoCodigo, d.tipoDescripcion, "E"),
                                            perEstadoImagen = getEstado(d.tipoCodigo, d.tipoDescripcion, "I"),
                                            VisibilidadImagen = getEstado(d.tipoCodigo, d.tipoDescripcion, "X"),
                                            perFoto = d.perFoto,
                                            paisId = d.paisId,
                                            paisNombre = d.paisNombre,
                                            perCelular = d.perCelular,
                                            perEstadoCivil = d.perEstadoCivil,
                                            perFechaCasamiento = d.perFechaCasamiento,
                                            perLeeoEscribe = (bool)(d.perLeeoEscribe == null ? false : d.perLeeoEscribe),
                                            profId = d.profId,
                                            profNombre = d.profNombre,
                                            provId = d.provId,
                                            provNombre = d.provNombre,
                                            tipoIdEstadoCivil = d.tipoIdEstadoCivil,
                                            perDomicilio = d.perDomicilio == null ? d.perDomicilio : d.perDomicilio.ToUpper(),
                                            perAutorizaNotificarSMS = d.perAutorizaNotificarSMS == null ? false : (bool)d.perAutorizaNotificarSMS,
                                            perFechaEstado = d.perFechaEstado,
                                            AsistenciaCodigo = d.AsistenciaDelDiaCodigo,
                                            AsistenciaEstado = d.AsistenciaDelDia,
                                            AsistenciaImagen = d.AsistenciaDelDiaCodigo <= 0 ? "Gris.png" : (d.AsistenciaDelDiaCodigo == 1 ? "Rojo.png" : (d.AsistenciaDelDiaCodigo == 2 ? "Verde.png" : "Amarillo.png")),
                                            Oficina = new catOficinas() { ofiCodigo = d.OficinaCodigo == null ? 0 : (int)d.OficinaCodigo, ofiNombre = d.Oficina },
                                            perFuncion = d.perFuncion,
                                            gdId = d.gdId,
                                            repId = d.repId,
                                            ZonaSanitaria = d.repNombre,
                                            perPertenecePlantaAdministrativa = d.perPertenecePlantaAdministrativa ?? false
                                        }).ToList();
                
                return _Datos.AsEnumerable();
            }
            catch (Exception ex)
            { }

            return null;
        }

        //private string getAntiguedad(EntityCollection<catPersonaEstado> pEstados)
        //{
        //    if (pEstados == null)
        //    {
        //        return "";
        //    }

        //    var _Estados = pEstados.ToList().Where(w => w.sisTipo.tipoCodigo != "LI").OrderBy(o => o.pereFecha).ToList();
        //    int _Anios = 0;
        //    int _Meses = 0;
        //    int _Dias = 0;
        //    string _Tipo = "";
        //    DateTime _FechaDesde = DateTime.Now;
        //    DateTime _FechaHasta = DateTime.Now;
        //    string _Antiguedad = "NO Determinada";

        //    if (_Estados.Count() > 1)
        //    {
        //        _Anios = 0;
        //    }

        //    foreach (var _Est in _Estados)
        //    {
        //        _Tipo = _Est.sisTipo.tipoCodigo;
        //        if (_Tipo == "AC")
        //        {
        //            _FechaDesde = (DateTime)_Est.pereFecha;
        //        }
        //        else
        //        {
        //            _FechaHasta = (DateTime)_Est.pereFecha;

        //            if ((_FechaHasta.Month - _FechaDesde.Month) < 0)
        //            {
        //                _Anios += _FechaHasta.Year - _FechaDesde.Year;
        //                _Meses += _FechaHasta.Month;
        //                _Dias += _FechaHasta.Day;
        //            }
        //            else
        //            {
        //                _Anios += _FechaHasta.Year - _FechaDesde.Year;
        //                _Meses += _FechaHasta.Month - _FechaDesde.Month;
        //                _Dias += _FechaHasta.Day - _FechaDesde.Day;
        //            }
        //        }
        //    }
        //    if (_Tipo == "AC")
        //    {
        //        _FechaHasta = DateTime.Now;

        //        if ((_FechaHasta.Month - _FechaDesde.Month) < 0)
        //        {
        //            _Anios += _FechaHasta.Year - _FechaDesde.Year;
        //            _Meses += _FechaHasta.Month;
        //            _Dias += _FechaHasta.Day;
        //        }
        //        else
        //        {
        //            _Anios += _FechaHasta.Year - _FechaDesde.Year;
        //            _Meses += _FechaHasta.Month - _FechaDesde.Month;
        //            _Dias += _FechaHasta.Day - _FechaDesde.Day;
        //        }
        //    }

        //    if ((_Anios + _Meses + _Dias) == 0)
        //    {
        //        _Antiguedad = "NO Determinada";
        //    }
        //    else
        //    {
        //        if (_Anios > 0)
        //        {
        //            _Antiguedad = _Anios.ToString() + " Años ";
        //        }
        //        if (_Meses > 0)
        //        {
        //            _Antiguedad += _Meses.ToString() + " Meses ";
        //        }
        //        if (_Dias > 0)
        //        {
        //            _Antiguedad += _Dias.ToString() + " Días ";
        //        }
        //    }

        //    return _Antiguedad;
        //}

        private string getAntiguedad(short? _Anios, short? _Meses, short? _Dias)
        {
            string _Antiguedad = "NO Determinada";

            if ((_Anios + _Meses + _Dias) > 0)
            {
                _Antiguedad = "";
                if (_Anios > 0)
                {
                    if (_Anios == 1)
                    {
                        _Antiguedad = _Anios.ToString() + " Año ";
                    }
                    else
                    {
                        _Antiguedad = _Anios.ToString() + " Años ";
                    }
                }
                if (_Meses > 0)
                {
                    if (_Meses == 1)
                    {
                        _Antiguedad += _Meses.ToString() + " Mes ";
                    }
                    else
                    {
                        _Antiguedad += _Meses.ToString() + " Meses ";
                    }
                }
                if (_Dias > 0)
                {
                    if (_Dias == 1)
                    {
                        _Antiguedad += _Dias.ToString() + " Día ";
                    }
                    else
                    {
                        _Antiguedad += _Dias.ToString() + " Días ";
                    }
                }
            }

            return _Antiguedad;
        }
        public ActionResult Index()
        {
            ViewBag.Catalogo = "Empleados";
            int _perId = 1;// All().First().perId;
            ViewData["FiltroContains"] = true;
            ViewData["Estados"] = AllEstados(_perId);
            Session["PathArchivos"] = Server.MapPath("~/Content/Archivos/FotosPersonal");
            ViewData["GrupoFamiliar"] = AllGrupoFamiliar(_perId);
            ViewData["Especialidades"] = AllEspecialidades(_perId);

            PopulateDropDownList();

            return PartialView();
        }

        private void Create(catPersona pItem)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    context.catPersona.AddObject(pItem);

                    //Registra log de usuario
                    new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catPersona", "Agregar", "");

                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("perApellidoyNombre", ex.Message);
                }
            }

            return;
        }

        private void Edit(catPersona pItem)
        {
            if (ModelState.IsValid)
            {
                //Registra log de usuario
                new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catPersona", "Modificar", "");

                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("perApellidoyNombre", ex.Message);
                }
            }
            return;
        }

        private void DeleteConfirmed(int id)
        {
            try
            {
                var _Item = context.catPersona.Single(x => x.perId == id);
                context.catPersona.DeleteObject(_Item);

                //Registra log de usuario
                new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catPersona", "Eliminar", "");

                context.SaveChanges();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("perApellidoyNombre", ex.Message);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult ViewDetails(int personaId)
        {
            var _MetodosUtiles = new MetodosUtiles();

            //Registra log de usuario
            new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catPersona", "Examinar", "");

            string _Idiomas = "";
            foreach (var _Idioma in context.catPersonaIdioma.Where(p => p.perId == personaId))
            {
                _Idiomas += _Idioma.catIdioma.idiomaDescripcion + ", ";
            }
            _Idiomas = _Idiomas == "" ? _Idiomas : _Idiomas.Substring(0, _Idiomas.Length - 2);

            catPersonas _Item = (from d in context.catPersona.Where(w => w.perId == personaId).ToList()
                                 select new catPersonas()
                                            {
                                                secId = d.secId,
                                                perAntiguedad = getAntiguedad(d.perAntiguedadAnios, d.perAntiguedadMeses, d.perAntiguedadDias),
                                                perAntiguedadPago = getAntiguedad(d.perAntiguedadAniosPago, d.perAntiguedadMesesPago, d.perAntiguedadDiasPago),
                                                perAntiguedadVacaciones = getAntiguedad(d.perAntiguedadAniosVacaciones, d.perAntiguedadMesesVacaciones, d.perAntiguedadDiasVacaciones),
                                                perCUIL = d.perCUIL,
                                                perDNI = d.perDNI,
                                                perEdad = _MetodosUtiles.Edad(d.perFechaNacimiento),
                                                perFechaNacimiento = d.perFechaNacimiento,
                                                perApellidoyNombre = d.perApellidoyNombre,
                                                perObservaciones = d.perObservaciones,
                                                perPadron = d.perPadron,
                                                perTelefono = d.perTelefono,
                                                tipoIdSexo = d.tipoIdSexo,
                                                perEmail = d.perEmail == null ? "" : d.perEmail,
                                                perEsContratado = d.perEsContratado,
                                                perId = d.perId,
                                                secNombre = d.catSector.secNombre,
                                                perFechaNacimientoTexto = d.perFechaNacimiento == null ? "" : d.perFechaNacimiento.Value.ToString("dd/MM/yyyy"),
                                                tipoIdSexoTexto = d.sisTipo == null ? "" : d.sisTipo.tipoDescripcion,
                                                perFoto = d.perFoto,
                                                paisId = d.paisId,
                                                paisNombre = d.paisId == null ? "" : d.catPais.paisNombre,
                                                perCelular = d.perCelular == null ? "" : d.perCelular,
                                                perEstadoCivil = d.sisTipo1 == null ? "" : d.sisTipo1.tipoDescripcion,
                                                perFechaCasamiento = d.perFechaCasamiento,
                                                perFechaCasamientoTexto = d.perFechaCasamiento == null ? "" : d.perFechaCasamiento.Value.ToString("dd/MM/yyyy"),
                                                perLeeoEscribe = (bool)(d.perLeeoEscribe == null ? false : d.perLeeoEscribe),
                                                profId = d.profId,
                                                profNombre = d.catProfesion == null ? "" : d.catProfesion.profNombre,
                                                provId = d.provId,
                                                provNombre = d.catProvincia == null ? "" : d.catProvincia.provNombre,
                                                tipoIdEstadoCivil = d.tipoIdEstadoCivil,
                                                perAutorizaNotificarSMS = (bool)(d.perAutorizaNotificarSMS == null ? false : d.perAutorizaNotificarSMS),
                                                perDomicilio = d.perDomicilio == null ? "" : d.perDomicilio,
                                                perIdiomas = _Idiomas,
                                                carDescripcion = (d.gdId != null && d.catGradosDesignacion1.gdFechaHasta != null) ? (d.catGradosDesignacion1.catGradosCategoria.catGrados.graDescripcion + " " + d.catGradosDesignacion1.catGradosCategoria.gracDescripcion + ", SERVICIO " + d.catGradosDesignacion1.gdServicio + ", " + d.catGradosDesignacion1.sisTipo.tipoDescripcion + ", " + d.catGradosDesignacion1.gdHoras + " HORAS (" + (d.catGradosDesignacion1.gdHorasAdicional > 0 ? d.catGradosDesignacion1.gdHorasAdicional + " HORAS ADICIONALES)" : "SIN HORAS ADICIONALES)")) : (d.catCargoDesignacion.Count == 0 ? "" : d.catCargoDesignacion.First(f => f.desigVigenciaHasta == null).catCargoCategoria.catCargo.catAgrupamiento.agrCodigo + ") " + d.catCargoDesignacion.First(f => f.desigVigenciaHasta == null).catCargoCategoria.catCargo.carDescripcion + " (Categoría " + d.catCargoDesignacion.First(f => f.desigVigenciaHasta == null).catCargoCategoria.categNumero + "-" + d.catCargoDesignacion.First().catCargoCategoria.categHoras + ")"),
                                                ofiId = d.ofiId,
                                                Oficina = d.ofiId == null? new catOficinas() { ofiCodigo = 0, ofiNombre = "" } : new catOficinas() { ofiCodigo = d.catOficina.ofiCodigo, ofiNombre = d.catOficina.ofiNombre },
                                                perFuncion = d.perFuncion,
                                                gdId = d.gdId
                                            }).FirstOrDefault();

            return Json(new { InfoPersona = _Item });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult setCertificado(int idPersona, bool ImprimeSueldos, decimal SueldoBruto, decimal SueldoNeto, bool PoseeEmbargos, string PresentarEn, bool ImprimeEmbargos, bool ImprimeGuardias, bool PoseeGuardias)
        {
            //Registra log de usuario
            new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catPersona", "Certificados", "");

            try
            {
                //Guardamos cambios\\
                string _tipoCodigo = ImprimeSueldos ? "CT" : "CS";
                short _tipoId = context.sisTipo.Where(s => s.tipoCodigo == _tipoCodigo).First().tipoId;
                catPersonaCertificado _Insert = new catPersonaCertificado();
                _Insert.percCopias = 0;
                _Insert.percFecha = DateTime.Now;
                _Insert.percLugarDePresentacion = PresentarEn;
                _Insert.percPoseeEmbargos = PoseeEmbargos;
                _Insert.percSueldoBruto = SueldoBruto;
                _Insert.percSueldoNeto = SueldoNeto;
                _Insert.perId = idPersona;
                _Insert.tipoId = _tipoId;
                _Insert.percImprimeEmbargos = ImprimeEmbargos;
                _Insert.percImprimeGuardias = ImprimeGuardias;
                _Insert.percPoseeGuardias = PoseeGuardias;
                _Insert.percAntiguedadVacaciones = "";

                context.catPersonaCertificado.AddObject(_Insert);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(new { isValid = false, Error = ex.InnerException.Message });
            }

            return Json(new { isValid = true, Error = "" });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult setCertificadoLAR(int idPersona, string Antiguedad)
        {
            //Registra log de usuario
            new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catPersona", "Certificados", "");

            try
            {
                //Guardamos cambios\\
                string _tipoCodigo = "CV";
                short _tipoId = context.sisTipo.Where(s => s.tipoCodigo == _tipoCodigo).First().tipoId;
                catPersonaCertificado _Insert = new catPersonaCertificado();
                _Insert.percCopias = 0;
                _Insert.percFecha = DateTime.Now;
                _Insert.percLugarDePresentacion = "";
                _Insert.percPoseeEmbargos = false;
                _Insert.percSueldoBruto = 0;
                _Insert.percSueldoNeto = 0;
                _Insert.perId = idPersona;
                _Insert.tipoId = _tipoId;
                _Insert.percImprimeEmbargos = false;
                _Insert.percImprimeGuardias = false;
                _Insert.percPoseeGuardias = false;
                _Insert.percAntiguedadVacaciones = Antiguedad;

                context.catPersonaCertificado.AddObject(_Insert);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(new { isValid = false, Error = ex.InnerException.Message });
            }

            return Json(new { isValid = true, Error = "" });
        }

        //Datos para el dropdown
        protected void PopulateDropDownList()
        {
            var _Sexo = (from s in context.sisTipo.ToList()
                        where s.sisTipoEntidad.tipoeCodigo == "SX"
                        select new sisTipo()
                        {
                            tipoId = s.tipoId,
                            tipoDescripcion = s.tipoDescripcion
                        }).ToList();

            var _Esta = (from s in context.sisTipo.ToList()
                         where s.sisTipoEntidad.tipoeCodigo == "PR"
                         select new sisTipo()
                         {
                             tipoId = s.tipoId,
                             tipoDescripcion = s.tipoDescripcion
                         }).ToList();

            var _EsCi = (from s in context.sisTipo.ToList()
                         where s.sisTipoEntidad.tipoeCodigo == "EC"
                         select new sisTipo()
                         {
                             tipoId = s.tipoId,
                             tipoDescripcion = s.tipoDescripcion
                         }).ToList();

            var _Pare = (from s in context.sisTipo.ToList()
                         where s.sisTipoEntidad.tipoeCodigo == "PA"
                         select new sisTipo()
                         {
                             tipoId = s.tipoId,
                             tipoDescripcion = s.tipoDescripcion
                         }).ToList();

            var _TDoc = (from s in context.sisTipo.ToList()
                         where s.sisTipoEntidad.tipoeCodigo == "TD"
                         select new sisTipo()
                         {
                             tipoId = s.tipoId,
                             tipoDescripcion = s.tipoDescripcion
                         }).ToList();

            var _Esp = (from s in context.catEspecialidad.ToList()
                        // where s.espIdPadre == null
                        orderby s.espNombre ascending 
                         select new catEspecialidades
                         {
                             espId = s.espId,
                             espNombre = s.espCodigo + " - " + s.espNombre
                         }).ToList();

            var _Ofi = (from s in context.catOficina.ToList()
                        select new catOficinas
                        {
                            ofiId = s.ofiId,
                            ofiCodigo = s.ofiCodigo,
                            ofiNombre = s.ofiNombre
                        }).ToList();

            var _Sec = (from s in context.catSector.ToList()
                        select new catSectores
                        {
                            secId = s.secId,
                            secNombre = "(" + s.secCodigo + ") " + s.secNombre
                        }).ToList();

            var _Prof = (from s in context.catProfesion.ToList()
                        select new catProfesion()
                        {
                            profId = s.profId,
                            profNombre = s.profNombre
                        }).ToList();

            ViewData["secId_Data"] = new SelectList(_Sec, "secId", "secNombre");
            ViewData["tipoIdSexo_Data"] = new SelectList(_Sexo, "tipoId", "tipoDescripcion");
            ViewData["tipoIdEstado_Data"] = new SelectList(_Esta, "tipoId", "tipoDescripcion");
            ViewData["motId_Data"] = new SelectList(context.catMotivo, "motId", "motDescripcion");
            ViewData["tipoIdEstadoCivil_Data"] = new SelectList(_EsCi, "tipoId", "tipoDescripcion");
            ViewData["provId_Data"] = new SelectList(context.catProvincia, "provId", "provNombre");
            ViewData["profId_Data"] = new SelectList(_Prof, "profId", "profNombre");
            ViewData["paisId_Data"] = new SelectList(context.catPais, "paisId", "paisNombre");
            ViewData["tipoId_Data"] = new SelectList(_Pare, "tipoId", "tipoDescripcion");
            ViewData["tipoIdTipoDocumento_Data"] = new SelectList(_TDoc, "tipoId", "tipoDescripcion");
            ViewData["espId_Data"] = new SelectList(_Esp, "espId", "espNombre");
            ViewData["ofiId_Data"] = new SelectList(_Ofi, "ofiId", "ofiNombre");
            ViewData["tdId_Data"] = new SelectList(context.catTipoDocumentacion, "tdId", "tdDescripcion");

            //Listas vacias que se agregan solo para el re uso de la clase \\
            ViewData["perId_Data"] = new SelectList(new List<catPersonas>(), "perId", "perId");
            ViewData["categId_Data"] = new SelectList(new List<catPersonas>(), "perId", "perId");
            ViewData["perIdSubRogancia_Data"] = new SelectList(new List<catPersonas>(), "perId", "perId");
        }

        [GridAction]
        public ActionResult _BindingAsistencia(Int64? idPersona)
        {
            idPersona = idPersona == null ? 1 : idPersona;

            if (idPersona == 0)
            {
                return View(new GridModel<Asistencia>
                {
                    Data = new List<Asistencia>()
                });
            }

            using (dbcDatosDataContext _Cronos = new dbcDatosDataContext())
            {
                var _Resultado = (from x in _Cronos.fnCronosAsistenciaDelMes((int)idPersona)
                                 select new Asistencia()
                                 {
                                     Fecha = x.Fecha,
                                     Codigo = x.Codigo,
                                     Estado = x.Estado,
                                     Entrada = x.Entrada,
                                     Salida = x.Salida,
                                     AsistenciaImagen = x.Codigo <= 0 ? "Gris.png" : (x.Codigo == 1 ? "Rojo.png" : (x.Codigo == 2 ? "Verde.png" : "Amarillo.png"))
                                 }).ToList();

                return View(new GridModel<Asistencia>
                {
                    Data = _Resultado
                });
            }
        }

        public ActionResult Exportar(int page, string orderBy, string filter)
        {
            //Get the data representing the current grid state - page, sort and filter
            GridModel model = getDatos().AsQueryable().ToGridModel(page, 9999999, orderBy, string.Empty, filter);
            var _Datos = model.Data.Cast<catPersonas>();

            //Create new Excel workbook
            var workbook = new HSSFWorkbook();

            //Create new Excel sheet
            var sheet = workbook.CreateSheet();
            //sheet.ProtectSheet("AhoraSi");

            //(Optional) set the width of the columns
            sheet.SetColumnWidth(0, 10 * 256);
            sheet.SetColumnWidth(1, 30 * 256);
            sheet.SetColumnWidth(2, 10 * 256);
            sheet.SetColumnWidth(3, 10 * 256);
            sheet.SetColumnWidth(4, 13 * 256);
            sheet.SetColumnWidth(5, 13 * 256);
            sheet.SetColumnWidth(6, 15 * 256);
            sheet.SetColumnWidth(7, 15 * 256);
            sheet.SetColumnWidth(8, 20 * 256);
            sheet.SetColumnWidth(9, 15 * 256);
            sheet.SetColumnWidth(10, 20 * 256);
            sheet.SetColumnWidth(11, 20 * 256);
            sheet.SetColumnWidth(12, 20 * 256);
            sheet.SetColumnWidth(13, 20 * 256);
            sheet.SetColumnWidth(14, 20 * 256);

            //Create a header row
            var headerRow = sheet.CreateRow(0);

            //Set the column names in the header row
            headerRow.CreateCell(0).SetCellValue("Padron");
            headerRow.CreateCell(1).SetCellValue("Apellido y Nombre");
            headerRow.CreateCell(2).SetCellValue("Sexo");
            headerRow.CreateCell(3).SetCellValue("Fecha de Nacimiento");
            headerRow.CreateCell(4).SetCellValue("DNI");
            headerRow.CreateCell(5).SetCellValue("CUIL");
            headerRow.CreateCell(6).SetCellValue("Teléfono");
            headerRow.CreateCell(7).SetCellValue("Celular");
            headerRow.CreateCell(8).SetCellValue("Correo Electrónico");
            headerRow.CreateCell(9).SetCellValue("Antigüedad");
            headerRow.CreateCell(10).SetCellValue("Antigüedad para Pago");
            headerRow.CreateCell(11).SetCellValue("Antigüedad para Vacaciones");
            headerRow.CreateCell(12).SetCellValue("Estado");
            headerRow.CreateCell(13).SetCellValue("Sector");
            //var Celda = headerRow.GetCell(13);
            //Celda.CellStyle.IsLocked = false;
            //Celda.CellStyle.FillBackgroundColor = 0;
            headerRow.CreateCell(14).SetCellValue("Profesión");
            //var OtraCelda = headerRow.GetCell(14);
            //OtraCelda.CellStyle.IsLocked = true;
            //headerRow.GetCell(14).CellStyle.IsLocked = false;
            //(Optional) freeze the header row so it is not scrolled
            sheet.CreateFreezePane(0, 1, 0, 1);
            int rowNumber = 1;

            //Populate the sheet with values from the grid data
            foreach (catPersonas _Info in _Datos)
            {
                //Create a new row
                var row = sheet.CreateRow(rowNumber);

                //Set values for the cells
                row.CreateCell(0).SetCellValue(_Info.perPadron == null ? "" : _Info.perPadron.ToString());
                row.CreateCell(1).SetCellValue(_Info.perApellidoyNombre);
                row.CreateCell(2).SetCellValue(_Info.tipoIdSexoTexto);
                row.CreateCell(3).SetCellValue(_Info.perFechaNacimiento == null ? "" : _Info.perFechaNacimiento.Value.ToString("dd/MM/yyyy"));
                row.CreateCell(4).SetCellValue(_Info.perDNI == null ? "" : _Info.perDNI.ToString());
                row.CreateCell(5).SetCellValue(_Info.perCUIL);
                row.CreateCell(6).SetCellValue(_Info.perTelefono);
                row.CreateCell(7).SetCellValue(_Info.perCelular);
                row.CreateCell(8).SetCellValue(_Info.perEmail);
                row.CreateCell(9).SetCellValue(_Info.perAntiguedad);
                row.CreateCell(10).SetCellValue(_Info.perAntiguedadPago);
                row.CreateCell(11).SetCellValue(_Info.perAntiguedadVacaciones);
                row.CreateCell(12).SetCellValue(_Info.perEstado);
                row.CreateCell(13).SetCellValue(_Info.secNombre);
                row.CreateCell(14).SetCellValue(_Info.profNombre);

                rowNumber++;
            }

            //Write the workbook to a memory stream
            MemoryStream output = new MemoryStream();
            workbook.Write(output);

            string _NombreArchivo = "Empleados-" + DateTime.Now.Date.ToString("yyyyMMddhhmmss") + ".xls";
            //Return the result to the end user

            return File(output.ToArray(),   //The binary data of the XLS file
                "application/vnd.ms-excel", //MIME type of Excel files
                _NombreArchivo);     //Suggested file name in the "Save as" dialog which will be displayed to the end user
        }

        [GridAction]
        public ActionResult _ConsultaDesignaciones(int idPersona)
        {
            var _Datos = (from d in context.getListaDesignaciones(idPersona)
                          select new catCargosCategoriasDesignados()
                          {
                              categId = d.categId,
                              perId = d.perId,
                              carDescripcion = d.carDescripcion,
                              perIdSubRogancia = d.perIdSubRogancia,
                              perNombre = d.perNombre,
                              Sector = new catSectores() { secNombre = d.secNombre, ccCuentaContable = new catCuentasContables() { ccDescripcion = d.ccDescripcion, ccCodigo = d.ccCodigo, ccId = d.ccId } },
                              perSubRoganciaNombre = d.perSubRoganciaNombre,
                              desigId = d.desigId,
                              desigSubRoganciaDesde = d.desigSubRoganciaDesde,
                              desigSubRoganciaHasta = d.desigSubRoganciaHasta,
                              desigVigenciaDesde = d.desigVigenciaDesde,
                              desigVigenciaHasta = d.desigVigenciaHasta,
                              tipoId = d.tipoId,
                              desigTipo = d.desigTipo,
                              desigObservaciones = d.desigObservaciones
                          }).OrderBy(o => o.perNombre).ToList();

            return View(new GridModel(_Datos.AsEnumerable()));
        }
    }
}