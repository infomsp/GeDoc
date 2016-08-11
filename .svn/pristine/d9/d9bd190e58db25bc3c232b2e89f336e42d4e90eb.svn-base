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

    public partial class sisUsuarioController : Controller
    {
        //Edición de datos
        [GridAction]
        public ActionResult _SelectEditingCentroDeSalud()
        {
            return View(new GridModel(AllCentroDeSalud()));
        }

        public IList<sisUsuariosCentroDeSalud> AllCentroDeSalud()
        {
            return getDatosCentroDeSalud().ToList();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _SaveEditingCentroDeSalud(int id)
        {
            sisUsuarioCentroDeSalud _Item = context.sisUsuarioCentroDeSalud.Where(p => p.ucsId == id).FirstOrDefault();
            sisUsuariosCentroDeSalud _Item2 = new sisUsuariosCentroDeSalud();

            TryUpdateModel(_Item2);
            TryUpdateModel(_Item);

            Edit(_Item);

            return View(new GridModel(AllCentroDeSalud().Where(exp => exp.usrId == _Item.usrId)));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _InsertEditingCentroDeSalud()
        {
            sisUsuarioCentroDeSalud _Item = new sisUsuarioCentroDeSalud();
            sisUsuariosCentroDeSalud _Item2 = new sisUsuariosCentroDeSalud();

            TryUpdateModel(_Item2);

            if (TryUpdateModel(_Item))
            {
                Create(_Item);
            }

            return View(new GridModel(AllCentroDeSalud().Where(exp => exp.usrId == _Item.usrId)));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _DeleteEditingCentroDeSalud(int id)
        {
            var _Item = context.sisUsuarioCentroDeSalud.Where(e => e.ucsId == id).First();

            DeleteConfirmedCentroDeSalud(id);

            return View(new GridModel(AllCentroDeSalud().Where(exp => exp.usrId == _Item.usrId)));
        }

        private IEnumerable<sisUsuariosCentroDeSalud> getDatosCentroDeSalud()
        {
            var _Datos = (from d in context.sisUsuarioCentroDeSalud.ToList()
                          select new sisUsuariosCentroDeSalud()
                                    {
                                        usrId = d.usrId,
                                        ucsId = d.ucsId,
                                        ucsCentroDeSalud = d.catCentroDeSalud.csNombre,
                                        csId = d.csId
                                    }).ToList();

            return _Datos.AsEnumerable();
        }

        [GridAction]
        public ActionResult _BindingCentroDeSalud(Int64? idUsuario)
        {
            idUsuario = idUsuario == null ? 1 : idUsuario;

            return View(new GridModel<sisUsuariosCentroDeSalud>
            {
                Data = AllCentroDeSalud().Where(exp => exp.usrId == idUsuario)
            });
        }

        private void Create(sisUsuarioCentroDeSalud pItem)
        {
            if (ModelState.IsValid)
            {
                context.sisUsuarioCentroDeSalud.AddObject(pItem);
                //Registra log de usuario
                new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "sisUsuario", "Agregar", "Agrega Centro de Salud al usuario");
                context.SaveChanges();
            }

            return;
        }

        private void Edit(sisUsuarioCentroDeSalud pItem)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //Registra log de usuario
                    new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "sisUsuario", "Modificar", "Modifica Centro de Salud del usuario");

                    context.SaveChanges();
                }
                catch (Exception ex)
                { }
            }
            return;
        }

        private void DeleteConfirmedCentroDeSalud(int id)
        {
            sisUsuarioCentroDeSalud _Item = context.sisUsuarioCentroDeSalud.Single(x => x.ucsId == id);
            context.sisUsuarioCentroDeSalud.DeleteObject(_Item);

            //Registra log de usuario
            new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "sisUsuario", "Eliminar", "Elimina Centro de Salud del usuario");
            context.SaveChanges();
        }

    }
}