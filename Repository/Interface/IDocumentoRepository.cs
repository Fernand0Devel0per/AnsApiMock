using MockAbiANS.Entities;

namespace MockAbiANS.Repository.Interface
{
    public interface IDocumentoRepository
    {
        Task<IEnumerable<Documento>> GetAllAsync();
        Task<Documento> GetByIdAsync(int id);
        Task<Documento> AddAsync(Documento documento);
        Task<Documento> UpdateAsync(Documento documento);
        Task DeleteAsync(int id);
    }
}
