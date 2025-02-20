using Microsoft.EntityFrameworkCore;
using FinanceManager.Domain.Entities;
using FinanceManager.Infrastructure.Persistence;

namespace FinanceManager.Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly FinanceManagerDbContext _context;

        public UsuarioRepository(FinanceManagerDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario> GetByIdAsync(int id)
        {
            return await _context.Usuarios.FindAsync(id);
        }

        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public async Task<Usuario> AddAsync(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario; // Retorne o usuário criado.
        }

        public async Task<Usuario> UpdateAsync(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
            return usuario; // Retorne o usuário atualizado.
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return false; // Retorna falso se o usuário não for encontrado.
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return true; // Retorna verdadeiro se a exclusão for bem-sucedida.
        }

        public async Task<Usuario> GetByEmailAsync(string email)
        {
            return await _context.Usuarios
                                 .FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
