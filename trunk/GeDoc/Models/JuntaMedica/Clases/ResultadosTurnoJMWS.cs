using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GeDoc.Models.JuntaMedica.Modelo;

namespace GeDoc
{
    public class ResultadosTurnoJMWS : getListadoJuntasMedicas_Result
    {
        [UIHint("Editor")]
        public string Resultados { get; set; }
    }
}
