/************************************************************************************************************
 * Descripción: Clase referente a la estructura de EncuestaAPS
 * Observaciones: 
 * Creado por: Nombre y Apellido
 * Historial de Revisiones:
 * -----------------------------------------------------------------------------------------------
 * Fecha        Autor                   Descripción
 * -----------------------------------------------------------------------------------------------
 * 26/03/2015	Rolando Delgado 		Implementación inicial
 * 11/06/2015   Rojas Federico          Actualizado para reflejar el modelo actual y su uso.
 * -----------------------------------------------------------------------------------------------
*/

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace GeDoc
{
    [KnownType(typeof(catEncuestaAPSEncuestadores))]
    [Serializable()]
    public class catEncuestaAPSEncuestadores : EntityObject
    {
        [ScaffoldColumn(false)]
        public int? encuestadorId
        {
            get;
            set;
        }
        [DisplayName("Documento")]
        public int? encuestadorDoc
        {
            get;
            set;
        }
        [DisplayName("Nombre")]
        public string encuestadorApyNom
        {
            get;
            set;
        }
        [DisplayName("Zona")]
        [UIHint("GridForeignKey")]
        public int? cszId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string repNombre
        {
            get;
            set;
        }

    }
}
