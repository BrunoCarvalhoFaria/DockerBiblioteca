using Biblioteca.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Application.Services
{
    public class UtilsService : IUtilsService
    {
        public bool TodosPropriedadesPreenchidas(object obj)
        {
            var propriedades = obj.GetType().GetProperties();
            return propriedades.All(propriedade =>
            {
                var value = propriedade.GetValue(obj);

                return value != null && !string.IsNullOrEmpty(value.ToString()) && value.ToString() != "0";
            });
        }
    }
}
