using FinanceManager.Application.Services;
using FinanceManager.Application.DTOs;
using FinanceManager.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace FinanceManager.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public LoginController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var retorno = await _usuarioService.LoginAsync(request.Email, request.Password);
                return Ok(retorno);

            }
            catch (Exception ex)
            {
                int statusCode = ExceptionHandler.GetStatusCodeForException(ex);
                return StatusCode(statusCode, ex.Message);
            }
        }
    }
}
