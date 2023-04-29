using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MockAbiANS.Entities;

namespace MockAbiANS.DTOs.Peticao
{

    public class PeticaoResponse
    {
        public string Codigo { get; set; }
        public string NumeroProcesso { get; set; }
        public string CodOperadora { get; set; }
        public int TipoRegistro { get; set; }
        public int Situacao { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public InformacoesAdicionaisDTO InformacoesAdicionais { get; set; }
        public Link Link { get; set; }
    }
}