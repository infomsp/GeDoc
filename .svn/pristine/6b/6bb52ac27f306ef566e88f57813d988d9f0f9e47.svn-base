namespace GeDoc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Telerik.Web.Mvc;
    using GeDoc.Models;

    public partial class sisLogUsuarioController : Controller
    {
        private efAccesoADatosEntities context = new efAccesoADatosEntities();

        public ActionResult Index()
        {
            string _usrId = All().First().usrNombreDeUsuario;
            ViewData["Grafico"] = AllEstadisticas(_usrId);
            ViewData["TopFive"] = TopFive();
            ViewData["sisUsuario"] = All();
            ViewData["sisLogUsuario"] = AllLog(_usrId);
            ViewData["idUsuario"] = _usrId;
            ViewBag.Catalogo = "Auditoría de Usuarios del Sistema";
            PopulateDropDownList();

            return PartialView();   
        }

        [GridAction]
        public ActionResult _SelectEditing()
        {
            return View(new GridModel(All()));
        }

        [GridAction]
        public ActionResult _SelectEditingLog(string idUsuario)
        {
            return View(new GridModel(AllLog(idUsuario).AsEnumerable()));
        }

        public IEnumerable<sisLogUsuarios> AllLog(string idUsuario)
        {
            var _Datos = (from d in context.sisLogUsuario.Where(w => w.sisUsuario.usrNombreDeUsuario == idUsuario && w.luDescripcion != "Ingreso al Sistema" && w.luDescripcion != "Salida del sistema").ToList()
                          select new sisLogUsuarios()
                          {
                              Fecha = d.luFecha,
                              FechaCorta = d.luFecha.Date,
                              Hora = d.luFecha.ToString("hh:mm"),
                              Operacion = d.luDescripcion == "" ? ((d.sisAccion == null ? "Consultar " : d.sisAccion.acDescripcion) + " " + d.sisMenu.mnuTitulo) : d.luDescripcion
                          }).OrderByDescending(o => o.Fecha).ToList();

            return _Datos.AsEnumerable();
        }

        public ActionResult RebindEstadisticas(string idUsuario)
        {
            List<GraficoLogUsuario> _Datos = new List<GraficoLogUsuario>();
            DateTime _Fecha = DateTime.Now.AddDays(-6);

            for (int _Dia = 0; _Dia < 7; _Dia++)
            {
                GraficoLogUsuario _UnDia = new GraficoLogUsuario();

                _UnDia.Fecha = _Fecha.AddDays(_Dia);
                _UnDia.Cantidad = (from d in context.sisLogUsuario.Where(w => w.sisUsuario.usrNombreDeUsuario == idUsuario).ToList()
                                   where d.luFecha.Date == _UnDia.Fecha.Date
                                   select d).Count();

                _Datos.Add(_UnDia);
            }

            ViewData["Grafico"] = _Datos.AsEnumerable();

            return Json(_Datos.AsEnumerable());
        }

        public IEnumerable<GraficoLogUsuario> AllEstadisticas(string idUsuario)
        {
            List<GraficoLogUsuario> _Datos = new List<GraficoLogUsuario>();
            DateTime _Fecha = DateTime.Now.AddDays(-6);

            for (int _Dia = 0; _Dia < 7; _Dia++)
            {
                GraficoLogUsuario _UnDia = new GraficoLogUsuario();

                _UnDia.Fecha = _Fecha.AddDays(_Dia);
                _UnDia.Cantidad = (from d in context.sisLogUsuario.Where(w => w.sisUsuario.usrNombreDeUsuario == idUsuario && w.luDescripcion != "Ingreso al Sistema" && w.luDescripcion != "Salida del sistema").ToList()
                                   where d.luFecha.Date == _UnDia.Fecha.Date
                                   select d).Count();

                _Datos.Add(_UnDia);
            }
            return _Datos.AsEnumerable();
        }

        public IEnumerable<GraficoLogUsuario> TopFive()
        {
            DateTime _Fecha = DateTime.Now.AddDays(-60).Date;

            var _Datos = (from d in context.sisLogUsuario
                          where d.luFecha >= _Fecha && d.luDescripcion != "Ingreso al Sistema" && d.luDescripcion != "Salida del sistema"
                               group d by d.sisUsuario.usrNombreDeUsuario into grupo
                               select new GraficoLogUsuario()
                               {
                                   Usuario = grupo.Key,
                                   Cantidad = grupo.Count()
                               }
                               ).ToList();

            _Datos = (from x in _Datos
                     orderby x.Cantidad descending, x.Usuario
                     select x).Take(5).ToList();

            return _Datos.AsEnumerable();
        }

        public ActionResult TopFiveOficinas(int ofiId)
        {
            DateTime _Fecha = DateTime.Now.AddDays(-60).Date;

            var _Datos = (from d in context.sisLogUsuario
                          join x in context.enlUsuarioOficina on d.usrId equals x.usrId
                          where d.luFecha >= _Fecha && d.luDescripcion != "Ingreso al Sistema" && d.luDescripcion != "Salida del sistema"
                                && x.ofiId == ofiId
                          group d by d.sisUsuario.usrNombreDeUsuario into grupo
                          select new GraficoLogUsuario()
                          {
                              Usuario = grupo.Key,
                              Cantidad = grupo.Count()
                          }
                               ).ToList();

            _Datos = (from x in _Datos
                      orderby x.Cantidad descending, x.Usuario
                      select x).Take(5).ToList();

            return Json(_Datos.AsEnumerable());
        }

        private IList<sisUsuarios> All()
        {
            return getDatos().ToList();
        }

        private IEnumerable<sisUsuarios> getDatos()
        {
            var _csId = (Session["UsuarioCentroDeSalud"] as sisUsuariosCentroDeSalud).csId;
            var _Datos = (from d in context.sisUsuario
                          join cs in context.sisUsuarioCentroDeSalud on d.usrId equals cs.usrId
                          where cs.csId == _csId
                          select new sisUsuarios()
                          {
                              usrId = d.usrId,
                              usrNombreDeUsuario = d.usrNombreDeUsuario,
                              usrApellidoyNombre = d.usrApellidoyNombre,
                              estId = d.estId,
                              usrPassword = "",
                              estDescripcion = d.estId == null ? "" : d.sisEstilos.estDescripcion,
                              perNombre = d.usrApellidoyNombre
                          }).ToList();

            return _Datos.AsEnumerable();
        }

        //Datos para el dropdown
        protected void PopulateDropDownList()
        {
            ViewData["estId_Data"] = new SelectList(context.sisEstilos, "estId", "estDescripcion");
            ViewData["ofiId_Data"] = new SelectList(context.catOficina.OrderBy(o => o.ofiNombre), "ofiId", "ofiNombre");
        }
    }
}