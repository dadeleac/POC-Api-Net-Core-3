using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore3.Api.DataAccess.Entities
{
    public class Author
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Job { get; set; }
        public ICollection<Course> Courses { get; set; } 
    }
}
