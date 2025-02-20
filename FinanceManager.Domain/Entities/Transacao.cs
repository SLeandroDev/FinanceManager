using System.Text.Json.Serialization;

namespace FinanceManager.Domain.Entities
{
    public class Transacao
    {
        
        public int Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public DateTime DataTransacao { get; set; }
        public TipoTransacao TipoTransacao { get; set; }
        public string CategoriaTransacao { get; set; } = string.Empty;

        // Relacionamento com a Conta, não com o Usuario diretamente
        public int ContaId { get; set; }  // Apenas a chave estrangeira para a Conta

        public int UsuarioId { get; set; }

        [JsonIgnore]  // Ignora a propriedade de navegação durante a serialização
        public Usuario? Usuario { get; set; }  // Propriedade de navegação para Usuario, mas não serializada
    }

    public enum TipoTransacao
    {
        Receita = 1,  
        Despesa = 2 
    }
}