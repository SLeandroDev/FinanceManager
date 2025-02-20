using FinanceManager.Domain.Entities;

namespace FinanceManager.Infrastructure.Repositories
{
    public interface IUsuarioRepository
    {
        Task<Usuario> GetByIdAsync(int id);
        Task<IEnumerable<Usuario>> GetAllAsync();
        Task<Usuario> AddAsync(Usuario usuario);
        Task<Usuario> UpdateAsync(Usuario usuario);
        Task<bool> DeleteAsync(int id);
        Task<Usuario> GetByEmailAsync(string email);
    }
}
