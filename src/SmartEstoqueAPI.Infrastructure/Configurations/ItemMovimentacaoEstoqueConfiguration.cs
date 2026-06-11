using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartEstoqueAPI.Domain.Entities;

namespace SmartEstoqueAPI.Infrastructure.Configurations;

public class ItemMovimentacaoEstoqueConfiguration : IEntityTypeConfiguration<ItemMovimentacaoEstoque>
{
    public void Configure(EntityTypeBuilder<ItemMovimentacaoEstoque> builder)
    {
        builder.ToTable("ItemMovimentacaoEstoque");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Quantidade)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(x => x.ValorUnitario)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.HasOne(x => x.MovimentacaoEstoque)
            .WithMany(x => x.Itens)
            .HasForeignKey(x => x.MovimentacaoEstoqueId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Produto)
            .WithMany(x => x.ItensMovimentacao)
            .HasForeignKey(x => x.ProdutoId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.MovimentacaoEstoqueId);

        builder.HasIndex(x => x.ProdutoId);
    }
}