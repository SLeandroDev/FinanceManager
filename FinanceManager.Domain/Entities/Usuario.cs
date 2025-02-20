using System.Text.Json.Serialization;

namespace FinanceManager.Domain.Entities
{
    public class Usuario
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;

        // Torna as coleções opcionais e não aparecerão no JSON
        [JsonIgnore]
        public ICollection<Transacao>? Transacoes { get; set; }

        [JsonIgnore]
        public ICollection<Conta>? Contas { get; set; }
    }
}