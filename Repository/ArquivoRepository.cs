using MockAbiANS.Data;
using MockAbiANS.Entities;
using MockAbiANS.Repository.Interface;

namespace MockAbiANS.Repository
{
    public class ArquivoRepository : IArquivoRepository
    {
        private readonly MockAnsDbContext _context;

        public ArquivoRepository(MockAnsDbContext context)
        {
            _context = context;
        }

        public async Task<Arquivo> GetByIdAsync(int id)
        {
            return await _context.Arquivos.FindAsync(id);
        }

        public async Task<Arquivo> AddAsync(Arquivo arquivo)
        {
            _context.Arquivos.Add(arquivo);
            await _context.SaveChangesAsync();
            return arquivo;
        }

        public async Task DeleteAsync(int id)
        {
            var arquivo = await _context.Arquivos.FindAsync(id);
            if (arquivo != null)
            {
                _context.Arquivos.Remove(arquivo);
                await _context.SaveChangesAsync();
            }
        }
    }

}


