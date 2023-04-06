using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.DTO.Common;
using Web.Models.Response;

namespace Web.Services.Interfaces
{
    public interface ICourseService
    {
        BaseResponse SaveCourse(CourseDTO course);

        IQueryable<CourseDTO> GetAllCourse();

        BaseResponse GetCourseDetails(int CourseId);
    }
}
