using AutoMapper;
using Biblioteca.Application.DTO;
using Biblioteca.Application.Interfaces;
using Biblioteca.Domain.Core.Enums;
using Biblioteca.Domain.Entities.Caixas;
using Biblioteca.Domain.Entities.Faltas;
using Biblioteca.Domain.Entities.Vendedores.Repository;
using Biblioteca.Domain.Interfaces;
using Biblioteca.Infra.Data.Repository;


namespace Biblioteca.Application.Services
{
    public class FaltaService : IFaltaService
    {
        private readonly IMapper _mapper;
        private readonly IFaltaRepository _faltaRepository;
        private readonly IUsuarioAutorizacaoService _usuarioAutorizacaoService;
        private readonly IVendedorRepository _vendedorRepository;
        public FaltaService(IFaltaRepository faltaRepository,
            IMapper mapper,
            IUsuarioAutorizacaoService usuarioAutorizacaoService,
            IVendedorRepository vendedorRepository)
        {
            _faltaRepository = faltaRepository;
            _usuarioAutorizacaoService = usuarioAutorizacaoService;
            _mapper = mapper;
            _vendedorRepository = vendedorRepository;
        }

        public long FaltaPost( FaltaDTO dto)
        {
            try
            {
                var usuarioId = _usuarioAutorizacaoService.ObterUsuarioLogado();
                dto.DataCriacao = DateTimeOffset.Now;
                dto.UsuarioCriacaoId = usuarioId;
                dto.Status = StatusFalta.Pendente;
                dto.VendedorId = _vendedorRepository.Buscar(p => p.UsuarioId == usuarioId).FirstOrDefault().Id;
                _faltaRepository.Add(_mapper.Map<Falta>(dto));
                return dto.Id;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<FaltaDTO> FaltaGetAll()
        {
            try
            {
                return _mapper.Map<List<FaltaDTO>>(_faltaRepository.GetAll());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public FaltaDTO? FaltaGetAById(long id)
        {
            try
            {
                return _mapper.Map<FaltaDTO>(_faltaRepository.GetById(id));
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string FaltaPut(FaltaDTO dto)
        {
            try
            {
                _faltaRepository.Update(_mapper.Map<Falta>(dto));
                return "Sucesso ao alterar falta.";

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
