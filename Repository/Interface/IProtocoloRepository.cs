using MockAbiANS.Entities;

namespace MockAbiANS.Repository.Interface
{
    public interface IProtocoloRepository
    {
        Task<IEnumerable<Protocolo>> GetAllAsync();
        Task<Protocolo> GetByIdAsync(int id);
        Task<Protocolo> GetByCodigoAsync(string codigo);
        Task<Protocolo> AddAsync(Protocolo protocolo);
        Task<Protocolo> UpdateAsync(Protocolo protocolo);
        Task DeleteAsync(int id);

    }
}
