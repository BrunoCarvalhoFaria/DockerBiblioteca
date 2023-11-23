using Biblioteca.Domain.Core.Models;
using Biblioteca.Domain.Entities.ApplicationUsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Domain.Entities.Caixas
{
    public class Caixa : Entity<Caixa>
    {
        public DateTimeOffset DataAbertura { get; set; }
        public DateTimeOffset DataFechamento { get; set; }
        public string UsuarioId { get; set; } = "";
        public float ValorAbertura { get; set; }
        public float ValorFechamento { get; set; }
        public float Sangria { get; set; }
        public float EntradaValor { get; set; }
        public float VendaCartao { get; set; }


        public virtual ApplicationUser Usuario { get; set; }


        public override bool EhValido()
        {
            return true;
        }
    }
}
