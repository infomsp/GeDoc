using System.IO;
using NPOI.HSSF.UserModel;
using Telerik.Web.Mvc.Extensions;

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

    public class catCreditoAnualController : Controller
    {
        private efAccesoADatosEntities context = new efAccesoADatosEntities();

        //Edición de datos

        [GridAction]
        public ActionResult _SelectEditing()
        {
            return View(new GridModel(All()));
        }

        private IList<catCreditosAnuales> All()
        {
            return getDatos().ToList();
        }

        private catCreditoAnual AsignaDatos(catCreditosAnuales pItem)
        {
            catCreditoAnual _Item2 = new catCreditoAnual();
            if (pItem.creId != 0 && pItem.creId != null)
            {
                _Item2 = context.catCreditoAnual.Where(p => p.creId == pItem.creId).FirstOrDefault();
            }

            _Item2.creFecha = pItem.creFecha;
            _Item2.creId = pItem.creId;
            _Item2.creImporte = pItem.creImporte;
            _Item2.fteId = pItem.fteId;
            _Item2.ccId = pItem.ccId;
            _Item2.creResolucion = pItem.creResolucion;
            _Item2.partId = pItem.partId;
            _Item2.creObservaciones = pItem.creObservaciones;

            return _Item2;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _SaveEditing(int id)
        {
            catCreditosAnuales _Item = new catCreditosAnuales();

            if (TryUpdateModel(_Item))
            {
                _Item.creId = id;
                Edit(AsignaDatos(_Item));
            }

            return View(new GridModel(All()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _InsertEditing()
        {
            catCreditosAnuales _Item = new catCreditosAnuales();

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

        private IEnumerable<catCreditosAnuales> getDatos()
        {
            var icId = (Session["Usuario"] as sisUsuario).icId;
            var _Datos = (from d in context.catCreditoAnual.Where(w => w.icId == icId)
                          select new catCreditosAnuales()
                          {
                              creFecha = d.creFecha,
                              creId = d.creId,
                              creImporte = d.creImporte,
                              fteId = d.fteId,
                              fteDescripcion = d.catFuente.fteCodigo + "-" + d.catFuente.fteDescripcion,
                              ccId = d.ccId,
                              ccDescripcion = d.catCuentaContable.ccCodigo + "-" + d.catCuentaContable.ccDescripcion,
                              partId = d.partId,
                              partNombre = d.catPartida.partNombre,
                              creResolucion = d.creResolucion,
                              creObservaciones = d.creObservaciones
                          }).ToList();

            return _Datos.AsEnumerable();
        }

        public ActionResult Index()
        {
            ViewBag.Catalogo = "Créditos Anuales";

            PopulateDropDownList();

            return PartialView();
        }

        private void Create(catCreditoAnual pItem)
        {
            if (ModelState.IsValid)
            {
                pItem.icId = (Session["Usuario"] as sisUsuario).icId;
                context.catCreditoAnual.AddObject(pItem);
                context.SaveChanges();
            }

            return;
        }

        private void Edit(catCreditoAnual pItem)
        {
            if (ModelState.IsValid)
            {
                context.SaveChanges();
            }
            return;
        }

        private void DeleteConfirmed(int id)
        {
            catCreditoAnual _Item = context.catCreditoAnual.Single(x => x.creId == id);
            context.catCreditoAnual.DeleteObject(_Item);
            context.SaveChanges();
        }

        //Datos para el dropdown
        protected void PopulateDropDownList()
        {
            var icId = (Session["Usuario"] as sisUsuario).icId;
            var _Fuentes = (from p in context.catFuente.Where(w => w.icId == icId).ToList()
                            select new catFuente
                            {
                                fteId = p.fteId,
                                fteDescripcion = p.fteCodigo.ToString() + "-" + p.fteDescripcion
                            }).ToList().OrderBy(p => p.fteDescripcion);

            var _Cuentas = (from p in context.catCuentaContable.Where(w => w.icId == icId).ToList()
                            select new catCuentaContable
                            {
                                ccId = p.ccId,
                                ccDescripcion = p.ccCodigo.ToString() + "-" + p.ccDescripcion
                            }).ToList().OrderBy(p => p.ccDescripcion);

            ViewData["fteId_Data"] = new SelectList(_Fuentes, "fteId", "fteDescripcion");
            ViewData["ccId_Data"] = new SelectList(_Cuentas, "ccId", "ccDescripcion");
            ViewData["partId_Data"] = new SelectList(context.catPartida.Where(w => w.icId == icId), "partId", "partNombre");

            return;
        }

        public ActionResult Exportar(int page, string orderBy, string filter)
        {
            //Get the data representing the current grid state - page, sort and filter
            GridModel model = getDatos().AsQueryable().ToGridModel(page, 9999999, orderBy, string.Empty, filter);
            var _Datos = model.Data.Cast<catCreditosAnuales>();

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

            //Create a header row
            var headerRow = sheet.CreateRow(0);

            //Set the column names in the header row
            headerRow.CreateCell(0).SetCellValue("Fuente");
            headerRow.CreateCell(1).SetCellValue("Estructura Programática");
            headerRow.CreateCell(2).SetCellValue("Partida Presupuestaria");
            headerRow.CreateCell(3).SetCellValue("Resolución");
            headerRow.CreateCell(4).SetCellValue("Fecha");
            headerRow.CreateCell(5).SetCellValue("Importe");
            headerRow.CreateCell(6).SetCellValue("Observaciones");
            sheet.CreateFreezePane(0, 1, 0, 1);
            int rowNumber = 1;

            //Populate the sheet with values from the grid data
            foreach (catCreditosAnuales _Info in _Datos)
            {
                //Create a new row
                var row = sheet.CreateRow(rowNumber);

                //Set values for the cells
                row.CreateCell(0).SetCellValue(_Info.fteDescripcion);
                row.CreateCell(1).SetCellValue(_Info.ccDescripcion);
                row.CreateCell(2).SetCellValue(_Info.partNombre);
                row.CreateCell(3).SetCellValue("SHF-" + _Info.creResolucion + _Info.creFecha.ToString("yyyy"));
                row.CreateCell(4).SetCellValue(_Info.creFecha.ToString("dd/MM/yyyy"));
                row.CreateCell(5).SetCellValue((double)_Info.creImporte);
                row.CreateCell(6).SetCellValue(_Info.creObservaciones);

                rowNumber++;
            }

            //Write the workbook to a memory stream
            MemoryStream output = new MemoryStream();
            workbook.Write(output);

            string _NombreArchivo = "CreditosAnuales-" + DateTime.Now.Date.ToString("yyyyMMddhhmmss") + ".xls";
            //Return the result to the end user

            return File(output.ToArray(),   //The binary data of the XLS file
                "application/vnd.ms-excel", //MIME type of Excel files
                _NombreArchivo);     //Suggested file name in the "Save as" dialog which will be displayed to the end user
        }


    }
}