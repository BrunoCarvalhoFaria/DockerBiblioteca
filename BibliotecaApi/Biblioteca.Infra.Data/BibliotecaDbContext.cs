using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Biblioteca.Domain.Entities.ApplicationUsers;
using Biblioteca.Domain.DTO;
using Microsoft.Extensions.Configuration;
using Biblioteca.Domain.Entities;
using Biblioteca.Infra.Data.Extensions;
using Biblioteca.Infra.Data.Mapping;

namespace Biblioteca.Infra.Data
{
    public class BibliotecaDbContext : IdentityDbContext<ApplicationUser>
    {
        public BibliotecaDbContext(DbContextOptions<BibliotecaDbContext> options) : base(options) 
        {
            
        }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Livro> Livro { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<LivroGenero> LivroGenero { get; set; }
        public DbSet<Estoque> Estoque { get; set; }
        public DbSet<Emprestimo> Emprestimo { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL(ObterStringConexao());
                base.OnConfiguring(optionsBuilder);

            }
        }
        public string ObterStringConexao()
        {
            return Configuracoes.Configuration.GetConnectionString("ConnectionMysql");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>().ToTable("AspNetUsers").HasKey(t => t.Id);

            modelBuilder.AddConfiguration(new LivroMapping());
            modelBuilder.AddConfiguration(new ClienteMapping());
            modelBuilder.AddConfiguration(new LivroGeneroMapping());
            modelBuilder.AddConfiguration(new EstoqueMapping());
            modelBuilder.AddConfiguration(new EmprestimoMapping());

            base.OnModelCreating(modelBuilder);
        }
    }
}
