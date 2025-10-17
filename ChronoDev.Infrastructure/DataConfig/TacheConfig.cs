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
    public class TacheConfig:IEntityTypeConfiguration<Tache>
    {
        public void Configure(EntityTypeBuilder<Tache> builder)
        {
            builder.HasKey(t => t.id);
            builder.Property(t => t.nom).IsRequired().HasMaxLength(100);
            builder.Property(t => t.dureeEstimee).IsRequired();
            builder.Property(t => t.dateDebut).IsRequired();
            builder.Property(t => t.dateFin).IsRequired();
            builder.HasOne(t => t.Projet)
                   .WithMany(p => p.Taches)
                   .HasForeignKey(t => t.ProjetId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
