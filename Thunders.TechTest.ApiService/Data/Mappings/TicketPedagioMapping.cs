using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Thunders.TechTest.ApiService.Entities;

namespace Thunders.TechTest.ApiService.Data.Mappings;

public class TicketPedagioMapping : IEntityTypeConfiguration<TicketPedagio>
{
    public void Configure(EntityTypeBuilder<TicketPedagio> builder)
    {
        builder.ToTable("TicketPedagio");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();
        builder.Property(x => x.Valor)
            .IsRequired();
        builder.Property(x => x.DataUtilizacao)
            .IsRequired();
        builder.Property(x => x.TipoVeiculo)
            .IsRequired();
        builder.Property(x => x.DataCriacao)
            .IsRequired();
        builder.Property(x => x.DataUltimaAtualizacao)
            .IsRequired(false);
        builder.HasOne(x => x.PraçaPedagio)
            .WithMany()
            .HasForeignKey(x => x.PedagioId);
    }
}

public class CidadeMapping : IEntityTypeConfiguration<Cidade>
{
    public void Configure(EntityTypeBuilder<Cidade> builder)
    {
        builder.ToTable("Cidade");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();
        builder.Property(x => x.Nome)
            .IsRequired();
        builder.HasOne(x => x.Estado)
            .WithMany()
            .HasForeignKey(x => x.EstadoId);
        builder.Property(x => x.DataCriacao)
         .IsRequired();
        builder.Property(x => x.DataUltimaAtualizacao)
            .IsRequired(false);
    }
}

public class EstadoMapping : IEntityTypeConfiguration<Estado>
{
    public void Configure(EntityTypeBuilder<Estado> builder)
    {
        builder.ToTable("Estado");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();
        builder.Property(x => x.Nome)
            .IsRequired();
        builder.Property(x => x.Sigla)
            .IsRequired();
        builder.Property(x => x.DataCriacao)
         .IsRequired();
        builder.Property(x => x.DataUltimaAtualizacao)
            .IsRequired(false);
    }
}

public class PedagioMapping : IEntityTypeConfiguration<Pedagio>
{
    public void Configure(EntityTypeBuilder<Pedagio> builder)
    {
        builder.ToTable("Pedagio");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();
        builder.Property(x => x.DataCriacao)
            .IsRequired();
        builder.HasOne(x => x.Cidade)
            .WithMany()
            .HasForeignKey(x => x.CidadeId);
        builder.Property(x => x.DataCriacao)
         .IsRequired();
        builder.Property(x => x.DataUltimaAtualizacao)
            .IsRequired(false);
    }
}
