using FinanceManager.Application.Services;
using FinanceManager.Domain.Entities;
using FinanceManager.Domain.Model;
using FinanceManager.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransacaoController : ControllerBase
    {
        private readonly ITransacaoService _transacaoService;

        public TransacaoController(ITransacaoService transacaoService)
        {
            _transacaoService = transacaoService;
        }

        // GET: api/Transacao
        [HttpGet]
        public async Task<IActionResult> GetTransacoes()
        {
            try
            {
                var transacoes = await _transacaoService.GetAllTransacaosAsync();
                return Ok(transacoes);
            }
            catch (Exception ex)
            {
                int statusCode = ExceptionHandler.GetStatusCodeForException(ex);
                return StatusCode(statusCode, ex.Message);
            }
        }

        // GET: api/Transacao
        [HttpGet("completas")]
        public async Task<IActionResult> GetTransacoesCompletas()
        {
            try
            {
                var transacoes = await _transacaoService.GetAllTransacaosComDetalhesAsync();
                return Ok(transacoes);
            }
            catch (Exception ex)
            {
                int statusCode = ExceptionHandler.GetStatusCodeForException(ex);
                return StatusCode(statusCode, ex.Message);
            }
        }

        // GET: api/Transacao/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransacao(int id)
        {
            try
            {

                var transacao = await _transacaoService.GetTransacaoByIdAsync(id);
                return Ok(transacao);
            }
            catch (Exception ex)
            {
                int statusCode = ExceptionHandler.GetStatusCodeForException(ex);
                return StatusCode(statusCode, ex.Message);
            }
        }

        // POST: api/Transacao
        [HttpPost]
        public async Task<IActionResult> PostTransacao([FromBody] TransacaoModel transacao)
        {
            try
            {
                var createdTransacao = await _transacaoService.AddTransacaoAsync(transacao);
                return CreatedAtAction(nameof(GetTransacao), new { id = createdTransacao.Id }, createdTransacao);
            }
            catch (Exception ex) 
            {
                int statusCode = ExceptionHandler.GetStatusCodeForException(ex);
                return StatusCode(statusCode, ex.Message);
            }
        }

        // PUT: api/Transacao/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransacao(int id, [FromBody] TransacaoModel transacao)
        {

            try
            {
                await _transacaoService.UpdateTransacaoAsync(transacao, id);
                return NoContent();
            }
            catch (Exception ex)
            {
                int statusCode = ExceptionHandler.GetStatusCodeForException(ex);
                return StatusCode(statusCode, ex.Message);
            }


        }

        // DELETE: api/Transacao/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransacao(int id)
        {
            try
            {
                await _transacaoService.DeleteTransacaoAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                int statusCode = ExceptionHandler.GetStatusCodeForException(ex);
                return StatusCode(statusCode, ex.Message);
            }
            
        }
    }
}
