namespace GeDoc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Telerik.Web.Mvc;
    using GeDoc.Models;

    public partial class HistoriaClinicaController : Controller
    {
        [GridAction]
        public ActionResult getHCListaCuidadosPreventivos(int hcaduid)
        {
            var Riesgo = context.catHCAduPraPreventivaResul.Where(f => f.hcaduid == hcaduid && f.catHCAduPraPreventiva.aduPraPrevCodigo == 31).OrderByDescending(o => o.aduPraPrevFecha).FirstOrDefault();
            var RiesgoCardiovascular = "";
            if (Riesgo != null)
            {
                var NivelRiesgo = int.Parse(Riesgo.aduPraPrevResultado.Replace(".00", ""));
                RiesgoCardiovascular = NivelRiesgo <= 1 ? "" : (NivelRiesgo == 2 ? "MEDIO" : "ALTO");
            }

            var preventivo = (from p in context.getHCCuidadosPreventivos(hcaduid).ToList()
                              select new catHCAduPraPreventivaResulWS()
                              {
                                  hcaduid = (int)p.hcaduid,
                                  aduPraPrevDescri = p.aduPraPrevDescri,
                                  aduPraPrevResId = p.aduPraPrevResId,
                                  aduPraPrevId = p.aduPraPrevId,
                                  usrId = p.usrId,
                                  aduPraPrevFecha = p.aduPraPrevFecha,
                                  aduPraPrevResultado = p.aduPraPrevId != 31 ? p.aduPraPrevResultado : (p.aduPraPrevResultado == "-1.00" || p.aduPraPrevResultado == "1.00" ? "<10%" : (p.aduPraPrevResultado == "2.00" ? "10% a <20%" : (p.aduPraPrevResultado == "3.00" ? "20% a 30%" : (p.aduPraPrevResultado == "4.00" ? "30% a <40%" : ">=40%")))),
                                  Usuario = p.usrApellidoyNombre,
                                  Cantidad = p.Cantidad,
                                  ColorRiesgo = p.aduPraPrevId != 31 ? "VerdeClaro-2.png" : (p.aduPraPrevResultado == "-1.00" || p.aduPraPrevResultado == "1.00" ? "VerdeClaro-2.png" : (p.aduPraPrevResultado == "2.00" ? "ConDetalle.png" : (p.aduPraPrevResultado == "3.00" ? "Marron.png" : (p.aduPraPrevResultado == "4.00" ? "Naranja-2.png" : "Rojo(2).png")))),
                                  RiesgoCardioActual = RiesgoCardiovascular,
                                  MostrarImagen = p.aduPraPrevId == 31
                              }).OrderByDescending(o => o.aduPraPrevFecha).ToList();

            return PartialView(new GridModel(preventivo));
        }

        [GridAction]
        public ActionResult getHistoriaClinicaCuidadosPreventivos(int id, long phcaduid)
        {
            var _Datos = (from x in context.catHCAduPraPreventivaResul
                          where x.aduPraPrevResId == id
                          select new catHCAduPraPreventivaResulWS()
                          {
                              hcaduid = x.hcaduid,
                              aduPraPrevFecha = x.aduPraPrevFecha,
                              aduPraPrevResultado = x.aduPraPrevResultado,
                              aduPraPrevId = x.aduPraPrevId,
                              aduPraPrevResId = x.aduPraPrevResId,
                              usrId = x.usrId
                          }).FirstOrDefault();

            ViewData["aduPraPrevId_Lista_Data"] = (from x in context.catHCAduPraPreventiva
                                                   where x.aduPraPrevCodigo != 31 && x.aduPraPrevCodigo != 4
                                                   select new ListaDesplegableWS()
                                                    {
                                                        idLista = (int)x.aduPraPrevId,
                                                        Texto = x.aduPraPrevDescri
                                                    }).ToList();
            var hcAdulto = context.catHCAdulto.FirstOrDefault(f => f.hcaduid == phcaduid);
            var Paciente = context.catPaciente.FirstOrDefault(f => f.pacId == hcAdulto.pacid);
            if (Paciente.tipoIdSexo == 10)
            {
                ViewData["aduPraPrevId_Lista_Data"] = (from x in context.catHCAduPraPreventiva
                                                       where x.aduPraPrevCodigo != 31 &&
                                                             x.aduPraPrevCodigo != 4 &&
                                                             x.aduPraPrevCodigo != 9 &&
                                                             x.aduPraPrevCodigo != 10 &&
                                                             x.aduPraPrevCodigo != 11
                                                       select new ListaDesplegableWS()
                                                       {
                                                           idLista = (int)x.aduPraPrevId,
                                                           Texto = x.aduPraPrevDescri
                                                       }).ToList();
            }

            if (_Datos == null)
            {
                _Datos = new catHCAduPraPreventivaResulWS()
                {
                    hcaduid = phcaduid
                };
            }

            return View(_Datos);
        }

        [GridAction]
        public ActionResult setHistoriaClinicaCuidadosPreventivos(catHCAduPraPreventivaResulWS popUp)
        {
            try
            {
                var Usuario = (Session["Usuario"] as sisUsuario).usrId;

                catHCAduPraPreventivaResul insert = new catHCAduPraPreventivaResul()
                {
                    hcaduid = popUp.hcaduid,
                    usrId = Usuario,
                    aduPraPrevFecha = DateTime.Now,
                    aduPraPrevResultado = popUp.aduPraPrevResultado,
                    aduPraPrevId = (long)popUp.aduPraPrevId
                };

                context.catHCAduPraPreventivaResul.AddObject(insert);
                context.SaveChanges();

                return Json(new
                {
                    Error = false,//!infoProceso.IsValid,
                    Mensaje = "",//infoProceso.Mensaje,
                    Foco = "aduPraPrevResultado"
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Error = true,
                    Mensaje = ex.InnerException == null ? ex.Message : ex.InnerException.Message,
                    Foco = "aduPraPrevResultado"
                });
            }
        }

    }
}