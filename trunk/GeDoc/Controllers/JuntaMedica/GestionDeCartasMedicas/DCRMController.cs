using System.Data.Objects.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using GeDoc.Models.JuntaMedica.Modelo;

namespace GeDoc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Telerik.Web.Mvc;
    using GeDoc.Filters;
    using GeDoc.Models;

    public partial class DCRMController : Controller
    {
        private efAccesoADatosJMJuntaMedicaEntities context = new efAccesoADatosJMJuntaMedicaEntities();

        //Edición de datos

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult onBuscarAgente(string nroDocumento)
        {
            var Agente = context.getBusquedaAgenteByDocumento(nroDocumento).First();

            if (Agente.agtDNI == 0)
            {
                return Json(new
                {
                    Encontrado = false,
                    Datos = new
                    {
                        agtId = 0,
                        agtApellidoyNombre = "",
                        cmedId = 0,
                        cmedNumero = "",
                        cmedFecha = DateTime.Now,
                        FechaTexto = DateTime.Now.ToString("dd/MM/yyyy"),
                        Hora = DateTime.Now.ToString("HH:mm"),
                        turId = 0
                    }
                });
            }

            var CM = context.getCartaMedicaByAgente(Agente.agtId).First();

            return Json(new
            {
                Encontrado = true,
                Datos = new
                {
                    agtId = Agente.agtId,
                    agtApellidoyNombre = Agente.agtApellidoyNombre,
                    cmedId = CM.cmedId,
                    cmedNumero = CM.cmedNumero,
                    cmedFecha = CM.cmedFecha,
                    FechaTexto = CM.cmedFecha.ToString("dd/MM/yyyy"),
                    Hora = CM.cmedFecha.ToString("HH:mm"),
                    turId = CM.turId
                }
            });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult onBuscarAgenteJM(string nroDocumento)
        {
            var Agente = context.getBusquedaAgenteByDocumento(nroDocumento).First();

            if (Agente.agtDNI == 0)
            {
                return Json(new
                {
                    Encontrado = false,
                    Datos = new
                    {
                        agtId = 0,
                        agtApellidoyNombre = "",
                        cmedId = 0,
                        cmedNumero = "",
                        cmedFecha = DateTime.Now,
                        FechaTexto = DateTime.Now.ToString("dd/MM/yyyy"),
                        Hora = DateTime.Now.ToString("HH:mm"),
                        turId = 0
                    }
                });
            }

            var CM = context.getJuntaMedicaByAgente(Agente.agtId).First();

            return Json(new
            {
                Encontrado = true,
                Datos = new
                {
                    agtId = Agente.agtId,
                    agtApellidoyNombre = Agente.agtApellidoyNombre,
                    cmedId = CM.cmedId,
                    cmedFecha = CM.turFecha,
                    FechaTexto = CM.turFecha.ToString("dd/MM/yyyy"),
                    Hora = CM.turFecha.ToString("HH:mm"),
                    turId = CM.turId
                }
            });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult onBuscarAgenteCMAA(string nroDocumento, DateTime FechaLicencia)
        {
            var CM = context.getBusquedaCMByDocumentoByFecha(nroDocumento, FechaLicencia).First();

            if (CM.cmedId == 0)
            {
                return Json(new
                {
                    Encontrado = false,
                    Datos = new
                    {
                        agtId = 0,
                        agtApellidoyNombre = "",
                        cmedId = 0,
                        cmedNumero = "",
                        cmedFecha = DateTime.Now,
                        FechaTexto = DateTime.Now.ToString("dd/MM/yyyy"),
                        Hora = DateTime.Now.ToString("HH:mm"),
                        turId = 0,
                        estEstado = "",
                        EstadoCM = ""
                    }
                });
            }

            return Json(new
            {
                Encontrado = true,
                Datos = new
                {
                    agtId = CM.agtId,
                    agtApellidoyNombre = CM.agtApellidoNombre,
                    cmedId = CM.cmedId,
                    cmedFecha = CM.cmedFecha,
                    FechaTexto = CM.cmedFecha.ToString("dd/MM/yyyy"),
                    Hora = CM.cmedFecha.ToString("HH:mm"),
                    cmedNumero = CM.cmedNumero,
                    cmedMotivoSolicitud = CM.cmedMotivoSolicitud,
                    FechaLicencia = CM.licFechaInicial.Value.ToString("dd/MM/yyyy"),
                    espNombre = CM.espNombre,
                    licCantidad = CM.licCantidad,
                    Articulo = CM.Articulo,
                    estEstado = CM.estEstado,
                    EstadoCM = CM.EstadoCM
                }
            });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult onConfirmaCM(int agtId)
        {
            try
            {
                var CM = context.getCartaMedicaByAgente(agtId).First();

                //Si ya está creada la CM solo devuelvo los valores\\
                if (CM.cmedId > 0)
                {
                    return Json(new
                    {
                        Encontrado = true,
                        Datos = new
                        {
                            cmedId = CM.cmedId,
                            cmedNumero = CM.cmedNumero,
                            cmedFecha = CM.cmedFecha,
                            FechaTexto = CM.cmedFecha.ToString("dd/MM/yyyy"),
                            Hora = CM.cmedFecha.ToString("HH:mm"),
                            turId = CM.turId
                        }
                    });
                }

                //Registramos la CM
                var Usuario = "XX";
                if (Session["Usuario"] != null)
                {
                    Usuario = (Session["Usuario"] as sisUsuario).usrNombreDeUsuario;
                }
                var infoProceso = new MensajeDeProceso();
                infoProceso = context.setCartaMedica(agtId, -1, "", "", Usuario, "Insertar").First();
                if (!infoProceso.IsValid)
                {
                    return Json(new
                    {
                        Encontrado = false,
                        Datos = new
                        {
                            cmedId = 0,
                            cmedNumero = "",
                            cmedFecha = DateTime.Now,
                            FechaTexto = DateTime.Now.ToString("dd/MM/yyyy"),
                            Hora = DateTime.Now.ToString("HH:mm"),
                            turId = 0
                        }
                    });
                }

                //Cargamos la CM Generada
                CM = context.getCartaMedicaByAgente(agtId).First();

                return Json(new
                {
                    Encontrado = true,
                    Datos = new
                    {
                        cmedId = CM.cmedId,
                        cmedNumero = CM.cmedNumero,
                        cmedFecha = CM.cmedFecha,
                        FechaTexto = CM.cmedFecha.ToString("dd/MM/yyyy"),
                        Hora = CM.cmedFecha.ToString("HH:mm"),
                        turId = CM.turId
                    }
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Encontrado = false,
                    Datos = new
                    {
                        cmedId = 0,
                        cmedNumero = "",
                        cmedFecha = DateTime.Now,
                        FechaTexto = DateTime.Now.ToString("dd/MM/yyyy"),
                        Hora = DateTime.Now.ToString("HH:mm"),
                        turId = 0
                    }
                });
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult onConfirmaJM(int agtId, int tjId, string Especialidades)
        {
            try
            {
                var CM = context.getJuntaMedicaByAgente(agtId).First();

                //Si ya está creada la CM solo devuelvo los valores\\
                if (CM.cmedId > 0)
                {
                    return Json(new
                    {
                        Encontrado = true,
                        Datos = new
                        {
                            cmedId = CM.cmedId,
                            cmedFecha = CM.turFecha,
                            FechaTexto = CM.turFecha.ToString("dd/MM/yyyy"),
                            Hora = CM.turFecha.ToString("HH:mm"),
                            turId = CM.turId
                        }
                    });
                }

                //Registramos la CM
                var Usuario = "XX";
                if (Session["Usuario"] != null)
                {
                    Usuario = (Session["Usuario"] as sisUsuario).usrNombreDeUsuario;
                }
                var infoProceso = new MensajeDeProceso();
                infoProceso = context.setJuntaMedica(null, (short)tjId, agtId, "", "", DateTime.Now, Usuario, Especialidades, false, "Insertar").First();
                if (!infoProceso.IsValid)
                {
                    return Json(new
                    {
                        Encontrado = false,
                        Datos = new
                        {
                            cmedId = 0,
                            cmedNumero = "",
                            cmedFecha = DateTime.Now,
                            FechaTexto = DateTime.Now.ToString("dd/MM/yyyy"),
                            Hora = DateTime.Now.ToString("HH:mm"),
                            turId = 0
                        }
                    });
                }

                //Cargamos la CM Generada
                CM = context.getJuntaMedicaByAgente(agtId).First();

                return Json(new
                {
                    Encontrado = true,
                    Datos = new
                    {
                        cmedId = CM.cmedId,
                        cmedFecha = CM.turFecha,
                        FechaTexto = CM.turFecha.ToString("dd/MM/yyyy"),
                        Hora = CM.turFecha.ToString("HH:mm"),
                        turId = CM.turId
                    }
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Encontrado = false,
                    Datos = new
                    {
                        cmedId = 0,
                        cmedNumero = "",
                        cmedFecha = DateTime.Now,
                        FechaTexto = DateTime.Now.ToString("dd/MM/yyyy"),
                        Hora = DateTime.Now.ToString("HH:mm"),
                        turId = 0
                    }
                });
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult onConfirmaTurnoJM(int agtId, int tjId, string Especialidades, DateTime pFecha)
        {
            try
            {
                var CM = context.getJuntaMedicaByAgente(agtId).First();

                //Si ya está creada la CM solo devuelvo los valores\\
                if (CM.cmedId > 0)
                {
                    return Json(new
                    {
                        Encontrado = true,
                        Datos = new
                        {
                            cmedId = CM.cmedId,
                            cmedFecha = CM.turFecha,
                            FechaTexto = CM.turFecha.ToString("dd/MM/yyyy"),
                            Hora = CM.turFecha.ToString("HH:mm"),
                            turId = CM.turId
                        },
                        Mensaje = "OK"
                    });
                }

                var Mensaje = context.ExecuteStoreQuery<string>("SELECT dbo.fnValidaDeterminacionFechaJM({0}, {1}) As Resultado", pFecha, Especialidades).First();
                if (Mensaje.Substring(0,2) != "OK")
                {
                    return Json(new
                    {
                        Encontrado = false,
                        Datos = new
                        {
                            cmedId = 0,
                            cmedNumero = "",
                            cmedFecha = DateTime.Now,
                            FechaTexto = DateTime.Now.ToString("dd/MM/yyyy"),
                            Hora = DateTime.Now.ToString("HH:mm"),
                            turId = CM.turId
                        },
                        Mensaje = Mensaje
                    });
                }

                //Registramos la CM
                pFecha = new DateTime(pFecha.Date.Year, pFecha.Date.Month, pFecha.Date.Day, int.Parse(Mensaje.Substring(2, 2)), int.Parse(Mensaje.Substring(5, 2)), int.Parse(Mensaje.Substring(8, 2)));
                var Usuario = "XX";
                if (Session["Usuario"] != null)
                {
                    Usuario = (Session["Usuario"] as sisUsuario).usrNombreDeUsuario;
                }
                var infoProceso = new MensajeDeProceso();
                infoProceso = context.setJuntaMedica(pFecha, (short)tjId, agtId, "", "", DateTime.Now, Usuario, Especialidades, false, "Insertar").First();
                if (!infoProceso.IsValid)
                {
                    return Json(new
                    {
                        Encontrado = false,
                        Datos = new
                        {
                            cmedId = 0,
                            cmedNumero = "",
                            cmedFecha = DateTime.Now,
                            FechaTexto = DateTime.Now.ToString("dd/MM/yyyy"),
                            Hora = DateTime.Now.ToString("HH:mm"),
                            turId = 0
                        },
                        Mensaje = infoProceso.Mensaje
                    });
                }

                //Cargamos la CM Generada
                CM = context.getJuntaMedicaByAgente(agtId).First();

                return Json(new
                {
                    Encontrado = true,
                    Datos = new
                    {
                        cmedId = CM.cmedId,
                        cmedFecha = CM.turFecha,
                        FechaTexto = CM.turFecha.ToString("dd/MM/yyyy"),
                        Hora = CM.turFecha.ToString("HH:mm"),
                        turId = CM.turId
                    },
                    Mensaje = "OK"
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Encontrado = false,
                    Datos = new
                    {
                        cmedId = 0,
                        cmedNumero = "",
                        cmedFecha = DateTime.Now,
                        FechaTexto = DateTime.Now.ToString("dd/MM/yyyy"),
                        Hora = DateTime.Now.ToString("HH:mm"),
                        turId = 0
                    },
                    Mensaje = ex.InnerException == null ? ex.Message : ex.InnerException.Message
                });
            }
        }

        public ActionResult SolicitudDeCartaMedica()
        {
            return View();
        }

        public ActionResult InformacionDeAgente()
        {
            return View();
        }

        public ActionResult getDatosCartaMedicaAA()
        {
            return PartialView("AltaAnulacionCartaMedica");
        }

        public ActionResult CRUDSolicitudDeTurnoJuntaMedica()
        {
            ViewBag.EspecialidadesJM = context.getDatosEspecialidades().ToList();

            ViewData["tjId_Lista_Data"] = (from x in context.getDatosTiposJuntaMedica()
                                           select new ListaDesplegableWS()
                                           {
                                               idLista = x.tjId,
                                               Texto = x.tjDescripcion
                                           }).ToList();

            return PartialView("CRUDSolicitudDeTurnoJuntaMedica");
        }

        public ActionResult SolicitudDeTurnoJuntaMedica()
        {
            ViewBag.EspecialidadesJM = context.getDatosEspecialidades().ToList();

            ViewData["tjId_Lista_Data"] = (from x in context.getDatosTiposJuntaMedica()
                                            select new ListaDesplegableWS()
                                            {
                                                idLista = x.tjId,
                                                Texto = x.tjDescripcion
                                            }).ToList();

            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult InformacionDeAgenteCRUD(int agtId, string DNI)
        {
            catAgenteWS DatosAgente = new catAgenteWS(); 
            var InfoAgente = context.catAgente.FirstOrDefault(w => w.agtId == agtId);
            if (InfoAgente != null)
            {
                DatosAgente.agtActividadFisica = InfoAgente.agtActividadFisica ?? false;
                DatosAgente.agtAlergias = InfoAgente.agtAlergias ?? false;
                DatosAgente.agtApellidoyNombre = InfoAgente.agtApellidoyNombre;
                DatosAgente.agtApellido = InfoAgente.agtApellido;
                DatosAgente.agtNombre = InfoAgente.agtNombre;
                DatosAgente.agtBarrio = InfoAgente.agtBarrio;
                DatosAgente.agtBlock = InfoAgente.agtBlock;
                DatosAgente.agtBlockDepto = InfoAgente.agtBlockDepto;
                DatosAgente.agtBlockPiso = InfoAgente.agtBlockPiso;
                DatosAgente.agtCalle = InfoAgente.agtCalle;
                DatosAgente.agtCalleNumero = InfoAgente.agtCalleNumero;
                DatosAgente.agtCodigoPostal = InfoAgente.agtCodigoPostal;
                DatosAgente.agtColesterol = InfoAgente.agtColesterol ?? false;
                DatosAgente.agtCorreoElectronico = InfoAgente.agtCorreoElectronico;
                DatosAgente.agtCUIL = InfoAgente.agtCUIL;
                DatosAgente.agtDiabetes = InfoAgente.agtDiabetes ?? false;
                DatosAgente.agtDNI = InfoAgente.agtDNI;
                DatosAgente.agtEdad = InfoAgente.agtEdad;
                DatosAgente.agtFechaIngreso = InfoAgente.agtFechaIngreso;
                DatosAgente.agtFechaNacimiento = InfoAgente.agtFechaNacimiento;
                DatosAgente.agtFoto = InfoAgente.agtFoto;
                DatosAgente.agtFuma = InfoAgente.agtFuma ?? false;
                DatosAgente.agtHTA = InfoAgente.agtHTA ?? false;
                DatosAgente.agtId = InfoAgente.agtId;
                DatosAgente.agtLugarTrabajo = InfoAgente.agtLugarTrabajo;
                DatosAgente.agtManzana = InfoAgente.agtManzana;
                DatosAgente.agtOrigenInfOSP = InfoAgente.agtOrigenInfOSP;
                DatosAgente.agtPeso = InfoAgente.agtPeso;
                DatosAgente.agtSector = InfoAgente.agtSector;
                DatosAgente.agtSexo = String.IsNullOrEmpty(InfoAgente.agtSexo) ? "Masculino" : InfoAgente.agtSexo;
                DatosAgente.agtTalla = InfoAgente.agtTalla;
                DatosAgente.agtTelefonoFijo = InfoAgente.agtTelefonoFijo;
                DatosAgente.agtTelefonoMovil = InfoAgente.agtTelefonoMovil;
                DatosAgente.locId = InfoAgente.locId;
                DatosAgente.ospeId = InfoAgente.ospeId;
                DatosAgente.tarId = InfoAgente.tarId;
                DatosAgente.agtDomicilioReferencia = InfoAgente.agtDomicilioReferencia;
            }
            else
            {
                InfoAgente = new catAgente();
                DatosAgente.agtActividadFisica = false;
                DatosAgente.agtAlergias = false;
                DatosAgente.agtApellidoyNombre = "";
                DatosAgente.agtApellido = "";
                DatosAgente.agtNombre = "";
                DatosAgente.agtBarrio = "";
                DatosAgente.agtBlock = "";
                DatosAgente.agtBlockDepto = "";
                DatosAgente.agtBlockPiso = "";
                DatosAgente.agtCalle = "";
                //DatosAgente.agtCalleNumero = 0;
                DatosAgente.agtCodigoPostal = 5400;
                DatosAgente.agtColesterol = false;
                DatosAgente.agtCorreoElectronico = "";
                DatosAgente.agtCUIL = "";
                DatosAgente.agtDiabetes = false;
                DatosAgente.agtDNI = int.Parse(DNI);
                //DatosAgente.agtEdad = InfoAgente.agtEdad;
                DatosAgente.agtFechaIngreso = DateTime.Now;
                DatosAgente.agtFechaNacimiento = DateTime.Now.AddYears(-18);
                DatosAgente.agtFoto = "";
                DatosAgente.agtFuma = false;
                DatosAgente.agtHTA = false;
                DatosAgente.agtId = agtId;
                DatosAgente.agtLugarTrabajo = "";
                DatosAgente.agtManzana = "";
                DatosAgente.agtOrigenInfOSP = false;
                //DatosAgente.agtPeso = InfoAgente.agtPeso;
                DatosAgente.agtSector = "";
                DatosAgente.agtSexo = String.IsNullOrEmpty(InfoAgente.agtSexo) ? "Masculino" : "Femenino";
                //DatosAgente.agtTalla = InfoAgente.agtTalla;
                //DatosAgente.agtTelefonoFijo = InfoAgente.agtTelefonoFijo;
                //DatosAgente.agtTelefonoMovil = InfoAgente.agtTelefonoMovil;
                //DatosAgente.locId = InfoAgente.locId;
                //DatosAgente.ospeId = InfoAgente.ospeId;
                //DatosAgente.tarId = InfoAgente.tarId;
                DatosAgente.agtDomicilioReferencia = "";
            }
            DatosAgente.ListaGrupoFamiliar = (from x in context.getDatosAgenteGrupoFamiliar(agtId).ToList()
                                                         select new catAgenteGrupoFamiliarWS()
                                                         {
                                                             agtfId = x.agtfId,
                                                             agtfApellidoyNombre = x.agtfApellidoyNombre,
                                                             agtfDNI = x.agtfDNI,
                                                             ospvId = x.ospvId,
                                                             agtIdGF = agtId,
                                                             agtfActivo = true,
                                                             agtfAccion = "",
                                                             Vinculo = x.ospvId == "" ? "" : (x.ospvId == "001" ? "CONYUGE" : (x.ospvId == "002" ? "HIJO/A" : (x.ospvId == "005" ? "CONCUBINA/O" : "EX-CONYUGE")))
                                                         }).ToList();
            Session["ListadoGrupoFamiliarAgente"] = DatosAgente.ListaGrupoFamiliar;
            DatosAgente.ListaReparticiones = (from x in context.getDatosAgenteReparticiones(agtId).ToList()
                                              select new catAgenteReparticionWS()
                                              {
                                                  agtIdRep = x.agtId,
                                                  repId = x.repId,
                                                  repNombre = x.repNombre,
                                                  repCodigo = x.repCodigo,
                                                  minId = x.minId,
                                                  repActivo = true,
                                                  repAccion = "",
                                                  minNombre = x.minNombre
                                              }).ToList();
            Session["ListadoReparticionesAgente"] = DatosAgente.ListaReparticiones;

            ViewData["ospeId_Lista_Data"] = (from x in context.ospEstadoCivil.ToList()
                                           select new ListaDesplegableWS()
                                           {
                                               idLista = Encoding.ASCII.GetBytes(x.ospeId)[0],
                                               Texto = x.ospeDescripcion,
                                               Codigo = x.ospeId
                                           }).ToList();

            ViewData["tarId_Lista_Data"] = (from x in context.catTarea
                                             select new ListaDesplegableWS()
                                             {
                                                 idLista = x.tarId,
                                                 Texto = x.tarDescripcion
                                             }).ToList();

            var Sexo = new List<ListaDesplegableWS>();
            Sexo.Add(new ListaDesplegableWS(){ idLista = 1, Texto = "Masculino" });
            Sexo.Add(new ListaDesplegableWS() { idLista = 2, Texto = "Femenino" });

            ViewData["agtSexo_Lista_Data"] = Sexo;
            ViewData["agtCalle_Data"] = context.catCalle.Select(s => s.callNombre);

            ViewData["depId_Lista_Data"] = (from x in context.catDepartamento.Where(w => w.prvId == 17)
                                           select new ListaDesplegableWS()
                                           {
                                               idLista = x.depId,
                                               Texto = x.depNombre
                                           }).ToList();

            var Depto = InfoAgente.locId == null ? (ViewData["depId_Lista_Data"] as List<ListaDesplegableWS>).First().idLista : InfoAgente.catLocalidad.depId;

            DatosAgente.depId = Depto;

            ViewData["locId_Lista_Data"] = (from x in context.catLocalidad.Where(w => w.depId == Depto)
                                            select new ListaDesplegableWS()
                                            {
                                                idLista = x.locId,
                                                Texto = x.locNombre
                                            }).ToList();

            return PartialView("InformacionDeAgenteCRUD", DatosAgente);
        }

        [GridAction]
        public ActionResult getListadoDeGrupoFamiliar()
        {
            return View(new GridModel<catAgenteGrupoFamiliarWS>
            {
                Data = (Session["ListadoGrupoFamiliarAgente"] as List<catAgenteGrupoFamiliarWS>).Where(w => w.agtfActivo).ToList()
            });
        }

        [GridAction]
        public ActionResult getListadoDeReparticiones()
        {
            return View(new GridModel<catAgenteReparticionWS>
            {
                Data = (Session["ListadoReparticionesAgente"] as List<catAgenteReparticionWS>).Where(w => w.repActivo).ToList()
            });
        }

        [GridAction]
        public ActionResult getGrupoFamiliar(string pAccion, int? agtfDNI)
        {
            var Datos = new catAgenteGrupoFamiliarWS();
            Datos.agtfAccion = pAccion;
            Datos.agtfDNI = (int)agtfDNI;

            if (pAccion != "Agregar")
            {
                //Cargamos los datos
                var DatosActuales = (Session["ListadoGrupoFamiliarAgente"] as List<catAgenteGrupoFamiliarWS>).Single(w => w.agtfDNI == agtfDNI);

                Datos.agtfId = DatosActuales.agtfId;
                Datos.agtfApellidoyNombre = DatosActuales.agtfApellidoyNombre;
                Datos.ospvId = DatosActuales.ospvId;
            }
            else
            {
                Datos.ospvId = "001";
            }

            return PartialView("CRUDGrupoFamiliarAgente", Datos);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult setAgente(catAgenteWS Datos)
        {
            try
            {
                var Registro = new catAgente();

                //Actualizamos los datos personales del paciente\\
                if (Datos.agtId > 0)
                {
                    Registro = context.catAgente.Single(s => s.agtId == Datos.agtId);
                }

                Datos.agtBlockDepto = Datos.agtBlockDepto ?? "";
                Datos.agtBlockPiso = Datos.agtBlockPiso ?? "";

                Registro.agtApellido = Datos.agtApellido.ToUpper();
                Registro.agtNombre = Datos.agtNombre.ToUpper();
                Registro.agtApellidoyNombre = Registro.agtApellido + " " + Registro.agtNombre;
                Registro.agtCalle = Datos.agtCalle.ToUpper();
                Registro.agtLugarTrabajo = Datos.agtLugarTrabajo.ToUpper();
                Registro.agtDomicilioReferencia = Datos.agtDomicilioReferencia.ToUpper();
                Registro.agtBlockDepto = Datos.agtBlockDepto.ToUpper();
                Registro.agtBlockPiso = Datos.agtBlockPiso.ToUpper();
                Registro.agtDNI = Datos.agtDNI;

                Registro.agtCUIL = Datos.agtCUIL;
                Registro.ospeId = Datos.ospeId;
                Registro.agtFechaNacimiento = Datos.agtFechaNacimiento;
                Registro.agtEdad = (byte)Datos.agtEdad;
                Registro.tarId = Datos.tarId;
                Registro.agtFechaIngreso = Datos.agtFechaIngreso;
                Registro.agtCalleNumero = (short)Datos.agtCalleNumero;
                Registro.agtBlock = Datos.agtBlock;
                Registro.agtBlockPiso = Datos.agtBlockPiso;
                Registro.agtBlockDepto = Datos.agtBlockDepto;
                Registro.agtBarrio = Datos.agtBarrio.ToUpper();
                Registro.agtManzana = "";
                Registro.agtSector = "";
                Registro.agtCodigoPostal = (short)Datos.agtCodigoPostal;
                Registro.locId = Datos.locId;
                Registro.agtOrigenInfOSP = false;
                Registro.agtFoto = Datos.agtFoto;
                Registro.agtSexo = Datos.agtSexo;
                Registro.agtFuma = Datos.agtFuma;
                Registro.agtHTA = Datos.agtHTA;
                Registro.agtColesterol = Datos.agtColesterol;
                Registro.agtDiabetes = Datos.agtDiabetes;
                Registro.agtActividadFisica = Datos.agtActividadFisica;
                Registro.agtAlergias = Datos.agtAlergias;
                Registro.agtPeso = Datos.agtPeso;
                Registro.agtTalla = Datos.agtTalla;
                Registro.agtTelefonoFijo = Datos.agtTelefonoFijo;
                Registro.agtTelefonoMovil = Datos.agtTelefonoMovil;
                Registro.agtCorreoElectronico = Datos.agtCorreoElectronico;

                if (Datos.agtFoto != null)
                {
                    byte[] data = Convert.FromBase64String(Datos.agtFoto);
                    MemoryStream ms = new MemoryStream(data);
                    Image returnImage = Image.FromStream(ms);
                    var NombreImagen = Datos.agtDNI + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".png";
                    string Archivo = Path.Combine(Server.MapPath("~/Content/Fotos/Agentes"), NombreImagen);
                    returnImage.Save(Archivo, ImageFormat.Png);
                    Registro.agtFoto = "~/Content/Fotos/Agentes/" + NombreImagen;
                }

                if (Datos.agtId <= 0)
                {
                    context.catAgente.AddObject(Registro);
                }

                context.SaveChanges();

                //Ahora se guarda el grupo familiar, es importante entender que se hace así a proposito, ya que se desea
                //guardar hasta donde se pueda, osea que si se produce algún error a esta altura del código, es importante
                //que lo mismo quede guardado todos los datos del agente.
                if (Datos.agtId <= 0)
                {
                    var Agente = context.catAgente.Single(s => s.agtDNI == Datos.agtDNI);
                    Datos.agtId = Agente.agtId;
                }

                //Grupo Familiar
                //Primero se eliminan los registros marcados para eliminar
                foreach (var Item in (Session["ListadoGrupoFamiliarAgente"] as List<catAgenteGrupoFamiliarWS>).Where(w => !w.agtfActivo))
                {
                    var EliminarGF = context.catAgenteGrupoFamiliar.FirstOrDefault(f => f.agtfDNI == Item.agtfDNI && f.agtId == Datos.agtId);
                    if (EliminarGF != null)
                    {
                        context.catAgenteGrupoFamiliar.DeleteObject(EliminarGF);
                        context.SaveChanges();
                    }
                }
                //Ahora se guarda todo lo demás
                foreach (var Item in (Session["ListadoGrupoFamiliarAgente"] as List<catAgenteGrupoFamiliarWS>).Where(w => w.agtfActivo))
                {
                    var UpdateGF = context.catAgenteGrupoFamiliar.FirstOrDefault(f => f.agtfDNI == Item.agtfDNI && f.agtId == Datos.agtId);
                    if (UpdateGF != null)
                    {
                        UpdateGF.agtId = Datos.agtId;
                        UpdateGF.agtfDNI = Item.agtfDNI;
                        UpdateGF.agtfApellidoyNombre = Item.agtfApellidoyNombre;
                        UpdateGF.ospvId = Item.ospvId;
                    }
                    else
                    {
                        var AgregarGF = new catAgenteGrupoFamiliar();
                        AgregarGF.agtId = Datos.agtId;
                        AgregarGF.agtfDNI = Item.agtfDNI;
                        AgregarGF.agtfApellidoyNombre = Item.agtfApellidoyNombre;
                        AgregarGF.ospvId = Item.ospvId;

                        context.catAgenteGrupoFamiliar.AddObject(AgregarGF);
                    }
                    context.SaveChanges();
                }

                //Reparticiones
                //Primero se eliminan los registros marcados para eliminar
                foreach (var Item in (Session["ListadoReparticionesAgente"] as List<catAgenteReparticionWS>).Where(w => !w.repActivo))
                {
                    var EliminarRep = context.catAgenteReparticion.FirstOrDefault(f => f.repId == Item.repId && f.agtId == Datos.agtId);
                    if (EliminarRep != null)
                    {
                        context.catAgenteReparticion.DeleteObject(EliminarRep);
                        context.SaveChanges();
                    }
                }
                //Ahora se guarda todo lo demás
                foreach (var Item in (Session["ListadoReparticionesAgente"] as List<catAgenteReparticionWS>).Where(w => w.repActivo))
                {
                    var UpdateRep = context.catAgenteReparticion.FirstOrDefault(f => f.repId == Item.repId && f.agtId == Datos.agtId);
                    if (UpdateRep == null)
                    {
                        var AgregarRep = new catAgenteReparticion();
                        AgregarRep.agtId = Datos.agtId;
                        AgregarRep.repId = Item.repId;

                        context.catAgenteReparticion.AddObject(AgregarRep);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Error = true,
                    Mensaje = ex.InnerException == null ? ex.Message : ex.InnerException.Message,
                    Foco = "agtApellido"
                });
            }

            return Json(new
            {
                Error = false,
                Mensaje = "OK",
                Foco = "agtApellido"
            });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult setAgenteGrupoFamiliar(catAgenteGrupoFamiliarWS Datos)
        {
            if (Datos.agtfAccion == "Modificar" || Datos.agtfAccion == "Agregar")
            {
                Datos.agtfApellidoyNombre = Datos.agtfApellidoyNombre ?? "";
                if (Datos.agtfApellidoyNombre.Trim().Length < 0)
                {
                    return Json(new
                    {
                        Error = true,
                        Mensaje = "Debe ingresar el Apellido y Nombre.",
                        Foco = "agtfApellidoyNombre"
                    });
                }

                if (Datos.agtfDNI < 999999)
                {
                    return Json(new
                    {
                        Error = true,
                        Mensaje = "Número de documento incorrecto.",
                        Foco = "agtfDNI"
                    });
                }
            }

            try
            {
                switch (Datos.agtfAccion)
                {
                    case "Agregar":
                    case "Modificar":
                        var Registro = new catAgenteGrupoFamiliarWS();

                        //Actualizamos los datos personales del paciente\\
                        if (Datos.agtfAccion != "Agregar" || ((Session["ListadoGrupoFamiliarAgente"] as List<catAgenteGrupoFamiliarWS>).Any(s => s.agtfDNI == Datos.agtfDNI)))
                        {
                            Registro = (Session["ListadoGrupoFamiliarAgente"] as List<catAgenteGrupoFamiliarWS>).Single(s => s.agtfDNI == Datos.agtfDNI);
                        }

                        Registro.agtfApellidoyNombre = Datos.agtfApellidoyNombre.ToUpper();
                        Registro.agtfDNI = Datos.agtfDNI;
                        Registro.ospvId = Datos.ospvId;
                        Registro.agtIdGF = Datos.agtIdGF;
                        Registro.agtfActivo = true;
                        Registro.Vinculo = Datos.ospvId == "001"
                            ? "CONYUGE"
                            : (Datos.ospvId == "002" ? "HIJO/A" : (Datos.ospvId == "005" ? "CONCUBINA/O" : "EX-CONYUGE"));

                        if (Datos.agtfAccion == "Agregar" && (!(Session["ListadoGrupoFamiliarAgente"] as List<catAgenteGrupoFamiliarWS>).Any(s => s.agtfDNI == Datos.agtfDNI)))
                        {
                            (Session["ListadoGrupoFamiliarAgente"] as List<catAgenteGrupoFamiliarWS>).Add(Registro);
                        }
                        break;
                    case "Eliminar":
                        var DeleteRegistro = (Session["ListadoGrupoFamiliarAgente"] as List<catAgenteGrupoFamiliarWS>).Single(s => s.agtfDNI == Datos.agtfDNI);
                        //(Session["ListadoGrupoFamiliarAgente"] as List<catAgenteGrupoFamiliarWS>).Remove(DeleteRegistro);
                        DeleteRegistro.agtfActivo = false;
                        break;
                }

                return Json(new
                {
                    Error = false,
                    Mensaje = "Se actualizó información en forma correcta.",
                    Foco = "agtfApellidoyNombre"
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Error = true,
                    Mensaje = ex.InnerException == null ? ex.Message : ex.InnerException.Message,
                    Foco = "agtfApellidoyNombre"
                });
            }
        }

        /// <summary>
        /// Reparticiones 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [GridAction]
        public ActionResult getReparticionAgente(string pAccion, int? repId)
        {
            var Datos = new catAgenteReparticionWS();
            Datos.repAccion = pAccion;

            ViewData["minId_Lista_Data"] = (from x in context.catMinisterio
                                            select new ListaDesplegableWS()
                                            {
                                                idLista = x.minId,
                                                Texto = x.minNombre
                                            }).ToList();

            if (pAccion != "Agregar")
            {
                //Cargamos los datos
                var DatosActuales = (Session["ListadoReparticionesAgente"] as List<catAgenteReparticionWS>).Single(w => w.repId == repId);

                Datos.minId = DatosActuales.minId;
                Datos.repId = DatosActuales.repId;
            }
            else
            {
                Datos.minId = (ViewData["minId_Lista_Data"] as List<ListaDesplegableWS>).First().idLista;
            }

            ViewData["repId_Lista_Data"] = (from x in context.catReparticion.Where(w => w.minId == Datos.minId)
                                            select new ListaDesplegableWS()
                                            {
                                                idLista = x.repId,
                                                Texto = x.repNombre
                                            }).ToList();


            return PartialView("CRUDReparticionAgente", Datos);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult setAgenteReparticion(catAgenteReparticionWS Datos)
        {
            try
            {
                switch (Datos.repAccion)
                {
                    case "Agregar":
                    case "Modificar":
                        var Registro = new catAgenteReparticionWS();

                        //Actualizamos los datos personales del paciente\\
                        if (Datos.repAccion != "Agregar" || ((Session["ListadoReparticionesAgente"] as List<catAgenteReparticionWS>).Any(s => s.repId == Datos.repId)))
                        {
                            Registro = (Session["ListadoReparticionesAgente"] as List<catAgenteReparticionWS>).Single(s => s.repId == Datos.repId);
                        }

                        Registro.minId = Datos.minId;
                        if ((Datos.minNombre ?? "") == "")
                        {
                            Registro.minNombre = context.catMinisterio.Single(s => s.minId == Datos.minId).minNombre;
                        }
                        else
                        {
                            Registro.minNombre = Datos.minNombre;
                        }
                        Registro.repId = Datos.repId;
                        if ((Datos.repCodigo ?? "") == "")
                        {
                            var Reparticion = context.catReparticion.Single(s => s.repId == Datos.repId);
                            Registro.repNombre = Reparticion.repNombre;
                            Registro.repCodigo = Reparticion.repCodigo;
                        }
                        else
                        {
                            Registro.repNombre = Datos.repNombre;
                            Registro.repCodigo = Datos.repCodigo;
                        }
                        Registro.agtIdRep = Datos.agtIdRep;
                        Registro.repActivo = true;

                        if (Datos.repAccion == "Agregar" && (!(Session["ListadoReparticionesAgente"] as List<catAgenteReparticionWS>).Any(s => s.repId == Datos.repId)))
                        {
                            (Session["ListadoReparticionesAgente"] as List<catAgenteReparticionWS>).Add(Registro);
                        }
                        break;
                    case "Eliminar":
                        var DeleteRegistro = (Session["ListadoReparticionesAgente"] as List<catAgenteReparticionWS>).Single(s => s.repId == Datos.repId);
                        DeleteRegistro.repActivo = false;
                        break;
                }

                return Json(new
                {
                    Error = false,
                    Mensaje = "Se actualizó información en forma correcta.",
                    Foco = "minNombre"
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Error = true,
                    Mensaje = ex.InnerException == null ? ex.Message : ex.InnerException.Message,
                    Foco = "minNombre"
                });
            }
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
        public ActionResult ListaDeReparticiones(int minId)
        {
            var Datos = context.catReparticion.Where(w => w.minId == minId).ToList();

            string listaDesplegable = "<select id=\"repId_Lista\" class=\"repId_Lista\"" + (Datos.Count == 0 ? " disabled " : "") + " style=\"width: 434px;\" >";
            foreach (var Item in Datos)
            {
                listaDesplegable += "<option value=\"" + Item.repId + "\">(" + Item.repCodigo + ") " + Item.repNombre + "</option>";
            }
            if (!Datos.Any())
            {
                listaDesplegable += "<option value=\"-1\" class=\"ES-PlaceHolder\">Ningún Item seleccionado</option>";
            }
            listaDesplegable += "</select>";

            return Json(listaDesplegable);
        }

    }
}