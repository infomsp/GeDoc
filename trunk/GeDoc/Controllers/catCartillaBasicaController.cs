using System;
using System.Collections.Generic;
using System.Json;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using GeDoc.Filters;
using GeDoc.Models;
using Telerik.Web.Mvc;

namespace GeDoc.Controllers
{
    public class catCartillaBasicaController : Controller
    {
        private readonly efAccesoADatosEntities _context = new efAccesoADatosEntities();
      
        //Datos para el dropdown
       
        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _SelectEditing(long pacId)
        {
            
            return View(new GridModel(All(pacId)));
        }
        public bool dropDowunlist()
        {
            catCartillaBasica catillaBasica;
            var _MIN = (from s in _context.catMinisterio.ToList()
                        select new catMinisterio()
                        {
                            minId = s.minId,
                            minNombre = s.minNombre
                        }).ToList();
            ViewData["minId_Data"] = new SelectList(_MIN, "minId", "minNombre");
            return true;
        }
        private IList<catCartillaBasicas>All(long pacId)
        {
                return getDatos(pacId).ToList();
        }
      
         private IEnumerable<catCartillaBasicas> getDatos(long pacId)
        {   var _Datos = (from a in _context.catCartillaBasica
                    join c in _context.catCartillaBasicaCuestionario on a.cartId equals  c.cartId
                    join b in _context.catEspecialidad on a.espId equals b.espId
                    join e in _context.catCentroDeSalud on a.csId equals e.csId
                    join f in _context.sisUsuario on a.usrId equals f.usrId
                    join g in _context.catMinisterio on a.minId equals g.minId
                    where a.pacId == pacId
                    orderby a.cartFecha descending
          //  let resp10 = c.resp10
          //  where resp10 != null
            select new catCartillaBasicas()
                    {
                        pacId =  a.pacId,
                        cartFecha = a.cartFecha,
                        csId = a.csId,
                        cartId = a.cartId,
                        espNombre = b.espNombre,
                        csNombre = e.csNombre,
                        usrApellidoyNombre = f.usrApellidoyNombre,
                        LugarTrabajo = a.LugarTrabajo,
                        minId = a.minId,
                        minDescripcion = g.minNombre,
                        resp15 = c.resp10 == 0 ? 0 : Math.Round( ((decimal)c.resp9 /(decimal) (c.resp10 * c.resp10))* 10000 ,2),
                         resp1  = c.resp1 ,
                         resp2  = c.resp2,
                         resp3  = c.resp3,
                         resp4  = c.resp4,
                         resp5  = c.resp5,
                         resp6  = c.resp6,
                         resp7  = c.resp7,
                         resp8  = c.resp8,
                         resp9  = c.resp9,
                         resp10  = c.resp10,
                         resp11  = c.resp11,
                         resp12  = c.resp12,
                         resp13  = c.resp13,
                        resp14 = c.resp14
                    }).ToList();
   

              return _Datos.AsEnumerable();
        }
         public decimal  ShowDecimalRound(decimal Argument, int Digits)
         {
             decimal rounded = decimal.Round(Argument, Digits);

             return rounded;
         }

        [AcceptVerbs(HttpVerbs.Post)]
         public void AddNewEntry(int pacId, int? minId, DateTime CartillaFecha, short espId, int? proId, int? turId, string LugarTrabajo, string resp1, string resp2, string resp3, string resp4, string resp5, string resp6, string resp7, string resp8, string resp9, string resp10, string resp11, string resp12, string resp13, string resp14)
        {
            var sisUsuariosCentroDeSalud = Session["UsuarioCentroDeSalud"] as sisUsuariosCentroDeSalud;


            if (sisUsuariosCentroDeSalud != null)
            {
                var userCsId = sisUsuariosCentroDeSalud.csId;
                var userUsrId = (short?)sisUsuariosCentroDeSalud.usrId;
                if (turId != null)
                {
                    catCartillaBasica newEntry = new catCartillaBasica
                    {
                        pacId = pacId,
                        espId = espId,
                        perId = proId > 0 ? proId : null,
                        conId = proId < 0 ? proId * -1 : null,
                        cartFecha = CartillaFecha,
                        csId = userCsId,
                        usrId = userUsrId,
                        turId = turId,
                        LugarTrabajo = LugarTrabajo,
                        minId = minId
                        
                    };
                    _context.catCartillaBasica.AddObject(newEntry);
                    _context.SaveChanges();
                    catCartillaBasicaCuestionario newEntryCuestionario = new catCartillaBasicaCuestionario
                    {
                       cartId = newEntry.cartId,
                        resp1 = resp1 == "on" ? 1: 0,
                       resp2 = resp2== null ? 0 : Int32.Parse(resp2),
                       resp3 = resp3 == "on" ? 1 : 0,
                       resp4 = resp4 == null ? 0 : Int32.Parse(resp4),
                       resp5 = resp5 == "on" ? 1 : 0,
                       resp6 = resp6 == "on" ? 1 : 0,
                       resp7 = resp7 == "on" ? 1 : 0,
                       resp8 = resp8 == "on" ? 1 : 0,
                       resp9 = resp9 == null ? 0 : decimal.Parse(resp9.Replace(".", ",")),
                       resp10 = resp10 == null ? 0 : Int32.Parse(resp10) ,
                       resp11 = resp11 == null ? 0 : Int32.Parse(resp11),
                       resp12 = resp12 == null ? 0 : Int32.Parse(resp12) ,
                       resp13 = resp13 == null ? 0 : Int32.Parse(resp13),
                       resp14 = resp14 == null ? 0 : decimal.Parse(resp14.Replace(".", ","))
                    };
                    _context.catCartillaBasicaCuestionario.AddObject(newEntryCuestionario);
                    _context.SaveChanges();
                }
               
            }
            else
            {
              //  RedirectToAction("LogOff", "Account");
            }
        }


    }
}
