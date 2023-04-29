using MockAbiANS.Entities;

namespace MockAbiANS.Repository.Interface
{
    public interface IArquivoRepository
    {
        Task<Arquivo> GetByIdAsync(int id);
        Task<Arquivo> AddAsync(Arquivo arquivo);
        Task DeleteAsync(int id);
    }
}
