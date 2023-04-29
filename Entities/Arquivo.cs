using System.ComponentModel.DataAnnotations;

namespace MockAbiANS.Entities
{
    public class Arquivo
    {
        public int Id { get; set; }
        public string Hash { get; set; }
        public string Nome { get; set; }
        public long Tamanho { get; set; }
        public int DocumentoId { get; set; }
        public Documento Documento { get; set; }
    }
}
