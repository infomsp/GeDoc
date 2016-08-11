using GeDoc.Models;
using Telerik.Web.Mvc.Extensions;

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


    public partial class proEstadisticaPlanillaController : Controller
    {
        //Edición de datos
        [GridAction]
        public ActionResult _SelectEditing_enlEstadisticaPlanillaEspecialidad(int plaId)
        {
            return View(new GridModel(All_enlEstadisticaPlanillaEspecialidad(plaId)));
        }

        private IList<enlEstadisticaPlanillaEspecialidades> All_enlEstadisticaPlanillaEspecialidad(int plaId)
        {
            return getDatos_enlEstadisticaPlanillaEspecialidad(plaId).ToList();
        }

        private IEnumerable<enlEstadisticaPlanillaEspecialidades> getDatos_enlEstadisticaPlanillaEspecialidad(int plaId)
        {
            var _Datos = (from d in context.enlEstadisticaPlanillaEspecialidad.ToList()
                          where d.plaId == plaId
                          select new enlEstadisticaPlanillaEspecialidades()
                          {
                              peId = d.peId,
                              plaId = d.plaId,
                              espId = d.espId,
                              Especialidades = new catEspecialidades()
                              {
                                  espAbreviatura = d.catEspecialidad.espAbreviatura,
                                  espCodigo = d.catEspecialidad.espCodigo,
                                  espDescripcionPlanilla = d.catEspecialidad.espDescripcionPlanilla,
                                  espId = d.espId,
                                  espIdMHO = d.catEspecialidad.espIdMHO,
                                  espIdPadre = d.catEspecialidad.espIdPadre,
                                  espNombre = d.catEspecialidad.espNombre
                              },
                              planillaValidada = d.planillaValidada,
                              planillaValidadaImagen = getImagenPlanillaValidada(d.planillaValidada),
                              planillaGenerada = d.planillaGenerada

                          }).ToList();
            return _Datos.AsEnumerable();
        }

        private string getImagenPlanillaValidada(int? _planillaValidada)
        {
            string _imagenCandado;
            if (_planillaValidada == 1)
                _imagenCandado = "candadoVerde.png";
            else
            {
                _imagenCandado = "candadoRojo.png";
            }
            return _imagenCandado;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _InsertEditing_enlEstadisticaPlanillaEspecialidad(int plaId,int espId)
        {
            enlEstadisticaPlanillaEspecialidad _Item = new enlEstadisticaPlanillaEspecialidad();
        
            if (TryUpdateModel(_Item))
            {
                _Item.plaId = plaId;
                _Item.espId = (short)espId;
                if (_Item.plaId > 0)
                {
                    enlEstadisticaPlanillaEspecialidad _datos = context.enlEstadisticaPlanillaEspecialidad.Where(p => p.plaId == _Item.plaId & p.espId == _Item.espId).FirstOrDefault();

                    if (_datos == null)
                    {
                        Create_enlEstadisticaPlanillaEspecialidad(_Item);
                    }
                }
            }

            return View(new GridModel(getDatos_enlEstadisticaPlanillaEspecialidad(plaId)));
        }

        private void Create_enlEstadisticaPlanillaEspecialidad(enlEstadisticaPlanillaEspecialidad item)
        {
            context.enlEstadisticaPlanillaEspecialidad.AddObject(item);
            context.SaveChanges();
        }


        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _SaveEditing_enlEstadisticaPlanillaEspecialidad(int id)
        {
            enlEstadisticaPlanillaEspecialidad _Item = context.enlEstadisticaPlanillaEspecialidad.Where(p => p.peId == id).FirstOrDefault();

            TryUpdateModel(_Item);

            Edit_enlEstadisticaPlanillaEspecialidad(_Item);

            return View(new GridModel(All_enlEstadisticaPlanillaEspecialidad(_Item.peId)));
        }
        private void Edit_enlEstadisticaPlanillaEspecialidad(enlEstadisticaPlanillaEspecialidad pItem)
        {
            if (ModelState.IsValid)
            {
                //pItem = pItem.usrId = (Session["Usuario"] as sisUsuario).usrId;
                context.SaveChanges();
            }
            return;
        }


        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _Delete_enlEstadisticaPlanillaEspecialidad(int id)
        {
            enlEstadisticaPlanillaEspecialidad _Item = context.enlEstadisticaPlanillaEspecialidad.Single(x => x.peId == id);
            DeleteConfirmed_enlEstadisticaPlanillaEspecialidad(id);
            return View(new GridModel(All_enlEstadisticaPlanillaEspecialidad(_Item.plaId)));
        }
        private void DeleteConfirmed_enlEstadisticaPlanillaEspecialidad(int id)
        {
            enlEstadisticaPlanillaEspecialidad _Item = context.enlEstadisticaPlanillaEspecialidad.Single(x => x.peId == id);
            context.enlEstadisticaPlanillaEspecialidad.DeleteObject(_Item);
            context.SaveChanges();
        }
    }

}