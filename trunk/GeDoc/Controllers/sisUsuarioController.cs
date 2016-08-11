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
    using System.Transactions;
    using Telerik.Web.Mvc.UI;
    using System.Data.Objects;

    public partial class sisUsuarioController : Controller
    {
        private efAccesoADatosEntities context = new efAccesoADatosEntities();

        //Edición de datos

        [GridAction]
        public ActionResult _SelectEditing()
        {
            return View(new GridModel(All()));
        }

        private IList<sisUsuarios> All()
        {
                return getDatos().ToList();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _SaveEditing(int id)
        {
            sisUsuarios _Result = new sisUsuarios();

            if (TryUpdateModel(_Result))
            {
                _Result.usrId = (short)id;
                Edit(_Result);
            }

            return View(new GridModel(All()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _InsertEditing()
        {
            sisUsuarios _Result = new sisUsuarios();

            if (TryUpdateModel(_Result))
            {
                Create(_Result);
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

        private IEnumerable<sisUsuarios> getDatos()
        {
            var _Datos = (from d in context.sisUsuario
                          select new sisUsuarios()
                                    {
                                        usrId = d.usrId,
                                        usrNombreDeUsuario = d.usrNombreDeUsuario,
                                        usrApellidoyNombre = d.usrApellidoyNombre,
                                        estId = d.estId,
                                        usrPassword = "",
                                        estDescripcion = d.estId == null ? "" : d.sisEstilos.estDescripcion,
                                        perNombre = d.usrApellidoyNombre,
                                        usrEmail = d.usrEmail,
                                        repId = d.repId,
                                        ZonaSanitaria = d.repId == null ? new catReparticiones() { repId = 0, repNombre = "" } : new catReparticiones() { repId = (int)d.repId, repNombre = d.catReparticion.repNombre },
                                        perId = d.perId == null ? (d.conId * -1) : d.perId,
                                        icId = d.icId,
                                        InstitucionContable = d.icId == null ? "" : d.catInstitucionContable.icDescripcion
                                    }).ToList();

            return _Datos.AsEnumerable();
        }

        public ActionResult Index()
        {
            ViewBag.Catalogo = "Usuarios del Sistema";
            ViewData["FiltroContains"] = true;

            PopulateDropDownList();

            return PartialView();   
        }

        private void Create(sisUsuarios pItem)
        {
            if (ModelState.IsValid)
            {
                using (var transaction = new TransactionScope())
                {
                    try
                    {
                        context.ProcActualizaUsuario(null, pItem.usrNombreDeUsuario, pItem.usrApellidoyNombre, pItem.usrPassword, pItem.estId, pItem.usrEmail, pItem.repId, pItem.perId, pItem.icId);
                        context.SaveChanges();
                        transaction.Complete();
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }

            return;
        }

        private void Edit(sisUsuarios pItem)
        {
            if (ModelState.IsValid)
            {

                context.ProcActualizaUsuario(pItem.usrId, pItem.usrNombreDeUsuario, pItem.usrApellidoyNombre, pItem.usrPassword, pItem.estId, pItem.usrEmail, pItem.repId, pItem.perId, pItem.icId);

                context.SaveChanges();
            }
            return;
        }

        private void DeleteConfirmed(int id)
        {
            sisUsuario _Item = context.sisUsuario.Single(x => x.usrId == id);
            context.sisUsuario.DeleteObject(_Item);
            context.SaveChanges();
        }

        //Datos para el dropdown
        protected void PopulateDropDownList()
        {
            var _Profesionales = context.getListaDesplegableMedicos().ToList();

            ViewData["perId_Data"] = new SelectList(_Profesionales, "perId", "perApellidoyNombre");

            ViewData["estId_Data"] = new SelectList(context.sisEstilos, "estId", "estDescripcion");
            ViewData["csId_Data"] = new SelectList(context.catCentroDeSalud.OrderBy(o => o.csNombre), "csId", "csNombre");
            ViewData["ofiId_Data"] = new SelectList(context.catOficina.OrderBy(o => o.ofiNombre), "ofiId", "ofiNombre");
            ViewData["csId_Data"] = new SelectList(context.catCentroDeSalud.OrderBy(o => o.csNombre), "csId", "csNombre");
            ViewData["repId_Data"] = new SelectList(context.catReparticion.OrderBy(o => o.repNombre), "repId", "repNombre");
            ViewData["icId_Data"] = new SelectList(context.catInstitucionContable, "icId", "icDescripcion");

            return;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult setPermisosUsuario(int idUsuario, string Datos)
        {
            List<sisUsuarioPermiso> _Insert = new List<sisUsuarioPermiso>();
            List<sisUsuarioPermiso> _Update = new List<sisUsuarioPermiso>();

            bool _Continuar = true;
            while (_Continuar)
            {
                string _Registro = Datos.Substring(0, Datos.IndexOf("\n"));
                int _mnuId = Convert.ToInt32(_Registro.Substring(_Registro.IndexOf("mnuId:") + 6, _Registro.IndexOf("#", _Registro.IndexOf("mnuId:")) - (_Registro.IndexOf("mnuId:") + 6)));
                int? _acId = Convert.ToInt32(_Registro.Substring(_Registro.IndexOf("acId:") + 5, _Registro.IndexOf("#", _Registro.IndexOf("acId:")) - (_Registro.IndexOf("acId:") + 5)));
                int _upId = Convert.ToInt32(_Registro.Substring(_Registro.IndexOf("upId:") + 5, _Registro.IndexOf("#", _Registro.IndexOf("upId:")) - (_Registro.IndexOf("upId:") + 5)));
                bool _upAutorizado = _Registro.Substring(_Registro.IndexOf("upAutorizado:") + 13) == "1";
                if (_upId == 0)
                {
                    _Insert.Add(new sisUsuarioPermiso() { usrId = (short)idUsuario, mnuId = _mnuId, upAutorizado = _upAutorizado, acId = _acId == 0 ? null : _acId });
                }
                else
                {
                    _Update.Add(new sisUsuarioPermiso() { upId = _upId, usrId = (short)idUsuario, mnuId = _mnuId, upAutorizado = _upAutorizado, acId = _acId == 0 ? null : _acId });
                }

                Datos = Datos.Remove(0, _Registro.Length + 1);
                _Continuar = (Datos != "");
            }

            try
            {
                //Ahora guardamos todos los cambios\\
                foreach (var _Ins in _Insert)
                {
                    context.sisUsuarioPermiso.AddObject(_Ins);
                }
                foreach (var _Upd in _Update)
                {
                    var _Item = context.sisUsuarioPermiso.Where(p => p.upId == _Upd.upId).FirstOrDefault();
                    
                    _Item.upAutorizado = _Upd.upAutorizado;
                }
                context.SaveChanges(SaveOptions.AcceptAllChangesAfterSave);
            }
            catch (Exception ex)
            {
                return Json(false);
            }

            return Json(true);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult getPermisosUsuario(int idUsuario)
        {
            return Json(getPermisos(idUsuario));
        }

        public IEnumerable<Telerik.Web.Mvc.UI.TreeViewItemModel> getPermisos(int idUsuario)
        {
            List<Telerik.Web.Mvc.UI.TreeViewItemModel> _Menu = new List<Telerik.Web.Mvc.UI.TreeViewItemModel>();
            var _Datos = context.sisMenu;

            int _usrId = idUsuario;

            foreach (var mnu in _Datos.Where(m => (m.mnuNombre.Substring(0, 1) == "/" && !m.mnuNombre.Substring(1).Contains("/"))).OrderBy(o => o.mnuOrden).ToList())
            {
                var _Acciones = context.sisMenuAccion.Where(p => p.mnuId == mnu.mnuId);
                if (_Acciones.Count() == 0)
                {
                    var _Checked = context.sisUsuarioPermiso.Where(p => p.usrId == _usrId && p.mnuId == mnu.mnuId && p.acId == null).FirstOrDefault();
                    Telerik.Web.Mvc.UI.TreeViewItemModel _Item = new Telerik.Web.Mvc.UI.TreeViewItemModel()
                    {
                        Checkable = true,
                        Checked = _Checked == null ? false : _Checked.upAutorizado,
                        Value = (_Checked == null ? "upId:0" : "upId:" + _Checked.upId.ToString()) + "#mnuId:" + mnu.mnuId.ToString() + "#acId:0",
                        ImageUrl = mnu.mnuImagen,
                        Text = mnu.mnuTitulo
                    };
                    try
                    {
                        var _SubMenu = _Datos.Where(m => (m.mnuNombre.Contains(mnu.mnuNombre) && m.mnuNombre.Replace(mnu.mnuNombre, "").Substring(0, 1) == "/" && !m.mnuNombre.Replace(mnu.mnuNombre, "").Substring(1).Contains("/"))).OrderBy(o => o.mnuOrden).ToList();
                        if (_SubMenu.Count > 0)
                        {
                            //Método recursivo
                            setSubMenu(mnu.mnuNombre, _SubMenu, _Datos.ToList(), _Item, idUsuario);
                        }
                    }
                    catch { }
                    _Menu.Add(_Item);
                }
                else
                {
                    var _Checked = context.sisUsuarioPermiso.Where(p => p.usrId == _usrId && p.mnuId == mnu.mnuId && p.acId == null).FirstOrDefault();
                    Telerik.Web.Mvc.UI.TreeViewItemModel _Item = new Telerik.Web.Mvc.UI.TreeViewItemModel()
                    {
                        Checkable = true,
                        Checked = _Checked == null ? false : _Checked.upAutorizado,
                        Value = (_Checked == null ? "upId:0" : "upId:" + _Checked.upId.ToString()) + "#mnuId:" + mnu.mnuId.ToString() + "#acId:0",
                        ImageUrl = mnu.mnuImagen,
                        Text = mnu.mnuTitulo
                    };
                    foreach (var _Accion in _Acciones)
                    {
                        _Checked = context.sisUsuarioPermiso.Where(p => p.usrId == _usrId && p.mnuId == mnu.mnuId && p.acId == _Accion.acId).FirstOrDefault();
                        Telerik.Web.Mvc.UI.TreeViewItemModel _ItemAccion = new Telerik.Web.Mvc.UI.TreeViewItemModel()
                        {
                            Checkable = true,
                            Checked = _Checked == null ? false : _Checked.upAutorizado,
                            Value = (_Checked == null ? "upId:0" : "upId:0" + _Checked.upId.ToString()) + "#mnuId:" + mnu.mnuId.ToString() + "#acId:" + _Accion.acId.ToString(),
                            ImageUrl = Url.Content("~/Content") + "/General/Vacio-Transparente.png",
                            Text = _Accion.sisAccion.acDescripcion
                        };
                        _ItemAccion.ImageHtmlAttributes.Add("style", "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat " + _Accion.sisAccion.acImagen + "width: 15px; height: 15px;");

                        _Item.Items.Add(_ItemAccion);
                    }
                    _Menu.Add(_Item);
                }
            }

            return _Menu.AsEnumerable();
        }

        public void setSubMenu(string pMenuNombre, List<sisMenu> _Menu, List<sisMenu> _MenuCompleto, Telerik.Web.Mvc.UI.TreeViewItemModel pMenu, int pusrId)
        {
            int _usrId = pusrId;

            foreach (var mnu in _Menu)
            {
                var _Acciones = context.sisMenuAccion.Where(p => p.mnuId == mnu.mnuId);
                if (_Acciones.Count() == 0)
                {
                    var _Checked = context.sisUsuarioPermiso.Where(p => p.usrId == _usrId && p.mnuId == mnu.mnuId && p.acId == null).FirstOrDefault();
                    Telerik.Web.Mvc.UI.TreeViewItemModel _Item = new Telerik.Web.Mvc.UI.TreeViewItemModel()
                    {
                        Checkable = true,
                        Checked = _Checked == null ? false : _Checked.upAutorizado,
                        Value = (_Checked == null ? "upId:0" : "upId:" + _Checked.upId.ToString()) + "#mnuId:" + mnu.mnuId.ToString() + "#acId:0",
                        ImageUrl = mnu.mnuImagen,
                        Text = mnu.mnuTitulo
                    };
                    try
                    {
                        var _SubMenu = _MenuCompleto.Where(m => (m.mnuNombre.Contains(mnu.mnuNombre) && m.mnuNombre != mnu.mnuNombre)).OrderBy(o => o.mnuOrden).ToList();
                        if (_SubMenu.Count > 0)
                        {
                            setSubMenu(mnu.mnuNombre, _SubMenu, _MenuCompleto, _Item, _usrId);
                        }
                    }
                    catch
                    {
                        var _SubMenu = _MenuCompleto.Where(m => (m.mnuNombre.Contains(mnu.mnuNombre) && m.mnuNombre != mnu.mnuNombre)).OrderBy(o => o.mnuOrden).ToList();
                        if (_SubMenu.Count > 0)
                        {
                            setSubMenu(mnu.mnuNombre, _SubMenu, _MenuCompleto, _Item, _usrId);
                        }
                    }

                    pMenu.Items.Add(_Item);
                }
                else
                {
                    var _Checked = context.sisUsuarioPermiso.Where(p => p.usrId == _usrId && p.mnuId == mnu.mnuId && p.acId == null).FirstOrDefault();
                    Telerik.Web.Mvc.UI.TreeViewItemModel _Item = new Telerik.Web.Mvc.UI.TreeViewItemModel()
                    {
                        Checkable = true,
                        Checked = _Checked == null ? false : _Checked.upAutorizado,
                        Value = (_Checked == null ? "upId:0" : "upId:" + _Checked.upId.ToString()) + "#mnuId:" + mnu.mnuId.ToString() + "#acId:0",
                        ImageUrl = mnu.mnuImagen,
                        Text = mnu.mnuTitulo
                    };
                    foreach (var _Accion in _Acciones)
                    {
                        _Checked = context.sisUsuarioPermiso.Where(p => p.usrId == _usrId && p.mnuId == mnu.mnuId && p.acId == _Accion.acId).FirstOrDefault();
                        Telerik.Web.Mvc.UI.TreeViewItemModel _ItemAccion = new Telerik.Web.Mvc.UI.TreeViewItemModel()
                        {
                            Checkable = true,
                            Checked = _Checked == null ? false : _Checked.upAutorizado,
                            Value = (_Checked == null ? "upId:0" : "upId:" + _Checked.upId.ToString()) + "#mnuId:" + mnu.mnuId.ToString() + "#acId:" + _Accion.acId.ToString(),
                            ImageUrl = Url.Content("~/Content") + "/General/Vacio-Transparente.png",
                            Text = _Accion.sisAccion.acDescripcion,
                        };
                        _ItemAccion.ImageHtmlAttributes.Add("style", "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat " + _Accion.sisAccion.acImagen + "width: 15px; height: 15px;");

                        _Item.Items.Add(_ItemAccion);
                    }
                    pMenu.Items.Add(_Item);
                }
            }

            return;
        }
    }
}