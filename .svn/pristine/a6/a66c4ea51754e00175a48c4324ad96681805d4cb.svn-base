/************************************************************************************************************
 * Descripción: Clase referente a la estructura de EncuestaAPSRecibo
 * Observaciones: 
 * Creado por: Federico Rojas
 * Historial de Revisiones:
 * -----------------------------------------------------------------------------------------------
 * Fecha        Autor                   Descripción
 * -----------------------------------------------------------------------------------------------
 * 12/06/2015	Rojas Federico 		    Implementación inicial
 * -----------------------------------------------------------------------------------------------
*/

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace GeDoc
{
    [KnownType(typeof(catEncuestaAPSRecibos))]
    [Serializable()]
    public class catEncuestaAPSRecibos : EntityObject
    {
        [ScaffoldColumn(false)]
        public int? encReciboId
        {
            get;
            set;
        }
         [ScaffoldColumn(false)]
        public DateTime encReciboFecha
        {
            get;
            set;
        }
        [DisplayName("Observación")]
        public string encReciboObservacion
        {
            get;
            set;
        }

        public bool? encReciboAnulado { get; set; }

        [DisplayName("Zona")]
        [UIHint("GridForeignKey")]
        public int? cszId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string cszNombre
        {
            get;
            set;
        }

    }
}
