using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using GeDoc.Models;

namespace GeDoc
{
    using System.Linq;
    using System.Web.Mvc;
    using Telerik.Web.Mvc;
    using System.Collections.Generic;
    using System.Web.Script.Serialization;

    public partial class HistoriaClinicaController : Controller
    {
        private efAccesoADatosEntities context = new efAccesoADatosEntities();

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult getMenuHC(Int64? IdPaciente, int tipoDocumento, int idTurno)
        {
            var _MetodosUtiles = new MetodosUtiles();
            var _Datos = new catHistoriaClinicaWS();
            var _pacId = IdPaciente;

            if (_pacId < 0)
            {
                //Se busca por DNI
                var DNI = IdPaciente * -1;
                var InfoPaciente = context.catPaciente.FirstOrDefault(w => w.pacNumeroDocumento == DNI && w.tipoIdTipoDocumento == tipoDocumento);
                if (InfoPaciente == null)
                {
                    _pacId = -1;
                    _Datos.Paciente = new catPacientes();
                    _Datos.Paciente.pacId = -1;
                    if (context.catPaciente.Count(w => w.pacNumeroDocumento == DNI) > 0)
                    {
                        var InfoExistente = context.catPaciente.FirstOrDefault(w => w.pacNumeroDocumento == DNI);
                        _Datos.Paciente.ErrorMessage = "El número de documento ingresado \"" + DNI + "\", tiene el mismo número de documento que \"" + InfoExistente.pacApellido + " " + InfoExistente.pacNombre + "\" pero con distinto tipo de documento.";
                    }
                    _Datos.Paciente.tipoIdTipoDocumento = (short)tipoDocumento;
                    _Datos.Paciente.pacNumeroDocumento = (int)DNI;

                    CargaListasDesplegablesCRUDPaciente(_Datos.Paciente);

                    return PartialView("hcPacienteCRUD", _Datos.Paciente);
                }
                else
                {
                    _pacId = InfoPaciente.pacId;
                }
            }

            _Datos.Paciente = (from d in context.getDatosDePaciente(null, (int)_pacId).ToList()
                               select new catPacientes()
                               {
                                   pacId = d.pacId,
                                   pacApellido = d.pacApellido.ToUpper(),
                                   pacNombre = d.pacNombre.ToUpper(),
                                   pacApellidoyNombre = d.pacApellido.ToUpper() + " " + d.pacNombre.ToUpper(),
                                   pacNumeroDocumento = d.pacNumeroDocumento,
                                   tipoIdTipoDocumento = d.tipoIdTipoDocumento,
                                   tipoDescDocumento = d.tipoDescDocumento,
                                   pacCUIL = d.pacCUIL,
                                   paisId = d.paisId,
                                   paisNombre = d.paisNombre,
                                   provNombre = d.provNombre,
                                   tipoSexoNombre = d.tipoIdSexoTexto,
                                   pacFechaNacimiento = d.pacFechaNacimiento,
                                   pacFechaNacimientoTexto = d.pacFechaNacimiento.Value.ToString("dd/MM/yyyy"),
                                   pacEdad = d.pacFechaNacimiento == null ? (short)0 : (short)_MetodosUtiles.Edad(d.pacFechaNacimiento),
                                   DescEstadoCivil = d.tipoEstadoCivilNombre,
                                   // OcupacionDescripcion = d.tipoIdOcupacion,
                                   pacDonaOrganos = (bool)(d.pacDonaOrganos ?? false),
                                   tipoIdGrupoSanguineo = d.tipoIdGrupoSanguineo,
                                   pacCalle = d.pacCalle,
                                   pacCalleNumero = Convert.ToInt16(d.pacCalleNumero),
                                   pacDomicilioReferencias = d.pacDomicilioReferencias,
                                   depId = d.depId,
                                   depNombre = d.depNombre,
                                   locId = Convert.ToInt16(d.locId),
                                   locNombre = d.locNombre,
                                   barId = Convert.ToInt16(d.barId),
                                   barNombre = d.barNombre,
                                   pacTelefonoCasa = d.pacTelefonoCasa,
                                   pacTelefonoTrabajo = d.pacTelefonoTrabajo,
                                   pacTelefonoCelular = d.pacTelefonoCelular,
                                   pacNotificarXSMS = (bool)(d.pacNotificarXSMS ?? false),
                                   osId = (short)d.osId,
                                   osNombre = d.osDenominacion,
                                   osSigla = d.osSigla,
                                   pacHospitalizado = d.pacHospitalizado == null ? false : (bool)d.pacHospitalizado,
                                   //pacImagenHospitalizado = getHospitalizado(d.pacHospitalizado == null ? false : (bool)d.pacHospitalizado),
                                   pacTransfusionesDeSangre = d.pacTransfusionesDeSangre,
                                   pacTransfusionesDeSangreUltima = d.pacTransfusionesDeSangreUltima == null ? DateTime.Now : (DateTime)d.pacTransfusionesDeSangreUltima,
                                   pacMail = d.pacMail,
                                   tipoIdSexo = d.tipoIdSexo,
                                   tipoIdEstadoCivil = d.tipoIdEstadoCivil,
                                   tipoIdOcupacion = d.tipoIdOcupacion,
                                   etnId = (short)d.etnId,
                                   pacPiso = d.pacPiso,
                                   pacDepto = d.pacDepto,
                                   pacManzana = d.pacManzana,
                                   csId = d.csId == null ? (short)0 : (short)d.csId
                                   //nroHC = getNroHc(d.pacId, _csId),
                                   //csNombre =  _csItem.csNombre,                                      
                               }).First();

            var DatosExtras = context.catPaciente.First(f => f.pacId == (int)_pacId);
            var hcAdulto = context.catHCAdulto.FirstOrDefault(f => f.pacid == (int)_pacId);
            _Datos.Paciente.pacEstudio = DatosExtras.pacEstudio;
            _Datos.Paciente.pacCodigoPostal = DatosExtras.pacCodigoPostal;
            _Datos.Paciente.pacCasa = DatosExtras.pacCasa;
            _Datos.Paciente.pacRiesgoCardiovascular = "";
            if (hcAdulto != null)
            {
                var Riesgo = context.catHCAduPraPreventivaResul.Where(f => f.hcaduid == hcAdulto.hcaduid && f.catHCAduPraPreventiva.aduPraPrevCodigo == 31).OrderByDescending(o => o.aduPraPrevFecha).FirstOrDefault();
                if (Riesgo != null)
                {
                    var NivelRiesgo = int.Parse(Riesgo.aduPraPrevResultado.Replace(".00", ""));
                    _Datos.Paciente.pacRiesgoCardiovascular = NivelRiesgo <= 1 ? "" : (NivelRiesgo == 2 ? "MEDIO" : "ALTO");
                }
            }

            var _Fecha = DateTime.Now;
            var Turnos = context.catTurno.Count(c => c.pacId == _pacId && c.turId != idTurno && c.turFecha <= _Fecha);
            _Datos.Paciente.visitaId = 1;
            if (Turnos > 0)
            {
                _Datos.Paciente.visitaId = 2;
            }

            _Datos.Paciente.turControlEmbarazo = false;
            _Datos.Paciente.IdTurno = idTurno;
            _Datos.Paciente.turTieneDiagnosticos = context.enlTurnoDiagnostico.Count(c => c.turId == idTurno) > 0;
            _Datos.Paciente.turTienePracticas = context.enlTurnoPractica.Count(c => c.turId == idTurno) > 0;

            CargaListasDesplegablesCRUDPaciente(_Datos.Paciente);
            //return PartialView("wHCMenu", _Datos);
            return PartialView("hcPacienteCRUD", _Datos.Paciente);
        }

        private void CargaListasDesplegablesCRUDPaciente(catPacientes InfoPaciente)
        {
            var Tipos = context.sisTipo.ToList();

            ViewData["tipoIdSexo_Lista_Data"] = (from x in Tipos.Where(w => w.sisTipoEntidad.tipoeCodigo == "SX")
                                                          select new ListaDesplegableWS()
                                                          {
                                                              idLista = x.tipoId,
                                                              Texto = x.tipoDescripcion
                                                          }).ToList();
            ViewData["tipoIdEstadoCivil_Lista_Data"] = (from x in Tipos.Where(w => w.sisTipoEntidad.tipoeCodigo == "EC")
                                                          select new ListaDesplegableWS()
                                                          {
                                                              idLista = x.tipoId,
                                                              Texto = x.tipoDescripcion
                                                          }).ToList();

            ViewData["depId_Lista_Data"] = (from x in context.catDepartamento.Where(w => w.provId == 17)
                                            select new ListaDesplegableWS()
                                            {
                                                idLista = x.depId,
                                                Texto = x.depNombre
                                            }).ToList();

            var Depto = InfoPaciente.locId == null ? (ViewData["depId_Lista_Data"] as List<ListaDesplegableWS>).First().idLista : (int)InfoPaciente.depId;

            InfoPaciente.depId = (short)Depto;

            ViewData["locId_Lista_Data"] = (from x in context.catLocalidad.Where(w => w.depId == Depto)
                                            select new ListaDesplegableWS()
                                            {
                                                idLista = x.locId,
                                                Texto = x.locNombre
                                            }).ToList();

            ViewData["barId_Lista_Data"] = (from x in context.catBarrio.Where(w => w.depId == Depto)
                                            select new ListaDesplegableWS()
                                            {
                                                idLista = x.barId,
                                                Texto = x.barNombre
                                            }).ToList();

            ViewData["paisId_Lista_Data"] = (from x in context.catPais
                                            select new ListaDesplegableWS()
                                            {
                                                idLista = x.paisId,
                                                Texto = x.paisNombre
                                            }).ToList();

            ViewData["tipoIdGrupoSanguineo_Lista_Data"] = (from x in Tipos.Where(w => w.sisTipoEntidad.tipoeCodigo == "GS")
                                                 select new ListaDesplegableWS()
                                                 {
                                                     idLista = x.tipoId,
                                                     Texto = x.tipoDescripcion
                                                 }).ToList();

            ViewData["tipoIdOcupacion_Lista_Data"] = (from x in Tipos.Where(w => w.sisTipoEntidad.tipoeCodigo == "OC")
                                                           select new ListaDesplegableWS()
                                                           {
                                                               idLista = x.tipoId,
                                                               Texto = x.tipoDescripcion
                                                           }).ToList();

            ViewData["osId_Lista_Data"] = (from x in context.catObraSocial.ToList()
                                                      select new ListaDesplegableWS()
                                                      {
                                                          idLista = x.osId,
                                                          Texto = x.osCodigo + "-" + x.osDenominacion
                                                      }).ToList().OrderBy(p => p.Texto).ToList();

            ViewData["etnId_Lista_Data"] = (from x in context.catEtnia.ToList()
                                           select new ListaDesplegableWS()
                                           {
                                               idLista = x.etnId,
                                               Texto = x.etnNombreComunidad + " (" + x.etnPueblo + ")"
                                           }).ToList().OrderBy(p => p.Texto).ToList();

            ViewData["pacCalle_Data"] = context.catPaciente.Where(w => w.pacCalle != null).Select(s => s.pacCalle).Distinct();

            var _listaVisita = new List<ListaDesplegableWS>();
            _listaVisita.Add(new ListaDesplegableWS() { idLista = 1, Texto = "Primera vez" });
            _listaVisita.Add(new ListaDesplegableWS() { idLista = 2, Texto = "Ulterior" });
            ViewData["visitaId_Lista_Data"] = _listaVisita;
        }

