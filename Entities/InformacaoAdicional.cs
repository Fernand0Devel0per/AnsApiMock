using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MockAbiANS.Entities
{
    public class InformacaoAdicional
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public long NumeroAtendimento { get; set; }
        public string CompetenciaAtendimento { get; set; }
        public DateTime DataFimAtendimento { get; set; }
        public Protocolo Protocolo { get; set; }
    }
}
