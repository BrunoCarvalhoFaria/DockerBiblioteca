using AutoMapper;
using Biblioteca.Application.AutoMapper;
using Biblioteca.Application.DTO;
using Biblioteca.Application.Interfaces;
using Biblioteca.Application.Services;
using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Interfaces;
using Biblioteca.Infra.Data.Repository;
using Microsoft.AspNetCore.Components.Forms;
using Moq;

namespace Biblioteca.TesteUnitario.Application.Services
{

    public class LivroServiceTest
    {
        private readonly ILivroService _livroService;
        private readonly Mock<IUsuarioAutorizacaoService> _usuarioAutorizacaoServiceMock;
        private readonly Mock<ILivroRepository> _repositoryMock;
        private readonly Mock<ILivroGeneroService> _livroGeneroServiceMock;
        private readonly IMapper _mapper;
        public LivroServiceTest()
        {
            _usuarioAutorizacaoServiceMock = new Mock<IUsuarioAutorizacaoService>();
            _livroGeneroServiceMock = new Mock<ILivroGeneroService>();

            _repositoryMock = new Mock<ILivroRepository>();
            var configuration = new MapperConfiguration(options =>
            {
                options.AddProfile<ApplicationMappingProfile>();
                options.AllowNullCollections = true;
            });
            IMapper _mapper = new Mapper(configuration);
            IUtilsService utilsService = new UtilsService();
            _livroService = new LivroService(_repositoryMock.Object, _mapper, _usuarioAutorizacaoServiceMock.Object, _livroGeneroServiceMock.Object, utilsService );
        }

        [Fact(DisplayName = "LivroPost01 - Deve retornar um erro caso alguma das propriedades não for preenchida")]
        public async Task LivroPost01()
        {
            LivroPostDTO livroPostDTO = new LivroPostDTO();
            livroPostDTO.Titulo = "";
            livroPostDTO.Autor = "";
            livroPostDTO.Ano = "";
            livroPostDTO.Editora = "";
            livroPostDTO.LivroGeneroId = 1;
            var exception = await Assert.ThrowsAsync<Exception>(() => _livroService.LivroPost(livroPostDTO));

            Assert.Equal("Todos os campos devem ser preenchidos", exception.Message);

            livroPostDTO = new LivroPostDTO();
            livroPostDTO.Titulo = "Título Teste";
            livroPostDTO.Autor = "Autor Teste";
            livroPostDTO.Ano = "2022";
            livroPostDTO.Editora = "Editora Teste";
            livroPostDTO.LivroGeneroId = 0;
            exception = await Assert.ThrowsAsync<Exception>(() => _livroService.LivroPost(livroPostDTO));

            Assert.Equal("Todos os campos devem ser preenchidos", exception.Message);

            livroPostDTO = new LivroPostDTO();
            livroPostDTO.Titulo = "Título Teste";
            livroPostDTO.Autor = "Autor Teste";
            livroPostDTO.Ano = "2022";
            livroPostDTO.Editora = "";
            livroPostDTO.LivroGeneroId = 1;
            exception = await Assert.ThrowsAsync<Exception>(() => _livroService.LivroPost(livroPostDTO));

            Assert.Equal("Todos os campos devem ser preenchidos", exception.Message);

            livroPostDTO = new LivroPostDTO();
            livroPostDTO.Titulo = "Título Teste";
            livroPostDTO.Autor = "Autor Teste";
            livroPostDTO.Ano = "";
            livroPostDTO.Editora = "Editora Teste";
            livroPostDTO.LivroGeneroId = 1;
            exception = await Assert.ThrowsAsync<Exception>(() => _livroService.LivroPost(livroPostDTO));

            Assert.Equal("Todos os campos devem ser preenchidos", exception.Message);

            livroPostDTO = new LivroPostDTO();
            livroPostDTO.Titulo = "Título Teste";
            livroPostDTO.Autor = "";
            livroPostDTO.Ano = "2022";
            livroPostDTO.Editora = "Editora Teste";
            livroPostDTO.LivroGeneroId = 1;
            exception = await Assert.ThrowsAsync<Exception>(() => _livroService.LivroPost(livroPostDTO));

            Assert.Equal("Todos os campos devem ser preenchidos", exception.Message);

            livroPostDTO = new LivroPostDTO();
            livroPostDTO.Titulo = "";
            livroPostDTO.Autor = "Autor Teste";
            livroPostDTO.Ano = "2022";
            livroPostDTO.Editora = "Editora Teste";
            livroPostDTO.LivroGeneroId = 1;
            exception = await Assert.ThrowsAsync<Exception>(() => _livroService.LivroPost(livroPostDTO));

            Assert.Equal("Todos os campos devem ser preenchidos", exception.Message);
        }

