using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MockAbiANS.DTOs.Peticao
{
    public class InformacoesAdicionaisDTO
    {
        public long NumeroAtendimento { get; set; }
        public string CompetenciaAtendimento { get; set; }
        public string DataFimAtendimento { get; set; }
    }
}