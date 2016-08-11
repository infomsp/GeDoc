/************************************************************************************************************
 * Descripción: Clase referente a la estructura de comentarios de los requerimientos
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

    [KnownType(typeof(catRequerimientoComentariosWS))]
    [Serializable()]
    public class catRequerimientoComentariosWS : EntityObject
    {
        [ScaffoldColumn(false)]
        public int reqcId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public DateTime reqcFecha
        {
            get;
            set;
        }

        [DisplayName("Comentario:")]
        [Required(ErrorMessage = "Ingrese un comentario")]
        [StringLength(255, ErrorMessage = "Máximo 255 carácteres")]
        [UIHint("MultilineText")]
        public string reqcComentario
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