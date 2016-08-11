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
        public ActionResult _SelectEditingOficinas()
        {
            return View(new GridModel(AllOficinas()));
        }

        public IList<enlUsuariosOficinas> AllOficinas()
        {
            return getDatosOficinas().ToList();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _SaveEditingOficinas(int id)
        {
            enlUsuarioOficina _Item = context.enlUsuarioOficina.Where(p => p.uoId == id).FirstOrDefault();
            enlUsuariosOficinas _Item2 = new enlUsuariosOficinas();

            TryUpdateModel(_Item2);
            TryUpdateModel(_Item);

            Edit(_Item);

            return View(new GridModel(AllOficinas().Where(exp => exp.usrId == _Item.usrId)));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _InsertEditingOficinas()
        {
            enlUsuarioOficina _Item = new enlUsuarioOficina();
            enlUsuariosOficinas _Item2 = new enlUsuariosOficinas();

            TryUpdateModel(_Item2);

            if (TryUpdateModel(_Item))
            {
                Create(_Item);
            }

            return View(new GridModel(AllOficinas().Where(exp => exp.usrId == _Item.usrId)));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _DeleteEditingOficinas(int id)
        {
            var _Item = context.enlUsuarioOficina.Where(e => e.uoId == id).First();

            DeleteConfirmedOficinas(id);

            return View(new GridModel(AllOficinas().Where(exp => exp.usrId == _Item.usrId)));
        }

        private IEnumerable<enlUsuariosOficinas> getDatosOficinas()
        {
            var _Datos = (from d in context.enlUsuarioOficina.ToList()
                          select new enlUsuariosOficinas()
                                    {
                                        usrId = d.usrId,
                                        uoId = d.uoId,
                                        Oficina = new catOficinas() { ofiCodigo = d.catOficina.ofiCodigo, ofiNombre = d.catOficina.ofiNombre },
                                        ofiId = d.ofiId
                                    }).ToList();

            return _Datos.AsEnumerable();
        }

        [GridAction]
        public ActionResult _BindingOficinas(Int64? idUsuario)
        {
            idUsuario = idUsuario == null ? 1 : idUsuario;

            return View(new GridModel<enlUsuariosOficinas>
            {
                Data = AllOficinas().Where(exp => exp.usrId == idUsuario)
            });
        }

        private void Create(enlUsuarioOficina pItem)
        {
            if (ModelState.IsValid)
            {
                context.enlUsuarioOficina.AddObject(pItem);
                //Registra log de usuario
                new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "sisUsuario", "Agregar", "Agrega Oficina al usuario");
                context.SaveChanges();
            }

            return;
        }

        private void Edit(enlUsuarioOficina pItem)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //Registra log de usuario
                    new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "sisUsuario", "Modificar", "Modifica Oficina del usuario");

                    context.SaveChanges();
                }
                catch (Exception ex)
                { }
            }
            return;
        }

        private void DeleteConfirmedOficinas(int id)
        {
            enlUsuarioOficina _Item = context.enlUsuarioOficina.Single(x => x.uoId == id);
            context.enlUsuarioOficina.DeleteObject(_Item);

            //Registra log de usuario
            new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "sisUsuario", "Eliminar", "Elimina Oficina del usuario");
            context.SaveChanges();
        }

    }
}