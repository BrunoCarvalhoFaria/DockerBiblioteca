using Biblioteca.Application.Interfaces;
using Biblioteca.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Biblioteca.TesteUnitario.Application.Services
{
    public class UtilsServiceTest
    {
        private readonly IUtilsService _utilsService;
        public UtilsServiceTest() {
            _utilsService = new UtilsService();

        }
        public class Objeto
        {
            public Objeto(string keyString, long? keyLong) { 
                KeyString = keyString;
                KeyLong = keyLong;
            }
            public string KeyString { get; set; }
            public long? KeyLong{ get; set; }
        }
        [Fact]
        public void TodosPropriedadesPreenchidas()
        {
            Objeto objeto = new Objeto("teste", 1);
            Assert.True(_utilsService.TodosPropriedadesPreenchidas(objeto));

            objeto = new Objeto("", 1);
            Assert.False(_utilsService.TodosPropriedadesPreenchidas(objeto));

            objeto = new Objeto("teste", null);
            Assert.False(_utilsService.TodosPropriedadesPreenchidas(objeto));
            objeto = new Objeto("teste", 0);
            Assert.False(_utilsService.TodosPropriedadesPreenchidas(objeto));
        }
    }
}
