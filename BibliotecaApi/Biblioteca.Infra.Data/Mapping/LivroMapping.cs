using Biblioteca.Domain.Entities;
using Biblioteca.Infra.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Biblioteca.Infra.Data.Mapping
{
    public class LivroMapping : EntityTypeConfiguration<Livro>
    {
        public override void Map(EntityTypeBuilder<Livro> builder)
        {
            builder.ToTable("Livro");
            builder.HasKey(x => x.Id);

            builder.HasOne(p => p.LivroGenero)
                .WithMany(e => e.Livros)
                .HasForeignKey(p => p.LivroGeneroId);

            builder.HasIndex(u => u.Codigo)
            .IsUnique();

            builder.Ignore(e => e.ValidationResult);
            builder.Ignore(p => p.ClassLevelCascadeMode);
            builder.Ignore(e => e.CascadeMode);
            builder.Ignore(p => p.RuleLevelCascadeMode);
        }
    }
}
