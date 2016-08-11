namespace GeDoc.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Telerik.Web.Mvc;
    using GeDoc.Models;
    using System.Data.Objects;
    public partial class catTurnoController : Controller
    {
        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _SelectEditingPacienteBusqueda(string TextoBuscado)
        {
            if (Session["UsuarioCentroDeSalud"] == null)
            {
                RedirectToAction("LogOff", "Account");
                return View(new GridModel(new List<spGetBusquedaPaciente_Result>()));
            }
            try
            {
                var _csId = (Session["UsuarioCentroDeSalud"] as GeDoc.sisUsuariosCentroDeSalud).csId;
                var _Datos = (from d in context.spGetBusquedaPaciente(_csId, TextoBuscado).ToList()
                              orderby d.pacId descending
                              select new spGetBusquedaPaciente_Result()
                                  {
                                      pacId = d.pacId,
                                      pacApellido = d.pacApellido.ToUpper(),
                                      pacNombre = d.pacNombre.ToUpper(),
                                      pacNumeroDocumento = d.pacNumeroDocumento,
                                      nroHC = d.nroHC,
                                  }).ToList();

                return View(new GridModel(_Datos));
            }
            catch (Exception ex)
            {
            }

            return View(new GridModel(new List<spGetBusquedaPaciente_Result>()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult patientAlreadyHasATurnOnThatDay(long _pacId, long _turId)
        {
            int _csId = (Session["UsuarioCentroDeSalud"] as GeDoc.sisUsuariosCentroDeSalud).csId;
            ObjectResult<spGetPacienteOtrosTurnos_Result> result = context.spGetPacienteOtrosTurnos(_csId, _pacId, _turId);
            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult updatePatientHC(long pacId, string nroHC, bool hasBeenWarned = false)
        {
            int csId = (Session["UsuarioCentroDeSalud"] as GeDoc.sisUsuariosCentroDeSalud).csId;

            var _Items = (from e in context.enlPacienteCtroSalud
                          join p in context.catPaciente on e.pacId equals p.pacId
                          where e.csId == csId && e.nroHC == nroHC
                          select new
                          {
                              p.pacNombre, p.pacApellido, p.pacNumeroDocumento

                          }).ToArray();

            if (_Items.Count() != 0 && !hasBeenWarned)
            {
                return Json(_Items);
            }
            enlPacienteCtroSalud epcs = context.enlPacienteCtroSalud.SingleOrDefault(q => q.pacId == pacId && q.csId == csId);
            if (epcs == null)
            {
                epcs = new enlPacienteCtroSalud();
                epcs.nroHC = nroHC;
                epcs.pacId = pacId;
                epcs.csId = csId;
                context.enlPacienteCtroSalud.AddObject(epcs);
            }
            else
            {
                epcs.nroHC = nroHC;
            }
            context.SaveChanges();
            new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catTurno", "Modificar", "Nueva HC para paciente " + pacId + ": " + nroHC);
            return Json("ok");     
        }
    }
}