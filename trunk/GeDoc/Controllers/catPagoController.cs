using System.IO;
using NPOI.HSSF.UserModel;
using Telerik.Web.Mvc.Extensions;

namespace GeDoc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Telerik.Web.Mvc;
    using GeDoc.Filters;
    using GeDoc.Models;

    public partial class catPagoController : Controller
    {
        private efAccesoADatosEntities context = new efAccesoADatosEntities();

        //Edición de datos

        [GridAction]
        public ActionResult _SelectEditing(string Filtro)
        {
            return View(new GridModel(All(Filtro)));
        }

        private IList<catPagoWS> All(string Filtro)
        {
            return getDatos(Filtro).ToList();
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
            var _Item = context.catPago.FirstOrDefault(p => p.pagId == id);
            var ItemPago = new catPagoWS();

            TryUpdateModel(_Item);
            TryUpdateModel(ItemPago);
            _Item.provId = ValidaProveedor(ItemPago);

            Edit(_Item);

            return View();
        }

        private int? ValidaProveedor(catPagoWS itemPago)
        {
            //Asigno el id de Proveedor si es que lo encuentra, si no lo agrega\\
            if (itemPago.Proveedor == null)
            {
                return null;
            }

            var Proveedor = context.catProveedor.FirstOrDefault(w => w.provRazonSocial == itemPago.Proveedor);
            int? provId;

            if (Proveedor != null)
            {
                provId = Proveedor.provId;
            }
            else
            {
                //No existe el proveedor, entonces lo agrego\\
                provId = null;
                try
                {
                    context.catProveedor.AddObject(new catProveedor() { provRazonSocial = itemPago.Proveedor, provNombreDeFantasia = itemPago.Proveedor, provRubro = "", provCUIT = "", provDomicilio = "", provTelefono = "", provCorreoElectronico = "" });
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("pagDetalle", ex.Message);
                    return null;
                }

                Proveedor = context.catProveedor.First(w => w.provRazonSocial == itemPago.Proveedor);
                if (Proveedor != null)
                {
                    provId = Proveedor.provId;
                }
            }

            return provId;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _InsertEditing()
        {
            var _Item = new catPago();

            if (TryUpdateModel(_Item))
            {
                if (Session["UsuarioCentroDeSalud"] == null)
                {
                    RedirectToAction("LogOff", "Account");
                    return null;
                }

                var ItemPago = new catPagoWS();

                TryUpdateModel(ItemPago);
                _Item.provId = ValidaProveedor(ItemPago);

                Create(_Item);
            }

            return View();
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
            DeleteConfirmed(id, false);

            return View();
        }

        private IEnumerable<catPagoWS> getDatos(string Filtro)
        {
            if (Session["UsuarioCentroDeSalud"] == null)
            {
                RedirectToAction("LogOff", "Account");
                return null;
            }
            var FechaActual = DateTime.Now.Date;
            if (Filtro != "~")
            {
                FechaActual = DateTime.Now.Date.AddYears(-100);
            }

            var _Datos = (from d in context.catPago.Where(w => w.pagFecha >= FechaActual).ToList()
                          select new catPagoWS()
                                    {
                                        pagDetalle = d.pagDetalle,
                                        pagExpediente = d.pagExpediente,
                                        pagFecha = d.pagFecha.Date,
                                        pagId = d.pagId,
                                        pagIdExpediente = d.pagIdExpediente,
                                        pagMonto = d.pagMonto,
                                        ceDescripcion = d.catCuentaEscritural.ceCodigo + "-" + d.catCuentaEscritural.ceDescripcion,
                                        ceId = d.ceId,
                                        provId = d.provId,
                                        Proveedor = d.provId == null ? "" : d.catProveedor.provRazonSocial,
                                        ProveedorNombreDeFantasia = d.provId == null ? "" : d.catProveedor.provNombreDeFantasia,
                                        ProveedorCUIT = d.provId == null ? "" : d.catProveedor.provCUIT,
                                        pagExpedienteCaratula = d.pagExpedienteCaratula,
                                        Fuente = d.catCuentaEscritural.catFuente.fteCodigo + "-" + d.catCuentaEscritural.catFuente.fteDescripcion
                                    }).ToList();

            Session["DatosDePagos"] = _Datos;
            return _Datos.AsEnumerable();
        }

        public ActionResult Index()
        {
            ViewBag.Catalogo = "Pagos";
            PopulateDropDownList();

            return View();
        }

        private void Create(catPago pItem)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    pItem.pagIdExpediente = (Session["ExpedienteEncontrado"] as vwExpedientes).Id;
                    pItem.pagExpedienteCaratula = (Session["ExpedienteEncontrado"] as vwExpedientes).iniciadorNombre + ", " + (Session["ExpedienteEncontrado"] as vwExpedientes).extracto;

                    context.catPago.AddObject(pItem);

                    new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catPago", "Agregar", "Agrega pago");

                    context.SaveChanges();

                    Session["ExpedienteEncontrado"] = null;
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("pagDetalle", ex.Message);
                }
            }
        }

        private void Edit(catPago pItem)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (pItem.pagIdExpediente != ((Session["ExpedienteEncontrado"] as vwExpedientes) == null ? pItem.pagIdExpediente : (Session["ExpedienteEncontrado"] as vwExpedientes).Id))
                    {
                        pItem.pagIdExpediente = (Session["ExpedienteEncontrado"] as vwExpedientes).Id;
                        pItem.pagExpedienteCaratula = (Session["ExpedienteEncontrado"] as vwExpedientes).iniciadorNombre + ", " + (Session["ExpedienteEncontrado"] as vwExpedientes).extracto;

                        Session["ExpedienteEncontrado"] = null;
                    }
                    //Registra log de usuario
                    new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catPago", "Modificar", "Modifica pago");

                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("pagDetalle", ex.Message);
                }
            }
            return;
        }

        private void DeleteConfirmed(int id, bool pEstado)
        {
            try
            {
                catPago _Item = context.catPago.Single(x => x.pagId == id);

                context.catPago.DeleteObject(_Item);
                //Registra log de usuario
                new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catPago", "Eliminar", "Elimina pago");
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("pagDetalle", ex.Message);
            }
        }

        //Datos para el dropdown
        protected void PopulateDropDownList()
        {
            var _CtasEs = (from p in context.catCuentaEscritural.ToList()
                           select new catCuentaEscritural
                           {
                               ceId = p.ceId,
                               ceDescripcion = p.ceCodigo.ToString() + "-" + p.ceDescripcion
                           }).ToList().OrderBy(p => p.ceDescripcion);

            ViewData["ceId_Data"] = new SelectList(_CtasEs, "ceId", "ceDescripcion");
            //ViewData["provId_Data"] = new SelectList(context.catProveedor, "provId", "provRazonSocial");
            ViewData["Proveedor_Data"] = context.catProveedor.Select(s => s.provRazonSocial);
        }

        public ActionResult Exportar(int page, string orderBy, string filter)
        {
            //Get the data representing the current grid state - page, sort and filter
            GridModel model = (Session["DatosDePagos"] as List<catPagoWS>).AsQueryable().ToGridModel(page, 9999999, orderBy, string.Empty, filter);
            var _Datos = model.Data.Cast<catPagoWS>();
            //var _Datos = (Session["DatosDePagos"] as List<catPagoWS>);

            //Create new Excel workbook
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

            //Create a header row
            var headerRow = sheet.CreateRow(0);

            //Set the column names in the header row
            headerRow.CreateCell(0).SetCellValue("Fecha");
            headerRow.CreateCell(1).SetCellValue("N° Expediente");
            headerRow.CreateCell(2).SetCellValue("Carátula Expediente");
            headerRow.CreateCell(3).SetCellValue("Proveedor");
            headerRow.CreateCell(4).SetCellValue("Proveedor CUIT");
            headerRow.CreateCell(5).SetCellValue("Detalle");
            headerRow.CreateCell(6).SetCellValue("Cuenta Escritural");
            headerRow.CreateCell(7).SetCellValue("Fuente");
            headerRow.CreateCell(8).SetCellValue("Importe");
            sheet.CreateFreezePane(0, 1, 0, 1);
            int rowNumber = 1;

            //pagDetalle = d.pagDetalle,
            //pagExpediente = d.pagExpediente,
            //pagFecha = d.pagFecha.Date,
            //pagMonto = d.pagMonto,
            //ceDescripcion = d.catCuentaEscritural.ceCodigo + "-" + d.catCuentaEscritural.ceDescripcion,
            //Proveedor = d.provId == null ? "" : d.catProveedor.provRazonSocial,
            //ProveedorNombreDeFantasia = d.provId == null ? "" : d.catProveedor.provNombreDeFantasia,
            //pagExpedienteCaratula = d.pagExpedienteCaratula
            //provCUIT


            //Populate the sheet with values from the grid data
            foreach (catPagoWS _Info in _Datos)
            {
                //Create a new row
                var row = sheet.CreateRow(rowNumber);

                //Set values for the cells   == null ? "" : _Info.perFechaNacimiento.Value.ToString("dd/MM/yyyy")
                row.CreateCell(0).SetCellValue(_Info.pagFecha.ToString("dd/MM/yyyy"));
                row.CreateCell(1).SetCellValue(_Info.pagExpediente);
                row.CreateCell(2).SetCellValue(_Info.pagExpedienteCaratula);
                row.CreateCell(3).SetCellValue(_Info.Proveedor);
                row.CreateCell(4).SetCellValue(_Info.ProveedorCUIT);
                row.CreateCell(5).SetCellValue(_Info.pagDetalle);
                row.CreateCell(6).SetCellValue(_Info.ceDescripcion);
                row.CreateCell(7).SetCellValue(_Info.Fuente);
                row.CreateCell(8).SetCellValue((double)_Info.pagMonto);

                rowNumber++;
            }

            //Write the workbook to a memory stream
            MemoryStream output = new MemoryStream();
            workbook.Write(output);

            string _NombreArchivo = "PagosTesoreria-" + DateTime.Now.Date.ToString("yyyyMMddhhmmss") + ".xls";
            //Return the result to the end user

            return File(output.ToArray(),   //The binary data of the XLS file
                "application/vnd.ms-excel", //MIME type of Excel files
                _NombreArchivo);     //Suggested file name in the "Save as" dialog which will be displayed to the end user
        }

    }
}