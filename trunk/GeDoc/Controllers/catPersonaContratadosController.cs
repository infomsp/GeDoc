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
    using System.Data.Objects.DataClasses;
    using NPOI.HSSF.UserModel;
    using System.IO;

    public partial class catPersonaContratadosController : Controller
    {
        private efAccesoADatosEntities context = new efAccesoADatosEntities();

        //Edición de datos

        [GridAction]
        public ActionResult _SelectEditing()
        {
            return View(new GridModel(All()));
        }

        private IList<catPersonasContratados> All()
        {
                return getDatos().ToList();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _SaveEditing(int id)
        {
            catPersonaContratados _Item = context.catPersonaContratados.Where(p => p.conId == id).FirstOrDefault();
            catPersonasContratados _Item2 = new catPersonasContratados();

            TryUpdateModel(_Item);
            TryUpdateModel(_Item2);

            _Item.conFechaBaja = _Item2.conFechaBaja;

            var _Persona = context.catPersonaContratados.FirstOrDefault(s => s.conDNI == _Item.conDNI && s.conId != id);
            if (_Persona != null)
            {
                ModelState.AddModelError("conDNI", "El Empleado ya existe en " + _Persona.catReparticion.repNombre + ". Solicite actualización a nivel central");
            }
            else
            {
                Edit(_Item);
            }

            return View(new GridModel(All()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _InsertEditing()
        {
            catPersonaContratados _Item = new catPersonaContratados();

            if (TryUpdateModel(_Item))
            {
                var _Persona = context.catPersonaContratados.FirstOrDefault(s => s.conDNI == _Item.conDNI);
                if (_Persona != null)
                {
                    ModelState.AddModelError("conDNI", "El Empleado ya existe en " + _Persona.catReparticion.repNombre + ". Solicite actualización a nivel central");
                }
                else
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

        private IEnumerable<catPersonasContratados> getDatos()
        {
            try
            {
                dbcDatosDataContext _Metodos = new dbcDatosDataContext();
                var ZonaSanitaria = (Session["Usuario"] as sisUsuario).repId;

                var _Datos = (from d in context.catPersonaContratados.ToList() //.Where(w => (ZonaSanitaria == null ? true : w.repId == ZonaSanitaria))
                              where ZonaSanitaria == null ? true : d.repId == ZonaSanitaria
                              select new catPersonasContratados()
                                        {
                                            repId = d.repId,
                                            conCUIL = d.conCUIL,
                                            conDNI = d.conDNI,
                                            conFecha = d.conFecha,
                                            conApellidoyNombre = d.conApellidoyNombre,
                                            conObservaciones = d.conObservaciones,
                                            conTelefono = d.conTelefono,
                                            tipoId = d.tipoId,
                                            conEmail = d.conEmail,
                                            conId = d.conId,
                                            repNombre = d.catReparticion.repNombre,
                                            conSexo = d.sisTipo.tipoDescripcion,
                                            profId = d.profId,
                                            profNombre = d.catProfesion.profNombre,
                                            conDomicilio = d.conDomicilio,
                                            conCargaHorariaSemanal = d.conCargaHorariaSemanal,
                                            conCelular = d.conCelular,
                                            conCuotas = d.conCuotas,
                                            conFechaBaja = d.conFechaBaja,
                                            conMontoMensual = d.conMontoMensual,
                                            conMotivoBaja = d.conMotivoBaja,
                                            espNombre = getEspecialidades(d.catPersonaContratadosEspecialidad.ToList()),
                                            conMontoAnual = d.conMontoMensual * d.conCuotas,
                                            conDeBaja = d.conFechaBaja != null,
                                            TextoMontoAnual = _Metodos.fnNumerosALetras(d.conMontoMensual * d.conCuotas),
                                            TextoMontoMensual = _Metodos.fnNumerosALetras(d.conMontoMensual)
                                        }).ToList();
                
                return _Datos.AsEnumerable();
            }
            catch (Exception ex)
            { }

            return null;
        }

        private string getEspecialidades(List<catPersonaContratadosEspecialidad> pEsp)
        {
            string _Resultado = "";
            foreach (var _res in pEsp)
            {
                _Resultado += _res.catEspecialidad.espNombre.ToString() + ", ";
            }

            if (_Resultado != "")
            {
                _Resultado = _Resultado.Substring(0, _Resultado.Length - 2);
            }
            return _Resultado;
        }

        public ViewResult Index()
        {
            ViewBag.Catalogo = "Contratados";
            ViewData["FiltroContains"] = true;

            PopulateDropDownList();

            return View();
        }

        private void Create(catPersonaContratados pItem)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    context.catPersonaContratados.AddObject(pItem);

                    //Registra log de usuario
                    new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catPersonaContratados", "Agregar", "");

                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("conApellidoyNombre", ex.Message);
                }
            }

            return;
        }

        private void Edit(catPersonaContratados pItem)
        {
            if (ModelState.IsValid)
            {
                //Registra log de usuario
                new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catPersonaContratados", "Modificar", "");

                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("conApellidoyNombre", ex.Message);
                }
            }
            return;
        }

        private void DeleteConfirmed(int id)
        {
            catPersonaContratados _Item = context.catPersonaContratados.Single(x => x.conId == id);
            context.catPersonaContratados.DeleteObject(_Item);

            //Registra log de usuario
            new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catPersonaContratados", "Eliminar", "");

            context.SaveChanges();
        }

        public ActionResult Exportar(int page, string orderBy, string filter)
        {
            //Get the data representing the current grid state - page, sort and filter
            GridModel model = getDatos().AsQueryable().ToGridModel(page, 9999999, orderBy, string.Empty, filter);
            var _Datos = model.Data.Cast<catPersonasContratados>();

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
            sheet.SetColumnWidth(5, 60 * 256);
            sheet.SetColumnWidth(6, 10 * 256);
            sheet.SetColumnWidth(7, 10 * 256);
            sheet.SetColumnWidth(8, 15 * 256);
            sheet.SetColumnWidth(9, 15 * 256);
            sheet.SetColumnWidth(10, 15 * 256);
            sheet.SetColumnWidth(11, 10 * 256);
            sheet.SetColumnWidth(12, 10 * 256);
            sheet.SetColumnWidth(13, 12 * 256);
            sheet.SetColumnWidth(14, 12 * 256);
            sheet.SetColumnWidth(15, 15 * 256);
            sheet.SetColumnWidth(16, 30 * 256);
            sheet.SetColumnWidth(17, 10 * 256);
            sheet.SetColumnWidth(18, 30 * 256);
            sheet.SetColumnWidth(19, 30 * 256);
            sheet.SetColumnWidth(20, 30 * 256);

            //Create a header row
            var headerRow = sheet.CreateRow(0);
            
            //Set the column names in the header row
            headerRow.CreateCell(0).SetCellValue("Fecha");
            headerRow.CreateCell(1).SetCellValue("Apellido y Nombre");
            headerRow.CreateCell(2).SetCellValue("Sexo");
            headerRow.CreateCell(3).SetCellValue("DNI");
            headerRow.CreateCell(4).SetCellValue("CUIL");
            headerRow.CreateCell(5).SetCellValue("Domicilio");
            headerRow.CreateCell(6).SetCellValue("Teléfono");
            headerRow.CreateCell(7).SetCellValue("Celular");
            headerRow.CreateCell(8).SetCellValue("Correo Electrónico");
            headerRow.CreateCell(9).SetCellValue("Actividad");
            headerRow.CreateCell(10).SetCellValue("Especialidad");
            headerRow.CreateCell(11).SetCellValue("Carga Horaria");
            headerRow.CreateCell(12).SetCellValue("Cuotas");
            headerRow.CreateCell(13).SetCellValue("Monto Mensual");
            headerRow.CreateCell(14).SetCellValue("Monto Anual");
            headerRow.CreateCell(15).SetCellValue("Zona Sanitaria");
            headerRow.CreateCell(16).SetCellValue("Observaciones");
            headerRow.CreateCell(17).SetCellValue("Fecha Baja");
            headerRow.CreateCell(18).SetCellValue("Motivo Baja");
            headerRow.CreateCell(19).SetCellValue("Texto Monto Mensual");
            headerRow.CreateCell(20).SetCellValue("Texto Monto Anual");

            //(Optional) freeze the header row so it is not scrolled
            sheet.CreateFreezePane(0, 1, 0, 1);

            int rowNumber = 1;

            //Populate the sheet with values from the grid data
            foreach (catPersonasContratados _Info in _Datos)
            {
                //Create a new row
                var row = sheet.CreateRow(rowNumber);

                //Set values for the cells
                row.CreateCell(0).SetCellValue(_Info.conFecha.Date);
                row.CreateCell(1).SetCellValue(_Info.conApellidoyNombre);
                row.CreateCell(2).SetCellValue(_Info.conSexo);
                row.CreateCell(3).SetCellValue(_Info.conDNI);
                row.CreateCell(4).SetCellValue(_Info.conCUIL);
                row.CreateCell(5).SetCellValue(_Info.conDomicilio);
                row.CreateCell(6).SetCellValue(_Info.conTelefono);
                row.CreateCell(7).SetCellValue(_Info.conCelular);
                row.CreateCell(8).SetCellValue(_Info.conEmail);
                row.CreateCell(9).SetCellValue(_Info.profNombre);
                row.CreateCell(10).SetCellValue(_Info.espNombre);
                row.CreateCell(11).SetCellValue(_Info.conCargaHorariaSemanal);
                row.CreateCell(12).SetCellValue(_Info.conCuotas);
                row.CreateCell(13).SetCellValue((double)_Info.conMontoMensual);
                row.CreateCell(14).SetCellValue((double)_Info.conMontoAnual);
                row.CreateCell(15).SetCellValue(_Info.repNombre);
                row.CreateCell(16).SetCellValue(_Info.conObservaciones);
                row.CreateCell(17).SetCellValue(_Info.conFecha);
                row.CreateCell(18).SetCellValue(_Info.conMotivoBaja);
                row.CreateCell(19).SetCellValue(_Info.TextoMontoMensual);
                row.CreateCell(20).SetCellValue(_Info.TextoMontoAnual);

                rowNumber++;
            }

            //Write the workbook to a memory stream
            MemoryStream output = new MemoryStream();
            workbook.Write(output);

            string _NombreArchivo = "Contratados-" + DateTime.Now.Date.ToString("yyyyMMddhhmmss") + ".xls";
            //Return the result to the end user

            return File(output.ToArray(),   //The binary data of the XLS file
                "application/vnd.ms-excel", //MIME type of Excel files
                _NombreArchivo);     //Suggested file name in the "Save as" dialog which will be displayed to the end user
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

            var _Esp = (from s in context.catEspecialidad.ToList()
                        where s.espIdPadre == null
                        select new catEspecialidades
                        {
                            espId = s.espId,
                            espNombre = s.espCodigo + " - " + s.espNombre
                        }).ToList();

            ViewData["repId_Data"] = new SelectList(context.catReparticion, "repId", "repNombre");
            ViewData["tipoId_Data"] = new SelectList(_Sexo, "tipoId", "tipoDescripcion");
            ViewData["profId_Data"] = new SelectList(context.catProfesion, "profId", "profNombre");
            ViewData["espId_Data"] = new SelectList(_Esp, "espId", "espNombre");

            return;
        }
    }
}