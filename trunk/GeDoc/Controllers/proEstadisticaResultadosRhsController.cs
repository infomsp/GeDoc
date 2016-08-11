namespace GeDoc
{
    using System;
    using System.Collections.Generic;

    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Telerik.Web.Mvc;
    using GeDoc.Filters;
    using GeDoc.Models;
        public partial class proEstadisticaPlanillaController : Controller
        {
            //Edición de datos
            [GridAction]
            public ActionResult _SelectEditingReslutados(int plaId, int espId)
            {
                return View(new GridModel(AllValores(plaId,espId)));
            }

            private IList<proEstadisticaResultadosRHS> AllValores(int plaId,int espId)
            {
                return getDatosValores(plaId, espId).ToList();
            }

            private IEnumerable<proEstadisticaResultadosRHS> getDatosValores(int plaId, int espId)
            {                
                var _Datos = (from d in context.proEstadisticaResultadosRH.ToList()
                              where d.plaId == plaId && d.espId == espId
                              select new proEstadisticaResultadosRHS()
                              {
                                  plaId = d.plaId,
                                  resId = d.resId,
                                  resDia = d.resDia,
                                  resNroHojas = d.resNroHojas,
                                  resHorasAtencion = d.resHorasAtencion,
                                  resCMNuevas = d.resCMNuevas,
                                  resCMRepetidas = d.resCMRepetidas,
                                  resMenos1DM = d.resMenos1DM,
                                  resMenos1DF = d.resMenos1DF,
                                  resMenos1MM = d.resMenos1MM,
                                  resMenos1MF = d.resMenos1MF,
                                  resde1a4M = d.resde1a4M,
                                  resde1a4F = d.resde1a4F,
                                  resde5a14M = d.resde5a14M,
                                  resde5a14F = d.resde5a14F,
                                  resde15a19M = d.resde15a19M,
                                  resde15a19F = d.resde15a19F,
                                  resde20a39M = d.resde20a39M,
                                  resde20a39F = d.resde20a39F,
                                  resde40a69M = d.resde40a69M,
                                  resde40a69F = d.resde40a69F,
                                  resde70ymasM = d.resde70ymasM,
                                  resde70ymasF = d.resde70ymasF,
                                  resTotEdadesM = d.resTotEdadesM,
                                  resTotEdadesF = d.resTotEdadesF,
                                  resTotTotal = d.resTotTotal,
                                  resTotControlEmbarazo = d.resTotControlEmbarazo,
                                  resCantProf = d.resCantProf,
                                  resCantProfCE = d.resCantProfCE
                              }).ToList();
                return _Datos.AsEnumerable();
            }
            
            public ActionResult _InsertEditingResultados()
            {
                proEstadisticaResultadosRH _Item = new proEstadisticaResultadosRH();

                if (TryUpdateModel(_Item))
                {
                    CreateResultados(_Item);
                }

                return View(new GridModel(AllValores(2,1)));  
            }

            private void CreateResultados(proEstadisticaResultadosRH item)
            {
               
            }

            [AcceptVerbs(HttpVerbs.Post)]
            [GridAction]
            public ActionResult generaValores(int plaId,int espId)
            {
                enlEstadisticaPlanillaEspecialidad _Item = context.enlEstadisticaPlanillaEspecialidad.Where(p => p.plaId == plaId & p.espId == espId & p.planillaGenerada == 1).FirstOrDefault();

                if (_Item == null)
                {
                    var _Datos = (from d in context.spEstadisticaGeneraValores(plaId, espId).ToList()
                        select new proEstadisticaResultadosRHS()
                        {
                            plaId = plaId,
                            resId = d.resId,
                            resDia = d.resDia,
                            resNroHojas = d.resNroHojas,
                            resHorasAtencion = d.resHorasAtencion,
                            resCMNuevas = d.resCMNuevas,
                            resCMRepetidas = d.resCMRepetidas,
                            resMenos1DM = d.resMenos1DM,
                            resMenos1DF = d.resMenos1DF,
                            resMenos1MM = d.resMenos1MM,
                            resMenos1MF = d.resMenos1MF,
                            resde1a4M = d.resde1a4M,
                            resde1a4F = d.resde1a4F,
                            resde5a14M = d.resde5a14M,
                            resde5a14F = d.resde5a14F,
                            resde15a19M = d.resde15a19M,
                            resde15a19F = d.resde15a19F,
                            resde20a39M = d.resde20a39M,
                            resde20a39F = d.resde20a39F,
                            resde40a69M = d.resde40a69M,
                            resde40a69F = d.resde40a69F,
                            resde70ymasM = d.resde70ymasM,
                            resde70ymasF = d.resde70ymasF,
                            resTotEdadesM = d.resTotEdadesM,
                            resTotEdadesF = d.resTotEdadesF,
                            resTotTotal = d.resTotTotal,
                            resTotControlEmbarazo = d.resTotControlEmbarazo,
                            resCantProf = d.resCantProf,
                            resCantProfCE = d.resCantProfCE
                        }
                        ).ToList();
                }
                return View(new GridModel(AllValores(plaId,espId))); 
            }

            [AcceptVerbs(HttpVerbs.Post)]
            [GridAction]
            public ActionResult _SelectEditinglistadoProfesionalesxTurno(int espId, int dia, int mes, int anio)
            {
              //  enlEstadisticaPlanillaEspecialidad _Item = context.enlEstadisticaPlanillaEspecialidad.Where(p => p.plaId == plaId & p.espId == espId & p.planillaGenerada == 1).FirstOrDefault();
                  var _csId = (Session["UsuarioCentroDeSalud"] as GeDoc.sisUsuariosCentroDeSalud).csId;
                  

                    return View(new GridModel(AllValoresProfxTur(espId,_csId,dia,mes,anio))); 
            }

            private IList<proEstadisticaProfesionales> AllValoresProfxTur(int espId, int _csId,int dia,int mes,int anio)
            {
                return getDatosValoresProf(espId, _csId, dia, mes, anio).ToList();
            }

            private IEnumerable<proEstadisticaProfesionales> getDatosValoresProf(int espId, int _csId,int dia,int mes,int anio)
            {
                var _Datos = (from d in context.getProfesionalesXTurno(espId, _csId, dia, mes, anio).ToList()
                              where d.conApellidoyNombre == null
                              select new proEstadisticaProfesionales()
                              {
                                  perApellidoyNombre = d.perApellidoyNombre,
                                  ControlEmbarazo = d.turControlEmbarazo == true ? "SI" :  "NO",
                                  espNombre =  d.espNombre
                              }
                       ).ToList();

                _Datos.AddRange((from d in context.getProfesionalesXTurno(espId,_csId,dia,mes,anio).ToList()
                                 where d.perApellidoyNombre == null
                                     select new proEstadisticaProfesionales()
                                     {
                                         perApellidoyNombre = d.conApellidoyNombre,
                                         ControlEmbarazo = d.turControlEmbarazo == true ? "SI" : "NO",
                                         espNombre = d.espNombre
                                     }).ToList());

                return _Datos.AsEnumerable();

            }


            [AcceptVerbs(HttpVerbs.Post)]
            [GridAction]
            public ActionResult validaValores(int plaId, int espId)
            {
                enlEstadisticaPlanillaEspecialidad _Item = context.enlEstadisticaPlanillaEspecialidad.Where(p => p.plaId == plaId & p.espId == espId & p.planillaGenerada == 1).FirstOrDefault();

                if (_Item != null)
                {
                    if (_Item.planillaGenerada == 1)
                    {
                        _Item.planillaValidada = 1;
                        context.SaveChanges();
                    }
                }
                return View(new GridModel(AllValores(plaId, espId)));
            }


            [AcceptVerbs(HttpVerbs.Post)]
            [CultureAwareAction]
            [GridAction]
            public ActionResult _SaveEditingResultados(int id)
            {
                proEstadisticaResultadosRH _Item = context.proEstadisticaResultadosRH.Where(p => p.resId == id).FirstOrDefault();

               // proEstadisticaResultadosRH _Item = new proEstadisticaResultadosRH();
                TryUpdateModel(_Item);

                EditValores(_Item);

                return View(new GridModel(AllValores((short)_Item.plaId, (short)_Item.espId))); 
            }


            private void EditValores(proEstadisticaResultadosRH pItem)
            {
                if (ModelState.IsValid)
                {
                    //pItem = pItem.usrId = (Session["Usuario"] as sisUsuario).usrId;
                    pItem.resTotEdadesM = pItem.resMenos1DM == null ? 0 : pItem.resMenos1DM + pItem.resMenos1MM == null ? 0 : pItem.resMenos1MM + pItem.resde1a4M == null ? 0 : pItem.resde1a4M + pItem.resde15a19M == null ? 0 : pItem.resde15a19M +
                        pItem.resde5a14M == null ? 0 : pItem.resde5a14M + pItem.resde20a39M == null ? 0 : pItem.resde20a39M + pItem.resde40a69M == null ? 0 : pItem.resde40a69M + pItem.resde70ymasM == null ? 0 : pItem.resde70ymasM;

                    pItem.resTotEdadesM =  pItem.resMenos1DM +
                        pItem.resMenos1MM + pItem.resde1a4M  +
                        pItem.resde15a19M  +pItem.resde5a14M  + 
                        pItem.resde20a39M  + pItem.resde40a69M + 
                        pItem.resde70ymasM;


                    pItem.resTotEdadesF = pItem.resMenos1DF +
                        pItem.resMenos1MF + pItem.resde1a4F +
                        pItem.resde15a19F + pItem.resde5a14F +
                        pItem.resde20a39F + pItem.resde40a69F +
                        pItem.resde70ymasF;
        
                    pItem.resTotTotal = pItem.resTotEdadesM + pItem.resTotEdadesF;
                    context.SaveChanges();
                }
                return;
            }
            public ActionResult _DeleteEditingResultados(int id)
            {
                DeleteConfirmedValores(id);

                return View(new GridModel(AllValores(0,1))); 
            }
            private void DeleteConfirmedValores(int id)
            {
                proEstadisticaResultadosRH _Item = context.proEstadisticaResultadosRH.Single(x => x.resId == id);
                context.proEstadisticaResultadosRH.DeleteObject(_Item);
                context.SaveChanges();
            }
        }
   
}