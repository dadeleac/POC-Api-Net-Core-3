using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NetCore3.Api.Domain.Models.Course
{
    public class CourseForUpdateModel : CourseValidationModel
    {
        //[Required(ErrorMessage = "Description is mandatory.")]
        public override string Description { get => base.Description; set => base.Description = value; }
    }
}
