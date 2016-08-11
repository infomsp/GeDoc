using GeDoc.Models;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.Mvc;

namespace GeDoc.Controllers
{
    public class rptAPS_REDESController : Controller
    {
        private readonly efAccesoADatosEntities context = new efAccesoADatosEntities();

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /rptAPS_REDES/
        #region ExportarReportes

        public ActionResult ExportarReportes()
        {
            return View("ExportarReportes");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult exportDatosPersonalToExcel(DateTime? fDesde, DateTime? fHasta, bool? tipoFiltro)
        {


            //Create new Excel workbook
            var workbook = new HSSFWorkbook();


            #region Hoja "Encuestadores"
            //Get the data representing the current grid state - page, sort and filter
            var data1 = context.spRptAPS_REDES_DatosPersonal1(fDesde, fHasta, tipoFiltro).ToList();

            //Create new Excel sheet
            var sheet1 = workbook.CreateSheet("Encuestadores");

            //(Optional) set the width of the columns
            sheet1.SetColumnWidth(0, 40 * 256);
            sheet1.SetColumnWidth(1, 30 * 256);
            sheet1.SetColumnWidth(2, 15 * 256);
            sheet1.SetColumnWidth(3, 10 * 256);

            //Create a header row
            var headerRow1 = sheet1.CreateRow(0);

            //Set the column names in the header row
            headerRow1.CreateCell(0).SetCellValue("Encuestador");
            headerRow1.CreateCell(1).SetCellValue("Zona");
            headerRow1.CreateCell(2).SetCellValue("Departamento");
            headerRow1.CreateCell(3).SetCellValue("Nº de Encuestas");

            //(Optional) freeze the header row so it is not scrolled
            sheet1.CreateFreezePane(0, 1, 0, 1);

            int rowNumber = 1;
            //Populate the sheet with values from the grid data
            foreach (var _Info in data1)
            {
                //Create a new row
                var row = sheet1.CreateRow(rowNumber);

                //Set values for the cells
                row.CreateCell(0).SetCellValue(_Info.Encuestador);
                row.CreateCell(1).SetCellValue(_Info.Zona);
                row.CreateCell(2).SetCellValue(_Info.Departamento);
                row.CreateCell(3).SetCellValue(double.Parse(_Info.Nº_de_Encuestas.ToString()));

                rowNumber++;
            }
            #endregion


            #region Hoja "Data Entrys"
            //Get the data representing the current grid state - page, sort and filter
            var data2 = context.spRptAPS_REDES_DatosPersonal2(fDesde, fHasta, tipoFiltro).ToList();

            //Create new Excel sheet
            var sheet2 = workbook.CreateSheet("Data Entrys");

            //(Optional) set the width of the columns
            sheet2.SetColumnWidth(0, 40 * 256);
            sheet2.SetColumnWidth(1, 30 * 256);
            sheet2.SetColumnWidth(2, 15 * 256);
            sheet2.SetColumnWidth(3, 10 * 256);

            //Create a header row
            var headerRow2 = sheet2.CreateRow(0);

            //Set the column names in the header row
            headerRow2.CreateCell(0).SetCellValue("Encuestador");
            headerRow2.CreateCell(1).SetCellValue("Zona");
            headerRow2.CreateCell(2).SetCellValue("Departamento");
            headerRow2.CreateCell(3).SetCellValue("Nº de Encuestas");

            //(Optional) freeze the header row so it is not scrolled
            sheet2.CreateFreezePane(0, 1, 0, 1);

            int rowNumber2 = 1;
            //Populate the sheet with values from the grid data
            foreach (var _Info in data2)
            {
                //Create a new row
                var row = sheet2.CreateRow(rowNumber2);

                //Set values for the cells
                row.CreateCell(0).SetCellValue(_Info.Data_Entry);
                row.CreateCell(1).SetCellValue(_Info.Zona);
                row.CreateCell(2).SetCellValue(_Info.Departamento);
                row.CreateCell(3).SetCellValue(double.Parse(_Info.Nº_de_Encuestas.ToString()));

                rowNumber2++;
            }
            #endregion


            //Write the workbook to a memory stream
            MemoryStream output = new MemoryStream();
            workbook.Write(output);

            string _NombreArchivo = "Datos de Encuestas - DATOS PERSONAL (" + DateTime.Now.Date.ToString("yyyyMMddhhmmss") + ").xls";
            //Return the result to the end user

            Session["ExcelDownload"] = File(output.ToArray(),   //The binary data of the XLS file
                 "application/vnd.ms-excel", //MIME type of Excel files
                 _NombreArchivo);     //Suggested file name in the "Save as" dialog which will be displayed to the end user

            return Json(new { Ok = true });
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetDatosPersonalToExcel()
        {
            return (FileContentResult)Session["ExcelDownload"];
        }




        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult exportRangoEtarioToExcel(DateTime? fDesde, DateTime? fHasta, bool? tipoFiltro)
        {


            //Create new Excel workbook
            var workbook = new HSSFWorkbook();


            #region Hoja "Rango Etario"
            //Get the data representing the current grid state - page, sort and filter
            var data1 = context.spRptAPS_REDES_RangoEtario(fDesde, fHasta, tipoFiltro).ToList();

            //Create new Excel sheet
            var sheet1 = workbook.CreateSheet("Rango Etario");

            //(Optional) set the width of the columns
            sheet1.SetColumnWidth(0, 30 * 256);
            sheet1.SetColumnWidth(1, 30 * 256);
            sheet1.SetColumnWidth(2, 30 * 256);
            sheet1.SetColumnWidth(3, 30 * 256);
            sheet1.SetColumnWidth(4, 30 * 256);
            sheet1.SetColumnWidth(5, 15 * 256);
            sheet1.SetColumnWidth(6, 15 * 256);
            sheet1.SetColumnWidth(7, 15 * 256);
            sheet1.SetColumnWidth(8, 15 * 256);
            sheet1.SetColumnWidth(9, 15 * 256);
            sheet1.SetColumnWidth(10, 15 * 256);
            sheet1.SetColumnWidth(11, 15 * 256);
            sheet1.SetColumnWidth(12, 15 * 256);
            sheet1.SetColumnWidth(13, 15 * 256);
            sheet1.SetColumnWidth(14, 15 * 256);
            sheet1.SetColumnWidth(15, 15 * 256);
            sheet1.SetColumnWidth(16, 15 * 256);
            sheet1.SetColumnWidth(17, 15 * 256);
            sheet1.SetColumnWidth(18, 15 * 256);
            sheet1.SetColumnWidth(19, 15 * 256);
            sheet1.SetColumnWidth(20, 15 * 256);
            sheet1.SetColumnWidth(21, 25 * 256);

            //Create a header row
            var headerRow1 = sheet1.CreateRow(0);

            //Set the column names in the header row
            headerRow1.CreateCell(0).SetCellValue("Zona Sanitaria");
            headerRow1.CreateCell(1).SetCellValue("Departamento");
            headerRow1.CreateCell(2).SetCellValue("Centro de Salud");
            headerRow1.CreateCell(3).SetCellValue("Sexo");
            headerRow1.CreateCell(4).SetCellValue("Rango");
            headerRow1.CreateCell(5).SetCellValue("Total");
            headerRow1.CreateCell(6).SetCellValue("Alto");
            headerRow1.CreateCell(7).SetCellValue("Moderado");
            headerRow1.CreateCell(8).SetCellValue("Bajo");
            headerRow1.CreateCell(9).SetCellValue("Sin Dato");
            headerRow1.CreateCell(10).SetCellValue("No Aplica");
            headerRow1.CreateCell(11).SetCellValue("DBT");
            headerRow1.CreateCell(12).SetCellValue("HTA");
            headerRow1.CreateCell(13).SetCellValue("SOBREPESO");
            headerRow1.CreateCell(14).SetCellValue("TBC");
            headerRow1.CreateCell(15).SetCellValue("CELIAQUIA");
            headerRow1.CreateCell(16).SetCellValue("CANCER");
            headerRow1.CreateCell(17).SetCellValue("TABAQUISMO");
            headerRow1.CreateCell(18).SetCellValue("ITS");
            headerRow1.CreateCell(19).SetCellValue("EPOC/ASMA");
            headerRow1.CreateCell(20).SetCellValue("COLESTEROL");
            headerRow1.CreateCell(21).SetCellValue("ECV: ACV / Infarto");

            //(Optional) freeze the header row so it is not scrolled
            sheet1.CreateFreezePane(0, 1, 0, 1);

            int rowNumber = 1;
            //Populate the sheet with values from the grid data
            foreach (var _Info in data1)
            {
                //Create a new row
                var row = sheet1.CreateRow(rowNumber);

                //Set values for the cells
                row.CreateCell(0).SetCellValue(_Info.Zona_Sanitaria);
                row.CreateCell(1).SetCellValue(_Info.Departamento);
                row.CreateCell(2).SetCellValue(_Info.Centro_de_Salud);
                row.CreateCell(3).SetCellValue(_Info.Sexo);
                row.CreateCell(4).SetCellValue(_Info.Rango);
                row.CreateCell(5).SetCellValue(double.Parse(_Info.Total.ToString()));
                row.CreateCell(6).SetCellValue(double.Parse(_Info.Alto.ToString()));
                row.CreateCell(7).SetCellValue(double.Parse(_Info.Moderado.ToString()));
                row.CreateCell(8).SetCellValue(double.Parse(_Info.Bajo.ToString()));
                row.CreateCell(9).SetCellValue(double.Parse(_Info.Sin_Dato.ToString()));
                row.CreateCell(10).SetCellValue(double.Parse(_Info.No_Aplica.ToString()));
                row.CreateCell(11).SetCellValue(double.Parse(_Info.DBT.ToString()));
                row.CreateCell(12).SetCellValue(double.Parse(_Info.HTA.ToString()));
                row.CreateCell(13).SetCellValue(double.Parse(_Info.SOBREPESO.ToString()));
                row.CreateCell(14).SetCellValue(double.Parse(_Info.TBC.ToString()));
                row.CreateCell(15).SetCellValue(double.Parse(_Info.CELIAQUIA.ToString()));
                row.CreateCell(16).SetCellValue(double.Parse(_Info.CANCER.ToString()));
                row.CreateCell(17).SetCellValue(double.Parse(_Info.TABAQUISMO.ToString()));
                row.CreateCell(18).SetCellValue(double.Parse(_Info.ITS.ToString()));
                row.CreateCell(19).SetCellValue(double.Parse(_Info.EPOC_ASMA.ToString()));
                row.CreateCell(20).SetCellValue(double.Parse(_Info.COLESTEROL.ToString()));
                row.CreateCell(21).SetCellValue(double.Parse(_Info.ECV__ACV___Infarto.ToString()));

                rowNumber++;
            }
            #endregion


            //Write the workbook to a memory stream
            MemoryStream output = new MemoryStream();
            workbook.Write(output);

            string _NombreArchivo = "Datos de Encuestas - RANGO ETARIO (" + DateTime.Now.Date.ToString("yyyyMMddhhmmss") + ").xls";
            //Return the result to the end user

            Session["ExcelDownload"] = File(output.ToArray(),   //The binary data of the XLS file
                "application/vnd.ms-excel", //MIME type of Excel files
                _NombreArchivo);     //Suggested file name in the "Save as" dialog which will be displayed to the end user

            return Json(new { Ok = true });
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetRangoEtarioToExcel()
        {
            return (FileContentResult)Session["ExcelDownload"];
        }




        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult exportRedesToExcel(DateTime? fDesde, DateTime? fHasta, bool? tipoFiltro)
        {


            //Create new Excel workbook
            var workbook = new HSSFWorkbook();


            #region Hoja "REDES"
            //Get the data representing the current grid state - page, sort and filter
            var data1 = context.spRptAPS_REDES_Redes(fDesde, fHasta, tipoFiltro).ToList();

            //Create new Excel sheet
            var sheet1 = workbook.CreateSheet("REDES");

            //(Optional) set the width of the columns
            sheet1.SetColumnWidth(0, 15 * 256);
            sheet1.SetColumnWidth(1, 15 * 256);
            sheet1.SetColumnWidth(2, 15 * 256);
            sheet1.SetColumnWidth(3, 15 * 256);
            sheet1.SetColumnWidth(4, 15 * 256);
            sheet1.SetColumnWidth(5, 15 * 256);
            sheet1.SetColumnWidth(6, 15 * 256);
            sheet1.SetColumnWidth(7, 15 * 256);
            sheet1.SetColumnWidth(8, 15 * 256);
            sheet1.SetColumnWidth(9, 15 * 256);
            sheet1.SetColumnWidth(10, 15 * 256);
            sheet1.SetColumnWidth(11, 15 * 256);
            sheet1.SetColumnWidth(12, 15 * 256);
            sheet1.SetColumnWidth(13, 15 * 256);
            sheet1.SetColumnWidth(14, 15 * 256);
            sheet1.SetColumnWidth(15, 15 * 256);
            sheet1.SetColumnWidth(16, 15 * 256);
            sheet1.SetColumnWidth(17, 15 * 256);
            sheet1.SetColumnWidth(18, 15 * 256);
            sheet1.SetColumnWidth(19, 15 * 256);
            sheet1.SetColumnWidth(20, 15 * 256);
            sheet1.SetColumnWidth(21, 15 * 256);
            sheet1.SetColumnWidth(22, 15 * 256);
            sheet1.SetColumnWidth(23, 15 * 256);
            sheet1.SetColumnWidth(24, 15 * 256);
            sheet1.SetColumnWidth(25, 15 * 256);
            sheet1.SetColumnWidth(26, 15 * 256);
            sheet1.SetColumnWidth(27, 15 * 256);
            sheet1.SetColumnWidth(28, 15 * 256);
            sheet1.SetColumnWidth(29, 15 * 256);
            sheet1.SetColumnWidth(30, 15 * 256);
            sheet1.SetColumnWidth(31, 15 * 256);
            sheet1.SetColumnWidth(32, 15 * 256);
            sheet1.SetColumnWidth(33, 15 * 256);
            sheet1.SetColumnWidth(34, 15 * 256);
            sheet1.SetColumnWidth(35, 15 * 256);
            sheet1.SetColumnWidth(36, 15 * 256);
            sheet1.SetColumnWidth(37, 15 * 256);
            sheet1.SetColumnWidth(38, 15 * 256);
            sheet1.SetColumnWidth(39, 15 * 256);
            sheet1.SetColumnWidth(40, 15 * 256);
            sheet1.SetColumnWidth(41, 15 * 256);

            //Create a header row
            var headerRow1 = sheet1.CreateRow(0);

            //Set the column names in the header row
            headerRow1.CreateCell(0).SetCellValue("ENCUESTA");
            headerRow1.CreateCell(1).SetCellValue("TIPO DNI");
            headerRow1.CreateCell(2).SetCellValue("NRODOC");
            headerRow1.CreateCell(3).SetCellValue("APELLIDO");
            headerRow1.CreateCell(4).SetCellValue("NOMBRE");
            headerRow1.CreateCell(5).SetCellValue("SEXO");
            headerRow1.CreateCell(6).SetCellValue("OBRA_SOCIAL");
            headerRow1.CreateCell(7).SetCellValue("FECHA_NACIMIENTO");
            headerRow1.CreateCell(8).SetCellValue("EDAD");
            headerRow1.CreateCell(9).SetCellValue("DEPARTAMENTO");
            headerRow1.CreateCell(10).SetCellValue("CODIGO LOCALIDAD");
            headerRow1.CreateCell(11).SetCellValue("LOCALIDAD");
            headerRow1.CreateCell(12).SetCellValue("BARRIO");
            headerRow1.CreateCell(13).SetCellValue("CALLE");
            headerRow1.CreateCell(14).SetCellValue("NRO");
            headerRow1.CreateCell(15).SetCellValue("MANZANA");
            headerRow1.CreateCell(16).SetCellValue("SECTOR");
            headerRow1.CreateCell(17).SetCellValue("MONOBLOCK");
            headerRow1.CreateCell(18).SetCellValue("PISO");
            headerRow1.CreateCell(19).SetCellValue("FIJO");
            headerRow1.CreateCell(20).SetCellValue("CELULAR");
            headerRow1.CreateCell(21).SetCellValue("ENCUESTADOR");
            headerRow1.CreateCell(22).SetCellValue("FECHA RETIRA");
            headerRow1.CreateCell(23).SetCellValue("DATA ENTRY");
            headerRow1.CreateCell(24).SetCellValue("FECHA DE CARGA");
            headerRow1.CreateCell(25).SetCellValue("COD. AREA PROGRAMATICA");
            headerRow1.CreateCell(26).SetCellValue("AREA PROGRAMATICA");
            headerRow1.CreateCell(27).SetCellValue("COD AREA INFLUENCIA");
            headerRow1.CreateCell(28).SetCellValue("AREA INFLUENCIA");
            headerRow1.CreateCell(29).SetCellValue("VALOR RIESGO");
            headerRow1.CreateCell(30).SetCellValue("RIESGO");
            headerRow1.CreateCell(31).SetCellValue("DBT");
            headerRow1.CreateCell(32).SetCellValue("HTA");
            headerRow1.CreateCell(33).SetCellValue("SOBREPESO");
            headerRow1.CreateCell(34).SetCellValue("TBC");
            headerRow1.CreateCell(35).SetCellValue("CELIAQUIA");
            headerRow1.CreateCell(36).SetCellValue("CANCER");
            headerRow1.CreateCell(37).SetCellValue("TABAQUISMO");
            headerRow1.CreateCell(38).SetCellValue("ITS");
            headerRow1.CreateCell(39).SetCellValue("EPOC/ASMA");
            headerRow1.CreateCell(40).SetCellValue("COLESTEROL");
            headerRow1.CreateCell(41).SetCellValue("ECV: ACV / Infarto");

            //(Optional) freeze the header row so it is not scrolled
            sheet1.CreateFreezePane(0, 1, 0, 1);

            int rowNumber = 1;
            //Populate the sheet with values from the grid data
            foreach (var _Info in data1)
            {
                //Create a new row
                var row = sheet1.CreateRow(rowNumber);

                //Set values for the cells
                row.CreateCell(0).SetCellValue(double.Parse(_Info.ENCUESTA.ToString()));
                row.CreateCell(1).SetCellValue(_Info.TIPO_DNI);
                row.CreateCell(2).SetCellValue(_Info.NRODOC);
                row.CreateCell(3).SetCellValue(_Info.NOMBRE);
                row.CreateCell(4).SetCellValue(_Info.APELLIDO);
                row.CreateCell(5).SetCellValue(_Info.SEXO);
                row.CreateCell(6).SetCellValue(_Info.OBRA_SOCIAL);
                row.CreateCell(7).SetCellValue(_Info.FECHA_NACIMIENTO);
                row.CreateCell(8).SetCellValue(_Info.EDAD);
                row.CreateCell(9).SetCellValue(_Info.DEPARTAMENTO);
                row.CreateCell(10).SetCellValue(_Info.CODIGO_LOCALIDAD);
                row.CreateCell(11).SetCellValue(_Info.LOCALIDAD);
                row.CreateCell(12).SetCellValue(_Info.BARRIO);
                row.CreateCell(13).SetCellValue(_Info.CALLE);
                row.CreateCell(14).SetCellValue(_Info.NRO.ToString());
                row.CreateCell(15).SetCellValue(_Info.MANZANA);
                row.CreateCell(16).SetCellValue(_Info.SECTOR);
                row.CreateCell(17).SetCellValue(_Info.MONOBLOCK);
                row.CreateCell(18).SetCellValue(_Info.PISO);
                row.CreateCell(19).SetCellValue(_Info.FIJO);
                row.CreateCell(20).SetCellValue(_Info.CELULAR);
                row.CreateCell(21).SetCellValue(_Info.ENCUESTADOR);
                row.CreateCell(22).SetCellValue(_Info.FECHA_RETIRA.ToString());
                row.CreateCell(23).SetCellValue(_Info.DATA_ENTRY);
                row.CreateCell(24).SetCellValue(_Info.FECHA_DE_CARGA);
                row.CreateCell(25).SetCellValue(_Info.COD__AREA_PROGRAMATICA);
                row.CreateCell(26).SetCellValue(_Info.AREA_PROGRAMATICA);
                row.CreateCell(27).SetCellValue(_Info.COD_AREA_INFLUENCIA);
                row.CreateCell(28).SetCellValue(_Info.AREA_INFLUENCIA);
                row.CreateCell(29).SetCellValue(_Info.VALOR_RIESGO);
                row.CreateCell(30).SetCellValue(_Info.RIESGO);
                row.CreateCell(31).SetCellValue(_Info.DBT);
                row.CreateCell(32).SetCellValue(_Info.HTA);
                row.CreateCell(33).SetCellValue(_Info.SOBREPESO);
                row.CreateCell(34).SetCellValue(_Info.TBC);
                row.CreateCell(35).SetCellValue(_Info.CELIAQUIA);
                row.CreateCell(36).SetCellValue(_Info.CANCER);
                row.CreateCell(37).SetCellValue(_Info.TABAQUISMO);
                row.CreateCell(38).SetCellValue(_Info.ITS);
                row.CreateCell(39).SetCellValue(_Info.EPOC_ASMA);
                row.CreateCell(40).SetCellValue(_Info.COLESTEROL);
                row.CreateCell(41).SetCellValue(_Info.ECV__ACV___Infarto);


                rowNumber++;
            }
            #endregion


            //Write the workbook to a memory stream
            MemoryStream output = new MemoryStream();
            workbook.Write(output);

            string _NombreArchivo = "Datos de Encuestas - REDES (" + DateTime.Now.Date.ToString("yyyyMMddhhmmss") + ").xls";
            //Return the result to the end user

            Session["ExcelDownload"] = File(output.ToArray(),   //The binary data of the XLS file
                "application/vnd.ms-excel", //MIME type of Excel files
                _NombreArchivo);     //Suggested file name in the "Save as" dialog which will be displayed to the end user

            return Json(new { Ok = true });
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetRedesToExcel()
        {
            return (FileContentResult)Session["ExcelDownload"];
        }




        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult exportTotalesEncuestaToExcel(DateTime? fDesde, DateTime? fHasta, bool? tipoFiltro)
        {


            //Create new Excel workbook
            var workbook = new HSSFWorkbook();


            #region Hoja "TotalesEncuesta"
            //Get the data representing the current grid state - page, sort and filter
            var data1 = context.spRptAPS_REDES_TotalesEncuesta(fDesde, fHasta, tipoFiltro).ToList();

            //Create new Excel sheet
            var sheet1 = workbook.CreateSheet("Totales Encuesta");

            //(Optional) set the width of the columns
            sheet1.SetColumnWidth(0, 15 * 256);
            sheet1.SetColumnWidth(1, 15 * 256);
            sheet1.SetColumnWidth(2, 15 * 256);
            sheet1.SetColumnWidth(3, 15 * 256);
            sheet1.SetColumnWidth(4, 15 * 256);
            sheet1.SetColumnWidth(5, 15 * 256);
            sheet1.SetColumnWidth(6, 15 * 256);
            sheet1.SetColumnWidth(7, 15 * 256);
            sheet1.SetColumnWidth(8, 15 * 256);
            sheet1.SetColumnWidth(9, 15 * 256);
            sheet1.SetColumnWidth(10, 15 * 256);
            sheet1.SetColumnWidth(11, 15 * 256);
            sheet1.SetColumnWidth(12, 15 * 256);
            sheet1.SetColumnWidth(13, 15 * 256);
            sheet1.SetColumnWidth(14, 15 * 256);
            sheet1.SetColumnWidth(15, 15 * 256);
            sheet1.SetColumnWidth(16, 15 * 256);
            sheet1.SetColumnWidth(17, 15 * 256);
            sheet1.SetColumnWidth(18, 15 * 256);
            sheet1.SetColumnWidth(19, 15 * 256);
            sheet1.SetColumnWidth(20, 15 * 256);
            sheet1.SetColumnWidth(21, 15 * 256);
            sheet1.SetColumnWidth(22, 15 * 256);
            sheet1.SetColumnWidth(23, 15 * 256);
            sheet1.SetColumnWidth(24, 15 * 256);
            sheet1.SetColumnWidth(25, 15 * 256);
            sheet1.SetColumnWidth(26, 15 * 256);
            sheet1.SetColumnWidth(27, 15 * 256);
            sheet1.SetColumnWidth(28, 15 * 256);
            sheet1.SetColumnWidth(29, 15 * 256);
            sheet1.SetColumnWidth(30, 15 * 256);
            sheet1.SetColumnWidth(31, 15 * 256);
            sheet1.SetColumnWidth(32, 15 * 256);
            sheet1.SetColumnWidth(33, 15 * 256);
            sheet1.SetColumnWidth(34, 15 * 256);
            sheet1.SetColumnWidth(35, 15 * 256);
            sheet1.SetColumnWidth(36, 15 * 256);

            //Create a header row
            var headerRow1 = sheet1.CreateRow(0);

            //Set the column names in the header row
            headerRow1.CreateCell(0).SetCellValue("ZONA");
            headerRow1.CreateCell(1).SetCellValue("DEPARTAMENTO");
            headerRow1.CreateCell(2).SetCellValue("CENTRO DE SALUD");
            headerRow1.CreateCell(3).SetCellValue("VIVIENDAS");
            headerRow1.CreateCell(4).SetCellValue("MASCULINO");
            headerRow1.CreateCell(5).SetCellValue("FEMENINO");
            headerRow1.CreateCell(6).SetCellValue("TOTAL");
            headerRow1.CreateCell(7).SetCellValue("ENC_VIVIENDAS");
            headerRow1.CreateCell(8).SetCellValue("ENC_MASCULINO");
            headerRow1.CreateCell(9).SetCellValue("ENC_FEMENINO");
            headerRow1.CreateCell(10).SetCellValue("ENC_TOTAL");
            headerRow1.CreateCell(11).SetCellValue("ALTO");
            headerRow1.CreateCell(12).SetCellValue("MODERADO");
            headerRow1.CreateCell(13).SetCellValue("BAJO");
            headerRow1.CreateCell(14).SetCellValue("SIN DATO");
            headerRow1.CreateCell(15).SetCellValue("NO APLICA");
            headerRow1.CreateCell(16).SetCellValue("DBT");
            headerRow1.CreateCell(17).SetCellValue("HTA");
            headerRow1.CreateCell(18).SetCellValue("SOBREPESO");
            headerRow1.CreateCell(19).SetCellValue("TBC");
            headerRow1.CreateCell(20).SetCellValue("CELIAQUIA");
            headerRow1.CreateCell(21).SetCellValue("CANCER");
            headerRow1.CreateCell(22).SetCellValue("TABAQUISMO");
            headerRow1.CreateCell(23).SetCellValue("ITS");
            headerRow1.CreateCell(24).SetCellValue("EPOC/ASMA");
            headerRow1.CreateCell(25).SetCellValue("COLESTEROL");
            headerRow1.CreateCell(26).SetCellValue("ECV: ACV / Infarto");
            headerRow1.CreateCell(27).SetCellValue("M_ALTO");
            headerRow1.CreateCell(28).SetCellValue("M_MODERADO");
            headerRow1.CreateCell(29).SetCellValue("M_BAJO");
            headerRow1.CreateCell(30).SetCellValue("M_SIN DATO");
            headerRow1.CreateCell(31).SetCellValue("M_NO APLICA");
            headerRow1.CreateCell(32).SetCellValue("F_ALTO");
            headerRow1.CreateCell(33).SetCellValue("F_MODERADO");
            headerRow1.CreateCell(34).SetCellValue("F_BAJO");
            headerRow1.CreateCell(35).SetCellValue("F_SIN DATO");
            headerRow1.CreateCell(36).SetCellValue("F_NO APLICA");

            //(Optional) freeze the header row so it is not scrolled
            sheet1.CreateFreezePane(0, 1, 0, 1);

            int rowNumber = 1;
            //Populate the sheet with values from the grid data
            foreach (var _Info in data1)
            {
                //Create a new row
                var row = sheet1.CreateRow(rowNumber);

                //Set values for the cells
                row.CreateCell(0).SetCellValue(_Info.ZONA);
                row.CreateCell(1).SetCellValue(_Info.DEPARTAMENTO);
                row.CreateCell(2).SetCellValue(_Info.CENTRO_DE_SALUD);
                row.CreateCell(3).SetCellValue(double.Parse(_Info.VIVIENDAS.ToString()));
                row.CreateCell(4).SetCellValue(double.Parse(_Info.MASCULINO.ToString()));
                row.CreateCell(5).SetCellValue(double.Parse(_Info.FEMENINO.ToString()));
                row.CreateCell(6).SetCellValue(double.Parse(_Info.TOTAL.ToString()));
                row.CreateCell(7).SetCellValue(double.Parse(_Info.ENC_VIVIENDAS.ToString()));
                row.CreateCell(8).SetCellValue(double.Parse(_Info.ENC_MASCULINO.ToString()));
                row.CreateCell(9).SetCellValue(double.Parse(_Info.ENC_FEMENINO.ToString()));
                row.CreateCell(10).SetCellValue(double.Parse(_Info.ENC_TOTAL.ToString()));
                row.CreateCell(11).SetCellValue(double.Parse(_Info.ALTO.ToString()));
                row.CreateCell(12).SetCellValue(double.Parse(_Info.MODERADO.ToString()));
                row.CreateCell(13).SetCellValue(double.Parse(_Info.BAJO.ToString()));
                row.CreateCell(14).SetCellValue(double.Parse(_Info.SIN_DATO.ToString()));
                row.CreateCell(15).SetCellValue(double.Parse(_Info.NO_APLICA.ToString()));
                row.CreateCell(16).SetCellValue(double.Parse(_Info.DBT.ToString()));
                row.CreateCell(17).SetCellValue(double.Parse(_Info.HTA.ToString()));
                row.CreateCell(18).SetCellValue(double.Parse(_Info.SOBREPESO.ToString()));
                row.CreateCell(19).SetCellValue(double.Parse(_Info.TBC.ToString()));
                row.CreateCell(20).SetCellValue(double.Parse(_Info.CELIAQUIA.ToString()));
                row.CreateCell(21).SetCellValue(double.Parse(_Info.CANCER.ToString()));
                row.CreateCell(22).SetCellValue(double.Parse(_Info.TABAQUISMO.ToString()));
                row.CreateCell(23).SetCellValue(double.Parse(_Info.ITS.ToString()));
                row.CreateCell(24).SetCellValue(double.Parse(_Info.EPOC_ASMA.ToString()));
                row.CreateCell(25).SetCellValue(double.Parse(_Info.COLESTEROL.ToString()));
                row.CreateCell(26).SetCellValue(double.Parse(_Info.ECV__ACV_Infarto.ToString()));
                row.CreateCell(27).SetCellValue(double.Parse(_Info.M_ALTO.ToString()));
                row.CreateCell(28).SetCellValue(double.Parse(_Info.M_MODERADO.ToString()));
                row.CreateCell(29).SetCellValue(double.Parse(_Info.M_BAJO.ToString()));
                row.CreateCell(30).SetCellValue(double.Parse(_Info.M_SIN_DATO.ToString()));
                row.CreateCell(31).SetCellValue(double.Parse(_Info.M_NO_APLICA.ToString()));
                row.CreateCell(32).SetCellValue(double.Parse(_Info.F_ALTO.ToString()));
                row.CreateCell(33).SetCellValue(double.Parse(_Info.F_MODERADO.ToString()));
                row.CreateCell(34).SetCellValue(double.Parse(_Info.F_BAJO.ToString()));
                row.CreateCell(35).SetCellValue(double.Parse(_Info.F_SIN_DATO.ToString()));
                row.CreateCell(36).SetCellValue(double.Parse(_Info.F_NO_APLICA.ToString()));


                rowNumber++;
            }
            #endregion


            //Write the workbook to a memory stream
            MemoryStream output = new MemoryStream();
            workbook.Write(output);

            string _NombreArchivo = "Datos de Encuestas - TOTALES ENCUESTA (" + DateTime.Now.Date.ToString("yyyyMMddhhmmss") + ").xls";
            //Return the result to the end user

            Session["ExcelDownload"] = File(output.ToArray(),   //The binary data of the XLS file
                "application/vnd.ms-excel", //MIME type of Excel files
                _NombreArchivo);     //Suggested file name in the "Save as" dialog which will be displayed to the end user

            return Json(new { Ok = true });
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetTotalesEncuestaToExcel()
        {
            return (FileContentResult)Session["ExcelDownload"];
        }


        #endregion ExportarReportes


        public ActionResult rptDatosPersonal()
        {
            return View("rptDatosPersonal");
        }

        public ActionResult rptRangoEtario()
        {
            return View("rptRangoEtario");
        }

        public ActionResult rptRedes()
        {
            return View("rptRedes");
        }

        public ActionResult rptTotalesEncuesta()
        {
            return View("rptTotalesEncuesta");
        }

        public ActionResult getRptTotalesEncuesta(int page)
        {
            if (Session["UsuarioCentroDeSalud"] == null)
            {
                RedirectToAction("LogOff", "Account");
                return null;
            }

            var _NombreUsuario = (Session["Usuario"] as sisUsuario).usrNombreDeUsuario.ToString();

            var query = context.spRptAPS_REDES_TotalesEncuesta(null, null, null).ToList();


            var totalRecords = query.Count();

            Session["rptAPS"] = query;

            int i = 0;
            var ret = Json(new
            {
                graficos = getDatosGraficos(),
                page,
                rows = (from item in query
                        select new
                        {
                            id = getIndex(ref i),
                            cell = new string[] {
                                item.ZONA,
                                item.DEPARTAMENTO,
                                item.CENTRO_DE_SALUD,

                                item.VIVIENDAS.ToString(),
                                item.MASCULINO.ToString(),
                                item.FEMENINO.ToString(),
                                item.TOTAL.ToString(),

                                item.ENC_VIVIENDAS.ToString(),
                                item.ENC_MASCULINO.ToString(),
                                item.ENC_FEMENINO.ToString(),
                                item.ENC_TOTAL.ToString(),

                                item.ALTO.ToString(),
                                item.MODERADO.ToString(),
                                item.BAJO.ToString(),
                                item.SIN_DATO.ToString(),
                                item.NO_APLICA.ToString(),

                                item.DBT.ToString(),
                                item.HTA.ToString(),
                                item.SOBREPESO.ToString(),
                                item.TBC.ToString(),
                                item.CELIAQUIA.ToString(),
                                item.CANCER.ToString(),
                                item.TABAQUISMO.ToString(),
                                item.ITS.ToString(),
                                item.EPOC_ASMA.ToString(),
                                item.COLESTEROL.ToString(),
                                item.ECV__ACV_Infarto.ToString(),

                                item.M_ALTO.ToString(),
                                item.M_MODERADO.ToString(),
                                item.M_BAJO.ToString(),
                                item.M_SIN_DATO.ToString(),
                                item.M_NO_APLICA.ToString(),

                                item.F_ALTO.ToString(),
                                item.F_MODERADO.ToString(),
                                item.F_BAJO.ToString(),
                                item.F_SIN_DATO.ToString(),
                                item.F_NO_APLICA.ToString(),
                            }
                        }).ToList(),
            },
            JsonRequestBehavior.AllowGet);

            return ret;
        }



        private int getIndex(ref int i)
        {
            i++;
            return i;
        }


        private JsonResult getDatosGraficos(string filtroDpto = null)
        {
            if (Session["rptAPS"] == null)
            {
                return null;
            }
            else
            {

                System.Collections.Generic.List<spRptAPS_REDES_TotalesEncuesta_Result> datos;

                if (filtroDpto == null)
                {
                    datos = ((System.Collections.Generic.List<spRptAPS_REDES_TotalesEncuesta_Result>)Session["rptAPS"]);
                }
                else
                {
                    datos = ((System.Collections.Generic.List<spRptAPS_REDES_TotalesEncuesta_Result>)Session["rptAPS"]).Where(r => r.DEPARTAMENTO.Equals(filtroDpto)).ToList();
                };

                //Grafico Viviendas
                var chartViviendas = new[] { 
                        new { label = "Encuestadas", value = datos.Sum(r=>r.ENC_VIVIENDAS) }, 
                        new { label = "Sin Encuestar", value = datos.Sum(r=>r.VIVIENDAS) - datos.Sum(r=>r.ENC_VIVIENDAS) }
                    };

                //Grafico Viviendas
                var chartDolencias = new[] { 
                        new { label = "DBT", value = datos.Sum(r=>r.DBT) }, 
                        new { label = "HTA", value = datos.Sum(r=>r.HTA)},
                        new { label = "SOBREPESO", value = datos.Sum(r=>r.SOBREPESO) }, 
                        new { label = "TBC", value = datos.Sum(r=>r.TBC)},
                        new { label = "CELIAQUIA", value = datos.Sum(r=>r.CELIAQUIA)},
                        new { label = "CANCER", value = datos.Sum(r=>r.CANCER) }, 
                        new { label = "TABAQUISMO", value = datos.Sum(r=>r.TABAQUISMO)},
                        new { label = "ITS", value = datos.Sum(r=>r.ITS) }, 
                        new { label = "EPOC/ASMA", value = datos.Sum(r=>r.EPOC_ASMA)},
                        new { label = "COLESTEROL", value = datos.Sum(r=>r.COLESTEROL)},
                        new { label = "ECV/ACV/Infarto", value = datos.Sum(r=>r.ECV__ACV_Infarto)},
                    };


                //Grafico Nivel de Riesgo
                var chartNivelDeRiesgo = new[] { 
                        new { label = "Alto", value = datos.Sum(r=>r.ALTO) }, 
                        new { label = "Moderado", value = datos.Sum(r=>r.MODERADO)},
                        new { label = "Bajo", value = datos.Sum(r=>r.BAJO) }, 
                        new { label = "Moderado", value = datos.Sum(r=>r.SIN_DATO)}
                    };

                return Json(new
                {
                    chartViviendas = chartViviendas,
                    chartDolencias = chartDolencias,
                    chartNivelDeRiesgo = chartNivelDeRiesgo,
                });



            }
        }




    }

}
