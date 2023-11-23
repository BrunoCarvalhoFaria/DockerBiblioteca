using AutoMapper;
using Biblioteca.Application.DTO;
using Biblioteca.Application.Interfaces;
using Biblioteca.Domain.DTO;
using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Application.Services
{
    public class EmprestimoService : IEmprestimoService
    {
        private readonly IMapper _mapper;
        private readonly IEmprestimoRepository _emprestimoRepository;
        private readonly IUsuarioAutorizacaoService _usuarioAutorizacaoService;
        private readonly IClienteService _clienteService;
        private readonly ILivroService _livroService;
        private readonly IEstoqueService _estoqueService;
        private readonly IRabbitMensagemService _rabbitMensagemService;
        public EmprestimoService(IEmprestimoRepository emprestimoRepository,
            IMapper mapper,
            IUsuarioAutorizacaoService usuarioAutorizacaoService,
            IClienteService clienteService,
            ILivroService livroService,
            IEstoqueService estoqueService,
            IRabbitMensagemService rabbitMensagemService
            )
        {
            _emprestimoRepository = emprestimoRepository;
            _usuarioAutorizacaoService = usuarioAutorizacaoService;
            _clienteService = clienteService;
            _livroService = livroService;
            _mapper = mapper;
            _estoqueService = estoqueService;
            _rabbitMensagemService = rabbitMensagemService;
        }

        public async Task<long> RealizarEmprestimo(long clienteId, long livroId)
        {
            try
            {
                var cliente = _clienteService.ClienteGetAById(clienteId);
                if (cliente == null)
                    throw new Exception("Cliente não encontrado.");
                var livro = _livroService.LivroGetAById(livroId);
                if (livro == null)
                    throw new Exception("Livro não encontrado.");

                _estoqueService.AlterarEstoque(livroId, -1);
                Emprestimo emprestimo = new Emprestimo
                {
                    ClienteId = clienteId,
                    DataEmprestimo = DateTimeOffset.Now,
                    LivroId = livroId
                };
                await _emprestimoRepository.Add(emprestimo);
                _rabbitMensagemService.EnviarMensagem(new RabbitMensagemDTO
                {
                    Id = emprestimo.Id,
                    LivroId = emprestimo.LivroId,
                    LivroNome = _livroService.LivroGetAById(emprestimo.LivroId).Titulo,
                    ClienteId = emprestimo.ClienteId,
                    ClienteNome = _clienteService.ClienteGetAById(emprestimo.ClienteId).Nome,
                    Operacao = "emprestimo"
                });
                return emprestimo.Id;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public long RealizarDevolucao(long emprestimoId)
        {
            try
            {
                var emprestimo = _emprestimoRepository.GetById(emprestimoId);
                if (emprestimo == null)
                    throw new Exception("Emprestimo não encontrado.");
                _estoqueService.AlterarEstoque(emprestimo.LivroId, 1);
                emprestimo.DataDevolucao = DateTimeOffset.Now;
                _emprestimoRepository.Update(emprestimo);
                _rabbitMensagemService.EnviarMensagem(new RabbitMensagemDTO
                {
                    Id = emprestimo.Id,
                    LivroId = emprestimo.LivroId,
                    LivroNome = _livroService.LivroGetAById(emprestimo.LivroId).Titulo,
                    ClienteId = emprestimo.ClienteId,
                    ClienteNome = _clienteService.ClienteGetAById(emprestimo.ClienteId).Nome,
                    Operacao = "devolucao"
                });
                return emprestimo.Id;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<EstoqueConsultaDTO> ObterEmprestimos(long? clienteId, bool apenasPendentesDevolucao, DateTimeOffset? dataInicial, DateTimeOffset? dataFinal)
        {
            try
            {

                return _emprestimoRepository.ObterEmprestimos(clienteId, apenasPendentesDevolucao, dataInicial, dataFinal);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
