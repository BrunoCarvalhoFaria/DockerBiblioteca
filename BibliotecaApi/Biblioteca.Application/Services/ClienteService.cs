using AutoMapper;
using Biblioteca.Application.DTO;
using Biblioteca.Application.Interfaces;
using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Interfaces;
using Biblioteca.Infra.Data.Repository;

namespace Biblioteca.Application.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IMapper _mapper;
        private readonly IClienteRepository _clienteRepository;
        private readonly IUsuarioAutorizacaoService _usuarioAutorizacaoService;
        public ClienteService(IClienteRepository clienteRepository,
            IMapper mapper,
            IUsuarioAutorizacaoService usuarioAutorizacaoService
            )
        {
            _clienteRepository = clienteRepository;
            _usuarioAutorizacaoService = usuarioAutorizacaoService;
            _mapper = mapper;
        }

        public async Task<long> ClientePost(ClienteDTO dto)
        {
            if (string.IsNullOrEmpty(dto.Email) || string.IsNullOrEmpty(dto.Nome))
                throw new Exception("Deve ser preenchido o email e nome do cliente.");
            Cliente cliente = _mapper.Map<Cliente>(dto);
            await _clienteRepository.Add(cliente);
            return cliente.Id;
        }

        public ClienteDTO? ClienteGetAById(long id)
        {
            return _mapper.Map<ClienteDTO>(_clienteRepository.GetById(id));
        }

        public List<ClienteDTO> ObterTodos()
        {
            return _mapper.Map<List<ClienteDTO>>(_clienteRepository.GetAll());
        }

        public string ClienteDelete(long id)
        {
            Cliente? cliente = _clienteRepository.GetById(id);
            if (cliente == null)
                throw new Exception("Cliente não encontrado");
            cliente.Excluir();
            _clienteRepository.Update(cliente);
            return "Cliente excluído com sucesso";
        }

        public string ClientePut(ClienteDTO dto)
        {
            if (dto.Id == null)
                throw new Exception("Cliente não encontrado");
            var cliente = _clienteRepository.GetById((long)dto.Id);
            if (cliente == null)
                throw new Exception("Cliente não encontrado");
            cliente.Nome = dto.Nome;
            cliente.Email = dto.Email;
            cliente.UsuarioId = dto.UsuarioId;
            _clienteRepository.Update(_mapper.Map<Cliente>(cliente));
            return "Sucesso ao alterar o cliente.";
        }
        public ClienteDTO ObtemClientePorEmail(string email)
        {
            return _mapper.Map<ClienteDTO>(_clienteRepository.ObtemClientePorEmail(email));
        }
    }
}
