using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetCore3.Api.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore3.Api.DataAccess.EntityConfig
{
    public class StudentCourseConfig
    {
        public static void SetEntityBuilder(EntityTypeBuilder<StudentCourse> entityBuilder)
        {
            entityBuilder.ToTable("StudentCourses");
            entityBuilder.HasKey(x => new { x.StudentId, x.CourseId });
            entityBuilder.Property(x => x.StudentId).IsRequired();
            entityBuilder.Property(x => x.CourseId).IsRequired();

            entityBuilder.HasOne(x => x.Student)
                .WithMany(x => x.StudentCourses)
                .HasForeignKey(x => x.StudentId);

            entityBuilder.HasOne(x => x.Course)
                .WithMany(x => x.StudentCourses)
                .HasForeignKey(x => x.CourseId);
        }
    }
}