        [Fact(DisplayName = "LivroPost02 - Deve retornar um erro caso o GeneroId não exista")]
        public async Task LivroPost02()
        {
            LivroPostDTO livroPostDTO = new LivroPostDTO();
            livroPostDTO.Titulo = "Título Teste";
            livroPostDTO.Autor = "Autor Teste";
            livroPostDTO.Ano = "2022";
            livroPostDTO.Editora = "Editora Teste";
            livroPostDTO.LivroGeneroId = 1;
            livroPostDTO.Codigo = "1e1";

            _livroGeneroServiceMock.Setup(s => s.LivroGeneroGetAById(livroPostDTO.LivroGeneroId));

            var exception = await Assert.ThrowsAsync<Exception>(() => _livroService.LivroPost(livroPostDTO));

            Assert.Equal("Gênero do livro não encontrado.", exception.Message);
        }

        [Fact(DisplayName = "LivroPost03 - Deve retornar Id = 0 simulando a criação de um novo livro")]
        public async Task LivroPost03()
        {
            LivroPostDTO livroPostDTO = new LivroPostDTO();
            livroPostDTO.Titulo = "Título Teste";
            livroPostDTO.Autor = "Autor Teste";
            livroPostDTO.Ano = "2022";
            livroPostDTO.Editora = "Editora Teste";
            livroPostDTO.LivroGeneroId = 1;
            livroPostDTO.Codigo = "1e1";
            Livro livro = new Livro
            {
                Codigo = "Codigo",
                Titulo = "Título Teste",
                Autor = "Autor Teste",
                Ano = "2022",
                Editora = "Editora Teste",
                LivroGeneroId = 0,
            };

            Livro livroResultado = new Livro
            {
                Codigo = "Codigo",
                Titulo = "Título Teste",
                Autor = "Autor Teste",
                Ano = "2022",
                Editora = "Editora Teste",
                LivroGeneroId = 1,
            };
            List<Livro> livroResultados = new List<Livro>();
            //livroResultados.Add(livroResultado);

            _livroGeneroServiceMock.Setup(s => s.LivroGeneroGetAById(livroPostDTO.LivroGeneroId)).Returns(new LivroGeneroDTO());
            _repositoryMock.Setup(s => s.Add(livro)).Returns(Task.FromResult(livroResultado));

            long resultado = await _livroService.LivroPost(livroPostDTO);
            
            Assert.Equal(resultado, livroResultado.Id);
        }
        [Fact(DisplayName = "LivroDelete01 - Uma erro deve ser retornado por não encontrar o livro")]
        public void LivroDelete01()
        {
            var livroId = 1;
            var exception = Assert.Throws<Exception>(() => _livroService.LivroDelete(livroId));
            Assert.Equal("Livro não encontrado", exception.Message);
        }
        [Fact(DisplayName = "LivroDelete02 - Um livro deve ser deletado")]
        public void LivroDelete02()
        {
            var livroId = 1;
            Livro livro = new Livro
            {
                Codigo = "Codigo",
                Titulo = "Título Teste",
                Autor = "Autor Teste",
                Ano = "2022",
                Editora = "Editora Teste",
                LivroGeneroId = 1,
            };
            _repositoryMock.Setup(s => s.GetById(livroId)).Returns(livro);
            var resultado = _livroService.LivroDelete(livroId);
            livro.Excluir();
            _repositoryMock.Verify(p => p.Update(livro), Times.Once);
            Assert.Equal("Livro excluído com sucesso", resultado);
        }
        [Fact(DisplayName = "LivroPut01 - Um erro deve ser retornado por não encontrar o livro")]
        public void LivroPut01()
        {
            LivroDTO livro = new LivroDTO
            {
                Titulo = "Título Teste",
                Codigo = "1e1",
            Autor = "Autor Teste",
                Ano = "2022",
                Editora = "Editora Teste",
                LivroGeneroId = 1,
                Id = 1
            };
            _repositoryMock.Setup(s => s.GetById(livro.Id));
            var exception = Assert.Throws<Exception>(() => _livroService.LivroPut(livro));
            Assert.Equal("Livro não encontrado", exception.Message);
        }
        [Fact(DisplayName = "LivroPut02 - Um livro deve ser alterado")]
        public void LivroPut02()
        {
            LivroDTO livroDTO = new LivroDTO
            {
                Titulo = "Título Teste",
                Codigo = "1e1",
        Autor = "Autor Teste",
                Ano = "2022",
                Editora = "Editora Teste",
                LivroGeneroId = 1,
                Id = 1
            };

            Livro livro = new Livro
            {
                Codigo = "Codigo",
                Titulo = "Título Teste",
                Autor = "Autor Teste",
                Ano = "2022",
                Editora = "Editora Teste",
                LivroGeneroId = 1,
            };

            _repositoryMock.Setup(s => s.GetById(livroDTO.Id)).Returns(livro);
            _livroGeneroServiceMock.Setup(s => s.LivroGeneroGetAById(livroDTO.LivroGeneroId)).Returns(new LivroGeneroDTO { Descricao = "teste" });
            var resultado = _livroService.LivroPut(livroDTO);
            _repositoryMock.Verify(p => p.Update(livro), Times.Once);
            Assert.Equal("Sucesso ao alterar o livro.", resultado);
        }
        [Fact(DisplayName = "LivroObterTodos01 - Deve retornar a página 1 do resultado paginado")]
        public void LivroObterTodos01()
        {
            int pagina = 1;
            int qtdRegistros = 6;

            List<Livro> livros = new List<Livro>();
            List<LivroDTO> livroDTOs = new List<LivroDTO>();

            for (int i = 0; i < 10; i++)
            {
                livros.Add(new Livro
                {
                    Codigo = "Codigo",
                    Titulo = "Título Teste",
                    Autor = "Autor Teste",
                    Ano = "2022",
                    Editora = "Editora Teste",
                    LivroGeneroId = i,
                });
            }
            int contador = qtdRegistros > livros.Count ? livros.Count : qtdRegistros;
            for (int i = 0; i < contador; i++)
            {
                livroDTOs.Add(new LivroDTO
                {
                    Titulo = "Título Teste",
                    Autor = "Autor Teste",
                    Ano = "2022",
                    Editora = "Editora Teste",
                    LivroGeneroId = i,
                    Codigo = "1e1"
            });
            }

            _repositoryMock.Setup(p => p.GetAll()).Returns(livros);
            LivroObterTodosDTO resultadoEsperado = new LivroObterTodosDTO
            {
                Livros = livroDTOs,
                TotalRegistros = livros.Count
            };

            var resultado = _livroService.ObterTodos(pagina, qtdRegistros);
            Assert.Equal(resultadoEsperado.TotalRegistros, resultado.TotalRegistros);
            for (int i = 0; i < resultadoEsperado.Livros.Count; i++)
            {
                Assert.Equal(resultadoEsperado.Livros[i].LivroGeneroId, resultado.Livros[i].LivroGeneroId);
            }
        }

