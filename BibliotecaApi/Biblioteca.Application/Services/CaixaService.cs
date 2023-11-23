using AutoMapper;
using Biblioteca.Application.DTO;
using Biblioteca.Application.Interfaces;
using Biblioteca.Domain.Entities.Caixas;
using Biblioteca.Domain.Interfaces;

namespace Biblioteca.Application.Services
{
    public class CaixaService : ICaixaService
    {
        private readonly IMapper _mapper;
        private readonly ICaixaRepository _caixaRepository;
        private readonly IUsuarioAutorizacaoService _usuarioAutorizacaoService;
        public CaixaService(ICaixaRepository caixaRepository,
            IMapper mapper,
            IUsuarioAutorizacaoService usuarioAutorizacaoService)
        {
            _caixaRepository = caixaRepository;
            _usuarioAutorizacaoService = usuarioAutorizacaoService;
            _mapper = mapper;
        }

        public async Task<long> CaixaPost(CaixaDTO dto)
        {
            try
            {
                //if (!_usuarioAutorizacaoService.UsuarioLogadoAdministrador())
                //    throw new Exception("Usuário não autorizado");
                Caixa caixa = _mapper.Map<Caixa>(dto);
                await _caixaRepository.Add(caixa);
                return caixa.Id;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<CaixaDTO> CaixaGetAll()
        {
            try
            {
                return _mapper.Map<List<CaixaDTO>>(_caixaRepository.GetAll());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public CaixaDTO? CaixaGetAById(long id)
        {
            try
            {
                return _mapper.Map<CaixaDTO>(_caixaRepository.GetById(id));
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string CaixaPut(CaixaDTO dto)
        {
            try
            {
                _caixaRepository.Update(_mapper.Map<Caixa>(dto));
                return "Sucesso ao alterar caixa.";

            }
            catch (Exception)
            {

                throw;
            }
        }
    
    }

}
