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
        public IList<catPersonasGrupoFamiliar> AllGrupoFamiliar(Int64? idPersona)
        {
            return getDatosGrupoFamiliar(idPersona).ToList();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _SaveEditingGrupoFamiliar(int id)
        {
            catPersonaGrupoFamiliar _Item = context.catPersonaGrupoFamiliar.Where(p => p.gfId == id).FirstOrDefault();
            catPersonasGrupoFamiliar _Item2 = new catPersonasGrupoFamiliar();

            TryUpdateModel(_Item2);
            TryUpdateModel(_Item);

            Edit(_Item);

            return View(new GridModel(AllGrupoFamiliar(_Item.perId)));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _InsertEditingGrupoFamiliar()
        {
            catPersonaGrupoFamiliar _Item = new catPersonaGrupoFamiliar();
            catPersonasGrupoFamiliar _Item2 = new catPersonasGrupoFamiliar();

            TryUpdateModel(_Item2);

            if (TryUpdateModel(_Item))
            {
                Create(_Item);
            }

            return View(new GridModel(AllGrupoFamiliar(_Item.perId)));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _DeleteEditingGrupoFamiliar(int id)
        {
            var _Item = context.catPersonaGrupoFamiliar.Where(e => e.gfId == id).First();

            DeleteConfirmedGrupoFamiliar(id);

            return View(new GridModel(AllGrupoFamiliar(_Item.perId)));
        }

        private IEnumerable<catPersonasGrupoFamiliar> getDatosGrupoFamiliar(Int64? idPersona)
        {
            var _Datos = (from d in context.catPersonaGrupoFamiliar
                          where d.perId == idPersona
                          select new catPersonasGrupoFamiliar()
                                    {
                                        perId = d.perId,
                                        gfParentesco = d.sisTipo2.tipoDescripcion,
                                        tipoId = d.tipoId,
                                        tipoIdSexo = d.tipoIdSexo,
                                        gfSexo = d.sisTipo.tipoDescripcion,
                                        tipoIdTipoDocumento = d.tipoIdTipoDocumento,
                                        gfTipoDocumento = d.sisTipo1.tipoDescripcion,
                                        gfApellidoyNombre = d.gfApellidoyNombre,
                                        gfFechaNacimiento = d.gfFechaNacimiento,
                                        gfFechaFallecimiento = d.gfFechaFallecimiento,
                                        gfId = d.gfId,
                                        gfNumeroDocumento = d.gfNumeroDocumento
                                    }).ToList();

            return _Datos.AsEnumerable();
        }

        [GridAction]
        public ActionResult _BindingGrupoFamiliar(Int64? idPersona)
        {
            idPersona = idPersona == null ? 1 : idPersona;

            return View(new GridModel<catPersonasGrupoFamiliar>
            {
                Data = AllGrupoFamiliar(idPersona)
            });
        }

        private void Create(catPersonaGrupoFamiliar pItem)
        {
            if (ModelState.IsValid)
            {
                context.catPersonaGrupoFamiliar.AddObject(pItem);
                //Registra log de usuario
                new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catPersona", "Agregar", "Agrega Grupo Familiar");
                context.SaveChanges();
            }

            return;
        }

        private void Edit(catPersonaGrupoFamiliar pItem)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //Registra log de usuario
                    new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catPersona", "Modificar", "Modifica Grupo Familiar");

                    context.SaveChanges();
                }
                catch (Exception ex)
                { }
            }
            return;
        }

        private void DeleteConfirmedGrupoFamiliar(int id)
        {
            catPersonaGrupoFamiliar _Item = context.catPersonaGrupoFamiliar.Single(x => x.gfId == id);
            context.catPersonaGrupoFamiliar.DeleteObject(_Item);

            //Registra log de usuario
            new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catPersona", "Eliminar", "Elimina Grupo Familiar");
            context.SaveChanges();
        }

    }
}