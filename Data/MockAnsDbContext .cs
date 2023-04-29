using Microsoft.EntityFrameworkCore;
using MockAbiANS.Entities;

namespace MockAbiANS.Data
{
    public class MockAnsDbContext : DbContext
    {
        public MockAnsDbContext(DbContextOptions<MockAnsDbContext> options)
            : base(options)
        {
        }


        public DbSet<Protocolo> Protocolos { get; set; }
        public DbSet<Situacao> Situacaos { get; set; }
        public DbSet<TipoRegistro> TiposRegistros { get; set; }
        public DbSet<InformacaoAdicional> InformacoesAdicionais { get; set; }
        public DbSet<Documento> Documentos { get; set; }
        public DbSet<TipoDocumento> TiposDocumento { get; set; }
        public DbSet<Arquivo> Arquivos { get; set; }

        public void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Situacao>().HasData(
                new Situacao { Id = 1, Descricao = "Andamento" }
            );

            modelBuilder.Entity<TipoRegistro>().HasData(
                new TipoRegistro { Id = 2, Descricao = "Petição (Encaminhado pela Operadora para a ANS)" }
            );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            SeedData(modelBuilder);

            modelBuilder.Entity<Documento>()
                .HasOne(d => d.Arquivo)
                .WithOne(a => a.Documento)
                .HasForeignKey<Arquivo>(a => a.DocumentoId)
                .OnDelete(DeleteBehavior.Cascade);  

            modelBuilder.Entity<Documento>()
                .HasOne(d => d.TipoDocumento)
                .WithOne(t => t.Documento)
                .HasForeignKey<TipoDocumento>(t => t.DocumentoId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Protocolo>()
                .HasOne(p => p.InformacoesAdicionais)
                .WithOne()
                .HasForeignKey<InformacaoAdicional>(ia => ia.Id);

            modelBuilder.Entity<Protocolo>()
              .HasOne(p => p.InformacoesAdicionais)
              .WithOne()
              .HasForeignKey<InformacaoAdicional>(ia => ia.Id)
              .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Documento>()
               .HasOne(d => d.Protocolo)
               .WithMany(p => p.Documentos)
               .HasForeignKey(d => d.ProtocoloId)
               .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
