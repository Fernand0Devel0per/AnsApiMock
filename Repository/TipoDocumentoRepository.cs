using MockAbiANS.Data;
using MockAbiANS.Entities;
using MockAbiANS.Repository.Interface;

namespace MockAbiANS.Repository
{
    public class TipoDocumentoRepository : ITipoDocumentoRepository
    {
        private readonly MockAnsDbContext _context;

        public TipoDocumentoRepository(MockAnsDbContext context)
        {
            _context = context;
        }

        public async Task<TipoDocumento> GetByIdAsync(int id)
        {
            return await _context.TiposDocumento.FindAsync(id);
        }

        public async Task<TipoDocumento> AddAsync(TipoDocumento tipoDocumento)
        {
            _context.TiposDocumento.Add(tipoDocumento);
            await _context.SaveChangesAsync();
            return tipoDocumento;
        }

        public async Task<TipoDocumento> UpdateAsync(TipoDocumento tipoDocumento)
        {
            _context.Update(tipoDocumento);
            await _context.SaveChangesAsync();
            return tipoDocumento;
        }

        public async Task DeleteAsync(int id)
        {
            var tipoDocumento = await _context.TiposDocumento.FindAsync(id);
            if (tipoDocumento != null)
            {
                _context.TiposDocumento.Remove(tipoDocumento);
                await _context.SaveChangesAsync();
            }
        }
    }
}
