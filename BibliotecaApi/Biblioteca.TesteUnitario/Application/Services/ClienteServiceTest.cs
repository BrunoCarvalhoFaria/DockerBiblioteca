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
    public class ClienteServiceTest
    {
        private readonly IClienteService _clienteService;
        private readonly Mock<IUsuarioAutorizacaoService> _usuarioAutorizacaoServiceMock;
        private readonly Mock<IClienteRepository> _repositoryMock;
        public ClienteServiceTest()
        {
            _usuarioAutorizacaoServiceMock = new Mock<IUsuarioAutorizacaoService>();

            _repositoryMock = new Mock<IClienteRepository>();
            var configuration = new MapperConfiguration(options =>
            {
                options.AddProfile<ApplicationMappingProfile>();
                options.AllowNullCollections = true;
            });
            IMapper mapper = new Mapper(configuration);

            _clienteService = new ClienteService(_repositoryMock.Object, mapper, _usuarioAutorizacaoServiceMock.Object);
        }

        [Fact(DisplayName = "ClientePost01 - Deve retornar um erro por faltar parâmetros")]
        public async Task ClientePost01()
        {
            ClienteDTO dto = new("", "email");
            var exception = await Assert.ThrowsAsync<Exception>(() => _clienteService.ClientePost(dto));
            Assert.Equal("Deve ser preenchido o email e nome do cliente.", exception.Message);

            dto = new("nome", "");
            exception = await Assert.ThrowsAsync<Exception>(() => _clienteService.ClientePost(dto));
            Assert.Equal("Deve ser preenchido o email e nome do cliente.", exception.Message);
        }

        [Fact(DisplayName = "ClientePost02 - Deve ser criado um novo Cliente")]
        public async Task ClientePost02()
        {
            ClienteDTO dto = new("nome", "email");
            Cliente cliente = new Cliente { Email = "resultadoEmail", Nome = "resultadoNome" };

            _repositoryMock.Setup(p => p.Add(cliente)).Returns(Task.FromResult(cliente));

            var Id = await _clienteService.ClientePost(dto);
            Assert.Equal(0, Id);
        }
        [Fact(DisplayName = "ClienteDelete01 - Deve retornar erro por não encontrar o cliente")]
        public void ClienteDelete01()
        {
            long id = 0;
            var exception = Assert.Throws<Exception>(() => _clienteService.ClienteDelete(id));
            Assert.Equal("Cliente não encontrado", exception.Message);
        }
        [Fact(DisplayName = "ClienteDelete02 - Deve ser feito um soft delete com sucesso um cliente")]
        public void ClienteDelete02()
        {
            long id = 0;

            Cliente cliente = new Cliente
            {
                Email = "EmailTeste",
                Nome = "NomeTeste"
            };
            _repositoryMock.Setup(s => s.GetById(id)).Returns(cliente);
            var resultado = _clienteService.ClienteDelete(id);
            cliente.Excluir();
            _repositoryMock.Verify(p => p.Update(cliente), Times.Once);
            Assert.Equal("Cliente excluído com sucesso", resultado);

        }
        [Fact(DisplayName = "ClientePut01 - Deve retornar erro por não encontrar o cliente")]
        public void ClientePut01()
        {
            ClienteDTO dto = new("nome", "email");
            var exception = Assert.Throws<Exception>(() => _clienteService.ClientePut(dto));
            Assert.Equal("Cliente não encontrado", exception.Message);
        }
        [Fact(DisplayName = "ClientePut02 - Deve alterar um cliente com sucesso")]
        public void ClientePut02()
        {
            ClienteDTO dto = new("nome", "email");
            dto.Id = 1;
            Cliente cliente = new Cliente
            {
                Email = "EmailTeste",
                Nome = "NomeTeste"
            };

            _repositoryMock.Setup(p => p.GetById((long)dto.Id)).Returns(cliente);

            var retorno = _clienteService.ClientePut(dto);
            _repositoryMock.Verify(p => p.Update(cliente), Times.Once);

            Assert.Equal("Sucesso ao alterar o cliente.", retorno);
        }
        [Fact(DisplayName = "ObtemClientePorEmail01 - Deve executar a rotina ObtemClientePorEmail uma vez")]
        public void ObtemClientePorEmail01 ()
        {
            string email = "teste@teste.com";
            var retorno = _clienteService.ObtemClientePorEmail(email);
            Assert.Equal(null, retorno);
            _repositoryMock.Verify(p => p.ObtemClientePorEmail(email), Times.Once);
        }

    }
}
