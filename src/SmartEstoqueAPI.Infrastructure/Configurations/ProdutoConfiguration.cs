using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartEstoqueAPI.Domain.Entities;

namespace SmartEstoqueAPI.Infrastructure.Configurations;

public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
        builder.ToTable("Produto");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Nome)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Descricao)
            .HasMaxLength(1000);

        builder.Property(x => x.QuantidadeAtual)
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.EstoqueMinimo)
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.ValorUnitario)
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.Ativo)
            .HasDefaultValue(true);

        builder.HasOne(x => x.Categoria)
            .WithMany(x => x.Produtos)
            .HasForeignKey(x => x.CategoriaId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Fornecedor)
            .WithMany(x => x.Produtos)
            .HasForeignKey(x => x.FornecedorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.CategoriaId);

        builder.HasIndex(x => x.FornecedorId);
    }
}