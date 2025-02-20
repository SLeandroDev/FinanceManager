using FinanceManager.Domain.Entities;
using FinanceManager.Domain.Model;
using FinanceManager.Application.DTOs;

namespace FinanceManager.Application.Services
{
    public interface ITransacaoService
    {
        Task<Transacao> GetTransacaoByIdAsync(int id);
        Task<IEnumerable<Transacao>> GetAllTransacaosAsync();
        Task<Transacao> AddTransacaoAsync(TransacaoModel Transacao);
        Task<Transacao> UpdateTransacaoAsync(TransacaoModel Transacao, int id);
        Task<bool> DeleteTransacaoAsync(int id);

        Task<IEnumerable<TransacaoDTO>> GetAllTransacaosComDetalhesAsync();
    }
}