/************************************************************************************************************
 * Descripción: Clase referente a la estructura de Proveedores.
 * Observaciones: 
 * Creado por: Nombre y Apellido
 * Historial de Revisiones:
 * -----------------------------------------------------------------------------------------------
 * Fecha        Autor   Descripción
 * -----------------------------------------------------------------------------------------------
 * 26/08/2015   GNT     Implementación inicial
 * -----------------------------------------------------------------------------------------------
*/
namespace GeDoc
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel;

    [KnownType(typeof(catProveedorRubroWS))]
    [Serializable()]
    public class catProveedorRubroWS : EntityObject
    {
        [ScaffoldColumn(false)]
        public int prId
        {
            get;
            set;
        }

        [DisplayName("Rubro:")]
        [Required(ErrorMessage = "Ingrese el Rubro")]
        [UIHint("GridForeignKeyAutoComplete")]
        public string prRubro
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string prAccion
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public int prprovId
        {
            get;
            set;
        }

    }
}