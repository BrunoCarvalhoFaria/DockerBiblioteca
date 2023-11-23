using Biblioteca.Domain.DTO;
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
    public class EmprestimoRepository : Repository<Emprestimo>, IEmprestimoRepository
    {
        private readonly DbContextOptions<BibliotecaDbContext> _optionsBuilder;

        public EmprestimoRepository(BibliotecaDbContext context) : base(context)
        {
            _optionsBuilder = new DbContextOptions<BibliotecaDbContext>();
        }


        public List<EstoqueConsultaDTO> ObterEmprestimos(long? clienteId, bool apenasPendentesDevolucao, DateTimeOffset? dataInicial, DateTimeOffset? dataFinal)
        {
            clienteId = clienteId ?? 0;
            
            bool filtrarCliente = clienteId > 0;
            bool filtrarDataInicial = dataInicial != null && dataInicial > DateTimeOffset.MinValue;
            bool filtrarDataFinal = dataFinal != null && dataInicial > DateTimeOffset.MinValue;

            DateTimeOffset _dataInicial = dataInicial ?? DateTimeOffset.MinValue;
            DateTimeOffset _dataFinal = dataFinal ?? DateTimeOffset.MaxValue;
            StringBuilder sql = new StringBuilder();
            sql.AppendLine($@"select a.Id,
                            a.ClienteId,
                            b.Nome as ClienteNome,
                            a.LivroId,
                            c.Titulo,
                            a.DataEmprestimo,
                            a.DataDevolucao
                            from emprestimo a
                            inner join Cliente b on b.Id = a.ClienteId
                            inner join Livro c on c.Id = a.LivroId
                            where 
                            (a.ClienteId = {clienteId} or {!filtrarCliente})
                            and (a.DataDevolucao is null or {!apenasPendentesDevolucao})
                            and ((a.DataDevolucao < '{_dataFinal.ToString("yyyy-MM-dd")}' OR a.DataDevolucao is null)  or {!filtrarDataFinal})
                            and (a.DataEmprestimo < '{_dataFinal.ToString("yyyy-MM-dd")}' or {!filtrarDataFinal})
                            and (a.DataEmprestimo > '{_dataInicial.ToString("yyyy-MM-dd")}' or {!filtrarDataInicial})");
            return data.Database.GetDbConnection().Query<EstoqueConsultaDTO>(sql.ToString()).ToList();
        }
    }
}
