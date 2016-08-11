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
    using Telerik.Web.Mvc.Extensions;  
    using GeDoc.Filters;
    using GeDoc.Models;
    using GeDoc.Models.dsAccesoExpedientesTableAdapters;  
    using NPOI.HSSF.UserModel;
    using System.IO;
    public partial class catFacturaController : Controller
    {
        private efAccesoADatosEntities context = new efAccesoADatosEntities();

        //Edición de datos

        [GridAction]
        public ActionResult _SelectEditing()
        {
            return View(new GridModel(All()));
        }

        private IList<catFacturas> All()
        {
            return getDatos().ToList();
        }

        private catFactura AsignaDatos(catFacturas pItem)
        {
            catFactura _Item2 = new catFactura();
            if (pItem.facId != 0 && pItem.facId != null)
            {
                _Item2 = context.catFactura.FirstOrDefault(p => p.facId == pItem.facId);
            }

            _Item2.facFecha = pItem.facFecha;
            _Item2.facFechaRecepcion = pItem.facFechaRecepcion;
            _Item2.facId = pItem.facId;
            _Item2.facImporte = pItem.facImporte;
            _Item2.facNumero = pItem.facNumero;
            _Item2.facPeriodoAnio = (short)pItem.facPeriodoAnio;
            _Item2.facPeriodoMes = pItem.facPeriodoMes;
            _Item2.osId = (short)pItem.osId;
            _Item2.sucId = pItem.sucId;

            return _Item2;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _SaveEditing(int id)
        {
            //catFactura _Item = context.catFactura.Where(p => p.facId == id).FirstOrDefault();
            catFacturas _Item = new catFacturas();

            if (TryUpdateModel(_Item))
            {
                _Item.facId = id;
                Edit(AsignaDatos(_Item));
            }

            return View(new GridModel(All()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _InsertEditing()
        {
            catFacturas _Item = new catFacturas();

            if (TryUpdateModel(_Item))
            {
                Create(AsignaDatos(_Item));
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

        private IEnumerable<catFacturas> getDatos()
        {
            try
            {
                var _Datos = (from d in context.getListadoFacturas()
                              //orderby d.facFecha descending
                              select new catFacturas()
                              {
                                  facFecha = d.facFecha,
                                  facId = d.facId,
                                  facFechaRecepcion = d.facFechaRecepcion,
                                  facImporte = d.facImporte,
                                  facNumero = d.facNumero,
                                  facPeriodoAnio = d.facPeriodoAnio,
                                  facPeriodoMes = d.facPeriodoMes,
                                  osId = d.osId,
                                  osNombre = d.osNombre,
                                  Periodo = d.Periodo,
                                  sucId = d.sucId,
                                  sucNombre = d.sucNombre,
                                  facHaber = d.facHaber,
                                  facSaldo = (decimal)d.facSaldo,
                                  facImporteTexto = d.facImporte.ToString("$ #,###.00"),
                                  facDiasVtoSSS = d.facDiasVtoSSS ?? 0,//getDiasRestantesReclamoSSS(d),
                                  facEstado = d.facEstado,//getEstado(d, false),
                                  facEstadoImagen = d.facEstadoImagen,//getEstado(d, true),
                                  VisibilidadImagen = d.facEstadoImagen.Contains("Naranja") || d.facEstadoImagen.Contains("Amarillo") || d.facEstadoImagen.Contains("Gris") || d.facEstadoImagen.Contains("Rojo") || d.facEstadoImagen.Contains("Azul") || d.facEstadoImagen.Contains("Violeta") ? "visible" : "collapse"
                              }).ToList();

                return _Datos.AsEnumerable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private int getDiasRestantesReclamoSSS(catFactura pDatos)
        {
            int _PlazoMaximo = (int)(new DateTime(pDatos.facPeriodoAnio, pDatos.facPeriodoMes, 1).AddYears(1) - DateTime.Now).TotalDays;
            if (_PlazoMaximo > 180)
            {
                _PlazoMaximo = 180;
            }
            int _facDiasVtoSSS = _PlazoMaximo - (int)(DateTime.Now - pDatos.facFecha).TotalDays;
            if (_facDiasVtoSSS < 0)
            {
                _facDiasVtoSSS = 0;
            }

            return _facDiasVtoSSS;
        }

        private string getEstado(catFactura pDatos, bool pgetColor)
        {
            int _PlazoMaximo = (int)(new DateTime(pDatos.facPeriodoAnio, pDatos.facPeriodoMes, 1).AddYears(1) - DateTime.Now).TotalDays;
            if(_PlazoMaximo > 180)
            {
                _PlazoMaximo = 180;
            }
            int _PlazoFactura = pDatos.facFechaRecepcion == null ? 60 : (int)(DateTime.Now - pDatos.facFechaRecepcion.Value).TotalDays;
            if (pDatos.catFacturaPago != null && pDatos.catFacturaPago.Count > 0)
            {
                if (pDatos.catFacturaPago.Where(w => w.sisTipo.tipoCodigo == "DE").Sum(s => s.pagImporte) == pDatos.facImporte)
                {
                    return pgetColor ? "Turquesa.png" : "Debitado";
                }
                else if (pDatos.catFacturaPago.Sum(s => s.pagImporte) == pDatos.facImporte)
                {
                    if (pDatos.catFacturaPago.Where(w => w.sisTipo.tipoCodigo == "DE").Count() > 0)
                    {
                        return pgetColor ? "Celeste.png" : "Cobrado y Debitado";
                    }
                    else
                    {
                        return pgetColor ? "Verde.png" : "Cobrado";
                    }
                }
                else
                {
                    return pgetColor ? "VerdeClaro.png" : "Cobrado parcialmente";
                }
            }
            else
            {
                if (_PlazoFactura < 60)
                {
                    return pgetColor ? "Gris.png" : "En término";
                }
                else
                {
                    if (_PlazoMaximo > 0)
                    {
                        if (pDatos.catFacturaNotificacion.Count == 0)
                        {
                            return pgetColor ? "Amarillo.png" : "Vencida";
                        }
                        else if (pDatos.catFacturaNotificacion.Where(w => w.sisTipo.tipoCodigo == "CD").Count() > 0)
                        {
                            DateTime _FechaCarta = pDatos.catFacturaNotificacion.Where(w => w.sisTipo.tipoCodigo == "CD").First().notFecha;
                            if ((int)(DateTime.Now - _FechaCarta).TotalDays > 10)
                            {
                                return pgetColor ? "Rojo.png" : "Carta documento vencida";
                            }
                            else
                            {
                                return pgetColor ? "Naranja.png" : "Carta documento";
                            }
                        }
                        else if (pDatos.catFacturaNotificacion.Where(w => w.sisTipo.tipoCodigo == "SS").Count() > 0)
                        {
                            return pgetColor ? "Azul.png" : "Reclamo enviado a la Super Intendencia de Seguro de Salud";
                        }
                    }
                    else
                    {
                        return pgetColor ? "Violeta.png" : "Sin reclamo a la Super Intendencia de Seguro de Salud";
                    }
                }
            }

            return "";
        }

        public ActionResult Index()
        {
            //int _facId = All().First().facId;
            ViewBag.Catalogo = "Facturas a Obras Sociales";
            ViewData["FiltroContains"] = true;
            ViewData["Pagos"] = AllPagos().Where(exp => exp.facId == -1);

            PopulateDropDownList();

            return PartialView();
        }

        private void Create(catFactura pItem)
        {
            if (ModelState.IsValid)
            {
                context.catFactura.AddObject(pItem);
                context.SaveChanges();
            }

            return;
        }

        private void Edit(catFactura pItem)
        {
            if (ModelState.IsValid)
            {
                context.SaveChanges();
            }
            return;
        }

        private void DeleteConfirmed(int id)
        {
            catFactura _Item = context.catFactura.Single(x => x.facId == id);
            context.catFactura.DeleteObject(_Item);
            context.SaveChanges();
        }

        public ActionResult Exportar(string Filtro, string Orden)
        {
            //Get the data representing the current grid state - page, sort and filter
            GridModel model = getDatos().AsQueryable().ToGridModel(1, 9999999, Orden, string.Empty, Filtro);
            var _Datos = model.Data.Cast<catFacturas>();

            //Create new Excel workbook
            var workbook = new HSSFWorkbook();

            //Create new Excel sheet
            var sheet = workbook.CreateSheet(); 

            //(Optional) set the width of the columns
            sheet.SetColumnWidth(0, 12 * 256);
            sheet.SetColumnWidth(1, 30 * 256);
            sheet.SetColumnWidth(2, 15 * 256);
            sheet.SetColumnWidth(3, 15 * 256);
            sheet.SetColumnWidth(4, 20 * 256);
            sheet.SetColumnWidth(5, 10 * 256);
            sheet.SetColumnWidth(6, 15 * 256);
            sheet.SetColumnWidth(7, 15 * 256);
            sheet.SetColumnWidth(8, 15 * 256);
            sheet.SetColumnWidth(9, 15 * 256);
            sheet.SetColumnWidth(10, 15 * 256);
            sheet.SetColumnWidth(11, 15 * 256);

            //Create a header row
            var headerRow = sheet.CreateRow(0);
            //Set the column names in the header row
            headerRow.CreateCell(0).SetCellValue("Estado");
            headerRow.CreateCell(1).SetCellValue("Días Restante ");
            headerRow.CreateCell(2).SetCellValue("Obra social");
            headerRow.CreateCell(3).SetCellValue("Centro de Salud");
            headerRow.CreateCell(4).SetCellValue("Periodo");
            headerRow.CreateCell(5).SetCellValue("Numero");
            headerRow.CreateCell(6).SetCellValue("Fecha");
            headerRow.CreateCell(7).SetCellValue("Recepcion");
            headerRow.CreateCell(8).SetCellValue("Debe");
            headerRow.CreateCell(9).SetCellValue("Haber");
            headerRow.CreateCell(10).SetCellValue("Saldo");

            //(Optional) freeze the header row so it is not scrolled
            sheet.CreateFreezePane(0, 1, 0, 1);

            int rowNumber = 1;

            //Populate the sheet with values from the grid data
            foreach (catFacturas _Info in _Datos)
            {
                //Create a new row
                var row = sheet.CreateRow(rowNumber);

                //Set values for the cells             
                row.CreateCell(0).SetCellValue(_Info.facEstado);               
                row.CreateCell(1).SetCellValue(_Info.facDiasVtoSSS);
                row.CreateCell(2).SetCellValue(_Info.osNombre);
                row.CreateCell(3).SetCellValue(_Info.sucNombre);
                row.CreateCell(4).SetCellValue(_Info.Periodo);
                row.CreateCell(5).SetCellValue(_Info.facNumero);
                row.CreateCell(6).SetCellValue(_Info.facFecha);
                row.CreateCell(7).SetCellValue(_Info.facFechaRecepcion == null ? "" : _Info.facFechaRecepcion.Value.ToString("dd/MM/yyyy"));
                row.CreateCell(8).SetCellValue((double)_Info.facImporte);
                row.CreateCell(9).SetCellValue((double)_Info.facHaber);
                row.CreateCell(10).SetCellValue((double)_Info.facSaldo);
                rowNumber++;
            }

            //Write the workbook to a memory stream
            MemoryStream output = new MemoryStream();
            workbook.Write(output);

            string _NombreArchivo = "Facturas-" + DateTime.Now.Date.ToString("yyyyMMddhhmmss") + ".xls";
            //Return the result to the end user

            return File(output.ToArray(),   //The binary data of the XLS file
                "application/vnd.ms-excel", //MIME type of Excel files
                _NombreArchivo);     //Suggested file name in the "Save as" dialog which will be displayed to the end user
        }

        public ActionResult ExportarDebitado(DateTime Desde, DateTime Hasta)
        {
            //Get the data representing the current grid state - page, sort and filter
            var _Datos = context.getListadoRecuperoCostos("D", Desde, Hasta);

            //Create new Excel workbook
            var workbook = new HSSFWorkbook();

            //Create new Excel sheet
            var sheet = workbook.CreateSheet();

            //(Optional) set the width of the columns
            sheet.SetColumnWidth(0, 12 * 256);
            sheet.SetColumnWidth(1, 30 * 256);
            sheet.SetColumnWidth(2, 15 * 256);
            sheet.SetColumnWidth(3, 15 * 256);
            sheet.SetColumnWidth(4, 20 * 256);
            sheet.SetColumnWidth(5, 10 * 256);
            sheet.SetColumnWidth(6, 15 * 256);
            sheet.SetColumnWidth(7, 15 * 256);
            sheet.SetColumnWidth(8, 15 * 256);
            sheet.SetColumnWidth(9, 15 * 256);
            sheet.SetColumnWidth(10, 15 * 256);
            sheet.SetColumnWidth(11, 15 * 256);

            //Create a header row
            var headerRow = sheet.CreateRow(0);
            //Set the column names in the header row
            headerRow.CreateCell(0).SetCellValue("Año");
            headerRow.CreateCell(1).SetCellValue("Mes");
            headerRow.CreateCell(2).SetCellValue("Nº Factura");
            headerRow.CreateCell(3).SetCellValue("Fecha");
            headerRow.CreateCell(4).SetCellValue("Fecha Recepción");
            headerRow.CreateCell(5).SetCellValue("Facturado");
            headerRow.CreateCell(6).SetCellValue("Obra Social");
            headerRow.CreateCell(7).SetCellValue("Centro de Salud");
            headerRow.CreateCell(8).SetCellValue("Fecha Débito");
            headerRow.CreateCell(9).SetCellValue("Motivo");
            headerRow.CreateCell(10).SetCellValue("Debitado");

            //(Optional) freeze the header row so it is not scrolled
            sheet.CreateFreezePane(0, 1, 0, 1);

            int rowNumber = 1;

            //Populate the sheet with values from the grid data
            foreach (getListadoRecuperoCostos_Result _Info in _Datos)
            {
                //Create a new row
                var row = sheet.CreateRow(rowNumber);

                //Set values for the cells             
                row.CreateCell(0).SetCellValue(_Info.facPeriodoAnio);
                row.CreateCell(1).SetCellValue(_Info.facPeriodoMes);
                row.CreateCell(2).SetCellValue(_Info.facNumero);
                row.CreateCell(3).SetCellValue(_Info.facFecha.ToString("dd/MM/yyyy"));
                row.CreateCell(4).SetCellValue(_Info.FacFechaRecepcion == null ? "" : _Info.FacFechaRecepcion.Value.ToString("dd/MM/yyyy"));
                row.CreateCell(5).SetCellValue((double)_Info.facImporte);
                row.CreateCell(6).SetCellValue(_Info.osDenominacion);
                row.CreateCell(7).SetCellValue(_Info.csNombre);
                row.CreateCell(8).SetCellValue(_Info.pagFecha.ToString("dd/MM/yyyy"));
                row.CreateCell(9).SetCellValue(_Info.pagMotivo);
                row.CreateCell(10).SetCellValue((double)_Info.pagImporte);
                rowNumber++;
            }

            //Write the workbook to a memory stream
            MemoryStream output = new MemoryStream();
            workbook.Write(output);

            string _NombreArchivo = "Debitado-" + DateTime.Now.Date.ToString("yyyyMMddhhmmss") + ".xls";
            //Return the result to the end user

            return File(output.ToArray(),   //The binary data of the XLS file
                "application/vnd.ms-excel", //MIME type of Excel files
                _NombreArchivo);     //Suggested file name in the "Save as" dialog which will be displayed to the end user
        }

        public ActionResult ExportarCobrado(DateTime Desde, DateTime Hasta)
        {
            //Get the data representing the current grid state - page, sort and filter
            var _Datos = context.getListadoRecuperoCostos("C", Desde, Hasta);

            //Create new Excel workbook
            var workbook = new HSSFWorkbook();

            //Create new Excel sheet
            var sheet = workbook.CreateSheet();

            //(Optional) set the width of the columns
            sheet.SetColumnWidth(0, 12 * 256);
            sheet.SetColumnWidth(1, 30 * 256);
            sheet.SetColumnWidth(2, 15 * 256);
            sheet.SetColumnWidth(3, 15 * 256);
            sheet.SetColumnWidth(4, 20 * 256);
            sheet.SetColumnWidth(5, 10 * 256);
            sheet.SetColumnWidth(6, 15 * 256);
            sheet.SetColumnWidth(7, 15 * 256);
            sheet.SetColumnWidth(8, 15 * 256);
            sheet.SetColumnWidth(9, 15 * 256);
            sheet.SetColumnWidth(10, 15 * 256);
            sheet.SetColumnWidth(11, 15 * 256);

            //Create a header row
            var headerRow = sheet.CreateRow(0);
            //Set the column names in the header row
            headerRow.CreateCell(0).SetCellValue("Año");
            headerRow.CreateCell(1).SetCellValue("Mes");
            headerRow.CreateCell(2).SetCellValue("Nº Factura");
            headerRow.CreateCell(3).SetCellValue("Fecha");
            headerRow.CreateCell(4).SetCellValue("Fecha Recepción");
            headerRow.CreateCell(5).SetCellValue("Facturado");
            headerRow.CreateCell(6).SetCellValue("Obra Social");
            headerRow.CreateCell(7).SetCellValue("Centro de Salud");
            headerRow.CreateCell(8).SetCellValue("Fecha Cobro");
            headerRow.CreateCell(9).SetCellValue("N° Recibo");
            headerRow.CreateCell(10).SetCellValue("Forma de Pago");
            headerRow.CreateCell(11).SetCellValue("Detalle");
            headerRow.CreateCell(12).SetCellValue("Cobrado");

            //(Optional) freeze the header row so it is not scrolled
            sheet.CreateFreezePane(0, 1, 0, 1);

            int rowNumber = 1;

            //Populate the sheet with values from the grid data
            foreach (getListadoRecuperoCostos_Result _Info in _Datos)
            {
                //Create a new row
                var row = sheet.CreateRow(rowNumber);

                //Set values for the cells             
                row.CreateCell(0).SetCellValue(_Info.facPeriodoAnio);
                row.CreateCell(1).SetCellValue(_Info.facPeriodoMes);
                row.CreateCell(2).SetCellValue(_Info.facNumero);
                row.CreateCell(3).SetCellValue(_Info.facFecha.ToString("dd/MM/yyyy"));
                row.CreateCell(4).SetCellValue(_Info.FacFechaRecepcion == null ? "" : _Info.FacFechaRecepcion.Value.ToString("dd/MM/yyyy"));
                row.CreateCell(5).SetCellValue((double)_Info.facImporte);
                row.CreateCell(6).SetCellValue(_Info.osDenominacion);
                row.CreateCell(7).SetCellValue(_Info.csNombre);
                row.CreateCell(8).SetCellValue(_Info.pagFecha.ToString("dd/MM/yyyy"));
                row.CreateCell(9).SetCellValue(_Info.pagNumeroRecibo.ToString());
                row.CreateCell(10).SetCellValue(_Info.tipoDescripcion);
                row.CreateCell(11).SetCellValue(_Info.pagDetalle);
                row.CreateCell(12).SetCellValue((double)_Info.pagImporte);
                rowNumber++;
            }

            //Write the workbook to a memory stream
            MemoryStream output = new MemoryStream();
            workbook.Write(output);

            string _NombreArchivo = "Cobrado-" + DateTime.Now.Date.ToString("yyyyMMddhhmmss") + ".xls";
            //Return the result to the end user

            return File(output.ToArray(),   //The binary data of the XLS file
                "application/vnd.ms-excel", //MIME type of Excel files
                _NombreArchivo);     //Suggested file name in the "Save as" dialog which will be displayed to the end user
        }

        public ActionResult ExportarPendiente(DateTime Desde, DateTime Hasta)
        {
            //Get the data representing the current grid state - page, sort and filter
            var _Datos = context.getListadoRecuperoCostos("P", Desde, Hasta);

            //Create new Excel workbook
            var workbook = new HSSFWorkbook();

            //Create new Excel sheet
            var sheet = workbook.CreateSheet();

            //(Optional) set the width of the columns
            sheet.SetColumnWidth(0, 12 * 256);
            sheet.SetColumnWidth(1, 30 * 256);
            sheet.SetColumnWidth(2, 15 * 256);
            sheet.SetColumnWidth(3, 15 * 256);
            sheet.SetColumnWidth(4, 20 * 256);
            sheet.SetColumnWidth(5, 10 * 256);
            sheet.SetColumnWidth(6, 15 * 256);
            sheet.SetColumnWidth(7, 15 * 256);
            sheet.SetColumnWidth(8, 15 * 256);
            //sheet.SetColumnWidth(9, 15 * 256);
            //sheet.SetColumnWidth(10, 15 * 256);
            //sheet.SetColumnWidth(11, 15 * 256);

            //Create a header row
            var headerRow = sheet.CreateRow(0);
            //Set the column names in the header row
            headerRow.CreateCell(0).SetCellValue("Año");
            headerRow.CreateCell(1).SetCellValue("Mes");
            headerRow.CreateCell(2).SetCellValue("Nº Factura");
            headerRow.CreateCell(3).SetCellValue("Fecha");
            headerRow.CreateCell(4).SetCellValue("Fecha Recepción");
            headerRow.CreateCell(5).SetCellValue("Facturado");
            headerRow.CreateCell(6).SetCellValue("Obra Social");
            headerRow.CreateCell(7).SetCellValue("Centro de Salud");
            //headerRow.CreateCell(8).SetCellValue("Fecha Débito");
            //headerRow.CreateCell(9).SetCellValue("Motivo");
            headerRow.CreateCell(8).SetCellValue("Saldo");

            //(Optional) freeze the header row so it is not scrolled
            sheet.CreateFreezePane(0, 1, 0, 1);

            int rowNumber = 1;

            //Populate the sheet with values from the grid data
            foreach (getListadoRecuperoCostos_Result _Info in _Datos)
            {
                //Create a new row
                var row = sheet.CreateRow(rowNumber);

                //Set values for the cells             
                row.CreateCell(0).SetCellValue(_Info.facPeriodoAnio);
                row.CreateCell(1).SetCellValue(_Info.facPeriodoMes);
                row.CreateCell(2).SetCellValue(_Info.facNumero);
                row.CreateCell(3).SetCellValue(_Info.facFecha.ToString("dd/MM/yyyy"));
                row.CreateCell(4).SetCellValue(_Info.FacFechaRecepcion == null ? "" : _Info.FacFechaRecepcion.Value.ToString("dd/MM/yyyy"));
                row.CreateCell(5).SetCellValue((double)_Info.facImporte);
                row.CreateCell(6).SetCellValue(_Info.osDenominacion);
                row.CreateCell(7).SetCellValue(_Info.csNombre);
                //row.CreateCell(8).SetCellValue(_Info.pagFecha.ToString("dd/MM/yyyyy"));
                //row.CreateCell(9).SetCellValue(_Info.pagMotivo);
                row.CreateCell(8).SetCellValue((double)_Info.pagImporte);
                rowNumber++;
            }

            //Write the workbook to a memory stream
            MemoryStream output = new MemoryStream();
            workbook.Write(output);

            string _NombreArchivo = "Pendiente-" + DateTime.Now.Date.ToString("yyyyMMddhhmmss") + ".xls";
            //Return the result to the end user

            return File(output.ToArray(),   //The binary data of the XLS file
                "application/vnd.ms-excel", //MIME type of Excel files
                _NombreArchivo);     //Suggested file name in the "Save as" dialog which will be displayed to the end user
        }

        //Datos para el dropdown
        protected void PopulateDropDownList()
        {
            dcAccesoCompadDataContext _Centros = new dcAccesoCompadDataContext();
            var _ObraSocial = (from p in context.catObraSocial.ToList()
                               select new catObraSocial
                               {
                                    osId = p.osId,
                                    osDenominacion = p.osCodigo.ToString() + "-" + p.osDenominacion
                               }).ToList().OrderBy(p => p.osDenominacion);

            var _CentroSalud = (from p in _Centros.CatSucursals
                                select new catCentrosDeSalud
                                {
                                   sucId = p.sucId,
                                   sucNombre = p.sucNombre
                                }).ToList().OrderBy(p => p.sucNombre);

            var _Tipo = (from s in context.sisTipo.ToList()
                         where s.sisTipoEntidad.tipoeCodigo == "TI"
                         orderby s.tipoDescripcion
                         select new sisTipo()
                         {
                             tipoId = s.tipoId,
                             tipoDescripcion = s.tipoDescripcion
                         }).ToList();

            var _Noti = (from s in context.sisTipo.ToList()
                         where s.sisTipoEntidad.tipoeCodigo == "TN"
                         orderby s.tipoDescripcion
                         select new sisTipo()
                         {
                             tipoId = s.tipoId,
                             tipoDescripcion = s.tipoDescripcion
                         }).ToList();

            var _Forma = (from s in context.sisTipo.ToList()
                          where s.sisTipoEntidad.tipoeCodigo == "FP"
                          select new sisTipo()
                          {
                              tipoId = s.tipoId,
                              tipoDescripcion = s.tipoDescripcion
                          }).ToList();

            ViewData["osId_Data"] = new SelectList(_ObraSocial, "osId", "osDenominacion");
            ViewData["sucId_Data"] = new SelectList(_CentroSalud, "sucId", "sucNombre");
            ViewData["facPeriodoMes_Data"] = new SelectList(new catMeses().Mes, "mesId", "mesNombre");
            ViewData["tipoId_Data"] = new SelectList(_Tipo, "tipoId", "tipoDescripcion");
            ViewData["tipoIdForma_Data"] = new SelectList(_Forma, "tipoId", "tipoDescripcion");
            ViewData["tipoIdNota_Data"] = new SelectList(_Noti, "tipoId", "tipoDescripcion");

            return;
        }
    }
}