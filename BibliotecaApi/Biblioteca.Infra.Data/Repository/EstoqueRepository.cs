using Biblioteca.Domain.DTO;
using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Interfaces;
using Dapper;
using DrPay.Infra.Data.Repository;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Biblioteca.Infra.Data.Repository
{
    public class EstoqueRepository : Repository<Estoque>, IEstoqueRepository
    {
        private readonly DbContextOptions<BibliotecaDbContext> _optionsBuilder;

        public EstoqueRepository(BibliotecaDbContext context) : base(context)
        {
            _optionsBuilder = new DbContextOptions<BibliotecaDbContext>();
        }

        public List<RetornoEstoqueDTO> ListarEstoque(List<long> livroIdList)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine($@"select 
                a.Titulo ,
                a.Autor,
                b.Qtd,
                a.Id as LivroId
                from livro a 
                inner join estoque b on b.LivroId = a.Id
                where a.Id in ({string.Join(",", livroIdList.ToList())}) ");
            var dados = data.Database.GetDbConnection().Query<RetornoEstoqueDTO>(sql.ToString());
            return dados.ToList();
        }

        public Estoque BuscarPorLivroId(long livroId)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine($@"select * from estoque a where a.LivroId = {livroId} ");
            
            return data.Database.GetDbConnection().Query<Estoque>(sql.ToString()).FirstOrDefault();
        }

    }
}
