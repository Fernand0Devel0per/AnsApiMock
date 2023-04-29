using Microsoft.EntityFrameworkCore;
using MockAbiANS.Data;
using MockAbiANS.Entities;
using MockAbiANS.Repository.Interface;

namespace MockAbiANS.Repository
{
    public class ProtocoloRepository : IProtocoloRepository
    {
        private readonly MockAnsDbContext _context;

        public ProtocoloRepository(MockAnsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Protocolo>> GetAllAsync()
        {
            return await _context.Protocolos.ToListAsync();
        }

        public async Task<Protocolo> GetByIdAsync(int id)
        {
            return await _context.Protocolos.FindAsync(id);
        }

        public async Task<Protocolo> GetByCodigoAsync(string codigo)
        {
            return await _context.Protocolos
                .Include(p => p.InformacoesAdicionais)
                .Include(p => p.TipoRegistro)
                .Include(p => p.Situacao)
                .FirstOrDefaultAsync(p => p.Codigo == codigo);
        }

        public async Task<Protocolo> AddAsync(Protocolo protocolo)
        {
            _context.Protocolos.Add(protocolo);
            await _context.SaveChangesAsync();
            return protocolo;
        }

        public async Task<Protocolo> UpdateAsync(Protocolo protocolo)
        {
            _context.Update(protocolo);
            await _context.SaveChangesAsync();
            return protocolo;
        }

        public async Task DeleteAsync(int id)
        {
            var protocolo = await _context.Protocolos.FindAsync(id);
            if (protocolo != null)
            {
                _context.Protocolos.Remove(protocolo);
                await _context.SaveChangesAsync();
            }
        }
    }
}
