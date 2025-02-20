using FinanceManager.Domain.Entities;
using FinanceManager.Domain.Model;

namespace FinanceManager.Infrastructure.Repositories
{
    public interface ITransacaoRepository
    {
        Task<Transacao> GetByIdAsync(int id);
        Task<IEnumerable<Transacao>> GetAllAsync();
        Task<Transacao> AddAsync(Transacao transacao);
        Task<Transacao> UpdateAsync(TransacaoModel transacao, int id);
        Task<Conta> GetContaByIdAsync(int id);

        Task<Usuario> GetUsuarioByIdAsync(int id);
        Task<bool> DeleteAsync(int id);
    }
}
