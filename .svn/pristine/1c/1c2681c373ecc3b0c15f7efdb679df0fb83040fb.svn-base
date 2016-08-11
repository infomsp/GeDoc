namespace GeDoc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using GeDoc.Models;

    public class MenuPrincipalController : Controller
    {
        //
        // GET: /MenuPrincipal/
        private efAccesoADatosEntities context = new efAccesoADatosEntities();

        public IEnumerable<Telerik.Web.Mvc.UI.MenuItem> getMenu(sisUsuario pUsuario)
        {
            List<Telerik.Web.Mvc.UI.MenuItem> _Menu = new List<Telerik.Web.Mvc.UI.MenuItem>();
            var _Datos = context.sisMenu;

            if (pUsuario == null)
            {
                return _Menu.AsEnumerable();
            }

            int _usrId = pUsuario.usrId;

            foreach (var mnu in _Datos.Where(m => (m.mnuNombre.Substring(0,1) == "/" && !m.mnuNombre.Substring(1).Contains("/"))).OrderBy(o => o.mnuOrden).ToList())
            {
                var _Autorizado = mnu.sisUsuarioPermiso.FirstOrDefault(p => p.mnuId == mnu.mnuId && p.usrId == _usrId);
                Telerik.Web.Mvc.UI.MenuItem _Item = new Telerik.Web.Mvc.UI.MenuItem()
                                                        {
                                                            ActionName = mnu.mnuAccion,
                                                            ControllerName = mnu.mnuController,
                                                            ImageUrl = mnu.mnuImagen,
                                                            Text = mnu.mnuTitulo,
                                                            Visible = _Autorizado == null ? false : _Autorizado.upAutorizado
                                                        };
                try
                {
                    var _SubMenu = _Datos.Where(m => (m.mnuNombre.Contains(mnu.mnuNombre) && m.mnuNombre.Replace(mnu.mnuNombre, "").Substring(0, 1) == "/" && !m.mnuNombre.Replace(mnu.mnuNombre, "").Substring(1).Contains("/"))).OrderBy(o => o.mnuOrden).ToList();
                    if (_SubMenu.Count > 0)
                    {
                        //Método recursivo
                        setSubMenu(mnu.mnuNombre, _SubMenu, _Datos.ToList(), _Item, pUsuario);
                    }
                }
                catch { }
                _Menu.Add(_Item);
            }

            return _Menu.AsEnumerable();
        }

        public void setSubMenu(string pMenuNombre, List<sisMenu> _Menu, List<sisMenu> _MenuCompleto, Telerik.Web.Mvc.UI.MenuItem pMenu, sisUsuario pUsuario)
        {
            int _usrId = pUsuario.usrId;

            foreach (var mnu in _Menu)
            {
                var _Autorizado = mnu.sisUsuarioPermiso.FirstOrDefault(p => p.mnuId == mnu.mnuId && p.usrId == _usrId);
                Telerik.Web.Mvc.UI.MenuItem _Item = new Telerik.Web.Mvc.UI.MenuItem()
                                                    {
                                                        ActionName = mnu.mnuAccion,
                                                        ControllerName = mnu.mnuController,
                                                        ImageUrl = mnu.mnuImagen,
                                                        Text = mnu.mnuTitulo,
                                                        Visible = _Autorizado == null ? false : _Autorizado.upAutorizado
                                                    };
                try
                {
                    var _SubMenu = _MenuCompleto.Where(m => (m.mnuNombre.Contains(mnu.mnuNombre) && m.mnuNombre != mnu.mnuNombre)).OrderBy(o => o.mnuOrden).ToList();
                    if (_SubMenu.Count > 0)
                    {
                        setSubMenu(mnu.mnuNombre, _SubMenu, _MenuCompleto, _Item, pUsuario);
                    }
                }
                catch
                {
                    var _SubMenu = _MenuCompleto.Where(m => (m.mnuNombre.Contains(mnu.mnuNombre) && m.mnuNombre != mnu.mnuNombre)).OrderBy(o => o.mnuOrden).ToList();
                    if (_SubMenu.Count > 0)
                    {
                        setSubMenu(mnu.mnuNombre, _SubMenu, _MenuCompleto, _Item, pUsuario);
                    }
                }

                pMenu.Items.Add(_Item);
            }

            return;
        }

        [HttpPost]
        public ActionResult MenuSeleccionado(string pMenuSeleccionado)
        {
            Session["MenuSeleccionado"] = pMenuSeleccionado;
            ViewBag.Catalogo = pMenuSeleccionado;

            return Json(true);
        }

        //Datos para el dropdown
        protected void PopulateDropDownList()
        {
            ViewData["estId_Data"] = new SelectList(context.sisEstilos, "estId", "estDescripcion");

            return;
        }

    }
}
