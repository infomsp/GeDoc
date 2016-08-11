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

    public partial class catAgendaController : Controller
    {
        //Edición de datos
        [GridAction]
        public ActionResult _SelectEditingAgendaHorario()
        {
            return View(new GridModel(AllAgendaHorario()));
        }

        public IList<catAgendasHorarios> AllAgendaHorario()
        {
            return getDatosAgendaHorario().ToList();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _SaveEditingAgendaHorario(int id)
        {
            catAgendaHorario _Item = context.catAgendaHorario.Where(p => p.aghId == id).FirstOrDefault();
            catAgendasHorarios _Item2 = new catAgendasHorarios();

            TryUpdateModel(_Item2);

            _Item2.aghHoraInicio = DateTime.Parse(_Item2.aghHoraInicio).ToString("HH:mm");
            _Item2.aghHoraFin = DateTime.Parse(_Item2.aghHoraFin).ToString("HH:mm");

            _Item.aghHoraFin = _Item2.aghHoraFin.Substring(0, 2);
            _Item.aghMinutoFin = _Item2.aghHoraFin.Substring(3, 2);
            _Item.aghHoraInicio = _Item2.aghHoraInicio.Substring(0, 2);
            _Item.aghMinutoInicio = _Item2.aghHoraInicio.Substring(3, 2);
            _Item.aghVigenciaDesde = _Item2.aghVigenciaDesde;
            _Item.aghVigenciaHasta = _Item2.aghVigenciaHasta;
            _Item.aghCantTurnos = _Item2.aghCantTurnos;
            _Item.aghSobreturnos = _Item2.aghSobreturnos;
           // _Item.aghDiaSemana = _Item2.aghDiaSemana;
            _Item.agId = _Item2.agId;

            Edit(_Item);

            return View(new GridModel(AllAgendaHorario().Where(exp => exp.agId == _Item.agId)));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _InsertEditingAgendaHorario()
        {
            catAgendasHorarios _Item2 = new catAgendasHorarios();
            List<catAgendaHorario> _Items = new List<catAgendaHorario>();

            if (TryUpdateModel(_Item2))
            {
                string _Error = "";

                _Item2.aghHoraInicio = DateTime.Parse(_Item2.aghHoraInicio).ToString("HH:mm");
                _Item2.aghHoraFin = DateTime.Parse(_Item2.aghHoraFin).ToString("HH:mm");

                for (int i = 1; i <= 7; i++)
                {
                    string _Dia = "";
                    switch(i)
                    {
                        case 1:
                            if (_Item2.Lunes)
                            {
                                _Dia = "Lunes";
                            }
                            break;
                        case 2:
                            if (_Item2.Martes)
                            {
                                _Dia = "Martes";
                            }
                            break;
                        case 3:
                            if (_Item2.Miercoles)
                            {
                                _Dia = "Miércoles";
                            }
                            break;
                        case 4:
                            if (_Item2.Jueves)
                            {
                                _Dia = "Jueves";
                            }
                            break;
                        case 5:
                            if (_Item2.Viernes)
                            {
                                _Dia = "Viernes";
                            }
                            break;
                        case 6:
                            if (_Item2.Sabado)
                            {
                                _Dia = "Sábado";
                            }
                            break;
                        case 7:
                            if (_Item2.Domingo)
                            {
                                _Dia = "Domingo";
                            }
                            break;
                    }
                    if (_Dia != "")
                    {
                        if (!ValidaSuperposicion(_Item2, _Dia))
                        {
                            _Error += _Dia + ", ";
                            _Dia = "";
                        }
                    }
                    if (_Dia != "")
                    {
                        catAgendaHorario _Item = new catAgendaHorario();
                        _Item.aghActivo = true;
                        _Item.aghHoraFin = _Item2.aghHoraFin.Substring(0, 2);
                        _Item.aghMinutoFin = _Item2.aghHoraFin.Substring(3, 2);
                        _Item.aghHoraInicio = _Item2.aghHoraInicio.Substring(0, 2);
                        _Item.aghMinutoInicio = _Item2.aghHoraInicio.Substring(3, 2);
                        _Item.aghVigenciaDesde = _Item2.aghVigenciaDesde;
                        _Item.aghVigenciaHasta = _Item2.aghVigenciaHasta;
                        _Item.aghDiaSemana = _Dia;
                        _Item.aghCantTurnos = _Item2.aghCantTurnos;
                        _Item.aghSobreturnos = _Item2.aghSobreturnos;
                        _Item.agId = _Item2.agId;                        
                        _Items.Add(_Item);
                    }
                }
                if (_Error == "")
                {
                    Create(_Items);
                }
                else
                {
                    _Error += "con horarios o fechas superpuestas";
                    ModelState.AddModelError("Jueves", _Error);
                }
            }

            return View(new GridModel(AllAgendaHorario().Where(exp => exp.agId == _Item2.agId)));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _ActivaRegistroHorario(int id)
        {
            var _Item = context.catAgendaHorario.Where(e => e.aghId == id).First();

            DeleteConfirmedAgendaHorario(id, true);

            return View(new GridModel(AllAgendaHorario().Where(exp => exp.agId == _Item.agId)));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _DeleteEditingAgendaHorario(int id)
        {
            var _Item = context.catAgendaHorario.Where(e => e.aghId == id).First();

            DeleteConfirmedAgendaHorario(id, false);

            return View(new GridModel(AllAgendaHorario().Where(exp => exp.agId == _Item.agId)));
        }

        private IEnumerable<catAgendasHorarios> getDatosAgendaHorario()
        {
            var _Datos = (from d in context.catAgendaHorario.ToList()
                          select new catAgendasHorarios()
                                    {
                                        aghActivo = d.aghActivo,
                                        Lunes = (d.aghDiaSemana == "Lunes"),
                                        Martes = (d.aghDiaSemana == "Martes"),
                                        Miercoles = (d.aghDiaSemana == "Miércoles"),
                                        Jueves = (d.aghDiaSemana == "Jueves"),
                                        Viernes = (d.aghDiaSemana == "Viernes"),
                                        Sabado = (d.aghDiaSemana == "Sábado"),
                                        Domingo = (d.aghDiaSemana == "Domingo"),
                                        aghDiaSemana = d.aghDiaSemana,
                                        aghFechas = d.aghVigenciaDesde.ToString("dd/MM/yyyy") + " - " + d.aghVigenciaHasta.ToString("dd/MM/yyyy"),
                                        aghHoraInicio = d.aghHoraInicio + ":" + d.aghMinutoInicio,
                                        aghHoraFin = d.aghHoraFin + ":" + d.aghMinutoFin,
                                        aghHoras = d.aghHoraInicio + ":" + d.aghMinutoInicio + " a " + d.aghHoraFin + ":" + d.aghMinutoFin,
                                        aghId = d.aghId,
                                        aghVigenciaDesde = d.aghVigenciaDesde,
                                        aghVigenciaHasta = d.aghVigenciaHasta,
                                        aghCantTurnos = d.aghCantTurnos,
                                        aghSobreturnos = d.aghSobreturnos,
                                        agId = d.agId                                        
                                    }).ToList();

            return _Datos.AsEnumerable();
        }

        [GridAction]
        public ActionResult _BindingAgendaHorario(Int64? idAgenda)
        {
            idAgenda = idAgenda == null ? 1 : idAgenda;

            return View(new GridModel<catAgendasHorarios>
            {
                Data = AllAgendaHorario().Where(exp => exp.agId == idAgenda)
            });
        }

        private void Create(List<catAgendaHorario> pItem)
        {
            if (ModelState.IsValid)
            {
                    //using (System.Transactions.TransactionScope transaction = new System.Transactions.TransactionScope())
                    //{
                        try
                        {
                            foreach (var Item in pItem)
                            {
                                context.catAgendaHorario.AddObject(Item);
                                context.SaveChanges(SaveOptions.AcceptAllChangesAfterSave);
                            }
                            //Registra log de usuario
                            new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catAgenda", "Agregar", "Agrega horario");
                            //Guardamos los datos de la primera entidad en BBDD en modo PESIMISTA                
                            //Persistimos definitivamente los datos en BBDD
                           // context.AcceptAllChanges();
                           context.SaveChanges();
                            //Marcamos la transacción como completada
                           // transaction.Complete();
                        }
                        //Capturamos las excepciones
                        catch (Exception ex)
                        {
                            //Deshacemos la transacción
                           // transaction.Dispose();
                            if (ex.InnerException == null)
                            {
                                ModelState.AddModelError("Jueves", ex.Message);
                            }
                            else
                            {
                                ModelState.AddModelError("Jueves", ex.InnerException.Message);
                            }
                        }

                    //}
                }

            return;
        }

        private void Edit(catAgendaHorario pItem)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //Registra log de usuario
                    new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catAgenda", "Modificar", "Modifica horario");

                    context.SaveChanges();
                }
                catch (Exception ex)
                { }
            }
            return;
        }

        private void DeleteConfirmedAgendaHorario(int id, bool pEstado)
        {
            context.SaveChanges();

            try
            {
                catAgendaHorario _Item = context.catAgendaHorario.Single(x => x.aghId == id);
                _Item.aghActivo = pEstado;
                //Registra log de usuario
                new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catAgenda", "Eliminar", "Elimina horario");
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Jueves", ex.Message);
            }
        }

        private bool ValidaSuperposicion(catAgendasHorarios pItem, string pDia)
        {
            //var perId = context.vwTurnosAgenda(s => s.
            var _Horarios = context.catAgendaHorario.Where(w => w.agId == pItem.agId && w.aghDiaSemana == pDia);
           // var _Horarios_ = context.catAgendaHorario.Where(w => w.catAgenda.perId ==  && w.aghDiaSemana == pDia);
            var _HoraInicio = DateTime.Parse(pItem.aghHoraInicio).ToString("HH:mm");
            var _HoraFin = DateTime.Parse(pItem.aghHoraFin).ToString("HH:mm");

            DateTime _FechaInicial = DateTime.Parse(pItem.aghVigenciaDesde.ToString("dd/MM/yyyy") + " " + _HoraInicio);
            DateTime _FechaFinal = DateTime.Parse(pItem.aghVigenciaHasta.ToString("dd/MM/yyyy") + " " + _HoraFin);
            int _HoraInicial = int.Parse(_HoraInicio.Replace(":", ""));
            int _HoraFinal = int.Parse(_HoraFin.Replace(":", ""));
            foreach (var _Item in _Horarios)
            {
                DateTime _FechaI = DateTime.Parse(_Item.aghVigenciaDesde.ToString("dd/MM/yyyy") + " " + _Item.aghHoraInicio + ":" + _Item.aghMinutoInicio);
                DateTime _FechaF = DateTime.Parse(_Item.aghVigenciaHasta.ToString("dd/MM/yyyy") + " " + _Item.aghHoraFin + ":" + _Item.aghMinutoFin);
                int _HoraI = int.Parse(_Item.aghHoraInicio + _Item.aghMinutoInicio);
                int _HoraF = int.Parse(_Item.aghHoraFin + _Item.aghMinutoFin);
                bool _SuperposicionDeFechas = false;

                if (_FechaInicial <= _FechaI && _FechaFinal >= _FechaI) { _SuperposicionDeFechas = true; }
                if (_FechaInicial > _FechaI && _FechaInicial <= _FechaF) { _SuperposicionDeFechas = true; }
                if (_FechaFinal >= _FechaI && _FechaFinal <= _FechaF) { _SuperposicionDeFechas = true; }
                if (_FechaFinal > _FechaF && _FechaInicial <= _FechaF) { _SuperposicionDeFechas = true; }
                if (_FechaInicial <= _FechaI && _FechaFinal >= _FechaF) { _SuperposicionDeFechas = true; }
                if (_FechaInicial >= _FechaI && _FechaFinal <= _FechaF) { _SuperposicionDeFechas = true; }

                if (_SuperposicionDeFechas)
                {
                    if (_HoraInicial <= _HoraI && _HoraF >= _HoraI) { return false; }
                    if (_HoraInicial > _HoraI && _HoraInicial <= _HoraF) { return false; }
                    if (_HoraF >= _HoraInicial && _HoraF <= _HoraFinal) { return false; }
                    if (_HoraFinal > _HoraF && _HoraInicial <= _HoraF) { return false; }
                    if (_HoraInicial <= _HoraI && _HoraFinal >= _HoraF) { return false; }
                    if (_HoraInicial >= _HoraI && _HoraFinal <= _HoraF) { return false; }
                }
            }

            return true;
        }
    }
}