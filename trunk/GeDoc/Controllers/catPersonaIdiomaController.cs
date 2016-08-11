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

    public partial class catPersonaController : Controller
    {
        //Edición de datos
        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _Save(catPersonasIdiomas id)
        {
            //Registra log de usuario
            new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catPersona", "Modificar", "Modifica Historial de Cambios de Empleados");

            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult getIdiomas(int idPersona)
        {
            return Json(getDatosIdiomas(idPersona));
        }

        public IEnumerable<Telerik.Web.Mvc.UI.TreeViewItemModel> getDatosIdiomas(int idPersona)
        {
            var _Datos = (from p in context.catIdioma.ToList()
                          join d in context.catPersonaIdioma.Where(x => x.perId == idPersona).ToList() on p.idiomaId equals d.idiomaId into res
                          from i in res.DefaultIfEmpty()
                          select new Telerik.Web.Mvc.UI.TreeViewItemModel()
                          {
                              Checkable = true,
                              Checked = i != null,
                              Value = "idiomaId:" + p.idiomaId.ToString() + (i == null ? "#pidiomaId:0" : "#pidiomaId:" + i.pidiomaId.ToString()),
                              Text = p.idiomaDescripcion
                          }).ToList();

            return _Datos.OrderBy(o => o.Text).AsEnumerable();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult setIdiomas(int idPersona, string Datos)
        {
            List<catPersonaIdioma> _Insert = new List<catPersonaIdioma>();
            List<catPersonaIdioma> _Delete = new List<catPersonaIdioma>();

            bool _Continuar = true;
            while (_Continuar)
            {
                string _Registro = Datos.Substring(0, Datos.IndexOf("\n"));
                short _idiomaId = Convert.ToInt16(_Registro.Substring(_Registro.IndexOf("idiomaId:") + 9, _Registro.IndexOf("#", _Registro.IndexOf("idiomaId:")) - (_Registro.IndexOf("idiomaId:") + 9)));
                short _pidiomaId = Convert.ToInt16(_Registro.Substring(_Registro.IndexOf("pidiomaId:") + 10, _Registro.IndexOf("#", _Registro.IndexOf("pidiomaId:")) - (_Registro.IndexOf("pidiomaId:") + 10)));
                bool _upAutorizado = _Registro.Substring(_Registro.IndexOf("upAutorizado:") + 13) == "1";
                if (_pidiomaId == 0 && _upAutorizado)
                {
                    _Insert.Add(new catPersonaIdioma() { perId = idPersona, idiomaId = _idiomaId });
                }
                else if (_pidiomaId != 0 && !_upAutorizado)
                {
                    _Delete.Add(new catPersonaIdioma() { pidiomaId = _pidiomaId });
                }

                Datos = Datos.Remove(0, _Registro.Length + 1);
                _Continuar = (Datos != "");
            }

            try
            {
                //Ahora guardamos todos los cambios\\
                foreach (var _Ins in _Insert)
                {
                    context.catPersonaIdioma.AddObject(_Ins);
                }
                foreach (var _Del in _Delete)
                {
                    var _Item = context.catPersonaIdioma.Where(p => p.pidiomaId == _Del.pidiomaId).FirstOrDefault();

                    context.catPersonaIdioma.DeleteObject(_Item);
                }
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(false);
            }

            return Json(true);
        }
    }
}