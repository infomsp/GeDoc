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

    public partial class catPersonaController : Controller
    {
        //Edición de datos
        public IList<catPersonasHorarios> AllPersonaHorario(Int64? idPersona)
        {
            return getDatosPersonaHorario(idPersona).ToList();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _SaveEditingPersonaHorario(int id)
        {
            catPersonaHorario _Item = context.catPersonaHorario.FirstOrDefault(p => p.perhId == id);
            catPersonasHorarios _Item2 = new catPersonasHorarios();

            TryUpdateModel(_Item2);

            _Item2.perhHoraInicio = DateTime.Parse(_Item2.perhHoraInicio).ToString("HH:mm");
            _Item2.perhHoraFin = DateTime.Parse(_Item2.perhHoraFin).ToString("HH:mm");

            _Item.perhHoraFin = _Item2.perhHoraFin.Substring(0, 2);
            _Item.perhMinutoFin = _Item2.perhHoraFin.Substring(3, 2);
            _Item.perhHoraInicio = _Item2.perhHoraInicio.Substring(0, 2);
            _Item.perhMinutoInicio = _Item2.perhHoraInicio.Substring(3, 2);
            //_Item.perhDiaSemana = _Item2.perhDiaSemana;
            //_Item.perId = _Item2.perId;

            Edit(_Item);

            return View(new GridModel(AllPersonaHorario(_Item.perId)));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _InsertEditingPersonaHorario()
        {
            catPersonasHorarios _Item2 = new catPersonasHorarios();
            List<catPersonaHorario> _Items = new List<catPersonaHorario>();
            try
            {
            if (TryUpdateModel(_Item2))
            {
                string _Error = "";

                _Item2.perhHoraInicio = DateTime.Parse(_Item2.perhHoraInicio).ToString("HH:mm");
                _Item2.perhHoraFin = DateTime.Parse(_Item2.perhHoraFin).ToString("HH:mm");

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
                        catPersonaHorario _Item = new catPersonaHorario();
                        _Item.perhActivo = true;
                        _Item.perhHoraFin = _Item2.perhHoraFin.Substring(0, 2);
                        _Item.perhMinutoFin = _Item2.perhHoraFin.Substring(3, 2);
                        _Item.perhHoraInicio = _Item2.perhHoraInicio.Substring(0, 2);
                        _Item.perhMinutoInicio = _Item2.perhHoraInicio.Substring(3, 2);
                        _Item.perhDiaSemana = _Dia;
                        _Item.perId = _Item2.perId;
                        _Items.Add(_Item);
                    }
                }
                if (_Error == "")
                {
                    _Error = Create(_Items);
                    if (_Error != "")
                    {
                        ModelState.AddModelError("Jueves", _Error);
                    }
                }
                else
                {
                    _Error += " con horarios o fechas superpuestas";
                    ModelState.AddModelError("Jueves", _Error);
                }
            }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Jueves", ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }

            return View(new GridModel(AllPersonaHorario(_Item2.perId)));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _ActivaRegistroHorario(int id)
        {
            var _Item = context.catPersonaHorario.Where(e => e.perhId == id).First();

            DeleteConfirmedPersonaHorario(id, true);

            return View(new GridModel(AllPersonaHorario(_Item.perId)));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _DeleteEditingPersonaHorario(int id)
        {
            var _Item = context.catPersonaHorario.Where(e => e.perhId == id).First();

            DeleteConfirmedPersonaHorario(id, false);

            return View(new GridModel(AllPersonaHorario(_Item.perId)));
        }

        private IEnumerable<catPersonasHorarios> getDatosPersonaHorario(Int64? idPersona)
        {
            var _Datos = (from d in context.catPersonaHorario
                          where d.perId == idPersona
                          select new catPersonasHorarios()
                                    {
                                        perhActivo = d.perhActivo,
                                        Lunes = (d.perhDiaSemana == "Lunes"),
                                        Martes = (d.perhDiaSemana == "Martes"),
                                        Miercoles = (d.perhDiaSemana == "Miércoles"),
                                        Jueves = (d.perhDiaSemana == "Jueves"),
                                        Viernes = (d.perhDiaSemana == "Viernes"),
                                        Sabado = (d.perhDiaSemana == "Sábado"),
                                        Domingo = (d.perhDiaSemana == "Domingo"),
                                        perhDiaSemana = d.perhDiaSemana,
                                        perhHoraInicio = d.perhHoraInicio + ":" + d.perhMinutoInicio,
                                        perhHoraFin = d.perhHoraFin + ":" + d.perhMinutoFin,
                                        perhHoras = d.perhHoraInicio + ":" + d.perhMinutoInicio + " a " + d.perhHoraFin + ":" + d.perhMinutoFin,
                                        perhId = d.perhId,
                                        perId = d.perId
                                    }).ToList();

            return _Datos.AsEnumerable();
        }

        [GridAction]
        public ActionResult _BindingPersonaHorario(Int64? idPersona)
        {
            idPersona = idPersona == null ? 1 : idPersona;

            return View(new GridModel<catPersonasHorarios>
            {
                Data = AllPersonaHorario(idPersona)
            });
        }

        private string Create(List<catPersonaHorario> pItem)
        {
            string lsError = "";
            if (ModelState.IsValid)
            {
                    using (System.Transactions.TransactionScope transaction = new System.Transactions.TransactionScope())
                    {
                        try
                        {
                            foreach (var Item in pItem)
                            {
                                context.catPersonaHorario.AddObject(Item);
                                context.SaveChanges(SaveOptions.AcceptAllChangesAfterSave);
                            }
                            //Registra log de usuario
                            new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catPersona", "Agregar", "Agrega horario");
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
                                lsError = ex.Message;
                            }
                            else
                            {
                                lsError = ex.InnerException.Message;
                            }
                        }

                    }
                }
            else
            {
               lsError = "Error al guardar información. Modelo no válido.";
            }

            return lsError;
        }

        private void Edit(catPersonaHorario pItem)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //Registra log de usuario
                    new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catPersona", "Modificar", "Modifica horario");

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
                catPersonaHorario _Item = context.catPersonaHorario.Single(x => x.perhId == id);
                _Item.perhActivo = pEstado;
                //Registra log de usuario
                new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catPersona", "Eliminar", "Elimina horario");
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Jueves", ex.Message);
            }
        }

        private bool ValidaSuperposicion(catPersonasHorarios pItem, string pDia)
        {
            var _Horarios = context.catPersonaHorario.Where(w => w.perId == pItem.perId && w.perhDiaSemana == pDia);

            var _HoraInicio = DateTime.Parse(pItem.perhHoraInicio).ToString("HH:mm");
            var _HoraFin = DateTime.Parse(pItem.perhHoraFin).ToString("HH:mm");

            int _HoraInicial = int.Parse(_HoraInicio.Replace(":", ""));
            int _HoraFinal = int.Parse(_HoraFin.Replace(":", ""));
            foreach (var _Item in _Horarios)
            {
                int _HoraI = int.Parse(_Item.perhHoraInicio + _Item.perhMinutoInicio);
                int _HoraF = int.Parse(_Item.perhHoraFin + _Item.perhMinutoFin);
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