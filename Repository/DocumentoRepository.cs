using Microsoft.EntityFrameworkCore;
using MockAbiANS.Data;
using MockAbiANS.Entities;
using MockAbiANS.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MockAbiANS.Repository
{
    public class DocumentoRepository : IDocumentoRepository
    {
        private readonly MockAnsDbContext _context;

        public DocumentoRepository(MockAnsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Documento>> GetAllAsync()
        {
            return await _context.Documentos
                .Include(d => d.TipoDocumento)
                .Include(d => d.Protocolo)
                .Include(d => d.Arquivo)
                .ToListAsync();
        }

        public async Task<Documento> GetByIdAsync(int id)
        {
            return await _context.Documentos
                .Include(d => d.TipoDocumento)
                .Include(d => d.Protocolo)
                .Include(d => d.Arquivo)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<Documento> AddAsync(Documento documento)
        {
            _context.Documentos.Add(documento);
            await _context.SaveChangesAsync();
            return documento;
        }

        public async Task<Documento> UpdateAsync(Documento documento)
        {
            _context.Update(documento);
            await _context.SaveChangesAsync();
            return documento;
        }

        public async Task DeleteAsync(int id)
        {
            var documento = await _context.Documentos.FindAsync(id);
            if (documento != null)
            {
                _context.Documentos.Remove(documento);
                await _context.SaveChangesAsync();
            }
        }
    }
}
