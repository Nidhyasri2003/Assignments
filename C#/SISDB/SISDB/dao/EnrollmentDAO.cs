using SISDB.entity;
using SISDB.util;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISDB.dao
{
    public class EnrollmentDAO : IEnrollmentDAO
    {
        public void EnrollStudent(Enrollment enrollment)
        {
            using (SqlConnection con = DBConnUtil.GetConnection())
            {
                string query = "INSERT INTO Enrollments VALUES (@id, @studentId, @courseId, @date)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", enrollment.EnrollmentId);
                cmd.Parameters.AddWithValue("@studentId", enrollment.StudentId);
                cmd.Parameters.AddWithValue("@courseId", enrollment.CourseId);
                cmd.Parameters.AddWithValue("@date", enrollment.EnrollmentDate);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<Enrollment> GetAllEnrollments()
        {
            List<Enrollment> enrollments = new List<Enrollment>();
            using (SqlConnection con = DBConnUtil.GetConnection())
            {
                string query = "SELECT * FROM Enrollments";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Enrollment e = new Enrollment(
                        (int)reader["enrollment_id"],
                        (int)reader["student_id"],
                        (int)reader["course_id"],
                        (DateTime)reader["enrollment_date"]
                    );
                    enrollments.Add(e);
                }
            }
            return enrollments;
        }
    }
}