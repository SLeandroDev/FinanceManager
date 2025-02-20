namespace FinanceManager.API.Models
{
    public class ContaModel
    {
        public string Banco { get; set; } = string.Empty;
        public decimal Saldo { get; set; }
        public int UsuarioId { get; set; }
    }
}
