using Microsoft.EntityFrameworkCore;
using NetCore3.Api.DataAccess.Entities;
using NetCore3.Api.DataAccess.EntityConfig;
using System;


namespace NetCore3.Api.DataAccess
{
    public class MoocDbContext : DbContext, IMoocDbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }

        public MoocDbContext(DbContextOptions options) : base(options) { }

        public MoocDbContext()
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            AuthorConfig.SetEntityBuilder(modelBuilder.Entity<Author>());
            CourseConfig.SetEntityBuilder(modelBuilder.Entity<Course>());
            StudentConfig.SetEntityBuilder(modelBuilder.Entity<Student>());
            StudentCourseConfig.SetEntityBuilder(modelBuilder.Entity<StudentCourse>());

            SeedDummyData(modelBuilder);

            base.OnModelCreating(modelBuilder); 
        }


        private void SeedDummyData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().HasData(
                new Author()
                {
                    Id = Guid.Parse("f11f51ad-355e-4bc6-9c9a-9acfb564bdba"),
                    Name = "Adan",
                    Surname = "Smith",
                    Job = "Computer Science"
                },
                new Author()
                {
                    Id = Guid.Parse("b2fd6b81-3a0e-4e8f-9eed-4dd659049831"),
                    Name = "Ben",
                    Surname = "Anderson",
                    Job = "Software Developer"
                },
                new Author()
                {
                    Id = Guid.Parse("51751abd-82a9-443e-bf6a-1566d3cc0ff4"),
                    Name = "Elliot",
                    Surname = "Simpson",
                    Job = "Scrum Master"
                });

            modelBuilder.Entity<Student>().HasData(
                new Student
                {
                    Id = Guid.Parse("239ad284-edea-41e8-9e28-29fe4030e43f"),
                    Name = "Helen",
                    Surname = "Murray",
                    Birthday = new DateTime(1995, 5, 13)
                },
                new Student
                {
                    Id = Guid.Parse("fcd3a962-bab8-4f29-b9f5-77d60ecfd5e6"),
                    Name = "Bill",
                    Surname = "Mirren",
                    Birthday = new DateTime(2000, 2, 10)
                });
            modelBuilder.Entity<Course>().HasData(
                new Course
                {
                    Id = Guid.Parse("98f83831-f789-45fc-9a12-45f6198bf8ba"), 
                    AuthorId = Guid.Parse("f11f51ad-355e-4bc6-9c9a-9acfb564bdba"), 
                    Title = "Python MOOC", 
                    Description = "Python MOOC",  
                },
                new Course
                {
                    Id = Guid.Parse("b398859a-7fb6-4866-a474-d043bff65e52"),
                    AuthorId = Guid.Parse("51751abd-82a9-443e-bf6a-1566d3cc0ff4"),
                    Title = "Scrum Master Certification",
                    Description = "Scrum Master MOOC",
                });
            modelBuilder.Entity<StudentCourse>().HasData(
                new StudentCourse
                {
                    CourseId = Guid.Parse("b398859a-7fb6-4866-a474-d043bff65e52"),
                    StudentId = Guid.Parse("239ad284-edea-41e8-9e28-29fe4030e43f")
                }); 
        }
    }
}
