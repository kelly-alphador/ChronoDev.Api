using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoDev.Domaine.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChronoDev.Infrastructure.DataConfig
{
    public class ProjectConfig:IEntityTypeConfiguration<Projet>
    {
        public void Configure(EntityTypeBuilder<Projet> builder)
        {
            builder.HasKey(p => p.id);
            builder.Property(p => p.nom).IsRequired().HasMaxLength(100);
            builder.Property(p => p.dateCreation).IsRequired();
            builder.Property(p => p.dureeEstimee).IsRequired();
            builder.Property(p => p.dateFin).IsRequired();
            builder.HasOne(p => p.Manager)
                   .WithMany(u => u.Projets)
                   .HasForeignKey(p => p.ManagerId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
