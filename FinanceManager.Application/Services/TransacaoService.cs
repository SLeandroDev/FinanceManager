using FinanceManager.Application.DTOs;
using FinanceManager.Domain.Entities;
using FinanceManager.Domain.Exceptions;
using FinanceManager.Domain.Model;
using FinanceManager.Infrastructure.Repositories;
using static System.Net.Mime.MediaTypeNames;

namespace FinanceManager.Application.Services
{
    public class TransacaoService : ITransacaoService
    {
        private readonly ITransacaoRepository _transacaoRepository;

        public TransacaoService(ITransacaoRepository transacaoRepository)
        {
            _transacaoRepository = transacaoRepository;
        }

        public async Task<Transacao> GetTransacaoByIdAsync(int id)
        {
            var isExisteTransacao = await _transacaoRepository.GetByIdAsync(id);

            if (isExisteTransacao == null)
            {
                throw new NotFoundException("Transação não existe");
            }

            return isExisteTransacao;
        }

        public async Task<IEnumerable<Transacao>> GetAllTransacaosAsync()
        {

            var isExisteTransacoes = await _transacaoRepository.GetAllAsync();

            if (isExisteTransacoes == null)
            {
                throw new NotFoundException("Nenhuma trasanção encontrada!");
            }
            return isExisteTransacoes;
        }

        public async Task<IEnumerable<TransacaoDTO>> GetAllTransacaosComDetalhesAsync()
        {
            var transacoes = await _transacaoRepository.GetAllAsync();

            if (transacoes == null || !transacoes.Any())
            {
                throw new NotFoundException("Nenhuma transação encontrada!");
            }

            var transacoesComDetalhes = new List<TransacaoDTO>();

            foreach (var transacao in transacoes)
            {
                var usuario = await _transacaoRepository.GetUsuarioByIdAsync(transacao.UsuarioId);
                var conta = await _transacaoRepository.GetContaByIdAsync(transacao.ContaId);

                if (usuario == null || conta == null)
                {
                    continue; // Ou você pode lançar uma exceção aqui caso não encontre
                }

                var transacaoDTO = new TransacaoDTO
                {
                    Id = transacao.Id,
                    Descricao = transacao.Descricao,
                    Valor = transacao.Valor,
                    DataTransacao = transacao.DataTransacao,
                    TipoTransacao = transacao.TipoTransacao,
                    CategoriaTransacao = transacao.CategoriaTransacao,
                    UsuarioNome = usuario.Nome,  // Assumindo que o nome do usuário é a propriedade 'Nome'
                    ContaNome = conta.Banco,  // Assumindo que o nome da conta é a propriedade 'Nome'
                    ContaId = transacao.ContaId,
                };

                transacoesComDetalhes.Add(transacaoDTO);
            }


            return transacoesComDetalhes;
        }

        public async Task<Transacao> AddTransacaoAsync(TransacaoModel transacao)
        {
            var usuario = await _transacaoRepository.GetUsuarioByIdAsync(transacao.UsuarioId);
            var conta = await _transacaoRepository.GetContaByIdAsync(transacao.ContaId);

            if (usuario == null) 
            {
                throw new NotFoundException("Usuário informado no JSON não encontrado");
            }
            if (conta == null)
            {
                throw new NotFoundException("Conta informada no JSON não encontrada");
            }

            var transacaoNova = new Transacao
            {
                Descricao = transacao.Descricao,
                Valor = transacao.Valor,
                DataTransacao = transacao.DataTransacao,
                TipoTransacao = transacao.TipoTransacao,
                CategoriaTransacao = transacao.CategoriaTransacao,
                UsuarioId = transacao.UsuarioId,
                ContaId = transacao.ContaId,
            };

            return await _transacaoRepository.AddAsync(transacaoNova);
        }

        public async Task<Transacao> UpdateTransacaoAsync(TransacaoModel transacao, int id)
        {

            var montaAtualizacaoTransacao = await _transacaoRepository.GetByIdAsync(id);

            if (montaAtualizacaoTransacao == null)
            {
                throw new NotFoundException("Transação não encontrada para atualização");
            }

            if (montaAtualizacaoTransacao.ContaId != transacao.ContaId)
            {
                throw new BusinessException("Não é possível alterar a conta bancária da transação");
            }

            if (montaAtualizacaoTransacao.UsuarioId != transacao.UsuarioId)
            {
                throw new BusinessException("Não é possível alterar o usuário da transação");
            }

            montaAtualizacaoTransacao.Descricao = transacao.Descricao;
            montaAtualizacaoTransacao.Valor = transacao.Valor;
            montaAtualizacaoTransacao.DataTransacao = transacao.DataTransacao;
            montaAtualizacaoTransacao.TipoTransacao = transacao.TipoTransacao;
            montaAtualizacaoTransacao.CategoriaTransacao = transacao.CategoriaTransacao;

            // Converte para ContaModel antes de enviar para o repositório
            var transacaoAtualizada = new TransacaoModel
            {
                Descricao = montaAtualizacaoTransacao.Descricao,
                Valor = montaAtualizacaoTransacao.Valor,
                DataTransacao = montaAtualizacaoTransacao.DataTransacao,
                TipoTransacao = montaAtualizacaoTransacao.TipoTransacao,
                CategoriaTransacao = montaAtualizacaoTransacao.CategoriaTransacao,
                UsuarioId = montaAtualizacaoTransacao.UsuarioId,
                ContaId = montaAtualizacaoTransacao.ContaId,
            };
            return await _transacaoRepository.UpdateAsync(transacaoAtualizada, id);
        }

        public async Task<bool> DeleteTransacaoAsync(int id)
        {
            //TODO: Colocar mais regras para deletar, talvez um status pra ver se deve ou não deletar a transação
            var deleted = await _transacaoRepository.DeleteAsync(id);
            if (!deleted)
            {
                throw new NotFoundException("Transação não encontrada!");
            }
            return deleted;
        }
    }
}
