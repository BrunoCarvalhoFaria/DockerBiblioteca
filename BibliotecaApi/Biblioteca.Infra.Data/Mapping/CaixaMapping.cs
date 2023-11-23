using Biblioteca.Domain.Entities.Caixas;
using Biblioteca.Domain.Entities.Vendedores;
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
    public class CaixaMapping : EntityTypeConfiguration<Caixa>
    {
        public override void Map(EntityTypeBuilder<Caixa> builder)
        {
            builder.ToTable("Caixa");
            builder.HasKey(p => p.Id);

            builder.HasOne(p=> p.Usuario)
                .WithMany(e => e.Caixas)
                .HasForeignKey(p => p.UsuarioId);

            builder.Ignore(e => e.ValidationResult);
            builder.Ignore(p => p.ClassLevelCascadeMode);
            builder.Ignore(p => p.RuleLevelCascadeMode);
        }
    }
}
