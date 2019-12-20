using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using NetCore3.Api.Application.Contracts;
using NetCore3.Api.Domain.Models.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore3.Api.Controllers
{
    [Route("api/[controller]/{authorId}/courses")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet(Name = "GetCourseForAuthor")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<IEnumerable<CourseModel>>> GetCourses()
        {
            var courses = await _courseService.GetCourses()
                .ConfigureAwait(false);

            if (courses == null)
            {
                return NotFound();
            }
            return Ok(courses);
        }


        [HttpGet("{courseId}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<CourseModel>> GetCourseForAuthor(Guid authorId, Guid courseId)
        {
            var course = await _courseService.GetCourseForAuthor(authorId, courseId)
                .ConfigureAwait(false);

            if (course == null)
            {
                return NotFound();
            }

            return Ok(course);

        }

        [HttpPost]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult<CourseModel>> CreateCourseForAuthor(Guid authorId, CourseForCreationModel course)
        {

            var createCourse = await _courseService.CreateCourseForAuthor(authorId, course)
                .ConfigureAwait(false);

            if(createCourse == null)
            {
                return NotFound(); 
            }

            return CreatedAtRoute("GetCourseForAuthor",
            new { authorId, courseId = createCourse.Id },
            createCourse);
        }

        [HttpPut]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public async Task<ActionResult> UpdateCourseForAuthor(Guid authorId, Guid courseId, CourseForUpdateModel course)
        {
            var courseUpdated = await _courseService.UpdateCourseForAuthor(authorId, courseId, course)
                .ConfigureAwait(false);

            if(courseUpdated == null)
            {
                return BadRequest(); 
            }

            return NoContent(); 
        }

        [HttpDelete("{courseId}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Delete))]
        public async Task<ActionResult> DeleteCourseForAuthor(Guid authorId, Guid courseId)
        {
            if(!await _courseService.DeleteCourseForAuthor(authorId, courseId).ConfigureAwait(false))
            {
                return NotFound(); 
            }

            return NoContent(); 
        }

        [HttpPatch]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Update))]
        public async Task<ActionResult> PartialUptadeCourseForAuthor(Guid authorId, Guid courseId, 
            JsonPatchDocument<CourseForUpdateModel> patchDocument)
        {
            var courseUpdated = await _courseService.PartialUptadeCourseForAuthor(authorId, courseId, patchDocument)
                .ConfigureAwait(false); 

            if(courseUpdated == null)
            {
                return BadRequest(); 
            }

            return NoContent(); 
        }

    }
}
