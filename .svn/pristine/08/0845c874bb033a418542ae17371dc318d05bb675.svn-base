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

    [KnownType(typeof(PieImputaciones))]
    [Serializable()]
    public class PieImputaciones
    {
        [DisplayName("Descripción")]
        public string Descripcion
        {
            get;
            set;
        }

        [DisplayName("Bienes de Consumo")]
        [DataType(DataType.Currency)]
        public decimal ImporteBienesConsumo
        {
            get;
            set;
        }

        [DisplayName("Servicios No Personales")]
        [DataType(DataType.Currency)]
        public decimal ImporteServiciosNoPersonales
        {
            get;
            set;
        }

        [DisplayName("Bienes de Uso")]
        [DataType(DataType.Currency)]
        public decimal ImporteBienesDeUso
        {
            get;
            set;
        }

        [DisplayName("Transferencias")]
        [DataType(DataType.Currency)]
        public decimal ImporteTranferencias
        {
            get;
            set;
        }

        [DisplayName("Gastos en Personal")]
        [DataType(DataType.Currency)]
        public decimal ImporteGastosEnPersonal
        {
            get;
            set;
        }

        [DisplayName("S.D.D.O.P.")]
        [DataType(DataType.Currency)]
        public decimal ImporteSDDOP
        {
            get;
            set;
        }

        [DisplayName("Total")]
        [DataType(DataType.Currency)]
        public decimal ImporteTotal
        {
            get;
            set;
        }
    }
}