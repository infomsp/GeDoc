using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace GeDoc.Controllers
{
    using System.Linq;
    using Models;
    using System.Web.Mvc;

    public partial class catTurnoController : Controller
    {
        public void CargarSalas()
        {
            var _csId = (Session["UsuarioCentroDeSalud"] as sisUsuariosCentroDeSalud).csId;

            var Salas = (from x in context.catCentroDeSaludSala.Where(x => x.csId == _csId)
                                          select new ListaDesplegableWS()
                                          {
                                              idLista = x.cssId,
                                              Texto = x.cssNombre
                                          }).ToList();

            ViewData["cssId_Lista_Data"] = Salas;
            var UnaSala = -1;
            if (Salas.Count > 0)
            {
                UnaSala = Salas.First().idLista;
            }
            ViewData["csscId_Lista_Data"] = (from x in context.catCentroDeSaludSalaConsultorio.Where(x => x.cssId == UnaSala)
                                          select new ListaDesplegableWS()
                                          {
                                              idLista = x.csscId,
                                              Texto = x.csscNombre
                                          }).ToList();

            ViewData["csstId_Lista_Data"] = (from x in context.catCentroDeSaludSalaTelevisor.Where(x => x.cssId == UnaSala)
                                             select new ListaDesplegableWS()
                                             {
                                                 idLista = x.csstId,
                                                 Texto = x.csstNombre
                                             }).ToList();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult getSalaConsultorios(int SalaId)
        {
            var Consultorios = (from x in context.catCentroDeSaludSalaConsultorio.Where(x => x.cssId == SalaId)
                                 select new ListaDesplegableWS()
                                 {
                                     idLista = x.csscId,
                                     Texto = x.csscNombre
                                 }).ToList();
            string listaProvincias = "<select id=\"csscId_Lista\" class=\"csscId_Lista\"" + (Consultorios.Count == 0 ? " disabled " : "") + " >";
            foreach (var Item in Consultorios)
            {
                listaProvincias += "<option value=\"" + Item.idLista + "\">" + Item.Texto + "</option>";
            }
            if (!Consultorios.Any())
            {
                listaProvincias += "<option value=\"-1\" class=\"ES-PlaceHolder\">Ningún Item seleccionado</option>";
            }
            listaProvincias += "</select>";

            return Json(listaProvincias);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult getSalaTelevisores(int SalaId)
        {
            var Televisores = (from x in context.catCentroDeSaludSalaTelevisor.Where(x => x.cssId == SalaId)
                                select new ListaDesplegableWS()
                                {
                                    idLista = x.csstId,
                                    Texto = x.csstNombre
                                }).ToList();
            string listaDesplegable = "<select id=\"csstId_Lista\" class=\"csstId_Lista\"" + (Televisores.Count == 0 ? " disabled " : "") + " >";
            foreach (var Item in Televisores)
            {
                listaDesplegable += "<option value=\"" + Item.idLista + "\">" + Item.Texto + "</option>";
            }
            if (!Televisores.Any())
            {
                listaDesplegable += "<option value=\"-1\" class=\"ES-PlaceHolder\">Ningún Item seleccionado</option>";
            }
            listaDesplegable += "</select>";

            return Json(listaDesplegable);
        }

    }
}