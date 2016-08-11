using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using GeDoc.Filters;
using GeDoc.Models;
using Telerik.Web.Mvc;

namespace GeDoc.Controllers
{
//controlador de Reporte de total encuestados------------------------------------------------------------------------------------------------------------

        public partial class ReporteProturAvanceController : Controller
        {

            private readonly efAccesoADatosEntities context = new efAccesoADatosEntities();

            public ActionResult Index()
            {
                cargaComboEspecialidad(1);
                cargaComboCentro(1);
                cargaComboLocalidad(1);
                cargaComboDepartamento(1);

                return View();
            }

            [GridAction]
            public ActionResult _SelectEditing(string FechaDesde, string FechaHasta, string Localidad, string Departamento, string Especialidad, string CentroDeSalud)
            {
                return View(new GridModel(All(FechaDesde,FechaHasta,Localidad,Departamento,Especialidad,CentroDeSalud)));
            }

            private IList<spRptProturTotalEncuestados> All(string FechaDesde, string FechaHasta, string Localidad, string Departamento, string Especialidad, string CentroDeSalud)
            {
                return getDatos(FechaDesde, FechaHasta, Localidad, Departamento, Especialidad, CentroDeSalud).ToList();
            }

            private IEnumerable<spRptProturTotalEncuestados> getDatos(string FechaDesde, string FechaHasta, string Localidad, string Departamento, string Especialidad, string CentroDeSalud)
            {
                
                if (Session["UsuarioCentroDeSalud"] == null)
                {
                    RedirectToAction("LogOff", "Account");
                    return null;
                }

                    var _csId = (Session["UsuarioCentroDeSalud"] as GeDoc.sisUsuariosCentroDeSalud).csId;

                    var _Datos = (from d in context.spRptProturTotalEncuestados(FechaDesde, FechaHasta, Localidad, Departamento, Especialidad, CentroDeSalud).ToList()
                                  //where (_csId == 0 ? 0 : d.plaId) == _csId

                                  select new spRptProturTotalEncuestados()
                                  {
                                      cantidad_turnos = d.cantidad_turnos,
                                      cantidad_encuestados = d.cantidad_encuestados,
                                      Centro_de_salud = d.Centro_de_salud.ToString(),
                                      atentido = d.atendido,
                                      no_resueltos = d.no_resueltos,
                                      derivados = d.derivados,
                                      csId = d.csId.Value
                                  }).ToList();

                return _Datos.AsEnumerable();
            }


            //Datos para el dropdown de centros de salud
            protected void cargaComboCentro(int? Id = 0)
            {
                GeDoc.Models.efAccesoADatosEntities context = new efAccesoADatosEntities();

                List<SelectListItem> Centros = new List<SelectListItem>();
                var IdUsuario = (Session["Usuario"] as sisUsuario).usrId;
                var Query = (from p in context.catCentroDeSalud.ToList() 
                             join x in context.sisUsuarioCentroDeSalud on p.csId equals x.csId
                             where x.usrId == IdUsuario
                             select new catCentroDeSalud
                                  {
                                      csNombre = p.csNombre,
                                      csId = p.csId
                                  }).ToList();

                foreach (var item in Query)



                    Centros.Add(new  SelectListItem()
                    {
                        Value = item.csId.ToString(),
                        Text = item.csNombre.Length < 40 ? item.csNombre.ToString() : item.csNombre.Substring(0,40)+"...",
                        Selected = Id == item.csId ? true : false
                    });

                ViewData["Centros"] = Centros;
            }

         
            
            //carga de localidad
            [AcceptVerbs(HttpVerbs.Post)]
            public ActionResult cargaLocalidad(int id) {

                GeDoc.Models.efAccesoADatosEntities context = new efAccesoADatosEntities();

                var nombre="";
                var loc="{";
                foreach (var item in context.catLocalidad)
                {
                    if (item.depId == id)
                    {
                         nombre = item.locNombre.Length < 40 ? item.locNombre.ToString() : item.locNombre.Substring(0, 40) + "...";
                         loc +=  "\"" + item.locId.ToString()+ '"' + ":" + '"' +nombre.ToString() + "\",";
                    }
                }

                loc = loc.Trim();
                loc = loc.Remove(loc.Length - 1);

                loc+="}";


                return new JsonResult(){ Data = loc   };
            }

            private void cargaComboLocalidad(int? Id = 0)
            { 
                
                GeDoc.Models.efAccesoADatosEntities context = new efAccesoADatosEntities();
                
                List<SelectListItem> Localidad = new List<SelectListItem>();
                Localidad.Add(new SelectListItem() { Value = "0", Text = "Seleccione Localidad", Selected = false });

                foreach (var item in context.catLocalidad)
                {
                    if (item.depId == Id)
                    {
                        Localidad.Add(new SelectListItem()
                        {
                            Value = item.locId.ToString(),
                            Text = item.locNombre.Length < 40 ? item.locNombre.ToString() : item.locNombre.Substring(0, 40) + "...",
                            Selected = Id == item.locId ? true : false
                        });
                    }
                }
                    ViewData["Localidad"] = Localidad;
            }

            protected void cargaComboDepartamento(int? Id = 0)
            {

                GeDoc.Models.efAccesoADatosEntities context = new efAccesoADatosEntities();

                List<SelectListItem> Departamento = new List<SelectListItem>();
                Departamento.Add(new SelectListItem() { Value = "0", Text = "Seleccione Departamento", Selected = false });

                foreach (var item in context.catDepartamento)
                {
                    if (item.provId == 17)
                    {

                        Departamento.Add(new SelectListItem()
                        {
                            Value = item.depId.ToString(),
                            Text = item.depNombre.Length < 40 ? item.depNombre.ToString() : item.depNombre.Substring(0, 40) + "...",
                            Selected = Id == item.depId ? true : false

                        });
                    }
                }
                ViewData["Departamento"] = Departamento;
            }


            protected void cargaComboEspecialidad(int? Id = 0)
            {

                GeDoc.Models.efAccesoADatosEntities context = new efAccesoADatosEntities();

                List<SelectListItem> Especialidad = new List<SelectListItem>();
                Especialidad.Add(new SelectListItem() { Value = "0", Text = "Todas", Selected = false });

                foreach (var item in context.catEspecialidad)
                    Especialidad.Add(new SelectListItem()
                    {
                        Value = item.espId.ToString(),
                        Text = item.espNombre.Length < 40 ? item.espNombre.ToString() : item.espNombre.Substring(0, 40) + "...",
                        Selected = Id == item.espId ? true : false
                    });

                ViewData["Especialidad"] = Especialidad;
            }
        }
}

