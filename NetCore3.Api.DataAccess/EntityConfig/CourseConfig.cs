using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetCore3.Api.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore3.Api.DataAccess.EntityConfig
{
    public class CourseConfig
    {
        public static void SetEntityBuilder(EntityTypeBuilder<Course> entityBuilder)
        {
            entityBuilder.ToTable("Courses"); 
            entityBuilder.HasKey(x => x.Id);
            entityBuilder.Property(x => x.Id).IsRequired();

            entityBuilder.HasOne(x => x.Author)
                .WithMany(x => x.Courses)
                .HasForeignKey(x => x.AuthorId)
                .IsRequired(); 
        }
    }
}
