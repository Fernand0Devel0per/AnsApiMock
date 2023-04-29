using MockAbiANS.DTOs.Peticao;
using System.Threading.Tasks;

namespace MockAbiANS.Service.Interface
{
    public interface IProtocoloService
    {
        Task<PeticaoResponse> CriarPeticao(int codOperadora, string tipoProtocolo, string assunto, PeticaoRequest peticaoRequest);
    }
}
