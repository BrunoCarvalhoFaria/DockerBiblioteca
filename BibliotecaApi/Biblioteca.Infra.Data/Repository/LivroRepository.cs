using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Interfaces;
using Dapper;
using DrPay.Infra.Data.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Infra.Data.Repository
{
    public class LivroRepository : Repository<Livro>, ILivroRepository
    {
        private readonly DbContextOptions<BibliotecaDbContext> _optionsBuilder;

        public LivroRepository(BibliotecaDbContext context) : base(context)
        {
            _optionsBuilder = new DbContextOptions<BibliotecaDbContext>();
        }

        public List<Livro> ObterTodosComFiltro(string? codigo, string? titulo, string? ano, string? autor, long? livroGeneroId, string? editora)
        {
            StringBuilder sql = new StringBuilder();
            livroGeneroId = livroGeneroId ?? 0;
            bool filtrarPorGenero = livroGeneroId > 0;
            sql.AppendLine($@"select * from livro 
                            where
                            (Titulo like '%{titulo ?? ""}%')
                            and (Codigo like '%{codigo ?? ""}%')
                            and (Autor like '%{autor?? ""}%')
                            and (Ano like '%{ano ?? ""}%')
                            and (Editora like '%{editora ?? ""}%')
                            and (LivroGeneroId = {livroGeneroId} or {!filtrarPorGenero})");
            return data.Database.GetDbConnection().Query<Livro>(sql.ToString()).ToList();
        }
    }
}
