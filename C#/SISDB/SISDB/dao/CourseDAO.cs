using SISDB.dao;
using SISDB.entity;
using SISDB.util;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace SISProject.dao
{
    public class CourseDAO : ICourseDAO
    {
        public void AddCourse(Course course)
        {
            using (SqlConnection con = DBConnUtil.GetConnection())
            {
                string query = "INSERT INTO Courses VALUES (@id, @name, @credits, @teacherId)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", course.CourseId);
                cmd.Parameters.AddWithValue("@name", course.CourseName);
                cmd.Parameters.AddWithValue("@credits", course.Credits);

                // Fix for C# 7.3: avoid using object?
                if (course.TeacherId.HasValue)
                {
                    cmd.Parameters.AddWithValue("@teacherId", course.TeacherId.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@teacherId", DBNull.Value);
                }

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<Course> GetAllCourses()
        {
            List<Course> courses = new List<Course>();
            using (SqlConnection con = DBConnUtil.GetConnection())
            {
                string query = "SELECT * FROM Courses";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int? teacherId = reader["teacher_id"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["teacher_id"]);

                    Course course = new Course(
                        Convert.ToInt32(reader["course_id"]),
                        reader["course_name"].ToString(),
                        Convert.ToInt32(reader["credits"]),
                        teacherId
                    );

                    courses.Add(course);
                }
            }
            return courses;
        }
    }
}
