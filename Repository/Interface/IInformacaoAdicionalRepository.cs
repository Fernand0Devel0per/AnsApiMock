using MockAbiANS.Entities;

namespace MockAbiANS.Repository.Interface
{
    public interface IInformacaoAdicionalRepository
    {
        Task<IEnumerable<InformacaoAdicional>> GetAllAsync();
        Task<InformacaoAdicional> GetByIdAsync(int id);
        Task<InformacaoAdicional> AddAsync(InformacaoAdicional informacaoAdicional);
        Task<InformacaoAdicional> UpdateAsync(InformacaoAdicional informacaoAdicional);
        Task DeleteAsync(int id);
    }
}
