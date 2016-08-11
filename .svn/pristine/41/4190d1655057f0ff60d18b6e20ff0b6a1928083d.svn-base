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

    public partial class catPersonaContratadosController : Controller
    {
        //Edición de datos
        [GridAction]
        public ActionResult _SelectEditingEspecialidades()
        {
            return View(new GridModel(AllEspecialidades()));
        }

        public IList<catPersonasContratadosEspecialidades> AllEspecialidades()
        {
            return getDatosEspecialidades().ToList();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _SaveEditingEspecialidades(int id)
        {
            catPersonaContratadosEspecialidad _Item = context.catPersonaContratadosEspecialidad.Where(p => p.coneId == id).FirstOrDefault();
            catPersonasContratadosEspecialidades _Item2 = new catPersonasContratadosEspecialidades();

            TryUpdateModel(_Item2);
            TryUpdateModel(_Item);

            Edit(_Item);

            return View(new GridModel(AllEspecialidades().Where(exp => exp.conId == _Item.conId)));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _InsertEditingEspecialidades()
        {
            catPersonaContratadosEspecialidad _Item = new catPersonaContratadosEspecialidad();
            catPersonasContratadosEspecialidades _Item2 = new catPersonasContratadosEspecialidades();

            TryUpdateModel(_Item2);

            if (TryUpdateModel(_Item))
            {
                Create(_Item);
            }

            return View(new GridModel(AllEspecialidades().Where(exp => exp.conId == _Item.conId)));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _DeleteEditingEspecialidades(int id)
        {
            var _Item = context.catPersonaContratadosEspecialidad.Where(e => e.coneId == id).First();

            DeleteConfirmedEspecialidades(id);

            return View(new GridModel(AllEspecialidades().Where(exp => exp.conId == _Item.conId)));
        }

        private IEnumerable<catPersonasContratadosEspecialidades> getDatosEspecialidades()
        {
            var _Datos = (from d in context.catPersonaContratadosEspecialidad.ToList()
                          select new catPersonasContratadosEspecialidades()
                                    {
                                        conId = d.conId,
                                        espId = d.espId,
                                        coneId = d.coneId,
                                        peEspecialidades = new catEspecialidades()
                                                                {
                                                                    espAbreviatura = d.catEspecialidad.espAbreviatura,
                                                                    espCodigo = d.catEspecialidad.espCodigo,
                                                                    espDescripcionPlanilla = d.catEspecialidad.espDescripcionPlanilla,
                                                                    espId = d.espId,
                                                                    espIdMHO = d.catEspecialidad.espIdMHO,
                                                                    espIdPadre = d.catEspecialidad.espIdPadre,
                                                                    espNombre = d.catEspecialidad.espNombre
                                                                }
                                    }).ToList();

            return _Datos.AsEnumerable();
        }

        [GridAction]
        public ActionResult _BindingEspecialidades(Int64? idPersona)
        {
            idPersona = idPersona == null ? 1 : idPersona;

            return View(new GridModel<catPersonasContratadosEspecialidades>
            {
                Data = AllEspecialidades().Where(exp => exp.conId == idPersona)
            });
        }

        private void Create(catPersonaContratadosEspecialidad pItem)
        {
            if (ModelState.IsValid)
            {
                context.catPersonaContratadosEspecialidad.AddObject(pItem);
                //Registra log de usuario
                new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catPersonaContratados", "Agregar", "Agrega especialidad");
                context.SaveChanges();
            }

            return;
        }

        private void Edit(catPersonaContratadosEspecialidad pItem)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //Registra log de usuario
                    new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catPersonaContratados", "Modificar", "Modifica especialidad");

                    context.SaveChanges();
                }
                catch (Exception ex)
                { }
            }
            return;
        }

        private void DeleteConfirmedEspecialidades(int id)
        {
            catPersonaContratadosEspecialidad _Item = context.catPersonaContratadosEspecialidad.Single(x => x.coneId == id);
            context.catPersonaContratadosEspecialidad.DeleteObject(_Item);

            //Registra log de usuario
            new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catPersonaContratados", "Eliminar", "Elimina especialidad");
            context.SaveChanges();
        }

    }
}