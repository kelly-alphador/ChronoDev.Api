using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoDev.Domaine.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChronoDev.Infrastructure.Context
{
    public class ApplicationDbContext:IdentityDbContext<ApplicationUser, IdentityRole<int>,int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Projet> Projets { get; set; }
        public DbSet<Tache> Taches { get; set; }
        public DbSet<SaisieTemps> SaisiesTemps { get; set; }
        public DbSet<Validation> Validations { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new DataConfig.ProjectConfig());
            builder.ApplyConfiguration(new DataConfig.TacheConfig());
            builder.ApplyConfiguration(new DataConfig.SaisieTempsConfig());
            builder.ApplyConfiguration(new DataConfig.ValidationConfig());

            //SEEDING=permet de mettre des valeur initial dans la table lors de la migrations
            //HasData : sert a mettre des donnees initial dans une table de base de donnees
            builder.Entity<IdentityRole<int>>().HasData(
                new IdentityRole<int> { Id = 1, Name = "Manager", NormalizedName = "MANAGER" ,ConcurrencyStamp=Guid.NewGuid().ToString() },
                new IdentityRole<int> { Id = 2, Name = "ChefProjet", NormalizedName = "CHEFPROJET" , ConcurrencyStamp = Guid.NewGuid().ToString() },
                new IdentityRole<int> { Id = 3, Name = "Developpeur", NormalizedName = "DEVELOPPEUR", ConcurrencyStamp = Guid.NewGuid().ToString() }
            );
        }
    }
}
