namespace MockAbiANS.DTOs.Peticao
{
    public class DocumentoRequest
    {
        public string Hash { get; set; }
        public string NomeArquivo { get; set; }
        public string Assunto { get; set; }
        public int? TipoDocumento { get; set; }
        public string DataDocumento { get; set; }
        public byte[] Arquivo { get; set; }
    }
}
