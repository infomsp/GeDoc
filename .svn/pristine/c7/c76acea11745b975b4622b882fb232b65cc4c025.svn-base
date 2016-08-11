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
    using System.Data.Objects;

    public partial class catBienesController : Controller
    {
        //Edición de datos
        [GridAction]
        public ActionResult _SelectEditingMovimiento()
        {
            return View(new GridModel(AllBienesMovimiento()));
        }

        public IList<catBienMovimientos> AllBienesMovimiento()
        {
            return getDatosBienesMovimiento().ToList();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _SaveEditingBienesMovimiento(int id)
        {
            //catBienesMovimientos _Item = context.catBienesMovimientos.Where(p => p.bimId == id).FirstOrDefault();
            //catBienMovimientos _Item2 = new catBienMovimientos();

            //TryUpdateModel(_Item2);

            //var _Tipo = context.sisTipo.Where(w => w.tipoCodigo == "TR" && w.sisTipoEntidad.tipoeCodigo == "EB").First();
            //if(_Tipo.tipoId == _Item2.tipoId)
            //{
            //    _Item2.bimCodigo = _Tipo.tipoId == _Item2.tipoId ? _Item2.bimCodigo : "";
            //}

            //Edit(_Item);

            return View(new GridModel(AllBienesMovimiento().Where(exp => exp.biId == id)));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _InsertEditingBienesMovimiento()
        {
            catBienMovimientos _Item2 = new catBienMovimientos();
            catBienesMovimientos _Item = new catBienesMovimientos();

            TryUpdateModel(_Item);
            if (TryUpdateModel(_Item2))
            {
                var _Tipo = context.sisTipo.Where(w => w.tipoCodigo == "TR" && w.sisTipoEntidad.tipoeCodigo == "EB").First();
                if(_Tipo.tipoId == _Item2.tipoId)
                {
                    // Validamos el código que no exista duplicado
                    var _Existe = context.catBienes.Where(w => w.biCodigo == _Item2.bimCodigo).Count();
                    if (_Existe > 0)
                    {
                        ModelState.AddModelError("bimCodigo", "No se puede transferir este código por ya existe");
                        return View(new GridModel(AllBienesMovimiento().Where(exp => exp.biId == _Item.biId)));
                    }
                    if (_Item2.bimCodigo == null || _Item2.bimCodigo == "")
                    {
                        ModelState.AddModelError("bimCodigo", "Debe ingresar el código");
                        return View(new GridModel(AllBienesMovimiento().Where(exp => exp.biId == _Item.biId)));
                    }
                }
                _Item2.bimCodigo = _Tipo.tipoId == _Item2.tipoId ? _Item2.bimCodigo : "";
                _Item.bimCodigo = _Item2.bimCodigo;
                _Item.bimfecha = DateTime.Now;

                Create(_Item, _Tipo.tipoId == _Item2.tipoId);
            }

            return View(new GridModel(AllBienesMovimiento().Where(exp => exp.biId == _Item.biId)));
        }

        //[AcceptVerbs(HttpVerbs.Post)]
        //[GridAction]
        //public ActionResult _ActivaRegistroHorario(int id)
        //{
        //    var _Item = context.catAgendaHorario.Where(e => e.aghId == id).First();

            //DeleteConfirmedAgendaHorario(id, true);

        //    return View(new GridModel(AllBienesMovimiento().Where(exp => exp.bimId == _Item.agId)));
        //}

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _DeleteEditingMovimiento(int id)
        {
            var _Item = context.catBienesMovimientos.Where(e => e.bimId == id).First();

            DeleteConfirmedAgendaHorario(id);

            return View(new GridModel(AllBienesMovimiento().Where(exp => exp.bimId == _Item.bimId)));
        }

        private IEnumerable<catBienMovimientos> getDatosBienesMovimiento()
        {
            var _Datos = (from d in context.catBienesMovimientos.ToList()
                          select new catBienMovimientos()
                                    {
                                        biId = d.biId,
                                        bimCodigo = d.bimCodigo,
                                        bimCodigoAnterior = d.bimCodigoAnterior,
                                        bimfecha = d.bimfecha,
                                        bimId = d.bimId,
                                        bimObservaciones = d.bimObservaciones,
                                        tipoId = d.tipoId,
                                        usrId = d.usrId,
                                        cscIdDestino = d.cscIdDestino,
                                        cscIdOrigen = d.cscIdOrigen,
                                        CSDestino = d.catCentroDeSaludConsultorio == null ? new catCentrosDeSaludConsultorios() { cscNombre = "", CentroDeSalud = new catCentrosDeSalud() { sucNombre = "" } } : new catCentrosDeSaludConsultorios() { cscNombre = d.catCentroDeSaludConsultorio.cscNombre, cscId = d.catCentroDeSaludConsultorio.cscId, CentroDeSalud = new catCentrosDeSalud() { sucNombre = d.catCentroDeSaludConsultorio.catCentroDeSalud.csNombre } },
                                        CSOrigen = d.catCentroDeSaludConsultorio1 == null ? new catCentrosDeSaludConsultorios() { cscNombre = "", CentroDeSalud = new catCentrosDeSalud() { sucNombre = "" } } : new catCentrosDeSaludConsultorios() { cscNombre = d.catCentroDeSaludConsultorio1.cscNombre, cscId = d.catCentroDeSaludConsultorio1.cscId, CentroDeSalud = new catCentrosDeSalud() { sucNombre = d.catCentroDeSaludConsultorio1.catCentroDeSalud.csNombre } },
                                        Tipo = d.sisTipo == null ? new sisTipos() : new sisTipos() { tipoCodigo = d.sisTipo.tipoCodigo, tipoDescripcion = d.sisTipo.tipoDescripcion },
                                        Usuario = d.sisUsuario == null ? new sisUsuarios() : new sisUsuarios() { usrApellidoyNombre = d.sisUsuario.usrApellidoyNombre, usrNombreDeUsuario = d.sisUsuario.usrNombreDeUsuario }
                                    }).ToList();

            return _Datos.AsEnumerable();
        }

        [GridAction]
        public ActionResult _BindingMovimiento(Int64? idBien)
        {
            idBien = idBien == null ? 1 : idBien;

            return View(new GridModel<catBienMovimientos>
            {
                Data = AllBienesMovimiento().Where(exp => exp.biId == idBien)
            });
        }

        private void Create(catBienesMovimientos pItem, bool EsTransferencia)
        {
            if (ModelState.IsValid)
            {
                    using (System.Transactions.TransactionScope transaction = new System.Transactions.TransactionScope())
                    {
                        try
                        {
                            if (EsTransferencia)
                            {
                                //Modificamos el código de transferencia
                                var _Bien = context.catBienes.Where(w => w.biId == pItem.biId).Single();
                                pItem.bimCodigoAnterior = _Bien.biCodigo;
                                _Bien.biCodigo = pItem.bimCodigo;
                            }
                            pItem.usrId = (Session["Usuario"] as sisUsuario).usrId;
                            context.catBienesMovimientos.AddObject(pItem);
                            context.SaveChanges(SaveOptions.AcceptAllChangesAfterSave);
                            //Registra log de usuario
                            new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catBienes", "Agregar", EsTransferencia ? "Registra una transferencia" : "Registra un movimiento");
                            //Guardamos los datos de la primera entidad en BBDD en modo PESIMISTA                
                            //Persistimos definitivamente los datos en BBDD
                            context.AcceptAllChanges();
                            //context.SaveChanges();
                            //Marcamos la transacción como completada
                            transaction.Complete();
                        }
                        //Capturamos las excepciones
                        catch (Exception ex)
                        {
                            //Deshacemos la transacción
                            transaction.Dispose();
                            ModelState.AddModelError("bimCodigo", ex.InnerException.Message);
                        }

                    }
                }

            return;
        }

        private void DeleteConfirmedAgendaHorario(int id)
        {
            context.SaveChanges();

            try
            {
                catBienesMovimientos _Item = context.catBienesMovimientos.Single(x => x.bimId == id);
                context.catBienesMovimientos.DeleteObject(_Item);

                //Registra log de usuario
                new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catBienes", "Eliminar", "Elimina un movimiento");
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("bimObservaciones", ex.Message);
            }
        }

    }
}