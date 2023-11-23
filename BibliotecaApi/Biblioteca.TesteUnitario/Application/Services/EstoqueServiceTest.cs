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
using System.Runtime.CompilerServices;

namespace Biblioteca.TesteUnitario.Application.Services
{
    public class EstoqueServiceTest
    {
        private readonly IEstoqueService _estoqueService;
        private readonly Mock<IUsuarioAutorizacaoService> _usuarioAutorizacaoServiceMock;
        private readonly Mock<IEstoqueRepository> _repositoryMock;
        private readonly Mock<ILivroService> _livroService;
        public EstoqueServiceTest()
        {
            _usuarioAutorizacaoServiceMock = new Mock<IUsuarioAutorizacaoService>();
            _livroService = new Mock<ILivroService>();
            _repositoryMock = new Mock<IEstoqueRepository>();
            var configuration = new MapperConfiguration(options =>
            {
                options.AddProfile<ApplicationMappingProfile>();
                options.AllowNullCollections = true;
            });
            IMapper mapper = new Mapper(configuration);

            _estoqueService = new EstoqueService(_repositoryMock.Object, mapper, _usuarioAutorizacaoServiceMock.Object, _livroService.Object);
        }
       
        [Fact(DisplayName = "CalcularEstoque01 - Deve alterar o estoque e verificar todas condições")]
        public void CalcularEstoque()
        {
            Estoque estoque = new Estoque
            {
                LivroId = 10,
                Qtd = 10
            };

            Assert.Equal( 15, _estoqueService.CalcularEstoque(estoque, 5));
            estoque.Qtd = 10;
            Assert.Equal(0, _estoqueService.CalcularEstoque(estoque, -10));
            estoque.Qtd = 10;
            var exception = Assert.Throws<Exception>(() => _estoqueService.CalcularEstoque(estoque, -15));

            Assert.Equal("Estoque negativo não permitido", exception.Message);
        }

        [Fact(DisplayName = "AlterarEstoque01 - Teste caso o estoque não seja encontrado")]
        public void AlterarEstoque01() {
            var exception = Assert.Throws<Exception>(() => _estoqueService.AlterarEstoque(0, 1));
            Assert.Equal("Estoque referente ao livro não encontrado.", exception.Message);
        }

        [Fact(DisplayName = "AlterarEstoque02 - Deve utilizar EstoqueRepository.Update uma vez")]
        public void AlterarEstoque02()
        {
            long livroId = 10;
            long qtdInserida = 1;
            Estoque estoque = new Estoque{
                LivroId = 10,
                Qtd = 10
            };

            Estoque resultadoEsperado = new Estoque{
                LivroId = 10,
                Qtd = estoque.Qtd + qtdInserida
            };

            _repositoryMock.Setup(s => s.BuscarPorLivroId(livroId)).Returns(estoque);
            Estoque resultado = _estoqueService.AlterarEstoque(livroId, qtdInserida);
            _repositoryMock.Verify(p => p.Update(resultado), Times.Once);
            Assert.Equal(resultadoEsperado, resultado);
        }

        [Fact(DisplayName = "PostEstoque01 - Deve retornar erro por não encontrar o livro")]
        public async Task PostEstoque01()
        {
            EstoqueDTO estoque = new EstoqueDTO { LivroId = 1, Qtd = 1 };
            var exception = await Assert.ThrowsAsync<Exception>(() => _estoqueService.PostEstoque(estoque));
            Assert.Equal("Livro não encontrado.", exception.Message);
        }
        [Fact(DisplayName = "PostEstoque02 - Deve adicionar o estoque com sucesso")]
        public void PostEstoque02()
        {
            EstoqueDTO estoqueDTO = new EstoqueDTO { LivroId = 1, Qtd = 1 };
            Estoque estoque = new Estoque { LivroId = 1, Qtd = 1 };
            LivroDTO livroDTO = new LivroDTO
            {
                Titulo = "Título Teste",
                Autor = "Autor Teste",
                Ano = "2022",
                Editora = "Editora Teste",
                LivroGeneroId = 1,
            };
            _livroService.Setup(p => p.LivroGetAById(estoqueDTO.LivroId)).Returns(livroDTO);
            _estoqueService.PostEstoque(estoqueDTO);
            _repositoryMock.Verify(p => p.Add(estoque), Times.Once);
        }
        [Fact(DisplayName = "ListarEstoque01 - deve executar a rotina ListarEstoque uma vez")]
        public void ListarEstoque01()
        {
            List<long> livroIds = new(new long[] { 1, 2, 3 });
            _estoqueService.ListarEstoque(livroIds);
            _repositoryMock.Verify(p => p.ListarEstoque(livroIds), Times.Once);
        }
    }
}
