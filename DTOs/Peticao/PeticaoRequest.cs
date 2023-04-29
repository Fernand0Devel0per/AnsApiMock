using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MockAbiANS.Entities;

namespace MockAbiANS.DTOs.Peticao
{
    public class PeticaoRequest
    {
        public string NumeroProcesso { get; set; }
        public InformacoesAdicionaisDTO InformacoesAdicionais { get; set; }
    }
}