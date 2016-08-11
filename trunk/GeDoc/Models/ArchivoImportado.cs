using System.Collections.Generic;

namespace GeDoc
{
    using System.ComponentModel.DataAnnotations;

    public class ArchivoImportado
    {
        [ScaffoldColumn(false)]
        public List<ArchivoImportadoColumna> Columnas { get; set; }

        [ScaffoldColumn(false)]
        public List<ArchivoImportadoValor> Valores { get; set; }
    }
}
