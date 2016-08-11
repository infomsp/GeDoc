namespace GeDoc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.Security;
    using GeDoc.Models;
    using System.Transactions;
    using System.Configuration;

    public class AccountController : Controller
    {

        private efAccesoADatosEntities context = new efAccesoADatosEntities();
        sisUsuario _DatosUsuario = new sisUsuario();

        public ActionResult LogOn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                //if (Membership.ValidateUser(model.UserName, model.Password))
                if (ValidarUsuario(model))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    Session["Usuario"] = context.sisUsuario.First(u => u.usrNombreDeUsuario == model.UserName);
                    var usrId = ((sisUsuario)Session["Usuario"]).usrId;
                    var _Acciones = (from p in context.sisUsuarioPermiso.Where(w => w.usrId == usrId && w.acId != null)
                                     select new Accion()
                                        {
                                            MenuController = p.sisMenu.mnuController,
                                            _Accion = p.sisAccion.acDescripcion,
                                            Visibilidad = p.upAutorizado ? "inline-block" : "none"
                                        }).ToList();
                    Session["Permisos"] = new Acciones() { _Acciones = _Acciones };
                    Session["Estilo"] = (Session["Usuario"] as sisUsuario).sisEstilos.estNombre;
                    int? repId = (Session["Usuario"] as sisUsuario).repId;//context.sisUsuario.SingleOrDefault(c => c.usrId == usrId).repId;
                    Session["ZonaSanitaria"] = repId == null ? null : context.catReparticion.Single(p => p.repId == repId).repNombre;
                    if ((Session["Usuario"] as sisUsuario).sisUsuarioCentroDeSalud.Count == 1)
                    {
                        var _UsuarioCS = (Session["Usuario"] as sisUsuario).sisUsuarioCentroDeSalud.First();
                        Session["UsuarioCentroDeSalud"] = new sisUsuariosCentroDeSalud() { csId = _UsuarioCS.csId, ucsId = _UsuarioCS.ucsId, usrId = _UsuarioCS.usrId, ucsCentroDeSalud = _UsuarioCS.catCentroDeSalud.csNombre };
                    }
                    //Registramos el ingreso al sistema\\
                    RegistrarLog(null, null, null, "Ingreso al Sistema");

                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "El nombre de usuario o contraseña ingresados, son incorrectos.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
            RegistrarLog(null, null, null, "Salida del sistema");
            FormsAuthentication.SignOut();
            Session["Usuario"] = null;
            Session["MenuPrincipal"] = new List<Telerik.Web.Mvc.UI.MenuItem>();
            Session["Permisos"] = new List<sisUsuarioPermiso>();
            Session["UsuarioCentroDeSalud"] = null;
            Session["ZonaSanitaria"] = null;

            return RedirectToAction("LogOn", "Account");
        }

        public ActionResult ValidaActualizacion()
        {

            string _Actualizando = ConfigurationManager.AppSettings.Get("Actualizando");

            if (_Actualizando == "S" || Session["Permisos"] == null)
            {
                return Json(true);
            }

            return Json(false);
        }

        //
        // GET: /Account/Register

        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                MembershipCreateStatus createStatus;
                Membership.CreateUser(model.UserName, model.Password, model.Email, null, null, true, null, out createStatus);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false /* createPersistentCookie */);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", ErrorCodeToString(createStatus));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePassword

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Account/ChangePassword

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            string lsError = "";
            string lsCampo = "NewPassword";

            if (ModelState.IsValid)
            {

                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                bool changePasswordSucceeded = true;
                try
                {
                    //MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true /* userIsOnline */);
                    //changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                    if (model.OldPassword == model.NewPassword)
                    {
                        lsCampo = "OldPassword";
                        lsError = "La contraseña actual y la nueva contraseña son iguales.";
                        changePasswordSucceeded = false;
                    }
                    else if (model.ConfirmPassword != model.NewPassword)
                    {
                        lsCampo = "NewPassword";
                        lsError = "La nueva contraseña y la confirmación, son distinta.";
                        changePasswordSucceeded = false;
                    }

                    if (changePasswordSucceeded)
                    {
                        var _Usuario = (Session["Usuario"] as sisUsuario);
                        //dbcDatosDataContext _Metodos = new dbcDatosDataContext();
                        using (var transaction = new TransactionScope())
                        {
                            try
                            {
                                context.ProcActualizaUsuario(_Usuario.usrId, _Usuario.usrNombreDeUsuario,
                                                              _Usuario.usrApellidoyNombre, model.NewPassword,
                                                              _Usuario.estId, _Usuario.usrEmail, _Usuario.repId, _Usuario.perId, _Usuario.icId);
                                context.SaveChanges();
                                transaction.Complete();
                            }
                            catch (Exception ex)
                            {
                                lsCampo = "NewPassword";
                                lsError = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                                changePasswordSucceeded = false;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    lsCampo = "NewPassword";
                    lsError = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError(lsCampo, lsError);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePasswordSuccess
        public ActionResult ChangeEstilo(short pEstilo)
        {
            int _usrId = (Session["Usuario"] as sisUsuario).usrId;
            var _Usuario = context.sisUsuario.Where(u => u.usrId == _usrId).FirstOrDefault();

            _Usuario.estId = pEstilo;

            context.SaveChanges();

            Session["Estilo"] = _Usuario.sisEstilos.estNombre;
            (Session["Usuario"] as GeDoc.Models.sisUsuario).estId = pEstilo;

            return Json(true);
        }

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        private bool ValidarUsuario(LogOnModel model)
        {
            using (dbcDatosDataContext _Validar = new dbcDatosDataContext())
            {
                var _Resultado = _Validar.fnValidarUsuario(model.UserName, model.Password).Value;
                return _Resultado;
            }
        }

        //Datos para el dropdown
        public SelectList PopulateDropDownList()
        {
            var _Estilos = new SelectList(context.sisEstilos, "estId", "estDescripcion");

            return _Estilos;
        }

        public void RegistrarLog(sisUsuario pUsuario, string pAccion, string pController, string pAccionCRUD, string pDescripcion)
        {
            _DatosUsuario = pUsuario;
            RegistrarLog(pAccion, pController, pAccionCRUD, pDescripcion);
        }

        public void RegistrarLog(string pAccion, string pController, string  pAccionCRUD, string pDescripcion)
        {
            short pusrId = Session == null ? _DatosUsuario.usrId : (Session["Usuario"] == null ? _DatosUsuario.usrId : (Session["Usuario"] as sisUsuario).usrId);
            if (pusrId == 0)
            {
                return;
            }
            int? pmnuId = null;
            int? pacId = null;
            if (pAccion != null)
            {
                pmnuId = context.sisMenu.First(u => u.mnuAccion == pAccion && u.mnuController == pController).mnuId;
                if (pAccionCRUD != null)
                {
                    pacId = context.sisAccion.First(a => a.acDescripcion == pAccionCRUD).acId;
                }
            }

            dbcDatosDataContext _Metodos = new dbcDatosDataContext();
            using (var transaction = new TransactionScope())
            {
                try
                {
                    _Metodos.ProcRegistrarLogUsuario(pDescripcion, pusrId, pmnuId, pacId);
                    _Metodos.SubmitChanges();
                    transaction.Complete();
                }
                catch (Exception ex)
                {
                }
            }
        }

        public bool VerNormasLegalesAVencer(sisUsuario pUsuario)
        {
            if (pUsuario == null)
            {
                return false;
            }

            int _usrId = pUsuario.usrId;

            var _Datos = context.sisUsuarioPermiso.Where(p => p.usrId == _usrId && p.sisMenu.mnuNombre == "/Resoluciones/Documentos" && p.acId == null && p.upAutorizado == true);

            return _Datos.Count() > 0;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult setCentroSaludUsuario(int idUcs)
        {
            var _UsuarioCS = (Session["Usuario"] as sisUsuario).sisUsuarioCentroDeSalud.First(w => w.ucsId == idUcs);
            Session["UsuarioCentroDeSalud"] = new sisUsuariosCentroDeSalud() { csId = _UsuarioCS.csId, ucsId = _UsuarioCS.ucsId, usrId = _UsuarioCS.usrId, ucsCentroDeSalud = _UsuarioCS.catCentroDeSalud.csNombre };

            return Json(true);
        }


        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "El nombre de usuario ya existe. Ingrese un nombre de usuario diferente por favor.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "Ya existe un Usuario con la dirección de correo ingresada. Ingrese una dirección de correo diferente por favor.";

                case MembershipCreateStatus.InvalidPassword:
                    return "La contraseña ingresada no es válida. Ingrese una contraseña válida por favor.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}
