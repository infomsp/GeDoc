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

    public partial class catPersonaContratadosController : Controller
    {
        //Edición de datos
        [GridAction]
        public ActionResult _SelectEditingPersonaHorario()
        {
            return View(new GridModel(AllPersonaHorario()));
        }

        public IList<catPersonasContratadosHorarios> AllPersonaHorario()
        {
            return getDatosPersonaHorario().ToList();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _SaveEditingPersonaHorario(int id)
        {
            catPersonaContratadosHorario _Item = context.catPersonaContratadosHorario.Where(p => p.conhId == id).FirstOrDefault();
            catPersonasContratadosHorarios _Item2 = new catPersonasContratadosHorarios();

            TryUpdateModel(_Item2);

            _Item2.conhHoraInicio = DateTime.Parse(_Item2.conhHoraInicio).ToString("HH:mm");
            _Item2.conhHoraFin = DateTime.Parse(_Item2.conhHoraFin).ToString("HH:mm");

            _Item.conhHoraFin = _Item2.conhHoraFin.Substring(0, 2);
            _Item.conhMinutoFin = _Item2.conhHoraFin.Substring(3, 2);
            _Item.conhHoraInicio = _Item2.conhHoraInicio.Substring(0, 2);
            _Item.conhMinutoInicio = _Item2.conhHoraInicio.Substring(3, 2);
            //_Item.conhDiaSemana = _Item2.conhDiaSemana;
            //_Item.conId = _Item2.conId;

            Edit(_Item);

            return View(new GridModel(AllPersonaHorario().Where(exp => exp.conId == _Item.conId)));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _InsertEditingPersonaHorario()
        {
            catPersonasContratadosHorarios _Item2 = new catPersonasContratadosHorarios();
            List<catPersonaContratadosHorario> _Items = new List<catPersonaContratadosHorario>();

            if (TryUpdateModel(_Item2))
            {
                string _Error = "";

                _Item2.conhHoraInicio = DateTime.Parse(_Item2.conhHoraInicio).ToString("HH:mm");
                _Item2.conhHoraFin = DateTime.Parse(_Item2.conhHoraFin).ToString("HH:mm");

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
                        catPersonaContratadosHorario _Item = new catPersonaContratadosHorario();
                        _Item.conhActivo = true;
                        _Item.conhHoraFin = _Item2.conhHoraFin.Substring(0, 2);
                        _Item.conhMinutoFin = _Item2.conhHoraFin.Substring(3, 2);
                        _Item.conhHoraInicio = _Item2.conhHoraInicio.Substring(0, 2);
                        _Item.conhMinutoInicio = _Item2.conhHoraInicio.Substring(3, 2);
                        _Item.conhDiaSemana = _Dia;
                        _Item.conId = _Item2.conId;
                        _Items.Add(_Item);
                    }
                }
                if (_Error == "")
                {
                    Create(_Items);
                }
                else
                {
                    _Error += "con horarios superpuestos";
                    ModelState.AddModelError("Jueves", _Error);
                }
            }

            return View(new GridModel(AllPersonaHorario().Where(exp => exp.conId == _Item2.conId)));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _ActivaRegistroHorario(int id)
        {
            var _Item = context.catPersonaContratadosHorario.Where(e => e.conhId == id).First();

            DeleteConfirmedPersonaHorario(id, true);

            return View(new GridModel(AllPersonaHorario().Where(exp => exp.conId == _Item.conId)));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _DeleteEditingPersonaHorario(int id)
        {
            var _Item = context.catPersonaContratadosHorario.Where(e => e.conhId == id).First();

            DeleteConfirmedPersonaHorario(id, false);

            return View(new GridModel(AllPersonaHorario().Where(exp => exp.conId == _Item.conId)));
        }

        private IEnumerable<catPersonasContratadosHorarios> getDatosPersonaHorario()
        {
            var _Datos = (from d in context.catPersonaContratadosHorario.ToList()
                          select new catPersonasContratadosHorarios()
                                    {
                                        conhActivo = d.conhActivo,
                                        Lunes = (d.conhDiaSemana == "Lunes"),
                                        Martes = (d.conhDiaSemana == "Martes"),
                                        Miercoles = (d.conhDiaSemana == "Miércoles"),
                                        Jueves = (d.conhDiaSemana == "Jueves"),
                                        Viernes = (d.conhDiaSemana == "Viernes"),
                                        Sabado = (d.conhDiaSemana == "Sábado"),
                                        Domingo = (d.conhDiaSemana == "Domingo"),
                                        conhDiaSemana = d.conhDiaSemana,
                                        conhHoraInicio = d.conhHoraInicio + ":" + d.conhMinutoInicio,
                                        conhHoraFin = d.conhHoraFin + ":" + d.conhMinutoFin,
                                        conhHoras = d.conhHoraInicio + ":" + d.conhMinutoInicio + " a " + d.conhHoraFin + ":" + d.conhMinutoFin,
                                        conhId = d.conhId,
                                        conId = d.conId
                                    }).ToList();

            return _Datos.AsEnumerable();
        }

        [GridAction]
        public ActionResult _BindingPersonaHorario(Int64? idPersona)
        {
            idPersona = idPersona == null ? 1 : idPersona;

            return View(new GridModel<catPersonasContratadosHorarios>
            {
                Data = AllPersonaHorario().Where(exp => exp.conId == idPersona)
            });
        }

        private void Create(List<catPersonaContratadosHorario> pItem)
        {
            if (ModelState.IsValid)
            {
                    using (System.Transactions.TransactionScope transaction = new System.Transactions.TransactionScope())
                    {
                        try
                        {
                            foreach (var Item in pItem)
                            {
                                context.catPersonaContratadosHorario.AddObject(Item);
                                context.SaveChanges(SaveOptions.AcceptAllChangesAfterSave);
                            }
                            //Registra log de usuario
                            new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catPersonaContratados", "Agregar", "Agrega horario");
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
                            if (ex.InnerException == null)
                            {
                                ModelState.AddModelError("Jueves", ex.Message);
                            }
                            else
                            {
                                ModelState.AddModelError("Jueves", ex.InnerException.Message);
                            }
                        }

                    }
                }

            return;
        }

        private void Edit(catPersonaContratadosHorario pItem)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //Registra log de usuario
                    new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catPersonaContratados", "Modificar", "Modifica horario");

                    context.SaveChanges();
                }
                catch (Exception ex)
                { }
            }
            return;
        }

        private void DeleteConfirmedPersonaHorario(int id, bool pEstado)
        {
            context.SaveChanges();

            try
            {
                catPersonaContratadosHorario _Item = context.catPersonaContratadosHorario.Single(x => x.conhId == id);
                _Item.conhActivo = pEstado;
                //Registra log de usuario
                new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catPersona", "Eliminar", "Elimina horario");
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Jueves", ex.Message);
            }
        }

        private bool ValidaSuperposicion(catPersonasContratadosHorarios pItem, string pDia)
        {
            var _Horarios = context.catPersonaContratadosHorario.Where(w => w.conId == pItem.conId && w.conhDiaSemana == pDia);

            var _HoraInicio = DateTime.Parse(pItem.conhHoraInicio).ToString("HH:mm");
            var _HoraFin = DateTime.Parse(pItem.conhHoraFin).ToString("HH:mm");

            int _HoraInicial = int.Parse(_HoraInicio.Replace(":", ""));
            int _HoraFinal = int.Parse(_HoraFin.Replace(":", ""));
            foreach (var _Item in _Horarios)
            {
                int _HoraI = int.Parse(_Item.conhHoraInicio + _Item.conhMinutoInicio);
                int _HoraF = int.Parse(_Item.conhHoraFin + _Item.conhMinutoFin);
                if (_HoraInicial <= _HoraI && _HoraF >= _HoraI) { return false; }
                if (_HoraInicial > _HoraI && _HoraInicial <= _HoraF) { return false; }
                if (_HoraF >= _HoraInicial && _HoraF <= _HoraFinal) { return false; }
                if (_HoraFinal > _HoraF && _HoraInicial <= _HoraF) { return false; }
                if (_HoraInicial <= _HoraI && _HoraFinal >= _HoraF) { return false; }
                if (_HoraInicial >= _HoraI && _HoraFinal <= _HoraF) { return false; }
            }

            return true;
        }
    }
}