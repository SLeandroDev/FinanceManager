using FinanceManager.Application.Services;
using FinanceManager.Domain.Entities;
using FinanceManager.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FinanceManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        // GET: api/usuario
        [HttpGet]
        public async Task<IActionResult> GetUsuarios()
        {
            try
            {
                var usuarios = await _usuarioService.GetAllUsuariosAsync();
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                int statusCode = ExceptionHandler.GetStatusCodeForException(ex);
                return StatusCode(statusCode, ex.Message);
            }
        }

        // GET: api/usuario/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsuario(int id)
        {
            try
            {
                var usuario = await _usuarioService.GetUsuarioByIdAsync(id);
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                int statusCode = ExceptionHandler.GetStatusCodeForException(ex);
                return StatusCode(statusCode, ex.Message);
            }
            
        }

        // POST: api/usuario
        [HttpPost]
        public async Task<IActionResult> PostUsuario([FromBody] Usuario usuario)
        {
            try
            {
                var createdUsuario = await _usuarioService.AddUsuarioAsync(usuario);
                return CreatedAtAction(nameof(GetUsuario), new { id = createdUsuario.Id }, createdUsuario);
            }
            catch (Exception ex)
            {
                int statusCode = ExceptionHandler.GetStatusCodeForException(ex);
                return StatusCode(statusCode, ex.Message);
            }

        }

        // PUT: api/usuario/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, [FromBody] Usuario usuario)
        {

            var usuarios = await _usuarioService.GetAllUsuariosAsync();
            bool isExisteUsuario = usuarios.Any(u => u.Id == id);

            if (!isExisteUsuario)
            {
                return NotFound("Usuário não encontrado para atualização");
            }
            
            if (usuario == null) {

                return BadRequest("Favor preencher o JSON para requisição");
            }


            var updatedUsuario = await _usuarioService.UpdateUsuarioAsync(usuario, id);
            if (updatedUsuario == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/usuario/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            try
            {
                await _usuarioService.DeleteUsuarioAsync(id);
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
