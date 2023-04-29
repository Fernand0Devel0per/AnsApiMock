using MockAbiANS.Entities;

namespace MockAbiANS.Repository.Interface
{
    public interface ITipoDocumentoRepository
    {
        Task<TipoDocumento> GetByIdAsync(int id);
        Task<TipoDocumento> AddAsync(TipoDocumento tipoDocumento);
        Task<TipoDocumento> UpdateAsync(TipoDocumento tipoDocumento);
        Task DeleteAsync(int id);
    }
}
