using Microsoft.EntityFrameworkCore;
using MockAbiANS.Data;
using MockAbiANS.Entities;
using MockAbiANS.Repository.Interface;

namespace MockAbiANS.Repository
{

    public class InformacaoAdicionalRepository : IInformacaoAdicionalRepository
    {
        private readonly MockAnsDbContext _context;

        public InformacaoAdicionalRepository(MockAnsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<InformacaoAdicional>> GetAllAsync()
        {
            return await _context.InformacoesAdicionais.ToListAsync();
        }

        public async Task<InformacaoAdicional> GetByIdAsync(int id)
        {
            return await _context.InformacoesAdicionais.FindAsync(id);
        }

        public async Task<InformacaoAdicional> AddAsync(InformacaoAdicional informacaoAdicional)
        {
            _context.InformacoesAdicionais.Add(informacaoAdicional);
            await _context.SaveChangesAsync();
            return informacaoAdicional;
        }

        public async Task<InformacaoAdicional> UpdateAsync(InformacaoAdicional informacaoAdicional)
        {
            _context.InformacoesAdicionais.Update(informacaoAdicional);
            await _context.SaveChangesAsync();
            return informacaoAdicional;
        }

        public async Task DeleteAsync(int id)
        {
            var informacaoAdicional = await _context.InformacoesAdicionais.FindAsync(id);
            if (informacaoAdicional != null)
            {
                _context.InformacoesAdicionais.Remove(informacaoAdicional);
                await _context.SaveChangesAsync();
            }
        }
    }
  
}
