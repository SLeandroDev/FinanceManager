
using System.Text.Json.Serialization;

namespace FinanceManager.Domain.Entities
{
    public class Conta
    {
       
        public int Id { get; set; }
        public string Banco { get; set; } = string.Empty;
        public decimal Saldo { get; set; }
        public int UsuarioId { get; set; }

        [JsonIgnore]  // Ignora a propriedade de navegação durante a serialização
        public Usuario? Usuario { get; set; }  // Propriedade de navegação para Usuario, mas não serializada

    }
}