using NetCore3.Api.Domain.Models.Course;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore3.Api.Domain.Models.Author
{
    public class AuthorForCreationModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Job { get; set; }
        public ICollection<CourseForCreationModel> Courses { get; set; }
    }
}
