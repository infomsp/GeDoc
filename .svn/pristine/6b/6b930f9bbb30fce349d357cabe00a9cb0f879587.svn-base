namespace GeDoc
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Telerik.Web.Mvc;
    using GeDoc.Models;

    public partial class catCentrosDeSaludController : Controller
    {
        private dcAccesoCompadDataContext context = new dcAccesoCompadDataContext();

        //Edición de datos

        [GridAction]
        public ActionResult _SelectEditing()
        {
            return View(new GridModel(All()));
        }

        private IList<catCentrosDeSalud> All()
        {
                return getDatos().ToList();
        }

        private IEnumerable<catCentrosDeSalud> getDatos()
        {
            var _Datos = (from d in context.CatSucursals
                          select new catCentrosDeSalud()
                                    {
                                        sucId = d.sucId,
                                        sucCodigo = d.sucCodigo,
                                        sucNombre = d.sucNombre
                                    }).ToList();

            return _Datos.AsEnumerable();
        }
    }
}