using MockAbiANS.DTOs.Peticao;
using MockAbiANS.Entities;
using MockAbiANS.Repository;
using MockAbiANS.Repository.Interface;
using MockAbiANS.Service.Interface;
using MockAbiANS.Util;
using MockAbiANS.Util.Extension;
using System.Globalization;

namespace MockAbiANS.Service
{
    public class ProtocoloService : IProtocoloService
    {
        private readonly IProtocoloRepository _protocoloRepository;
        private readonly IInformacaoAdicionalRepository _informacaoAdicionalRepository;


        public ProtocoloService(IProtocoloRepository protocoloRepository,
                                IInformacaoAdicionalRepository informacaoAdicionalRepository)
        {
            _protocoloRepository = protocoloRepository;
            _informacaoAdicionalRepository = informacaoAdicionalRepository;
        }

        public async Task<PeticaoResponse> CriarPeticao(int codOperadora, string tipoProtocolo, string assunto, PeticaoRequest peticaoRequest)
        {
            string codigo;
            do
            {
                codigo = GerarCodigoAleatorio();
            } while (await _protocoloRepository.GetByCodigoAsync(codigo) != null);

            var protocolo = FactoryProtocolo(peticaoRequest.NumeroProcesso, codOperadora.ToString(), codigo);
            var protocoloPersist = await _protocoloRepository.AddAsync(protocolo);

            var informacoesAdicionais = FactoryInformacaoAdicional(peticaoRequest, protocoloPersist);
            await _informacaoAdicionalRepository.AddAsync(informacoesAdicionais);


            return FactoryPeticaoResponse(codigo, 
                                          peticaoRequest, 
                                          codOperadora.ToString(), 
                                          protocolo.NumeroProcesso);
        }

        private static string GerarCodigoAleatorio()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, 8)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private Protocolo FactoryProtocolo(string numeroProcesso, string codOperadora, string codigo)
        {
            return new Protocolo
            {
                Codigo = codigo,
                NumeroProcesso = numeroProcesso,
                CodOperadora = codOperadora,
                TipoRegistroId = (int)TipoRegistroEnum.Peticao,
                SituacaoId = (int)SituacaoEnum.Andamento,
                DataCadastro = DateTime.Now.ToUniversalTime(),
                DataAtualizacao = DateTime.Now.ToUniversalTime()
            };
        }

        private InformacaoAdicional FactoryInformacaoAdicional(PeticaoRequest peticaoRequest, Protocolo protocolo)
        {
            return new InformacaoAdicional
            {
                Id = protocolo.Id,
                NumeroAtendimento = peticaoRequest.InformacoesAdicionais.NumeroAtendimento,
                CompetenciaAtendimento = peticaoRequest.InformacoesAdicionais.CompetenciaAtendimento,
                DataFimAtendimento = peticaoRequest.InformacoesAdicionais.DataFimAtendimento.ToDateTime("dd-MM-yyyy", CultureInfo.InvariantCulture).ToUniversalTime(),
                Protocolo = protocolo
            };
        }

        private PeticaoResponse FactoryPeticaoResponse(string codigo,
                                                       PeticaoRequest peticaoRequest,
                                                       string codOperadora,
                                                       string numeroDoProcesso)
        {
            return new PeticaoResponse
            {
                Codigo = codigo,
                NumeroProcesso = numeroDoProcesso,
                CodOperadora = codOperadora,
                TipoRegistro = (int)TipoRegistroEnum.Peticao,
                Situacao = (int)SituacaoEnum.Andamento,
                DataCadastro = DateTime.Now,
                DataAtualizacao = DateTime.Now,
                InformacoesAdicionais = peticaoRequest.InformacoesAdicionais,
                Link = null
            };
        }
    }
}
