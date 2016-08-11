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

    [KnownType(typeof(catEspecialidades))]
    [Serializable()]
    public class catEspecialidades : EntityObject
    {
        [ScaffoldColumn(false)]
        public int espId
        {
            get;
            set;
        }

         [DisplayName("Agrupar:")]
        public bool espAgrup
        {
            get;
            set;
        }
        [DisplayName("Especialidad Padre:")]
        [UIHint("GridForeignKeyComboBoxNull")]
        public short? espIdPadre
        {
            get;
            set;
        }
          [ScaffoldColumn(false)]
        public string espNombrePadre
        {
            get;
            set;
        }
        [DisplayName("Nombre:")]
        [Required(ErrorMessage = "Debe especificar el Nombre")]
        public string espNombre
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public int Contratados
        {
            set;
            get;
        }
       

        // [ScaffoldColumn(false)]
        //public short? espIdPadre
        //{
        //    set;
        //    get;
        //}

        [DisplayName("Código:")]
        public string espCodigo
        {
            set;
            get;
        }

         [DisplayName("Descripción Planilla:")]
        public string espDescripcionPlanilla
        {
            set;
            get;
        }

        [DisplayName("Abreviatura:")]
        public string espAbreviatura
        {
            set;
            get;
        }

        [ScaffoldColumn(false)]
        public int espIdMHO
        {
            set;
            get;
        }

        public override bool Equals(object obj)
        {
            catEspecialidades objCatEspecialidades = obj as catEspecialidades;
            if (objCatEspecialidades == null)
                return false;
            return espId == objCatEspecialidades.espId;
        }

        public override int GetHashCode()
        {
            return espId;
        }
    }
}