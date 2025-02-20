using Microsoft.EntityFrameworkCore;
using FinanceManager.Domain.Entities;
using FinanceManager.Infrastructure.Persistence;
using FinanceManager.Domain.Model;
using static System.Net.Mime.MediaTypeNames;

namespace FinanceManager.Infrastructure.Repositories
{
    public class TransacaoRepository : ITransacaoRepository
    {
        private readonly FinanceManagerDbContext _context;

        public TransacaoRepository(FinanceManagerDbContext context)
        {
            _context = context;
        }

        public async Task<Transacao> GetByIdAsync(int id)
        {
            return await _context.Transacoes.FindAsync(id);
        }

        public async Task<Conta> GetContaByIdAsync(int id)
        {
            return await _context.Contas.FindAsync(id);
        }

        public async Task<Usuario> GetUsuarioByIdAsync(int id)
        {
            return await _context.Usuarios.FindAsync(id);
        }

        public async Task<IEnumerable<Transacao>> GetAllAsync()
        {
            return await _context.Transacoes.ToListAsync();
        }

        public async Task<Transacao> AddAsync(Transacao transacao)
        {
            _context.Transacoes.Add(transacao);
            await _context.SaveChangesAsync();
            return transacao; // Retorne a transação criada.
        }

        public async Task<Transacao> UpdateAsync(TransacaoModel transacao, int id)
        {
            var transacaoExistente = await _context.Transacoes.FindAsync(id);

            transacaoExistente.Descricao = transacao.Descricao;
            transacaoExistente.Valor = transacao.Valor;
            transacaoExistente.DataTransacao = transacao.DataTransacao;
            transacaoExistente.TipoTransacao = transacao.TipoTransacao;
            transacaoExistente.CategoriaTransacao = transacao.CategoriaTransacao;
            transacaoExistente.UsuarioId = transacao.UsuarioId;
            transacaoExistente.ContaId = transacao.ContaId;

            _context.Transacoes.Update(transacaoExistente);
            await _context.SaveChangesAsync();
            return transacaoExistente; // Retorne a transação atualizada.
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var transacao = await _context.Transacoes.FindAsync(id);
            if (transacao == null)
            {
                return false; // Retorna falso se a transação não for encontrada.
            }

            _context.Transacoes.Remove(transacao);
            await _context.SaveChangesAsync();
            return true; // Retorna verdadeiro se a exclusão for bem-sucedida.
        }
    }
}
