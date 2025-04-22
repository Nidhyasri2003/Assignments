using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SISDB.entity;
using SISDB.util;

namespace SISDB.dao
{
    public class TeacherDAO : ITeacherDAO
    {
        public void AddTeacher(Teacher teacher)
        {
            using (SqlConnection con = DBConnUtil.GetConnection())
            {
                string query = "INSERT INTO Teacher VALUES (@id, @fname, @lname, @email)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", teacher.TeacherId);
                cmd.Parameters.AddWithValue("@fname", teacher.FirstName);
                cmd.Parameters.AddWithValue("@lname", teacher.LastName);
                cmd.Parameters.AddWithValue("@email", teacher.Email);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<Teacher> GetAllTeachers()
        {
            List<Teacher> teachers = new List<Teacher>();
            using (SqlConnection con = DBConnUtil.GetConnection())
            {
                string query = "SELECT * FROM Teacher";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Teacher t = new Teacher(
                        (int)reader["teacher_id"],
                        reader["first_name"].ToString(),
                        reader["last_name"].ToString(),
                        reader["email"].ToString()
                    );
                    teachers.Add(t);
                }
            }
            return teachers;
        }
    }
}