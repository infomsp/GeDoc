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
        public IList<catPersonasEspecialidades> AllEspecialidades(Int64? idPersona)
        {
            return getDatosEspecialidades(idPersona).ToList();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _SaveEditingEspecialidades(int id)
        {
            catPersonaEspecialidad _Item = context.catPersonaEspecialidad.Where(p => p.peId == id).FirstOrDefault();
            catPersonasEspecialidades _Item2 = new catPersonasEspecialidades();

            TryUpdateModel(_Item2);
            TryUpdateModel(_Item);

            Edit(_Item);

            return View(new GridModel(AllEspecialidades(_Item.perId)));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _InsertEditingEspecialidades()
        {
            catPersonaEspecialidad _Item = new catPersonaEspecialidad();
            catPersonasEspecialidades _Item2 = new catPersonasEspecialidades();

            TryUpdateModel(_Item2);

            if (TryUpdateModel(_Item))
            {
                Create(_Item);
            }

            return View(new GridModel(AllEspecialidades(_Item.perId)));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _DeleteEditingEspecialidades(int id)
        {
            var _Item = context.catPersonaEspecialidad.Where(e => e.peId == id).First();

            DeleteConfirmedEspecialidades(id);

            return View(new GridModel(AllEspecialidades(_Item.perId)));
        }

        private IEnumerable<catPersonasEspecialidades> getDatosEspecialidades(Int64? idPersona)
        {
            var _Datos = (from d in context.catPersonaEspecialidad
                          where d.perId == idPersona
                          select new catPersonasEspecialidades()
                                    {
                                        perId = d.perId,
                                        espId = d.espId,
                                        peId = d.peId,
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

            return View(new GridModel<catPersonasEspecialidades>
            {
                Data = AllEspecialidades(idPersona)
            });
        }

        private void Create(catPersonaEspecialidad pItem)
        {
            if (ModelState.IsValid)
            {
                context.catPersonaEspecialidad.AddObject(pItem);
                //Registra log de usuario
                new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catPersona", "Agregar", "Agrega especialidad");
                context.SaveChanges();
            }

            return;
        }

        private void Edit(catPersonaEspecialidad pItem)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //Registra log de usuario
                    new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catPersona", "Modificar", "Modifica especialidad");

                    context.SaveChanges();
                }
                catch (Exception ex)
                { }
            }
            return;
        }

        private void DeleteConfirmedEspecialidades(int id)
        {
            catPersonaEspecialidad _Item = context.catPersonaEspecialidad.Single(x => x.peId == id);
            context.catPersonaEspecialidad.DeleteObject(_Item);

            //Registra log de usuario
            new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catPersona", "Eliminar", "Elimina especialidad");
            context.SaveChanges();
        }

    }
}