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

    public partial class conpadPadronPersonasController : Controller
    {
        private efAccesoADatosEntities context = new efAccesoADatosEntities();

        //Edición de datos

        [GridAction]
        public ActionResult _SelectEditing()
        {
            return View(new GridModel(All()));
        }

        private IList<conpadPadronDePersonas> All()
        {
                return getDatos().ToList();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _SaveEditing(int id, bool pClasificacion)
        {
            if (Session["UsuarioCentroDeSalud"] == null)
            {
                RedirectToAction("LogOff", "Account");
                return null;
            }
            conpadPadronPersonas _Item = context.conpadPadronPersonas.Where(p => p.ppId == id).FirstOrDefault();

            var _csId = (Session["UsuarioCentroDeSalud"] as sisUsuariosCentroDeSalud).csId;
            var _CS = context.catCentroDeSalud.Where(w => w.csId == _csId).FirstOrDefault();
            var _csNombre = "";
            if(_CS != null)
            {
                _csNombre = _CS.csNombre;
            }

            if (!pClasificacion)
            {
                _Item.csId = null;
                _csNombre = "";
            }
            else
            {
                _Item.csId = _csId;
            }

            var _Resultado = Edit(_Item);
            if (_Resultado != "OK")
            {
                return Json(new { IsValid = false, Clasificacion = !pClasificacion, CS = "" });
            }

            return Json(new { IsValid = true, Clasificacion = pClasificacion, CS = _csNombre });
        }

        //[AcceptVerbs(HttpVerbs.Post)]
        //[CultureAwareAction]
        //[GridAction]
        //public ActionResult _InsertEditing()
        //{
        //    catAgenda _Item = new catAgenda();

        //    if (TryUpdateModel(_Item))
        //    {
        //        if (Session["UsuarioCentroDeSalud"] == null)
        //        {
        //            RedirectToAction("LogOff", "Account");
        //            return null;
        //        }
        //        var _csId = (Session["UsuarioCentroDeSalud"] as GeDoc.sisUsuariosCentroDeSalud).csId;

        //        _Item.csId = _csId;
        //        Create(_Item);
        //    }

        //    return View(new GridModel(All()));
        //}

        //[AcceptVerbs(HttpVerbs.Post)]
        //[GridAction]
        //public ActionResult _DeleteEditing(int id)
        //{
        //    if (Session["UsuarioCentroDeSalud"] == null)
        //    {
        //        RedirectToAction("LogOff", "Account");
        //        return null;
        //    }
        //    DeleteConfirmed(id, false);

        //    return View(new GridModel(All()));
        //}

        //[AcceptVerbs(HttpVerbs.Post)]
        //[GridAction]
        //public ActionResult _ActivaRegistro(int id)
        //{
        //    DeleteConfirmed(id, true);

        //    return View(new GridModel(All()));
        //}

        private IEnumerable<conpadPadronDePersonas> getDatos()
        {
            if (Session["UsuarioCentroDeSalud"] == null)
            {
                RedirectToAction("LogOff", "Account");
                return null;
            }
            var _csId = (Session["UsuarioCentroDeSalud"] as GeDoc.sisUsuariosCentroDeSalud).csId;
            var _CsDepto = context.catCentroDeSalud.Where(w => w.csId == _csId).First().catDepartamento;
            var _Datos = (from d in context.conpadPadronPersonas.ToList()
                          where (_CsDepto == null ? "" : ((d.ppDepartamento == null || d.ppDepartamento == "") ? _CsDepto.depNombre : d.ppDepartamento.ToLower())) == (_CsDepto == null ? "" : _CsDepto.depNombre.ToLower())
                          orderby d.ppApellido, d.ppNombre
                          select new conpadPadronDePersonas()
                                    {
                                        ppApellido = d.ppApellido,
                                        ppCP = d.ppCP,
                                        ppCUIL = d.ppCUIL,
                                        ppDepartamento = d.ppDepartamento,
                                        ppDomBarrio_Loc = d.ppDomBarrio_Loc,
                                        ppDomCalle = d.ppDomCalle,
                                        ppDomDepto = d.ppDomDepto,
                                        ppDomNroCalle = d.ppDomNroCalle,
                                        ppDomPiso = d.ppDomPiso,
                                        ppEdad = d.ppEdad,
                                        ppFechaNacimiento = d.ppFechaNacimiento,
                                        ppId = d.ppId,
                                        ppNivelValidacion = d.ppNivelValidacion,
                                        ppNombre = d.ppNombre,
                                        ppNroDoc = d.ppNroDoc,
                                        ppObraSocial = d.ppObraSocial,
                                        ppSexo = d.ppSexo,
                                        ppSexoId = d.ppSexoId,
                                        ppTelefono1 = d.ppTelefono1,
                                        ppTelefono2 = d.ppTelefono2,
                                        ppTipoEdad = d.ppTipoEdad,
                                        Localidad = d.Localidad,
                                        locDepartamento = d.locDepartamento,
                                        locId = d.locId,
                                        tdocId = d.tdocId,
                                        tipoDocumento = d.tipoDocumento,
                                        ttelId1 = d.ttelId1,
                                        ttelId2 = d.ttelId2,
                                        CentroDeSalud = d.csId == null ? "" : d.catCentroDeSalud.csNombre,
                                        Clasificado = d.csId != null,
                                        Documento = d.tipoDocumento + ": " + d.ppNroDoc,
                                        csId = d.csId
                                    }).ToList();

            return _Datos.AsEnumerable();
        }

        public ActionResult Index()
        {
            ViewBag.Catalogo = "Padrón de Personas";// +(Session["UsuarioCentroDeSalud"] as GeDoc.sisUsuariosCentroDeSalud).ucsCentroDeSalud;
            PopulateDropDownList();

            return PartialView();
        }

        //private void Create(catAgenda pItem)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            pItem.agActivo = true;
        //            if (pItem.perId < 0)
        //            {
        //                pItem.conId = (pItem.perId * -1);
        //                pItem.perId = null;

        //                if (context.catAgenda.Where(w => w.conId == pItem.conId && w.espId == pItem.espId).Count() > 0)
        //                {
        //                    ModelState.AddModelError("perId", "Ya tiene agenda asignada");
        //                }
        //            }
        //            else
        //            {
        //                if (context.catAgenda.Where(w => w.perId == pItem.perId && w.espId == pItem.espId).Count() > 0)
        //                {
        //                    ModelState.AddModelError("perId", "Ya tiene agenda asignada");
        //                    return;
        //                }
        //            }

        //            context.catAgenda.AddObject(pItem);

        //            new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catAgenda", "Agregar", "Agrega agenda");

        //            context.SaveChanges();
        //        }
        //        catch (Exception ex)
        //        {
        //            ModelState.AddModelError("agDuracion", ex.Message);
        //        }
        //    }

        //    return;
        //}

        private string Edit(conpadPadronPersonas pItem)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //Registra log de usuario
                    new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "conpadPadronPersonas", "Modificar", "Asigna persona");

                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
            return "OK";
        }

        //private void DeleteConfirmed(int id, bool pEstado)
        //{
        //    try
        //    {
        //        catAgenda _Item = context.catAgenda.Single(x => x.agId == id);
        //        _Item.agActivo = pEstado;
        //        //Registra log de usuario
        //        new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catAgenda", "Eliminar", "Elimina agenda");
        //        context.SaveChanges();
        //    }
        //    catch (Exception ex)
        //    {
        //        ModelState.AddModelError("agDuracion", ex.Message);
        //    }
        //}

        //Datos para el dropdown
        protected void PopulateDropDownList()
        {
        }
    }
}