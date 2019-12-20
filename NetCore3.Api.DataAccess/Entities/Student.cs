using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore3.Api.DataAccess.Entities
{
    public class Student
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTimeOffset Birthday { get; set; }
        public ICollection<StudentCourse> StudentCourses { get; set; }
    }
}
