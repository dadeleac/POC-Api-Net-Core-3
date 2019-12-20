using NetCore3.Api.Domain.Models.Course;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NetCore3.Api.Domain.ValidationAttributes
{
    public class CourseTitleMustBeDifferentFromDescription : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var course = (CourseValidationModel)validationContext.ObjectInstance;

            if (course.Title == course.Description)
            {
                return new ValidationResult(ErrorMessage,
                   new[] { nameof(CourseValidationModel) });
            }

            return ValidationResult.Success;
        }
    }
}
