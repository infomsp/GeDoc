using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Script.Serialization;
using GeDoc.Models;
using GeDoc.Models.JuntaMedica.Modelo;
using Microsoft.SqlServer.Server;
using NPOI.HSSF.UserModel;
using Telerik.Web.Mvc.Extensions;

namespace GeDoc
{
    using System.Linq;
    using System.Web.Mvc;
    using Telerik.Web.Mvc;

    public class JuntaMedicaController : Controller
    {
        private efAccesoADatosJMJuntaMedicaEntities context = new efAccesoADatosJMJuntaMedicaEntities();

        //Edición de datos

        [GridAction]
        public ActionResult getListadoDeCartasARecibir(string textoBuscado)
        {
            var _Datos = context.getListaCMPorEstados("#PE#EM#", textoBuscado).ToList();
            Session["ListaCMRecibir"] = _Datos;

            return PartialView(new GridModel(_Datos));
        }

        [GridAction]
        public ActionResult getListadoDeCartasABuscar(string textoBuscado)
        {
            var TieneJM = Session["ParaJuntaMedica"].ToString();
            var _Datos = (from x in context.getListaCMPorEstados("#ES#", textoBuscado)
                          where x.TieneJM == TieneJM
                          orderby x.prioridad, x.cmedOrdenRecepcion
                          select new getListaCMPorEstadosWS()
                          {
                              agtApellidoNombre = x.agtApellidoNombre,
                              agtDNI = x.agtDNI,
                              agtId = x.agtId,
                              cmedFecha = x.cmedFecha,
                              cmedId = x.cmedId,
                              cmedMotivoSolicitud = x.cmedMotivoSolicitud,
                              cmedNumero = x.cmedNumero,
                              cmedObservaciones = x.cmedObservaciones,
                              cmedOrdenRecepcion = x.cmedOrdenRecepcion,
                              EstadoCM = x.EstadoCM,
                              espId = x.espId,
                              espNombre = x.espNombre,
                              estEstado = x.estEstado,
                              estFecha = x.estFecha,
                              estHora = x.estHora,
                              usrId = x.usrId,
                              prioridad = x.prioridad,
                              Seleccionado = false,
                              sucId = x.sucId,
                              Ministerios = x.Ministerios,
                              agtLugarTrabajo = x.agtLugarTrabajo,
                              Reparticiones = x.Reparticiones
                          }
                          ).ToList();

            Session["ListaCMBuscar"] = _Datos;

            return PartialView(new GridModel(_Datos));
        }

        [GridAction]
        public ActionResult getListadoDeCartasAGestionCM(string textoBuscado, string Filtro)
        {
            var Estados = textoBuscado == "" ? "#AA#AC#AM#CD#EM#ES#LA#AL#NA#PE#" : "#AL#NA#AA#AC#AD#AM#AN#CA#CD#CN#EL#EM#ES#LA#NV#PE#";
            var _Datos = context.getListaCMGestion(Estados, textoBuscado, (Session["Usuario"] as sisUsuario).usrNombreDeUsuario).ToList();
            Session["ListaCMGestionCM"] = _Datos;

            return PartialView(new GridModel(_Datos));
        }

        [GridAction]
        public ActionResult getListadoDeCartasGestionCMEspecial(int minId, DateTime FechaDesde, DateTime FechaHasta, string TipoFecha)
        {
            var _Datos = context.getListaCMGestionFiltroEspecial(minId, FechaDesde, FechaHasta, TipoFecha).ToList();
            Session["ListaCMGestionCM"] = _Datos;

            return PartialView(new GridModel(_Datos));
        }

