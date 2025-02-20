using FinanceManager.Application.Services;
using FinanceManager.Domain.Entities;
using FinanceManager.Domain.Model;
using FinanceManager.Filters;
using FinanceManager.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContaController : ControllerBase
    {
        private readonly IContaService _contaService;

        public ContaController(IContaService ContaService)
        {
            _contaService = ContaService;
        }


        // GET: api/Conta
        [HttpGet]
        public async Task<IActionResult> GetContas()
        {
            try
            {
                var contas = await _contaService.GetAllContasAsync();
                return Ok(contas);
            }
            catch (Exception ex)
            {
                int statusCode = ExceptionHandler.GetStatusCodeForException(ex);
                return StatusCode(statusCode, ex.Message);
            }
           
        }

        // GET: api/Conta/5
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetConta(int id)
        {

            try
            {
                var conta = await _contaService.GetContaByIdAsync(id);
                return Ok(conta);
            }
            catch (Exception ex)
            {
                int statusCode = ExceptionHandler.GetStatusCodeForException(ex);
                return StatusCode(statusCode, ex.Message);
            }
           
        }

        // POST: api/Conta
        [HttpPost]
        public async Task<IActionResult> PostConta([FromBody] ContaModel conta)
        {
            try
            {
                var createdConta = await _contaService.AddContaAsync(conta);
                return CreatedAtAction(nameof(GetConta), new { id = createdConta.Id }, createdConta);
            }
            catch (Exception ex)
            {
                int statusCode = ExceptionHandler.GetStatusCodeForException(ex);
                return StatusCode(statusCode, ex.Message);
            }

        }

        // PUT: api/Conta/5?banco=""
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConta(int id, [FromBody] ContaModel conta)
        {

            try
            {
                await _contaService.UpdateContaAsync(conta, id);
                return NoContent();
            }
            catch (Exception ex)
            {
                int statusCode = ExceptionHandler.GetStatusCodeForException(ex);
                return StatusCode(statusCode, ex.Message);
            }


        }

        // DELETE: api/Conta/5?banco=""
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConta(int id)
        {
            try
            {
                await _contaService.DeleteContaAsync(id);
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
