using AutoMapper;
using Biblioteca.Application.DTO;
using Biblioteca.Application.Interfaces;
using Biblioteca.Domain.DTO;
using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Interfaces;
using Biblioteca.Infra.Data.Repository;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Reflection;

namespace Biblioteca.Application.Services
{
    public class EstoqueService : IEstoqueService
    {
        private readonly IMapper _mapper;
        private readonly IEstoqueRepository _estoqueRepository;
        private readonly IUsuarioAutorizacaoService _usuarioAutorizacaoService;
        private readonly ILivroService _livroService;
        public EstoqueService(IEstoqueRepository estoqueRepository,
            IMapper mapper,
            IUsuarioAutorizacaoService usuarioAutorizacaoService,
            ILivroService livroService)
        {
            _estoqueRepository = estoqueRepository;
            _usuarioAutorizacaoService = usuarioAutorizacaoService;
            _mapper = mapper;
            _livroService = livroService;
        }
        public long CalcularEstoque(Estoque estoque, long qtd)
        {
            try
            {
                estoque.Qtd += qtd;
                if (estoque.Qtd < 0)
                    throw new Exception("Estoque negativo não permitido");
                return estoque.Qtd;
            }
            catch (Exception)
            {
                throw;
            }

        }
        public Estoque AlterarEstoque(long livroId, long qtd)
        {
            try
            {
                Estoque? estoque = _estoqueRepository.BuscarPorLivroId(livroId);
                if (estoque == null)
                    throw new Exception("Estoque referente ao livro não encontrado.");
                estoque.Qtd = CalcularEstoque(estoque, qtd);
                _estoqueRepository.Update(estoque);
                return estoque;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<long> PostEstoque(EstoqueDTO dto)
        {
            try
            {
                    if (_livroService.LivroGetAById(dto.LivroId) == null)
                        throw new Exception("Livro não encontrado.");
                    Estoque estoque = _mapper.Map<Estoque>(dto);
                    await _estoqueRepository.Add(estoque);
                    return estoque.Id;
                
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<RetornoEstoqueDTO> ListarEstoque(List<long> livroIdList) {
            try
            {
                List<RetornoEstoqueDTO> retorno = _estoqueRepository.ListarEstoque(livroIdList);
                return retorno;
            }
            catch (Exception )
            {

                throw;
            }
        
        
        }
    }
}
