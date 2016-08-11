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

    public partial class catBienesController : Controller
    {
        private efAccesoADatosEntities context = new efAccesoADatosEntities();

        //Edición de datos

        [GridAction]
        public ActionResult _SelectEditing()
        {
            return View(new GridModel(All()));
        }

        private IList<catBien> All()
        {
                return getDatos().ToList();
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
            catBienes _Item = context.catBienes.Where(p => p.biId == id).FirstOrDefault();

            TryUpdateModel(_Item);

            _Item.biObservaciones = _Item.biObservaciones == null ? "" : _Item.biObservaciones;
            if (!_Item.biEsBiometrico)
            {
                _Item.biCodigoBiomedico = "";
                _Item.biMarca = "";
                _Item.biManuales = "";
                _Item.biFabricacion = null;
                _Item.biModelo = "";
                _Item.biNroSerie = "";
            }
            if (ValidarDatosBiomedicos(_Item))
            {
                Edit(_Item);
            }

            return View(new GridModel(All()));
        }

        private bool ValidarDatosBiomedicos(catBienes pItem)
        {
            if (pItem.biEsBiometrico)
            {
                if (pItem.biCodigoBiomedico == "")
                {
                    ModelState.AddModelError("biCodigoBiomedico", "Ingrese el código");
                }
                if (pItem.biMarca == "")
                {
                    ModelState.AddModelError("biMarca", "Ingrese la marca");
                }
                if (pItem.biModelo == "")
                {
                    ModelState.AddModelError("biModelo", "Ingrese el modelo");
                }
                if (pItem.biNroSerie == "")
                {
                    ModelState.AddModelError("biNroSerie", "Ingrese el Nº de serie");
                }
                if (pItem.biFabricacion == null)
                {
                    ModelState.AddModelError("biFabricacion", "Ingrese la fecha de fabricación");
                }
            }

            return true;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _InsertEditing()
        {
            catBienes _Item = new catBienes();

            if (TryUpdateModel(_Item))
            {
                if (Session["UsuarioCentroDeSalud"] == null)
                {
                    RedirectToAction("LogOff", "Account");
                    return null;
                }
                //var _csId = (Session["UsuarioCentroDeSalud"] as GeDoc.sisUsuariosCentroDeSalud).csId;

                //_Item.csId = _csId;
                _Item.biObservaciones = _Item.biObservaciones == null ? "" : _Item.biObservaciones;

                if (ValidarDatosBiomedicos(_Item))
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
            if (Session["UsuarioCentroDeSalud"] == null)
            {
                RedirectToAction("LogOff", "Account");
                return null;
            }
            DeleteConfirmed(id);

            return View(new GridModel(All()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _ActivaRegistro(int id)
        {
            DeleteConfirmed(id);

            return View(new GridModel(All()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult BindingEspecialidades(int perId)
        {
            ViewData["cscId_Data"] = new SelectList(getConsultorios(), "cscId", "cscNombre");

            return Json((SelectList)ViewData["cscId_Data"]);
        }

        private IEnumerable<catBien> getDatos()
        {
            if (Session["UsuarioCentroDeSalud"] == null)
            {
                RedirectToAction("LogOff", "Account");
                return null;
            }
            var _csId = (Session["UsuarioCentroDeSalud"] as GeDoc.sisUsuariosCentroDeSalud).csId;
            var _Datos = (from d in context.catBienes.ToList()
                          join b in context.catCentroDeSaludConsultorio on d.cscId equals b.cscId
                          where (_csId == 0 ? 0 : b.csId) == _csId
                          select new catBien
                                    {
                                        Sector = d.catCentroDeSaludConsultorio.cscNombre,
                                        biCodigo = d.biCodigo,
                                        biDescripcion = d.biDescripcion,
                                        biDetalle1 = d.biDetalle1,
                                        biDetalle2 = d.biDetalle2,
                                        biEsBiometrico = d.biEsBiometrico,
                                        biId = d.biId,
                                        biObservaciones = d.biObservaciones,
                                        cscId = d.cscId,
                                        Movimientos = d.catBienesMovimientos.Count,
                                        biCodigoBiomedico = d.biCodigoBiomedico,
                                        biManuales = d.biManuales,
                                        biMarca = d.biMarca,
                                        biFabricacion = d.biFabricacion,
                                        biModelo = d.biModelo,
                                        biNroSerie = d.biNroSerie
                                    }).ToList();

            return _Datos.AsEnumerable();
        }

        public ActionResult Index()
        {
            ViewBag.Catalogo = "Bienes";
            PopulateDropDownList();
            ViewData["BienesMovimientos"] = new List<catBienMovimientos>();

            return PartialView();
        }

        private void Create(catBienes pItem)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var _usrId = (Session["Usuario"] as sisUsuario).usrId;
                    var _tipoId = context.sisTipoEntidad.Where(w => w.tipoeCodigo == "EB").First().sisTipo.Where(w => w.tipoCodigo == "AL").First().tipoId;

                    pItem.catBienesMovimientos.Add(new catBienesMovimientos() { bimfecha = DateTime.Now, bimObservaciones = "Alta inicial", tipoId = _tipoId, usrId = _usrId, bimCodigo = pItem.biCodigo, cscIdDestino = pItem.cscId });
                    context.catBienes.AddObject(pItem);
                    new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catBienes", "Agregar", "Agrega un bien");


                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("biCodigo", ex.Message);
                }
            }

            return;
        }

        private void Edit(catBienes pItem)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //Registra log de usuario
                    new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catBienes", "Modificar", "Modifica un bien");

                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("biCodigo", ex.Message);
                }
            }
            return;
        }

        private void DeleteConfirmed(int id)
        {
            try
            {
                catBienes _Item = context.catBienes.Single(x => x.biId == id);
                var _Movimientos = context.catBienesMovimientos.Single(x => x.biId == id);
                //Registra log de usuario
                new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catBienes", "Eliminar", "Elimina el bien Código: " + _Item.biCodigo);

                context.catBienesMovimientos.DeleteObject(_Movimientos);
                context.catBienes.DeleteObject(_Item);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("biCodigo", ex.Message);
            }
        }

        //Datos para el dropdown
        protected void PopulateDropDownList()
        {
            var _Consultorios = getConsultorios();
            ViewData["cscId_Data"] = new SelectList(_Consultorios, "cscId", "cscNombre");
            ViewData["usrId_Data"] = new SelectList(context.sisUsuario, "usrId", "usrApellidoyNombre");
            ViewData["tipoId_Data"] = new SelectList(context.sisTipo.Where(w => w.sisTipoEntidad.tipoeCodigo == "EB" && w.tipoCodigo!= "AL").OrderBy(o => o.tipoDescripcion), "tipoId", "tipoDescripcion");
            ViewData["cscIdOrigen_Data"] = new SelectList(_Consultorios, "cscId", "cscNombre");
            ViewData["cscIdDestino_Data"] = new SelectList(_Consultorios, "cscId", "cscNombre");

            return;
        }

        //Datos para el dropdown
        protected List<catCentroDeSaludConsultorio> getConsultorios()
        {
            var _csId = (Session["UsuarioCentroDeSalud"] as GeDoc.sisUsuariosCentroDeSalud).csId;
            var _Consultorios = (from s in context.catCentroDeSaludConsultorio.ToList()
                                 where s.csId == _csId && s.cscActivo
                                 select new catCentroDeSaludConsultorio
                                 {
                                     cscId = s.cscId,
                                     cscNombre = s.cscNombre
                                 }).ToList().OrderBy(o => o.cscNombre);
            return _Consultorios.ToList();
        }
    }
}