namespace MockAbiANS.DTOs.Peticao
{
    public class DocumentoResponse
    {
        public string Numero { get; set; }
        public TipoDocumentoResponse TipoDocumento { get; set; }
        public string Assunto { get; set; }
        public DateTime DataDocumento { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public ArquivoResponse Arquivo { get; set; }
    }
}
