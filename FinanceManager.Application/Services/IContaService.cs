using FinanceManager.Domain.Entities;
using FinanceManager.Domain.Model;
namespace FinanceManager.Application.Services
{
    public interface IContaService
    {
        Task<Conta> GetContaByIdAsync(int id);
        Task<IEnumerable<Conta>> GetAllContasAsync();
        Task<Conta> AddContaAsync(ContaModel Conta);
        Task<Conta> UpdateContaAsync(ContaModel Conta, int id);
        Task<bool> DeleteContaAsync(int id);
        Task<Conta> GetByBancoAsync(string banco);
    }
}