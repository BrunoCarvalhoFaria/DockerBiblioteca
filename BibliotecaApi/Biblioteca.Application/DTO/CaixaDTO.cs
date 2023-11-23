using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Application.DTO
{
    public class CaixaDTO
    {
        public long Id { get; set; }
        public DateTimeOffset DataAbertura { get; set; }
        public DateTimeOffset DataFechamento { get; set; }
        public string UsuarioId { get; set; }
        public float ValorAbertura { get; set; }
        public float ValorFechamento { get; set; }
        public float Sangria { get; set; }
        public float EntradaValor { get; set; }
        public float VendaCartao { get; set; }
    }
}
