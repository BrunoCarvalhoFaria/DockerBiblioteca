using AutoMapper;
using Biblioteca.Application.DTO;
using Biblioteca.Application.Interfaces;
using Biblioteca.Domain.Entities.Vendedores;
using Biblioteca.Domain.Entities.Vendedores.Repository;

namespace Biblioteca.Application.Services
{
    public class VendedorService : IVendedorService
    {
        private readonly IMapper _mapper;
        private readonly IVendedorRepository _vendedorRepository;
        private readonly IUsuarioAutorizacaoService _usuarioAutorizacaoService;
        public VendedorService(IVendedorRepository vendedorRepository,
            IMapper mapper,
            IUsuarioAutorizacaoService usuarioAutorizacaoService) { 
            _vendedorRepository = vendedorRepository;
            _usuarioAutorizacaoService = usuarioAutorizacaoService;
            _mapper = mapper;
        }

        public async Task<long> VendedorPost(VendedorDTO dto)
        {
            try
            {
                //if (!_usuarioAutorizacaoService.UsuarioLogadoAdministrador())
                //    throw new Exception("Usuário não autorizado");
                Vendedor vendedor = _mapper.Map<Vendedor>(dto);
                await _vendedorRepository.Add(vendedor);
                return vendedor.Id;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<VendedorDTO> VendedorGetAll()
        {
            try
            {
                return  _mapper.Map< List<VendedorDTO>>(_vendedorRepository.GetAll());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public VendedorDTO? VendedorGetAById(long id)
        {
            try
            {
                return _mapper.Map<VendedorDTO>(_vendedorRepository.GetById(id));
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
