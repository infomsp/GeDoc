/************************************************************************************************************
 * Descripción: Clase referente a la estructura de Pacientes.
 * Observaciones: 
 * Creado por: Federico Rojas
 * Historial de Revisiones:
 * -----------------------------------------------------------------------------------------------
 * Fecha        Autor   Descripción
 * -----------------------------------------------------------------------------------------------
 * 06/08/2015   FAR     Implementación inicial.
 * -----------------------------------------------------------------------------------------------
*/

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace GeDoc.Models
{
    [KnownType(typeof(catDepartamentos))]
    [Serializable()]
    public class catDepartamentos : EntityObject
    {
        [UIHint("GridForeignKeyComboBox")]
        [DisplayName(@"Departamento")]
        public int depId { get; set; }

        [ScaffoldColumn(false)]
        public string depNombre { get; set; }

        [ScaffoldColumn(false)]
        public int provId { get; set; }


    }
}