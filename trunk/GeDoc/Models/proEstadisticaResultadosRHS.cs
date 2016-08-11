using System.Data;
using System.Diagnostics;
using System.IO;
using System.Transactions;
using NPOI.HSSF.UserModel;
using Telerik.Web.Mvc.Extensions;
namespace GeDoc
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel;
    

    [KnownType(typeof(proEstadisticaResultadosRHS))]
    [Serializable()]
    public class proEstadisticaResultadosRHS : EntityObject
    {
        [ScaffoldColumn(false)]
        public int resId
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

        [ScaffoldColumn(false)]
        public int? resDia
        {
            get;
            set;
        }


           [DisplayName("Hojas")]
           [Required(AllowEmptyStrings = true, ErrorMessage = "Debe ingresar un valor númerico")]
        public int? resNroHojas
        {
            get;
            set;
        }


           [DisplayName("Horas")]
           [Required(AllowEmptyStrings = true, ErrorMessage = "Debe ingresar un valor númerico")]
        public int? resHorasAtencion
        {
            get;
            set;
        }


         [DisplayName("N")]
         [Required(AllowEmptyStrings = true, ErrorMessage = "Debe ingresar un valor númerico")]
        public int? resCMNuevas
        {
            get;
            set;
        }

         [DisplayName("R")]
         [Required(AllowEmptyStrings = true, ErrorMessage = "Debe ingresar un valor númerico")]
        public int? resCMRepetidas
        {
            get;
            set;
        }

        [DisplayName("Menor de 1 año /Dias M:")]
        [Required(AllowEmptyStrings = true, ErrorMessage = "Debe ingresar un valor númerico")]
        public int? resMenos1DM
        {
            get;
            set;
        }

        [DisplayName("Menor de 1 año /Dias F:")]
        [Required(AllowEmptyStrings = true, ErrorMessage = "Debe ingresar un valor númerico")]
        public int? resMenos1DF
        {
            get;
            set;
        }

        [DisplayName("Menor de 1 año /Mes M:")]
        [Required(AllowEmptyStrings = true, ErrorMessage = "Debe ingresar un valor númerico")]
        public int? resMenos1MM
        {
            get;
            set;
        }

        [DisplayName("Menor de 1 año /Mes F:")]
        [Required(AllowEmptyStrings = true, ErrorMessage = "Debe ingresar un valor númerico")]
        public int? resMenos1MF
        {
            get;
            set;
        }

        [DisplayName("de 1 a 4 M:")]
        [Required(AllowEmptyStrings = true, ErrorMessage = "Debe ingresar un valor númerico")]
        public int? resde1a4M
        {
            get;
            set;
        }

        [DisplayName("de 1 a 4 F:")]
        [Required(AllowEmptyStrings = true, ErrorMessage = "Debe ingresar un valor númerico")]
        public int? resde1a4F
        {
            get;
            set;
        }

        [DisplayName("de 5 a 14 M:")]
        [Required(AllowEmptyStrings = true, ErrorMessage = "Debe ingresar un valor númerico")]
        public int? resde5a14M
        {
            get;
            set;
        }

       [DisplayName("de 5 a 14 F:")]
        public int? resde5a14F
        {
            get;
            set;
        }

        [DisplayName("de 15 a 19 M:")]
        [Required(AllowEmptyStrings = true, ErrorMessage = "Debe ingresar un valor númerico")]
        public int? resde15a19M
        {
            get;
            set;
        }

        [DisplayName("de 15 a 19 F:")]
        [Required(AllowEmptyStrings = true, ErrorMessage = "Debe ingresar un valor númerico")]
        public int? resde15a19F
        {
            get;
            set;
        }

        [DisplayName("de 20 a 39 M:")]
        [Required(AllowEmptyStrings = true, ErrorMessage = "Debe ingresar un valor númerico")]
        public int? resde20a39M
        {
            get;
            set;
        }

        [DisplayName("de 20 a 39 F:")]
        [Required(AllowEmptyStrings = true, ErrorMessage = "Debe ingresar un valor númerico")]
        public int? resde20a39F
        {
            get;
            set;
        }

        [DisplayName("de 40 a 69 M:")]
        [Required(AllowEmptyStrings = true, ErrorMessage = "Debe ingresar un valor númerico")]
        public int? resde40a69M
        {
            get;
            set;
        }

         [DisplayName("de 40 a 69 F:")]
         [Required(AllowEmptyStrings = true, ErrorMessage = "Debe ingresar un valor númerico")]
        public int? resde40a69F
        {
            get;
            set;
        }

        [DisplayName("70 y Mas M:")]
        [Required(AllowEmptyStrings = true, ErrorMessage = "Debe ingresar un valor númerico")]
        public int? resde70ymasM
        {
            get;
            set;
        }

        [DisplayName("70 y Mas F:")]
        [Required(AllowEmptyStrings = true, ErrorMessage = "Debe ingresar un valor númerico")]
        public int? resde70ymasF
        {
            get;
            set;
        }

        [DisplayName("Tot. Edades M:")]
        public int? resTotEdadesM
        {
            get;
            set;
        }

        [DisplayName("Tot. Edades F:")]
        public int? resTotEdadesF
        {
            get;
            set;
        }

        [DisplayName("Total:")]
        public int? resTotTotal
        {
            get;
            set;
        }

         [DisplayName("Tot. Control Embarazo:")]
         [Required(AllowEmptyStrings = true, ErrorMessage = "Debe ingresar un valor númerico")]
        public int? resTotControlEmbarazo
        {
            get;
            set;
        }

         [DisplayName("Cant Prof:")]
         public int? resCantProf
         {
             get;
             set;
         }

         [DisplayName("Cant. Prof. C.E.:")]
         public int? resCantProfCE
         {
             get;
             set;
         }

    }
}