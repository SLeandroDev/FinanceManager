using FinanceManager.Domain.Entities;
using FinanceManager.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Infrastructure.Persistence
{
    public class FinanceManagerDbContext : DbContext
    {
        public FinanceManagerDbContext(DbContextOptions<FinanceManagerDbContext> options)
            : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Conta> Contas { get; set; }
        public DbSet<Transacao> Transacoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Conta>()
        .HasOne(c => c.Usuario)  // Cada Conta tem um Usuario
        .WithMany(u => u.Contas)  // Um Usuario pode ter muitas Contas
        .HasForeignKey(c => c.UsuarioId);  // Chave estrangeira na entidade Conta

            // Relacionamento entre Transacao e Conta
            modelBuilder.Entity<Transacao>()
                .HasOne<Conta>()  // Cada Transacao está vinculada a uma Conta
                .WithMany()  // Não precisa navegar de volta para Transacao a partir de Conta
                .HasForeignKey(t => t.ContaId);  // Chave estrangeira na entidade Transacao
        }
    }
}
