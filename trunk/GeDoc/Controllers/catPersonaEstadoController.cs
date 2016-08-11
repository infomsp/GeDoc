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
        [GridAction]
        public IList<catPersonasEstados> AllEstados(Int64? idPersona)
        {
            return getDatosEstados(idPersona).ToList();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _SaveEditingEstados(int id)
        {
            catPersonaEstado _Item = context.catPersonaEstado.Where(p => p.pereId == id).FirstOrDefault();
            catPersonasEstados _Item2 = new catPersonasEstados();

            TryUpdateModel(_Item2);
            TryUpdateModel(_Item);

            Edit(_Item);

            return View(new GridModel(AllEstados(_Item.perId)));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _InsertEditingEstados()
        {
            catPersonaEstado _Item = new catPersonaEstado();
            catPersonasEstados _Item2 = new catPersonasEstados();

            TryUpdateModel(_Item2);

            if (TryUpdateModel(_Item))
            {
                Create(_Item);
            }

            return View(new GridModel(AllEstados(_Item.perId)));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _DeleteEditingEstados(int id)
        {
            var _Item = context.catPersonaEstado.Where(e => e.pereId == id).First();

            DeleteConfirmedEstados(id);

            return View(new GridModel(AllEstados(_Item.perId)));
        }

        private IEnumerable<catPersonasEstados> getDatosEstados(Int64? idPersona)
        {
            var _Datos = (from d in context.catPersonaEstado
                          where d.perId == idPersona
                          select new catPersonasEstados()
                                    {
                                        perId = d.perId,
                                        motId = d.motId,
                                        perMotivo = d.motId == null ? "" : d.catMotivo.motDescripcion,
                                        pereObservaciones = d.pereObservaciones,
                                        perEstado = d.tipoIdEstado == null ? "" : d.sisTipo.tipoDescripcion,
                                        pereFecha = d.pereFecha,
                                        pereId = d.pereId,
                                        tipoIdEstado = d.tipoIdEstado
                                    }).ToList();

            return _Datos.AsEnumerable();
        }

        [GridAction]
        public ActionResult _BindingEstados(Int64? idPersona)
        {
            idPersona = idPersona == null ? 1 : idPersona;

            return View(new GridModel<catPersonasEstados>
            {
                Data = AllEstados(idPersona)
            });
        }

        private void Create(catPersonaEstado pItem)
        {
            if (ModelState.IsValid)
            {
                context.catPersonaEstado.AddObject(pItem);
                //Registra log de usuario
                new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catPersona", "Modificar", "Agrega Historial de Cambios de Empleados");
                context.SaveChanges();
            }

            return;
        }

        private void Edit(catPersonaEstado pItem)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //Registra log de usuario
                    new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catPersona", "Modificar", "Modifica Historial de Cambios de Empleados");

                    context.SaveChanges();
                }
                catch (Exception ex)
                { }
            }
            return;
        }

        private void DeleteConfirmedEstados(int id)
        {
            catPersonaEstado _Item = context.catPersonaEstado.Single(x => x.pereId == id);
            context.catPersonaEstado.DeleteObject(_Item);

            //Registra log de usuario
            new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catPersona", "Modificar", "Elimina Historial de Cambios de Empleados");
            context.SaveChanges();
        }

        private string getEstado(string _EstadoCodigo, string _EstadoTexto, string _Tipo)
        {
            if (_EstadoCodigo != null)
            {
                switch (_EstadoCodigo)
                {
                    case "BA":
                        return _Tipo == "I" ? "Rojo.png" : (_Tipo == "E" ? _EstadoTexto : "visible");
                    case "OP":
                        return _Tipo == "I" ? "Verde.png" : (_Tipo == "E" ? _EstadoTexto : "visible");
                    case "NO":
                        return _Tipo == "I" ? "Violeta.png" : (_Tipo == "E" ? _EstadoTexto : "visible");
                    case "ET":
                        return _Tipo == "I" ? "Amarillo.png" : (_Tipo == "E" ? _EstadoTexto : "visible");
                    case "NT":
                        return _Tipo == "I" ? "Celeste.png" : (_Tipo == "E" ? _EstadoTexto : "visible");
                }
            }

            return _Tipo == "I" ? "Gris.png" : (_Tipo == "E" ? "Activo" : "collapse");
        }
    }
}