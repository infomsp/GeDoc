/************************************************************************************************************
 * Descripción: Clase referente a la estructura de EncuestaAPSReciboDetalles
 * Observaciones: 
 * Creado por: Federico Rojas
 * Historial de Revisiones:
 * -----------------------------------------------------------------------------------------------
 * Fecha        Autor                   Descripción
 * -----------------------------------------------------------------------------------------------
 * 15/06/2015	Rojas Federico 		    Implementación inicial
 * -----------------------------------------------------------------------------------------------
*/

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace GeDoc
{
    [KnownType(typeof(catEncuestaAPSReciboDetalles))]
    [Serializable()]
    public class catEncuestaAPSReciboDetalles : EntityObject
    {
        
        [ScaffoldColumn(false)]
        public int? encReciboDetalleId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public int? encReciboId
        {
            get;
            set;
        }
        
        [DisplayName("Encuestador")]
        [UIHint("GridForeignKeyComboBox")]
        public int? encuestadorId
        {
            get;
            set;
        }

        [DisplayName("Departamento")]     
        [UIHint("GridForeignKey")]      
        public int? depId
        {
            get;
            set;
        }

        [DisplayName("Cantidad")]
        [Range(typeof(int),"0","999999")]
        [Required]
        public int? cantidad
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string encuestadorApyNom
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string depNombre
        {
            get;
            set;
        }

    }
}
