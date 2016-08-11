
using System.Web.UI;
using System.Web.UI.WebControls;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.HSSF.Util;
using NPOI.HPSF;
using NPOI.POIFS.FileSystem;
using NPOI.SS.Util;
//using Spire.Xls;


namespace GeDoc{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Telerik.Web.Mvc;
    using Telerik.Web.Mvc.Extensions;
    using GeDoc.Filters;
    using GeDoc.Models;
    using System.IO;
    //using Excel = Microsoft.Office.Interop.Excel;
    //using Microsoft.Office.Interop.Excel;
    using System.Xml.Serialization;
    using iTextSharp.text;
    using System.Data;
    using System.Drawing;
    using System.Data.SqlClient;
    using System.Configuration;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.IO;
    using System.Data;
    using System.Drawing;
    using System.Data.SqlClient;
    using System.Configuration;


    public partial class proEstadisticaPlanillaController : Controller
    {
        private efAccesoADatosEntities context = new efAccesoADatosEntities();

        //Edición de datos

        [GridAction]
        public ActionResult _SelectEditing()
        {
            return View(new GridModel(All()));
        }

        private IEnumerable<proEstadisticaPlanillas> All()
        {
            return getDatos().ToList();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _SaveEditing(int id)
        {
            proEstadisticaPlanilla _Item = context.proEstadisticaPlanilla.Where(p => p.plaId == id).FirstOrDefault();

            TryUpdateModel(_Item);

            Edit(_Item);

            return View(new GridModel(All()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _InsertEditing()
        {
            proEstadisticaPlanilla _Item = new proEstadisticaPlanilla();

            if (TryUpdateModel(_Item))
            {
                proEstadisticaPlanilla _datos =
                    context.proEstadisticaPlanilla.FirstOrDefault(p => p.csId == _Item.csId & p.plaMes == _Item.plaMes & p.plaAnio == _Item.plaAnio);
                if (_datos == null)
                {
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
        private IEnumerable<proEstadisticaPlanillas> getDatos()
        {
            var _csId = (Session["UsuarioCentroDeSalud"] as GeDoc.sisUsuariosCentroDeSalud).csId;
            var _Datos = (from d in context.proEstadisticaPlanilla.ToList()
                          where d.csId == _csId
                select new proEstadisticaPlanillas()
                {
                    plaId = d.plaId,
                    depId = d.depId,
                    departamentoTexto = d.catDepartamento.depNombre,
                    csId = d.csId,
                    centroSaludTexto = d.catCentroDeSalud.csNombre,
                    plaFecha = d.plaFecha,
                    usrId = d.usrId,
                    plaMes = d.plaMes,
                    plaAnio = d.plaAnio,
                    plaMesTexto = getMes((short)d.plaMes)
                    
                }).ToList();
          
            return _Datos.AsEnumerable();
        }
        public ActionResult Index()
        {
            ViewBag.Catalogo = "RESUMEN DIARIO MENSUAL DE CONSULTAS MEDICAS";
            ViewData["FiltroContains"] = true;
            PopulateDropDownList();
            return PartialView();
        }
        public string getMes(int _mes)
        {
            switch (_mes)
            {
                case 1:
                    return ("Enero");
                    break;
                case 2:
                    return ("Febrero");
                    break;
                case 3:
                    return ("Marzo");
                    break;
                case 4:
                    return ("Abril");
                    break;
                case 5:
                    return ("Mayo");
                    break;
                case 6:
                    return ("Junio");
                    break;
                case 7:
                    return ("Julio");
                    break;
                case 8:
                    return ("Agosto");
                    break;
                case 9:
                    return ("Septiembre");
                    break;
                case 10:
                    return ("Octubre");
                    break;
                case 11:
                    return ("Noviembre");
                    break;
                case 12:
                    return ("Diciembre");
                    break;
              
            }

            return "";
        }

        //[AcceptVerbs(HttpVerbs.Post)]
        //[GridAction]
        public ActionResult Exportar(int plaId, int espId )
        {
            //Get the data representing the current grid state - page, sort and filter
            GridModel model = getDatosValores(plaId, espId).AsQueryable().ToGridModel(1, 31, "", string.Empty, "");
            var _Datos = model.Data.Cast<proEstadisticaResultadosRHS>();
             var _item = (from d in context.proEstadisticaPlanilla
                         where d.plaId == plaId
                         select new proEstadisticaPlanillas()
                         {
                             plaId = d.plaId,
                             depId = d.catDepartamento.depId,
                             departamentoTexto = d.catDepartamento.depNombre,
                             csId = d.catCentroDeSalud.csId,
                             centroSaludTexto = d.catCentroDeSalud.csNombre,
                             plaFecha = d.plaFecha,
                             usrId = d.sisUsuario.usrId,
                             plaMes = d.plaMes,
                             plaAnio = d.plaAnio
                         }).FirstOrDefault();
            //Create new Excel workbook
              var _especialidad = (from d in context.catEspecialidad
                where d.espId == espId
                select new catEspecialidades()
                {
                    espNombre = d.espNombre,
                    espCodigo = d.espCodigo,
                    espIdPadre = d.espIdPadre,
                    espNombrePadre = d.espNombre,
                    espId = d.espId
                }).FirstOrDefault();
            var workbook = new HSSFWorkbook();

            //Create new Excel sheet
            var sheet = workbook.CreateSheet();

            //(Optional) set the width of the columns
            sheet.SetColumnWidth(0,3 *256);
            sheet.SetColumnWidth(1,3 *256);
            sheet.SetColumnWidth(2,3 *256);
            sheet.SetColumnWidth(3,3 *256);
            sheet.SetColumnWidth(4,3 *256);
            sheet.SetColumnWidth(5,3 *256);
            sheet.SetColumnWidth(6,3 *256);
            sheet.SetColumnWidth(7,3 *256);
            sheet.SetColumnWidth(8,3 *256);
            sheet.SetColumnWidth(9,3 *256);
            sheet.SetColumnWidth(10,3 *256);
            sheet.SetColumnWidth(11,3 *256);
            sheet.SetColumnWidth(12,3 *256);
            sheet.SetColumnWidth(13,3 *256);
            sheet.SetColumnWidth(14,3 *256);
            sheet.SetColumnWidth(15,3 *256);
            sheet.SetColumnWidth(16,3 *256);
            sheet.SetColumnWidth(17,3 *256);

            sheet.SetColumnWidth(18, 3 * 256);
            sheet.SetColumnWidth(19, 4 * 256);
            sheet.SetColumnWidth(20, 3 * 256);
            sheet.SetColumnWidth(21, 5 * 256);
            sheet.SetColumnWidth(22, 5 * 256);
            sheet.SetColumnWidth(23, 5 * 256);
            sheet.SetColumnWidth(24, 5 * 256);
            sheet.SetColumnWidth(25, 5 * 256);
            sheet.SetColumnWidth(26, 5 * 256);
            sheet.SetColumnWidth(27, 5 * 256);


            // Crea bordes
            ICellStyle style = workbook.CreateCellStyle();
            style.BorderBottom = CellBorderType.THIN;
            style.BottomBorderColor = HSSFColor.BLACK.index;
            style.BorderLeft = CellBorderType.THIN;
            style.LeftBorderColor = HSSFColor.BLACK.index;
            style.BorderRight = CellBorderType.THIN;
            style.RightBorderColor = HSSFColor.BLACK.index;
            style.BorderTop = CellBorderType.THIN;
            style.TopBorderColor = HSSFColor.BLACK.index;
            for (int iRow = 10; iRow <= 45; iRow++)
            {
                IRow row = sheet.CreateRow(iRow);
                for (int iCol = 0; iCol <= 26; iCol++)
                {
                    //the first cell of each row * the first cell of each column
                    // string formula = GetCellPosition(iRow, 0) + "*" + GetCellPosition(0, iCol);
                    var cell = row.CreateCell(iCol);
                    //cell.CellFormula = formula;
                    //set the cellstyle to the cell
                    cell.CellStyle = style;
                }
            }

            //estilo de titulo
            var font1 = workbook.CreateFont();
            font1.Color = HSSFColor.BLACK.index;
            font1.Boldweight = (short)FontBoldWeight.BOLD;

            var font2 = workbook.CreateFont();
            font2.Color = HSSFColor.BLACK.index;
            font2.Boldweight = (short)FontBoldWeight.BOLD;
            font2.FontHeightInPoints = 10;
        
           // font1.FontHeight = 2;
            //  font1.Underline = (byte)FontUnderlineType.DOUBLE;
          //  font1.FontHeightInPoints = 10;

            var style1 = workbook.CreateCellStyle();
            style1.SetFont(font1);

            style1.BorderBottom = CellBorderType.THIN;
            style1.BottomBorderColor = HSSFColor.BLACK.index;
            style1.BorderLeft = CellBorderType.THIN;
            style1.LeftBorderColor = HSSFColor.BLACK.index;
            style1.BorderRight = CellBorderType.THIN;
            style1.RightBorderColor = HSSFColor.BLACK.index;
            style1.BorderTop = CellBorderType.THIN;
            style1.TopBorderColor = HSSFColor.BLACK.index;

            var style2 = workbook.CreateCellStyle();
            style1.SetFont(font2);

            style1.BorderBottom = CellBorderType.THIN;
            style1.BottomBorderColor = HSSFColor.BLACK.index;
            style1.BorderLeft = CellBorderType.THIN;
            style1.LeftBorderColor = HSSFColor.BLACK.index;
            style1.BorderRight = CellBorderType.THIN;
            style1.RightBorderColor = HSSFColor.BLACK.index;
            style1.BorderTop = CellBorderType.THIN;
            style1.TopBorderColor = HSSFColor.BLACK.index;

            var bordeTopGrueso = workbook.CreateCellStyle();
            bordeTopGrueso.SetFont(font1);
            bordeTopGrueso.BorderTop = CellBorderType.THICK;
            bordeTopGrueso.TopBorderColor = HSSFColor.BLACK.index;
            bordeTopGrueso.BorderBottom = CellBorderType.THIN;
            bordeTopGrueso.BottomBorderColor = HSSFColor.BLACK.index;
            bordeTopGrueso.BorderLeft = CellBorderType.THIN;
            bordeTopGrueso.LeftBorderColor = HSSFColor.BLACK.index;
            bordeTopGrueso.BorderRight = CellBorderType.THIN;
            bordeTopGrueso.RightBorderColor = HSSFColor.BLACK.index;

            var bordeBottomGrueso = workbook.CreateCellStyle();
            bordeBottomGrueso.SetFont(font1);
            bordeBottomGrueso.BorderBottom = CellBorderType.THICK;
            bordeBottomGrueso.BottomBorderColor = HSSFColor.BLACK.index;


            var bordeBottomyTopGrueso = workbook.CreateCellStyle();
            bordeBottomyTopGrueso.SetFont(font1);
            bordeBottomyTopGrueso.BorderBottom = CellBorderType.THICK;
            bordeBottomyTopGrueso.BottomBorderColor = HSSFColor.BLACK.index;
            bordeBottomyTopGrueso.BorderTop = CellBorderType.THICK;
            bordeBottomyTopGrueso.TopBorderColor = HSSFColor.BLACK.index;

            var borderightGrueso = workbook.CreateCellStyle();
            borderightGrueso.SetFont(font1);
            borderightGrueso.BorderRight = CellBorderType.THICK;
            borderightGrueso.RightBorderColor = HSSFColor.BLACK.index;
    


        //titulos
           sheet.CreateRow(2).CreateCell(0).SetCellValue("MINISTERIO DE SALUD PUBLICA - RESUMEN DIARIO MENSUAL DE CONSULTAS MEDICAS");
            sheet.CreateRow(3).CreateCell(0).SetCellValue("DEPARTAMENTO PLANIFICACION - DIVISION BIOESTADISTICA");
            sheet.CreateRow(4).CreateCell(0).SetCellValue( "1- IDENTIFICACION DEL ESTABLECIMIENTO: " + _item.centroSaludTexto);
            sheet.CreateRow(6).CreateCell(0).SetCellValue( "DEPARTAMENTO DE LOCALIZACION: " + _item.departamentoTexto + "            2- FECHA: " +
                                      getMes((short)_item.plaMes) + " / " + _item.plaAnio);
            if (_especialidad != null)
            sheet.CreateRow(7).CreateCell(0).SetCellValue("3- UNIDAD OPERATIVA: "+_especialidad.espNombrePadre);
            sheet.CreateRow(9).CreateCell(3).SetCellValue("Cons.");
            sheet.GetRow(9).CreateCell(5).SetCellValue("Menos de 1 año");
            sheet.GetRow(9).CreateCell(9).SetCellValue("1 a 4");
            sheet.GetRow(9).CreateCell(11).SetCellValue("5 a 14");
            sheet.GetRow(9).CreateCell(13).SetCellValue("15 a 19");
            sheet.GetRow(9).CreateCell(15).SetCellValue("20 a 39");
            sheet.GetRow(9).CreateCell(17).SetCellValue("40 a 69");
            sheet.GetRow(9).CreateCell(19).SetCellValue("70 y +");
            sheet.GetRow(9).CreateCell(21).SetCellValue("Tot. Edades");
            sheet.GetRow(9).CreateCell(23).SetCellValue("Total");
            sheet.GetRow(9).CreateCell(24).SetCellValue("Cont. Emb");
            sheet.GetRow(9).CreateCell(25).SetCellValue("Prof.");
            sheet.GetRow(9).CreateCell(26).SetCellValue("P.C.E.");
            
            
            sheet.CreateRow(53).CreateCell(0).SetCellValue("OBSERVACIONES");
            sheet.GetRow(53).CreateCell(19).SetCellValue("FIRMA RESPONSABLE");
            sheet.CreateRow(61).CreateCell(19).SetCellValue("ACLARACION");
   

            //Create a header row
            var headerRow = sheet.CreateRow(10);
            ////Set the column names in the header row
            headerRow.CreateCell(0).SetCellValue("Dia");
             headerRow.CreateCell(1).SetCellValue("Hojas");
             headerRow.CreateCell(2).SetCellValue("Horas");
             headerRow.CreateCell(3).SetCellValue("Medicas");
            //headerRow.CreateCell(4).SetCellValue(" ");
             headerRow.CreateCell(5).SetCellValue("Dia");
           //  headerRow.CreateCell(6).SetCellValue(" ");
             headerRow.CreateCell(7).SetCellValue("Mes");
          

             var headerRow11 = sheet.GetRow(11);
       
             headerRow11.GetCell(3).SetCellValue("N");

             headerRow11.GetCell(4).SetCellValue("R");
             headerRow11.GetCell(5).SetCellValue("M");
             headerRow11.GetCell(6).SetCellValue("F");
             headerRow11.GetCell(7).SetCellValue("M");
             headerRow11.GetCell(8).SetCellValue("F");
             headerRow11.GetCell(9).SetCellValue("M");
             headerRow11.GetCell(10).SetCellValue("F");
             headerRow11.GetCell(11).SetCellValue("M");
             headerRow11.GetCell(12).SetCellValue("F");
             headerRow11.GetCell(13).SetCellValue("M");
             headerRow11.GetCell(14).SetCellValue("F");
             headerRow11.GetCell(15).SetCellValue("M");
             headerRow11.GetCell(16).SetCellValue("F");
             headerRow11.GetCell(17).SetCellValue("M");
             headerRow11.GetCell(18).SetCellValue("F");
             headerRow11.GetCell(19).SetCellValue("M");
             headerRow11.GetCell(20).SetCellValue("F");
             headerRow11.GetCell(21).SetCellValue("M");
             headerRow11.GetCell(22).SetCellValue("F");
          //  sheet.CreateFreezePane(0, 3, 0, 3);

            sheet.GetRow(2).GetCell(0).CellStyle = style1;
            sheet.GetRow(3).GetCell(0).CellStyle = style1;
            sheet.GetRow(4).GetCell(0).CellStyle = style1;
            sheet.GetRow(6).GetCell(0).CellStyle = style1;
            sheet.GetRow(7).GetCell(0).CellStyle = style1;
            sheet.GetRow(9).GetCell(3).CellStyle = style2;
            sheet.GetRow(10).GetCell(0).CellStyle = style2;
            sheet.GetRow(11).GetCell(0).CellStyle = style1;


            int rowNumber = 12;

           
            foreach (proEstadisticaResultadosRHS _Info in _Datos)
            {
                //     //Create a new row
                //     var row = sheet.CreateRow(rowNumber);
              

                var row = sheet.GetRow(rowNumber);
                //ICell cell = row.CreateCell(0);
                //cell.CellStyle = style;
               
                if (_Info.resDia != null)
                {
                    row.GetCell(0).SetCellValue((int) _Info.resDia);
                    if (_Info.resNroHojas != null) row.GetCell(1).SetCellValue((int)_Info.resNroHojas);

                    if (_Info.resHorasAtencion != null) row.GetCell(2).SetCellValue((int)_Info.resHorasAtencion);
                    if (_Info.resCMNuevas != null) row.GetCell(3).SetCellValue((int)_Info.resCMNuevas);
                    if (_Info.resCMRepetidas != null) row.GetCell(4).SetCellValue((int)_Info.resCMRepetidas);
                    if (_Info.resMenos1DM != null) row.GetCell(5).SetCellValue((int)_Info.resMenos1DM);
                    if (_Info.resMenos1DF != null) row.GetCell(6).SetCellValue((int)_Info.resMenos1DF);
                    if (_Info.resMenos1MM != null) row.GetCell(7).SetCellValue((int)_Info.resMenos1MM);
                    if (_Info.resMenos1MM != null) row.GetCell(8).SetCellValue((int)_Info.resMenos1MF);
                    if (_Info.resde1a4M != null) row.GetCell(9).SetCellValue((int)_Info.resde1a4M);
                    if (_Info.resde1a4F != null) row.GetCell(10).SetCellValue((int)_Info.resde1a4F);
                    if (_Info.resde5a14M != null) row.GetCell(11).SetCellValue((int)_Info.resde5a14M);
                    if (_Info.resde5a14F != null) row.GetCell(12).SetCellValue((int)_Info.resde5a14F);
                    if (_Info.resde15a19M != null) row.GetCell(13).SetCellValue((int)_Info.resde15a19M);
                    if (_Info.resde15a19F != null) row.GetCell(14).SetCellValue((int)_Info.resde15a19F);
                    if (_Info.resde20a39M != null) row.GetCell(15).SetCellValue((int)_Info.resde20a39M);
                    if (_Info.resde20a39F != null) row.GetCell(16).SetCellValue((int)_Info.resde20a39F);
                    if (_Info.resde40a69M != null) row.GetCell(17).SetCellValue((int)_Info.resde40a69M);
                    if (_Info.resde40a69F != null) row.GetCell(18).SetCellValue((int)_Info.resde40a69F);
                    if (_Info.resde70ymasM != null) row.GetCell(19).SetCellValue((int)_Info.resde70ymasM);
                    if (_Info.resde70ymasF != null) row.GetCell(20).SetCellValue((int)_Info.resde70ymasF);
                    if (_Info.resTotEdadesM != null) row.GetCell(21).SetCellValue((int)_Info.resTotEdadesM);
                    if (_Info.resTotEdadesF != null) row.GetCell(22).SetCellValue((int)_Info.resTotEdadesF);
                    if (_Info.resTotTotal != null) row.GetCell(23).SetCellValue((int)_Info.resTotTotal);
                    if (_Info.resTotControlEmbarazo != null) row.GetCell(24).SetCellValue((int)_Info.resTotControlEmbarazo);
                    if (_Info.resCantProf != null) row.GetCell(25).SetCellValue((int)_Info.resCantProf);
                    if (_Info.resCantProfCE != null) row.GetCell(26).SetCellValue((int)_Info.resCantProfCE);
                   // row.Cells. = style;

                }
             
                rowNumber++;
            }


            for (int iRow = 43; iRow <= 43; iRow++)
            {
             //   sheet.CreateRow(iRow);
                for (int iCol = 1; iCol <= 25; iCol++)
                {
                    string formula = "SUM("+GetCellPosition(12, iCol)+ ":"+ GetCellPosition(42, iCol)+")";
                    var cell = sheet.GetRow(iRow).GetCell(iCol);
                    cell.CellFormula = formula;
                    sheet.GetRow(iRow).GetCell(iCol).CellStyle = style1;
                  
                }
            }

            var formula_1 = "SUM(" + GetCellPosition(12,25 ) + ":" + GetCellPosition(42, 25) + ")";
            var cell_1 = sheet.GetRow(43).GetCell(25);
            cell_1.CellFormula = formula_1;
        
            var formula_2 = "= PROMEDIO(" + GetCellPosition(12, 25) + ":" + GetCellPosition(42, 25)+")";
            var cell_2 = sheet.GetRow(44).GetCell(25);
            cell_2.SetCellValue(formula_2);


             formula_2 = "= MEDIANA(" + GetCellPosition(12, 25) + ":" + GetCellPosition(42, 25)+")";
             cell_2 = sheet.GetRow(45).GetCell(25);
            cell_2.SetCellValue(formula_2);

            formula_2 = "= PROMEDIO(AA13:AA43)";
             cell_2 = sheet.GetRow(44).GetCell(26);
            cell_2.SetCellValue(formula_2);

            formula_2 = "= MEDIANA(AA13:AA43)";
            cell_2 = sheet.GetRow(45).GetCell(26);
            cell_2.SetCellValue(formula_2);



            var formula1 = "SUM(" + GetCellPosition(43, 3) + ":" + GetCellPosition(43, 4) + ")";
            var cell1 = sheet.GetRow(44).GetCell(4);
            cell1.CellFormula = formula1;
            sheet.GetRow(44).GetCell(4).CellStyle = style1;

            var formula2 = "SUM(" + GetCellPosition(43, 5) + ":" + GetCellPosition(43, 6) + ")";
            var cell2 = sheet.GetRow(44).GetCell(6);
            cell2.CellFormula = formula2;
            sheet.GetRow(44).GetCell(6).CellStyle = style1;

            var formula3 = "SUM(" + GetCellPosition(43, 7) + ":" + GetCellPosition(43, 8) + ")";
            var cell3 = sheet.GetRow(44).GetCell(8);
            cell3.CellFormula = formula3;
            sheet.GetRow(44).GetCell(8).CellStyle = style1;

            var formula4 = "SUM(" + GetCellPosition(43, 9) + ":" + GetCellPosition(43, 10) + ")";
            var cell4 = sheet.GetRow(44).GetCell(10);
            cell4.CellFormula = formula4;
            sheet.GetRow(44).GetCell(10).CellStyle = style1;

            var formula5 = "SUM(" + GetCellPosition(43, 11) + ":" + GetCellPosition(43, 12) + ")";
            var cell5 = sheet.GetRow(44).GetCell(12);
            cell5.CellFormula = formula5;
            sheet.GetRow(44).GetCell(12).CellStyle = style1;

            var formula6 = "SUM(" + GetCellPosition(43, 13) + ":" + GetCellPosition(43, 14) + ")";
            var cell6 = sheet.GetRow(44).GetCell(14);
            cell6.CellFormula = formula6;
            sheet.GetRow(44).GetCell(14).CellStyle = style1;


            var formula7 = "SUM(" + GetCellPosition(43, 15) + ":" + GetCellPosition(43, 16) + ")";
            var cell7 = sheet.GetRow(44).GetCell(16);
            cell7.CellFormula = formula7;
            sheet.GetRow(44).GetCell(16).CellStyle = style1;

            var formula8 = "SUM(" + GetCellPosition(43, 17) + ":" + GetCellPosition(43, 18) + ")";
            var cell8 = sheet.GetRow(44).GetCell(18);
            cell8.CellFormula = formula8;
            sheet.GetRow(44).GetCell(18).CellStyle = style1;

            var formula9 = "SUM(" + GetCellPosition(43, 19) + ":" + GetCellPosition(43, 20) + ")";
            var cell9 = sheet.GetRow(44).GetCell(20);
            cell9.CellFormula = formula9;
            sheet.GetRow(44).GetCell(20).CellStyle = style1;


            var formula10 = "SUM(" + GetCellPosition(43, 21) + ":" + GetCellPosition(43, 22) + ")";
            var cell10 = sheet.GetRow(44).GetCell(22);
            cell10.CellFormula = formula10;
            sheet.GetRow(44).GetCell(22).CellStyle = style1;





            for (int iRow = 11; iRow <= 11; iRow++)
            {
                for (int iCol = 0; iCol <= 26; iCol++)
                {

                    if (iRow == 11)
                        sheet.GetRow(iRow).GetCell(iCol).CellStyle = bordeBottomGrueso;
                    else
                    {
                        if (iCol >= 0 & iCol <= 8)
                        {
                            sheet.GetRow(iRow).GetCell(iCol).CellStyle = bordeTopGrueso;
                        }
                        else
                        {
                            sheet.GetRow(iRow).CreateCell(iCol).CellStyle = bordeTopGrueso;
                        }

                    }
                }
            }

            for (int iRow = 5; iRow <= 5; iRow++)
            {
                var row = sheet.CreateRow(iRow);
                for (int iCol = 0; iCol <= 26; iCol++)
                {

                    if (iRow == 5)
                    {

                        var cell = row.CreateCell(iCol);

                        cell.CellStyle = bordeBottomyTopGrueso;
                    }

                }
            }
            for (int iRow = 8; iRow <= 8; iRow++)
            {
                var row = sheet.CreateRow(iRow);
                for (int iCol = 0; iCol <= 26; iCol++)
                {
                    var cell = row.CreateCell(iCol);
                    cell.CellStyle = bordeBottomyTopGrueso;
                }
            }
            for (int iRow = 1; iRow <= 1; iRow++)
            {
                var row = sheet.CreateRow(iRow);
                for (int iCol = 0; iCol <= 26; iCol++)
                {
                    var cell = row.CreateCell(iCol);
                    cell.CellStyle = bordeBottomGrueso;
                }
            }

          //bordes derechos
            for (int iRow = 2; iRow <= 4; iRow++)
            {
                var row = sheet.GetRow(iRow);
                var cell = row.CreateCell(26);
                cell.CellStyle = borderightGrueso;

            }

            for (int iRow = 6; iRow <= 7; iRow++)
            {
                var row = sheet.GetRow(iRow);

                var cell = row.CreateCell(26);
                cell.CellStyle = borderightGrueso;

            }
            

            
            for (int iRow = 10; iRow <= 11; iRow++)
            {
                var row = sheet.GetRow(iRow);

                var cell = row.GetCell(26);
              //  cell.CellStyle = borderightGrueso;

            }
           

            //Write the workbook to a memory stream
            MemoryStream output = new MemoryStream();
            workbook.Write(output);
          
            string _NombreArchivo = _item.centroSaludTexto+'_'+ _especialidad.espNombre +"_"+ _item.plaAnio +"_"+_item.plaMes + ".xls";
            _NombreArchivo = _NombreArchivo.Replace("/", "_");

            return File(output.ToArray(),   //The binary data of the XLS file
                "application/vnd.ms-excel", //MIME type of Excel files
                _NombreArchivo);     //Suggested file name in the "Save as" dialog which will be displayed to the end user
        }
        static string GetCellPosition(int row, int col)
        {
            col = Convert.ToInt32('A') + col;
            row = row + 1;
            return ((char)col) + row.ToString();
        }
      
      
        public string Especialidades_Hijas(int plaId)
        {
             var _csId = (Session["UsuarioCentroDeSalud"] as GeDoc.sisUsuariosCentroDeSalud).csId;
            var _Pla = (from d in context.enlEstadisticaPlanillaEspecialidad
                where d.plaId == plaId
                select new enlEstadisticaPlanillaEspecialidades()
                {
                 espId = d.espId
                }).FirstOrDefault();

            var _Esp = (from d in context.catEspecialidad
                where d.espIdPadre == _Pla.espId
                select new catEspecialidades()
                {
                    espNombre = d.espNombre
                });
                    
                            
            return _Esp.First().espNombre;
        }

        public string Profesionales_detalle(int plaId,int plaDia)
        {
            var _csId = (Session["UsuarioCentroDeSalud"] as GeDoc.sisUsuariosCentroDeSalud).csId;
            var _Pla = (from d in context.enlEstadisticaPlanillaEspecialidad
                        where d.plaId == plaId
                        select new enlEstadisticaPlanillaEspecialidades()
                        {
                            espId = d.espId
                        }).FirstOrDefault();

            var _Esp = (from d in context.catEspecialidad
                        where d.espIdPadre == _Pla.espId
                        select new catEspecialidades()
                        {
                            espNombre = d.espNombre
                        });


            return _Esp.First().espNombre;
        }
        private void Create(proEstadisticaPlanilla pItem)
        {
            if (ModelState.IsValid)
            {
                pItem.usrId = (Session["Usuario"] as sisUsuario).usrId;
                context.proEstadisticaPlanilla.AddObject(pItem);
                context.SaveChanges();
            }

            return;
        }

        private void Edit(proEstadisticaPlanilla pItem)
        {
            if (ModelState.IsValid)
            {
                pItem.usrId = pItem.usrId = (Session["Usuario"] as sisUsuario).usrId;
                context.SaveChanges();
            }
            return;
        }

        private void DeleteConfirmed(int id)
        {
            proEstadisticaPlanilla _Item = context.proEstadisticaPlanilla.Single(x => x.plaId == id);
            context.proEstadisticaPlanilla.DeleteObject(_Item);
            context.SaveChanges();
        }

        //Datos para el dropdown
        protected void PopulateDropDownList()
        {
            dcAccesoCompadDataContext _Centros = new dcAccesoCompadDataContext();
            var _csId = (Session["UsuarioCentroDeSalud"] as GeDoc.sisUsuariosCentroDeSalud).csId;
            var _dpto = (from s in context.catCentroDeSalud.ToList()
                where s.csId == _csId
                select new catCentroDeSalud()
                {
                    depId = s.depId,

                }).First();
            var _CS = (from s in context.catCentroDeSalud.ToList()
                where s.csId == _csId
                select new catCentroDeSalud()
                {
                    csId = s.csId,
                    csNombre = s.csNombre
                }
                ).ToList();
            var _DN = (from s in context.catDepartamento.ToList()
                where s.provId == 17 && s.depId == _dpto.depId
                orderby s.depNombre ascending
                select new catDepartamento()
                {
                    depId = s.depId,
                    depNombre = s.depNombre
                }).ToList();

            var _ES = (from s in context.catEspecialidad.ToList()
                       where s.espIdPadre == null
                select new catEspecialidades
                {
                    espId = s.espId,
                    espNombre = "(" + s.espCodigo + ") " + s.espNombre
                }).ToList().OrderBy(o => o.espNombre);

            ViewData["csId_Data"] = new SelectList(_CS, "csId", "csNombre");
            ViewData["depId_Data"] = new SelectList(_DN, "depId", "depNombre");
            ViewData["espId_Data"] = new SelectList(_ES, "espId", "espNombre");
            ViewData["plaMes_Data"] = new SelectList(new catMeses().Mes, "mesId", "mesNombre");
        }

    }
}