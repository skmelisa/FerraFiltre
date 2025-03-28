using ferraFiltre.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using ferraFiltre.Data;


namespace ferraFiltre.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<FerraOrjinalMuadil> FerraOrjinalMuadil{ get; set; }
        public DbSet<Filtreler> Filtreler { get; set; }
        public DbSet<CrossReferenceResult> CrossReferenceResults { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Tuple içeren modelleri ignore et
            modelBuilder.Ignore<List<ValueTuple<string, string>>>();
        }
    }
}
