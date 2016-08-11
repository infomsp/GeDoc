using System;

namespace GeDoc.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Telerik.Web.Mvc;
    using GeDoc.Filters;
    using GeDoc.Models;

    public partial class catTurnoController : Controller
    {
        public ActionResult VisualizadorDeTurnos()
        {
            ViewBag.Catalogo = "Llamador de pacientes";
            Session["TurnosLlamados"] = new List<catCentroDeSaludLLamadorWS>();
            Session["TelevisorId"] = -1;
            Session["CentroDeSaludLogOut"] = "OK";
            CargarSalas();

            return PartialView();
        }

        public ActionResult LlamadorDePacientes(int codigoTV)
        {
            ViewBag.Catalogo = "Llamador de pacientes";
            Session["TurnosLlamados"] = new List<catCentroDeSaludLLamadorWS>();
            Session["TelevisorId"] = codigoTV;
            var Tele = context.catCentroDeSaludSalaTelevisor.FirstOrDefault(f => f.csstId == codigoTV);
            if (Tele != null)
            {
                Session["CentroDeSaludLogOut"] = Tele.catCentroDeSaludSala.catCentroDeSalud.csNombre;
            }
            else
            {
                Session["CentroDeSaludLogOut"] = "ERROR";
            }
            //CargarSalas();

            return PartialView();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult getLlamarProximoPaciente(int TelevisorId)
        {
            var Llamados = context.getTurnosLlamados(TelevisorId).ToList();
            var Marquesina = context.getMarquesina(TelevisorId).First().Marquesina;

            Session["TurnosLlamados"] = (from x in Llamados.Where(w => !w.llamLlamar)
                select new catCentroDeSaludLLamadorWS()
                {
                    llamTexto2 = x.llamTexto2,
                    llamId = x.llamId,
                    llamTexto1 = x.llamTexto1,
                    llamLlamar = x.llamLlamar,
                    llamFecha = x.llamFecha,
                    llamIdentificador = x.llamIdentificador,
                    llamOficina = x.llamOficina,
                    llamOrigen = x.llamOrigen,
                    llamFechaDeLlamado = x.llamFechaDeLlamado.Value,
                    llamHoraDeLlamado = x.llamFechaDeLlamado == null ? "" : x.llamFechaDeLlamado.Value.ToString("HH:mm")
                }).ToList();

            var Datos = (from x in Llamados.Where(w => w.llamLlamar)
                select new catCentroDeSaludLLamadorWS()
                {
                    llamTexto2 = x.llamTexto2,
                    llamId = x.llamId,
                    llamTexto1 = x.llamTexto1,
                    llamLlamar = x.llamLlamar,
                    llamFecha = x.llamFecha,
                    llamIdentificador = x.llamIdentificador,
                    llamOficina = x.llamOficina,
                    llamOrigen = x.llamOrigen,
                    llamFechaDeLlamado = DateTime.Now,
                    llamHoraDeLlamado = DateTime.Now.ToString("HH:mm")
                }).ToList();

            return Json(new { Datos = Datos.Count > 0 ? Datos.First() : new catCentroDeSaludLLamadorWS(), Llamar = (Datos.Count > 0), Hora = DateTime.Now.ToString("HH:mm"), Marquesina = Marquesina, Grilla = (Session["TurnosLlamados"] as List<catCentroDeSaludLLamadorWS>) });
        }

        [GridAction]
        public ActionResult _SelectEditingLlamados()
        {
            var Datos = new List<catCentroDeSaludLLamadorWS>();
            if (Session["TurnosLlamados"] != null)
            {
                Datos = ((List<catCentroDeSaludLLamadorWS>)Session["TurnosLlamados"]).ToList();
            }

            return View(new GridModel<catCentroDeSaludLLamadorWS>
            {
                Data = Datos
            });
        }

    }
}