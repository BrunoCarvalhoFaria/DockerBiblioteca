using Biblioteca.Domain.Entities;
using Biblioteca.Infra.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Biblioteca.Infra.Data.Mapping
{
    public class LivroGeneroMapping : EntityTypeConfiguration<LivroGenero>
    {
        public override void Map(EntityTypeBuilder<LivroGenero> builder)
        {
            builder.ToTable("LivroGenero");
            builder.HasKey(x => x.Id);

            builder.Ignore(e => e.ValidationResult);
            builder.Ignore(p => p.ClassLevelCascadeMode);
            builder.Ignore(e => e.CascadeMode);
            builder.Ignore(p => p.RuleLevelCascadeMode);
        }
    }
}
