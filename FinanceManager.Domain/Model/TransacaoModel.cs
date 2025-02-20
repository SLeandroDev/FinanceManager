using FinanceManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Domain.Model
{
    public class TransacaoModel
    {
        public string Descricao { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public DateTime DataTransacao { get; set; }
        public TipoTransacao TipoTransacao { get; set; }
        public string CategoriaTransacao { get; set; } = string.Empty;

        // Relacionamento com a Conta, não com o Usuario diretamente
        public int ContaId { get; set; }  // Apenas a chave estrangeira para a Conta

        public int UsuarioId { get; set; }

    }
}
