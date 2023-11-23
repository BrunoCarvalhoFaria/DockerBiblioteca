using AutoMapper;
using Biblioteca.Application.DTO;
using Biblioteca.Domain.DTO;
using Biblioteca.Domain.Entities;

namespace Biblioteca.Application.AutoMapper
{
    public class ApplicationMappingProfile : Profile
    {
        public ApplicationMappingProfile() {
            CreateMap<ClienteDTO, Cliente>().ReverseMap();
            CreateMap<LivroDTO, Livro>().ReverseMap();
            CreateMap<LivroPostDTO, Livro>().ReverseMap();
            CreateMap<LivroGeneroDTO, LivroGenero>().ReverseMap();
            CreateMap<EmprestimoDTO, Emprestimo>().ReverseMap();
            CreateMap<EstoqueDTO, Estoque>().ReverseMap();
            CreateMap<RabbitMensagemDTO, RabbitMensagem>().ReverseMap();
        }
    }
}
