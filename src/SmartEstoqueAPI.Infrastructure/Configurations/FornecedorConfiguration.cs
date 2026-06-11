using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartEstoqueAPI.Domain.Entities;

namespace SmartEstoqueAPI.Infrastructure.Configurations;

public class FornecedorConfiguration : IEntityTypeConfiguration<Fornecedor>
{
    public void Configure(EntityTypeBuilder<Fornecedor> builder)
    {
        builder.ToTable("Fornecedor");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.RazaoSocial)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.NomeFantasia)
            .HasMaxLength(200);

        builder.Property(x => x.Cnpj)
            .HasMaxLength(14);

        builder.Property(x => x.Email)
            .HasMaxLength(200);

        builder.Property(x => x.Telefone)
            .HasMaxLength(20);

        builder.Property(x => x.Ativo)
            .IsRequired()
            .HasDefaultValue(true);
    }
}