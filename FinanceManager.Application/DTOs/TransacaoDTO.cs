using FinanceManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Application.DTOs
{
    public class TransacaoDTO
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataTransacao { get; set; }
        public TipoTransacao TipoTransacao { get; set; }
        public string CategoriaTransacao { get; set; }
        public string UsuarioNome { get; set; }
        public string ContaNome { get; set; }
        public int ContaId { get; set; }
    }
}
