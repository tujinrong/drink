using System.Collections.Generic;
using WebEvaluation.Models;

namespace WebEvaluation.ViewModels
{
    public class InstructorIndexData
    {
        public IEnumerable<_Instructor> Instructors { get; set; }
        public IEnumerable<_Course> Courses { get; set; }
        public IEnumerable<_Enrollment> Enrollments { get; set; }
    }
}