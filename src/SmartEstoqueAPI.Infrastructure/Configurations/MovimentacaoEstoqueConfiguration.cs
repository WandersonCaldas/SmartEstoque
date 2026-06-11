using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartEstoqueAPI.Domain.Entities;

namespace SmartEstoqueAPI.Infrastructure.Configurations;

public class MovimentacaoEstoqueConfiguration : IEntityTypeConfiguration<MovimentacaoEstoque>
{
    public void Configure(EntityTypeBuilder<MovimentacaoEstoque> builder)
    {
        builder.ToTable("MovimentacaoEstoque");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.TipoMovimentacao)
            .IsRequired();

        builder.Property(x => x.DataMovimentacao)
            .IsRequired();

        builder.Property(x => x.Observacao)
            .HasMaxLength(1000);
    }
}