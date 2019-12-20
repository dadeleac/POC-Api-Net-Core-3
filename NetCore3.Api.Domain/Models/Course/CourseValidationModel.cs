using NetCore3.Api.Domain.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NetCore3.Api.Domain.Models.Course
{
    [CourseTitleMustBeDifferentFromDescription(
        ErrorMessage = "Title must be different from description.")]
    public abstract class CourseValidationModel
    {
        public string Title { get; set; }
        public virtual string Description { get; set; }
    }
}
