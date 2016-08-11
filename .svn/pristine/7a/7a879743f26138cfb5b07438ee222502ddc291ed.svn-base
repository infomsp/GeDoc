/************************************************************************************************************
 * Descripción: Clase referente a la estructura de EncuestaAPS
 * Observaciones: 
 * Creado por: Nombre y Apellido
 * Historial de Revisiones:
 * -----------------------------------------------------------------------------------------------
 * Fecha        Autor                   Descripción
 * -----------------------------------------------------------------------------------------------
 * 26/03/2015	Rolando Delgado 		Implementación inicial
 * -----------------------------------------------------------------------------------------------
*/

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace GeDoc
{
    [KnownType(typeof(catEncuestaSinAdmisionEncuestadores))]
    [Serializable()]
    public class catEncuestaSinAdmisionEncuestadores : EntityObject
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
        [DisplayName("Origen")]
        public int? Origen
        {
            get;
            set;
        }

    }
}
