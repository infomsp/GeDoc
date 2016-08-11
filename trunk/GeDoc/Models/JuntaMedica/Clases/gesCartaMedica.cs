using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GeDoc.Models.JuntaMedica.Modelo;

namespace GeDoc
{
    public class gesCartaMedica
    {
        public getListaCMPorEstados_Result DatosCM { get; set; }
        public getDatosAgente_Result DatosAgente { get; set; }
        public List<getDatosAgenteGrupoFamiliar_Result> ListaGrupoFamiliar { get; set; }
        public List<getDatosAgenteReparticiones_Result> ListaReparticiones { get; set; }

        [UIHint("GridForeignKeyComboBoxLoad")]
        public int? espId { get; set; }

        public string TipoEnfermedad { get; set; }
        public string gesAccion { get; set; }
    }
}
