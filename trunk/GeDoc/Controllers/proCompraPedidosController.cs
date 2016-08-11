using System.IO;
using NPOI.HSSF.UserModel;

namespace GeDoc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Telerik.Web.Mvc;
    using GeDoc.Filters;
    using GeDoc.Models;

    public partial class proCompraController : Controller
    {
        //Edición de datos
        public IList<proCompraSeguimientoWS> AllSeguimiento(Int64? idCompra)
        {
            return getDatosSeguimiento(idCompra).ToList();
        }

        private IEnumerable<proCompraSeguimientoWS> getDatosSeguimiento(Int64? idCompra)
        {
            var _Datos = (from d in context.proCompraSeguimiento.Where(w => w.comId == idCompra).ToList()
                          select new proCompraSeguimientoWS()
                                    {
                                        segcomId = d.comId,
                                        segId = d.segId,
                                        segObservaciones = d.segObservaciones,
                                        segRespuesta = d.segRespuesta,
                                        pcId = d.pcId,
                                        provId = d.provId,
                                        segFecha = d.segFecha,
                                        Proveedor = d.catProveedor.provRazonSocial,
                                        Contacto = d.pcId == null ? "" : d.catProveedorContacto.pcApellidoyNombre,
                                        tipoIdTipoNotificacion = d.tipoIdTipoNotificacion,
                                        TipoNotificacion = new sisTipos() {tipoDescripcion = d.sisTipo.tipoDescripcion, tipoImagen = d.sisTipo.tipoImagen}
                                    }).ToList();

            return _Datos.AsEnumerable();
        }

        [GridAction]
        public ActionResult _BindingSeguimiento(Int64? idCompra)
        {
            idCompra = idCompra ?? 1;

            PopulateDropDownListSeguimiento();

            return View(new GridModel<proCompraSeguimientoWS>
            {
                Data = AllSeguimiento(idCompra)
            });
        }

        [GridAction]
        public ActionResult onDataBinding_getProveedoresWS()
        {
            return View(new GridModel<getProveedoresWS>
            {
                Data = (Session["vd_getProveedoresWS"] as List<getProveedoresWS>).AsEnumerable()
            });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CheckedProveedor(int Id, bool pChecked)
        {
            var Seleccion = (Session["vd_getProveedoresWS"] as List<getProveedoresWS>).First(w => w.Id == Id);
            Seleccion.provSeleccionado = pChecked;

            return Json(true);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CheckedContacto(int Id, bool pChecked)
        {
            var Seleccion = (Session["vd_getProveedoresWS"] as List<getProveedoresWS>).First(w => w.Id == Id);
            Seleccion.pcSeleccionado = pChecked;

            return Json(true);
        }

        [GridAction]
        public ActionResult Seguimiento()
        {
            PopulateDropDownListSeguimiento();

            return PartialView("Seguimiento");
        }

        [GridAction]
        public ActionResult getSeguimiento(string Accion, Int64? segId, Int64? idCompra)
        {
            var _Datos = new proCompraSeguimientoWS();
            _Datos.segAccion = Accion;
            _Datos.segcomId = (int)idCompra;
            _Datos.segId = (int)segId;

            if(Accion == "Agregar")
            {
                _Datos.segFecha = DateTime.Now;
            }
            else
            {
                //Cargamos los datos de la oferta
                var DatosActuales = context.proCompraSeguimiento.Single(w => w.segId == segId);

                _Datos.segFecha = DatosActuales.segFecha;
                _Datos.tipoIdTipoNotificacion = DatosActuales.tipoIdTipoNotificacion;
                _Datos.segRespuesta = DatosActuales.segRespuesta;
                _Datos.segObservaciones = DatosActuales.segObservaciones;
                _Datos.provId = DatosActuales.provId;
                _Datos.pcId = DatosActuales.pcId;
            }

            Session["vd_getProveedoresWS"] = context.getProveedores().ToList();

            PopulateDropDownListSeguimiento();

            return PartialView("CRUDSeguimiento", _Datos);
        }

        [GridAction]
        public ActionResult getEnvioCorreo()
        {
            return PartialView("EnvioCorreoElectronico");
        }

        //Datos para el dropdown
        public void PopulateDropDownListSeguimiento()
        {
            var _Proveedor = (from p in context.catProveedor.ToList()
                              orderby p.provRazonSocial
                              select new catProveedor()
                              {
                                  provId = p.provId,
                                  provRazonSocial = p.provRazonSocial,
                                  provCUIT = p.provCUIT
                              }).ToList();

            var Proveedor = _Proveedor.First().provId;

            var _Tipos = context.sisTipo.ToList();

            ViewData["tipoIdTipoNotificacion_Data"] = new SelectList(_Tipos.Where(w => w.sisTipoEntidad.tipoeCodigo == "TO").ToList(), "tipoId", "tipoDescripcion");
            ViewData["provId_Data"] = new SelectList(_Proveedor, "provId", "provRazonSocial");
            CargaContactos(Proveedor);
        }

        private void CargaContactos(int provId)
        {
            var _Contacto = (from p in context.catProveedorContacto.Where(w => w.provId == provId).ToList()
                             orderby p.pcApellidoyNombre
                             select new catProveedorContactoWS()
                             {
                                 pcId = p.pcId,
                                 pcApellidoyNombre = p.pcApellidoyNombre
                             }).ToList();

            ViewData["pcId_Data"] = new SelectList(_Contacto, "pcId", "pcApellidoyNombre");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult setSeguimiento(proCompraSeguimientoWS Datos)
        {
            if (Datos.segAccion == "Modificar" || Datos.segAccion == "Agregar")
            {
                if (Datos.tipoIdTipoNotificacion == null)
                {
                    return Json(new
                        {
                            Error = true,
                            Mensaje = "Debe ingresar el tipo de notificación.",
                            Foco = "tipoIdTipoNotificacion"
                        });
                }

                if (Datos.provId == null)
                {
                    return Json(new
                    {
                        Error = true,
                        Mensaje = "Debe ingresar el Proveedor.",
                        Foco = "provId"
                    });
                }
            }

            try
            {
                switch (Datos.segAccion)
                {
                    case "Agregar":
                    case "Modificar":
                        var NewCompra = new proCompraSeguimiento();

                        if (Datos.segAccion == "Agregar")
                        {
                            NewCompra.segFecha = DateTime.Now;
                            NewCompra.tipoIdTipoNotificacion = (short)Datos.tipoIdTipoNotificacion;
                            NewCompra.provId = Datos.provId;
                            NewCompra.pcId = Datos.pcId;
                            NewCompra.comId = Datos.segcomId;
                        }
                        else
                        {
                            NewCompra = context.proCompraSeguimiento.Single(w => w.segId == Datos.segId);
                        }

                        NewCompra.segObservaciones = Datos.segObservaciones ?? "";
                        NewCompra.segRespuesta = Datos.segRespuesta ?? "";

                        if (Datos.segAccion == "Agregar")
                        {
                            context.proCompraSeguimiento.AddObject(NewCompra);
                        }
                        break;
                    case "Eliminar":
                        var DeleteCompra = context.proCompraSeguimiento.Single(w => w.segId == Datos.segId);
                        context.proCompraSeguimiento.DeleteObject(DeleteCompra);
                        break;
                }

                context.SaveChanges();

                return Json(new
                {
                    Error = false,
                    Mensaje = "Se actualizaron los datos de seguimiento en forma correcta",
                    Foco = "segObservaciones"
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Error = true,
                    Mensaje = ex.InnerException == null ? ex.Message : ex.InnerException.Message,
                    Foco = "segObservaciones"
                });
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult setEnvioCorreos(int comId)
        {
            var DatosProveedor = (Session["vd_getProveedoresWS"] as List<getProveedoresWS>).Where(w => w.provSeleccionado);
            var DatosContacto = (Session["vd_getProveedoresWS"] as List<getProveedoresWS>).Where(w => w.pcSeleccionado);

            if (DatosProveedor.Count() == 0 && DatosContacto.Count() == 0)
            {
                return Json(new
                {
                    Error = true,
                    Mensaje = "Debe seleccionar al menos un Proveedor o Contacto para enviar Correo Electrónico.",
                    Foco = "gridgetProveedoresWS"
                });
            }

            try
            {
                var tipoIdEmail = context.sisTipo.First(f => f.sisTipoEntidad.tipoeCodigo == "TO" && f.tipoCodigo == "EM").tipoId;
                foreach (var getProveedoresWs in DatosProveedor)
                {
                    var Email = new catRequerimientoNotificaciones();
                    Email.reqnArchivoAdjunto = GenerarAdjunto(getProveedoresWs.provId, comId);
                    Email.reqnDestinatario = getProveedoresWs.provRazonSocial;
                    Email.reqnDestinatarioEmail = getProveedoresWs.provCorreoElectronico;
                    Email.reqnEnviado = false;
                    Email.reqnError = "";
                    Email.reqnFecha = DateTime.Now;
                    //Email.reqnMensaje = "Estimado " + getProveedoresWs.provRazonSocial + " el Ministerio de Salud Pública, le solicita el envío de un presupuesto del detalle de productos que figuran en el archivo adjunto. <span>Muchas Gracias.</span>";
                    Email.reqnMensaje = "<p>Estimado " + getProveedoresWs.provRazonSocial + " el Ministerio de Salud Pública le solicita nos ayude con su cotización de los renglones que se envían en archivo adjunto.</p>";
                    Email.reqnMensaje += "<p></p>";
                    Email.reqnMensaje += "<p>RECUERDE QUE:</p>";
                    Email.reqnMensaje += "<p>- Presupuestos Menores a $20.000 con renglones cotizados en su totalidad: agradeceríamos que sean enviados a nuestras oficinas en Piso 3° Nucleo 3 Centro Cívico  Oficina de Compras. De no ser posible comunicarse al Tel.: 4305617.</p>";
                    Email.reqnMensaje += "<p>- Presupuestos Mayores a $20.000: Remitir a el e-mail de esta oficina (comprasmsp1@gmail.com), de ser posible en formato PDF con logo de la empresa o en excel con logo de la empresa.</p>";
                    Email.reqnMensaje += "<p></p>";
                    Email.reqnMensaje += "<p>OBSERVACIONES:</p>";
                    Email.reqnMensaje += "<p>- En hoja 1 y 2 se adjunta detalle de contacto obrante en nuestros archivo, se ruega controlar y comunicarse ante cualquier cambio al Tel.: 4305617.</p>";
                    Email.reqnMensaje += "<p></p>";
                    Email.reqnMensaje += "<p>Muchas Gracias por su colaboración.</p>";
                    context.catRequerimientoNotificaciones.AddObject(Email);

                    //Guardamos el registro de seguimiento del envío de mail.
                    var NewCompra = new proCompraSeguimiento();
                    NewCompra.segFecha = DateTime.Now;
                    NewCompra.tipoIdTipoNotificacion = tipoIdEmail;
                    NewCompra.provId = getProveedoresWs.provId;
                    NewCompra.pcId = null;
                    NewCompra.comId = comId;
                    NewCompra.segObservaciones = "";
                    NewCompra.segRespuesta = "";
                    context.proCompraSeguimiento.AddObject(NewCompra);
                }
                foreach (var getProveedoresWs in DatosContacto)
                {
                    var Email = new catRequerimientoNotificaciones();
                    Email.reqnArchivoAdjunto = GenerarAdjunto(getProveedoresWs.provId, comId);
                    Email.reqnDestinatario = getProveedoresWs.pcApellidoyNombre + ", Representante de " + getProveedoresWs.provRazonSocial;
                    Email.reqnDestinatarioEmail = getProveedoresWs.pcCorreoElectronico;
                    Email.reqnEnviado = false;
                    Email.reqnError = "";
                    Email.reqnFecha = DateTime.Now;
                    Email.reqnMensaje = "Estimado " + getProveedoresWs.provRazonSocial + " el Ministerio de Salud Pública, le solicita el envío de un presupuesto del detalle de productos que figuran en el archivo adjunto. <span>Muchas Gracias.</span>";
                    context.catRequerimientoNotificaciones.AddObject(Email);

                    //Guardamos el registro de seguimiento del envío de mail.
                    var NewCompra = new proCompraSeguimiento();
                    NewCompra.segFecha = DateTime.Now;
                    NewCompra.tipoIdTipoNotificacion = tipoIdEmail;
                    NewCompra.provId = getProveedoresWs.provId;
                    NewCompra.pcId = getProveedoresWs.pcId;
                    NewCompra.comId = comId;
                    NewCompra.segObservaciones = "";
                    NewCompra.segRespuesta = "";
                    context.proCompraSeguimiento.AddObject(NewCompra);
                }

                context.SaveChanges();

                return Json(new
                {
                    Error = false,
                    Mensaje = "Se generó la información para envío de Correos Eletrónicos forma correcta",
                    Foco = "gridgetProveedoresWS"
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Error = true,
                    Mensaje = ex.InnerException == null ? ex.Message : ex.InnerException.Message,
                    Foco = "gridgetProveedoresWS"
                });
            }
        }

        private string GenerarAdjunto(int provId, int comId)
        {
            //Datos del Proveedor
            var Proveedor = context.catProveedor.First(p => p.provId == provId);
            var Contactos = context.catProveedorContacto.Where(p => p.provId == provId);
            var Productos = context.proCompraDetalle.Where(p => p.comId == comId && p.comdActivo);
            var Compra = context.proCompra.First(p => p.comId == comId);

            //Create new Excel workbook
            var workbook = new HSSFWorkbook();

            //Create new Excel sheet
            var sheet = workbook.CreateSheet("Proveedor");
            var headerRow1 = sheet.CreateRow(0);
            headerRow1.CreateCell(0).SetCellValue("Razón Social");
            headerRow1.CreateCell(1).SetCellValue("CUIT");
            headerRow1.CreateCell(2).SetCellValue("Domicilio");
            headerRow1.CreateCell(3).SetCellValue("Teléfono");
            headerRow1.CreateCell(4).SetCellValue("Correo Electrónico");
            headerRow1 = sheet.CreateRow(1);
            headerRow1.CreateCell(0).SetCellValue(Proveedor.provRazonSocial);
            headerRow1.CreateCell(1).SetCellValue(Proveedor.provCUIT);
            headerRow1.CreateCell(2).SetCellValue(Proveedor.provDomicilio);
            headerRow1.CreateCell(3).SetCellValue(Proveedor.provTelefono);
            headerRow1.CreateCell(4).SetCellValue(Proveedor.provCorreoElectronico);
            
            //Create a header row
            var sheetContacto = workbook.CreateSheet("Contactos");
            var headerRow = sheetContacto.CreateRow(0);
            //Set the column names in the header row
            headerRow.CreateCell(0).SetCellValue("Apellido y Nombre");
            headerRow.CreateCell(1).SetCellValue("Teléfono");
            headerRow.CreateCell(2).SetCellValue("Correo Electrónico");

            int rowNumber = 1;

            //Populate the sheet with values from the grid data
            foreach (catProveedorContacto _Info in Contactos)
            {
                //Create a new row
                var row = sheetContacto.CreateRow(rowNumber);

                //Set values for the cells
                row.CreateCell(0).SetCellValue(_Info.pcApellidoyNombre);
                row.CreateCell(1).SetCellValue(_Info.pcTelefono);
                row.CreateCell(2).SetCellValue(_Info.pcCorreoElectronico);

                rowNumber++;
            }

            //Create a header row
            var sheetProductos = workbook.CreateSheet("Productos");
            headerRow = sheetProductos.CreateRow(0);
            //Set the column names in the header row
            headerRow.CreateCell(0).SetCellValue("Producto");
            headerRow.CreateCell(1).SetCellValue("Cantidad");
            headerRow.CreateCell(2).SetCellValue("Precio Unitario");
            headerRow.CreateCell(3).SetCellValue("Marca / Tipo");
            headerRow.CreateCell(4).SetCellValue("Observaciones");

            rowNumber = 1;

            //Populate the sheet with values from the grid data
            foreach (proCompraDetalle _Info in Productos)
            {
                //Create a new row
                var row = sheetProductos.CreateRow(rowNumber);

                //Set values for the cells
                row.CreateCell(0).SetCellValue(_Info.comdDetalle);
                row.CreateCell(1).SetCellValue((double)_Info.comdCantidad);
                row.CreateCell(2).SetCellValue("");
                row.CreateCell(3).SetCellValue("");
                row.CreateCell(4).SetCellValue("");

                rowNumber++;
            }

            //Write the workbook to a memory stream
            string _NombreArchivo = "MSP PedidoDePresupuesto " + Compra.sisTipo.tipoDescripcion + " Nro " + Compra.comComprobante + "-Proveedor " + provId.ToString() + ".xls";
            string PathArchivos = Server.MapPath("~/Content/Archivos/Requerimientos");
            string physicalPath = Path.Combine(PathArchivos, _NombreArchivo);

            FileStream output = new FileStream(physicalPath, FileMode.Create);
            workbook.Write(output);
            output.Close();

            return _NombreArchivo;
        }

        public ActionResult ExportarPedidoDetalle(int comId)
        {
            var Productos = context.proCompraDetalle.Where(p => p.comId == comId && p.comdActivo);

            //Create new Excel workbook
            var workbook = new HSSFWorkbook();

            //Create a header row
            var sheetProductos = workbook.CreateSheet("Productos");
            var headerRow = sheetProductos.CreateRow(0);
            //Set the column names in the header row
            sheetProductos.SetColumnWidth(0, 50 * 256);
            headerRow.CreateCell(0).SetCellValue("Producto");
            headerRow.CreateCell(1).SetCellValue("Cantidad");
            headerRow.CreateCell(2).SetCellValue("Precio Unitario");
            headerRow.CreateCell(3).SetCellValue("SubTotal");

            int rowNumber = 1;

            //Populate the sheet with values from the grid data
            foreach (proCompraDetalle _Info in Productos)
            {
                //Create a new row
                var row = sheetProductos.CreateRow(rowNumber);

                //Set values for the cells
                row.CreateCell(0).SetCellValue(_Info.comdDetalle);
                row.CreateCell(1).SetCellValue((double)_Info.comdCantidad);
                row.CreateCell(2).SetCellValue((double)_Info.comdPrecioEstimado);
                row.CreateCell(3).SetCellValue((double)(_Info.comdCantidad * _Info.comdPrecioEstimado));

                rowNumber++;
            }

            //Write the workbook to a memory stream
            MemoryStream output = new MemoryStream();
            workbook.Write(output);

            string _NombreArchivo = "PedidosDeComprasProductos-" + DateTime.Now.Date.ToString("yyyyMMddhhmmss") + ".xls";
            //Return the result to the end user

            return File(output.ToArray(),   //The binary data of the XLS file
                "application/vnd.ms-excel", //MIME type of Excel files
                _NombreArchivo);     //Suggested file name in the "Save as" dialog which will be displayed to the end user
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult BindingContactos(int provId)
        {
            CargaContactos(provId);

            return Json((SelectList)ViewData["pcId_Data"]);
        }

    }
}