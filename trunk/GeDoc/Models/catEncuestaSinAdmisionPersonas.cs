/************************************************************************************************************
 * Descripción: Clase referente a la estructura de EncuestaAPS
 * Observaciones: 
 * Creado por: Nombre y Apellido
 * Historial de Revisiones:
 * -----------------------------------------------------------------------------------------------
 * Fecha        Autor                   Descripción
 * -----------------------------------------------------------------------------------------------
 * 14/01/2016	Rolando Delgado 		Implementación inicial
 * -----------------------------------------------------------------------------------------------
*/

using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace GeDoc
{
    [KnownType(typeof(catEncuestaSinadmisionPersonas))]
    [Serializable()]
    public class catEncuestaSinadmisionPersonas : EntityObject
    {
      
        [ScaffoldColumn(false)]
        public long? pacId
        {
            get;
            set;
        }
        [ScaffoldColumn(false)]
        public catEspecialidades Especialidades
        {
            get;
            set;
        }
        [ScaffoldColumn(false)]
        public short? TipoDocumento
        {
            get;
            set;
        }
        [ScaffoldColumn(false)]
        public int? Documento
        {
            get;
            set;
        }
        [ScaffoldColumn(false)]
        public string ApellidoyNombre
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public int? enfId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public int? encId
        {
            get;
            set;
        }
        [ScaffoldColumn(false)]
        public string UsuariodeCarga
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public int? usrId
        {
            get;
            set;
        }
        [ScaffoldColumn(false)]
        public string fechadeCarga
        {
            get;
            set;
        }
        [ScaffoldColumn(false)]
        public DateTime? encFechaCarga
        {
            get;
            set;
        }


        [ScaffoldColumn(false)]
        public int? plaId
        {
            get;
            set;
        }
        //[ScaffoldColumn(false)]
        //public string espNombre
        //{
        //    get;
        //    set;
        //}
        [ScaffoldColumn(false)]
        public int? encCausa
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public int? cantesp
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string causaNombre
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public int? derivado
        {
            get;
            set;
        }
        [ScaffoldColumn(false)]
        public int? tranId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string EncuestadorApellidoyNombre
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string AtendidoLocal
        {
            get;
            set;
        }
        [ScaffoldColumn(false)]
        public string tipoDescripcion
        {
            get;
            set;
        }
    }
}