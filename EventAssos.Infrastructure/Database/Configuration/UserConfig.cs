using EventAssos.Domain.Entities;
using EventAssos.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace EventAssos.Infrastructure.Database.Configuration
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public static readonly Guid AdminId = Guid.Parse("e1f2a3b4-c5d6-4e7f-8a9b-0c1d2e3f4a5b");
        public void Configure(EntityTypeBuilder<User> builder)
        {

            builder.ToTable(t =>
            t.HasCheckConstraint("CK_User_Email_Format", "Email LIKE '%_@%_.%_'"));

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                .ValueGeneratedOnAdd();

            builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(255);

            builder.HasIndex(u => u.Email)
            .IsUnique();

            builder.Property(u => u.Pseudo)
            .HasMaxLength(50);

            builder.HasIndex(u => u.Pseudo)
            .IsUnique()
            .HasFilter("[Pseudo] IS NOT NULL"); //Le pseudo doit être unique, SAUF s'il est nul

            builder.Property(u => u.Password)
            .HasMaxLength(255);

            builder.Property(u => u.Birthdate);

            builder.Property(u => u.UserGender);

            builder.Property(u => u.Role);

            builder.HasData(
              new User
              {
                  Id = AdminId,
                  Pseudo = "MmeDupont",
                  Email = "dupont@admin.com",
                  Password = "1/cmKqS67O6WI/bjc6BKIklFI9YkiiVgLqooeeLsCwCkaE7s4QJNPDlks+3R8trv", 
                  Role = UserRole.Admin,
                  Birthdate = new DateOnly(1980, 5, 15),

              });

        }
    }
}
