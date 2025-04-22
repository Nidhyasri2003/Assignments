using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISDB.entity
{
    public class Course
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int Credits { get; set; }
        public int? TeacherId { get; set; } // Nullable if no teacher assigned yet

        public Course(int courseId, string courseName, int credits, int? teacherId)
        {
            CourseId = courseId;
            CourseName = courseName;
            Credits = credits;
            TeacherId = teacherId;
        }
    }
}