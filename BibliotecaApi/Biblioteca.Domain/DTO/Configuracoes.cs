using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Domain.DTO
{
    public class Configuracoes
    {
        public static IConfigurationRoot Configuration { get; set; }
    }
}