        [GridAction]
        public ActionResult getListadoDeTrunosJM()
        {
            var _Datos = (from x in context.getListadoJuntasMedicas()
                          orderby x.turFecha
                          select new getListadoJuntasMedicas_Result()
                          {
                              agtApellidoYNombre = x.agtApellidoYNombre,
                              agtDNI = x.agtDNI,
                              agtId = x.agtId,
                              usrId = x.usrId,
                              turId = x.turId,
			                  turFecha = x.turFecha,
			                  tjId = x.tjId,
			                  tjDescripcion = x.tjDescripcion,
			                  agtCUIL = x.agtCUIL,
			                  turResultadoJM  = x.turResultadoJM,
			                  turEstado = x.turEstado,
			                  tipoDescripcion = x.tipoDescripcion,
			                  turEstadoFecha = x.turEstadoFecha,
			                  usrUsuario = x.usrUsuario,
			                  usrNombre = x.usrNombre,
			                  usrApellido = x.usrApellido,
			                  turEsSobreTurno = x.turEsSobreTurno,
                              CantidadEspecialidades = x.CantidadEspecialidades,
                              CantidadMedicos = x.CantidadMedicos,
                              Medicos = x.Medicos,
                              Especialidades = x.Especialidades
                          }
                          ).ToList();

            return PartialView(new GridModel(_Datos));
        }

        public ActionResult getDatosMedicosJuntaMedica(int turId, string MedicosTurnos)
        {
            var Datos = new MedicosTurnoJMWS();

            Datos.Medicos = (from x in context.getListaMedicos().ToList()
                         select new ListaDesplegableWS()
                         {
                             idLista = x.medId,
                             Texto = x.medApellidoyNombre
                         }).ToList();
            Datos.TurnosMedicos = MedicosTurnos;

            return PartialView("AdministracionJuntasMedicasCRUDMedicos", Datos);
        }

        [HttpPost]
        public ActionResult getDatosJuntaMedica(string Item)
        {
            var jss = new JavaScriptSerializer();
            var Datos = jss.Deserialize<getListadoJuntasMedicas_Result>(Item);

            return PartialView("AdministracionJuntasMedicasAnulacion", Datos);
        }

        public ActionResult Recepcion()
        {
            ViewBag.Catalogo = "Recepción de Cartas Médicas";

            return PartialView();
        }

        public ActionResult GestionCM()
        {
            ViewBag.Catalogo = "Gestión de Cartas Médicas";

            return PartialView();
        }

        public ActionResult GestionCMEspecial()
        {
            ViewBag.Catalogo = "Consulta de Cartas Médicas";

            ViewData["minId_Lista_Data"] = (from x in context.getListaDesplegableMinisterios().ToList()
                                            select new ListaDesplegableWS()
                                            {
                                                idLista = x.minId,
                                                Texto = x.minNombre
                                            }).ToList();

            return PartialView();
        }

        public ActionResult BusquedaCM()
        {
            Session["ParaJuntaMedica"] = "NO";
            ViewBag.Catalogo = "Recepción de Cartas Médicas";

            return PartialView();
        }

        public ActionResult BusquedaCMJuntaMedica()
        {
            Session["ParaJuntaMedica"] = "SI";
            ViewBag.Catalogo = "Búsqueda de Cartas Médicas asociadas a una Junta Médica";

            return PartialView();
        }

        public ActionResult AdministracionJuntasMedicas()
        {
            ViewBag.Catalogo = "Administración de Juntas Médicas";

            ViewData["medId_Lista_Data"] = (from x in context.getListaMedicos().ToList()
                                             select new ListaDesplegableWS()
                                             {
                                                 idLista = x.medId,
                                                 Texto = x.medApellidoyNombre
                                             }).ToList();
            return PartialView();
        }

        [GridAction]
        public ActionResult getDatosCM(Int64? cmedId)
        {
            var datosCM = new gesCartaMedica();

            datosCM.DatosCM = (Session["ListaCMRecibir"] as List<getListaCMPorEstados_Result>).First(f => f.cmedId == cmedId);
            datosCM.DatosAgente = context.getDatosAgente(datosCM.DatosCM.agtId).First();
            datosCM.ListaGrupoFamiliar = context.getDatosAgenteGrupoFamiliar(datosCM.DatosCM.agtId).ToList();
            datosCM.ListaReparticiones = context.getDatosAgenteReparticiones(datosCM.DatosCM.agtId).ToList();

            datosCM.DatosAgente.agtFechaIngreso = datosCM.DatosAgente.agtFechaIngreso.Date;
            datosCM.DatosAgente.agtFechaNacimiento = datosCM.DatosAgente.agtFechaNacimiento.Date;
            datosCM.espId = datosCM.DatosCM.espId;

            ViewData["espId_Data"] = new SelectList(context.getListaEspecialidades().ToList(), "espId", "espNombre");

            return PartialView("RecepcionCRUD", datosCM);
        }

