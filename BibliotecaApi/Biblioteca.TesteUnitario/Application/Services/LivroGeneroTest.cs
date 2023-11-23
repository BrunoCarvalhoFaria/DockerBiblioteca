using AutoMapper;
using Biblioteca.Application.AutoMapper;
using Biblioteca.Application.DTO;
using Biblioteca.Application.Interfaces;
using Biblioteca.Application.Services;
using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Interfaces;
using Biblioteca.Infra.Data.Repository;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.TesteUnitario.Application.Services
{
    public class LivroGeneroServiceTest
    {
        private readonly ILivroGeneroService _livroGeneroService;
        private readonly Mock<IUsuarioAutorizacaoService> _usuarioAutorizacaoServiceMock;
        private readonly Mock<ILivroGeneroRepository> _repositoryMock;
        private readonly IMapper _mapper;
        public LivroGeneroServiceTest()
        {
            _usuarioAutorizacaoServiceMock = new Mock<IUsuarioAutorizacaoService>();

            _repositoryMock = new Mock<ILivroGeneroRepository>();
            var configuration = new MapperConfiguration(options =>
            {
                options.AddProfile<ApplicationMappingProfile>();
                options.AllowNullCollections = true;
            });
            IMapper _mapper = new Mapper(configuration);
            IUtilsService utilsService = new UtilsService();
            _livroGeneroService = new LivroGeneroService(_repositoryMock.Object, _mapper, _usuarioAutorizacaoServiceMock.Object, utilsService);
        }

        [Fact(DisplayName = "LivroGeneroPost01 - Deve retornar uma erro por não ser enviado a descrição")]
        public async Task LivroGeneroPost01()
        {
            var descricao = "";
            var exception = await Assert.ThrowsAsync<Exception>(() => _livroGeneroService.LivroGeneroPost(descricao));
            Assert.Equal("Deve conter uma descrição.", exception.Message);
        }

        [Fact(DisplayName = "LivroGeneroPost02 - Um novo gênero de livro deve ser criado")]
        public async Task LivroGeneroPost02()
        {
            var descricao = "Teste";
            LivroGenero livroGenero = new(descricao);
            await _livroGeneroService.LivroGeneroPost(descricao);
            _repositoryMock.Verify(p => p.Add(livroGenero), Times.Once);
        }

        [Fact(DisplayName = "LivroGeneroDelete01 - Deve retornar um erro por não encontrar o gênero")]
        public void LivroGeneroDelete01()
        {
            long id = 1;
            var exception = Assert.Throws<Exception>(() => _livroGeneroService.LivroGeneroDelete(id));
            Assert.Equal("Gênero não encontrado", exception.Message);
        }
        [Fact(DisplayName = "LivroGeneroDelete02 - Deve excluir um gênero com sucesso")]
        public void LivroGeneroDelete02()
        {
            long id = 1;
            
            _repositoryMock.Setup(p => p.GetById(id)).Returns(new LivroGenero("teste"));


            string resultado = _livroGeneroService.LivroGeneroDelete(id);
            Assert.Equal("Gênero excluído com sucesso", resultado);
        }

        [Fact(DisplayName = "LivroGeneroPut01 - Deve ser retornado um erro por não encontrar o gênero")]
        public void LivroGeneroPut01()
        {
            LivroGeneroDTO dto = new LivroGeneroDTO();
            var exception = Assert.Throws<Exception>(() => _livroGeneroService.LivroGeneroPut(dto));
            Assert.Equal("Gênero não encontrado", exception.Message);
        }
        [Fact(DisplayName = "LivroGeneroPut02 - Deve ser alterado um gênero")]
        public void LivroGeneroPut02()
        {
            LivroGeneroDTO dto = new LivroGeneroDTO();
            _repositoryMock.Setup(p => p.GetById(0)).Returns(new LivroGenero("teste"));    
            var resultado = _livroGeneroService.LivroGeneroPut(dto);
            _repositoryMock.Verify(p => p.Update(new LivroGenero("teste")), Times.Once);
            Assert.Equal("Sucesso ao alterar o livroGenero.", resultado);
        }
        [Fact(DisplayName = "Obtertodos01 - Deve executar a rotina GettAll uma vez")]
        public void Obtertodos01()
        {
            _repositoryMock.Setup(p => p.GetAll()).Returns(new List<LivroGenero>());

            _livroGeneroService.Obtertodos();
            _repositoryMock.Verify(p => p.GetAll(),Times.Once);
        }
    }
}
