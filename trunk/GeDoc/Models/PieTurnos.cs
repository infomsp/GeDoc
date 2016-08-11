namespace GeDoc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel;
    using Microsoft.Web.Mvc;

    [KnownType(typeof(PieTurnos))]
    [Serializable()]
    public class PieTurnos
    {
        [ScaffoldColumn(false)]
        public Int64 turId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public int aghId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public DateTime turFecha
        {
            get;
            set;
        }
        [ScaffoldColumn(false)]
        public string turFechadelTurno
        {
            get;
            set;
        }
        [ScaffoldColumn(false)]
        public int hora
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        [DataType(DataType.Time)]
        public string horayminutos
        {
            get;
            set;
        }
        [ScaffoldColumn(false)]
        public bool turEsProgramado
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int16? tipoIdTipoDocumento
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string PacienteDescripcionTipoDocumento
        {
            get;
            set;
        }
        [ScaffoldColumn(false)]
        public Int64? pacNumeroDocumento
        {
            get;
            set;
        }
        [ScaffoldColumn(false)]
        [UIHint("GridForeignKey")]
        public Int64? pacId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string pacApellido
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string pacNombre
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int32 tipoId
        {
            get;
            set;
        }


          [DisplayName("Descripcion")]       
        public string tipoDescripcion
        {
            get;
            set;
        }

        [DisplayName("Estado Turno")]     
        public string turEstadoImagen
        {
            get;
            set;
        }
        [UIHint("GridForeignKeyComboBox")]
        [DisplayName("Admision del Turno:")]
        public Int32 tipoId_Admision
        {
            get;
            set;
        }
        [ScaffoldColumn(false)]
        public string tipoAdmisionDescripcion
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool turEsSobreturno
        {
            get;
            set;
        }
        [ScaffoldColumn(false)]
        public string turEsSobreturno_descripcion
        {
            get;
            set;
        }
        [ScaffoldColumn(false)]
        public string turEspecialidadNombre
        {
            get;
            set;
        }
        [ScaffoldColumn(false)]
        public string conApellidoyNombre
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string profApellidoyNombre
        {
            get;
            set;
        }
        [ScaffoldColumn(false)]
        public string _csNombre
        {
            get;
            set;
        }
        [ScaffoldColumn(false)]
        public string _usNombre
        {
            get;
            set;
        }
        [ScaffoldColumn(false)]
        public DateTime? turHoraAtendido
        {
            get;
            set;
        }
        [ScaffoldColumn(false)]
        public string _HoraAtendido
        {
            get;
            set;
        }
        [ScaffoldColumn(false)]
        public string nroHC
        {
            get;
            set;
        }
        [ScaffoldColumn(false)]
        public string turPrimUlt
        {
            get;
            set;
        }
        [ScaffoldColumn(false)]
        public string tipoSexoDescripcion
        {
            get;
            set;
        }
        [ScaffoldColumn(false)]
        public string Descripcion
        {
            get;
            set;
        }
        [DisplayName("Cantidad")]       
        public Int16 CantidadTurnos
        {
            get;
            set;
        }
    }
}