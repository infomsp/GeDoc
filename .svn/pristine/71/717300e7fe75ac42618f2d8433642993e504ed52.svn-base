/************************************************************************************************************
 * Descripción: Clase referente a la estructura de LogWork de los requerimientos
 * Observaciones: 
 * Creado por: Gustavo N. Tripolone
 * Historial de Revisiones:
 * -----------------------------------------------------------------------------------------------
 * Fecha        Autor   Descripción
 * -----------------------------------------------------------------------------------------------
 * 29/05/2014   GNT     Implementación inicial
 * -----------------------------------------------------------------------------------------------
*/

namespace GeDoc
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel;

    [KnownType(typeof(catRequerimientoLogWorkWS))]
    [Serializable()]
    public class catRequerimientoLogWorkWS : EntityObject
    {
        [ScaffoldColumn(false)]
        public int reqlId
        {
            get;
            set;
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingresar la fecha")]
        [DisplayName("Fecha:")]
        [DataType(DataType.Date)]
        public DateTime reqlFecha
        {
            get;
            set;
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese el Tiempo")]
        [DisplayName("Tiempo (Horas):")]
        [UIHint("Integer")]
        public int reqlTiempo
        {
            get;
            set;
        }

        [DisplayName("Observaciones:")]
        [Required(ErrorMessage = "Ingrese observaciones")]
        [StringLength(255, ErrorMessage = "Máximo 255 carácteres")]
        [UIHint("MultilineText")]
        public string reqlObservaciones
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int32 usrId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public sisUsuarios Usuario
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public int reqId
        {
            get;
            set;
        }

    }
}