using FinanceManager.Filters;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using FinanceManager.API.Models;
using Microsoft.AspNetCore.Http;

namespace FinanceManager.Controllers
{
    [ServiceFilter(typeof(AuthFilter))]
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;

        // Injeta o HttpClient no controlador
        public HomeController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        

        public IActionResult Login()
        {
            // Verifica se o usuário já está autenticado
            var token = HttpContext.Session.GetString("AuthToken");

            if (!string.IsNullOrEmpty(token))
            {
                // Redireciona para a página principal caso o usuário já tenha um token
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var loginData = new { email, password };
            var content = new StringContent(JsonConvert.SerializeObject(loginData), Encoding.UTF8, "application/json");

            // Envia a requisição POST para a API de autenticação
            var response = await _httpClient.PostAsync("https://localhost:7241/api/auth/login", content);

            if (!response.IsSuccessStatusCode)
            {
                // Caso as credenciais sejam inválidas
                ViewData["Error"] = "Credenciais inválidas.";
                return View();
            }

            // Lê o corpo da resposta e desserializa o JSON para um objeto dinâmico
            var responseBody = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<LoginResponse>(responseBody);

            if (responseObject == null || string.IsNullOrEmpty(responseObject.Token))
            {
                ViewData["Error"] = "Token inválido.";
                return View();
            }

            // Recupera o token e o nome do usuário da resposta
            string token = responseObject.Token;
            string usuarioId = responseObject.idUser;
            string nomeDoUsuario = responseObject.Nome ?? "Usuário"; // Caso o nome venha nulo, usa um padrão


            // Armazena os dados na sessão
            HttpContext.Session.SetString("Token", token);
            HttpContext.Session.SetString("NomeDoUsuario", nomeDoUsuario);
            HttpContext.Session.SetString("UsuarioId", usuarioId);

            Console.WriteLine($"✅ Login bem-sucedido! Nome: {responseObject.Nome}");
            // Redireciona para a página principal
            return RedirectToAction("Index", "Home");
        }


        public IActionResult Index()
        {
            // Recupera dados da sessão
            var nomeUsuario = HttpContext.Session.GetString("NomeDoUsuario");

            // Passando para a View
            ViewData["NomeDoUsuario"] = nomeUsuario;

            return View();
        }

        public IActionResult Logout()
        {
            // Remove os dados de autenticação da sessão
            HttpContext.Session.Remove("Token");
            HttpContext.Session.Remove("NomeDoUsuario");

            // Redireciona para a página de Login
            return RedirectToAction("Login", "Home");
        }

        public IActionResult Contas()
        {
            // Recupera dados da sessão
            var usuarioId = HttpContext.Session.GetString("UsuarioId");

            // Passando para a View
            ViewData["UsuarioId"] = usuarioId;
            return View();
        }

        public IActionResult Transacoes()
        {
            // Recupera dados da sessão
            var usuarioId = HttpContext.Session.GetString("UsuarioId");

            // Passando para a View
            ViewData["UsuarioId"] = usuarioId;
            return View();
        }


    }
}
