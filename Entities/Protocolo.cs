using Newtonsoft.Json;

namespace MockAbiANS.Entities
{
    public class Protocolo
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string NumeroProcesso { get; set; }
        public string CodOperadora { get; set; }
        public int TipoRegistroId { get; set; }
        public TipoRegistro TipoRegistro { get; set; }
        public int SituacaoId { get; set; }
        public Situacao Situacao { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public InformacaoAdicional InformacoesAdicionais { get; set; }
        public List<Documento> Documentos { get; set; }
    }
}
