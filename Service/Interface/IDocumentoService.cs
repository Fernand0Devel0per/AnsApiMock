using MockAbiANS.DTOs.Peticao;

namespace MockAbiANS.Service.Interface
{
    public interface IDocumentoService
    {
        Task<DocumentoResponse> ProcessarDocumentoAsync(int codOperadora, DocumentoRequest request, string codProtocolo);
    }
}
