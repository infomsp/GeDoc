using GeDoc.Models.JuntaMedica.Modelo;
using NPOI.HSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telerik.Web.Mvc;
using Telerik.Web.Mvc.Extensions;

namespace GeDoc.Controllers.JuntaMedica.Estadisticas
{
    public class ListadoDeAtencionController : Controller
    {
        private efAccesoADatosJMJuntaMedicaEntities context = new efAccesoADatosJMJuntaMedicaEntities();
        //
        // GET: /ListadoDeAtencion/

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult ListadoDeAtencion()
        {
            ViewBag.Catalogo = "Listado de Atención Médica";

            return PartialView("~/Views/JuntaMedica/ListadoDeAtencion.cshtml");
        }

        [GridAction]
        public ActionResult getListadoDeListadoDeAtencion(DateTime FechaDesde, DateTime FechaHasta)
        {
            var _Datos = context.getEstadisticaDCRM(FechaDesde, FechaHasta).ToList();
            Session["ListaEstadisticaDCRM"] = _Datos;

            return PartialView(new GridModel(_Datos));
        }

        public ActionResult ExportarEspecial(string Filtro, string Orden)
        {
            var InfoCM = (Session["ListaEstadisticaDCRM"] as List<getEstadisticaDCRM_Result>);
            GridModel model = InfoCM.AsQueryable().ToGridModel(1, 9999999, Orden, string.Empty, Filtro);
            var _Datos = model.Data.Cast<getEstadisticaDCRM_Result>();

            var workbook = new HSSFWorkbook();

            //Create new Excel sheet
            var sheet = workbook.CreateSheet();
            //(Optional) set the width of the columns
            sheet.SetColumnWidth(0, 30 * 256);
            sheet.SetColumnWidth(1, 30 * 256);
            sheet.SetColumnWidth(2, 30 * 256);
            sheet.SetColumnWidth(3, 10 * 256);
            sheet.SetColumnWidth(4, 20 * 256);
            sheet.SetColumnWidth(5, 20 * 256);
            sheet.SetColumnWidth(6, 20 * 256);
            sheet.SetColumnWidth(7, 20 * 256);
            sheet.SetColumnWidth(8, 20 * 256);
            sheet.SetColumnWidth(9, 20 * 256);
            sheet.SetColumnWidth(10, 20 * 256);
            sheet.SetColumnWidth(11, 20 * 256);
            sheet.SetColumnWidth(12, 20 * 256);
            sheet.SetColumnWidth(13, 20 * 256);

            //Create a header row
            var headerRow = sheet.CreateRow(0);

            //Set the column names in the header row
            headerRow.CreateCell(0).SetCellValue("Médico");
            headerRow.CreateCell(1).SetCellValue("Especialidad Médico");
            headerRow.CreateCell(2).SetCellValue("Especialidad Carta Médica");
            headerRow.CreateCell(3).SetCellValue("Fecha");
            headerRow.CreateCell(4).SetCellValue("Mín. Tiempo de Espera");
            headerRow.CreateCell(5).SetCellValue("Prom. Tiempo de Espera");
            headerRow.CreateCell(6).SetCellValue("Máx. Tiempo de Espera");
            headerRow.CreateCell(7).SetCellValue("Mín. Tiempo de At.");
            headerRow.CreateCell(8).SetCellValue("Prom. Tiempo de At.");
            headerRow.CreateCell(9).SetCellValue("Máx. Tiempo de At.");
            headerRow.CreateCell(10).SetCellValue("Mín. Tiempo Ocioso");
            headerRow.CreateCell(11).SetCellValue("Prom. Tiempo Ocioso");
            headerRow.CreateCell(12).SetCellValue("Máx. Tiempo Ocioso");
            headerRow.CreateCell(13).SetCellValue("Cantidad");
            sheet.CreateFreezePane(0, 1, 0, 1);
            int rowNumber = 1;

            //Populate the sheet with values from the grid data
            foreach (getEstadisticaDCRM_Result _Info in _Datos)
            {
                //Create a new row
                var row = sheet.CreateRow(rowNumber);

                //Set values for the cells
                row.CreateCell(0).SetCellValue(_Info.Medico);
                row.CreateCell(1).SetCellValue(_Info.EspecialidadMedico);
                row.CreateCell(2).SetCellValue(_Info.EspecialidadCM);
                row.CreateCell(3).SetCellValue(_Info.Fecha.Value.ToString("dd/MM/yyyy"));
                row.CreateCell(4).SetCellValue(_Info.MinimoTiempoDeEspera.ToString());
                row.CreateCell(5).SetCellValue(_Info.PromedioTiempoDeEspera.ToString());
                row.CreateCell(6).SetCellValue(_Info.MaximoTiempoDeEspera.ToString());
                row.CreateCell(7).SetCellValue(_Info.MinimoTiempoDeAtencion.ToString());
                row.CreateCell(8).SetCellValue(_Info.PromedioTiempoDeAtencion.ToString());
                row.CreateCell(9).SetCellValue(_Info.MaximoTiempoDeAtencion.ToString());
                row.CreateCell(10).SetCellValue(_Info.MinimoTiempoOcioso.ToString());
                row.CreateCell(11).SetCellValue(_Info.PromedioTiempoOcioso.ToString());
                row.CreateCell(12).SetCellValue(_Info.MaximoTiempoOcioso.ToString());
                row.CreateCell(13).SetCellValue(_Info.CantidadDeAtenciones.ToString());

                rowNumber++;
            }

            //Write the workbook to a memory stream
            MemoryStream output = new MemoryStream();
            workbook.Write(output);

            string _NombreArchivo = "ListadoDeAtencion-" + DateTime.Now.Date.ToString("yyyyMMddhhmmss") + ".xls";
            //Return the result to the end user

            return File(output.ToArray(),   //The binary data of the XLS file
                "application/vnd.ms-excel", //MIME type of Excel files
                _NombreArchivo);     //Suggested file name in the "Save as" dialog which will be displayed to the end user
        }
    }
}
