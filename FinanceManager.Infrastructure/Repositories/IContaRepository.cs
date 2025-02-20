using FinanceManager.Domain.Entities;
using FinanceManager.Domain.Model;
namespace FinanceManager.Infrastructure.Repositories
{
    public interface IContaRepository
    {
        Task<Conta> GetByIdAsync(int id);
        Task<IEnumerable<Conta>> GetAllAsync();
        Task<Conta> AddAsync(Conta conta);
        Task<Usuario> GetUsuarioByIdAsync(int id);
        Task<Conta> UpdateAsync(ContaModel conta, int id);
        Task<bool> DeleteAsync(int id);
        Task<Conta> GetByBancoAsync(string banco);
    }
}