using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore3.Api.Domain.Models.Course
{
    public class CourseModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public Guid AuthorId { get; set; }

    }
}
