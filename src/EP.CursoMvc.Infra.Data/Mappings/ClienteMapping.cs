using EP.CursoMvc.Domain.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace EP.CursoMvc.Infra.Data.Mappings
{
    // FLUENT API
    public class ClienteMapping : EntityTypeConfiguration<Cliente>
    {
        public ClienteMapping()
        {
            // O EF entende por conta própria que a palavra Id é uma chave primária, mas para vc determinar com qualquer nome a PK este é o comando
            HasKey(c => c.Id);

            Property(c => c.Nome)
                .IsRequired()
                .HasMaxLength(150);

            Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(100);

            Property(c => c.CPF)
                .IsRequired()
                .HasMaxLength(11)
                .IsFixedLength()
                // Criação de índice !
                .HasColumnAnnotation("Index",new IndexAnnotation(
                    new IndexAttribute("IX_CPF") { IsUnique = true }));

            Property(c => c.DataNascimento)
                .IsRequired();

            Property(c => c.Ativo)
                .IsRequired();

            Property(c => c.Excluido)
                .IsRequired();

            ToTable("Clientes");
        }
    }
}
