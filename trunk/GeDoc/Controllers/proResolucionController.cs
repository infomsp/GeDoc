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
    using System.IO;

    public partial class proResolucionController : Controller
    {
        private efAccesoADatosEntities context = new efAccesoADatosEntities();

        //Edición de datos

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _BindingResoluciones(string lstFiltros)
        {
            bool _Filtrar = false;
            if (Session["EsAlerta"] != null)
            {
                _Filtrar = (bool)Session["EsAlerta"];
            }

            if (_Filtrar)
            {
                return PartialView(new GridModel(All(lstFiltros).Where(e => e.resEsImportante && e.resNotificacionVencimiento.Value.Date >= DateTime.Now.Date && e.resNotificacionVencimiento.Value.AddDays((double)(e.resNotificacionDiaInicio * -1)).Date <= DateTime.Now.Date)));
            }
            else
            {
                return PartialView(new GridModel(All(lstFiltros)));
            }
        }

        public bool HayNormasLegalesAVencer()
        {
            var _Datos = (from d in context.vwNormasLegalesAVencer.ToList()
                          select new proResoluciones()
                          {
                              resId = d.resId,
                              resNumero = d.resNumero,
                              resFecha = d.resFecha == null ? DateTime.Now : (DateTime)d.resFecha
                          }).ToList();

            return _Datos.Count() > 0;
        }

        public IList<proResoluciones> All(string pFiltro)
        {
            return getDatos(pFiltro).ToList();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _SaveEditing(int id)
        {
            proResolucion _Item = context.proResolucion.Where(p => p.resId == id).FirstOrDefault();
            proResoluciones _Item2 = new proResoluciones();

            TryUpdateModel(_Item2);
            TryUpdateModel(_Item);

            _Item.resConsiderando = HttpUtility.HtmlDecode(_Item.resConsiderando);
            //_Item.resResuelve = HttpUtility.HtmlDecode(_Item.resResuelve);
            if (Session["upArchivo"] != null)
            {
                PDFParser _PDF = new PDFParser();
                string _Texto = _PDF.ExtractText(Path.Combine(Session["PathArchivos"].ToString(), Session["upArchivo"].ToString()));
                // Modificado para remover los caracteres "basura" que trae el nuevo programa de OCR.
                _Item.resConsiderando = _Texto.Replace("\r","").Replace("\n","");
                _Item.resLinkArchivo = Session["upArchivo"].ToString();
            }

            Edit(_Item);

            return View(new GridModel(All("")));
        }

        public ActionResult _EditarRegistro(int id)
        {
            proResolucion _Item = context.proResolucion.Where(p => p.resId == id).FirstOrDefault();

            return View(_Item);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _InsertEditing()
        {
            proResolucion _Item = new proResolucion();
            proResoluciones _Item2 = new proResoluciones();

            TryUpdateModel(_Item2);

            if (TryUpdateModel(_Item))
            {
                //_Item.resConsiderando = HttpUtility.HtmlDecode(_Item.resConsiderando);
                //_Item.resResuelve = HttpUtility.HtmlDecode(_Item.resResuelve);
                if (Session["upArchivo"] != null)
                {
                    PDFParser _PDF = new PDFParser();
                    string _Texto = _PDF.ExtractText(Path.Combine(Session["PathArchivos"].ToString(), Session["upArchivo"].ToString()));
                    _Item.resConsiderando = _Texto;
                    _Item.resLinkArchivo = Session["upArchivo"].ToString();
                }

                Create(_Item);
            }

            return View(new GridModel(All("")));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _DeleteEditing(int id)
        {
            DeleteConfirmed(id);

            return View(new GridModel(All("")));
        }

        private IEnumerable<proResoluciones> getDatos(string pFiltro)
        {
            var _Datos = (from d in context.getDatosResoluciones(pFiltro)
                            select new proResoluciones()
                                    {
                                        resId = d.resId,
                                        resNumero = d.resNumero,
                                        resConsiderando = pFiltro,//d.resConsiderando,
                                        resEsImportante = d.resEsImportante,
                                        resFecha = d.resFecha == null ? DateTime.Now : (DateTime)d.resFecha,
                                        decNumero = d.decNumero,//d.proDecretoResoluciones != null ? (d.proDecretoResoluciones.Count > 0 ? d.proDecretoResoluciones.FirstOrDefault().proDecreto.decNumero : null) : null,
                                        decLinkArchivo = d.decLinkArchivo,//d.proDecretoResoluciones != null ? (d.proDecretoResoluciones.Count > 0 ? d.proDecretoResoluciones.FirstOrDefault().proDecreto.decLinkArchivo : "") : "",
                                        resLinkArchivo = d.resLinkArchivo,
                                        resNotificacionDiaInicio = d.resNotificacionDiaInicio,
                                        resNotificacionVencimiento = d.resNotificacionVencimiento,
                                        resResuelve = "",//d.resResuelve,
                                        tnlId = d.tnlId,
                                        tnlNombre = d.tnlNombre//d.tnlId == null ? "" : d.catTipoNormaLegal.tnlNombre
                                        //Expedientes = getExpedientes(d.proResolucionExpedientes.ToList())
                                    }).ToList();

            return _Datos.AsEnumerable();
        }

        private string getExpedientes(List<proResolucionExpedientes> pExpedientes)
        {
            string _Resultado = "";
            foreach (var _exp in pExpedientes)
            {
                _Resultado += _exp.catZona.zonCodigo.ToString() + "-" + _exp.reseExpedienteNumero.ToString("0000") + "-" + _exp.reseExpedienteAnio.ToString() + ", ";
            }

            if (_Resultado != "")
            {
                _Resultado = _Resultado.Substring(0, _Resultado.Length - 2);
            }
            return _Resultado;
        }

        public ActionResult Index()
        {
            Int64 _resId = context.proResolucion.First().resId;
            ViewData["Expedientes"] = AllExpedientes().Where(exp => exp.resId == _resId);
            ViewData["Empleados"] = AllEmpleados().Where(exp => exp.resId == _resId);
            ViewData["Estadisticas"] = Estadisticas();
            ViewData["idResolucion"] = _resId;
            ViewBag.Catalogo = "Resoluciones";
            Session["EsAlerta"] = false;
            Session["PathArchivos"] = Server.MapPath("~/Content/Archivos/Resoluciones");

            PopulateDropDownList();

            return PartialView();
        }

        private IEnumerable<GraficoLogUsuario> Estadisticas()
        {
            List<GraficoLogUsuario> _Resultado = new List<GraficoLogUsuario>();

            var _Datos = (from d in context.getEstadisticasResoluciones()
                          select new GraficoLogUsuario()
                          {
                              Usuario = d.tnlNombre,
                              Cantidad = d.Cantidad
                          }).ToList();

            int _Total = _Datos.Sum(s => s.Cantidad);

            var _Mejores30 = _Datos.OrderByDescending(o => o.Cantidad).Take(30).ToList();

            int _Resto = _Total - _Mejores30.Sum(s => s.Cantidad);

            _Mejores30.Add(new GraficoLogUsuario() { Usuario = "OTROS TIPOS DE RESOLUCIONES", Cantidad = _Resto });

            _Resultado.Add(new GraficoLogUsuario()
            {
                Cantidad = 0,
                Fecha = DateTime.Now,
                Datos = _Mejores30.OrderBy(o => o.Usuario).ToList(),
                Usuario = ""
            });

            return _Resultado.AsEnumerable();
        }

        public ActionResult Alertas()
        {
            Int64 _resId = All("").First().resId;
            ViewData["Expedientes"] = AllExpedientes().Where(exp => exp.resId == _resId);
            ViewData["Empleados"] = AllEmpleados().Where(exp => exp.resId == _resId);
            ViewData["idResolucion"] = _resId;
            ViewBag.Catalogo = "Resoluciones próximas a vencer";
            Session["EsAlerta"] = true;

            PopulateDropDownList();

            return View();
        }

        private void Create(proResolucion pItem)
        {
            if (ModelState.IsValid)
            {
                if (pItem.resEsImportante && (pItem.resNotificacionDiaInicio == null || pItem.resNotificacionDiaInicio <= 4 || pItem.resNotificacionVencimiento == null))
                {
                    pItem.resNotificacionDiaInicio = null;
                    pItem.resNotificacionVencimiento = null;
                    pItem.resEsImportante = false;
                }
                pItem.resConsiderando = pItem.resConsiderando == null ? "" : pItem.resConsiderando;

                context.proResolucion.AddObject(pItem);
                
                //Registra log de usuario
                new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "proResolucion", "Agregar", "");

                context.SaveChanges();
            }

            return;
        }

        private void Edit(proResolucion pItem)
        {
            if (ModelState.IsValid)
            {
                //Registra log de usuario
                new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "proResolucion", "Modificar", "");
                if (pItem.resEsImportante && (pItem.resNotificacionDiaInicio == null || pItem.resNotificacionDiaInicio <= 4 || pItem.resNotificacionVencimiento == null))
                {
                    pItem.resNotificacionDiaInicio = null;
                    pItem.resNotificacionVencimiento = null;
                    pItem.resEsImportante = false;
                }
                pItem.resConsiderando = pItem.resConsiderando == null ? "" : pItem.resConsiderando;

                context.SaveChanges();
            }
            return;
        }

        private void DeleteConfirmed(int id)
        {
            proResolucion _Item = context.proResolucion.Single(x => x.resId == id);
            context.proResolucion.DeleteObject(_Item);
            
            //Registra log de usuario
            new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "proResolucion", "Eliminar", "");

            context.SaveChanges();
        }

        //Datos para el dropdown
        protected void PopulateDropDownList()
        {
            var _Empleados = (from per in context.getListaDesplegablePersonas()
                              select new
                                 {
                                     perId = per.perId,
                                     perNombre = per.perApellidoyNombre
                                 }).ToList();

            ViewData["zonId_Data"] = new SelectList(context.catZona, "zonId", "zonNombre");
            ViewData["perId_Data"] = new SelectList(_Empleados, "perId", "perNombre");
            ViewData["tnlId_Data"] = new SelectList(context.catTipoNormaLegal, "tnlId", "tnlNombre");

            return;
        }

        public ActionResult RegistrarLog(string pAccion)
        {
            //Registra log de usuario
            new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "proResolucion", pAccion, "");

            return Json(true);
        }

    }
}