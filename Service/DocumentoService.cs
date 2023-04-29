using AutoMapper;
using MockAbiANS.DTOs.Peticao;
using MockAbiANS.Entities;
using MockAbiANS.Repository;
using MockAbiANS.Repository.Interface;
using MockAbiANS.Service.Interface;
using MockAbiANS.Util;
using MockAbiANS.Util.Exceptions;
using MockAbiANS.Util.Extension;
using System.Globalization;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace MockAbiANS.Service
{
    public class DocumentoService : IDocumentoService
    {
        private readonly IDocumentoRepository _documentoRepository;
        private readonly IProtocoloRepository _protocoloRepository;
        private readonly IMapper _mapper;

        public DocumentoService(IDocumentoRepository documentoRepository,
                                IProtocoloRepository protocoloRepository,
                                IMapper mapper)
        {
            _documentoRepository = documentoRepository;
            _protocoloRepository = protocoloRepository;
            _mapper = mapper;
        }

        public async Task<DocumentoResponse> ProcessarDocumentoAsync(int codOperadora, DocumentoRequest request, string codProtocolo)
        {

            var protocolo = await _protocoloRepository.GetByCodigoAsync(codProtocolo);

            string arquivoHash = CalcularHash(request.Arquivo);

            Validacoes(protocolo, arquivoHash, request.Hash, request.NomeArquivo);

            string numeroDocumento = GerarNumeroDocumento(codOperadora);

            int tamanhoArquivo = request.Arquivo.Length;

            Documento documento = new Documento
            {
                Numero = numeroDocumento,
                ProtocoloId = protocolo.Id,
                Assunto = request.Assunto,
                DataDocumento = request.DataDocumento?.ToDateTime("yyyy-MM-dd", CultureInfo.InvariantCulture).ToUniversalTime() ?? DateTime.UtcNow,
                DataCadastro = DateTime.UtcNow,
                DataAtualizacao = DateTime.UtcNow,
                TipoDocumento = new TipoDocumento
                {
                    Nome = "Petição",
                    Tipo = request.TipoDocumento ?? (int)TipoDocumentoEnum.DocumentoPrincipal,
                    Status = (int)StatusEnum.Ativo,
                    DataCadastro = DateTime.UtcNow
                },
                Arquivo = new Arquivo
                {
                    Hash = request.Hash,
                    Nome = request.NomeArquivo,
                    Tamanho = tamanhoArquivo
                }
            };

            var documentoSalvo = await _documentoRepository.AddAsync(documento);

            if (documentoSalvo is null)
            {
                throw new DatabaseOperationException("Erro ao salvar o documento no banco de dados.");
            }   


            return _mapper.Map<DocumentoResponse>(documentoSalvo);
        }

        private bool ValidarNomeArquivo(string nomeArquivo)
        {
            Regex regex = new Regex(@"^[\w\s\-.]+\.[A-Za-z]{2,4}$");
            return regex.IsMatch(nomeArquivo);
        }

        private string CalcularHash(byte[] arquivo)
        {
            using SHA256 sha256 = SHA256.Create();
            byte[] hashBytes = sha256.ComputeHash(arquivo);
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }

        private string GerarNumeroDocumento(int codOperadora)
        {
            Random random = new Random();
            int numeroAleatorio = random.Next(1000, 10000);
            return $"{codOperadora}-{numeroAleatorio}";
        }

        private void Validacoes(Protocolo protocolo,string arquivoHash, string hashRecebido, string nomeArquivo)
        {
            if (protocolo is null)
            {
                throw new ArgumentException("Protocolo Informado não encontrado.");
            }

            if (!ValidarNomeArquivo(nomeArquivo))
            {
                throw new ArgumentException("O nome do arquivo deve estar no formato 'nome.extensao'");
            }


            if (arquivoHash != hashRecebido)
            {
                throw new ArgumentException("O hash informado não corresponde ao hash do arquivo enviado.");
            }
        }
    }
}
