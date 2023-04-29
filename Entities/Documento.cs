namespace MockAbiANS.Entities
{
    public class Documento
    {
        public int Id { get; set; }
        public string Numero { get; set; }
        public int ProtocoloId { get; set; }
        public Protocolo Protocolo { get; set; }
        public TipoDocumento TipoDocumento { get; set; }
        public string Assunto { get; set; }
        public DateTime DataDocumento { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public Arquivo Arquivo { get; set; }
    }
}
