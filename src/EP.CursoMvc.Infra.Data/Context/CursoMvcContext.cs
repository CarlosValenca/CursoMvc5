using EP.CursoMvc.Domain.Models;
using EP.CursoMvc.Infra.Data.Mappings;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

namespace EP.CursoMvc.Infra.Data.Context
{
    public class CursoMvcContext : DbContext
    {
        // Considera a DefaultConnection registrada no Web.config do projeto de apresentação
        // uma vez que este projeto esteja como Startup Project, se não for o caso considerará a 
        // Web.config do projeto que estiver como Startup Project
        public CursoMvcContext() : base("DefaultConnection")
        {
            // Parâmetros para aumentar a performance
            // Se necessário você pode entrar num método específico e habilitar
            // um dos três parâmetros abaixo

            // Não vai criar por padrão um proxy
            Configuration.ProxyCreationEnabled = false;

            // Não vai habilitar o lazy loading
            Configuration.LazyLoadingEnabled = false;

            // Não vai fazer tracking
            Configuration.AutoDetectChangesEnabled = false;
        }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Retirando convenções de geração automática do EF para não colocar nome de entidades no plural e
            //  não colocar comandos de cascata como On Delete Cascade automaticamente
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            // Esta configuração determina que propriedades do tipo texto serão varchar e não nvarchar(que é o dobro em tamanho de varchar)
            modelBuilder.Properties<string>()
                .Configure(p => p.HasColumnType("varchar"));

            // Quando não informado na propriedade determina o valor máximo como 100 e não como max
            modelBuilder.Properties<string>()
                .Configure(p => p.HasMaxLength(100));

            // Define o esquema que será criado as entidades, aqui é o melhor lugar para definir pois será utilizado em todas as entidades
            modelBuilder.HasDefaultSchema("dbo");

            // Obedeçe as instruções deste arquivo e utiliza as instruções dos arquivos de mapping ClienteMapping/EnderecoMapping
            modelBuilder.Configurations.Add(new ClienteMapping());
            modelBuilder.Configurations.Add(new EnderecoMapping());

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            // Aqui definimos que a propriedade DataCadastro somente será incluída com a data/hora atual, e que no momento
            // de alteração da entidade relacionada a propriedade DataCadastro esta propriedade não será enviada ao banco
            // de forma desnecessária, isto valeria para todas as entidades que tiverem esta propriedade DataCadastro, o
            // que é muito legal ! Cuidado para não encher de validações que poderiam tornar mais lento o momento de salvar
            foreach (var entry in ChangeTracker.Entries()
                .Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
            {
                if (entry.State == EntityState.Added)
                    entry.Property("DataCadastro").CurrentValue = DateTime.Now;

                if (entry.State == EntityState.Modified)
                    entry.Property("DataCadastro").IsModified = false;
            }
            return base.SaveChanges();
        }
    }
}
