using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using testeEFCore.Business.Models;

namespace testeEFCore.Data.Mappings
{
    public class EnderecoMapping : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(c => c.Logradouro)
                .IsRequired()
                .HasColumnType("nvarchar(200)");

            //builder.Property(c => c.Numero)
            //    .IsRequired()
            //    .HasColumnType("nvarchar(50)");

            builder.Property(c => c.Cep)
                .IsRequired()
                .HasColumnType("nvarchar(8)");

            //builder.Property(c => c.Complemento)
            //    .HasColumnType("nvarchar(250)");

            builder.Property(c => c.Bairro)
                .IsRequired()
                .HasColumnType("nvarchar(100)");

            builder.Property(c => c.Cidade)
                .IsRequired()
                .HasColumnType("nvarchar(100)");

            builder.Property(c => c.Estado)
                .IsRequired()
                .HasColumnType("nvarchar(50)");

            builder.ToTable("Endereco");
        }
    }
}