        public ActionResult hcPaciente()
        {
            ViewData["tipoIdTipoDocumento_Lista_Data"] = (from x in context.sisTipo.Where(w => w.sisTipoEntidad.tipoeCodigo == "TD")
                                                          select new ListaDesplegableWS()
                                                          {
                                                              idLista = x.tipoId,
                                                              Texto = x.tipoDescripcion
                                                          }).ToList();

            return PartialView("hcPaciente");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult getGrillaHC(Int64? pacId)
        {
            return PartialView("wHCListaPerinatales");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ListaDeLocalidades(int depId)
        {
            var Datos = context.catLocalidad.Where(w => w.depId == depId).ToList();

            string listaDesplegable = "<select id=\"locId_Lista\" class=\"locId_Lista\"" + (Datos.Count == 0 ? " disabled " : "") + " >";
            foreach (var Item in Datos)
            {
                listaDesplegable += "<option value=\"" + Item.locId + "\">" + Item.locNombre + "</option>";
            }
            if (!Datos.Any())
            {
                listaDesplegable += "<option value=\"-1\" class=\"ES-PlaceHolder\">Ningún Item seleccionado</option>";
            }
            listaDesplegable += "</select>";

            return Json(listaDesplegable);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ListaDeBarrios(int depId)
        {
            var Datos = context.catBarrio.Where(w => w.depId == depId).ToList();

            string listaDesplegable = "<select id=\"barId_Lista\" class=\"barId_Lista\"" + (Datos.Count == 0 ? " disabled " : "") + " >";
            foreach (var Item in Datos)
            {
                listaDesplegable += "<option value=\"" + Item.barId + "\">" + Item.barNombre + "</option>";
            }
            if (!Datos.Any())
            {
                listaDesplegable += "<option value=\"-1\" class=\"ES-PlaceHolder\">Ningún Item seleccionado</option>";
            }
            listaDesplegable += "</select>";

            return Json(listaDesplegable);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult setImagenFamiligrama(string Imagen, int hcAdultoId)
        {
            string Resultado = "";
            try
            {
                byte[] data = Convert.FromBase64String(Imagen);
                MemoryStream ms = new MemoryStream(data);
                Image returnImage = Image.FromStream(ms);
                var FechaActual = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                string Archivo = Path.Combine(Server.MapPath("~/Content/Fotos/Familigramas"), "Familigrama-" + FechaActual + ".png");
                returnImage.Save(Archivo, ImageFormat.Png);
                Resultado = "Familigrama-" + FechaActual + ".png";
                #region Adulto - Familigrama
                var updateAdulto = context.catHCAdulto.FirstOrDefault(item => item.hcaduid == hcAdultoId);
                if (updateAdulto != null)
                {
                    updateAdulto.hcaduFamiligrama = "Familigrama-" + FechaActual + ".png";
                    context.SaveChanges();
                }

                #endregion Adulto - Familigrama
            }
            catch
            {
                Resultado = "Error";
            }
            return Json(Resultado);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult setPaciente(catPacientes Datos)
        {
            try
            {
                var _csId = (Session["UsuarioCentroDeSalud"] as sisUsuariosCentroDeSalud).csId;
                if (Datos.pacId <= 0)
                {
                    //Se Inserta un paciente nuevo\\
                    var NewPaciente = new catPaciente();
                    NewPaciente.pacApellido = Datos.pacApellido.ToUpper();
                    NewPaciente.pacNombre = Datos.pacNombre.ToUpper();
                    NewPaciente.tipoIdTipoDocumento = Datos.tipoIdTipoDocumento;
                    NewPaciente.pacNumeroDocumento = (int)Datos.pacNumeroDocumento;
                    NewPaciente.pacCUIL = Datos.pacCUIL.ToUpper();
                    NewPaciente.tipoIdSexo = Datos.tipoIdSexo;
                    NewPaciente.pacFechaNacimiento = Datos.pacFechaNacimiento;
                    NewPaciente.tipoIdEstadoCivil = Datos.tipoIdEstadoCivil;
                    NewPaciente.tipoIdOcupacion = Datos.tipoIdOcupacion;
                    NewPaciente.paisId = Datos.paisId;
                    NewPaciente.osId = (short)Datos.osId;
                    NewPaciente.pacPiso = Datos.pacPiso == null ? "" : Datos.pacPiso.ToUpper();
                    //NewPaciente.pacManzana = Datos.pacManzana.ToUpper();
                    NewPaciente.pacDepto = Datos.pacDepto == null ? "" : Datos.pacDepto.ToUpper();
                    NewPaciente.pacCalle = Datos.pacCalle.ToUpper();
                    NewPaciente.pacCalleNumero = Datos.pacCalleNumero;
                    NewPaciente.pacDomicilioReferencias = Datos.pacDomicilioReferencias == null ? "" : Datos.pacDomicilioReferencias.ToUpper();
                    NewPaciente.depId = Datos.depId;
                    NewPaciente.locId = Datos.locId;
                    NewPaciente.barId = Datos.barId;
                    NewPaciente.pacDonaOrganos = Datos.pacDonaOrganos;
                    NewPaciente.tipoIdGrupoSanguineo = Datos.tipoIdGrupoSanguineo;
                    NewPaciente.pacTelefonoCasa = Datos.pacTelefonoCasa ?? "";
                    //NewPaciente.pacTelefonoTrabajo = Datos.pacTelefonoTrabajo.ToUpper();
                    NewPaciente.pacTelefonoCelular = Datos.pacTelefonoCelular ?? "";
                    //NewPaciente.pacNotificarXSMS = Datos.pacNotificarXSMS;
                    //NewPaciente.pacHospitalizado = Datos.pacHospitalizado;
                    //NewPaciente.pacTransfusionesDeSangre = Datos.pacTransfusionesDeSangre;
                    //NewPaciente.pacTransfusionesDeSangreUltima = Datos.pacTransfusionesDeSangreUltima;
                    NewPaciente.provId = Datos.provId;
                    NewPaciente.pacMail = Datos.pacMail;
                    NewPaciente.etnId = Datos.etnId;
                    NewPaciente.csaId = _csId;
                    NewPaciente.csId = _csId;
                    NewPaciente.pacPadron = Datos.pacPadron;
                    NewPaciente.nroHC = Datos.pacNumeroDocumento.ToString();
                    NewPaciente.pacCodigoPostal = (short)Datos.pacCodigoPostal;
                    //NewPaciente.pacCalleOrientacion = Datos.pacCalleOrientacion.ToUpper();
                    NewPaciente.pacEstudio = Datos.pacEstudio;
                    NewPaciente.pacCasa = Datos.pacCasa;
                    NewPaciente.pacManzana = Datos.pacManzana;

                    context.catPaciente.AddObject(NewPaciente);
                }
                else
                {
                    //Paciente existente\\
                    var UpdatePaciente = context.catPaciente.First(w => w.pacId == Datos.pacId);
                    UpdatePaciente.pacApellido = Datos.pacApellido.ToUpper();
                    UpdatePaciente.pacNombre = Datos.pacNombre.ToUpper();
                    //UpdatePaciente.tipoIdTipoDocumento = Datos.tipoIdTipoDocumento;
                    //UpdatePaciente.pacNumeroDocumento = Datos.pacNumeroDocumento;
                    UpdatePaciente.pacCUIL = Datos.pacCUIL.ToUpper();
                    UpdatePaciente.tipoIdSexo = Datos.tipoIdSexo;
                    UpdatePaciente.pacFechaNacimiento = Datos.pacFechaNacimiento;
                    UpdatePaciente.tipoIdEstadoCivil = Datos.tipoIdEstadoCivil;
                    UpdatePaciente.tipoIdOcupacion = Datos.tipoIdOcupacion;
                    UpdatePaciente.paisId = Datos.paisId;
                    UpdatePaciente.osId = (short)Datos.osId;
                    UpdatePaciente.pacPiso = Datos.pacPiso == null ? "" : Datos.pacPiso.ToUpper();
                    //UpdatePaciente.pacManzana = Datos.pacManzana.ToUpper();
                    UpdatePaciente.pacDepto = Datos.pacDepto == null ? "" : Datos.pacDepto.ToUpper();
                    UpdatePaciente.pacCalle = Datos.pacCalle.ToUpper();
                    UpdatePaciente.pacCalleNumero = Datos.pacCalleNumero;
                    UpdatePaciente.pacDomicilioReferencias = Datos.pacDomicilioReferencias == null ? "" : Datos.pacDomicilioReferencias.ToUpper();
                    UpdatePaciente.depId = Datos.depId;
                    UpdatePaciente.locId = Datos.locId;
                    UpdatePaciente.barId = Datos.barId;
                    UpdatePaciente.pacDonaOrganos = Datos.pacDonaOrganos;
                    UpdatePaciente.tipoIdGrupoSanguineo = Datos.tipoIdGrupoSanguineo;
                    UpdatePaciente.pacTelefonoCasa = Datos.pacTelefonoCasa ?? "";
                    //UpdatePaciente.pacTelefonoTrabajo = Datos.pacTelefonoTrabajo.ToUpper();
                    UpdatePaciente.pacTelefonoCelular = Datos.pacTelefonoCelular ?? "";
                    //UpdatePaciente.pacNotificarXSMS = Datos.pacNotificarXSMS;
                    //UpdatePaciente.pacHospitalizado = Datos.pacHospitalizado;
                    //UpdatePaciente.pacTransfusionesDeSangre = Datos.pacTransfusionesDeSangre;
                    //UpdatePaciente.pacTransfusionesDeSangreUltima = Datos.pacTransfusionesDeSangreUltima;
                    UpdatePaciente.provId = Datos.provId;
                    UpdatePaciente.pacMail = Datos.pacMail;
                    UpdatePaciente.etnId = Datos.etnId;
                    UpdatePaciente.csaId = _csId;
                    UpdatePaciente.csId = _csId;
                    UpdatePaciente.pacPadron = Datos.pacPadron;
                    //UpdatePaciente.nroHC = Datos.nroHC.ToUpper();
                    UpdatePaciente.pacCodigoPostal = Datos.pacCodigoPostal == null ? (short)0 : (short)Datos.pacCodigoPostal;
                    //UpdatePaciente.pacCalleOrientacion = Datos.pacCalleOrientacion.ToUpper();
                    UpdatePaciente.pacEstudio = Datos.pacEstudio;
                    UpdatePaciente.pacCasa = Datos.pacCasa;
                    UpdatePaciente.pacManzana = Datos.pacManzana;

                    if (Datos.IdTurno > 0)
                    {
                        var DatosTurno = context.catTurno.Single(w => w.turId == Datos.IdTurno);
                        DatosTurno.turPrimUlt = Datos.visitaId == 1 ? "P" : "U";
                        DatosTurno.turControlEmbarazo = Datos.turControlEmbarazo;
                        
                    }
                }
                context.SaveChanges();

                //Revisamos si está la HC de adulto y si no la generamos\\
                var InfoPaciente = context.catPaciente.FirstOrDefault(w => w.pacNumeroDocumento == Datos.pacNumeroDocumento && w.tipoIdTipoDocumento == Datos.tipoIdTipoDocumento);
                if (context.catHCAdulto.Count(c => c.pacid == InfoPaciente.pacId) == 0)
                {
                    context.catHCAdulto.AddObject(new catHCAdulto() { pacid = InfoPaciente.pacId, Fecha = DateTime.Now });
                    context.SaveChanges();
                }
                return Json(new
                {
                    Error = false,
                    Mensaje = "OK",
                    Foco = "pacApellido",
                    IdPaciente = InfoPaciente.pacId
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Error = true,
                    Mensaje = ex.InnerException == null ? ex.Message : ex.InnerException.Message,
                    Foco = "pacApellido",
                    IdPaciente = -1
                });
            }
        }

        [GridAction]
        public ActionResult getHCListaPerinatales(Int64? pacId)
        {
            var _Datos = (from x in context.getListadoHCPerinatales(pacId).ToList()
                          select new catHCPerinatalWS()
                          {
                              hcperid = x.hcperid,
                              pacid = x.pacid,
                              activa = x.activa,
                              Fecha = x.Fecha
                          });

            return PartialView(new GridModel(_Datos));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult getGrillaHCAdultos(Int64? pacId)
        {
            return PartialView("wHCListaAdultos");
        }

        [GridAction]
        public ActionResult getHCListaAdultos(Int64? pacId)
        {
            var _Datos = (from x in context.getListadoHCAdultos(pacId).ToList()
                          select new catHCAdultoWS()
                          {
                              hcaduid = x.hcaduid,
                              pacid = x.pacid,
                              //activa = x.activa,
                              Fecha = x.Fecha
                          });

            return PartialView(new GridModel(_Datos));
        }

        [GridAction]
        public ActionResult getHistoriaClinicaAdulto(string pAccion, Int64? pacId)
        {
            var datosHC = new catHCAdultoWS();
            var _MetodosUtiles = new MetodosUtiles();

            if (pAccion != "Consultar")
            {
                //var InfoHC = context.catHCAdulto.Single(s => s.hcaduid == hcaduid);
                //var InfoPaciente = InfoHC.catHCPerPaciente.First();
                datosHC = new catHCAdultoWS()
                {
                };
            }
            else
            {
                var InfoPac = context.catPaciente.Single(s => s.pacId == pacId);
                var InfoHCAdu = context.catHCAdulto.FirstOrDefault(s => s.pacid == pacId);

                datosHC.hcaduAccion = pAccion;
                datosHC.pacid = (int)pacId;
                datosHC.Fecha = DateTime.Now;
                if (InfoHCAdu != null)
                {
                    datosHC.Fecha = InfoHCAdu.Fecha;
                    datosHC.Familigrama = InfoHCAdu.hcaduFamiligrama;
                    datosHC.FechaFamiligrama = InfoHCAdu.hcaduFechaFamiligrama;
                    datosHC.hcaduid = InfoHCAdu.hcaduid;
                }
                //datosHC.activa = true;
                //datosHC.hcperAccion = pAccion;
                datosHC.InformacionPaciente = new catHCPerPacienteWS()
                {
                    perDomicilio = InfoPac.pacCalle,
                    perLocalidad = InfoPac.catDepartamento.depNombre,
                    perTelefono = InfoPac.pacTelefonoCasa,
                    perInfoPaciente =
                        new catPacientes()
                        {
                            pacApellido = InfoPac.pacApellido,
                            pacNombre = InfoPac.pacNombre,
                            pacFechaNacimiento = InfoPac.pacFechaNacimiento,
                            pacNumeroDocumento = InfoPac.pacNumeroDocumento,
                            tipoSexoNombre = InfoPac.sisTipo.tipoCodigo, 
                            pacCUIL = InfoPac.pacCUIL,
                            DescEstadoCivil = InfoPac.sisTipo2.tipoCodigo,
                            pacDonaOrganos = (bool)InfoPac.pacDonaOrganos,
                            paisNombre = InfoPac.catPais.paisNombre,
                            OcupacionDescripcion = InfoPac.sisTipo4.tipoDescripcion,
                            grupoSanguineo = InfoPac.sisTipo3.tipoDescripcion,
                            pacCalle = InfoPac.pacCalle,
                            pacCalleNumero = InfoPac.pacCalleNumero,
                            pacDomicilioReferencias = InfoPac.pacDomicilioReferencias,
                            //locNombre = InfoPac.cat
                            depNombre = InfoPac.catDepartamento.depNombre,
                            pacTelefonoCasa = InfoPac.pacTelefonoCasa == null ? "" : InfoPac.pacTelefonoCasa,
                            pacTelefonoCelular = InfoPac.pacTelefonoCelular == null ? "" : InfoPac.pacTelefonoCelular,
                            pacTelefonoTrabajo = InfoPac.pacTelefonoTrabajo == null ? "" : InfoPac.pacTelefonoTrabajo,
                            provNombre = InfoPac.catProvincia.provNombre,
                            tipoDescDocumento = InfoPac.sisTipo1.tipoCodigo,
                            osNombre = InfoPac.catObraSocial.osDenominacion,
                            tieneOS = (InfoPac.catObraSocial.osCodigo == 0 ? false : true)
                        },
                    perEdad = (short)_MetodosUtiles.Edad(InfoPac.pacFechaNacimiento)
                };

            }

            return PartialView("wHCCRUDAdultos", datosHC);
        }

        public ActionResult hcPacienteFamiligrama(Int64? pacId)
        {
            var datosHC = new catHCAdultoWS();

            var InfoHCAdu = context.catHCAdulto.FirstOrDefault(s => s.pacid == pacId);

            datosHC.hcaduAccion = "Modificar";
            datosHC.pacid = (int)pacId;
            datosHC.Fecha = DateTime.Now;
            if (InfoHCAdu != null)
            {
                datosHC.Fecha = InfoHCAdu.Fecha;
                datosHC.Familigrama = InfoHCAdu.hcaduFamiligrama;
                datosHC.FechaFamiligrama = InfoHCAdu.hcaduFechaFamiligrama;
                datosHC.hcaduid = InfoHCAdu.hcaduid;
            }

            return PartialView("hcPacienteFamiligrama", datosHC);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult setHistoriaClinicaFamiliarYPersonal(catHCAduHistoriaFamiliarTabWS tab2)
        {
            try
            {
                var Usuario = (Session["Usuario"] as sisUsuario).usrNombreDeUsuario;

                #region Antecedentes Familiares

                var updateAnteFamiliar = context.catHCAduAnteFamiliar.FirstOrDefault(item => item.hcaduid == tab2.antecedentesFamiliares.hcaduid);
                if (updateAnteFamiliar == null)
                {
                    catHCAduAnteFamiliar historialPersonalYFamiliar = new catHCAduAnteFamiliar()
                    {
                        hcaduid = tab2.antecedentesFamiliares.hcaduid,
                        aduAFHipertension = tab2.antecedentesFamiliares.aduAFHipertension,
                        aduAFCardiacas = tab2.antecedentesFamiliares.aduAFCardiacas,
                        aduAFDiabetes = tab2.antecedentesFamiliares.aduAFDiabetes,
                        aduAFACV = tab2.antecedentesFamiliares.aduAFACV,
                        aduAFCancerColon = tab2.antecedentesFamiliares.aduAFCancerColon,
                        aduAFCancerMama = tab2.antecedentesFamiliares.aduAFCancerMama,
                        aduAFCancerOtro = tab2.antecedentesFamiliares.aduAFCancerOtro,
                        aduAFCancerOtroCual = tab2.antecedentesFamiliares.aduAFCancerOtroCual,
                        aduAFCeliaca = tab2.antecedentesFamiliares.aduAFCeliaca,
                        aduAFDroga = tab2.antecedentesFamiliares.aduAFDroga,
                        aduAFDepresion = tab2.antecedentesFamiliares.aduAFDepresion
                    };

                    context.catHCAduAnteFamiliar.AddObject(historialPersonalYFamiliar);
                }
                else
                {
                    updateAnteFamiliar.hcaduid = tab2.antecedentesFamiliares.hcaduid;
                    updateAnteFamiliar.aduAFHipertension = tab2.antecedentesFamiliares.aduAFHipertension;
                    updateAnteFamiliar.aduAFCardiacas = tab2.antecedentesFamiliares.aduAFCardiacas;
                    updateAnteFamiliar.aduAFDiabetes = tab2.antecedentesFamiliares.aduAFDiabetes;
                    updateAnteFamiliar.aduAFACV = tab2.antecedentesFamiliares.aduAFACV;
                    updateAnteFamiliar.aduAFCancerColon = tab2.antecedentesFamiliares.aduAFCancerColon;
                    updateAnteFamiliar.aduAFCancerMama = tab2.antecedentesFamiliares.aduAFCancerMama;
                    updateAnteFamiliar.aduAFCancerOtro = tab2.antecedentesFamiliares.aduAFCancerOtro;
                    updateAnteFamiliar.aduAFCancerOtroCual = tab2.antecedentesFamiliares.aduAFCancerOtroCual;
                    updateAnteFamiliar.aduAFCeliaca = tab2.antecedentesFamiliares.aduAFCeliaca;
                    updateAnteFamiliar.aduAFDroga = tab2.antecedentesFamiliares.aduAFDroga;
                    updateAnteFamiliar.aduAFDepresion = tab2.antecedentesFamiliares.aduAFDepresion;
                }

                #endregion

                #region Antecedentes Médicos 

                var updateAntecedentesMedicos = context.catHCAduAnteMedico.FirstOrDefault(item => item.hcaduid == tab2.antecedentesFamiliares.hcaduid);

                if (updateAntecedentesMedicos == null)
                {
                    catHCAduAnteMedico antecedentesMedicos = new catHCAduAnteMedico()
                    {
                        hcaduid = tab2.antecedentesFamiliares.hcaduid,
                        aduAMAlergia = tab2.antecedentesMedicos.aduAMAlergia,
                        aduAMAlergiaCual = tab2.antecedentesMedicos.aduAMAlergiaCual,
                        aduAMInterna = tab2.antecedentesMedicos.aduAMInterna,
                        aduAMMotivo1 = tab2.antecedentesMedicos.aduAMMotivo1,
                        aduAMMotivo2 = tab2.antecedentesMedicos.aduAMMotivo2,
                        aduAMMotivo3 = tab2.antecedentesMedicos.aduAMMotivo3,
                        aduAMAño1 = (short)tab2.antecedentesMedicos.aduAMAño1,
                        aduAMAño2 = (short)tab2.antecedentesMedicos.aduAMAño2,
                        aduAMAño3 = (short)tab2.antecedentesMedicos.aduAMAño3,
                        aduAMTranfusiones = tab2.antecedentesMedicos.aduAMTranfusiones,
                        aduAMInfecciones = tab2.antecedentesMedicos.aduAMInfecciones,
                        aduAMInfeccionesCual = tab2.antecedentesMedicos.aduAMInfeccionesCual
                    };
                    if (tab2.antecedentesMedicos.aduAMTranfusiones)
                    {
                        antecedentesMedicos.aduAMTranfusionesCuando = tab2.antecedentesMedicos.aduAMTranfusionesCuando;
                    }
                    else
                    {
                        antecedentesMedicos.aduAMTranfusionesCuando = null;
                    }

                    context.catHCAduAnteMedico.AddObject(antecedentesMedicos);
                }
                else 
                {
                    updateAntecedentesMedicos.hcaduid = tab2.antecedentesFamiliares.hcaduid;
                    updateAntecedentesMedicos.aduAMAlergia = tab2.antecedentesMedicos.aduAMAlergia;
                    updateAntecedentesMedicos.aduAMAlergiaCual = tab2.antecedentesMedicos.aduAMAlergiaCual;
                    updateAntecedentesMedicos.aduAMInterna = tab2.antecedentesMedicos.aduAMInterna;
                    updateAntecedentesMedicos.aduAMMotivo1 = tab2.antecedentesMedicos.aduAMMotivo1;
                    updateAntecedentesMedicos.aduAMMotivo2 = tab2.antecedentesMedicos.aduAMMotivo2;
                    updateAntecedentesMedicos.aduAMMotivo3 = tab2.antecedentesMedicos.aduAMMotivo3;
                    updateAntecedentesMedicos.aduAMAño1 = (short)tab2.antecedentesMedicos.aduAMAño1;
                    updateAntecedentesMedicos.aduAMAño2 = (short)tab2.antecedentesMedicos.aduAMAño2;
                    updateAntecedentesMedicos.aduAMAño3 = (short)tab2.antecedentesMedicos.aduAMAño3;
                    updateAntecedentesMedicos.aduAMTranfusiones = tab2.antecedentesMedicos.aduAMTranfusiones;
                    //updateAntecedentesMedicos.aduAMTranfusionesCuando = tab2.antecedentesMedicos.aduAMTranfusionesCuando;
                    updateAntecedentesMedicos.aduAMInfecciones = tab2.antecedentesMedicos.aduAMInfecciones;
                    updateAntecedentesMedicos.aduAMInfeccionesCual = tab2.antecedentesMedicos.aduAMInfeccionesCual;
                    if (tab2.antecedentesMedicos.aduAMTranfusiones)
                    {
                        updateAntecedentesMedicos.aduAMTranfusionesCuando = tab2.antecedentesMedicos.aduAMTranfusionesCuando;
                    }
                    else
                    {
                        updateAntecedentesMedicos.aduAMTranfusionesCuando = null;
                    }

                }

                #endregion 

                #region Antecedentes Ginecologicos 

                var updateAntecedentesGinecologicos = context.catHCAduAnteGineco.FirstOrDefault(item => item.hcaduid == tab2.antecedentesFamiliares.hcaduid);

                if (updateAntecedentesGinecologicos == null)
                {
                   catHCAduAnteGineco antecedentesGineco = new catHCAduAnteGineco()
                   {
                       hcaduid = tab2.antecedentesFamiliares.hcaduid,
                       aduAGMenarca = (short)tab2.antecedentesGinecologicos.aduAGMenarca,
                       aduAGIRS = (short)tab2.antecedentesGinecologicos.aduAGIRS,
                       aduAGGestas = (short)tab2.antecedentesGinecologicos.aduAGGestas,
                       aduAGPartos = (short)tab2.antecedentesGinecologicos.aduAGPartos,
                       aduAGCesareas = (short)tab2.antecedentesGinecologicos.aduAGCesareas,
                       aduAGAbortos = (short)tab2.antecedentesGinecologicos.aduAGAbortos,
                       aduAGCiclos = tab2.antecedentesGinecologicos.aduAGCiclos,
                       aduAGMenopausia = (short)tab2.antecedentesGinecologicos.aduAGMenopausia,
                       aduAGQuirurgica = tab2.antecedentesGinecologicos.aduAGQuirurgica,
                       aduAGDIU = tab2.antecedentesGinecologicos.aduAGDIU,
                       aduAGImplante = tab2.antecedentesGinecologicos.aduAGImplante,
                       aduAGMadreSola = tab2.antecedentesGinecologicos.aduAGMadreSola
                   };

                   context.catHCAduAnteGineco.AddObject(antecedentesGineco);
                }
                else
                {
                    updateAntecedentesGinecologicos.hcaduid = tab2.antecedentesFamiliares.hcaduid;
                    updateAntecedentesGinecologicos.aduAGMenarca = (short)tab2.antecedentesGinecologicos.aduAGMenarca;
                    updateAntecedentesGinecologicos.aduAGIRS = (short)tab2.antecedentesGinecologicos.aduAGIRS;
                    updateAntecedentesGinecologicos.aduAGGestas = (short)tab2.antecedentesGinecologicos.aduAGGestas;
                    updateAntecedentesGinecologicos.aduAGPartos = (short)tab2.antecedentesGinecologicos.aduAGPartos;
                    updateAntecedentesGinecologicos.aduAGCesareas = (short)tab2.antecedentesGinecologicos.aduAGCesareas;
                    updateAntecedentesGinecologicos.aduAGAbortos = (short)tab2.antecedentesGinecologicos.aduAGAbortos;
                    updateAntecedentesGinecologicos.aduAGCiclos = tab2.antecedentesGinecologicos.aduAGCiclos;
                    updateAntecedentesGinecologicos.aduAGMenopausia = (short)tab2.antecedentesGinecologicos.aduAGMenopausia;
                    updateAntecedentesGinecologicos.aduAGQuirurgica = tab2.antecedentesGinecologicos.aduAGQuirurgica;
                    updateAntecedentesGinecologicos.aduAGDIU = tab2.antecedentesGinecologicos.aduAGDIU;
                    updateAntecedentesGinecologicos.aduAGImplante = tab2.antecedentesGinecologicos.aduAGImplante;
                    updateAntecedentesGinecologicos.aduAGMadreSola = tab2.antecedentesGinecologicos.aduAGMadreSola;
                }

                #endregion 

                #region Hábitos

                var updateHabitos = context.catHCAduHabitos.FirstOrDefault(item => item.hcaduid == tab2.antecedentesFamiliares.hcaduid);

                if (updateHabitos == null)
                {
                    catHCAduHabitos habitos = new catHCAduHabitos()
                    {
                        hcaduid = tab2.antecedentesFamiliares.hcaduid,
                        aduHAFuma = (tab2.habitos.tabaco == "FU" ? true : false),
                        aduHAFumadorPasivo = (tab2.habitos.tabaco == "PS" ? true : false),
                        aduHANuncaFuma = (tab2.habitos.tabaco == "NU" ? true : false),
                        aduHADejo = (tab2.habitos.tabaco == "DJ" ? true : false),
                        aduHABebe = tab2.habitos.alcohol == "BE" ? true : false,
                        aduHANoBebe = tab2.habitos.alcohol == "NB" ? true : false,
                        aduHABebeComidas = tab2.habitos.alcohol == "CM" ? true : false,
                        aduHAVasosPorDia = (short)(tab2.habitos.alcohol == "CM" || tab2.habitos.alcohol == "BE" ? tab2.habitos.aduHAVasosPorDia : 0),
                        aduHASedentarismo = tab2.habitos.aduHASedentarismo,
                        aduHAFisico = tab2.habitos.aduHAFisico,
                        aduHAFisicoTipo = tab2.habitos.aduHAFisicoTipo,
                        aduHAFisicoFrecuancia = tab2.habitos.aduHAFisicoFrecuancia,
                        aduHASalero = tab2.habitos.aduHASalero,
                        aduHADesayuno = tab2.habitos.aduHADesayuno,
                        aduHAComida = tab2.habitos.aduHAComida,
                        aduHACascoAVeces = tab2.habitos.cinturon == "AV" ? true : false,
                        aduHACascoSiempre = tab2.habitos.cinturon == "SI" ? true : false,
                        aduHACascoNunca = tab2.habitos.cinturon == "NO" ? true : false,
                        aduHADrogaSi = tab2.habitos.drogas == "SI" ? true : false,
                        aduHADrogaNo = tab2.habitos.drogas == "NO" ? true : false,
                        aduHADrogaCual = tab2.habitos.aduHADrogaCual
                    };

                    if (tab2.habitos.tabaco == null)
                    {
                        habitos.aduHADejoCuando = null;
                        habitos.aduHAFumaCuando = null;
                    }
                    else
                    {
                        if (tab2.habitos.tabaco == "DJ")
                        {
                            habitos.aduHADejoCuando = tab2.habitos.aduHADejoCuando;
                        }
                        else
                        {
                            habitos.aduHADejoCuando = null;
                        }
                        if (tab2.habitos.tabaco == "FU")
                        {
                            habitos.aduHAFumaCuando = tab2.habitos.aduHAFumaCuando;
                        }
                        else
                        {
                            habitos.aduHAFumaCuando = null;
                        }
                    }

                    context.catHCAduHabitos.AddObject(habitos);
                }
                else
                {
                    updateHabitos.aduHAFuma = (tab2.habitos.tabaco == "FU" ? true : false);
                    updateHabitos.aduHAFumadorPasivo = (tab2.habitos.tabaco == "PS" ? true : false);
                    //updateHabitos.aduHAFumaCuando = tab2.habitos.aduHAFumaCuando;
                    updateHabitos.aduHANuncaFuma = (tab2.habitos.tabaco == "NU" ? true : false);
                    updateHabitos.aduHADejo = (tab2.habitos.tabaco == "DJ" ? true : false);
                    //updateHabitos.aduHADejoCuando = tab2.habitos.aduHADejoCuando;
                    updateHabitos.aduHABebe = tab2.habitos.alcohol == "BE" ? true : false;
                    updateHabitos.aduHANoBebe = tab2.habitos.alcohol == "NB" ? true : false;
                    updateHabitos.aduHABebeComidas = tab2.habitos.alcohol == "CM" ? true : false;
                    updateHabitos.aduHAVasosPorDia = (short)(tab2.habitos.alcohol == "CM" || tab2.habitos.alcohol == "BE" ? tab2.habitos.aduHAVasosPorDia : 0);
                    updateHabitos.aduHASedentarismo = tab2.habitos.aduHASedentarismo;
                    updateHabitos.aduHAFisico = tab2.habitos.aduHAFisico;
                    updateHabitos.aduHAFisicoTipo = tab2.habitos.aduHAFisicoTipo;
                    updateHabitos.aduHAFisicoFrecuancia = tab2.habitos.aduHAFisicoFrecuancia;
                    updateHabitos.aduHASalero = tab2.habitos.aduHASalero;
                    updateHabitos.aduHADesayuno = tab2.habitos.aduHADesayuno;
                    updateHabitos.aduHAComida = tab2.habitos.aduHAComida;
                    updateHabitos.aduHACascoAVeces = tab2.habitos.cinturon == "AV" ? true : false;
                    updateHabitos.aduHACascoSiempre = tab2.habitos.cinturon == "SI" ? true : false;
                    updateHabitos.aduHACascoNunca = tab2.habitos.cinturon == "NO" ? true : false;
                    updateHabitos.aduHADrogaSi = tab2.habitos.drogas == "SI" ? true : false;
                    updateHabitos.aduHADrogaNo = tab2.habitos.drogas == "NO" ? true : false;
                    updateHabitos.aduHADrogaCual = tab2.habitos.aduHADrogaCual;

                    if (tab2.habitos.tabaco == "DJ")
                    { updateHabitos.aduHADejoCuando = tab2.habitos.aduHADejoCuando; }
                    else
                    { updateHabitos.aduHADejoCuando = null; }
                    if (tab2.habitos.tabaco == "FU")
                    { updateHabitos.aduHAFumaCuando = tab2.habitos.aduHAFumaCuando; }
                    else
                    { updateHabitos.aduHAFumaCuando = null; }
                }

                #endregion 

                #region Situacion Psicosocial

                var updatePsicosocial = context.catHCAduSituacionPsico.FirstOrDefault(item => item.hcaduid == tab2.antecedentesFamiliares.hcaduid);

                if (updatePsicosocial == null)
                {
                    catHCAduSituacionPsico psicosocial = new catHCAduSituacionPsico()
                    {
                        aduSPviolenciaSi = tab2.psicosocial.violencia == "SI" ? true : false,
                        aduSPviolenciaNo = tab2.psicosocial.violencia == "NO" ? true : false,
                        aduSPDuelo = tab2.psicosocial.aduSPDuelo,
                        aduSPTrabajo = tab2.psicosocial.aduSPTrabajo,
                        aduSPSeparacion = tab2.psicosocial.aduSPSeparacion,
                        aduSPTraslado = tab2.psicosocial.aduSPTraslado,
                        aduSPNacimiento = tab2.psicosocial.aduSPNacimiento,
                        aduSPEmpleoNo = tab2.psicosocial.empleo == false ? true : false,
                        aduSPEmpleoSi = tab2.psicosocial.empleo == true ? true : false,
                        aduSPEmpleoTipo = tab2.psicosocial.aduSPEmpleoTipo,
                        aduSPAmigos = tab2.psicosocial.aduSPAmigos,
                        aduSPVecinos = tab2.psicosocial.aduSPVecinos,
                        aduSPFamilia = tab2.psicosocial.aduSPFamilia,
                        aduSPInstituto = tab2.psicosocial.aduSPInstituto,
                        aduSPOtros = tab2.psicosocial.aduSPOtros,
                        aduSPCuales = tab2.psicosocial.aduSPCuales,
                        aduSPNadie = tab2.psicosocial.aduSPNadie,
                        hcaduid = tab2.antecedentesFamiliares.hcaduid
                    };

                    context.catHCAduSituacionPsico.AddObject(psicosocial);
                }
                else
                {
                    updatePsicosocial.aduSPviolenciaSi = tab2.psicosocial.violencia == "SI" ? true : false;
                    updatePsicosocial.aduSPviolenciaNo = tab2.psicosocial.violencia == "NO" ? true : false;
                    updatePsicosocial.aduSPDuelo = tab2.psicosocial.aduSPDuelo;
                    updatePsicosocial.aduSPTrabajo = tab2.psicosocial.aduSPTrabajo;
                    updatePsicosocial.aduSPSeparacion = tab2.psicosocial.aduSPSeparacion;
                    updatePsicosocial.aduSPTraslado = tab2.psicosocial.aduSPTraslado;
                    updatePsicosocial.aduSPNacimiento = tab2.psicosocial.aduSPNacimiento;
                    updatePsicosocial.aduSPEmpleoNo = tab2.psicosocial.empleo == false ? true : false;
                    updatePsicosocial.aduSPEmpleoSi = tab2.psicosocial.empleo == true ? true : false;
                    updatePsicosocial.aduSPEmpleoTipo = tab2.psicosocial.aduSPEmpleoTipo;
                    updatePsicosocial.aduSPAmigos = tab2.psicosocial.aduSPAmigos;
                    updatePsicosocial.aduSPVecinos = tab2.psicosocial.aduSPVecinos;
                    updatePsicosocial.aduSPFamilia = tab2.psicosocial.aduSPFamilia;
                    updatePsicosocial.aduSPInstituto = tab2.psicosocial.aduSPInstituto;
                    updatePsicosocial.aduSPOtros = tab2.psicosocial.aduSPOtros;
                    updatePsicosocial.aduSPCuales = tab2.psicosocial.aduSPCuales;
                    updatePsicosocial.aduSPNadie = tab2.psicosocial.aduSPNadie;
                    updatePsicosocial.hcaduid = tab2.antecedentesFamiliares.hcaduid;
                }

                #endregion 

                #region medio ambiente

                var updateMedioAmbiente = context.catHCAduMedioAmbiente.FirstOrDefault(item => item.hcaduid == tab2.antecedentesFamiliares.hcaduid);

                if (updateMedioAmbiente == null)
                {
                    catHCAduMedioAmbiente medioAmbiente = new catHCAduMedioAmbiente()
                    {
                        aduMAViviendaSi = tab2.medioAmbiente.vivienda == true ? true : false,
                        aduMAViviendaNo = tab2.medioAmbiente.vivienda == false ? true : false,
                        aduMAAguaSi = tab2.medioAmbiente.agua == true ? true : false,
                        aduMAAguaNo = tab2.medioAmbiente.agua == false ? true : false,
                        aduMAResiduoSi = tab2.medioAmbiente.residuos == true ? true : false,
                        aduMAResiduoNo = tab2.medioAmbiente.residuos == false ? true : false,
                        aduMAResiduoFrecu = tab2.medioAmbiente.aduMAResiduoFrecu,
                        aduMABanoSi = tab2.medioAmbiente.banio == true ? true : false,
                        aduMABanoNo = tab2.medioAmbiente.banio == false ? true : false,
                        aduMACloacasSi = tab2.medioAmbiente.cloacas == true ? true : false,
                        aduMACloacasNo = tab2.medioAmbiente.cloacas == false ? true : false,
                        hcaduid = tab2.antecedentesFamiliares.hcaduid
                    };

                    if(tab2.medioAmbiente.aduMAHabitaciones != null)
                    { medioAmbiente.aduMAHabitaciones = (short)tab2.medioAmbiente.aduMAHabitaciones;} 
                    else { medioAmbiente.aduMAHabitaciones = null;}

                    if (tab2.medioAmbiente.aduMAConvivientes != null)
                    { medioAmbiente.aduMAConvivientes = (short)tab2.medioAmbiente.aduMAConvivientes; }
                    else { medioAmbiente.aduMAConvivientes = null; }

                    context.catHCAduMedioAmbiente.AddObject(medioAmbiente);
                }
                else
                {
                    updateMedioAmbiente.aduMAViviendaSi = tab2.medioAmbiente.vivienda == true ? true : false;
                    updateMedioAmbiente.aduMAViviendaNo = tab2.medioAmbiente.vivienda == false ? true : false;
                    updateMedioAmbiente.aduMAAguaSi = tab2.medioAmbiente.agua == true ? true : false;
                    updateMedioAmbiente.aduMAAguaNo = tab2.medioAmbiente.agua == false ? true : false;
                    updateMedioAmbiente.aduMAResiduoSi = tab2.medioAmbiente.residuos == true ? true : false;
                    updateMedioAmbiente.aduMAResiduoNo = tab2.medioAmbiente.residuos == false ? true : false;
                    updateMedioAmbiente.aduMAResiduoFrecu = tab2.medioAmbiente.aduMAResiduoFrecu;
                    updateMedioAmbiente.aduMABanoSi = tab2.medioAmbiente.banio == true ? true : false;
                    updateMedioAmbiente.aduMABanoNo = tab2.medioAmbiente.banio == false ? true : false;
                    updateMedioAmbiente.aduMACloacasSi = tab2.medioAmbiente.cloacas == true ? true : false;
                    updateMedioAmbiente.aduMACloacasNo = tab2.medioAmbiente.cloacas == false ? true : false;
                    updateMedioAmbiente.hcaduid = tab2.antecedentesFamiliares.hcaduid;

                    if (tab2.medioAmbiente.aduMAHabitaciones != null)
                    { updateMedioAmbiente.aduMAHabitaciones = (short)tab2.medioAmbiente.aduMAHabitaciones; }
                    else { updateMedioAmbiente.aduMAHabitaciones = null; }

                    if (tab2.medioAmbiente.aduMAConvivientes != null)
                    { updateMedioAmbiente.aduMAConvivientes = (short)tab2.medioAmbiente.aduMAConvivientes; }
                    else { updateMedioAmbiente.aduMAConvivientes = null; }
                }

                #endregion 

                context.SaveChanges();

                return Json(new
                {
                    Error = false,//!infoProceso.IsValid,
                    Mensaje = "",//infoProceso.Mensaje,
                    Foco = "hcperid"
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Error = true,
                    Mensaje = ex.InnerException == null ? ex.Message : ex.InnerException.Message,
                    Foco = "hcperid"
                });
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult setHistoriaClinicaExamenFisico(catHCAduEXAFISICOWS tab5)
        {
            try
            {
                var Usuario = (Session["Usuario"] as sisUsuario).usrNombreDeUsuario;

                #region Antecedentes Familiares

                var updateExamenFisico = context.catHCAduEXAFISICO.Where(item => item.hcaduid == tab5.hcaduid).FirstOrDefault();
                if (updateExamenFisico == null)
                {
                    catHCAduEXAFISICO examenFisico = new catHCAduEXAFISICO()
                    {
                        hcaduid = tab5.hcaduid,
                        aduFITA = tab5.aduFITA,
                        aduFICC = tab5.aduFICC,
                        aduFIPeso = tab5.aduFIPeso,
                        aduFITalla = tab5.aduFITalla,
                        aduFIIMC = tab5.aduFIIMC,
                        aduFIFC = tab5.aduFIFC,
                        aduFIPiel = tab5.aduFIPiel,
                        aduFILentesNo = tab5.aduFILentes == true ? false : true,
                        aduFILentesSi = tab5.aduFILentes == false ? false : true,
                        aduFIOidos = tab5.aduFIOidos,
                        aduFIDentadura = tab5.aduFIDentadura,
                        aduFIPulmones = tab5.aduFIPulmones,
                        aduFICorazon = tab5.aduFICorazon,
                        aduFIAbdomen = tab5.aduFIAbdomen,
                        aduFIGenitales = tab5.aduFIGenitales,
                        aduFIMamas = tab5.aduFIMamas,
                        aduFIOsteo = tab5.aduFIOsteo
                    };

                    context.catHCAduEXAFISICO.AddObject(examenFisico);
                }
                else
                {
                    updateExamenFisico.hcaduid = tab5.hcaduid;
                    updateExamenFisico.aduFITA = tab5.aduFITA;
                    updateExamenFisico.aduFICC = tab5.aduFICC;
                    updateExamenFisico.aduFIPeso = tab5.aduFIPeso;
                    updateExamenFisico.aduFITalla = tab5.aduFITalla;
                    updateExamenFisico.aduFIIMC = tab5.aduFIIMC;
                    updateExamenFisico.aduFIFC = tab5.aduFIFC;
                    updateExamenFisico.aduFIPiel = tab5.aduFIPiel;
                    updateExamenFisico.aduFILentesNo = tab5.aduFILentes == true ? false : true;
                    updateExamenFisico.aduFILentesSi = tab5.aduFILentes == false ? false : true;
                    updateExamenFisico.aduFIOidos = tab5.aduFIOidos;
                    updateExamenFisico.aduFIDentadura = tab5.aduFIDentadura;
                    updateExamenFisico.aduFIPulmones = tab5.aduFIPulmones;
                    updateExamenFisico.aduFICorazon = tab5.aduFICorazon;
                    updateExamenFisico.aduFIAbdomen = tab5.aduFIAbdomen;
                    updateExamenFisico.aduFIGenitales = tab5.aduFIGenitales;
                    updateExamenFisico.aduFIMamas = tab5.aduFIMamas;
                    updateExamenFisico.aduFIOsteo = tab5.aduFIOsteo;
                }

                #endregion

                foreach (var item in tab5.patologicas)
                {
                    var update = (from i in context.catHCAduPraPatologicasResul
                                 where i.aduPraPatResId == item.aduPraPatResId
                                 select i).First();

                    update.aduPraPatFecha1 = item.aduPraPatFecha1;
                    update.aduPraPatFecha2 = item.aduPraPatFecha2;
                    update.aduPraPatFecha3 = item.aduPraPatFecha3;
                    update.aduPraPatFecha4 = item.aduPraPatFecha4;
                    update.aduPraPatFecha5 = item.aduPraPatFecha5;
                    update.aduPraPatResul1 = item.aduPraPatResul1;
                    update.aduPraPatResul2 = item.aduPraPatResul2;
                    update.aduPraPatResul3 = item.aduPraPatResul3;
                    update.aduPraPatResul4 = item.aduPraPatResul4;
                    update.aduPraPatResul5 = item.aduPraPatResul5;
                }

                context.SaveChanges();

                return Json(new
                {
                    Error = false,//!infoProceso.IsValid,
                    Mensaje = "",//infoProceso.Mensaje,
                    Foco = "hcperid"
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Error = true,
                    Mensaje = ex.InnerException == null ? ex.Message : ex.InnerException.Message,
                    Foco = "hcperid"
                });
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult setHistoriaCuidadosPreventivos(catHCAduPraPreventivaResulWS tab4)
        {
            try
            {
                var Usuario = (Session["Usuario"] as sisUsuario).usrNombreDeUsuario;

                #region Cuidados Preventivos

                //foreach (var item in tab4.listado)
                //{
                //    var update = (from i in context.catHCAduPraPreventivaResul
                //                  where i.aduPraPrevResId == item.aduPraPrevResId
                //                  select i).First();

                //    update.aduPraPrevFecha = item.aduPraPrevFecha;
                //    update.aduPraPrevResultado = item.aduPraPrevResultado;
                //}

                context.SaveChanges();

                #endregion

                return Json(new
                {
                    Error = false,//!infoProceso.IsValid,
                    Mensaje = "",//infoProceso.Mensaje,
                    Foco = "hcperid"
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Error = true,
                    Mensaje = ex.InnerException == null ? ex.Message : ex.InnerException.Message,
                    Foco = "hcperid"
                });
            }
        }

        [GridAction]
        public ActionResult getHistoriaClinicaPerinatal(Int64? hcperid, string pAccion, Int64? pacId)
        {
            var datosHC = new catHCPerinatalWS();
            var _MetodosUtiles = new MetodosUtiles();

            var _CentrosDeSalud = (from x in context.catCentroDeSalud
                                                   select new ListaDesplegableWS
                                                   {
                                                       idLista = x.csId,
                                                       Texto = x.csNombre
                                                   }).ToList();

            ViewData["csIdPrenatal_Lista_Data"] = _CentrosDeSalud;
            ViewData["csIdParto_Lista_Data"] = _CentrosDeSalud;

            if (pAccion != "Agregar")
            {
                var InfoHC = context.catHCPerinatal.Single(s => s.hcperid == hcperid);
                var InfoPaciente = InfoHC.catHCPerPaciente.First();
                datosHC = new catHCPerinatalWS()
                {
                    hcperid = InfoHC.hcperid,
                    pacid = InfoHC.pacid,
                    Fecha = InfoHC.Fecha,
                    activa = InfoHC.activa,
                    hcperAccion = pAccion,
                    InformacionPaciente = new catHCPerPacienteWS()
                    {
                        perhcId = InfoPaciente.perhcId,
                        hcperid = InfoPaciente.hcperid,
                        perDomicilio = InfoPaciente.perDomicilio,
                        perLocalidad = InfoPaciente.perLocalidad,
                        perTelefono = InfoPaciente.perTelefono,
                        perEdad = InfoPaciente.perEdad,
                        perEtniaBlanca = InfoPaciente.perEtniaBlanca,
                        perEtniaIndigena = InfoPaciente.perEtniaIndigena,
                        perEtniaMestiza = InfoPaciente.perEtniaMestiza,
                        perEtniaNegra = InfoPaciente.perEtniaNegra,
                        perEtniaOtra = InfoPaciente.perEtniaOtra,
                        perEstudioNinguno = InfoPaciente.perEstudioNinguno,
                        perEstudioPrimaria = InfoPaciente.perEstudioPrimaria,
                        perEstudioSecundaria = InfoPaciente.perEstudioSecundaria,
                        perEstudioUniversitaria = InfoPaciente.perEstudioUniversitaria,
                        perEstudioAnios = InfoPaciente.perEstudioAnios,
                        perEstadoCivilCasada = InfoPaciente.perEstadoCivilCasada,
                        perEstadoCivilUnionEstable = InfoPaciente.perEstadoCivilUnionEstable,
                        perEstadoCivilSoltera = InfoPaciente.perEstadoCivilSoltera,
                        perEstadoCivilOtro = InfoPaciente.perEstadoCivilOtro,
                        perAlfabeta = InfoPaciente.perAlfabeta,
                        perViveSola = InfoPaciente.perViveSola,
                        perInfoPaciente = new catPacientes()
                        {
                            pacApellidoyNombre = InfoHC.catPaciente.pacNombre + " " + InfoHC.catPaciente.pacApellido,
                            pacNumeroDocumento = InfoHC.catPaciente.pacNumeroDocumento
                        },
                        //perPrenatal = InfoPaciente.perPrenatal,
                        //perParto = InfoPaciente.perParto
                    }
                };
            }
            else
            {
                var InfoPac = context.catPaciente.Single(s => s.pacId == pacId);
                var _csId = (Session["UsuarioCentroDeSalud"] as sisUsuariosCentroDeSalud).csId;

                datosHC.hcperAccion = pAccion;
                datosHC.pacid = (int)pacId;
                datosHC.Fecha = DateTime.Now;
                datosHC.activa = true;
                datosHC.hcperAccion = pAccion;
                datosHC.InformacionPaciente = new catHCPerPacienteWS()
                {
                    perDomicilio = InfoPac.pacCalle,
                    perLocalidad = InfoPac.catDepartamento.depNombre,
                    perTelefono = InfoPac.pacTelefonoCasa,
                    csIdPrenatal = _csId,
                    csIdParto = InfoPac.csId,
                    perInfoPaciente =
                        new catPacientes()
                        {
                            pacApellido = InfoPac.pacApellido,
                            pacNombre = InfoPac.pacNombre,
                            pacApellidoyNombre = InfoPac.pacNombre + " " + InfoPac.pacApellido,
                            pacFechaNacimiento = InfoPac.pacFechaNacimiento,
                            pacNumeroDocumento = InfoPac.pacNumeroDocumento
                        },
                    perEdad = (short)_MetodosUtiles.Edad(InfoPac.pacFechaNacimiento)
                };

            }

            //ViewData["espId_Data"] = new SelectList(context.getListaEspecialidades().ToList(), "espId", "espNombre");

            return PartialView("wHCCRUDPerinatales", datosHC);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult setHistoriaClinicaPerinatal(catHCPerinatalWS pInfoCrud)
        {
            try
            {
                //var Usuario = (Session["Usuario"] as sisUsuario).usrNombreDeUsuario;
                //var infoProceso = context.setCartaMedica(cmedId, espId, tipoCM, prioridad, Usuario, "Recibir").First();
                return Json(new
                {
                    Error = false,//!infoProceso.IsValid,
                    Mensaje = "",//infoProceso.Mensaje,
                    Foco = "hcperid"
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Error = true,
                    Mensaje = ex.InnerException == null ? ex.Message : ex.InnerException.Message,
                    Foco = "hcperid"
                });
            }
        }

        [GridAction]
        public ActionResult CalendarioDeProgramacion(int Id)
        {
            catHCPerCalendarioWS model = new catHCPerCalendarioWS();
            if (Session["ListaCalendario"] == null)
            {
                var ListaDeDatos = context.getCalendarioDeProgramacion(Id, 2016);

                model.gridListaDeDatos = ListaDeDatos.ToList();
                Session["ListaCalendario"] = model.gridListaDeDatos;
            }
            else
            {
                model.gridListaDeDatos = (Session["ListaCalendario"] as List<getCalendarioDeProgramacion_Result>);
            }

            return View(model);
        }

        public ActionResult getCalendarioCRUD(string Mes, int Dia, int idHC)
        {
            var Consulta = (Session["ListaCalendario"] as List<getCalendarioDeProgramacion_Result>).First(f => f.Mes == Mes);

            var CRUDconsulta = new catHCPerCalendarioWS();

            switch (Dia) {
                case 1:
                    CRUDconsulta.hcperid = idHC;
                    CRUDconsulta.perCACumplida = Consulta.perCACumplida1.Value;
                    CRUDconsulta.perCAFecha = Consulta.perCAFecha1.Value;
                    CRUDconsulta.perCATipo = Consulta.perCATipo1;
                    CRUDconsulta.perCAid = Consulta.perCAid1.Value;
                    break;
                case 2:
                    CRUDconsulta.hcperid = idHC;
                    CRUDconsulta.perCACumplida = Consulta.perCACumplida2.Value;
                    CRUDconsulta.perCAFecha = Consulta.perCAFecha2.Value;
                    CRUDconsulta.perCATipo = Consulta.perCATipo2;
                    CRUDconsulta.perCAid = Consulta.perCAid2.Value;
                    break;
                case 3:
                    CRUDconsulta.hcperid = idHC;
                    CRUDconsulta.perCACumplida = Consulta.perCACumplida3.Value;
                    CRUDconsulta.perCAFecha = Consulta.perCAFecha3.Value;
                    CRUDconsulta.perCATipo = Consulta.perCATipo3;
                    CRUDconsulta.perCAid = Consulta.perCAid3.Value;
                    break;
                case 4:
                    CRUDconsulta.hcperid = idHC;
                    CRUDconsulta.perCACumplida = Consulta.perCACumplida4.Value;
                    CRUDconsulta.perCAFecha = Consulta.perCAFecha4.Value;
                    CRUDconsulta.perCATipo = Consulta.perCATipo4;
                    CRUDconsulta.perCAid = Consulta.perCAid4.Value;
                    break;
                case 5:
                    CRUDconsulta.hcperid = idHC;
                    CRUDconsulta.perCACumplida = Consulta.perCACumplida5.Value;
                    CRUDconsulta.perCAFecha = Consulta.perCAFecha5.Value;
                    CRUDconsulta.perCATipo = Consulta.perCATipo5;
                    CRUDconsulta.perCAid = Consulta.perCAid5.Value;
                    break;
                case 6:
                    CRUDconsulta.hcperid = idHC;
                    CRUDconsulta.perCACumplida = Consulta.perCACumplida6.Value;
                    CRUDconsulta.perCAFecha = Consulta.perCAFecha6.Value;
                    CRUDconsulta.perCATipo = Consulta.perCATipo6;
                    CRUDconsulta.perCAid = Consulta.perCAid6.Value;
                    break;
                case 7:
                    CRUDconsulta.hcperid = idHC;
                    CRUDconsulta.perCACumplida = Consulta.perCACumplida7.Value;
                    CRUDconsulta.perCAFecha = Consulta.perCAFecha7.Value;
                    CRUDconsulta.perCATipo = Consulta.perCATipo7;
                    CRUDconsulta.perCAid = Consulta.perCAid7.Value;
                    break;
                case 8:
                    CRUDconsulta.hcperid = idHC;
                    CRUDconsulta.perCACumplida = Consulta.perCACumplida8.Value;
                    CRUDconsulta.perCAFecha = Consulta.perCAFecha8.Value;
                    CRUDconsulta.perCATipo = Consulta.perCATipo8;
                    CRUDconsulta.perCAid = Consulta.perCAid8.Value;
                    break;
                case 9:
                    CRUDconsulta.hcperid = idHC;
                    CRUDconsulta.perCACumplida = Consulta.perCACumplida9.Value;
                    CRUDconsulta.perCAFecha = Consulta.perCAFecha9.Value;
                    CRUDconsulta.perCATipo = Consulta.perCATipo9;
                    CRUDconsulta.perCAid = Consulta.perCAid9.Value;
                    break;
                case 10:
                    CRUDconsulta.hcperid = idHC;
                    CRUDconsulta.perCACumplida = Consulta.perCACumplida10.Value;
                    CRUDconsulta.perCAFecha = Consulta.perCAFecha10.Value;
                    CRUDconsulta.perCATipo = Consulta.perCATipo10;
                    CRUDconsulta.perCAid = Consulta.perCAid10.Value;
                    break;
                case 11:
                    CRUDconsulta.hcperid = idHC;
                    CRUDconsulta.perCACumplida = Consulta.perCACumplida11.Value;
                    CRUDconsulta.perCAFecha = Consulta.perCAFecha11.Value;
                    CRUDconsulta.perCATipo = Consulta.perCATipo11;
                    CRUDconsulta.perCAid = Consulta.perCAid11.Value;
                    break;
                case 12:
                    CRUDconsulta.hcperid = idHC;
                    CRUDconsulta.perCACumplida = Consulta.perCACumplida12.Value;
                    CRUDconsulta.perCAFecha = Consulta.perCAFecha12.Value;
                    CRUDconsulta.perCATipo = Consulta.perCATipo12;
                    CRUDconsulta.perCAid = Consulta.perCAid12.Value;
                    break;
                case 13:
                    CRUDconsulta.hcperid = idHC;
                    CRUDconsulta.perCACumplida = Consulta.perCACumplida13.Value;
                    CRUDconsulta.perCAFecha = Consulta.perCAFecha13.Value;
                    CRUDconsulta.perCATipo = Consulta.perCATipo13;
                    CRUDconsulta.perCAid = Consulta.perCAid13.Value;
                    break;
                case 14:
                    CRUDconsulta.hcperid = idHC;
                    CRUDconsulta.perCACumplida = Consulta.perCACumplida14.Value;
                    CRUDconsulta.perCAFecha = Consulta.perCAFecha14.Value;
                    CRUDconsulta.perCATipo = Consulta.perCATipo14;
                    CRUDconsulta.perCAid = Consulta.perCAid14.Value;
                    break;
                case 15:
                    CRUDconsulta.hcperid = idHC;
                    CRUDconsulta.perCACumplida = Consulta.perCACumplida15.Value;
                    CRUDconsulta.perCAFecha = Consulta.perCAFecha15.Value;
                    CRUDconsulta.perCATipo = Consulta.perCATipo15;
                    CRUDconsulta.perCAid = Consulta.perCAid15.Value;
                    break;
                case 16:
                    CRUDconsulta.hcperid = idHC;
                    CRUDconsulta.perCACumplida = Consulta.perCACumplida16.Value;
                    CRUDconsulta.perCAFecha = Consulta.perCAFecha16.Value;
                    CRUDconsulta.perCATipo = Consulta.perCATipo16;
                    CRUDconsulta.perCAid = Consulta.perCAid16.Value;
                    break;
                case 17:
                    CRUDconsulta.hcperid = idHC;
                    CRUDconsulta.perCACumplida = Consulta.perCACumplida17.Value;
                    CRUDconsulta.perCAFecha = Consulta.perCAFecha17.Value;
                    CRUDconsulta.perCATipo = Consulta.perCATipo17;
                    CRUDconsulta.perCAid = Consulta.perCAid17.Value;
                    break;
                case 18:
                    CRUDconsulta.hcperid = idHC;
                    CRUDconsulta.perCACumplida = Consulta.perCACumplida18.Value;
                    CRUDconsulta.perCAFecha = Consulta.perCAFecha18.Value;
                    CRUDconsulta.perCATipo = Consulta.perCATipo18;
                    CRUDconsulta.perCAid = Consulta.perCAid18.Value;
                    break;
                case 19:
                    CRUDconsulta.hcperid = idHC;
                    CRUDconsulta.perCACumplida = Consulta.perCACumplida19.Value;
                    CRUDconsulta.perCAFecha = Consulta.perCAFecha19.Value;
                    CRUDconsulta.perCATipo = Consulta.perCATipo19;
                    CRUDconsulta.perCAid = Consulta.perCAid19.Value;
                    break;
                case 20:
                    CRUDconsulta.hcperid = idHC;
                    CRUDconsulta.perCACumplida = Consulta.perCACumplida20.Value;
                    CRUDconsulta.perCAFecha = Consulta.perCAFecha20.Value;
                    CRUDconsulta.perCATipo = Consulta.perCATipo20;
                    CRUDconsulta.perCAid = Consulta.perCAid20.Value;
                    break;
                case 21:
                    CRUDconsulta.hcperid = idHC;
                    CRUDconsulta.perCACumplida = Consulta.perCACumplida21.Value;
                    CRUDconsulta.perCAFecha = Consulta.perCAFecha21.Value;
                    CRUDconsulta.perCATipo = Consulta.perCATipo21;
                    CRUDconsulta.perCAid = Consulta.perCAid21.Value;
                    break;
                case 22:
                    CRUDconsulta.hcperid = idHC;
                    CRUDconsulta.perCACumplida = Consulta.perCACumplida22.Value;
                    CRUDconsulta.perCAFecha = Consulta.perCAFecha22.Value;
                    CRUDconsulta.perCATipo = Consulta.perCATipo22;
                    CRUDconsulta.perCAid = Consulta.perCAid22.Value;
                    break;
                case 23:
                    CRUDconsulta.hcperid = idHC;
                    CRUDconsulta.perCACumplida = Consulta.perCACumplida23.Value;
                    CRUDconsulta.perCAFecha = Consulta.perCAFecha23.Value;
                    CRUDconsulta.perCATipo = Consulta.perCATipo23;
                    CRUDconsulta.perCAid = Consulta.perCAid23.Value;
                    break;
                case 24:
                    CRUDconsulta.hcperid = idHC;
                    CRUDconsulta.perCACumplida = Consulta.perCACumplida24.Value;
                    CRUDconsulta.perCAFecha = Consulta.perCAFecha24.Value;
                    CRUDconsulta.perCATipo = Consulta.perCATipo24;
                    CRUDconsulta.perCAid = Consulta.perCAid24.Value;
                    break;
                case 25:
                    CRUDconsulta.hcperid = idHC;
                    CRUDconsulta.perCACumplida = Consulta.perCACumplida25.Value;
                    CRUDconsulta.perCAFecha = Consulta.perCAFecha25.Value;
                    CRUDconsulta.perCATipo = Consulta.perCATipo25;
                    CRUDconsulta.perCAid = Consulta.perCAid25.Value;
                    break;
                case 26:
                    CRUDconsulta.hcperid = idHC;
                    CRUDconsulta.perCACumplida = Consulta.perCACumplida26.Value;
                    CRUDconsulta.perCAFecha = Consulta.perCAFecha26.Value;
                    CRUDconsulta.perCATipo = Consulta.perCATipo26;
                    CRUDconsulta.perCAid = Consulta.perCAid26.Value;
                    break;
                case 27:
                    CRUDconsulta.hcperid = idHC;
                    CRUDconsulta.perCACumplida = Consulta.perCACumplida27.Value;
                    CRUDconsulta.perCAFecha = Consulta.perCAFecha27.Value;
                    CRUDconsulta.perCATipo = Consulta.perCATipo27;
                    CRUDconsulta.perCAid = Consulta.perCAid27.Value;
                    break;
                case 28:
                    CRUDconsulta.hcperid = idHC;
                    CRUDconsulta.perCACumplida = Consulta.perCACumplida28.Value;
                    CRUDconsulta.perCAFecha = Consulta.perCAFecha28.Value;
                    CRUDconsulta.perCATipo = Consulta.perCATipo28;
                    CRUDconsulta.perCAid = Consulta.perCAid28.Value;
                    break;
                case 29:
                    CRUDconsulta.hcperid = idHC;
                    CRUDconsulta.perCACumplida = Consulta.perCACumplida29.Value;
                    CRUDconsulta.perCAFecha = Consulta.perCAFecha29.Value;
                    CRUDconsulta.perCATipo = Consulta.perCATipo29;
                    CRUDconsulta.perCAid = Consulta.perCAid29.Value;
                    break;
                case 30:
                    CRUDconsulta.hcperid = idHC;
                    CRUDconsulta.perCACumplida = Consulta.perCACumplida30.Value;
                    CRUDconsulta.perCAFecha = Consulta.perCAFecha30.Value;
                    CRUDconsulta.perCATipo = Consulta.perCATipo30;
                    CRUDconsulta.perCAid = Consulta.perCAid30.Value;
                    break;
                case 31:
                    CRUDconsulta.hcperid = idHC;
                    CRUDconsulta.perCACumplida = Consulta.perCACumplida31.Value;
                    CRUDconsulta.perCAFecha = Consulta.perCAFecha31.Value;
                    CRUDconsulta.perCATipo = Consulta.perCATipo31;
                    CRUDconsulta.perCAid = Consulta.perCAid31.Value;
                    break;
            }

            return PartialView("CalendarioDeProgramacionCRUD", CRUDconsulta);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult setCalendarioCRUD(string Mes, int Dia, bool Cumplida, string Tipo)
        {
            var Consulta = (Session["ListaCalendario"] as List<getCalendarioDeProgramacion_Result>).First(f => f.Mes == Mes);

            switch (Dia)
            {
                case 1:
                    Consulta.perCACumplida1 = Cumplida;
                    Consulta.perCATipo1 = Tipo;
                    break;
                case 2:
                    Consulta.perCACumplida2 = Cumplida;
                    Consulta.perCATipo2 = Tipo;
                    break;
                case 3:
                    Consulta.perCACumplida3 = Cumplida;
                    Consulta.perCATipo3 = Tipo;
                    break;
                case 4:
                    Consulta.perCACumplida4 = Cumplida;
                    Consulta.perCATipo4 = Tipo;
                    break;
                case 5:
                    Consulta.perCACumplida5 = Cumplida;
                    Consulta.perCATipo5 = Tipo;
                    break;
                case 6:
                    Consulta.perCACumplida6 = Cumplida;
                    Consulta.perCATipo6 = Tipo;
                    break;
                case 7:
                    Consulta.perCACumplida7 = Cumplida;
                    Consulta.perCATipo7 = Tipo;
                    break;
                case 8:
                    Consulta.perCACumplida8 = Cumplida;
                    Consulta.perCATipo8 = Tipo;
                    break;
                case 9:
                    Consulta.perCACumplida9 = Cumplida;
                    Consulta.perCATipo9 = Tipo;
                    break;
                case 10:
                    Consulta.perCACumplida10 = Cumplida;
                    Consulta.perCATipo10 = Tipo;
                    break;
                case 11:
                    Consulta.perCACumplida11 = Cumplida;
                    Consulta.perCATipo11 = Tipo;
                    break;
                case 12:
                    Consulta.perCACumplida12 = Cumplida;
                    Consulta.perCATipo12 = Tipo;
                    break;
                case 13:
                    Consulta.perCACumplida13 = Cumplida;
                    Consulta.perCATipo13 = Tipo;
                    break;
                case 14:
                    Consulta.perCACumplida14 = Cumplida;
                    Consulta.perCATipo14 = Tipo;
                    break;
                case 15:
                    Consulta.perCACumplida15 = Cumplida;
                    Consulta.perCATipo15 = Tipo;
                    break;
                case 16:
                    Consulta.perCACumplida16 = Cumplida;
                    Consulta.perCATipo16 = Tipo;
                    break;
                case 17:
                    Consulta.perCACumplida17 = Cumplida;
                    Consulta.perCATipo17 = Tipo;
                    break;
                case 18:
                    Consulta.perCACumplida18 = Cumplida;
                    Consulta.perCATipo18 = Tipo;
                    break;
                case 19:
                    Consulta.perCACumplida19 = Cumplida;
                    Consulta.perCATipo19 = Tipo;
                    break;
                case 20:
                    Consulta.perCACumplida20 = Cumplida;
                    Consulta.perCATipo20 = Tipo;
                    break;
                case 21:
                    Consulta.perCACumplida21 = Cumplida;
                    Consulta.perCATipo21 = Tipo;
                    break;
                case 22:
                    Consulta.perCACumplida22 = Cumplida;
                    Consulta.perCATipo22 = Tipo;
                    break;
                case 23:
                    Consulta.perCACumplida23 = Cumplida;
                    Consulta.perCATipo23 = Tipo;
                    break;
                case 24:
                    Consulta.perCACumplida24 = Cumplida;
                    Consulta.perCATipo24 = Tipo;
                    break;
                case 25:
                    Consulta.perCACumplida25 = Cumplida;
                    Consulta.perCATipo25 = Tipo;
                    break;
                case 26:
                    Consulta.perCACumplida26 = Cumplida;
                    Consulta.perCATipo26 = Tipo;
                    break;
                case 27:
                    Consulta.perCACumplida27 = Cumplida;
                    Consulta.perCATipo27 = Tipo;
                    break;
                case 28:
                    Consulta.perCACumplida28 = Cumplida;
                    Consulta.perCATipo28 = Tipo;
                    break;
                case 29:
                    Consulta.perCACumplida29 = Cumplida;
                    Consulta.perCATipo29 = Tipo;
                    break;
                case 30:
                    Consulta.perCACumplida30 = Cumplida;
                    Consulta.perCATipo30 = Tipo;
                    break;
                case 31:
                    Consulta.perCACumplida31 = Cumplida;
                    Consulta.perCATipo31 = Tipo;
                    break;
            }

            return Json(new
            {
                IsValid = true,
                Mensaje = ""
            });
        }

        [GridAction]
        public ActionResult ConsultasAntenatales(int Id)
        {
            Session["Iniciando_g_catHCPerConsultasWS"] = true;
            Session["g_catHCPerConsultasWS"] = new List<catHCPerConsultasWS>();
            catHCPerinatalWS model=new catHCPerinatalWS();
            catHCPerinatal item= context.catHCPerinatal.Where(i => i.hcperid == Id).FirstOrDefault();
            if (item != null)
                model.hcperid = item.hcperid;
            else model.hcperid = 0;

            return View(model);
        }


        [GridAction]
        public ActionResult getConsultasAntenatales(int Id)
        {
            List<catHCPerConsultasWS> list = (List<catHCPerConsultasWS>)Session["g_catHCPerConsultasWS"];
            bool listaIniciada = (bool)Session["Iniciando_g_catHCPerConsultasWS"];
            if (list.Count == 0 && listaIniciada == true)
            {
                foreach (var x in context.catHCPerConsultas.Where(i => i.hcperid == Id).ToList())
                {
                    list.Add(new catHCPerConsultasWS()
                    {
                        hcperid = x.hcperid,
                        perCAlturaUt = x.perCAlturaUt,
                        perCEdadGes = x.perCEdadGes,
                        perCFCF = x.perCFCF,
                        perCFecha = x.perCFecha,
                        perCFechaProx = x.perCFechaProx,
                        perCMovF = x.perCMovF,
                        perConsId = x.perConsId,
                        perCPA = x.perCPA,
                        perCpeso = x.perCpeso,
                        perCPresenta = x.perCPresenta,
                        perCSignos = x.perCSignos,
                        perCTecnico = x.perCTecnico,
                        turId = x.turId
                    });
                }
                Session["Iniciando_g_catHCPerConsultasWS"] = false;
            }
            return View(new GridModel(list));
        }
        [GridAction]
        public ActionResult getConsultaAntenatal(int Id, string pAccion)
        {
            catHCPerConsultasWS consulta = new catHCPerConsultasWS() { perCFecha=DateTime.Now, perCFechaProx = DateTime.Now.AddDays(30) };
            if (pAccion != "Agregar")
            {
                consulta = (from x in (List<catHCPerConsultasWS>)Session["g_catHCPerConsultasWS"]
                            where x.hcperid == Id
                            select new catHCPerConsultasWS()
                           {
                               hcperid = x.hcperid,
                               perCFechaProx = x.perCFechaProx,
                               perCFecha = x.perCFecha,
                               perCAlturaUt = x.perCAlturaUt,
                               pAccion = x.pAccion,
                               perCEdadGes = x.perCEdadGes,
                               perCFCF = x.perCFCF,
                               perCMovF = x.perCMovF,
                               perConsId = x.perConsId,
                               perCPresenta = x.perCPresenta,
                               perCpeso = x.perCpeso,
                               perCSignos = x.perCSignos,
                               perCTecnico = x.perCTecnico,
                               perCPA = x.perCPA,
                               turId = x.turId
                           }).FirstOrDefault();
            }
            return PartialView("CRUDConsultaAntenatal", consulta);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult setConsultaAntenatal(catHCPerConsultasWS consulta)
        {
            try
            {
                List<catHCPerConsultasWS> listaActual = (List<catHCPerConsultasWS>)Session["g_catHCPerConsultasWS"];
                catHCPerConsultasWS tituloEnLista = listaActual.FirstOrDefault(i => i.hcperid == consulta.hcperid && consulta.pAccion != "Agregar");
                if (consulta.pAccion != "Eliminar")
                {                    
                    if (tituloEnLista != null)
                        listaActual.Remove(tituloEnLista);
                    else
                    {
                        consulta.hcperid = listaActual.Count > 0 ? listaActual.Max(i => i.hcperid) + 1 : 1;
                    }

                    listaActual.Add(consulta);
                }
                else
                {
                    listaActual.Remove(tituloEnLista);
                }
                Session["g_catHCPerConsultasWS"] = listaActual;
                return Json(new
                {
                    Id = 0,
                    Error = false
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Error = true,
                    Mensaje = ex.InnerException == null ? ex.Message : ex.InnerException.Message,
                });
            }
        }

        [GridAction]
        public ActionResult ConsultasHistoriaFamiliarYPersonal(int pacId)
        {
            catHCAduHistoriaFamiliarTabWS tab = new catHCAduHistoriaFamiliarTabWS();

            var paciente = context.catHCAdulto.FirstOrDefault(item => item.pacid == pacId);

            if (paciente != null)
            {
                #region Antecedentes familiares

                tab.antecedentesFamiliares =    (from af in context.catHCAduAnteFamiliar
                                                where af.hcaduid == paciente.hcaduid
                                                select new catHCAduAnteFamiliarWS()
                                                {
                                                    hcaduid = af.hcaduid,
                                                    aduAFHipertension = (bool)af.aduAFHipertension,
                                                    aduAFCardiacas = (bool)af.aduAFCardiacas,
                                                    aduAFDiabetes = (bool)af.aduAFDiabetes,
                                                    aduAFACV = (bool)af.aduAFACV,
                                                    aduAFCancerColon = (bool)af.aduAFCancerColon,
                                                    aduAFCancerMama = (bool)af.aduAFCancerMama,
                                                    aduAFCancerOtro = (bool)af.aduAFCancerOtro,
                                                    aduAFCeliaca = (bool)af.aduAFCeliaca,
                                                    aduAFDroga = (bool)af.aduAFDroga,
                                                    aduAFDepresion = (bool)af.aduAFDepresion,
                                                    aduAFCancerOtroCual = af.aduAFCancerOtroCual
                                                }).FirstOrDefault();

                if (tab.antecedentesFamiliares == null)
                {
                    tab.antecedentesFamiliares = new catHCAduAnteFamiliarWS();
                    tab.antecedentesFamiliares.hcaduid = paciente.hcaduid;
                }
                #endregion 

                #region Antecedentes médicos

                tab.antecedentesMedicos = (from am in context.catHCAduAnteMedico
                                          where am.hcaduid == paciente.hcaduid
                                          select new catHCAduAnteMedicoWS()
                                          {
                                              aduAMAlergia = (bool)am.aduAMAlergia,
                                              aduAMAlergiaCual = am.aduAMAlergiaCual,
                                              aduAMMotivo1 = am.aduAMMotivo1,
                                              aduAMMotivo2 = am.aduAMMotivo2,
                                              aduAMMotivo3 = am.aduAMMotivo3,
                                              aduAMAño1 = (Int32)am.aduAMAño1,
                                              aduAMAño2 = (Int32)am.aduAMAño2,
                                              aduAMAño3 = (Int32)am.aduAMAño3,
                                              aduAMTranfusiones = (bool)am.aduAMTranfusiones,
                                              aduAMTranfusionesCuando = am.aduAMTranfusionesCuando,
                                              aduAMInfecciones = (bool)am.aduAMInfecciones,
                                              aduAMInfeccionesCual = am.aduAMInfeccionesCual,
                                              aduAMInterna = (bool)am.aduAMInterna
                                          }).FirstOrDefault();

                if (tab.antecedentesMedicos == null)
                {
                    tab.antecedentesMedicos = new catHCAduAnteMedicoWS();
                }
                #endregion

                #region Antecedentes ginecologicos 

                tab.antecedentesGinecologicos = (from ag in context.catHCAduAnteGineco
                                                 where ag.hcaduid == paciente.hcaduid
                                                 select new catHCAduAnteGinecoWS() { 
                                                    aduAGMenarca = (short)ag.aduAGMenarca,
                                                    aduAGIRS = (short)ag.aduAGIRS,
                                                    aduAGGestas = (short)ag.aduAGGestas,
                                                    aduAGPartos = (short)ag.aduAGPartos,
                                                    aduAGCesareas = (short)ag.aduAGCesareas,
                                                    aduAGAbortos = (short)ag.aduAGAbortos,
                                                    aduAGCiclos = ag.aduAGCiclos,
                                                    aduAGMenopausia = (short)ag.aduAGMenopausia,
                                                    aduAGQuirurgica = (bool)ag.aduAGQuirurgica,
                                                    aduAGDIU = (bool)ag.aduAGDIU,
                                                    aduAGImplante = (bool)ag.aduAGImplante,
                                                    aduAGMadreSola = (bool)ag.aduAGMadreSola,
                                                 }).FirstOrDefault();

                if (tab.antecedentesGinecologicos == null)
                {
                    tab.antecedentesGinecologicos = new catHCAduAnteGinecoWS();
                }

                #endregion 

                #region Habitos

                var habitos = (from ha in context.catHCAduHabitos
                               where ha.hcaduid == paciente.hcaduid
                               select ha).FirstOrDefault();


                if (habitos == null)
                {
                    tab.habitos = new catHCAduHabitosWS();
                }
                else
                {
                    tab.habitos = new catHCAduHabitosWS()
                               {
                                   aduHAFuma = (bool)habitos.aduHAFuma,
                                   //aduHAFumaCuando = (DateTime)ha.aduHAFumaCuando,
                                   aduHANuncaFuma = (bool)habitos.aduHANuncaFuma,
                                   aduHADejo = (bool)habitos.aduHADejo,
                                   //aduHADejoCuando = (DateTime)ha.aduHADejoCuando,
                                   aduHAFumadorPasivo = (bool)habitos.aduHAFumadorPasivo,
                                   aduHANoBebe = (bool)habitos.aduHANoBebe,
                                   aduHABebe = (bool)habitos.aduHABebe,
                                   aduHABebeComidas = (bool)habitos.aduHABebeComidas,
                                   aduHAVasosPorDia = (int)habitos.aduHAVasosPorDia,
                                   aduHASedentarismo = (bool)habitos.aduHASedentarismo,
                                   aduHAFisico = (bool)habitos.aduHAFisico,
                                   aduHAFisicoTipo = habitos.aduHAFisicoTipo,
                                   aduHAFisicoFrecuancia = habitos.aduHAFisicoFrecuancia,
                                   aduHASalero = (bool)habitos.aduHASalero,
                                   aduHAComida = (bool)habitos.aduHAComida,
                                   aduHADesayuno = (bool)habitos.aduHADesayuno,
                                   aduHACascoAVeces = (bool)habitos.aduHACascoAVeces,
                                   aduHACascoNunca = (bool)habitos.aduHACascoNunca,
                                   aduHACascoSiempre = (bool)habitos.aduHACascoSiempre,
                                   aduHADrogaSi = (bool)habitos.aduHADrogaSi,
                                   aduHADrogaNo = (bool)habitos.aduHADrogaNo,
                                   aduHADrogaCual = habitos.aduHADrogaCual,
                               };

                    if (habitos.aduHAFumaCuando == null)
                    { tab.habitos.aduHAFumaCuando = DateTime.Now; }
                    else { tab.habitos.aduHAFumaCuando = (DateTime)habitos.aduHAFumaCuando; }
                    if (habitos.aduHADejoCuando == null)
                    { tab.habitos.aduHADejoCuando = DateTime.Now; }
                    else { tab.habitos.aduHADejoCuando = (DateTime)habitos.aduHADejoCuando; }

                    //Tabaco
                    if (tab.habitos.aduHAFuma == true) { tab.habitos.tabaco = "FU"; }
                    if (tab.habitos.aduHANuncaFuma == true) { tab.habitos.tabaco = "NU"; }
                    if (tab.habitos.aduHADejo == true) { tab.habitos.tabaco = "DJ"; }
                    if (tab.habitos.aduHAFumadorPasivo == true) { tab.habitos.tabaco = "PS"; }
                    if (tab.habitos.tabaco == "")
                    {
                        tab.habitos.tabaco = "NU";
                    }
                    //Alcohol
                    if (tab.habitos.aduHABebe == true) { tab.habitos.alcohol = "BE"; }
                    if (tab.habitos.aduHANoBebe == true) { tab.habitos.alcohol = "NB"; }
                    if (tab.habitos.aduHABebeComidas == true) { tab.habitos.alcohol = "CM"; }
                    if (tab.habitos.alcohol == "")
                    {
                        tab.habitos.alcohol = "NB";
                    }
                    //Cinturon
                    if (tab.habitos.aduHACascoSiempre == true) { tab.habitos.cinturon = "SI"; }
                    if (tab.habitos.aduHACascoAVeces == true) { tab.habitos.cinturon = "AV"; }
                    if (tab.habitos.aduHACascoNunca == true) { tab.habitos.cinturon = "NO"; }
                    if (tab.habitos.cinturon == "")
                    {
                        tab.habitos.cinturon = "NO";
                    }
                    //Drogas
                    if (tab.habitos.aduHADrogaSi == true)
                    {
                        tab.habitos.drogas = "SI";
                    }
                    else
                    {
                        tab.habitos.drogas = "NO";
                    }
                }

                #endregion

                #region Situacion Psicologica

                tab.psicosocial = (from ps in context.catHCAduSituacionPsico
                                   where ps.hcaduid == paciente.hcaduid
                                   select new catHCAduSituacionPsicoWS()
                                   {
                                       aduSPviolenciaNo = (bool)ps.aduSPviolenciaNo,
                                       aduSPviolenciaSi = (bool)ps.aduSPviolenciaSi,
                                       aduSPDuelo = (bool)ps.aduSPDuelo,
                                       aduSPSeparacion = (bool)ps.aduSPSeparacion,
                                       aduSPTrabajo = (bool)ps.aduSPTrabajo,
                                       aduSPTraslado = (bool)ps.aduSPTraslado,
                                       aduSPNacimiento = (bool)ps.aduSPNacimiento,
                                       aduSPEmpleoNo = (bool)ps.aduSPEmpleoNo,
                                       aduSPEmpleoSi = (bool)ps.aduSPEmpleoSi,
                                       aduSPEmpleoTipo = ps.aduSPEmpleoTipo,
                                       aduSPAmigos = (bool)ps.aduSPAmigos,
                                       aduSPVecinos = (bool)ps.aduSPVecinos,
                                       aduSPFamilia = (bool)ps.aduSPFamilia,
                                       aduSPInstituto = (bool)ps.aduSPInstituto,
                                       aduSPOtros = (bool)ps.aduSPOtros,
                                       aduSPCuales = ps.aduSPCuales,
                                       aduSPNadie = (bool)ps.aduSPNadie
                                   }).FirstOrDefault();

                if (tab.psicosocial == null)
                {
                    tab.psicosocial = new catHCAduSituacionPsicoWS();
                }
                else 
                {
                    if (tab.psicosocial.aduSPviolenciaSi == true)
                    {
                        tab.psicosocial.violencia = "SI";
                    }
                    else
                    {
                        tab.psicosocial.violencia = "NO";
                    }

                    if (tab.psicosocial.aduSPEmpleoSi == true)
                    {
                        tab.psicosocial.empleo = true;
                    }
                    else
                    {
                        tab.psicosocial.empleo = false;
                    }
                }
                #endregion 

                #region Medio Ambiente

                tab.medioAmbiente = (from md in context.catHCAduMedioAmbiente
                                     where md.hcaduid == paciente.hcaduid
                                     select new catHCAduMedioAmbienteWS()
                                     {
                                         aduMAHabitaciones = (int)md.aduMAHabitaciones,
                                         aduMAConvivientes = (int)md.aduMAConvivientes,
                                         aduMAViviendaSi = (bool)md.aduMAViviendaSi,
                                         aduMAViviendaNo = (bool)md.aduMAViviendaNo,
                                         aduMAAguaSi = (bool)md.aduMAViviendaSi,
                                         aduMAAguaNo = (bool)md.aduMAAguaNo,
                                         aduMAResiduoSi = (bool)md.aduMAResiduoSi,
                                         aduMAResiduoNo = (bool)md.aduMAResiduoNo,
                                         aduMAResiduoFrecu = md.aduMAResiduoFrecu,
                                         aduMABanoSi = (bool)md.aduMABanoSi,
                                         aduMABanoNo = (bool)md.aduMABanoNo,
                                         aduMACloacasSi = (bool)md.aduMACloacasSi,
                                         aduMACloacasNo = (bool)md.aduMACloacasNo
                                     }).FirstOrDefault();

                if (tab.medioAmbiente == null)
                {
                    tab.medioAmbiente = new catHCAduMedioAmbienteWS();
                }
                else
                {
                    if (tab.medioAmbiente.aduMAViviendaSi == true)
                    {
                        tab.medioAmbiente.vivienda = true;
                    }
                    else
                    {
                        tab.medioAmbiente.vivienda = false;
                    }

                    if (tab.medioAmbiente.aduMAAguaSi == true)
                    {
                        tab.medioAmbiente.agua = true;
                    }
                    else
                    {
                        tab.medioAmbiente.agua = false;
                    }

                    if (tab.medioAmbiente.aduMAResiduoSi == true)
                    {
                        tab.medioAmbiente.residuos = true;
                    }
                    else
                    {
                        tab.medioAmbiente.residuos = false;
                    }

                    if (tab.medioAmbiente.aduMABanoSi == true)
                    {
                        tab.medioAmbiente.banio = true;
                    }
                    else
                    {
                        tab.medioAmbiente.banio = false;
                    }

                    if (tab.medioAmbiente.aduMACloacasSi == true)
                    {
                        tab.medioAmbiente.cloacas = true;
                    }
                    else
                    {
                        tab.medioAmbiente.cloacas = false;
                    }
                }

                #endregion
            }
            else
            {
                context.catHCAdulto.AddObject(new catHCAdulto() { pacid = pacId, Fecha = DateTime.Now, hcaduFamiligrama = "" });
                context.SaveChanges();

                long id = context.catHCAdulto.Max(item => item.hcaduid);

                #region Antecedentes familiares 

                tab.antecedentesFamiliares = new catHCAduAnteFamiliarWS();
                tab.antecedentesFamiliares.hcaduid = id;

                #endregion 

                #region Antecedentes médicos

                tab.antecedentesMedicos = new catHCAduAnteMedicoWS();

                #endregion

                #region Antecedentes Ginecologicos 

                tab.antecedentesGinecologicos = new catHCAduAnteGinecoWS();

                #endregion 

                #region Habitos 

                tab.habitos = new catHCAduHabitosWS();

                #endregion 

                #region Situacion Psicologica

                #endregion

                #region Situacion Psicologica

                tab.psicosocial = new catHCAduSituacionPsicoWS();

                #endregion

                #region Medio Ambiente

                tab.medioAmbiente = new catHCAduMedioAmbienteWS();

                #endregion 
            }

            return View(tab);
        }

        [GridAction]
        public ActionResult ConsultasHistorialFamiliarYPersonal(int pacId)
        {
            var paciente = context.catHCAdulto.FirstOrDefault(item => item.pacid == pacId);

            catHCAdultoWS adu = new catHCAdultoWS()
            {
                pacid = paciente.pacid,
                hcaduid = paciente.hcaduid,
                Fecha = paciente.Fecha,
                Familigrama = paciente.hcaduFamiligrama,
                FechaFamiligrama = paciente.hcaduFechaFamiligrama
            };
            return View(adu);
        }

        [GridAction]
        public ActionResult hcPacienteAdultoCuidadosPreventivos(int pacId)
        {
            var paciente = context.catHCAdulto.FirstOrDefault(item => item.pacid == pacId);

            catHCAdultoWS adu = new catHCAdultoWS()
            {
                pacid = paciente.pacid,
                hcaduid = paciente.hcaduid,
                Fecha = paciente.Fecha,
                Familigrama = paciente.hcaduFamiligrama,
                FechaFamiligrama = paciente.hcaduFechaFamiligrama
            };
            return View(adu);
        }

        [GridAction]
        public ActionResult hcPacienteAdultoProblemaCronico(int pacId)
        {
            var paciente = context.catHCAdulto.FirstOrDefault(item => item.pacid == pacId);

            catHCAdultoWS adu = new catHCAdultoWS()
            {
                pacid = paciente.pacid,
                hcaduid = paciente.hcaduid,
                Fecha = paciente.Fecha,
                Familigrama = paciente.hcaduFamiligrama,
                FechaFamiligrama = paciente.hcaduFechaFamiligrama
            };
            return View(adu);
        }

        [GridAction]
        public ActionResult hcPacienteAdultoProblemaTransitorio(int pacId)
        {
            var paciente = context.catHCAdulto.FirstOrDefault(item => item.pacid == pacId);

            catHCAdultoWS adu = new catHCAdultoWS()
            {
                pacid = paciente.pacid,
                hcaduid = paciente.hcaduid,
                Fecha = paciente.Fecha,
                Familigrama = paciente.hcaduFamiligrama,
                FechaFamiligrama = paciente.hcaduFechaFamiligrama
            };
            return View(adu);
        }

        [GridAction]
        public ActionResult getHCListaProblemasCronicos(int hcaduid)
        {
            var _Datos = (from x in context.catHCAduProCronicos
                          where x.hcaduid == hcaduid
                          select new catHCAduProCronicosWS()
                          {
                              hcaduid = x.hcaduid,
                              aduCRFecha = x.aduCRFecha,
                              aduCRProblema = x.aduCRProblema,
                              aduCRCodigo = x.aduCRCodigo,
                              aduCRMedi = x.aduCRMedi,
                              usrId = x.usrId,
                              aduCRId = x.aduCRId
                          });

            return PartialView(new GridModel(_Datos));
        }

        [GridAction]
        public ActionResult getHistoriaClinicaProblemasCronicos(int id, long phcaduid)
        {
            var _Datos = (from x in context.catHCAduProCronicos
                          where x.aduCRId == id
                          select new catHCAduProCronicosWS()
                          {
                              hcaduid = x.hcaduid,
                              aduCRFecha = x.aduCRFecha,
                              aduCRProblema = x.aduCRProblema,
                              aduCRCodigo = x.aduCRCodigo,
                              aduCRMedi = x.aduCRMedi,
                              usrId = x.usrId,
                              aduCRId = x.aduCRId
                          }).FirstOrDefault();

            ViewData["aduCRProblema_Data"] = context.catHCProblemaCronico.Select(s => s.pcroDescripcion);
            ViewData["aduCRMedi_Data"] = context.catHCVademecum.Select(s => s.vadDescripcion);

            if (_Datos == null)
            {
                _Datos = new catHCAduProCronicosWS()
                {
                    hcaduid = phcaduid
                };
            }

            return View(_Datos);
        }

        [GridAction]
        public ActionResult setHistoriaClinicaProblemasCronicos(catHCAduProCronicosWS popUp)
        {
            try
            {
                popUp.aduCRProblema = popUp.aduCRProblema ?? "";
                popUp.aduCRMedi = popUp.aduCRMedi ?? "";
                if (popUp.aduCRProblema.Trim().Length == 0)
                {
                    return Json(new
                    {
                        Error = true,
                        Mensaje = "Ingrese un problema.",
                        Foco = "aduCRProblema"
                    });
                }

                if (context.catHCProblemaCronico.Count(c => c.pcroDescripcion == popUp.aduCRProblema) == 0)
                {
                    return Json(new
                    {
                        Error = true,
                        Mensaje = "El Problema ingresado no existe.",
                        Foco = "aduCRProblema"
                    });
                }
                if (popUp.aduCRMedi.Trim().Length == 0)
                {
                    return Json(new
                    {
                        Error = true,
                        Mensaje = "Ingrese la medicación.",
                        Foco = "aduCRMedi"
                    });
                }
                if (context.catHCVademecum.Count(c => c.vadDescripcion == popUp.aduCRMedi) == 0)
                {
                    return Json(new
                    {
                        Error = true,
                        Mensaje = "La medicación ingresada no existe.",
                        Foco = "aduCRMedi"
                    });
                }

                if (context.catHCAduProCronicos.Count(c => c.hcaduid == popUp.hcaduid && c.aduCRProblema == popUp.aduCRProblema && c.aduCRMedi == popUp.aduCRMedi) > 0)
                {
                    return Json(new
                    {
                        Error = true,
                        Mensaje = "El problema + la medicación ya existe.",
                        Foco = "aduCRMedi"
                    });
                }

                var Usuario = (Session["Usuario"] as sisUsuario).usrId;

                catHCAduProCronicos insert = new catHCAduProCronicos()
                {
                    hcaduid = popUp.hcaduid,
                    aduCRFecha = popUp.aduCRFecha,
                    aduCRProblema = popUp.aduCRProblema,
                    aduCRCodigo = context.catHCProblemaCronico.First(c => c.pcroDescripcion == popUp.aduCRProblema).pcroCodigo.ToString(),
                    aduCRMedi = popUp.aduCRMedi,
                    usrId = Usuario
                };

                context.catHCAduProCronicos.AddObject(insert);
                context.SaveChanges();

                return Json(new
                {
                    Error = false,//!infoProceso.IsValid,
                    Mensaje = "",//infoProceso.Mensaje,
                    Foco = "hcperid"
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Error = true,
                    Mensaje = ex.InnerException == null ? ex.Message : ex.InnerException.Message,
                    Foco = "hcperid"
                });
            }
        }

        [GridAction]
        public ActionResult getHCListaProblemasTransitorios(int hcaduid)
        {
            var _Datos = (from x in context.catHCAduProTransitorios
                          where x.hcaduid == hcaduid
                          select new catHCAduProTransitoriosWS()
                          {
                              hcaduid = x.hcaduid,
                              aduTRId = x.aduTRId,
                              aduTRFecha = x.aduTRFecha,
                              aduTRProblema = x.aduTRProblema,
                              aduTRCodigo = x.aduTRCodigo,
                              aduTRMedi = x.aduTRMedi,
                              usrId = x.usrId
                          });

            return PartialView(new GridModel(_Datos));
        }

        [GridAction]
        public ActionResult getHistoriaClinicaProblemasTransitorios(int id, long phcaduid)
        {
            var _Datos = (from x in context.catHCAduProTransitorios
                          where x.aduTRId == id
                          select new catHCAduProTransitoriosWS()
                          {
                              hcaduid = x.hcaduid,
                              aduTRFecha = x.aduTRFecha,
                              aduTRProblema = x.aduTRProblema,
                              aduTRCodigo = x.aduTRCodigo,
                              aduTRMedi = x.aduTRMedi,
                              usrId = x.usrId,
                              aduTRId = x.aduTRId
                          }).FirstOrDefault();

            if (_Datos == null)
            {
                _Datos = new catHCAduProTransitoriosWS()
                {
                    hcaduid = phcaduid
                };
            }

            return View(_Datos);
        }

        [GridAction]
        public ActionResult setHistoriaClinicaProblemasTransitorios(catHCAduProTransitoriosWS popUp)
        {
            try
            {
                if (context.catHCAduProTransitorios.Where(c => c.hcaduid == popUp.hcaduid && c.aduTRProblema == popUp.aduTRProblema && c.aduTRMedi == popUp.aduTRMedi).ToList().Count(c => c.aduTRFecha.Value.Date == popUp.aduTRFecha.Value.Date) > 0)
                {
                    return Json(new
                    {
                        Error = true,
                        Mensaje = "El problema más la medicación ya existe en la misma fecha.",
                        Foco = "aduTRFecha"
                    });
                }

                var Usuario = (Session["Usuario"] as sisUsuario).usrId;

                catHCAduProTransitorios insert = new catHCAduProTransitorios()
                {
                    hcaduid = popUp.hcaduid,
                    aduTRFecha = popUp.aduTRFecha,
                    aduTRProblema = popUp.aduTRProblema,
                    aduTRCodigo = popUp.aduTRCodigo,
                    aduTRMedi = popUp.aduTRMedi,
                    usrId = Usuario
                };

                context.catHCAduProTransitorios.AddObject(insert);
                context.SaveChanges();

                return Json(new
                {
                    Error = false,//!infoProceso.IsValid,
                    Mensaje = "",//infoProceso.Mensaje,
                    Foco = "hcperid"
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Error = true,
                    Mensaje = ex.InnerException == null ? ex.Message : ex.InnerException.Message,
                    Foco = "hcperid"
                });
            }
        }

        [GridAction]
        public ActionResult ConsultasHistoriaCuidadosPreventivos(int pacId)
        {
            var paciente = context.catHCAdulto.First(item => item.pacid == pacId);

            var preventivo = (from p in context.getHCCuidadosPreventivos((int)paciente.hcaduid).ToList()
                              select new catHCAduPraPreventivaResulWS()
                                    {
                                        hcaduid = (int)p.hcaduid,
                                        aduPraPrevDescri = p.aduPraPrevDescri,
                                        aduPraPrevResId = p.aduPraPrevResId,
                                        aduPraPrevId = p.aduPraPrevId,
                                        usrId = p.usrId,
                                        aduPraPrevFecha = p.aduPraPrevFecha,
                                        aduPraPrevResultado = p.aduPraPrevResultado,
                                        Usuario = p.usrApellidoyNombre,
                                        Cantidad = p.Cantidad
                                    }).ToList();
            return View(preventivo);
        }

        [GridAction]
        public ActionResult ConsultasHistoriaExamenFisico(int pacId)
        {
            catHCAduEXAFISICOWS fisico = new catHCAduEXAFISICOWS();

            var paciente = context.catHCAdulto.Where(item => item.pacid == pacId).FirstOrDefault();

            if (paciente != null)
            {
                fisico = (from fi in context.catHCAduEXAFISICO
                          where fi.hcaduid == paciente.hcaduid
                          select new catHCAduEXAFISICOWS()
                          {
                              hcaduid = paciente.hcaduid,
                              aduFILentes = fi.aduFILentesSi == true ? true : false,
                              aduFITA = fi.aduFITA,
                              aduFICC = fi.aduFICC,
                              aduFIPeso = fi.aduFIPeso,
                              aduFITalla = fi.aduFITalla,
                              aduFIIMC = fi.aduFIIMC,
                              aduFIFC = fi.aduFIFC,
                              aduFIPiel = (bool)fi.aduFIPiel,
                              aduFILentesNo = (bool)fi.aduFILentesNo,
                              aduFILentesSi = (bool)fi.aduFILentesSi,
                              aduFIOidos = (bool)fi.aduFIOidos,
                              aduFIDentadura = (bool)fi.aduFIDentadura,
                              aduFIPulmones = (bool)fi.aduFIPulmones,
                              aduFICorazon = (bool)fi.aduFICorazon,
                              aduFIAbdomen = (bool)fi.aduFIAbdomen,
                              aduFIGenitales = (bool)fi.aduFIGenitales,
                              aduFIMamas = (bool)fi.aduFIMamas,
                              aduFIOsteo = (bool)fi.aduFIOsteo
                          }).FirstOrDefault();

                fisico.patologicas = (from p in context.catHCAduPraPatologicasResul
                                     where p.hcaduid == paciente.hcaduid
                                     select new catHCAduPraPatologicasResulWS()
                                     {
                                         hcaduid = paciente.hcaduid,
                                         aduPraPatId = p.aduPraPatId,
                                         aduPraPatResId = p.aduPraPatResId,
                                         aduPraPatDescri = p.catHCAduPraPatologicas.aduPraPatDescri,
                                         aduPraPatFecha1 = p.aduPraPatFecha1,
                                         aduPraPatFecha2 = p.aduPraPatFecha2,
                                         aduPraPatFecha3 = p.aduPraPatFecha3,
                                         aduPraPatFecha4 = p.aduPraPatFecha4,
                                         aduPraPatFecha5 = p.aduPraPatFecha5,
                                         aduPraPatResul1 = p.aduPraPatResul1,
                                         aduPraPatResul2 = p.aduPraPatResul2,
                                         aduPraPatResul3 = p.aduPraPatResul3,
                                         aduPraPatResul4 = p.aduPraPatResul4,
                                         aduPraPatResul5 = p.aduPraPatResul5
                                     }).ToArray();

                if (fisico.patologicas.Count() == 0)
                {
                    var pa_ = (from p_ in context.catHCAduPraPatologicas
                               select p_).ToList();

                    foreach (var x in pa_)
                    {
                        catHCAduPraPatologicasResul insert = new catHCAduPraPatologicasResul()
                        {
                            hcaduid = paciente.hcaduid,
                            aduPraPatId = x.aduPraPatId,
                            usrId = (Session["Usuario"] as sisUsuario).usrId
                        };

                        context.catHCAduPraPatologicasResul.AddObject(insert);
                    }
                    context.SaveChanges();

                    fisico.patologicas = (from p in context.catHCAduPraPatologicasResul
                                          where p.hcaduid == paciente.hcaduid
                                          select new catHCAduPraPatologicasResulWS()
                                          {
                                              aduPraPatResId = p.aduPraPatResId,
                                              hcaduid = paciente.hcaduid,
                                              aduPraPatId = p.aduPraPatId,
                                              aduPraPatDescri = p.catHCAduPraPatologicas.aduPraPatDescri,
                                              aduPraPatFecha1 = p.aduPraPatFecha1,
                                              aduPraPatFecha2 = p.aduPraPatFecha2,
                                              aduPraPatFecha3 = p.aduPraPatFecha3,
                                              aduPraPatFecha4 = p.aduPraPatFecha4,
                                              aduPraPatFecha5 = p.aduPraPatFecha5,
                                              aduPraPatResul1 = p.aduPraPatResul1,
                                              aduPraPatResul2 = p.aduPraPatResul2,
                                              aduPraPatResul3 = p.aduPraPatResul3,
                                              aduPraPatResul4 = p.aduPraPatResul4,
                                              aduPraPatResul5 = p.aduPraPatResul5
                                          }).ToArray();
                }
                            

                if (fisico == null)
                {
                    fisico = new catHCAduEXAFISICOWS();
                    fisico.aduFILentes = false;
                    fisico.hcaduid = paciente.hcaduid;
                    var pa_ = (from p_ in context.catHCAduPraPatologicas
                               select p_).ToList();

                    foreach (var x in pa_)
                    {
                        catHCAduPraPatologicasResul insert = new catHCAduPraPatologicasResul()
                        {
                            hcaduid = paciente.hcaduid,
                            aduPraPatId = x.aduPraPatId,
                            usrId = (Session["Usuario"] as sisUsuario).usrId
                        };

                        context.catHCAduPraPatologicasResul.AddObject(insert);
                    }
                    context.SaveChanges();

                    fisico.patologicas = (from p in context.catHCAduPraPatologicasResul
                                          where p.hcaduid == paciente.hcaduid
                                          select new catHCAduPraPatologicasResulWS()
                                          {
                                              aduPraPatResId = p.aduPraPatResId,
                                              hcaduid = paciente.hcaduid,
                                              aduPraPatId = p.aduPraPatId,
                                              aduPraPatDescri = p.catHCAduPraPatologicas.aduPraPatDescri,
                                              aduPraPatFecha1 = p.aduPraPatFecha1,
                                              aduPraPatFecha2 = p.aduPraPatFecha2,
                                              aduPraPatFecha3 = p.aduPraPatFecha3,
                                              aduPraPatFecha4 = p.aduPraPatFecha4,
                                              aduPraPatFecha5 = p.aduPraPatFecha5,
                                              aduPraPatResul1 = p.aduPraPatResul1,
                                              aduPraPatResul2 = p.aduPraPatResul2,
                                              aduPraPatResul3 = p.aduPraPatResul3,
                                              aduPraPatResul4 = p.aduPraPatResul4,
                                              aduPraPatResul5 = p.aduPraPatResul5
                                          }).ToArray();
                }
                else
                {
                    fisico.aduFILentes = fisico.aduFILentesSi == true ? true : false;
                }
            }
            else
            {
                fisico.aduFILentes = false;
                fisico.hcaduid = paciente.hcaduid;
                var pa_ = (from p_ in context.catHCAduPraPatologicas
                           select p_).ToList();

                foreach (var x in pa_)
                {
                    catHCAduPraPatologicasResul insert = new catHCAduPraPatologicasResul()
                    {
                        hcaduid = paciente.hcaduid,
                        aduPraPatId = x.aduPraPatId,
                        usrId = (Session["Usuario"] as sisUsuario).usrId
                    };

                    context.catHCAduPraPatologicasResul.AddObject(insert);
                }
                context.SaveChanges();

                fisico.patologicas = (from p in context.catHCAduPraPatologicasResul
                                      where p.hcaduid == paciente.hcaduid
                                      select new catHCAduPraPatologicasResulWS()
                                      {
                                          aduPraPatResId = p.aduPraPatResId,
                                          hcaduid = paciente.hcaduid,
                                          aduPraPatId = p.aduPraPatId,
                                          aduPraPatDescri = p.catHCAduPraPatologicas.aduPraPatDescri,
                                          aduPraPatFecha1 = p.aduPraPatFecha1,
                                          aduPraPatFecha2 = p.aduPraPatFecha2,
                                          aduPraPatFecha3 = p.aduPraPatFecha3,
                                          aduPraPatFecha4 = p.aduPraPatFecha4,
                                          aduPraPatFecha5 = p.aduPraPatFecha5,
                                          aduPraPatResul1 = p.aduPraPatResul1,
                                          aduPraPatResul2 = p.aduPraPatResul2,
                                          aduPraPatResul3 = p.aduPraPatResul3,
                                          aduPraPatResul4 = p.aduPraPatResul4,
                                          aduPraPatResul5 = p.aduPraPatResul5
                                      }).ToArray();
            }

            return View(fisico);
        } 
        
        [GridAction]
        public ActionResult ConsultasHistoriaEvolucion(int Id)
        {
            return View();
        }

        [GridAction]
        public ActionResult hcDocumentacion(int idPaciente)
        {
            return PartialView("hcDocumentacion");
        }

        [GridAction]
        public ActionResult hcPacienteDiagnosticos(int idPaciente)
        {
            ViewData["adId_Data"] = new SelectList(context.catDiagnosticoAgrupamiento.OrderBy(o => o.adId), "adId", "adDescripcion");

            return PartialView("hcPacienteDiagnosticos");
        }

        [GridAction]
        public ActionResult hcPacientePracticas(int idPaciente)
        {
            ViewData["nomId_Data"] = new SelectList(context.catNomenclador.OrderBy(o => o.nomId), "nomId", "nomDescripcion"); 
            
            return PartialView("hcPacientePracticas");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult getDiagnosticosPaciente(int turId)
        {
            var _Datos = new List<enlTurnosDiagnosticos>();
            int _pacId = 0;
            if (turId > 0)
            {
                _pacId = (int)context.catTurno.Single(s => s.turId == turId).pacId;
            }
            else
            {
                _pacId = turId*-1;
            }
            _Datos = (from d in context.enlTurnoDiagnostico
                      where d.catTurno.pacId == _pacId
                      orderby d.catTurno.turFecha descending
                      select new enlTurnosDiagnosticos()
                      {
                          tdId = d.tdId,
                          diagId = d.diagId,
                          turId = d.turId,
                          diagnosticoDescripcion = d.catDiagnosticoPadron.diagDescripcion,
                          diagnosticoTipo1_Nombre =
                              d.catDiagnosticoPadron.catDiagnosticoSubCapitulo.catDiagnosticoCapitulo.tdDescripcion,
                          diagnosticoTipo2_Nombre = d.catDiagnosticoPadron.catDiagnosticoSubCapitulo.subDescripcion,
                          Medico = d.catTurno.catAgendaHorario.catAgenda.perId != null ? d.catTurno.catAgendaHorario.catAgenda.catPersona.perApellidoyNombre : d.catTurno.catAgendaHorario.catAgenda.catPersonaContratados.conApellidoyNombre,
                          Especialidad = d.catTurno.catAgendaHorario.catAgenda.catEspecialidad.espNombre,
                          Fecha = d.catTurno.turFecha,
                          CentroDeSalud = d.catTurno.catCentroDeSalud.csNombre
                      }).ToList();

            return View(new GridModel(_Datos));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult getPracticasPaciente(int turId)
        {
            var _Datos = new List<enlTurnosPracticas>();
            int _pacId = 0;
            if (turId > 0)
            {
                _pacId = (int)context.catTurno.Single(s => s.turId == turId).pacId;
            }
            else
            {
                _pacId = turId * -1;
            }
            _Datos = (from d in context.enlTurnoPractica
                        where d.catTurno.pacId == _pacId
                        orderby d.catTurno.turFecha descending
                        select new enlTurnosPracticas()
                        {
                            pracId = d.pracId,
                            turId = d.turId,
                            PracticaDescripcion = d.catPractica.pracDescripcion,
                            NomencladorDescripcion = d.catPractica.catNomenclador.nomDescripcion,
                            turpracId = d.turpracId,
                            pracCosto = d.pracCosto,
                            pracUop = d.pracUop,
                            turpracCantidad = d.turpracCantidad,
                            Medico = d.catTurno.catAgendaHorario.catAgenda.perId != null ? d.catTurno.catAgendaHorario.catAgenda.catPersona.perApellidoyNombre : d.catTurno.catAgendaHorario.catAgenda.catPersonaContratados.conApellidoyNombre,
                            Especialidad = d.catTurno.catAgendaHorario.catAgenda.catEspecialidad.espNombre,
                            Fecha = d.catTurno.turFecha,
                            CentroDeSalud = d.catTurno.catCentroDeSalud.csNombre
                        }).ToList();

            return View(new GridModel(_Datos));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult setEvolucionAdulto(int IdPaciente, string Comentario, int idTurno)
        {
            try
            {
                //var HC = context.catHCAdulto.Single(w => w.pacid == IdPaciente);
                ////var HCSOAP = context.catHCAduSOAP.Single(w => w.hcaduid == IdPaciente);
                //context.catHCAduSOAP.AddObject(new catHCAduSOAP()
                //{
                //    hcaduid = HC.hcaduid,
                //    aduSOAPEvolucion = Comentario,
                //    aduSOAPFecha = DateTime.Now,
                //    usrId = (Session["Usuario"] as sisUsuario).usrId
                //});

                //context.SaveChanges();
                var sisUsuariosCentroDeSalud = Session["UsuarioCentroDeSalud"] as sisUsuariosCentroDeSalud;
                if (sisUsuariosCentroDeSalud != null)
                {
                    var userCsId = sisUsuariosCentroDeSalud.csId;
                    var userUsrId = (short?)sisUsuariosCentroDeSalud.usrId;
                    int? EnteroNulo = null;

                    catEvolucion newEntry = new catEvolucion
                    {
                        pacId = IdPaciente,
                        //espId = espId,
                        //perId = proId > 0 ? proId : null,
                        //conId = proId < 0 ? proId * -1 : null,
                        evoFecha = DateTime.Now,
                        evoDescripcion = Comentario,
                        csId = userCsId,
                        usrId = userUsrId,
                        turId = idTurno == 0 ? EnteroNulo : idTurno
                    };

                    context.catEvolucion.AddObject(newEntry);
                    context.SaveChanges();
                }
                else
                {
                    RedirectToAction("LogOff", "Account");
                }
            }
            catch (Exception ex)
            {
                return Json(new { IsValid = false, Mensaje = ex.InnerException == null ? ex.Message : ex.InnerException.Message });
            }

            return Json(new { IsValid = true, Mensaje = "OK" });
        }

        [GridAction]
        public ActionResult _getEvolucionHCAdulto(int idPaciente)
        {
            return View(new GridModel(AllEvolucionHCAdulto(idPaciente)));
        }

        private IList<catHCAduSOAPWS> AllEvolucionHCAdulto(int idPaciente)
        {
            return getEvolucionHCAdulto(idPaciente).ToList();
        }

        private IEnumerable<catHCAduSOAPWS> getEvolucionHCAdulto(int idPaciente)
        {
            var _Datos = (from d in context.catEvolucion
                          where d.pacId == idPaciente
                          orderby d.evoFecha descending
                          select new catHCAduSOAPWS()
                          {
                              usrId = d.usrId == null ? 0 : (short)d.usrId,
                              aduSOAPFecha = d.evoFecha,
                              Usuario = d.usrId == null ? 
                                          new sisUsuarios()
                                          {
                                              usrApellidoyNombre = ""
                                          } : new sisUsuarios()
                                          {
                                              usrApellidoyNombre = d.sisUsuario.usrApellidoyNombre
                                          },
                              aduSOAPEvolucion = d.evoDescripcion,
                              CentroDeSalud = d.catCentroDeSalud.csNombre
                          }).ToList();

            return _Datos.AsEnumerable();
        }

    }
}