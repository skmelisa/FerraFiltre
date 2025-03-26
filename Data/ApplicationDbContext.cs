using ferraFiltre.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ferraFiltre.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<FerraOrjinalMuadil> FiltrelerIlk { get; set; }
        //public DbSet<Filtreler> FiltersTwo { get; set; }


    }
}
