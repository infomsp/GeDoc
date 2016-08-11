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

    public partial class proDecretoController : Controller
    {
        private efAccesoADatosEntities context = new efAccesoADatosEntities();

        [GridAction]
        public ActionResult _BindingDecretos(string lstFiltros)
        {
            return PartialView(new GridModel(All(lstFiltros)));
        }


        public IList<proDecretos> All(string lstFiltros)
        {
                return getDatos(lstFiltros).ToList();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _SaveEditing(int id)
        {
            proDecreto _Item = context.proDecreto.Where(p => p.decId == id).FirstOrDefault();

            TryUpdateModel(_Item);

            if (context.proDecreto.Where(d => d.decNumero == _Item.decNumero && d.decFecha.Value.Year == _Item.decFecha.Value.Year && d.decEsAcuerdo == _Item.decEsAcuerdo).Count() > 1)
            {
                ModelState.AddModelError("decNumero", "N° Repetido.");
            }
            else
            {
                //_Item.decConsiderando = HttpUtility.HtmlDecode(_Item.decConsiderando);
                _Item.decResuelve = HttpUtility.HtmlDecode(_Item.decResuelve);
                if (Session["upArchivo"] != null)
                {
                    PDFParser _PDF = new PDFParser();
                    string _Texto = _PDF.ExtractText(Path.Combine(Session["PathArchivos"].ToString(), Session["upArchivo"].ToString()));
                    _Item.decConsiderando = _Texto;
                    _Item.decLinkArchivo = Session["upArchivo"].ToString();
                }

                Edit(_Item);
            }

            return View(new GridModel(All("")));
        }

        public ActionResult _EditarRegistro(int id)
        {
            proDecreto _Item = context.proDecreto.Where(p => p.decId == id).FirstOrDefault();

            return View(_Item);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _InsertEditing()
        {
            proDecreto _Item = new proDecreto();

            if (TryUpdateModel(_Item))
            {
                if (context.proDecreto.Where(d => d.decNumero == _Item.decNumero && d.decFecha.Value.Year == _Item.decFecha.Value.Year && d.decEsAcuerdo == _Item.decEsAcuerdo).Count() > 0)
                {
                    ModelState.AddModelError("decNumero", "N° Repetido.");
                }
                else
                {
                    //_Item.decConsiderando = HttpUtility.HtmlDecode(_Item.decConsiderando);
                    _Item.decResuelve = HttpUtility.HtmlDecode(_Item.decResuelve);
                    PDFParser _PDF = new PDFParser();
                    string _Texto = _PDF.ExtractText(Path.Combine(Session["PathArchivos"].ToString(), Session["upArchivo"].ToString()));
                    _Item.decConsiderando = _Texto;
                    _Item.decLinkArchivo = Session["upArchivo"].ToString();

                    Create(_Item);
                }
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

        private IEnumerable<proDecretos> getDatos(string pFiltro)
        {
            var _Datos = (from d in context.getDatosDecretos(pFiltro).ToList()
                            select new proDecretos()
                                    {
                                        decId = d.decId,
                                        decNumero = d.decNumero,
                                        decEsAcuerdo = d.decEsAcuerdo,
                                        decConsiderando = pFiltro,//d.decConsiderando,
                                        decFecha = (DateTime)d.decFecha,
                                        decLinkArchivo = d.decLinkArchivo,
                                        decResuelve = d.decResuelve,
                                        Resoluciones = d.Resoluciones//getResoluciones(d.proDecretoResoluciones.ToList())
                                    }).ToList();

            return _Datos.AsEnumerable();
        }

        public ActionResult Index()
        {
            var _Datos = context.proDecreto.FirstOrDefault();
            Int64 _decId = _Datos == null ? 1 : _Datos.decId;
            ViewData["Expedientes"] = AllExpedientes().Where(exp => exp.decId == _decId);
            ViewData["Resoluciones"] = AllResoluciones().Where(exp => exp.decId == _decId);
            ViewData["idDecreto"] = _decId;
            ViewBag.Catalogo = "Decretos";
            Session["PathArchivos"] = Server.MapPath("~/Content/Archivos/Decretos");

            PopulateDropDownList();

            return PartialView();   
        }

        private void Create(proDecreto pItem)
        {
            if (ModelState.IsValid)
            {
                context.proDecreto.AddObject(pItem);
                
                //Registra log de usuario
                new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "proDecreto", "Agregar", "");

                context.SaveChanges();
            }

            return;
        }

        private void Edit(proDecreto pItem)
        {
            if (ModelState.IsValid)
            {
                //Registra log de usuario
                new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "proDecreto", "Modificar", "");

                context.SaveChanges();
            }
            return;
        }

        private void DeleteConfirmed(int id)
        {
            proDecreto _Item = context.proDecreto.Single(x => x.decId == id);
            context.proDecreto.DeleteObject(_Item);
            
            //Registra log de usuario
            new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "proDecreto", "Eliminar", "");

            context.SaveChanges();
        }

        //Datos para el dropdown
        protected void PopulateDropDownList()
        {
            var _Resoluciones = (from per in context.getDatosResoluciones("").AsEnumerable()
                                 select new
                                     {
                                         per.resId,
                                         resNumero = 
                                                (per.resNumero != null ? 
                                                per.resNumero.Value.ToString("0000") :
                                                "0000") + "-" + per.resFecha.Value.Year.ToString()
                                     }).ToList();

            ViewData["zonId_Data"] = new SelectList(context.catZona, "zonId", "zonNombre");
            ViewData["resId_Data"] = new SelectList(_Resoluciones, "resId", "resNumero");

            return;
        }

        public ActionResult RegistrarLog(string pAccion)
        {
            //Registra log de usuario
            new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "proDecreto", pAccion, "");

            return Json(true);
        }

    }
}