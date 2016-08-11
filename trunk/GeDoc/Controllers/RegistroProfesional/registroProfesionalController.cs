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
    using GeDoc.Models.RegistroProfesionales;
    using System.IO;
    using System.Drawing;
    using System.Drawing.Imaging;
    
    public partial class registroProfesionalController : Controller
    {
        private efAccesoADatosEntities context = new efAccesoADatosEntities();

        public ActionResult rpConsultas()
        {
            return PartialView();
        }

        [GridAction]
        public ActionResult getProfesionalConsulta(string contenidoBusqueda)
        {
            cargaInformacionProfesional();
            catProfesionalWS profesional = new catProfesionalWS();

            var profesionales = context.spGetBusquedaProfesional(contenidoBusqueda).Count();// context.catProfesional.Where(i => (i.pNombre + i.pApellido).Contains(contenidoBusqueda)).ToList().Count();

            if (profesionales == 0)
            {
                profesional.pFechaNacimiento = DateTime.Now.AddYears(-21);
                string msj = @"<div style='text-align: center;margin-top: 10%;'><labe1 style='font-size: 20px;color: red;font-weight: bold;'>No se encontro información del Profesional.</labe1></div>'";
                return Content(msj);
            }

            if (profesionales == 1)
            {
                profesional = GetProfesionalByCadena(contenidoBusqueda);
                rpProfesionalConsulta consulta = new rpProfesionalConsulta();
                profesional.modoLectura = true;
                consulta.profesional = profesional;                
                consulta.tramites = new List<regProDetalleTramite>();
                return PartialView("rpProfesionalConsulta", consulta);
            }
            if (profesionales > 1)
            {
                return PartialView("rpConsultaProfesionalGrilla");
            }

            return PartialView();
        }

        public ActionResult Index()
        {
            PopulateDropDownList();
            return PartialView();
        }

        [GridAction]
        public ActionResult _getProfesionalesByDNI(string contenidoBusqueda)
        {
            List<catProfesionalWS> lista = new List<catProfesionalWS>();
            var profesionales = context.spGetBusquedaProfesional(contenidoBusqueda).ToList();
            foreach (var x in profesionales)
            {
                lista.Add(new catProfesionalWS()
                {
                    pNumDocumento = x.dni,
                    pNombre = x.pNombre,
                    pApellido = x.pApellido
                });
            }            
            return View(new GridModel(lista));
        }

        //Datos para el dropdown
        protected void PopulateDropDownList()
        {
            var _TipoDocumento = (from s in context.sisTipo.ToList()
                                  where s.sisTipoEntidad.tipoeCodigo == "TD"
                                  select new sisTipo()
                                  {
                                      tipoId = s.tipoId,
                                      tipoDescripcion = s.tipoDescripcion
                                  }).ToList();
            ViewData["tipoIdTipoDocumento_Data"] = new SelectList(_TipoDocumento, "tipoId", "tipoDescripcion");
        }

        public JsonResult getMenu(string Id)
        {
            var result = (from s in context.catRPTramites.ToList()
                          select new catRPTramites()
                          {
                              traId = s.traId,
                              traDescripcion = s.traDescripcion,
                              traCodigo = s.traCodigo
                          }).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [GridAction]
        public ActionResult getDetalleMenu(int p_rptId)
        {

            regProDetalleTramite tramiteSeleccionado = new regProDetalleTramite() { requerimientosDeTramite = new List<rpItemTramite>() };

            tramiteSeleccionado.tramite = (from x in context.proRegistroProfesionalTramite
                                           where x.rptId == p_rptId
                                           select x).First();

            //requerimiento actuales del sistema, armo las opciones en base a todos ya que pueden crear nuevos despues de que se creo el registro para el usuario
            List<GeDoc.Models.sisGrupoTramite> requerimientostramite = (from x in context.sisGrupoTramite
                                                                        where x.traId == tramiteSeleccionado.tramite.traId
                                                                        select x).ToList();

            //los requerimientos del tramite que fueron presentados 
            List<proRegistroProfesionalTramiteDetalle> requerimientostramiteActual = (from x in context.proRegistroProfesionalTramiteDetalle
                                                                                      where x.rptId == tramiteSeleccionado.tramite.rptId
                                                                                      select x).ToList();

            foreach (var tra in requerimientostramite)
            {
                rpItemTramite item = new rpItemTramite()
                {
                    gtId = tra.gtId,
                    traId = tra.traId,
                    gtDescripcion = tra.gtDescripcion,
                    detalle = new List<rpItemTramiteDetalle>()
                };

                foreach (var traDet in tra.sisGrupoTramiteDetalle)
                {
                    rpItemTramiteDetalle itemDetalle = new rpItemTramiteDetalle()
                    {
                        gtdId = traDet.gtdId,
                        gtdValor = false,
                        gtdDescripcion = traDet.gtdDescripcion
                    };

                    proRegistroProfesionalTramiteDetalle verificoSiPresentoEsteItem = requerimientostramiteActual.Where(i => i.gtdId == itemDetalle.gtdId).FirstOrDefault();
                    if (verificoSiPresentoEsteItem != null)
                    {
                        itemDetalle.gtdValor = verificoSiPresentoEsteItem.rptdValor;
                        itemDetalle.gtdFecha = verificoSiPresentoEsteItem.rptdFecha;
                    }
                    item.detalle.Add(itemDetalle);
                }
                tramiteSeleccionado.requerimientosDeTramite.Add(item);
            }            
            return PartialView("rpMenuDetalle", tramiteSeleccionado);
        }
      
        [GridAction]
        public ActionResult getConsultaTramite(int p_rptId)
        {

            regProDetalleTramite tramiteSeleccionado = new regProDetalleTramite() { requerimientosDeTramite = new List<rpItemTramite>() };

            tramiteSeleccionado.tramite = (from x in context.proRegistroProfesionalTramite
                                           where x.rptId == p_rptId
                                           select x).First();

            //requerimiento actuales del sistema, armo las opciones en base a todos ya que pueden crear nuevos despues de que se creo el registro para el usuario
            List<GeDoc.Models.sisGrupoTramite> requerimientostramite = (from x in context.sisGrupoTramite
                                                                        where x.traId == tramiteSeleccionado.tramite.traId
                                                                        select x).ToList();

            //los requerimientos del tramite que fueron presentados 
            List<proRegistroProfesionalTramiteDetalle> requerimientostramiteActual = (from x in context.proRegistroProfesionalTramiteDetalle
                                                                                      where x.rptId == tramiteSeleccionado.tramite.rptId
                                                                                      select x).ToList();

            foreach (var tra in requerimientostramite)
            {
                rpItemTramite item = new rpItemTramite()
                {
                    gtId = tra.gtId,
                    traId = tra.traId,
                    gtDescripcion = tra.gtDescripcion,
                    detalle = new List<rpItemTramiteDetalle>()
                };

                foreach (var traDet in tra.sisGrupoTramiteDetalle)
                {
                    rpItemTramiteDetalle itemDetalle = new rpItemTramiteDetalle()
                    {
                        gtdId = traDet.gtdId,
                        gtdValor = false,
                        gtdDescripcion = traDet.gtdDescripcion
                    };

                    proRegistroProfesionalTramiteDetalle verificoSiPresentoEsteItem = requerimientostramiteActual.Where(i => i.gtdId == itemDetalle.gtdId).FirstOrDefault();
                    if (verificoSiPresentoEsteItem != null)
                    {
                        itemDetalle.gtdValor = verificoSiPresentoEsteItem.rptdValor;
                        itemDetalle.gtdFecha = verificoSiPresentoEsteItem.rptdFecha;
                    }
                    item.detalle.Add(itemDetalle);
                }
                
                if(tramiteSeleccionado.tramite.catProfesional!=null)
                tramiteSeleccionado.profesionalNombre = tramiteSeleccionado.tramite.catProfesional.pApellido + ", " + tramiteSeleccionado.tramite.catProfesional.pNombre; 
                
                tramiteSeleccionado.requerimientosDeTramite.Add(item);
            }
            return PartialView("rpConsultaTramite", tramiteSeleccionado);
        }

        [GridAction]
        public ActionResult _consultaTramites(int dni)
        {
            //BUSCA TRAMITES
            List<rpConsultaTramite> listaDeTramites = new List<rpConsultaTramite>();
            foreach (var x in (from x in context.proRegistroProfesionalTramite
                               where x.rptDNI == dni
                               select x))
            {                               
                rpConsultaTramite itemTramite = new rpConsultaTramite();
                itemTramite.Id = x.rptId;
                itemTramite.nombreTramite = x.catRPTramites.traDescripcion;
                itemTramite.fechaInicio = x.rptFecha;
                itemTramite.Estado = x.sisTipo.tipoDescripcion;
                itemTramite.fechaActualizacion = x.rptFechaCambioEstado;
                listaDeTramites.Add(itemTramite);
            }
            return View(new GridModel(listaDeTramites));
        }

       

        public void cargaInformacionProfesional() 
        {
            var _TipoDocumento = (from s in context.sisTipo.ToList()
                                  where s.sisTipoEntidad.tipoeCodigo == "TD"
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
            var _Sexo = (from s in context.sisTipo.ToList()
                         where s.sisTipoEntidad.tipoeCodigo == "SX"
                         select new sisTipo()
                         {
                             tipoId = s.tipoId,
                             tipoDescripcion = s.tipoDescripcion
                         }).ToList();

            var _tipoTelefono = (from s in context.sisTipo.ToList()
                         where s.sisTipoEntidad.tipoeCodigo == "TF"
                         select new sisTipo()
                         {
                             tipoId = s.tipoId,
                             tipoDescripcion = s.tipoDescripcion
                         }).ToList();

            var _LN = (from s in context.catLocalidad.ToList()
                       // where s.depId == _dpto.depId
                       orderby s.locId descending
                       select new catLocalidad()
                       {
                           locId = s.locId,
                           locNombre = s.locNombre
                       }).ToList();
         
            ViewData["tipoIdDocumento_Data"] = new SelectList(_TipoDocumento, "tipoId", "tipoDescripcion");
            ViewData["tipoIdSexo_Data"] = new SelectList(_Sexo, "tipoId", "tipoDescripcion");
            ViewData["paisIdNacionalidad_Data"] = ReorderList(context.catPais.ToList(), "paisId", "paisNombre");
            ViewData["paisId_Data"] = ReorderList(context.catPais.ToList(), "paisId", "paisNombre");
            ViewData["provId_Data"] = new SelectList(context.catProvincia, "provId", "provNombre");
            ViewData["depId_Data"] = new SelectList(_DN, "depId", "depNombre");
            ViewData["locId_Data"] = ReorderList(_LN, "locId", "locNombre");
            ViewData["paisIdNacimiento_Data"] = ReorderList(context.catPais.ToList(), "paisId", "paisNombre");
            ViewData["provIdNacimiento_Data"] = new SelectList(context.catProvincia, "provId", "provNombre");
            ViewData["depIdNacimiento_Data"] = new SelectList(_DN, "depId", "depNombre");
            ViewData["locIdNacimiento_Data"] = ReorderList(_LN, "locId", "locNombre");            
            ViewData["tipoIdTelefono1_Data"] = new SelectList(_tipoTelefono, "tipoId", "tipoDescripcion");
            ViewData["tipoIdTelefono2_Data"] = new SelectList(_tipoTelefono, "tipoId", "tipoDescripcion");
            ViewData["tipoIdTelefono3_Data"] = new SelectList(_tipoTelefono, "tipoId", "tipoDescripcion");
            ViewData["tipoIdTelefono4_Data"] = new SelectList(_tipoTelefono, "tipoId", "tipoDescripcion");
       }
     
        private SelectList ReorderList<T>(List<T> listObject, string valueField, string textField)
        {
            var unknownItem = listObject.Single<T>(a => int.Parse(a.GetType().GetProperty(valueField).GetValue(a, null).ToString()) == 0);
            listObject = listObject.Where(a => int.Parse(a.GetType().GetProperty(valueField).GetValue(a, null).ToString()) != 0)
                                   .OrderBy(a => a.GetType().GetProperty(textField).GetValue(a, null)).ToList();
            listObject.Insert(0, unknownItem);
            return new SelectList(listObject, valueField, textField);
        }

        [AcceptVerbs(HttpVerbs.Post)]        
        public ActionResult setProfesionalDetalle(int tipoDocumento, int numDocumento,int tramiteId)
        {
            try
            {
                //selecciono el tramite 
                proRegistroProfesionalTramite tramite = (from x in context.proRegistroProfesionalTramite
                                                         where x.rptDNI == numDocumento && x.traId == tramiteId
                                                         select x).FirstOrDefault();
                if (tramite == null)
                {
                    tramite = new proRegistroProfesionalTramite();
                    tramite.traId = tramiteId;
                    tramite.rptFecha = DateTime.Now;
                    tramite.rptDNI = numDocumento;

                    catProfesional profesional = (from x in context.catProfesional
                                                  where x.pNumDocumento == numDocumento && x.tipoIdDocumento == tipoDocumento
                                                  select x).FirstOrDefault();

                    if (profesional != null)
                    {
                        tramite.catProfesional = profesional;
                        tramite.pId = profesional.pId;
                    }

                    //ASIGNO EL ESTADO ACTIVO
                    tramite.rptEstado = (from z in context.sisTipo
                                         where z.sisTipoEntidad.tipoeCodigo == "RM" && z.tipoCodigo == "ET"
                                         select z).First().tipoId;

                    context.proRegistroProfesionalTramite.AddObject(tramite);
                    context.SaveChanges();
                }
                return Json(new
                {                    
                    Id = tramite.rptId
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Error = true,
                    Mensaje = ex.InnerException == null ? ex.Message : ex.InnerException.Message,
                });
            }
        }
      
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult setProfesionalDetalleTramite(int rptId, string[] items)
        {
            try
            {
                int dni = 0;
                if (items != null)
                {
                    proRegistroProfesionalTramite tramiteEnProcesso = (from x in context.proRegistroProfesionalTramite
                                                                       where x.rptId == rptId
                                                                       select x).FirstOrDefault();


                    //elimino sus items 
                    context.procEliminaRegistroProfesionalTramiteDetalle(rptId);
                    //agrego los ultimos items asociados al tramite
                    foreach (var x in items)
                    {
                        proRegistroProfesionalTramiteDetalle detalle = new proRegistroProfesionalTramiteDetalle();
                        detalle.rptId = rptId;
                        detalle.rptdValor = true;
                        detalle.rptdFecha = DateTime.Now;
                        detalle.gtdId = int.Parse(x.Split('-')[1]);
                        tramiteEnProcesso.proRegistroProfesionalTramiteDetalle.Add(detalle);
                    }

                    context.SaveChanges();
                    dni = tramiteEnProcesso.rptDNI;
                }
                return Json(new
                {
                    dni = dni,
                    Mensaje = "Operación Exitosa.",
                    
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Error = true,
                    Mensaje = ex.InnerException == null ? ex.Message : ex.InnerException.Message,                  
                });
            }
        }

        //usado para buscar todos los paises
        public ActionResult _AjaxGetPais(string text)
        {            
            return new JsonResult { Data = ReorderList(context.catPais.ToList(), "paisId", "paisNombre")};
        }

        #region pais/provincia/dpto/localidad lugar de nacimiento
        //usado para domicilio
        public ActionResult _AjaxGetProvinciasNacimiento(int? paisIdNacimiento)
        {
            var provList = context.catProvincia.Where(i => i.paisId == paisIdNacimiento);
            return new JsonResult { Data = new SelectList(provList, "provId", "provNombre") };

        }
        //usado para nacimiento
        public ActionResult _AjaxGetDepartamentosNacimiento(string provIdNacimiento)
        {

            if (provIdNacimiento.Length <= 0)
                provIdNacimiento = "-1";

            var _DN = (from s in context.catDepartamento.ToList()
                       where s.depId != 0 && s.provId == int.Parse(provIdNacimiento)
                       orderby s.depNombre ascending
                       select new catDepartamento()
                       {
                           depId = s.depId,
                           depNombre = s.depNombre
                       }).ToList();

            // Return all products as JSON - {Text:'ProductName', Value:'ProductID'}
            return new JsonResult { Data = new SelectList(_DN, "depId", "depNombre") };
        }
        //usado para nacimiento
        public ActionResult _AjaxGetLocalidadesNacimiento(string depIdNacimiento)
        {

            if (depIdNacimiento.Length <= 0)
                depIdNacimiento = "-1";

            var _DN = (from s in context.catLocalidad.ToList()
                       where s.depId == int.Parse(depIdNacimiento)
                       orderby s.locNombre ascending
                       select new catLocalidad()
                       {
                           locId = s.locId,
                           locNombre = s.locNombre
                       }).ToList();

            return new JsonResult { Data = new SelectList(_DN, "locId", "locNombre") };
        }
        #endregion

        #region pais/provincia/dpto/localidad domicilio

        public ActionResult _AjaxGetProvincias(int? paisId)
        {                        
            var provList=context.catProvincia.Where(i=>i.paisId == paisId);
            return new JsonResult { Data = new SelectList(provList, "provId", "provNombre") };

        }

        
        public ActionResult _AjaxGetDepartamentos(string provId)
        {

            if (provId.Length <= 0)
                provId = "-1";

                var _DN = (from s in context.catDepartamento.ToList()
                           where s.depId != 0 && s.provId == int.Parse(provId)
                           orderby s.depNombre ascending
                           select new catDepartamento()
                           {
                               depId = s.depId,
                               depNombre = s.depNombre
                           }).ToList();

                // Return all products as JSON - {Text:'ProductName', Value:'ProductID'}
                return new JsonResult { Data = new SelectList(_DN, "depId", "depNombre") };                       
        }

        
        public ActionResult _AjaxGetLocalidades(string depId)
        {

            if (depId != null && depId.Length <= 0)
                depId = "-1";

            var _DN = (from s in context.catLocalidad.ToList()
                       where s.depId == int.Parse(depId)
                       orderby s.locNombre ascending
                       select new catLocalidad()
                       {
                           locId = s.locId,
                           locNombre = s.locNombre
                       }).ToList();
            
            return new JsonResult { Data = new SelectList(_DN, "locId", "locNombre") };
        }

        #endregion


        #region provincia/dpto/ para Titulo

        public ActionResult _AjaxGetProvinciasTitulo(int? paisTituloId)
        {
            var provList = context.catProvincia.Where(i => i.paisId == paisTituloId);
            return new JsonResult { Data = new SelectList(provList, "provId", "provNombre") };

        }


        public ActionResult _AjaxGetDepartamentosTitulo(string provTituloId)
        {

            if (provTituloId.Length <= 0)
                provTituloId = "-1";

            var _DN = (from s in context.catDepartamento.ToList()
                       where s.depId != 0 && s.provId == int.Parse(provTituloId)
                       orderby s.depNombre ascending
                       select new catDepartamento()
                       {
                           depId = s.depId,
                           depNombre = s.depNombre
                       }).ToList();
            
            return new JsonResult { Data = new SelectList(_DN, "depId", "depNombre") };
        }

        #endregion


        #region provincia/dpto/ para ORGANISMO EMISOR

        public ActionResult _AjaxGetProvinciasOrgEmisor(int? paisOEmisor)
        {
            var provList = context.catProvincia.Where(i => i.paisId == paisOEmisor);
            return new JsonResult { Data = new SelectList(provList, "provId", "provNombre") };

        }


        public ActionResult _AjaxGetDepartamentosOrgEmisor(string provOrgEmisorId)
        {

            if (provOrgEmisorId.Length <= 0)
                provOrgEmisorId = "-1";

            var _DN = (from s in context.catDepartamento.ToList()
                       where s.depId != 0 && s.provId == int.Parse(provOrgEmisorId)
                       orderby s.depNombre ascending
                       select new catDepartamento()
                       {
                           depId = s.depId,
                           depNombre = s.depNombre
                       }).ToList();

            return new JsonResult { Data = new SelectList(_DN, "depId", "depNombre") };
        }

        #endregion

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult setProfesional(catProfesionalWS pProfesional)
        {
            try
            {
                bool insert = true;
                catProfesional lprofesion = context.catProfesional.Where(i => i.pId == pProfesional.pId).FirstOrDefault();
                if (lprofesion == null)
                {
                    lprofesion = new catProfesional();
                    lprofesion.pFechaAlta = DateTime.Now;
                }
                else {
                    lprofesion.pFechaActualizacion = DateTime.Now;
                    insert = false;
                }
                lprofesion.pNombre = pProfesional.pNombre;
                lprofesion.pApellido = pProfesional.pApellido;
                lprofesion.tipoIdDocumento = pProfesional.tipoIdDocumento;
                lprofesion.pNumDocumento = pProfesional.pNumDocumento;
                lprofesion.pTieneTelefono = pProfesional.pTieneTelefono;
                lprofesion.tipoIdSexo = pProfesional.tipoIdSexo;
                lprofesion.tipoIdTelefono1 = pProfesional.tipoIdTelefono1;
                lprofesion.tipoIdTelefono2 = pProfesional.tipoIdTelefono2;
                lprofesion.tipoIdTelefono3 = pProfesional.tipoIdTelefono3;
                lprofesion.tipoIdTelefono4 = pProfesional.tipoIdTelefono4;
                lprofesion.pTelefono1 = pProfesional.pTelefono1;
                lprofesion.pTelefono2 = pProfesional.pTelefono2;
                lprofesion.pTelefono3 = pProfesional.pTelefono3;
                lprofesion.pTelefono4 = pProfesional.pTelefono4;
                lprofesion.provId = pProfesional.provId;
                lprofesion.paisId = pProfesional.paisId;
                lprofesion.depId = pProfesional.depId;
                lprofesion.provIdNacimiento = pProfesional.provIdNacimiento;
                lprofesion.paisIdNacimiento = pProfesional.paisIdNacimiento;
                lprofesion.depIdNacimiento = pProfesional.depIdNacimiento;
                lprofesion.locIdNacimiento = pProfesional.locIdNacimiento;
                lprofesion.pNroCalle = pProfesional.pNroCalle;
                lprofesion.pCalle= pProfesional.pCalle;
                lprofesion.pCP = pProfesional.pCP;
                lprofesion.pCuil = pProfesional.pCuil;
                lprofesion.pEmail = pProfesional.pEmail;
                lprofesion.pEmail2 = pProfesional.pEmail2;
                lprofesion.pTieneFirmaDigital = pProfesional.pTieneFirmaDigital;
                lprofesion.pFallecido = pProfesional.pFallecido;
                lprofesion.pFechaFallecimiento = pProfesional.pFechaFallecimiento;
                lprofesion.pFechaNacimiento = pProfesional.pFechaNacimiento;
                lprofesion.pFotoPerfil = pProfesional.pFotoPerfil;
                lprofesion.pObservaciones = pProfesional.pObservaciones;
                lprofesion.paisIdNacionalidad = pProfesional.paisIdNacionalidad;
                lprofesion.pBarrio = pProfesional.pBarrio;
                lprofesion.pPiso = pProfesional.pPiso;
                lprofesion.pNumDpto = pProfesional.pNroDpto;            
                try
                {
                    byte[] data = Convert.FromBase64String(pProfesional.pFotoPerfilCanvas);
                    MemoryStream ms = new MemoryStream(data);
                    Image returnImage = Image.FromStream(ms);
                    string Archivo = Path.Combine(Server.MapPath("~/Content/Fotos/Profesionales"), pProfesional.pNumDocumento + ".png");
                    returnImage.Save(Archivo, ImageFormat.Png);
                    lprofesion.pFotoPerfil = "~/Content/Fotos/Profesionales/"+ pProfesional.pNumDocumento + ".png";
                }
                catch 
                {
                    //lprofesion.pFotoPerfil = "~/Content/Fotos/Profesionales/default.png";
                }

                if (insert)                
                    context.catProfesional.AddObject(lprofesion);

                context.SaveChanges();

               
                List<catProfesionalTitulosWS> listaActual = (List<catProfesionalTitulosWS>)Session["DetalleProfesionalTitulosWS"];
                if (listaActual != null)
                {                    
                    foreach (var x in listaActual)
                    {
                        catProfesion profesion = context.catProfesion.Where(i => i.profNombre == x.profNombre).FirstOrDefault();
                        catOrganismoEmisor orgEmisor = context.catOrganismoEmisor.Where(i => i.oeNombre == x.oeNombre).FirstOrDefault();

                        catTitulo titulo = context.catTitulo.Where(i => i.titDenominacion == x.ptitTitulo).FirstOrDefault();
                        if (titulo == null)
                        {
                            titulo = new catTitulo() { titDenominacion = x.ptitTitulo };
                            context.catTitulo.AddObject(titulo);
                            context.SaveChanges();
                        }
                        catProfesionalTitulo pTitulo = new catProfesionalTitulo()
                        {
                            titId = titulo.titId,
                            paisId = x.paisTituloId,
                            depId = x.depTituloId,
                            profId = profesion.profId,
                            provId = x.provTituloId,
                            ptitObservacionMatricula = x.ptitMatriculaObservacion,
                            ptitObservacionDiploma = x.ptitObservacionDiploma,
                            ptitMarticulaNro = x.ptitMarticulaNro,
                            ptitMatriculaFolio = x.ptitMatriculaFolio,
                            ptitFechaDiploma = x.ptitFechaDiploma,
                            ptitMatriculaFechaInscr = x.ptitMatriculaFechaInscr,
                            oeId = orgEmisor.oeId,
                            ptitMatriculaLibro = x.ptitMatriculaLibro,
                            pId = lprofesion.pId,
                        };
                        context.catProfesionalTitulo.AddObject(pTitulo);
                    }
                    context.spEliminaProfesionalTitulos(lprofesion.pId);
                    context.SaveChanges();
                }

                return Json(new
                {
                    Id = lprofesion.pId,
                    Error = false
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Error = true,
                    Mensaje = ex.InnerException == null ? ex.Message : ex.InnerException.Message,
                });
            }
        }
       
        public catProfesionalWS GetProfesionalByDNI(int tipoDocumento, int numDocumento)
        {

            catProfesionalWS lprofesion = new catProfesionalWS();
            catProfesional pProfesional = context.catProfesional.Where(i => i.pNumDocumento == numDocumento && i.tipoIdDocumento == tipoDocumento).FirstOrDefault();
            if (pProfesional != null)
            {
                lprofesion.pNombre = pProfesional.pNombre;
                lprofesion.pId = pProfesional.pId;
                lprofesion.pApellido = pProfesional.pApellido;
                lprofesion.tipoIdDocumento = (short)pProfesional.tipoIdDocumento;
                lprofesion.pNumDocumento = pProfesional.pNumDocumento;
                lprofesion.pTieneTelefono = pProfesional.pTieneTelefono;
                lprofesion.tipoIdSexo = pProfesional.tipoIdSexo;
                lprofesion.tipoIdTelefono1 = pProfesional.tipoIdTelefono1;
                lprofesion.tipoIdTelefono2 = pProfesional.tipoIdTelefono2;
                lprofesion.tipoIdTelefono3 = pProfesional.tipoIdTelefono3;
                lprofesion.tipoIdTelefono4 = pProfesional.tipoIdTelefono4;
                lprofesion.pTelefono1 = pProfesional.pTelefono1;
                lprofesion.pTelefono2 = pProfesional.pTelefono2;
                lprofesion.pTelefono3 = pProfesional.pTelefono3;
                lprofesion.pTelefono4 = pProfesional.pTelefono4;
                lprofesion.provId = pProfesional.provId;
                lprofesion.paisId = pProfesional.paisId;
                lprofesion.depId = pProfesional.depId;
                lprofesion.locId = pProfesional.locId;
                lprofesion.provIdNacimiento = pProfesional.provIdNacimiento;
                lprofesion.paisIdNacimiento = pProfesional.paisIdNacimiento;
                lprofesion.depIdNacimiento = pProfesional.depIdNacimiento;
                lprofesion.locIdNacimiento = pProfesional.locIdNacimiento;
                lprofesion.pFallecido = pProfesional.pFallecido == null ? false : pProfesional.pFallecido.Value;
                lprofesion.pNroCalle = pProfesional.pNroCalle;
                lprofesion.pCalle = pProfesional.pCalle;
                lprofesion.pCP = pProfesional.pCP;
                lprofesion.pCuil = pProfesional.pCuil;
                lprofesion.pEmail = pProfesional.pEmail;
                lprofesion.pEmail2 = pProfesional.pEmail2;
                lprofesion.pTieneFirmaDigital = pProfesional.pTieneFirmaDigital == null ? false : true;
                lprofesion.pFechaFallecimiento = pProfesional.pFechaFallecimiento;
                lprofesion.pFechaNacimiento = pProfesional.pFechaNacimiento;
                lprofesion.pFotoPerfil = pProfesional.pFotoPerfil;
                lprofesion.pObservaciones = pProfesional.pObservaciones;
                lprofesion.paisIdNacionalidad = pProfesional.paisIdNacionalidad;
                lprofesion.pBarrio = pProfesional.pBarrio;
                lprofesion.pPiso = pProfesional.pPiso;
                lprofesion.pNroDpto = pProfesional.pNumDpto;
            }

            return lprofesion;
        }


        public catProfesionalWS GetProfesionalByCadena(string contenidoBusqueda)
        {

            catProfesionalWS lprofesion = new catProfesionalWS();
            var spProfesional = context.spGetBusquedaProfesional(contenidoBusqueda).FirstOrDefault();
            catProfesional pProfesional = context.catProfesional.Where(i => i.pId == spProfesional.pid).FirstOrDefault();
            if (pProfesional != null)
            {
                lprofesion.pNombre = pProfesional.pNombre;
                lprofesion.pId = pProfesional.pId;
                lprofesion.pApellido = pProfesional.pApellido;
                lprofesion.tipoIdDocumento = (short)pProfesional.tipoIdDocumento;
                lprofesion.pNumDocumento = pProfesional.pNumDocumento;
                lprofesion.pTieneTelefono = pProfesional.pTieneTelefono;
                lprofesion.tipoIdSexo = pProfesional.tipoIdSexo;
                lprofesion.tipoIdTelefono1 = pProfesional.tipoIdTelefono1;
                lprofesion.tipoIdTelefono2 = pProfesional.tipoIdTelefono2;
                lprofesion.tipoIdTelefono3 = pProfesional.tipoIdTelefono3;
                lprofesion.tipoIdTelefono4 = pProfesional.tipoIdTelefono4;
                lprofesion.pTelefono1 = pProfesional.pTelefono1;
                lprofesion.pTelefono2 = pProfesional.pTelefono2;
                lprofesion.pTelefono3 = pProfesional.pTelefono3;
                lprofesion.pTelefono4 = pProfesional.pTelefono4;
                lprofesion.provId = pProfesional.provId;
                lprofesion.paisId = pProfesional.paisId;
                lprofesion.depId = pProfesional.depId;
                lprofesion.locId = pProfesional.locId;
                lprofesion.provIdNacimiento = pProfesional.provIdNacimiento;
                lprofesion.paisIdNacimiento = pProfesional.paisIdNacimiento;
                lprofesion.depIdNacimiento = pProfesional.depIdNacimiento;
                lprofesion.locIdNacimiento = pProfesional.locIdNacimiento;
                lprofesion.pTieneFirmaDigital = pProfesional.pTieneFirmaDigital==null?false:true;
                lprofesion.pNroCalle = pProfesional.pNroCalle;
                lprofesion.pCalle = pProfesional.pCalle;
                lprofesion.pCP = pProfesional.pCP;
                lprofesion.pCuil = pProfesional.pCuil;
                lprofesion.pEmail = pProfesional.pEmail;
                lprofesion.pEmail2 = pProfesional.pEmail2;
                lprofesion.pFallecido = pProfesional.pFallecido == null ? false : pProfesional.pFallecido.Value;
                lprofesion.pFechaFallecimiento = pProfesional.pFechaFallecimiento;
                lprofesion.pFechaNacimiento = pProfesional.pFechaNacimiento;
                lprofesion.pFotoPerfil = pProfesional.pFotoPerfil;
                lprofesion.pObservaciones = pProfesional.pObservaciones;
                lprofesion.paisIdNacionalidad = pProfesional.paisIdNacionalidad;
                lprofesion.paisNacionalidad = pProfesional.paisIdNacionalidad == null ? "" : context.catPais.Where(i => i.paisId == pProfesional.paisIdNacionalidad.Value).FirstOrDefault().paisNombre;
                lprofesion.pBarrio = pProfesional.pBarrio;
                lprofesion.pPiso = pProfesional.pPiso;
                lprofesion.pNroDpto = pProfesional.pNumDpto;
            }

            return lprofesion;
        }

        [GridAction]
        public ActionResult getProfesionalDetalle(string accion)
        {
            PopulateDropDownList();
            return PartialView("rpBusquedaProfesionaByDNI", accion);
        }

        //muestra todos los datos del profesional
        [GridAction]
        public ActionResult getProfesionalDetalleCompleto(int tipoDocumento, int numDocumento)
        {
            cargaInformacionProfesional();
            catProfesionalWS profesional = new catProfesionalWS();
            profesional.modoLectura = false;
            profesional = GetProfesionalByDNI(tipoDocumento,numDocumento);
            profesional.pNumDocumento = numDocumento;
            if (profesional.pId == 0)
                profesional.pFechaNacimiento = DateTime.Now.AddYears(-21);
            return PartialView("rpProfesionalDatosCompleto", profesional);
        }

        [GridAction]
        public ActionResult _profesionalTitulos(int pId)
        {
            List<catProfesionalTitulosWS> list = (List<catProfesionalTitulosWS>)Session["DetalleProfesionalTitulosWS"];
            bool listaIniciada = (bool) Session["IniciandoDetalleProfesionalTitulosWS"];
            if (list.Count == 0 && listaIniciada==true)
            {
                foreach (var x in context.catProfesionalTitulo.Where(i => i.pId == pId).ToList())
                {
                    list.Add(new catProfesionalTitulosWS()
                    {
                        depTituloId = x.depId,
                        provTituloId = x.provId,
                        paisTituloId = x.paisId,
                        oeNombre = x.catOrganismoEmisor.oeNombre,
                        pId = x.pId,                        
                        profId = x.profId,
                        ptitFechaDiploma = x.ptitFechaDiploma,
                        ptitId = x.ptitId,
                        ptitMarticulaNro = x.ptitMarticulaNro,
                        ptitMatriculaFechaInscr = x.ptitMatriculaFechaInscr,
                        ptitMatriculaFolio = x.ptitMatriculaFolio,
                        ptitMatriculaLibro = x.ptitMatriculaLibro,
                        ptitMatriculaObservacion = x.ptitObservacionMatricula,
                        ptitObservacionDiploma = x.ptitObservacionDiploma,
                        ptitOrganismoEmisor = x.catOrganismoEmisor.oeNombre,
                        profNombre = x.catProfesion.profNombre,
                        ptitProfesion = x.catProfesion.profNombre,
                        ptitTitulo = x.catTitulo.titDenominacion,
                        //titId = x.titId
                    });
                }
                Session["IniciandoDetalleProfesionalTitulosWS"] = false;
            }
            return View(new GridModel(list));
        }      

        [GridAction]
        public ActionResult rpProfesionalTitulos(int pId, bool modoLectura)
        {
            Session["IniciandoDetalleProfesionalTitulosWS"] = true;
            Session["DetalleProfesionalTitulosWS"] = new List<catProfesionalTitulosWS>();
            catProfesionalWS profesional = new catProfesionalWS() { pId=pId };
            profesional.modoLectura = modoLectura;
            return View(profesional);
        }
         [GridAction]
         public ActionResult getProfesionalTitulo(string pAccion, Int64? ptitId)
         {
             catProfesionalTitulosWS lTitulo = new catProfesionalTitulosWS();
             lTitulo.ptitFechaDiploma = DateTime.Now;
             lTitulo.ptitMatriculaFechaInscr = DateTime.Now;
             ViewData["ptitTitulo_Data"] = context.catTitulo.Select(s => s.titDenominacion);
             ViewData["profNombre_Data"] = context.catProfesion.Select(s => s.profNombre);
             ViewData["oeNombre_Data"] = context.catOrganismoEmisor.Select(s => s.oeNombre);
             ViewData["profNombre_OnChange"] = "profNombre_OnChange";
             if (pAccion != "Agregar")
             {
                 lTitulo = (from x in (List<catProfesionalTitulosWS>)Session["DetalleProfesionalTitulosWS"]
                            where x.ptitId == ptitId
                            select new catProfesionalTitulosWS()
                            {
                                 oeNombre = x.ptitOrganismoEmisor,
                                ptitObservacionDiploma = x.ptitObservacionDiploma,
                                ptitMatriculaObservacion = x.ptitMatriculaObservacion,
                                ptitMatriculaFolio = x.ptitMatriculaFolio,
                                ptitMatriculaLibro = x.ptitMatriculaLibro,
                                ptitFechaDiploma = x.ptitFechaDiploma,
                                ptitMarticulaNro = x.ptitMarticulaNro,
                                ptitId = x.ptitId,
                                profId = x.profId,
                                paisTituloId = x.paisTituloId,
                                 profNombre=x.profNombre,
                                provTituloId = x.provTituloId,
                                depTituloId = x.depTituloId,
                                ptitMatriculaFechaInscr = x.ptitMatriculaFechaInscr,
                                ptitOrganismoEmisor = x.ptitOrganismoEmisor,
                                ptitProfesion = x.ptitProfesion,
                                ptitTitulo = x.ptitTitulo,
                                pAccion = pAccion
                            }).FirstOrDefault();
             }


             return PartialView("CRUDTitulo", lTitulo);
         }

         [AcceptVerbs(HttpVerbs.Post)]
         public ActionResult setProfesionalTitulo(catProfesionalTitulosWS pTitulo)
         {
             try
             {
                 List<catProfesionalTitulosWS> listaActual = (List<catProfesionalTitulosWS>)Session["DetalleProfesionalTitulosWS"];
                 catProfesionalTitulosWS tituloEnLista = listaActual.Where(i => i.ptitId == pTitulo.ptitId && pTitulo.pAccion!="Agregar").FirstOrDefault();
                 if (pTitulo.pAccion != "Eliminar")
                 {
                     catOrganismoEmisor emison = context.catOrganismoEmisor.Where(i => i.oeNombre == pTitulo.oeNombre).FirstOrDefault();
                     catProfesion profesion = context.catProfesion.Where(i => i.profNombre == pTitulo.profNombre).FirstOrDefault();

                     if (profesion == null)
                     {
                         return Json(new
                         {
                             Id = 0,
                             Mensaje = "La Profesion ingresada no existe en el sistema.",
                             Error = true
                         });
                     }
                    
                     if (emison == null)
                     {
                         return Json(new
                            {
                                Id = 0,
                                Mensaje = "El organismo emisor ingresado no existe en el sistema.",
                                Error = true
                            });
                     }
                     pTitulo.ptitTitulo = pTitulo.ptitTitulo;
                     pTitulo.ptitProfesion = profesion.profNombre;
                     pTitulo.ptitOrganismoEmisor = emison.oeNombre;
                     if (tituloEnLista != null)
                         listaActual.Remove(tituloEnLista);
                     else
                     { 
                         pTitulo.ptitId = listaActual.Count > 0 ? listaActual.Max(i => i.ptitId) + 1 : 1;
                     }

                     listaActual.Add(pTitulo);
                 }
                 else
                 {
                     listaActual.Remove(tituloEnLista);
                 }
                 Session["DetalleProfesionalTitulosWS"] = listaActual;
                 return Json(new
                 {
                     Id = 0,                     
                     Error = false
                 });
             }
             catch (Exception ex)
             {
                 return Json(new
                 {
                     Error = true,                     
                     Mensaje = ex.InnerException == null ? ex.Message : ex.InnerException.Message,
                 });
             }
         }

        private IList<catProfesionalTitulosWS> getDetalleProfesionalTitulosWS()
        {
            return (IList<catProfesionalTitulosWS>)Session["DetalleProfesionalTitulosWS"];
        }

        [GridAction]
        public ActionResult existeProfesion(string pProfesion)
        {
                       
            try
            {
                // TODO: Add update logic here
                if(context.catProfesion.Where(i=>i.profNombre==pProfesion).Count()>0)
                    return Json(new { success = true, existe = true },JsonRequestBehavior.AllowGet);
                else return Json(new { success = true, existe = false }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { success = true, existe = false }, JsonRequestBehavior.AllowGet); 
            }
        }
        [GridAction]
        public ActionResult existeOrganismoEmisor(string pTexto)
        {
            try
            {
                // TODO: Add update logic here
                if (context.catOrganismoEmisor.Where(i => i.oeNombre == pTexto).Count() > 0)
                    return Json(new { success = true, existe = true }, JsonRequestBehavior.AllowGet);
                else return Json(new { success = true, existe = false }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { success = true, existe = false }, JsonRequestBehavior.AllowGet);
            }
        }
        [GridAction]
        public ActionResult CRUDProfesion()
        {
            catProfesionWS lProfesion = new catProfesionWS();
            ViewData["tfId_Data"] = new SelectList(context.catTipoFormacion, "tfId", "tdDenominacion");            
            return PartialView("CRUDProfesion", lProfesion);
        }
        [GridAction]
        public ActionResult CRUDOrganismoEmisor()
        {
            catOrganismoEmisorWS lOrgEmisor = new catOrganismoEmisorWS();
            return PartialView("CRUDOrganismoEmisor", lOrgEmisor);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult setOrganismoEmisor(catOrganismoEmisorWS pOrgEmisor)
        {
            try
            {
                if (context.catOrganismoEmisor.Where(i => i.oeNombre == pOrgEmisor.oEmisorNombre).Count() == 0)
                {
                    catOrganismoEmisor item = new catOrganismoEmisor()
                    {
                        oeNombre = pOrgEmisor.oEmisorNombre,
                        provId = pOrgEmisor.provOrgEmisorId,
                        paisId = pOrgEmisor.paisOrgEmisorId,
                        depId = pOrgEmisor.depOrgEmisorId,
                        oeSISA = pOrgEmisor.oeSISA
                    };
                    context.catOrganismoEmisor.AddObject(item);
                    context.SaveChanges();
                   
                    return Json(new
                    {
                        Id = item.oeId,
                        oeNombre = pOrgEmisor.oEmisorNombre,
                        Error = false
                    });
                }
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Error = true,
                    Mensaje = ex.InnerException == null ? ex.Message : ex.InnerException.Message,
                });
            }
            return View();
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult setProfesion(catProfesionWS pProfesional)
        {
            try
            {
                if (context.catProfesion.Where(i => i.profNombre == pProfesional.profesionNombre).Count() == 0)
                {
                    catProfesion item = new catProfesion()
                    {
                        profNombre = pProfesional.profesionNombre,                        
                        tfId = pProfesional.tfId,
                         profSISA = pProfesional.profSISA
                    };
                    context.catProfesion.AddObject(item);
                    context.SaveChanges();
                    ViewData["profNombre_Data"] = context.catProfesion.Select(s => s.profNombre);
                    return Json(new
                    {
                        Id = item.profId,
                        profNombre  = pProfesional.profesionNombre,
                        Error = false
                    });
                }
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Error = true,
                    Mensaje = ex.InnerException == null ? ex.Message : ex.InnerException.Message,
                });
            }
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult retiraMatricula(int p_rptId)
        {
            try
            {
                 proRegistroProfesionalTramite tramite = (from x in context.proRegistroProfesionalTramite
                                               where x.rptId == p_rptId
                                               select x).First();

                 (from s in context.sisTipo.ToList()
                                  where s.sisTipoEntidad.tipoeCodigo == "TD"
                                  select new sisTipo()
                                  {
                                      tipoId = s.tipoId,
                                      tipoDescripcion = s.tipoDescripcion
                                  }).ToList();

                 tramite.rptEstado = (from z in context.sisTipo
                                      where z.sisTipoEntidad.tipoeCodigo == "RM" && z.tipoCodigo == "RT"
                                      select z).First().tipoId;

                 tramite.rptFechaCambioEstado = DateTime.Now;

                 context.SaveChanges();

                return Json(new
                {
                    Id = 0,
                    Error = false
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Error = true,
                    Mensaje = ex.InnerException == null ? ex.Message : ex.InnerException.Message,
                });
            }           
        }



        #region Profesional Credencial
        [GridAction]
        public ActionResult rpCredencialBusqueda()
        {
            var _TipoDocumento = (from s in context.sisTipo.ToList()
                                  where s.sisTipoEntidad.tipoeCodigo == "TD"
                                  select new sisTipo()
                                  {
                                      tipoId = s.tipoId,
                                      tipoDescripcion = s.tipoDescripcion
                                  }).ToList();
            ViewData["tipoIdDocumento_Data"] = new SelectList(_TipoDocumento, "tipoId", "tipoDescripcion");

            return View();
        }


        [GridAction]
        public ActionResult getCredencialProfesional(string contenidoBusqueda, int profId)
        {
            catProfesionalWS profesional = new catProfesionalWS();
            var profesionales = context.spGetBusquedaProfesional(contenidoBusqueda).Count();
            if (profesionales == 0)
            {                
                string msj = @"<div style='text-align: center;margin-top: 10%;'><labe1 style='font-size: 20px;color: red;font-weight: bold;'>No se encontro información del Profesional.</labe1></div>'";
                return Content(msj);
            }

            if (profesionales == 1)
            {
                profesional = GetProfesionalByCadena(contenidoBusqueda);
                rpProfesionalConsulta consulta = new rpProfesionalConsulta();
                consulta.tramites = new List<regProDetalleTramite>();
                profesional.modoLectura = true;
                consulta.profesional = profesional;

                catProfesionalTitulo titulo = null;
                if (profId != 0)
                    titulo = context.catProfesionalTitulo.Where(i => i.pId == profesional.pId && i.profId == profId).FirstOrDefault();
                else
                    titulo = context.catProfesionalTitulo.Where(i => i.pId == profesional.pId).FirstOrDefault();

                if (titulo == null)
                {
                    string msj = @"<div style='text-align: center;margin-top: 10%;'><labe1 style='font-size: 20px;color: red;font-weight: bold;'>No se encontro matriculas asociadas al Profesional.</labe1></div>'";
                    return Content(msj);
                }
                else
                {
                    consulta.titulo = new catProfesionalTitulosWS()
                    {
                        profNombre = titulo.catProfesion.profNombre,
                        ptitMarticulaNro = titulo.ptitMarticulaNro
                    };
                    return PartialView("rpCredencialProfesional", consulta);
                }
            }

            return PartialView();
        }


        //usado para domicilio
        public ActionResult _AjaxGetTitulosProfesional(int? pId)
        {
            catProfesional profesional = context.catProfesional.Where(i => i.pNumDocumento == pId).FirstOrDefault();
            List<catProfesionalTitulosWS> profesionesDelProfesional = new List<catProfesionalTitulosWS>();
            if (profesional!=null)
            {
                profesionesDelProfesional = (from x in context.catProfesionalTitulo.Where(i => i.pId == profesional.pId)
                                             select new catProfesionalTitulosWS()
                                               {
                                                   profId = x.profId,
                                                   profNombre = x.catProfesion.profNombre
                                               }).ToList();
            }
            return new JsonResult { Data = new SelectList(profesionesDelProfesional, "profId", "profNombre") };
        }


        #endregion
    }
}

