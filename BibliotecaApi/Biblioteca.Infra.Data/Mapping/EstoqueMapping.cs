using Biblioteca.Domain.Entities;
using Biblioteca.Infra.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Infra.Data.Mapping
{
    public class EstoqueMapping : EntityTypeConfiguration<Estoque>
    {
        public override void Map(EntityTypeBuilder<Estoque> builder)
        {
            builder.ToTable("Estoque");
            builder.HasKey(x => x.Id);

            builder.HasOne(p => p.Livro)
                .WithMany(e => e.Estoques)
                .HasForeignKey(p => p.LivroId);

            builder.Ignore(e => e.ValidationResult);
            builder.Ignore(e => e.CascadeMode);
            builder.Ignore(p => p.ClassLevelCascadeMode);
            builder.Ignore(p => p.RuleLevelCascadeMode);
        }
    }
}
