using FinanceManager.Domain.Entities;

namespace FinanceManager.Application.Services
{
    public interface IUsuarioService
    {
        Task<Usuario> GetUsuarioByIdAsync(int id);
        Task<IEnumerable<Usuario>> GetAllUsuariosAsync();
        Task<Usuario> AddUsuarioAsync(Usuario usuario);
        Task<Usuario> UpdateUsuarioAsync(Usuario usuario, int id);
        Task<bool> DeleteUsuarioAsync(int id);

        Task<Object> LoginAsync(string username, string password);
    }
}