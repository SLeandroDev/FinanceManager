using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FinanceManager.Domain.Model
{
    public class ContaModel
    {
        
        public string Banco { get; set; } = string.Empty;
        public decimal Saldo { get; set; }
        public int UsuarioId { get; set; }
    }
}
