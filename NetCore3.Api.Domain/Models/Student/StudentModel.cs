using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore3.Api.Domain.Models.Student
{
    public class StudentModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
    }
}
