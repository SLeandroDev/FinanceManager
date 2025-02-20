using Microsoft.EntityFrameworkCore;
using FinanceManager.Domain.Entities;
using FinanceManager.Domain.Model;
using FinanceManager.Infrastructure.Persistence;
using FinanceManager.Domain.Model;
using FinanceManager.Domain.Exceptions;

namespace FinanceManager.Infrastructure.Repositories
{
    public class ContaRepository : IContaRepository
    {
        private readonly FinanceManagerDbContext _context;

        public ContaRepository(FinanceManagerDbContext context)
        {
            _context = context;
        }

        public async Task<Conta> GetByIdAsync(int id)
        {
            return await _context.Contas.FindAsync(id);
        }


        public async Task<IEnumerable<Conta>> GetAllAsync()
        {
            return await _context.Contas.ToListAsync();
        }

        public async Task<Conta> AddAsync(Conta conta)
        {
            
            _context.Contas.Add(conta);
            await _context.SaveChangesAsync();
            return conta; // Retorne a conta criada.
        }

        public async Task<Conta> UpdateAsync(ContaModel conta, int id)
        {
            var contaExistente = await _context.Contas.FindAsync(id);

            contaExistente.Banco = conta.Banco;
            contaExistente.Saldo = conta.Saldo;


            _context.Contas.Update(contaExistente);
            await _context.SaveChangesAsync();
            return contaExistente; // Retorne a conta atualizada.


            
            // Atualiza os valores sem criar uma nova entidade
            
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var conta = await _context.Contas.FindAsync(id);
            if (conta == null)
            {
                return false; // Retorna falso se a conta não for encontrada.
            }

            _context.Contas.Remove(conta);
            await _context.SaveChangesAsync();
            return true; // Retorna verdadeiro se a exclusão for bem-sucedida.
        }

        public async Task<Usuario> GetUsuarioByIdAsync(int id)
        {
            return await _context.Usuarios.FindAsync(id);
        }

        public async Task<Conta> GetByBancoAsync(string banco)
        {
            return await _context.Contas
                                 .FirstOrDefaultAsync(c => c.Banco == banco);
        }
    }
}
