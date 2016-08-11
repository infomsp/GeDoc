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
    using GeDoc.Models;
    [KnownType(typeof(catEncuestasAPS))]
    [Serializable()]
    public class catEncuestasAPS : EntityObject
    {

        [ScaffoldColumn(false)]
        public int encId
        {
            get;
            set;
        }
        //[DisplayName("Encuesta Nº")]
        //public String encEncuestaTexto
        //{
        //    get;
        //    set;
        //}

        [ScaffoldColumn(false)]
        public Int16? depId
        {
            get;
            set;
        }

        [UIHint("GridForeignKeyComboBoxOnChange")]
        [DisplayName("Departamento:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe Seleccionar Depto.")]
        public Int16? depEncId
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
        [UIHint("GridForeignKeyComboBox")]       
        [DisplayName("Localidad:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe Seleccionar Loc.")]
        public Int16? locEncId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int16? locId
        {
            get;
            set;
        }
        [UIHint("GridForeignKeyComboBox")]       
        [DisplayName("Barrio:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe Seleccionar Barrio")]
        public Int32 barEncId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string barNombre
        {
            get;
            set;
        }

  
        [DisplayName("Calle:")]
        public string encDomCalle
        {
            get;
            set;
        }

        
        [DisplayName("Número:")]
        public Int16? encDomNro
        {
            get;
            set;
        }

   
        [DisplayName("Manzana:")]
        public string encDomMzna
        {
            get;
            set;
        }


        [DisplayName("Sector:")]
        public string encDomSector
        {
            get;
            set;
        }

        [DisplayName("Monoblock:")]
        public string encDomMonoblock
        {
            get;
            set;
        }

     
        [DisplayName("Piso:")]
        public string encDomPiso
        {
            get;
            set;
        }


        [DisplayName("Dpto:")]
        public string encDomDpto
        {
            get;
            set;
        }

        [DisplayName("Tel. Fijo:")]
        public string encTelFijo
        {
            get;
            set;
        }


        [DisplayName("Tel. Celular:")]
        public string encTelCel
        {
            get;
            set;
        }


        [UIHint("GridForeignKeyComboBox")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe Seleccionar Centro de Salud.")]
        [DisplayName("Centro de Salud Cercano:")]
        public Int16 csEncId
        {
            get;
            set;
        }



        [ScaffoldColumn(false)]
        public string csNombre
        {
            get;
            set;
        }

       [UIHint("GridForeignKeyComboBox")]
      [DisplayName("Encuestador:")]
      [Required(AllowEmptyStrings = false, ErrorMessage = "Debe Seleccionar Encuestador")]
        public int? encuestadorId
        {
            get;
            set;
        }      
        [ScaffoldColumn(false)]
       public string encuestadorNombre
       {
           get;
           set;
       }

         [DisplayName("Fecha que retira:")]
         [Required(AllowEmptyStrings = false, ErrorMessage = "Debe Ingresar Fecha.")]
         [DataType(DataType.Date)]
            public DateTime? encFechaRetira 
          {
              get;
              set;
          }
      
          [ScaffoldColumn(false)]
          [DataType(DataType.Date)]          
         public DateTime? encFechaCarga
          {
              get;
              set;
          }
          [ScaffoldColumn(false)]
          public string zonaNombre
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
        public string usrNombre
        {
            get;
            set;
        }
        [ScaffoldColumn(false)]
        public int? cantidad
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public int? encRedesPuntaje
        {
            get;
            set;
        }
    }
}