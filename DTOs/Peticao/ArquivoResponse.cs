using MockAbiANS.Entities;

namespace MockAbiANS.DTOs.Peticao
{
    public class ArquivoResponse
    {
        public string Hash { get; set; }
        public string Nome { get; set; }
        public int Tamanho { get; set; }
        public Link Link { get; set; }
    }
}
