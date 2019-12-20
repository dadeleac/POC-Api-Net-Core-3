using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetCore3.Api.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore3.Api.DataAccess.EntityConfig
{
    public class AuthorConfig
    {
        public static void SetEntityBuilder(EntityTypeBuilder<Author> entityBuilder)
        {
            entityBuilder.ToTable("Author");
            entityBuilder.HasKey(x => x.Id); 
            entityBuilder.Property(x => x.Id).IsRequired();

            entityBuilder.HasMany(x => x.Courses)
                .WithOne(x => x.Author)
                .HasForeignKey(x => x.AuthorId)
                .IsRequired();
        }
    }
}
