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
    public class SaisieTempsConfig:IEntityTypeConfiguration<SaisieTemps>
    {
        public void Configure(EntityTypeBuilder<SaisieTemps> builder)
        {
            builder.HasKey(s => s.id);
            builder.Property(s => s.dateSaisie).IsRequired();
            builder.Property(s => s.heure_deb).IsRequired();
            builder.Property(s => s.heure_fin).IsRequired();
            builder.Property(s => s.commentaire).HasMaxLength(500);
            builder.HasOne(s => s.Tache)
                   .WithMany(t => t.SaisiesTemps)
                   .HasForeignKey(s => s.TacheId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(s => s.Utilisateur)
                   .WithMany(u => u.SaisiesTemps)
                   .HasForeignKey(s => s.UtilisateurId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
