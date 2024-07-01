using System.ComponentModel.DataAnnotations;

namespace WebEvaluation.Models
{
    public enum Grade
    {
        A, B, C, D, F
    }

    public class _Enrollment
    {
        [Key]
        public int EnrollmentID { get; set; }

        public int CourseID { get; set; }

        public string PersonCD { get; set; }

        [DisplayFormat(NullDisplayText = "No grade")]

        public Grade? Grade { get; set; }

        public virtual _Course Course { get; set; }

        public virtual M_Staff Student { get; set; }
    }
}