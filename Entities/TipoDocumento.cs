using System.ComponentModel.DataAnnotations;

namespace MockAbiANS.Entities
{
    public class TipoDocumento
    {
        
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Status { get; set; }
        public int Tipo { get; set; }
        public DateTime DataCadastro { get; set; }
        public int DocumentoId { get; set; }
        public Documento Documento { get; set; }
    }
}
