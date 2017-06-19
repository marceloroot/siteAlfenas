using Site2016.Dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site2016.InfraEstrutura
{
    public class AppContexto:DbContext
    {

        static AppContexto()
        {
            // Not initialize database
            //  Database.SetInitializer<ProjectDatabase>(null);
            // Database initialize
            Database.SetInitializer<AppContexto>(new DbInitializer());
            using (AppContexto db = new AppContexto())
                db.Database.Initialize(false);
        }
        //public AppContexto()
        //    : base("AppContexto")
        //{
        //    //Configuration.ProxyCreationEnabled = false;
        //    //Configuration.LazyLoadingEnabled = false;
        //    var copiaDLL = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        //}
            public DbSet<Usuario> Usuario { get; set; }
            public DbSet<Arquivo> Arquivo { get; set; }
            public DbSet<Imagem> Imagem { get; set; }
            public DbSet<Jari> Jari { get; set; }
            public DbSet<Noticia> Noticia { get; set; }
            public DbSet<Permissao> Permissao { get; set; }
            public DbSet<Secretaria> Secretaria { get; set; }
            public DbSet<Telefone> Telefone { get; set; }
            public DbSet<TipoArquivo> TipoArquivo { get; set; }
            public DbSet<TipoImagem> TipoImagem { get; set; }
            public DbSet<TipoJari> TipoJari { get; set; }
            public DbSet<TipoNoticia> TipoNoticia { get; set; }


            protected override void OnModelCreating(DbModelBuilder modelBuilder)
            {
                modelBuilder.Entity<TipoImagem>().Property(a => a.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
                modelBuilder.Entity<TipoArquivo>().Property(a => a.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
                modelBuilder.Entity<TipoNoticia>().Property(a => a.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
                modelBuilder.Entity<TipoJari>().Property(a => a.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);


            }

            class DbInitializer : CreateDatabaseIfNotExists<AppContexto>
            {
                protected override void Seed(AppContexto context)
                {


                    base.Seed(context);
                }
            }

        
        
    }
}
