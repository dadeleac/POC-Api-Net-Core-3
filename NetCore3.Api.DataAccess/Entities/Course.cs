using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NetCore3.Api.DataAccess.Entities
{
    public class Course
    {

        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        [ForeignKey("AuthorId")]
        public Author Author { get; set; }

        public Guid AuthorId { get; set; }

        public virtual ICollection<StudentCourse> StudentCourses { get; set; } 
    }
}
