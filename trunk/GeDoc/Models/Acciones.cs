namespace GeDoc
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Web.Mvc;

    public class Acciones
    {
        public List<Accion> _Acciones { get; set; }

        public string Visibilidad(string pMenuController, string pAccion)
        {
            var _Resultado = _Acciones.Find(a => a.MenuController == pMenuController && a._Accion == pAccion);
            if (_Resultado == null)
            {
                return "none";
            }

            return _Resultado.Visibilidad;
        }
    }
}
