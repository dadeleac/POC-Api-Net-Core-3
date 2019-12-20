using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetCore3.Api.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore3.Api.DataAccess.EntityConfig
{
    public class StudentConfig
    {
        public static void SetEntityBuilder(EntityTypeBuilder<Student> entityBuilder)
        {
            entityBuilder.ToTable("Students");
            entityBuilder.HasKey(x => x.Id);
            entityBuilder.Property(x => x.Id).IsRequired();
        }
    }
}
