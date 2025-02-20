using FinanceManager.Domain.Entities;
using FinanceManager.Domain.Model;
using FinanceManager.Domain.Exceptions;
using FinanceManager.Infrastructure.Persistence;
using FinanceManager.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace FinanceManager.Application.Services
{
    public class ContaService : IContaService
    {
        private readonly IContaRepository _contaRepository;
        private readonly FinanceManagerDbContext _context;

        public ContaService(IContaRepository ContaRepository, FinanceManagerDbContext context)
        {
            _contaRepository = ContaRepository;
            _context = context;
        }

        public async Task<Conta> GetContaByIdAsync(int id)
        {
            var isExisteConta = await _contaRepository.GetByIdAsync(id);

            if (isExisteConta == null)
            {
                throw new NotFoundException("Conta não existe!");
            }

            return isExisteConta;

        }

        public async Task<IEnumerable<Conta>> GetAllContasAsync()
        {
            var isExisteContas = await _contaRepository.GetAllAsync();

            if (isExisteContas == null)
            {
                throw new NotFoundException("Nenhuma conta encontrada!");
            }
            return isExisteContas;
        }

        public async Task<Conta> AddContaAsync(ContaModel conta)
        {
            var usuario = await _contaRepository.GetUsuarioByIdAsync(conta.UsuarioId);

            if (usuario == null)
            {
                throw new NotFoundException("Usuário informado no JSON não encontrado");
            }

            if (conta.Banco == "")
            {
                throw new NotFoundException("Banco deve ser preenchido!");
            }
            var contaNova = new Conta
            {
                Banco = conta.Banco,
                Saldo = conta.Saldo,
                UsuarioId = conta.UsuarioId,
            };


            return await _contaRepository.AddAsync(contaNova);
        }

        public async Task<Conta> UpdateContaAsync(ContaModel conta, int id)
        {
           
            var contaExistente = await _contaRepository.GetByIdAsync(id);

            if (contaExistente == null)
            {
                throw new NotFoundException("Conta não encontrada para atualização!");
            }

            if (contaExistente.UsuarioId != conta.UsuarioId)
            {
                throw new BusinessException("Não é possível trocar o usuário da conta!");
            }

            contaExistente.Banco = conta.Banco;
            contaExistente.Saldo = conta.Saldo;

            // Converte para ContaModel antes de enviar para o repositório
            var contaAtualizada = new ContaModel
            {
                
                Banco = contaExistente.Banco,
                Saldo = contaExistente.Saldo,
                UsuarioId = contaExistente.UsuarioId
            };


            return await _contaRepository.UpdateAsync(contaAtualizada, id);

        }

        public async Task<bool> DeleteContaAsync(int id)
        {
            //TODO: Colocar mais regras para deletar

            var deleted = await _contaRepository.DeleteAsync(id);
            if (!deleted)
            {
                throw new NotFoundException("Conta não encontrada!");
            }
            return deleted;

        }

        public async Task<Conta> GetByBancoAsync(string banco)
        {
            return await _context.Contas
                                 .FirstOrDefaultAsync(c => c.Banco == banco);
        }
    }
}
