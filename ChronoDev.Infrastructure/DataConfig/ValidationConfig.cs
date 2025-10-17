using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoDev.Domaine.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChronoDev.Infrastructure.DataConfig
{
    public class ValidationConfig:IEntityTypeConfiguration<Validation>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Validation> builder)
        {
            builder.HasKey(v => v.id);
            builder.Property(v => v.dateValidation).IsRequired();
            builder.Property(v => v.Decision).IsRequired().HasMaxLength(50);
            builder.HasOne(v => v.SaisieDeTemps)
                   .WithMany(s => s.Validations)
                   .HasForeignKey(v => v.SaisieDeTempsId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(v => v.Manager)
                   .WithMany(u => u.Validations)
                   .HasForeignKey(v => v.ManagerId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