        [GridAction]
        public ActionResult getDatosGestionCM(Int64? cmedId, string pAccion)
        {
            var datosCM = new gesCartaMedica();

            if (pAccion != "Agregar")
            {
                var InfoCM = (Session["ListaCMGestionCM"] as List<getListaCMGestion_Result>).First(f => f.cmedId == cmedId);
                datosCM.DatosCM = new getListaCMPorEstados_Result()
                {
                    cmedId = InfoCM.cmedId,
                    cmedMotivoSolicitud = InfoCM.cmedMotivoSolicitud,
                    cmedOrdenRecepcion = InfoCM.cmedOrdenRecepcion,
                    cmedObservaciones = InfoCM.cmedObservaciones,
                    cmedNumero = InfoCM.cmedNumero,
                    cmedFecha = InfoCM.cmedFecha,
                    EstadoCM = InfoCM.EstadoCM,
                    agtLugarTrabajo = InfoCM.agtLugarTrabajo,
                    agtId = InfoCM.agtId,
                    agtDNI = InfoCM.agtDNI,
                    agtApellidoNombre = InfoCM.agtApellidoNombre,
                    estFecha = InfoCM.estFecha,
                    espNombre = InfoCM.espNombre,
                    estHora = InfoCM.estHora,
                    estEstado = InfoCM.estEstado,
                    espId = InfoCM.espId,
                    licFechaFinal = InfoCM.licFechaFinal,
                    licFechaInicial = InfoCM.licFechaInicial,
                    Reparticiones = InfoCM.Reparticiones,
                    usrId = InfoCM.usrId,
                    sucId = InfoCM.sucId,
                    prioridad = InfoCM.prioridad,
                    Ministerios = InfoCM.Ministerios
                };
                datosCM.DatosAgente = context.getDatosAgente(datosCM.DatosCM.agtId).First();
                datosCM.ListaGrupoFamiliar = context.getDatosAgenteGrupoFamiliar(datosCM.DatosCM.agtId).ToList();
                datosCM.ListaReparticiones = context.getDatosAgenteReparticiones(datosCM.DatosCM.agtId).ToList();

                datosCM.DatosAgente.agtFechaIngreso = datosCM.DatosAgente.agtFechaIngreso.Date;
                datosCM.DatosAgente.agtFechaNacimiento = datosCM.DatosAgente.agtFechaNacimiento.Date;
                datosCM.espId = datosCM.DatosCM.espId;
            }
            else
            {
                datosCM.DatosAgente = new getDatosAgente_Result();
                datosCM.DatosAgente.agtFechaIngreso = datosCM.DatosAgente.agtFechaIngreso.Date;
                datosCM.DatosAgente.agtFechaNacimiento = datosCM.DatosAgente.agtFechaNacimiento.Date;
                datosCM.espId = 0;
                datosCM.gesAccion = pAccion;
            }

            ViewData["espId_Data"] = new SelectList(context.getListaEspecialidades().ToList(), "espId", "espNombre");

            return PartialView("GestionCMCRUD", datosCM);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult setRecibirCM(int cmedId, string tipoCM, string prioridad, int espId)
        {
            try
            {
                var Usuario = (Session["Usuario"] as sisUsuario).usrNombreDeUsuario;
                var infoProceso = context.setCartaMedica(cmedId, espId, tipoCM, prioridad, Usuario, "Recibir").First();

                //Registra log de usuario
                new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Recepcion", "JuntaMedica", null, "Recibe Carta Médica (Id: " + cmedId + ")");

                return Json(new
                {
                    Error = !infoProceso.IsValid,
                    Mensaje = infoProceso.Mensaje,
                    Foco = "espId"
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Error = true,
                    Mensaje = ex.InnerException == null ? ex.Message : ex.InnerException.Message,
                    Foco = "espId"
                });
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult setMedicosTurnosJM(int turId, string Medicos)
        {
            try
            {
                var infoProceso = context.setMedicosJuntaMedica(turId, Medicos).First();
                return Json(new
                {
                    Error = !infoProceso.IsValid,
                    Mensaje = infoProceso.Mensaje,
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
        public ActionResult setResultadoJM(int turId, string Resultados)
        {
            try
            {
                var infoProceso = context.setResultadoJuntaMedica(turId, Resultados).First();
                return Json(new
                {
                    Error = !infoProceso.IsValid,
                    Mensaje = infoProceso.Mensaje,
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
        public ActionResult setAnulacionTurnoJM(int turId, string Motivo)
        {
            try
            {
                var infoProceso = context.setAnulacionJuntaMedica(turId, Motivo).First();

                //Registra log de usuario
                new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "AdministracionJuntasMedicas", "JuntaMedica", null, "Anula Junta Médica (Id: " + turId + ")");

                return Json(new
                {
                    Error = !infoProceso.IsValid,
                    Mensaje = infoProceso.Mensaje,
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
        public ActionResult setGestionCM(int cmedId, string tipoCM, string prioridad, int espId)
        {
            try
            {
                var Usuario = (Session["Usuario"] as sisUsuario).usrNombreDeUsuario;
                var infoProceso = context.setCartaMedica(cmedId, espId, tipoCM, prioridad, Usuario, "Gestion").First();

                //Registra log de usuario
                new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "GestionCM", "JuntaMedica", null, "Gestión de Carta Médica (Id: " + cmedId + ")");

                return Json(new
                {
                    Error = !infoProceso.IsValid,
                    Mensaje = infoProceso.Mensaje,
                    Foco = "espId"
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Error = true,
                    Mensaje = ex.InnerException == null ? ex.Message : ex.InnerException.Message,
                    Foco = "espId"
                });
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult setCartaMedicaAA(int cmedId, string pAccion)
        {
            try
            {
                var Usuario = (Session["Usuario"] as sisUsuario).usrNombreDeUsuario;
                var infoProceso = context.setCartaMedica(cmedId, -1, "", "", Usuario, pAccion).First();

                //Registra log de usuario
                new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "getDatosCartaMedicaAA", "DCRM", null, pAccion + " Carta Médica (Id: " + cmedId + ")");

                return Json(new
                {
                    Error = !infoProceso.IsValid,
                    Mensaje = infoProceso.Mensaje
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Error = true,
                    Mensaje = ex.InnerException == null ? ex.Message : ex.InnerException.Message
                });
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult setListoParaAtencion(int cmedId, string Operacion)
        {
            try
            {
                var Usuario = (Session["Usuario"] as sisUsuario).usrNombreDeUsuario;
                var infoProceso = new MensajeDeProceso();
                if (Operacion == "Masivo")
                {
                    foreach (var Item in (Session["ListaCMBuscar"] as List<getListaCMPorEstadosWS>).Where(w => w.Seleccionado))
                    {
                        infoProceso = context.setCartaMedica(Item.cmedId, -1, "", "", Usuario, "ListoParaAtencion").First();
                    }
                    infoProceso.Mensaje = "Se procesaron " + (Session["ListaCMBuscar"] as List<getListaCMPorEstadosWS>).Count(w => w.Seleccionado) + " Cartas Médicas";
                    infoProceso.IsValid = true;
                }
                else
                {
                    infoProceso = context.setCartaMedica(cmedId, -1, "", "", Usuario, "ListoParaAtencion").First();
                }

                //Registra log de usuario
                new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "BusquedaCM", "JuntaMedica", null, "Carta Médica Lista para Atención (Id: " + cmedId + ")");

                return Json(new
                {
                    Error = !infoProceso.IsValid,
                    Mensaje = infoProceso.Mensaje,
                    Foco = "espId"
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Error = true,
                    Mensaje = ex.InnerException == null ? ex.Message : ex.InnerException.Message,
                    Foco = "espId"
                });
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult setListoParaAtencionChecked(int cmedId, bool pChecked)
        {
            (Session["ListaCMBuscar"] as List<getListaCMPorEstadosWS>).Single(w => w.cmedId == cmedId).Seleccionado = pChecked;

            return Json((Session["ListaCMBuscar"] as List<getListaCMPorEstadosWS>).Count(w => w.Seleccionado));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult setListoParaAtencionCheckedAll(bool pChecked)
        {
            foreach (var Item in (Session["ListaCMBuscar"] as List<getListaCMPorEstadosWS>))
            {
                Item.Seleccionado = pChecked;
            }

            return Json((Session["ListaCMBuscar"] as List<getListaCMPorEstadosWS>).Count(w => w.Seleccionado));
        }

        public ActionResult Exportar(string Filtro, string Orden)
        {
                var InfoCM = (Session["ListaCMGestionCM"] as List<getListaCMGestion_Result>);
                GridModel model = InfoCM.AsQueryable().ToGridModel(1, 9999999, Orden, string.Empty, Filtro);
                var _Datos = model.Data.Cast<getListaCMGestion_Result>();

                var workbook = new HSSFWorkbook();

                //Create new Excel sheet
                var sheet = workbook.CreateSheet();
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
                headerRow.CreateCell(0).SetCellValue("Número de CM");
                headerRow.CreateCell(1).SetCellValue("Fecha");
                headerRow.CreateCell(2).SetCellValue("Agente");
                headerRow.CreateCell(3).SetCellValue("DNI");
                headerRow.CreateCell(4).SetCellValue("Licencia Desde");
                headerRow.CreateCell(5).SetCellValue("Días de Licencia");
                headerRow.CreateCell(6).SetCellValue("Artículo");
                headerRow.CreateCell(7).SetCellValue("Lugar de Trabajo");
                headerRow.CreateCell(8).SetCellValue("Ministerio");
                headerRow.CreateCell(9).SetCellValue("Reparticiones");
                headerRow.CreateCell(10).SetCellValue("Estado");
                headerRow.CreateCell(11).SetCellValue("Fecha cambio de Estado");
                headerRow.CreateCell(12).SetCellValue("Hora cambio de Estado");
                sheet.CreateFreezePane(0, 1, 0, 1);
                int rowNumber = 1;

                //Populate the sheet with values from the grid data
                foreach (getListaCMGestion_Result _Info in _Datos)
                {
                    //Create a new row
                    var row = sheet.CreateRow(rowNumber);

                    //Set values for the cells
                    row.CreateCell(0).SetCellValue(_Info.cmedNumero);
                    row.CreateCell(1).SetCellValue(_Info.cmedFecha.ToString("dd/MM/yyyy"));
                    row.CreateCell(2).SetCellValue(_Info.agtApellidoNombre);
                    row.CreateCell(3).SetCellValue(_Info.agtDNI);
                    row.CreateCell(4).SetCellValue(_Info.licFechaInicial == null ? "" : _Info.licFechaInicial.Value.ToString("dd/MM/yyyy"));
                    row.CreateCell(5).SetCellValue(_Info.licFechaInicial == null ? "" : _Info.licCantidad.ToString());
                    row.CreateCell(6).SetCellValue(_Info.Articulo);
                    row.CreateCell(7).SetCellValue(_Info.agtLugarTrabajo);
                    row.CreateCell(8).SetCellValue(_Info.Ministerios);
                    row.CreateCell(9).SetCellValue(_Info.Reparticiones);
                    row.CreateCell(10).SetCellValue(_Info.EstadoCM);
                    row.CreateCell(11).SetCellValue(_Info.estFecha.ToString("dd/MM/yyyy"));
                    row.CreateCell(12).SetCellValue(_Info.estHora);

                    rowNumber++;
                }

                //Write the workbook to a memory stream
                MemoryStream output = new MemoryStream();
                workbook.Write(output);

                string _NombreArchivo = "CartasMedicas-" + DateTime.Now.Date.ToString("yyyyMMddhhmmss") + ".xls";
                //Return the result to the end user

                return File(output.ToArray(),   //The binary data of the XLS file
                    "application/vnd.ms-excel", //MIME type of Excel files
                    _NombreArchivo);     //Suggested file name in the "Save as" dialog which will be displayed to the end user
        }

        public ActionResult ExportarEspecial(string Filtro, string Orden)
        {
            var InfoCM = (Session["ListaCMGestionCM"] as List<getListaCMGestionFiltroEspecial_Result>);
            GridModel model = InfoCM.AsQueryable().ToGridModel(1, 9999999, Orden, string.Empty, Filtro);
            var _Datos = model.Data.Cast<getListaCMGestionFiltroEspecial_Result>();

            var workbook = new HSSFWorkbook();

            //Create new Excel sheet
            var sheet = workbook.CreateSheet();
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
            headerRow.CreateCell(0).SetCellValue("Número de CM");
            headerRow.CreateCell(1).SetCellValue("Fecha");
            headerRow.CreateCell(2).SetCellValue("Agente");
            headerRow.CreateCell(3).SetCellValue("DNI");
            headerRow.CreateCell(4).SetCellValue("Licencia Desde");
            headerRow.CreateCell(5).SetCellValue("Días de Licencia");
            headerRow.CreateCell(6).SetCellValue("Artículo");
            headerRow.CreateCell(7).SetCellValue("Lugar de Trabajo");
            headerRow.CreateCell(8).SetCellValue("Ministerio");
            headerRow.CreateCell(9).SetCellValue("Reparticiones");
            headerRow.CreateCell(10).SetCellValue("Estado");
            headerRow.CreateCell(11).SetCellValue("Fecha cambio de Estado");
            headerRow.CreateCell(12).SetCellValue("Hora cambio de Estado");
            sheet.CreateFreezePane(0, 1, 0, 1);
            int rowNumber = 1;

            //Populate the sheet with values from the grid data
            foreach (getListaCMGestionFiltroEspecial_Result _Info in _Datos)
            {
                //Create a new row
                var row = sheet.CreateRow(rowNumber);

                //Set values for the cells
                row.CreateCell(0).SetCellValue(_Info.cmedNumero);
                row.CreateCell(1).SetCellValue(_Info.cmedFecha.Value.ToString("dd/MM/yyyy"));
                row.CreateCell(2).SetCellValue(_Info.agtApellidoNombre);
                row.CreateCell(3).SetCellValue(_Info.agtDNI);
                row.CreateCell(4).SetCellValue(_Info.licFechaInicial == null ? "" : _Info.licFechaInicial.Value.ToString("dd/MM/yyyy"));
                row.CreateCell(5).SetCellValue(_Info.licFechaInicial == null ? "" : _Info.licCantidad.ToString());
                row.CreateCell(6).SetCellValue(_Info.Articulo);
                row.CreateCell(7).SetCellValue(_Info.agtLugarTrabajo);
                row.CreateCell(8).SetCellValue(_Info.Ministerios);
                row.CreateCell(9).SetCellValue(_Info.Reparticiones);
                row.CreateCell(10).SetCellValue(_Info.EstadoCM);
                row.CreateCell(11).SetCellValue(_Info.estFecha.ToString("dd/MM/yyyy"));
                row.CreateCell(12).SetCellValue(_Info.estHora);

                rowNumber++;
            }

            //Write the workbook to a memory stream
            MemoryStream output = new MemoryStream();
            workbook.Write(output);

            string _NombreArchivo = "CartasMedicas-" + DateTime.Now.Date.ToString("yyyyMMddhhmmss") + ".xls";
            //Return the result to the end user

            return File(output.ToArray(),   //The binary data of the XLS file
                "application/vnd.ms-excel", //MIME type of Excel files
                _NombreArchivo);     //Suggested file name in the "Save as" dialog which will be displayed to the end user
        }
    }
}