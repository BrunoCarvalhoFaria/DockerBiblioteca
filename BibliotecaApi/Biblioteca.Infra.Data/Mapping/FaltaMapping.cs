using Biblioteca.Domain.Entities.Caixas;
using Biblioteca.Domain.Entities.Faltas;
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
    public class FaltaMapping : EntityTypeConfiguration<Falta>
    {
        public override void Map(EntityTypeBuilder<Falta> builder)
        {
            builder.ToTable("Falta");
            builder.HasKey(p => p.Id);

            builder.HasOne(p => p.Usuario)
                .WithMany(e => e.Faltas)
                .HasForeignKey(p => p.UsuarioCriacaoId);

            builder.HasOne(p => p.Vendedor)
                .WithMany(e => e.Faltas)
                .HasForeignKey(p => p.VendedorId);

            builder.HasOne(p => p.Fornecedor)
                .WithMany(e => e.Faltas)
                .HasForeignKey(p => p.FornecedorId);

            builder.Ignore(e => e.ValidationResult);
            builder.Ignore(p => p.ClassLevelCascadeMode);
            builder.Ignore(p => p.RuleLevelCascadeMode);
        }
    }
}
