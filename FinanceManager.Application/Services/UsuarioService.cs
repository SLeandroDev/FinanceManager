using FinanceManager.Domain.Entities;
using FinanceManager.Domain.Exceptions;
using FinanceManager.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using Newtonsoft.Json.Linq;

namespace FinanceManager.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IConfiguration _configuration;

        public UsuarioService(IUsuarioRepository usuarioRepository, IConfiguration configuration)
        {
            _usuarioRepository = usuarioRepository;
            _configuration = configuration;
        }

        public async Task<Object> LoginAsync(string email, string password)
        {
            var isEmailValido = ValidarEmail(email);

            if (!isEmailValido)
            {
                throw new BusinessException("O e-mail informado deve estar formatado corretamente(conter @ e .com)!");
            }

            var usuario = await _usuarioRepository.GetByEmailAsync(email);

            if (usuario == null)
            {
                throw new NotFoundException("Usuário não existe!");
            }

            

            var isSenhaCorreta = PasswordHasher.VerifyPassword(usuario.PasswordHash, password);

            if (usuario == null || !isSenhaCorreta)
            {
                throw new BusinessException("E-mail ou senha inválidos.");
            }

            var token = GenerateJwtToken(usuario);
            var nome = usuario.Nome;
            var idUser = usuario.Id.ToString();
            return new { token, nome, idUser };
        }

        private string GenerateJwtToken(Usuario usuario)
        {
            var key = Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]);
            int expirationInMinutes = Convert.ToInt32(_configuration["JwtSettings:ExpirationInMinutes"]);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.Name, usuario.Nome),
                new Claim("iss", _configuration["JwtSettings:Issuer"]), // Adicionando o Issuer
                new Claim("aud", _configuration["JwtSettings:Audience"])  // Adicionando o Audience
            };


            // Criando o token com as configurações e a expiração ajustada
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(expirationInMinutes), // Usando UtcNow e a expiração configurada
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public async Task<Usuario> GetUsuarioByIdAsync(int id)
        {
            var isExisteUsuario = await _usuarioRepository.GetByIdAsync(id);

            if (isExisteUsuario == null)
            {
                throw new NotFoundException("Usuário não existe!");
            }

            return isExisteUsuario;
        }

        public async Task<IEnumerable<Usuario>> GetAllUsuariosAsync()
        {
            var isExisteUsuarios = await _usuarioRepository.GetAllAsync();

            if (isExisteUsuarios == null)
            {
                throw new NotFoundException("Nenhum usuário encontrada!");
            }
            return isExisteUsuarios;
        }

        public async Task<Usuario> AddUsuarioAsync(Usuario usuario)
        {
            usuario.PasswordHash = PasswordHasher.HashPassword(usuario.PasswordHash);

            if (usuario.Nome == "" || usuario.Email == "")
            {
                throw new BusinessException("Nome e E-mail do usuário devem ser preenchidos");
            }

            var isEmailValido = ValidarEmail(usuario.Email);

            if (!isEmailValido)
            {
                throw new BusinessException("O e-mail informado deve ser válido!");
            }

            return await _usuarioRepository.AddAsync(usuario);
        }

        public async Task<Usuario> UpdateUsuarioAsync(Usuario usuario, int id)
        {
            var usuarioExistente = await _usuarioRepository.GetByIdAsync(id);

            usuarioExistente.Nome = usuario.Nome;
            usuarioExistente.Email = usuario.Email;
            usuarioExistente.PasswordHash = usuario.PasswordHash;

            return await _usuarioRepository.UpdateAsync(usuarioExistente);
        }

        public async Task<bool> DeleteUsuarioAsync(int id)
        {
            //TODO: Colocar mais regras para deletar
            var deleted = await _usuarioRepository.DeleteAsync(id);
            if (!deleted)
            {
                throw new NotFoundException("Usuário não encontrado!");
            }
            return deleted;

        }

        private static bool ValidarEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }

    }
}
