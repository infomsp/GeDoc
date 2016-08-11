namespace GeDoc
{
    using System.ComponentModel.DataAnnotations;

    public class ArchivoImportadoValor
    {
        [ScaffoldColumn(false)]
        public string[] Valor { get; set; }
    }
}
