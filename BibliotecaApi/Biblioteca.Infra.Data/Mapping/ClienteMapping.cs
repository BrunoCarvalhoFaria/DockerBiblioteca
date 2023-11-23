using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Entities.ApplicationUsers;
using Biblioteca.Infra.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Biblioteca.Infra.Data.Mapping
{
    public class ClienteMapping : EntityTypeConfiguration<Cliente>
    {
        public override void Map(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Cliente");
            builder.HasKey(x => x.Id);

            builder.HasIndex(u => u.Email)
            .IsUnique();

            builder.HasOne(p => p.Usuario)
                .WithMany(e => e.Clientes)
                .HasForeignKey(p => p.UsuarioId);

            builder.Ignore(e => e.ValidationResult);
            builder.Ignore(e => e.CascadeMode);
            builder.Ignore(p => p.ClassLevelCascadeMode);
            builder.Ignore(p => p.RuleLevelCascadeMode);
        }
    }
}