        [Fact(DisplayName = "LivroObterTodos02 - Deve retornar a página 2 do resultado paginado")]
        public void LivroObterTodos02()
        {
            int pagina = 2;
            int qtdRegistros = 6;

            List<Livro> livros = new List<Livro>();
            List<LivroDTO> livroDTOs = new List<LivroDTO>();

            for (int i = 0; i < 10; i++)
            {
                livros.Add(new Livro
                {
                    Codigo = "Codigo",
                    Titulo = "Título Teste",
                    Autor = "Autor Teste",
                    Ano = "2022",
                    Editora = "Editora Teste",
                    LivroGeneroId = i,
                });
            }
            for (int i = 0; i < 4; i++)
            {
                livroDTOs.Add(new LivroDTO
                {
                    Titulo = "Título Teste",
                    Autor = "Autor Teste",
                    Ano = "2022",
                    Editora = "Editora Teste",
                    Codigo = "1e1",
                LivroGeneroId = i + ((pagina - 1) * qtdRegistros) ,
                });
            }

            _repositoryMock.Setup(p => p.GetAll()).Returns(livros);
            LivroObterTodosDTO resultadoEsperado = new LivroObterTodosDTO
            {
                Livros = livroDTOs,
                TotalRegistros = livros.Count
            };

            var resultado = _livroService.ObterTodos(pagina, qtdRegistros);
            Assert.Equal(resultadoEsperado.TotalRegistros, resultado.TotalRegistros);
            for (int i = 0; i < resultadoEsperado.Livros.Count; i++)
            {
                Assert.Equal(resultadoEsperado.Livros[i].LivroGeneroId, resultado.Livros[i].LivroGeneroId);
            }
        }
        [Fact(DisplayName = "ObterTodosComFiltro01 - Deve executar o método ObterTodosComFiltro uma vez")]
        public void ObterTodosComFiltro01()
        {
                        
            List<Livro> resultado = new();
            List<LivroDTO> resultado2 = new();
            for (int i = 0; i < 20; i++)
            {
                resultado.Add(new Livro
                {
                    Codigo = "Codigo",
                    Titulo = "Título Teste",
                    Autor = "Autor Teste",
                    Ano = "2022",
                    Editora = "Editora Teste",
                    LivroGeneroId = i,
                });
                resultado2.Add(new LivroDTO
                {
                    Codigo = "Codigo",
                    Titulo = "Título Teste",
                    Autor = "Autor Teste",
                    Ano = "2022",
                    Editora = "Editora Teste",
                    LivroGeneroId = i,
                });
            }

            _repositoryMock.Setup(p => p.ObterTodosComFiltro(null, null, null, null, null, null)).Returns(resultado);

            resultado2 = resultado2.Skip(15).ToList();
            var retorno = _livroService.ObterTodosComFiltro(null,null , null, null, null, null,2,15);
            for (int i = 0; i < retorno.Livros.Count; i++)
            {
                Assert.Equal(resultado2[i].LivroGeneroId, retorno.Livros[i].LivroGeneroId);
            }
            Assert.Equal(20, retorno.TotalRegistros);
        }
    }
}
