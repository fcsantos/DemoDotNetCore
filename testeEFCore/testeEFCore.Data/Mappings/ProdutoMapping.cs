using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using testeEFCore.Business.Models;

namespace testeEFCore.Data.Mappings
{
    public class ProdutoMapping : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome)
                .IsRequired()
                .HasColumnType("nvarchar(200)");

            builder.Property(p => p.Descricao)
                .IsRequired()
                .HasColumnType("nvarchar(1000)");

            builder.Property(p => p.Imagem)
                .IsRequired()
                .HasColumnType("nvarchar(100)");

            builder.Property(p => p.DataCadastro)
                .IsRequired(false)
                .HasColumnType("DateTime");

            builder.Property(p => p.DataAtualizacao)
                .IsRequired(false)
                .HasColumnType("DateTime");

            builder.ToTable("Produto");
        }
    }
}
