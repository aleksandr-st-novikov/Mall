using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class MallDBContext : DbContext
    {
        public MallDBContext()
            : base("MallDBContext")
        {
        }

        public DbSet<Template> Template { get; set; }
        public DbSet<TemplateTable> TemplateTable { get; set; }
        public DbSet<SettingDoc> SettingDoc { get; set; }
        public DbSet<Document> Document { get; set; }
        public DbSet<DocumentTable> DocumentTable { get; set; }
        public DbSet<Brand> Brand { get; set; }
        public DbSet<Dictionary> Dictionary { get; set; }
        public DbSet<DictionaryTable> DictionaryTable { get; set; }
        public DbSet<Setting> Setting { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
